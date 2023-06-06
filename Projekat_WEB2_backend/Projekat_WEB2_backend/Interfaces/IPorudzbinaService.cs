using Projekat_WEB2_backend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Interfaces
{
    public interface IPorudzbinaService
    {
        PorudzbinaDto AddPorudzbina(PorudzbinaDto newPorudzbinaDto);
        List<PorudzbinaDto> GetAllPorudzbina();
        PorudzbinaPrikazDto GetPorudzbinaById(long id);
        PorudzbinaDto UpdatePorudzbina(long id, PorudzbinaDto updatePorudzbinaDto);
        void DeletePorudzbina(long id);
        List<PorudzbinaDto> GetKupcevePorudzbine(long id);
        ResponsePorudzbinaDto OtkaziPorudzbinu(long id, string statusVerifikacije);
        List<PorudzbinaDto> GetProdavceveNovePorudzbine(long id);

        List<PorudzbinaDto> GetProdavcevePrethodnePorudzbine(long id);

        
    }
}
