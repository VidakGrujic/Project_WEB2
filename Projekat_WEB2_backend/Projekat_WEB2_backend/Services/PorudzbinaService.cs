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
            Porudzbina newPorudzbina = _mapper.Map<Porudzbina>(newPorudzbinaDto);
            _dbContext.Porudzbine.Add(newPorudzbina);
            _dbContext.SaveChanges();

            return _mapper.Map<PorudzbinaDto>(newPorudzbina);
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
            HelperClass.UpdatePorudzbinaFields(updatePorudzbina, updatePorudzbinaDto);

            _dbContext.SaveChanges();

            return _mapper.Map<PorudzbinaDto>(updatePorudzbina);
        }
    }
}
