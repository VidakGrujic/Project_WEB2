import React, {useState, useEffect} from "react";
import { DeleteArtikal, GetProdavceveArtikle } from "../../Services/ArtikalService";
import { useNavigate } from "react-router-dom";

export default function ProdavacPregledArtikala() {
  const [loading, setLoading] = useState(true);
  const [artikli, setArtikli] = useState([]);
  const [showMessage, setShowMessage] = useState(false);
  const [message, setMessage] = useState({
    type: "", //dal je pozitivna ili negativna poruka
    content: "",
  });


  const navigate = useNavigate();

  useEffect(() => {
    const getArtikle = async () => {
        const prodavac = JSON.parse(sessionStorage.getItem('korisnik'));
        const prodavacId = prodavac.id;
        const token = sessionStorage.getItem('token');
        
        const response = await GetProdavceveArtikle(prodavacId, token);
        if(response !== null){
            setArtikli(response);
            console.log(response);
            setLoading(false);
            setShowMessage(true);
            setMessage({
              type: "positive",
              content: "Uspesno ucitani podaci",
            });
        }
    }
    getArtikle();
  }, [message])


  const handleClickPromeniArtikal = (e) => {
    const artikalId = e.target.id;
    navigate(`/prodavacPregledArtikala/IzmeniArtikal/${artikalId}`);
    
  }

  const handleClickObrisiArtikal = async (e) => {
    const stringId = e.target.name.split(' ')[1];
    const artikalId = parseInt(stringId);
    const token = sessionStorage.getItem('token');
    const response = await DeleteArtikal(artikalId, token);
    if(response !== null){
      setMessage({
        type: "positive",
        content: "Uspesno obrisan artikal"
      })
      alert('Uspesno obrisan artikal')
    }
  }

  return ( 
    <div className="verification-container">
      {loading && (
        <div className="loader-container">
          <div className="ui active inverted dimmer">
            <div className="ui large text loader">Ucitavanje Artikala</div>
          </div>
        </div>
      )}
      {!loading && (
        <div>
        {showMessage && (
            <div className={`ui large ${message.type} message`}>
              <div className="ui center aligned header">{message.content}</div>
            </div>
          )}
          <table className="ui fixed blue celled table">
            <thead>
              <tr>
                <th>Artikal</th>
                <th>Opis</th>
                <th>Obrisite/Izmenite</th>
              </tr>
            </thead>
            <tbody>
              {artikli.map((artikal) => (
                <tr>
                  <td>
                    <h4 className="ui image header">
                      <img
                        className="ui big image"
                        src={artikal.fotografija}
                      ></img>
                      <div className="content">
                        {artikal.naziv}
                        <div className="sub header">Cena: {artikal.cena} $</div>
                        <div className="sub header">
                          Kolicina: {artikal.kolicina}
                        </div>
                        <div className="sub header">
                          Cena Dostave: {artikal.cenaDostave}
                        </div>
                      </div>
                    </h4>
                  </td>
                  <td className="center aligned">{artikal.opis}</td>
                  <td className="center aligned">
                    <button
                      className="ui blue labeled icon button"
                      id={artikal.id}
                      onClick={(e) => handleClickPromeniArtikal(e)}
                    >
                      <i className="check icon"></i>
                      Promenite artikal
                    </button>{" "}
                    <br />
                    <button
                      className="ui red labeled icon button"
                      name={`obrisi ${artikal.id}`}
                      onClick={(e) => handleClickObrisiArtikal(e)}
                    >
                      <i className="x icon"></i>
                      Obrisite Artikal
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
}
