import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { GetPorudzbinaById } from "../../Services/ComponentService";

export default function PrikazPorudzbina() {
  const [id, setId] = useState("");
  const [porudzbina, setPorudzbina] = useState({});
  const [loading, setLoading] = useState(true);

  const naviagte = useNavigate();

  useEffect(() => {
    const getPordzbinaId = async () => {
      const pathname = window.location.pathname;
      const stringId = pathname.split("/")[3]; //eksperimentalno se doslo do ovoga da je id na 3. mestu , vrv jer je prvi localhost:3000, odnosno 0

      const idInt = parseInt(stringId);
      setId(idInt);

      const response = await GetPorudzbinaById(idInt, sessionStorage.getItem('token'));
      response.datumDostave = new Date(response.datumDostave);
      response.datumKreiranja = new Date(response.datumKreiranja);
      console.log(response)
      setPorudzbina(response);
      setLoading(false);


    };
    getPordzbinaId();
  }, []);


  const handleClickPovratak = () => {
    naviagte('/kupacPorudzbine');
  }

  return (
    <div className="card">
      {loading && (
        <div className="loader-container">
          <div className="ui active inverted dimmer">
            <div className="ui large text loader">Ucitavanje porudzbine</div>
          </div>
        </div>
        )}
      {!loading && ( <><h1 className="ui block blue center aligned header">
        Pourdzbina br {porudzbina.id}
      </h1>
      <div className="field">
        <div className="ui blue message">
          <div className="ui green message">
            <h3 className="ui green center aligned header">Nazivi artikala</h3>
            <ul className="ui large list">
              {porudzbina.imenaArtikala.map((ime) => (
                <li className="item">{ime}</li>
              ))}
            </ul>
          </div>

          <br />
          <div className="field">
            <h3 className="ui blue center aligned header">
              Adresa dostave: {porudzbina.adresa}
            </h3>
          </div>
          <br />
          <div className="field">
            <h3 className="ui blue center aligned header">
              Datum porucivanja:{" "}
              {porudzbina.datumKreiranja.toLocaleDateString()} u{" "}
              {porudzbina.datumKreiranja.toLocaleTimeString()}
            </h3>
          </div>
          <br />
          <div className="field">
            <h3 className="ui blue center aligned header">
              Datum dostave: {porudzbina.datumDostave.toLocaleDateString()} u{" "}
              {porudzbina.datumDostave.toLocaleTimeString()}
            </h3>
          </div>
          <br />
          <div className="field">
            <h3 className="ui blue center aligned header">
              Cena porudzbine: {porudzbina.cenaPorudzbine}
            </h3>
          </div>
          <br />
          <div className="field">
            <h3 className="ui blue center aligned header">
              Komentar: {porudzbina.komentar}
            </h3>
          </div>
          <br />
        </div>
        <div className="field">
          <button className="ui blue right floated button"
                  onClick={handleClickPovratak}>Povratak</button>
        </div>
      </div>
      <br /></>)}
    </div>
  );
}
