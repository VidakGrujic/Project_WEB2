using AutoMapper;
using Projekat_WEB2_backend.Dto;
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

        public PorudzbinaDto AddPorudzbina(PorudzbinaDto newPorudzbinaDto)
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
            _dbContext.SaveChanges(); //mora save changes da bi porudzbina dobila id

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
                        _dbContext.SaveChanges();
                        return null;
                    }

                    if (artikal.Kolicina < artikalPorudzbineDto.Kolicina)
                    {
                        //stavim da se porudzbina izvbaci ako se trazi vise nego sto ima
                        _dbContext.Porudzbine.Remove(newPorudzbina);
                        _dbContext.SaveChanges();
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

                _dbContext.SaveChanges();

                PorudzbinaDto returnPorudzbinaDto = _mapper.Map<PorudzbinaDto>(newPorudzbina);
                return returnPorudzbinaDto;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                //obrisati porudzbinu
                PorudzbinaHelperClass.ReturnKolicinaArtikalaPorudzbine(newPorudzbinaDto.ArtikliPorudzbine, _dbContext);
                _dbContext.Porudzbine.Remove(newPorudzbina);
                _dbContext.SaveChanges();
                return null;
            }
           
        }

        public void DeletePorudzbina(long id)
        {
            Porudzbina deletePorudzbina = _dbContext.Porudzbine.Find(id);
            _dbContext.Porudzbine.Remove(deletePorudzbina);

            _dbContext.SaveChanges();
        }

        public List<PorudzbinaDto> GetAllPorudzbina()
        {
            return _mapper.Map<List<PorudzbinaDto>>(_dbContext.Porudzbine.ToList());
        }

        public PorudzbinaDto GetPorudzbinaById(long id)
        {
            return _mapper.Map<PorudzbinaDto>(_dbContext.Porudzbine.Find(id));
        }

        public PorudzbinaDto UpdatePorudzbina(long id, PorudzbinaDto updatePorudzbinaDto)
        {
            Porudzbina updatePorudzbina = _dbContext.Porudzbine.Find(id);
            PorudzbinaHelperClass.UpdatePorudzbinaFields(updatePorudzbina, updatePorudzbinaDto);

            _dbContext.SaveChanges();

            return _mapper.Map<PorudzbinaDto>(updatePorudzbina);
        }
    }
}
