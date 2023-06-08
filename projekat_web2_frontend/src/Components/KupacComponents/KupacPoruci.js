import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AddPorudzbina } from "../../Services/PorudzbinaService";

export default function KupacPoruci() {
  const [adresa, setAdresa] = useState("");
  const [komentar, setKomentar] = useState("");
  const [error, setError] = useState(false);
  const [izabraniArtikli, setIzabraniArtikli] = useState(
    JSON.parse(sessionStorage.getItem("porudzbina"))
  );
  const [cenaPorudzbine, setCenaPorudzbine] = useState("");

  const navigate = useNavigate();

  useEffect(() => {
    const ukupnaCenaPorudzbine = () => {
      var ceneDostaveArtikala = []; //u njega stavljam sve razlicite cene dostave jer ima razlicitih prodavaca
      var ukupnaCenaDostave = 0;
      var ukupnaCenaPorudzbine = 0;
      for (let i = 0; i < izabraniArtikli.length; i++) {
        if (!ceneDostaveArtikala.includes(izabraniArtikli[i].cenaDostave)) {
          //odredimo da li je zabelezena cena dostave
          ceneDostaveArtikala.push(izabraniArtikli[i].cenaDostave); //ako nije ubaci je
          ukupnaCenaDostave += izabraniArtikli[i].cenaDostave; //i dodaj na ukupnu cenu dostave
        }
        ukupnaCenaPorudzbine +=
          izabraniArtikli[i].kolicina * izabraniArtikli[i].cena; //saberi klasika, cena artikla puta kolicina artikla
      }
      ukupnaCenaPorudzbine += ukupnaCenaDostave; //na to dodaj cenu dostave
      setCenaPorudzbine(ukupnaCenaPorudzbine);
    };
    ukupnaCenaPorudzbine();
  }, [cenaPorudzbine]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const korisnik = JSON.parse(sessionStorage.getItem("korisnik"));
    const korisnikId = korisnik.id;

    const token = sessionStorage.getItem("token");

    if (adresa.length === 0) {
      setError(true);
      return;
    }

    const artikliPorudzbine = [];
    for (let i = 0; i < izabraniArtikli.length; i++) {
      var porudzbinaArtikal = {
        artikalId: izabraniArtikli[i].artikalId,
        kolicina: izabraniArtikli[i].kolicina,
      };
      artikliPorudzbine.push(porudzbinaArtikal);
    }

    const porudzbinaDto = JSON.stringify({
      komentar,
      adresa,
      korisnikId,
      artikliPorudzbine,
      cenaPorudzbine
    });

    
    const data = await AddPorudzbina(porudzbinaDto, token);
    if(data !== null){
        console.log(data);
        alert("Uspesno prihvacena porudzbina");
        navigate('/kupacDashboard');
    
    } 
  };

  return (
    <div className="card">
      <form className="ui form" onSubmit={handleSubmit}>
        <h2 className="ui center aligned header">Potvrdite porudzbinu</h2>
        <div className="field">
          <div className="ui green message">
            <div className="header">Odabrani artikli</div>
            <ul className="list">
              {izabraniArtikli.map((izabraniArtikal) => (
                <li>{izabraniArtikal.naziv}</li>
              ))}
            </ul>
            <br />
            <h3 className="ui left aligned header">
              Cena porudzbine: {cenaPorudzbine}
            </h3>
          </div>
        </div>
        <div className="field">
          <label>Adresa dostave</label>
          <input
            type="text"
            name="adresaDostave"
            placeholder="Adresa dostave"
            value={adresa}
            onChange={(e) => setAdresa(e.target.value)}
          />
          {error && adresa.length === 0 ? (
            <div className="ui pointing red basic label">
              Morate uneti adresu dostave
            </div>
          ) : null}
        </div>
        <div className="field">
          <label>Komentar</label>
          <textarea
            className="textarea-resize"
            rows="6"
            placeholder="Unesite dodatne komentare, odnosno napomene"
            value={komentar}
            onChange={(e) => setKomentar(e.target.value)}
          />
        </div>
        <button className="ui blue button" type="submit">
          Porucite
        </button>
      </form>
    </div>
  );
}
