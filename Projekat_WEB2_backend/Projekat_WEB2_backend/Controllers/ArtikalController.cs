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
    [Route("api/products")]
    [ApiController]
    public class ArtikalController : ControllerBase
    {
        private readonly IArtikalService _artikalService;

        public ArtikalController(IArtikalService artikalService)
        {
            _artikalService = artikalService;
        }

        [HttpGet]
        public IActionResult GetAllArtikals()
        {
            return Ok(_artikalService.GetAllArtikals());
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="prodavac")]
        public IActionResult GetArtikalById(long id) //id artikla
        {
            ArtikalDto artikal = _artikalService.GetArtikalById(id);
            if(artikal == null)
            {
                return BadRequest("Artikal ne postoji");
            }
            return Ok(artikal);
        }

        [HttpPost("addArtikal")]
        [Authorize(Roles = "prodavac")]
        public IActionResult CreateArtikal([FromBody] ArtikalDto artikal)
        {
            ArtikalDto newArtikalDto = _artikalService.AddArtikal(artikal);
            if(newArtikalDto == null)
            {
                return BadRequest("Polja nisu dobro popunjena ili prodavac ne postoji");
            }
            return Ok(newArtikalDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "prodavac")]
        public IActionResult ChangeArtikal(long id, [FromBody] ArtikalDto artikal)
        {
            ArtikalDto updateArtikal = _artikalService.UpdateArtikal(id, artikal);
            if(updateArtikal == null)
            {
                return BadRequest("Artikal ne postoji");
            }
            return Ok(updateArtikal); 
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "prodavac")]
        public IActionResult DeleteArtikal(long id)
        {
            if (!_artikalService.DeleteArtikal(id))
            {
                return BadRequest("Artikal ne postoji ili nije uspesno obrisan");
            }
            return Ok($"Artikal id {id} je uspesno obrisan");
        }       

        [HttpGet("getProdavceveArtikle/{id}")]
        [Authorize(Roles = "prodavac")]
        public IActionResult GetProdavceveArtikle(long id) //prodavcev id
        {
            List<ArtikalDto> prodavceviArtikli = _artikalService.GetProdavceveArtikle(id);
            if(prodavceviArtikli == null)
            {
                return BadRequest("Korisnik ne postoji");
            }
            return Ok(prodavceviArtikli);
        }        


    }
}
