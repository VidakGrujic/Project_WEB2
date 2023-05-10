using Projekat_WEB2_backend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Interfaces
{
    interface IArtikalService
    {
        ArtikalDto AddArtikal(ArtikalDto newArtikal);
        List<ArtikalDto> GetAllArtikals();
        ArtikalDto GetArtikalById();
        ArtikalDto UpdateArtikal(long id, ArtikalDto updateArtikal);
        void DeleteStudent(long id);


    }
}
