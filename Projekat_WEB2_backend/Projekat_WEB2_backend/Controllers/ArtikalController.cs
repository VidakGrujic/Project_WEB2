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

        [HttpGet("getAll")]
        public IActionResult GetAllArtikals()
        {
            return Ok(_artikalService.GetAllArtikals());
        }

        [HttpGet("{id}")]
        public IActionResult GetArtikalById(long id)
        {
            return Ok(_artikalService.GetArtikalById(id));
        }

        [HttpPost]
        public IActionResult CreateArtikal([FromBody] ArtikalDto artikal)
        {
            return Ok(_artikalService.AddArtikal(artikal));
        }

        [HttpPut("{id}")]
        public IActionResult ChangeArtikal(long id, [FromBody] ArtikalDto artikal)
        {
            return Ok(_artikalService.UpdateArtikal(id, artikal)); 
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteArtikal(long id)
        {
            _artikalService.DeleteArtikal(id);
            return Ok($"Artikal id {id} je uspesno obrisan");
        }       
    }
}
