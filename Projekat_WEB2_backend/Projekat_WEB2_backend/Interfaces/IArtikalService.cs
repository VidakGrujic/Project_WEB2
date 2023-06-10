using Projekat_WEB2_backend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Interfaces
{
    public interface IArtikalService
    {
        Task<ArtikalDto> AddArtikal(ArtikalDto newArtikalDto);
        Task<List<ArtikalDto>> GetAllArtikals();
        Task<ArtikalDto> GetArtikalById(long id);
        Task<ArtikalDto> UpdateArtikal(long id, ArtikalDto updateArtikalDto);
        Task<bool> DeleteArtikal(long id);
        Task<List<ArtikalDto>> GetProdavceveArtikle(long id);

    }
}
