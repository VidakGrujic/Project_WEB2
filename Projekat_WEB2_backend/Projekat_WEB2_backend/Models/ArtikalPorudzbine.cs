using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Models
{
    public class ArtikalPorudzbine
    {
        public long Id { get; set; }
        public long ArtikalId { get; set; }
        public int Kolicina { get; set; }
        public long PorudzbinaId { get; set; }
        public Porudzbina Porudzbina { get; set; }
    }
}
