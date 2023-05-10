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
    public class AdministratorService : IAdministratorService
    {
        private readonly IMapper _mapper;
        private readonly ProdavnicaDbContext _dbContext;

        public AdministratorService(IMapper mapper, ProdavnicaDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public AdministratorDto AddAdministrator(AdministratorDto newAdministrator)
        {
            Administrator newAdmin = _mapper.Map<Administrator>(newAdministrator);
            newAdmin.Lozinka = HelperClass.HashPassword(newAdmin.Lozinka);
            _dbContext.Administratori.Add(newAdmin);
            _dbContext.SaveChanges();
            return _mapper.Map<AdministratorDto>(newAdmin);
        }

        public void DeleteAdministrator(long id)
        {
            Administrator deleteAdmin = _dbContext.Administratori.Find(id);
            if (deleteAdmin != null)
            {
                _dbContext.Administratori.Remove(deleteAdmin);
                _dbContext.SaveChanges();
            }
        }

        public AdministratorDto GetAdministratorById(long id)
        {
            return _mapper.Map<AdministratorDto>(_dbContext.Administratori.Find(id));
        }

        public List<AdministratorDto> GetAllAdministrator()
        {
            return _mapper.Map<List<AdministratorDto>>(_dbContext.Administratori.ToList());
        }

        public AdministratorDto UpdateAdministrator(long id, AdministratorDto updateAdministrator)
        {
            Administrator updateAdmin = _dbContext.Administratori.Find(id);
            HelperClass.UpdateKorisnikFields(updateAdmin, updateAdministrator);
            _dbContext.SaveChanges();
            return _mapper.Map<AdministratorDto>(updateAdmin);
        }
    }
}
