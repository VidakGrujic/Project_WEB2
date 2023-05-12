using Projekat_WEB2_backend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Interfaces
{
    public interface IKorisnikService
    {
        KorisnikDto AddKorisnik(KorisnikDto newKorisnik);
        List<PorudzbinaDto> GetAllKorisnik();
        PorudzbinaDto GetPorudzbinaById(long id);
        PorudzbinaDto UpdatePorudzbina(long id, PorudzbinaDto updatePorudzbinaDto);
        void DeletePorudzbina(long id);
    }
}
