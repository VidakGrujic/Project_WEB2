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
    public class KorisnikService : IKorisnikService
    {
        private readonly IMapper _mapper;
        private readonly ProdavnicaDbContext _dbContext;

        public KorisnikService(IMapper mapper, ProdavnicaDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public KorisnikDto AddKorisnik(KorisnikDto newKorisnikDto)
        {
            Korisnik newKorisnik = _mapper.Map<Korisnik>(newKorisnikDto);
            _dbContext.Korisnici.Add(newKorisnik);
            _dbContext.SaveChanges();

            return _mapper.Map<KorisnikDto>(newKorisnik);
        }

        public void DeleteKorisnik(long id)
        {
            Korisnik deleteKorisnik = _dbContext.Korisnici.Find(id);
            _dbContext.Korisnici.Remove(deleteKorisnik);
            _dbContext.SaveChanges();
        }

        public List<KorisnikDto> GetAllKorisnik()
        {
            List<KorisnikDto> korisnici = _mapper.Map<List<KorisnikDto>>(_dbContext.Korisnici.ToList());
            return korisnici;
        }

        public KorisnikDto GetKorisnikById(long id)
        {
            Korisnik findKorisnik = _dbContext.Korisnici.Find(id);
            return _mapper.Map<KorisnikDto>(findKorisnik);
        }

        public KorisnikDto UpdateKorisnik(long id, KorisnikDto updateKorisnikDto)
        {
            Korisnik updateKorisnik = _dbContext.Korisnici.Find(id);
            HelperClass.UpdateKorisnikFields(updateKorisnik, updateKorisnikDto);
            _dbContext.SaveChanges();

            return _mapper.Map<KorisnikDto>(updateKorisnik);
        }
    }
}
