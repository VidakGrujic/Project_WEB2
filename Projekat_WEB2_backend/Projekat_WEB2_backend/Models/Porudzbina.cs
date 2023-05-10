using Projekat_WEB2_backend.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Models
{
    public class Porudzbina
    {
        public long Id { get; set; }
        public string Komentar { get; set;}
        public string Adresa { get; set; }
        public StanjePorudzbine StanjePorudzbine { get; set; }

        //vezano za bazu i relacije
        public List<Artikal> Artikli { get; set; }
        public long KupacId { get; set; }
        public Kupac Kupac { get; set; }
    }
}
