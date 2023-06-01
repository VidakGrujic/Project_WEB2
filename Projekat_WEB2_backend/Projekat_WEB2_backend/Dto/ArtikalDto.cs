using Projekat_WEB2_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Dto
{
    public class ArtikalDto
    {
        public long Id { get; set; }
        public string Naziv { get; set; }
        public double Cena { get; set; }
        public int Kolicina { get; set; }
        public string Opis { get; set; }
        public string Fotografija { get; set; }
        public long ProdavacId { get; set; }
        
    }
}
