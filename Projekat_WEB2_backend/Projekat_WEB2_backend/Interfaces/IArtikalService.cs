using Projekat_WEB2_backend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Interfaces
{
    public interface IArtikalService
    {
        ArtikalDto AddArtikal(ArtikalDto newArtikalDto);
        List<ArtikalDto> GetAllArtikals();
        ArtikalDto GetArtikalById(long id);
        ArtikalDto UpdateArtikal(long id, ArtikalDto updateArtikalDto);
        bool DeleteArtikal(long id);
        List<ArtikalDto> GetProdavceveArtikle(long id);

    }
}
