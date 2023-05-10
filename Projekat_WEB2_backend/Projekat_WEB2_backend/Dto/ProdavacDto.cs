using Projekat_WEB2_backend.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Dto
{
    public class ProdavacDto : KorisnikDto
    {
        public StatusVerifikacije StatusVerifikacije { get; set; }
    }
}
