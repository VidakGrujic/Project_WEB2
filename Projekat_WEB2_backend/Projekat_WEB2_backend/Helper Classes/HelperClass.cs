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
    public class HelperClass
    {
        public static string HashPassword(string password)
        {
            using(var sha256 = SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
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

        public static void UpdatePorudzbinaFields(Porudzbina porudzbina, PorudzbinaDto porudzbinaDto)
        {
            porudzbina.Komentar = porudzbinaDto.Komentar;
            porudzbina.Adresa = porudzbinaDto.Adresa;
            porudzbina.StanjePorudzbine = porudzbinaDto.StanjePorudzbine;
        }

        public static void UpdateArtikalFiels(Artikal artikal, ArtikalDto artikalDto)
        {
            artikal.Naziv = artikalDto.Naziv;
            artikal.Cena = artikalDto.Cena;
            artikal.Kolicina = artikalDto.Kolicina;
            artikal.Opis = artikalDto.Opis;
            artikal.Fotografija = artikalDto.Fotografija;
        }
    }
}
