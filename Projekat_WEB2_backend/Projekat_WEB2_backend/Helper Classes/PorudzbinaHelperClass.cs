using Projekat_WEB2_backend.Dto;
using Projekat_WEB2_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Helper_Classes
{
    public class PorudzbinaHelperClass
    {
        public static void UpdatePorudzbinaFields(Porudzbina porudzbina, PorudzbinaDto porudzbinaDto)
        {
            porudzbina.Komentar = porudzbinaDto.Komentar;
            porudzbina.Adresa = porudzbinaDto.Adresa;
            porudzbina.StanjePorudzbine = porudzbinaDto.StanjePorudzbine;
        }
    }
}
