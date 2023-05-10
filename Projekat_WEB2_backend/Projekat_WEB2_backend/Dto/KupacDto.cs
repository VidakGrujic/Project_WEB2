using Projekat_WEB2_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Dto
{
    public class KupacDto : KorisnikDto
    {
        public List<Porudzbina> Porudzbine { get; set; }
    }
}
