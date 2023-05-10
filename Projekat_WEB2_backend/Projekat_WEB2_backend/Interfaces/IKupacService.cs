using Projekat_WEB2_backend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Interfaces
{
    public interface IKupacService
    {
        KupacDto AddKupac(KupacDto newKupac);
        List<KupacDto> GetAllKupac();
        KupacDto GetKupacById(long id);
        KupacDto UpdateKupac(long id, KupacDto updateKupac);
        void DeleteKupac(long id);
    }
}
