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
        public StanjePorudzbine StanjePorudzbine { get; set; }

        //vezano za bazu i relacije
        public List<Artikal> Artikli { get; set; }
        
        public Kupac Kupac { get; set; }
    }
}
