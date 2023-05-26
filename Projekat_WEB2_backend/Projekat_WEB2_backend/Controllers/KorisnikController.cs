﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly IEmailVerifyService _emailVerifyService;

        public KorisnikController(IKorisnikService korisnikService, IEmailVerifyService emailVerifyService)
        {
            _korisnikService = korisnikService;
            _emailVerifyService = emailVerifyService;
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
                return BadRequest("Postoje neka prazna polja(mozda korisnik ne postoji)");
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
            if(responseDto.KorisnikDto == null)
            {
                return BadRequest(responseDto.Result);
            }

            responseDto.KorisnikDto.Lozinka = loginKorisnikDto.Lozinka;
            return Ok(responseDto);
        }

        [HttpPost("registration")]
        public IActionResult Registration([FromBody] KorisnikDto registerKorisnikDto)
        {
            ResponseDto responseDto = _korisnikService.Registration(registerKorisnikDto);
            if (responseDto.KorisnikDto == null)
                return BadRequest(responseDto.Result);

            responseDto.KorisnikDto.Lozinka = registerKorisnikDto.Lozinka;
            return Ok(responseDto);

        }


        [HttpGet("getProdavce")]
        [Authorize(Roles = "administrator")]
        public IActionResult GetProdavce()
        {
            return Ok(_korisnikService.GetProdavce());
        }


        [HttpPut("verifyProdavca/{id}")]
        [Authorize(Roles = "administrator")]
        public IActionResult VerifyProdavca(long id, [FromBody] string statusVerifikacije)
        {
            List<KorisnikDto> verifiedProdavci = _korisnikService.VerifyProdavce(id, statusVerifikacije);
            if(verifiedProdavci == null)
            {
                return BadRequest("Ne postoji prodavac");
            }

            KorisnikDto prodavac = _korisnikService.GetKorisnikById(id);
            _emailVerifyService.SendVerificationMail(prodavac.Email, statusVerifikacije);

            return Ok(verifiedProdavci);
        }

    }
}
