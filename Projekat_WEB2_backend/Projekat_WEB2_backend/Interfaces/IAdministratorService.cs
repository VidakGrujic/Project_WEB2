using Projekat_WEB2_backend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Interfaces
{
    public interface IAdministratorService
    {
        AdministratorDto AddAdministrator(AdministratorDto newAdministrator);
        List<AdministratorDto> GetAllAdministrator();
        AdministratorDto GetAdministratorById(long id);
        AdministratorDto UpdateAdministrator(long id, AdministratorDto updateAdministrator);
        void DeleteAdministrator(long id);
    }
}
