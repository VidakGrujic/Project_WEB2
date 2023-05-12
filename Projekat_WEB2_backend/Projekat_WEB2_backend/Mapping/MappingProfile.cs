using AutoMapper;
using Projekat_WEB2_backend.Dto;
using Projekat_WEB2_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Korisnik, KorisnikDto>().ReverseMap();
            CreateMap<Artikal, ArtikalDto>().ReverseMap();
            CreateMap<Porudzbina, PorudzbinaDto>().ReverseMap();
        }
    }
}
