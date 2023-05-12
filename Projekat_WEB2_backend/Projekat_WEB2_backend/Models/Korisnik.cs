using Projekat_WEB2_backend.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Models
{
    public class Korisnik
    {
        public Korisnik()
        {
        }

        public long Id { get; set; }
        public string KorisnickoIme { get; set; }
        public string Email { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Adresa { get; set; }
        public TipKorisnika TipKorisnika { get; set; }
        public string Slika { get; set; }
        public StatusVerifikacije StatusVerifikacije { get; set; }
        public List<Porudzbina> Porudzbine { get; set; }
    }
}
