using Projekat_WEB2_backend.Dto;
using Projekat_WEB2_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Helper_Classes
{
    public class KorisnikHelperClass
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static void UpdateKorisnikFields(Korisnik korisnik, KorisnikDto korisnikDto)
        {
            korisnik.KorisnickoIme = korisnikDto.KorisnickoIme;
            korisnik.Email = korisnikDto.Email;
            korisnik.Lozinka = HashPassword(korisnikDto.Lozinka);
            korisnik.Ime = korisnikDto.Ime;
            korisnik.Prezime = korisnikDto.Prezime;
            korisnik.DatumRodjenja = korisnikDto.DatumRodjenja;
            korisnik.Adresa = korisnikDto.Adresa;
            korisnik.Slika = korisnikDto.Slika;
            korisnik.StatusVerifikacije = korisnikDto.StatusVerifikacije;
        }

        public static bool IsKorisnikFieldsValid(KorisnikDto korisnikDto)
        {
            if (string.IsNullOrEmpty(korisnikDto.KorisnickoIme))
                return false;
            if (string.IsNullOrEmpty(korisnikDto.Email))
                return false;
            if (string.IsNullOrEmpty(korisnikDto.Lozinka))
                return false;
            if (string.IsNullOrEmpty(korisnikDto.Ime))
                return false;
            if (string.IsNullOrEmpty(korisnikDto.Prezime))
                return false;
            if (korisnikDto.DatumRodjenja > DateTime.Now) //ne moze da se rodi u buducnost \
                return false;
            if (string.IsNullOrEmpty(korisnikDto.Adresa))
                return false;

            return true;
        }

    }
}
