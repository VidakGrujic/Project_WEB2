using Projekat_WEB2_backend.Dto;
using Projekat_WEB2_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Helper_Classes
{
    public class ArtikalHelperClass
    {
        public static void UpdateArtikalFiels(Artikal artikal, ArtikalDto artikalDto)
        {
            artikal.Naziv = artikalDto.Naziv;
            artikal.Cena = artikalDto.Cena;
            artikal.Kolicina = artikalDto.Kolicina;
            artikal.Opis = artikalDto.Opis;
            artikal.Fotografija = artikalDto.Fotografija;
        }
    
        public static bool IsArtikalFieldsValid(ArtikalDto artikalDto)
        {
            if (string.IsNullOrEmpty(artikalDto.Naziv))
                return false;
            if (string.IsNullOrEmpty(artikalDto.Opis))
                return false;
            if (artikalDto.Cena == 0)
                return false;
            if (artikalDto.Kolicina == 0)
                return false;
            if (artikalDto.ProdavacId == 0)
                return false;
            if (string.IsNullOrEmpty(artikalDto.Fotografija))
                return false;
            

            return true;

        }
    }
}
