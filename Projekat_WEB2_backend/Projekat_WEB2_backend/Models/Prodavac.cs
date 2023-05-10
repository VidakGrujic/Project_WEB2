using Projekat_WEB2_backend.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Models
{
    public class Prodavac : Korisnik
    {
        public Prodavac()
        {
        }
        public StatusVerifikacije StatusVerifikacije { get; set; }
        
    }
}
