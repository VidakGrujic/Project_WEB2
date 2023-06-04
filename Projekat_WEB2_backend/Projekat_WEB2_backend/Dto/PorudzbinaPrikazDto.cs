using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Dto
{
    public class PorudzbinaPrikazDto
    {
        public long Id { get; set; }
        public string Adresa { get; set; }
        public string Komentar { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public DateTime DatumDostave { get; set; }
        public double CenaPorudzbine { get; set; }
        public List<string> ImenaArtikala { get; set; }
    }
}
