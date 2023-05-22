using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projekat_WEB2_backend.Dto;
using Projekat_WEB2_backend.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private readonly IKorisnikService _korisnikService;

        public KorisnikController(IKorisnikService korisnikService)
        {
            _korisnikService = korisnikService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            return Ok(_korisnikService.GetAllKorisnik());
        }

        [HttpGet("{id}")]
        public IActionResult GetKorisnikById(long id)
        {
            return Ok(_korisnikService.GetKorisnikById(id));
        }

        [HttpPost]
        public IActionResult CreateKorisnik([FromBody] KorisnikDto korisnik)
        {
            return Ok(_korisnikService.AddKorisnik(korisnik));
        }

        [HttpPut("{id}")]
        public IActionResult ChangeKorisnik(long id, [FromBody] KorisnikDto korisnik)
        {
            KorisnikDto updatedKorisnik = _korisnikService.UpdateKorisnik(id, korisnik);
            if (updatedKorisnik == null)
            {
                return Unauthorized("Ne postoji korinsik");
            }
            
            updatedKorisnik.Lozinka = korisnik.Lozinka;
            return Ok(updatedKorisnik);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteKorisnik(long id)
        {
            _korisnikService.DeleteKorisnik(id);
            return Ok($"Korisnik sa id = {id} je uspesno obrisan.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginKorisnikDto loginKorisnikDto)
        {
            ResponseDto responseDto = _korisnikService.Login(loginKorisnikDto);
            if (responseDto == null)
                return Unauthorized("Ili nisu uneti dobri podaci ili korisnik ne postoji u sistemu");

            responseDto.KorisnikDto.Lozinka = loginKorisnikDto.Lozinka;
            return Ok(responseDto);
        }

        [HttpPost("registration")]
        public IActionResult Registration([FromBody] KorisnikDto registerKorisnikDto)
        {
            ResponseDto responseDto = _korisnikService.Registration(registerKorisnikDto);
            if (responseDto == null)
                return Unauthorized();

            responseDto.KorisnikDto.Lozinka = registerKorisnikDto.Lozinka;
            return Ok(responseDto);

        }

    }
}
