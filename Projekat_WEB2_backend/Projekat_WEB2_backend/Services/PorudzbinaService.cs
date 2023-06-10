using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Projekat_WEB2_backend.Dto;
using Projekat_WEB2_backend.Enumerations;
using Projekat_WEB2_backend.Helper_Classes;
using Projekat_WEB2_backend.Infrastructure;
using Projekat_WEB2_backend.Interfaces;
using Projekat_WEB2_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Services
{
    public class PorudzbinaService : IPorudzbinaService
    {
        private readonly IMapper _mapper;
        private readonly ProdavnicaDbContext _dbContext;

        public PorudzbinaService(IMapper mapper, ProdavnicaDbContext dbContext)
        {
           
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<PorudzbinaDto> AddPorudzbina(PorudzbinaDto newPorudzbinaDto)
        {
            
            //ako je lista artikala prazna, onda ne moze da se napravi porudzbina
            if (newPorudzbinaDto.ArtikliPorudzbine == null)
            {
                return null;
            }
            //potom napraviti novu porudzbinu i njoj dodeliti ove elemente
            Porudzbina newPorudzbina = new Porudzbina
            {
                Adresa = newPorudzbinaDto.Adresa,
                Komentar = newPorudzbinaDto.Komentar,
                CenaPorudzbine = newPorudzbinaDto.CenaPorudzbine,
                StanjePorudzbine = newPorudzbinaDto.StanjePorudzbine,
                DatumKreiranja = DateTime.Now,
                KorisnikId = newPorudzbinaDto.KorisnikId,
                Korisnik = _dbContext.Korisnici.Find(newPorudzbinaDto.KorisnikId),
                ArtikliPorudzbine = new List<ArtikalPorudzbine>()
            };

            //ostalo mi vreme dostave 
            //generisem neki broj izmedju 2 i 12, jer vreme dostave mora biti vece od 1h
            Random hoursGenerator = new Random();
            int additionalHours = hoursGenerator.Next(2, 12);
            newPorudzbina.DatumDostave = DateTime.Now.AddHours(additionalHours);


            //porudzbini podesiti vreme dostave i datum kreiranja
            //ubaciti porudzbinu u db set
            _dbContext.Porudzbine.Add(newPorudzbina);
            await _dbContext.SaveChangesAsync(); //mora save changes da bi porudzbina dobila id

            //sve ovo mora da stavim u try, jer u slucaju da pukne, porudzbina mora da se obrise iz baze
            //mora da napravim transkacionu operaciju

            try
            {
                //ostali mi artikili u porudzbini
                foreach (ArtikalPorudzbineDto artikalPorudzbineDto in newPorudzbinaDto.ArtikliPorudzbine)
                {
                    //pronaci artikal koji odgovara artiklu porudzbine
                    Artikal artikal = _dbContext.Artikli.Find(artikalPorudzbineDto.ArtikalId);

                    //ako artikal ne postoji vrati null
                    if (artikal == null)
                    {
                        //stavim da se porudzbina izvbaci ako nije uspelo pronalazenje artikla
                        _dbContext.Porudzbine.Remove(newPorudzbina);
                        await _dbContext.SaveChangesAsync();
                        return null;
                    }

                    if (artikal.Kolicina < artikalPorudzbineDto.Kolicina)
                    {
                        //stavim da se porudzbina izvbaci ako se trazi vise nego sto ima
                        _dbContext.Porudzbine.Remove(newPorudzbina);
                        await _dbContext.SaveChangesAsync();
                        return null;
                    }
                    //skinuti kolicinu artikala kolko je poruceno
                    artikal.Kolicina -= artikalPorudzbineDto.Kolicina;

                    ArtikalPorudzbine artikalPorudzbine = new ArtikalPorudzbine
                    {
                        ArtikalId = artikal.Id,
                        Kolicina = artikalPorudzbineDto.Kolicina,
                        PorudzbinaId = newPorudzbina.Id,
                        Porudzbina = newPorudzbina,
                    };
                    //dodati svaki porudzbinaArtikal u db set
                    newPorudzbina.ArtikliPorudzbine.Add(artikalPorudzbine);
                    _dbContext.ArtikliUPorudzbinama.Add(artikalPorudzbine);
                }

                //kad je napravljena porudzbina, onda nju treba dodati u kupcu i na kraju konacno sacuvati svae changes
                //mozda ne treba da se ovo uradi jer je uradjen svae changes, on je vec sacuvao korisniku na osnovu ID porudbinu
                //_dbContext.Korisnici.Find(newPorudzbinaDto.KorisnikId).Porudzbine.Add(newPorudzbina);

                await _dbContext.SaveChangesAsync();

                PorudzbinaDto returnPorudzbinaDto = _mapper.Map<PorudzbinaDto>(newPorudzbina);
                return returnPorudzbinaDto;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                //obrisati porudzbinu
                PorudzbinaHelperClass.ReturnKolicinaArtikalaPorudzbine(newPorudzbinaDto.ArtikliPorudzbine, _dbContext);
                _dbContext.Porudzbine.Remove(newPorudzbina);
                await _dbContext.SaveChangesAsync();  
                return null;
            }
           
        }

        public async Task DeletePorudzbina(long id)
        {
            Porudzbina deletePorudzbina = await _dbContext.Porudzbine.FindAsync(id);
            _dbContext.Porudzbine.Remove(deletePorudzbina);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<PorudzbinaDto>> GetAllPorudzbina()
        {
            return _mapper.Map<List<PorudzbinaDto>>(await _dbContext.Porudzbine.ToListAsync());
        }

        public async Task<List<PorudzbinaDto>> GetKupcevePorudzbine(long id)
        {
            //moraju sugavi includovi zbog lazy loadinga
            Korisnik kupac = await  _dbContext.Korisnici.Include(x => x.Porudzbine)
                                                 .ThenInclude(porudzbina => porudzbina.ArtikliPorudzbine)
                                                 .FirstOrDefaultAsync(x => x.Id == id);
            if(kupac == null)
            {
                return null;
            }


            //ovde treba da se stavi provera da li je porudzbina otkazana ili ne
            List<PorudzbinaDto> kupcevePorudzbineDto = _mapper.Map<List<PorudzbinaDto>>(kupac.Porudzbine.FindAll(x => x.StanjePorudzbine != StanjePorudzbine.Otkazana));


            return kupcevePorudzbineDto;           

        }

        public async Task<PorudzbinaPrikazDto> GetPorudzbinaById(long id)
        {
            Porudzbina porudzbinaPrikaz = await _dbContext.Porudzbine.Include(x => x.ArtikliPorudzbine).FirstOrDefaultAsync(x => x.Id == id);
            if(porudzbinaPrikaz == null)
            {
                return null;
            }

            PorudzbinaPrikazDto porudzbinaPrikazDto = _mapper.Map<PorudzbinaPrikazDto>(porudzbinaPrikaz);
            porudzbinaPrikazDto.ImenaArtikala = new List<string>();

            foreach(ArtikalPorudzbine artikalPorudzbine in porudzbinaPrikaz.ArtikliPorudzbine)
            {
                Artikal praviArtikal = _dbContext.Artikli.Find(artikalPorudzbine.ArtikalId);
                if(praviArtikal == null)
                {
                    return null;
                }
                porudzbinaPrikazDto.ImenaArtikala.Add(praviArtikal.Naziv);
            }

            return porudzbinaPrikazDto;
        }

        public async Task<PorudzbinaDto> UpdatePorudzbina(long id, PorudzbinaDto updatePorudzbinaDto)
        {
            Porudzbina updatePorudzbina = _dbContext.Porudzbine.Find(id);
            PorudzbinaHelperClass.UpdatePorudzbinaFields(updatePorudzbina, updatePorudzbinaDto);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<PorudzbinaDto>(updatePorudzbina);
        }

        public async Task<ResponsePorudzbinaDto> OtkaziPorudzbinu(long id, string statusVerifikacije)
        {
            try
            {
                Porudzbina otkazivanjePorudzbina =  await _dbContext.Porudzbine.Include(x => x.ArtikliPorudzbine).FirstOrDefaultAsync(x => x.Id == id);
                if((DateTime.UtcNow - otkazivanjePorudzbina.DatumKreiranja).TotalHours < 1 && statusVerifikacije == "Otkazana")
                {
                    otkazivanjePorudzbina.StanjePorudzbine = StanjePorudzbine.Otkazana;

                    foreach (ArtikalPorudzbine artikalPorudzbine in otkazivanjePorudzbina.ArtikliPorudzbine)
                    {
                        Artikal praviArtikal = _dbContext.Artikli.Find(artikalPorudzbine.ArtikalId);
                        if (praviArtikal != null)
                        {
                            praviArtikal.Kolicina += artikalPorudzbine.Kolicina;
                        }
                    }

                    ResponsePorudzbinaDto otkazanaPorudzbinaResponseDto = new ResponsePorudzbinaDto
                    {
                        Message = "Uspesno otkazana porudzbina",
                        PorudzbinaDto = _mapper.Map<PorudzbinaDto>(otkazivanjePorudzbina)
                    };

                    await _dbContext.SaveChangesAsync();
                    return otkazanaPorudzbinaResponseDto;
                } 
                else
                {
                    return new ResponsePorudzbinaDto { PorudzbinaDto = null, Message = "Nije moguce otkazati porudzbinu nakon sat vremena" };
                }
            }
            catch(Exception e)
            {
               
                Console.WriteLine(e.Message);
                /*
                otkazivanjePorudzbina.StanjePorudzbine = StanjePorudzbine.Prihvacena;

                foreach (ArtikalPorudzbine artikalPorudzbine in otkazivanjePorudzbina.ArtikliPorudzbine)
                {
                    Artikal praviArtikal = _dbContext.Artikli.Find(artikalPorudzbine.ArtikalId);
                    if (praviArtikal != null)
                    {
                        praviArtikal.Kolicina -= artikalPorudzbine.Kolicina;
                    }
                }*/
                return new ResponsePorudzbinaDto { PorudzbinaDto = null, Message = "Desio se neki problem" };
            }
           


        }

        public async Task<List<PorudzbinaDto>> GetProdavceveNovePorudzbine(long id)
        {
            try
            {
                Korisnik prodavac = await _dbContext.Korisnici.Include(x => x.ProdavceviArtikli).FirstAsync(x => x.Id == id);
                List<Porudzbina> prodavcevePorudzbine = new List<Porudzbina>();

                foreach(Artikal prodavcevArtikal in prodavac.ProdavceviArtikli)
                {
                    foreach(ArtikalPorudzbine prodavcevArtikalPorudzbine in _dbContext.ArtikliUPorudzbinama.Include(x => x.Porudzbina).ToList())
                    {
                        //ako je datum isporuke porudzbine veci od trenutnog, i ako je stanje porudzbine nije otkazana i ako porudzbina vec nije ubacena tu
                        if (prodavcevArtikalPorudzbine.ArtikalId == prodavcevArtikal.Id
                            && prodavcevArtikalPorudzbine.Porudzbina.DatumDostave > DateTime.Now
                            && prodavcevArtikalPorudzbine.Porudzbina.StanjePorudzbine != StanjePorudzbine.Otkazana 
                            && !prodavcevePorudzbine.Contains(prodavcevArtikalPorudzbine.Porudzbina))
                        {
                            prodavcevePorudzbine.Add(prodavcevArtikalPorudzbine.Porudzbina);
                        }
                    }
                }
                return _mapper.Map<List<PorudzbinaDto>>(prodavcevePorudzbine);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<List<PorudzbinaDto>> GetProdavcevePrethodnePorudzbine(long id)
        {
            try
            {
                Korisnik prodavac = await _dbContext.Korisnici.Include(x => x.ProdavceviArtikli).FirstAsync(x => x.Id == id);
                List<Porudzbina> prodavcevePorudzbine = new List<Porudzbina>();

                foreach (Artikal prodavcevArtikal in prodavac.ProdavceviArtikli)
                {
                    foreach (ArtikalPorudzbine prodavcevArtikalPorudzbine in _dbContext.ArtikliUPorudzbinama.Include(x => x.Porudzbina).ToList())
                    {
                        //ako je datum isporuke porudzbine veci od trenutnog, i ako je stanje porudzbine nije otkazana i ako porudzbina vec nije ubacena tu
                        if (prodavcevArtikalPorudzbine.ArtikalId == prodavcevArtikal.Id
                            && prodavcevArtikalPorudzbine.Porudzbina.DatumDostave < DateTime.Now
                            && prodavcevArtikalPorudzbine.Porudzbina.StanjePorudzbine != StanjePorudzbine.Otkazana
                            && !prodavcevePorudzbine.Contains(prodavcevArtikalPorudzbine.Porudzbina))
                        {
                            prodavcevePorudzbine.Add(prodavcevArtikalPorudzbine.Porudzbina);
                        }
                    }
                }
                return _mapper.Map<List<PorudzbinaDto>>(prodavcevePorudzbine);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
