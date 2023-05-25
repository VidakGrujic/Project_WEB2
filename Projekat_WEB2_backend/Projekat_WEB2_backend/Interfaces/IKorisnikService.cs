using Projekat_WEB2_backend.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Interfaces
{
    public interface IKorisnikService
    {
        KorisnikDto AddKorisnik(KorisnikDto newKorisnikDto);
        List<KorisnikDto> GetAllKorisnik();
        KorisnikDto GetKorisnikById(long id);
        KorisnikDto UpdateKorisnik(long id, KorisnikDto updateKorisnikDto);
        void DeleteKorisnik(long id);

        ResponseDto Login(LoginKorisnikDto loginKorisnikDto);
        ResponseDto Registration(KorisnikDto registerKorisnik);


        List<KorisnikDto> GetProdavce();
        List<KorisnikDto> VerifyProdavce(long id, string statusVerifikacije);


    }
}
