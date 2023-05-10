using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Models
{
    public class Kupac : Korisnik
    {
        public List<Porudzbina> Porudzbine { get; set; }

    }
}
