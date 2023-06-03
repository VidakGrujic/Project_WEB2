using Projekat_WEB2_backend.Enumerations;
using Projekat_WEB2_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Dto
{
    public class PorudzbinaDto
    {
        public long Id { get; set; }
        public string Komentar { get; set; }
        public string Adresa { get; set; }
        public DateTime DatumDostave { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public StanjePorudzbine StanjePorudzbine { get; set; }
        public long KorisnikId { get; set; }
        public List<ArtikalPorudzbineDto> ArtikliPorudzbine { get; set; }
        public double CenaPorudzbine { get; set; }
       

        
    }
}
