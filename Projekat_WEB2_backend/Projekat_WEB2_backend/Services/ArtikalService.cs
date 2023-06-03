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
    public class ArtikalService : IArtikalService
    {
        private readonly IMapper _mapper;
        private readonly ProdavnicaDbContext _dbContext;

        public ArtikalService(IMapper mapper, ProdavnicaDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        
        public ArtikalDto AddArtikal(ArtikalDto newArtikalDto)
        {
            if (!ArtikalHelperClass.IsArtikalFieldsValid(newArtikalDto))
                return null;
            if (_dbContext.Korisnici.Find(newArtikalDto.ProdavacId) == null)
                return null;

            Artikal newArtikal = _mapper.Map<Artikal>(newArtikalDto);
            newArtikal.Prodavac = _dbContext.Korisnici.Find(newArtikalDto.ProdavacId);
            newArtikal.CenaDostave = _dbContext.Korisnici.Find(newArtikalDto.ProdavacId).CenaDostave;
            _dbContext.Artikli.Add(newArtikal);

            _dbContext.SaveChanges();

            return _mapper.Map<ArtikalDto>(newArtikal);
        }

        public void DeleteArtikal(long id)
        {
            Artikal deleteArtikal = _dbContext.Artikli.Find(id);
            _dbContext.Artikli.Remove(deleteArtikal);
            _dbContext.SaveChanges();

        }

        public List<ArtikalDto> GetAllArtikals()
        {
            List<ArtikalDto> artikalList = _mapper.Map<List<ArtikalDto>>(_dbContext.Artikli.ToList());
            return artikalList;
        }

        public ArtikalDto GetArtikalById(long id)
        {
            return _mapper.Map<ArtikalDto>(_dbContext.Artikli.Find(id));
        }

        public ArtikalDto UpdateArtikal(long id, ArtikalDto updateArtikalDto)
        {
            Artikal updateArtikal = _dbContext.Artikli.Find(id);
            ArtikalHelperClass.UpdateArtikalFiels(updateArtikal, updateArtikalDto);
            _dbContext.SaveChanges();

            return _mapper.Map<ArtikalDto>(updateArtikal);
            
        }
    }
}
