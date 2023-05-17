using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Dto
{
    public class ResponseDto
    {
        public string Token { get; set; }
        public KorisnikDto KorisnikDto { get; set; }

        public ResponseDto(string token, KorisnikDto korisnikDto)
        {
            Token = token;
            KorisnikDto = korisnikDto;
        }
    }
}
