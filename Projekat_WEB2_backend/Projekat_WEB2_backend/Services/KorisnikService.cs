using AutoMapper;
using Microsoft.Extensions.Configuration;
using Projekat_WEB2_backend.Dto;
using Projekat_WEB2_backend.Helper_Classes;
using Projekat_WEB2_backend.Infrastructure;
using Projekat_WEB2_backend.Interfaces;
using Projekat_WEB2_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using System.Security.Claims;
using Projekat_WEB2_backend.Enumerations;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Projekat_WEB2_backend.Services
{
    public class KorisnikService : IKorisnikService
    {
        private readonly IMapper _mapper;
        private readonly ProdavnicaDbContext _dbContext;
        private readonly IConfigurationSection _secretKey;

        public KorisnikService(IMapper mapper, ProdavnicaDbContext dbContext, IConfiguration configuration)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _secretKey = configuration.GetSection("SecretKey");
        }

        public KorisnikDto AddKorisnik(KorisnikDto newKorisnikDto)
        {
            Korisnik newKorisnik = _mapper.Map<Korisnik>(newKorisnikDto);
            newKorisnik.Lozinka = KorisnikHelperClass.HashPassword(newKorisnik.Lozinka);
            _dbContext.Korisnici.Add(newKorisnik);
            _dbContext.SaveChanges();

            return _mapper.Map<KorisnikDto>(newKorisnik);
        }

        public void DeleteKorisnik(long id)
        {
            Korisnik deleteKorisnik = _dbContext.Korisnici.Find(id);
            _dbContext.Korisnici.Remove(deleteKorisnik);
            _dbContext.SaveChanges();
        }

        public List<KorisnikDto> GetAllKorisnik()
        {
            List<KorisnikDto> korisnici = _mapper.Map<List<KorisnikDto>>(_dbContext.Korisnici.ToList());
            return korisnici;
        }

        public KorisnikDto GetKorisnikById(long id)
        {
            Korisnik findKorisnik = _dbContext.Korisnici.Find(id);
            return _mapper.Map<KorisnikDto>(findKorisnik);
        }

        public KorisnikDto UpdateKorisnik(long id, KorisnikDto updateKorisnikDto)
        {
            Korisnik updateKorisnik = _dbContext.Korisnici.Find(id);
            KorisnikHelperClass.UpdateKorisnikFields(updateKorisnik, updateKorisnikDto);
            _dbContext.SaveChanges();

            return _mapper.Map<KorisnikDto>(updateKorisnik);
        }

        public string Login(LoginKorisnikDto loginKorisnikDto)
        {
            if (string.IsNullOrEmpty(loginKorisnikDto.Email) && string.IsNullOrEmpty(loginKorisnikDto.Lozinka))
                return null;

            Korisnik loginKorisnik = _dbContext.Korisnici.First(x => x.Email == loginKorisnikDto.Email);

            if (loginKorisnik == null)
            {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(loginKorisnikDto.Lozinka, loginKorisnik.Lozinka))
            {
                List<Claim> claims = new List<Claim>();
                if (loginKorisnik.TipKorisnika == TipKorisnika.Administrator)
                    claims.Add(new Claim(ClaimTypes.Role, "administrator"));
                if (loginKorisnik.TipKorisnika == TipKorisnika.Kupac)
                    claims.Add(new Claim(ClaimTypes.Role, "kupac"));
                if (loginKorisnik.TipKorisnika == TipKorisnika.Prodavac)
                    claims.Add(new Claim(ClaimTypes.Role, "prodavac"));


                SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
                SigningCredentials signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:44385",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(40),
                    signingCredentials: signInCredentials
                    );
                string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return token;
            }
            else
            {
                return null;
            }


        }


        //kod registracije, mora se proveriti i uloga korisnika
        //ako je kupac ili administrator, status verifikacije je odmah prihvacen
        //ako je prodavac, onda se stavi na status obrade

        public string Registration(KorisnikDto registerKorisnik)
        {
            if (string.IsNullOrEmpty(registerKorisnik.Email)) //ako nije unet email, baci gresku
                return null;

            foreach(Korisnik k in _dbContext.Korisnici)
            {
                if (k.Email == registerKorisnik.Email)
                    return null;
            }

            if(registerKorisnik.TipKorisnika == TipKorisnika.Prodavac)
            {
                registerKorisnik.StatusVerifikacije = StatusVerifikacije.Procesuira_se;
            }
          

            if (!KorisnikHelperClass.IsKorisnikFieldsValid(registerKorisnik)) //ako nisu validna polja onda nista
                return null;

            KorisnikDto registeredKorisnik = AddKorisnik(registerKorisnik);

            if (registeredKorisnik == null)
                return null;

            
            

            //nema provere za password, pa odmah vracamo token
            List<Claim> claims = new List<Claim>();
            if (registerKorisnik.TipKorisnika == TipKorisnika.Administrator)
                claims.Add(new Claim(ClaimTypes.Role, "administrator"));
            if (registerKorisnik.TipKorisnika == TipKorisnika.Kupac)
                claims.Add(new Claim(ClaimTypes.Role, "kupac"));
            if (registerKorisnik.TipKorisnika == TipKorisnika.Prodavac)
                claims.Add(new Claim(ClaimTypes.Role, "prodavac"));

            SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey.Value));
            SigningCredentials signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:44385",
                claims: claims,
                expires: DateTime.Now.AddMinutes(40),
                signingCredentials: signInCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;



        }
        
    }
}
