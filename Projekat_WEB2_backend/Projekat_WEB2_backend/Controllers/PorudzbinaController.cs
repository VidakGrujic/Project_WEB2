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
    [Route("api/orders")]
    [ApiController]
    public class PorudzbinaController : ControllerBase
    {
        private readonly IPorudzbinaService _porudzbinaService;

        public PorudzbinaController(IPorudzbinaService porudzbinaService)
        {
            _porudzbinaService = porudzbinaService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAllPorudzbina()
        {
            return Ok(_porudzbinaService.GetAllPorudzbina());
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="kupac")]
        public IActionResult GetById(long id)
        {
            PorudzbinaPrikazDto porudzbinaZaPrikazDto = _porudzbinaService.GetPorudzbinaById(id);
            if(porudzbinaZaPrikazDto == null)
            {
                return BadRequest("Porudzbina ne postoji");
            }

            return Ok(porudzbinaZaPrikazDto);
        }

        [HttpPost("addPorudzbina")]
        [Authorize(Roles = "kupac")]
        public IActionResult CreatePorudzbina([FromBody] PorudzbinaDto porudzbina)
        {
            PorudzbinaDto newPorudzbinaDto = _porudzbinaService.AddPorudzbina(porudzbina);
            if (newPorudzbinaDto == null)
            {
                return BadRequest("Postoji neki problem prilikom dodavanja porudzbine");
            }
            return Ok(newPorudzbinaDto);
        }

        [HttpPut("{id}")]
        public IActionResult ChangePorudzbina(long id, [FromBody] PorudzbinaDto porudzbina)
        {
            return Ok(_porudzbinaService.UpdatePorudzbina(id, porudzbina));
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePorudzbina(long id)
        {
            _porudzbinaService.DeletePorudzbina(id);
            return Ok($"Porudzbina sa id = {id} je uspesno obrisan.");
        }

        [HttpGet("getKupcevePorudzbine/{id}")]
        [Authorize(Roles = "kupac")]
        public IActionResult GetKupcevePorudzbine(long id)
        {
            List<PorudzbinaDto> korisnikovePorudzbineDto = _porudzbinaService.GetKupcevePorudzbine(id);
            if(korisnikovePorudzbineDto == null)
            {
                return BadRequest("Ne postoji korisnik sa datim id");
            }
            return Ok(korisnikovePorudzbineDto);
        }



    }
}
