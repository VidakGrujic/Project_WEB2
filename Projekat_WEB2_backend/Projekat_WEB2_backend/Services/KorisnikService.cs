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

            Korisnik updateKorisnik = new Korisnik();

            try
            {
                updateKorisnik = _dbContext.Korisnici.Find(id);
            }
            catch(Exception e)
            {
                return null;
            }
            if (!KorisnikHelperClass.IsKorisnikFieldsValid(updateKorisnikDto))
                return null;

            updateKorisnik.Lozinka = KorisnikHelperClass.HashPassword(updateKorisnikDto.Lozinka);
            KorisnikHelperClass.UpdateKorisnikFields(updateKorisnik, updateKorisnikDto);
            _dbContext.SaveChanges();

            return _mapper.Map<KorisnikDto>(updateKorisnik);
        }

        public ResponseDto Login(LoginKorisnikDto loginKorisnikDto)
        {

            Korisnik loginKorisnik = new Korisnik();
            if (string.IsNullOrEmpty(loginKorisnikDto.Email) && string.IsNullOrEmpty(loginKorisnikDto.Lozinka))
            {
                return new ResponseDto("Niste uneli email ili lozinku.");
            }
            
            try
            {
                loginKorisnik = _dbContext.Korisnici.First(x => x.Email == loginKorisnikDto.Email);
            }
            catch (Exception e)
            {
                return new ResponseDto($"Korisnik sa emailom {loginKorisnikDto.Email} ne postoji");
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
                KorisnikDto korisnikDto = _mapper.Map<KorisnikDto>(loginKorisnik);

                ResponseDto responseDto = new ResponseDto(token, korisnikDto, "Uspesno ste se logovali na sistem");
                return responseDto;
            }
            else
            {
                return new ResponseDto("Lozinka nije ispravno uneta");
            }
        }

        public ResponseDto Registration(KorisnikDto registerKorisnik)
        {
            if (string.IsNullOrEmpty(registerKorisnik.Email)) //ako nije unet email, baci gresku
                return new ResponseDto("Niste uneli email");

            foreach(Korisnik k in _dbContext.Korisnici)
            {
                if (k.Email == registerKorisnik.Email)
                    return new ResponseDto("Email vec postoji");
            }

            if(registerKorisnik.TipKorisnika == TipKorisnika.Prodavac)
            {
                registerKorisnik.StatusVerifikacije = StatusVerifikacije.Procesuira_se;
            }

            if(registerKorisnik.TipKorisnika != TipKorisnika.Prodavac)
            {
                registerKorisnik.CenaDostave = 0;
            }
            

            if (!KorisnikHelperClass.IsKorisnikFieldsValid(registerKorisnik)) //ako nisu validna polja onda nista
                return new ResponseDto("Ostala polja moraju biti validna");

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

            ResponseDto responseDto = new ResponseDto(token, registeredKorisnik, "Uspesno ste se registrovali");
            return responseDto;
        }

        public List<KorisnikDto> GetProdavce()
        {
            List<Korisnik> prodavci = new List<Korisnik>();
            foreach(Korisnik korisnik in _dbContext.Korisnici.ToList())
            {
                if(korisnik.TipKorisnika == TipKorisnika.Prodavac && korisnik.StatusVerifikacije != StatusVerifikacije.Odbijen)
                {
                    prodavci.Add(korisnik);
                }
            }

            return _mapper.Map<List<KorisnikDto>>(prodavci);
        }

        public List<KorisnikDto> VerifyProdavce(long id, string statusVerifikacije)
        {
            Korisnik prodavac = _dbContext.Korisnici.Find(id);
            if(prodavac == null)
            {
                return null;
            }

            if (statusVerifikacije.Equals(StatusVerifikacije.Prihvacen.ToString()))
            {
                prodavac.StatusVerifikacije = StatusVerifikacije.Prihvacen;
            }
            else if (statusVerifikacije.Equals(StatusVerifikacije.Odbijen.ToString()))
            {
                prodavac.StatusVerifikacije = StatusVerifikacije.Odbijen;
            }
            _dbContext.SaveChanges();

            return GetProdavce();
        }
    }
}
