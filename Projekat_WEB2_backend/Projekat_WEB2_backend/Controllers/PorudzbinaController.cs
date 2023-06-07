using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize(Roles ="administrator")]
        public IActionResult GetAllPorudzbina()
        {
            return Ok(_porudzbinaService.GetAllPorudzbina());
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="kupac,prodavac,administrator")]
        public IActionResult GetById(long id) //id porudzbine
        { 
            PorudzbinaPrikazDto porudzbinaZaPrikazDto = _porudzbinaService.GetPorudzbinaById(id);
            if(porudzbinaZaPrikazDto == null)
            {
                return BadRequest("Porudzbina ne postoji");
            }

            return Ok(porudzbinaZaPrikazDto);
        }
        
        //izbaciti ovde add, jer je ovo crud operacija
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
        [Authorize(Roles ="kupac")]
        public IActionResult GetKupcevePorudzbine(long id) // id kupca
        {
            List<PorudzbinaDto> korisnikovePorudzbineDto = _porudzbinaService.GetKupcevePorudzbine(id);
            if(korisnikovePorudzbineDto == null)
            {
                return BadRequest("Ne postoji korisnik sa datim id");
            }
            return Ok(korisnikovePorudzbineDto);
        }


        [HttpPut("otkaziPorudzbinu/{id}")]
        [Authorize(Roles = "kupac")]
        public IActionResult OtkaziPorudzbine(long id, [FromBody] string statusVerifikacije) //id kupca
        {
            ResponsePorudzbinaDto otkazanaPorudzbinaDto = _porudzbinaService.OtkaziPorudzbinu(id, statusVerifikacije);
            if(otkazanaPorudzbinaDto.PorudzbinaDto == null)
            {
                return BadRequest(otkazanaPorudzbinaDto);
            }
            return Ok(otkazanaPorudzbinaDto);
        }

        
        [HttpGet("getProdavceveNovePorudzbine/{id}")]
        [Authorize(Roles = "prodavac")]
        public IActionResult GetProdavceveNovePorudzbine(long id) //prodavcev id
        {
            List<PorudzbinaDto> prodavcevePorudzbine = _porudzbinaService.GetProdavceveNovePorudzbine(id);
            if(prodavcevePorudzbine == null)
            {
                return BadRequest("Nesto nije kako treba");
            }

            return Ok(prodavcevePorudzbine);
        }

        [HttpGet("getProdavcevePrethodnePorudzbine/{id}")]
        [Authorize(Roles ="prodavac")]
        public IActionResult GetProdavcevePrethodnePorudzbine(long id) //prodavcev id
        {
            List<PorudzbinaDto> prodavcevePrethodnePorudzbineDto = _porudzbinaService.GetProdavcevePrethodnePorudzbine(id);
            if(prodavcevePrethodnePorudzbineDto == null)
            {
                return BadRequest("Nisu lepo ucitane prodavceve produzbine");
            }
            return Ok(prodavcevePrethodnePorudzbineDto);

        }
    }
}
