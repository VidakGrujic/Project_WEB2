using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class ArtikalService : IArtikalService
    {
        private readonly IMapper _mapper;
        private readonly ProdavnicaDbContext _dbContext;

        public ArtikalService(IMapper mapper, ProdavnicaDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        
        public async Task<ArtikalDto> AddArtikal(ArtikalDto newArtikalDto)
        {
            if (!ArtikalHelperClass.IsArtikalFieldsValid(newArtikalDto))
                return null;
            if (_dbContext.Korisnici.Find(newArtikalDto.ProdavacId) == null)
                return null;

            Artikal newArtikal = _mapper.Map<Artikal>(newArtikalDto);
            newArtikal.Prodavac = _dbContext.Korisnici.Find(newArtikalDto.ProdavacId);
            newArtikal.CenaDostave = _dbContext.Korisnici.Find(newArtikalDto.ProdavacId).CenaDostave;
            _dbContext.Artikli.Add(newArtikal);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ArtikalDto>(newArtikal);
        }

        public async Task<bool> DeleteArtikal(long id)
        {
            Artikal deleteArtikal = _dbContext.Artikli.Find(id);
            if(deleteArtikal == null)
            {
                return false;
            }
            _dbContext.Artikli.Remove(deleteArtikal);

            _dbContext.ArtikliUPorudzbinama.RemoveRange(_dbContext.ArtikliUPorudzbinama.Where(x => x.ArtikalId == deleteArtikal.Id));
            await _dbContext.SaveChangesAsync();
            return true;

        }

        public async Task<List<ArtikalDto>> GetAllArtikals()
        {
            List<ArtikalDto> artikalList = _mapper.Map<List<ArtikalDto>>(await _dbContext.Artikli.ToListAsync());
            return artikalList;
        }

        public async Task<ArtikalDto> GetArtikalById(long id)
        {
            Artikal artikal = await _dbContext.Artikli.FindAsync(id);
            if(artikal == null)
            {
                return null;
            }
            return _mapper.Map<ArtikalDto>(artikal);
        }

        public async Task<ArtikalDto> UpdateArtikal(long id, ArtikalDto updateArtikalDto)
        {
            Artikal updateArtikal = await _dbContext.Artikli.FindAsync(id);
            if(updateArtikal == null)
            {
                return null;
            }
            ArtikalHelperClass.UpdateArtikalFiels(updateArtikal, updateArtikalDto);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ArtikalDto>(updateArtikal);
            
        }


        public async Task<List<ArtikalDto>> GetProdavceveArtikle(long id)
        {
            Korisnik prodavac = await _dbContext.Korisnici.Include(x => x.ProdavceviArtikli).FirstOrDefaultAsync(x=> x.Id == id);
            if(prodavac == null)
            {
                return null;
            }

            return _mapper.Map<List<ArtikalDto>>(prodavac.ProdavceviArtikli);
        }


    }
}
