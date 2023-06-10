using Projekat_WEB2_backend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Interfaces
{
    public interface IPorudzbinaService
    {
        Task<PorudzbinaDto> AddPorudzbina(PorudzbinaDto newPorudzbinaDto);
        Task<List<PorudzbinaDto>> GetAllPorudzbina();
        Task<PorudzbinaPrikazDto> GetPorudzbinaById(long id);
        Task<PorudzbinaDto> UpdatePorudzbina(long id, PorudzbinaDto updatePorudzbinaDto);
        Task DeletePorudzbina(long id);
        Task<List<PorudzbinaDto>> GetKupcevePorudzbine(long id);
        Task<ResponsePorudzbinaDto> OtkaziPorudzbinu(long id, string statusVerifikacije);
        Task<List<PorudzbinaDto>> GetProdavceveNovePorudzbine(long id);

        Task<List<PorudzbinaDto>> GetProdavcevePrethodnePorudzbine(long id);

        
    }
}
