using Projekat_WEB2_backend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Interfaces
{
    public interface IPorudzbinaService
    {
        PorudzbinaDto AddPorudzbina(PorudzbinaDto newPorudzbina);
        List<PorudzbinaDto> GetAllPorudzbina();
        PorudzbinaDto GetPorudzbinaById(long id);
        PorudzbinaDto UpdatePorudzbina(long id, PorudzbinaDto updatePorudzbina);
        void DeletePorudzbina(long id);
    }
}
