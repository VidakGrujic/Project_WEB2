import React, { useState, useEffect } from "react";
import axios from "../../api/axios";
import { useNavigate } from "react-router-dom";

const KupacDashboard = () => {
  const [artikli, setArtikli] = useState([]);
  const [izabraniArtikli, setIzabraniArtikli] = useState([]); //ovo su artikli koji sadrze id artikla i kolicinu koju korisnik hoce da poruic
  const [loading, setLoading] = useState(true);

  //koriste se za prikaz poruke i njen kontent
  const [showMessage, setShowMessage] = useState(false);
  const [message, setMessage] = useState({
    type: "", //dal je pozitivna ili negativna poruka
    content: "",
  });

  const navigate = useNavigate();

  const GET_ARTIKLE_URL = "/products/getAll";
  const token = sessionStorage.getItem('token')

  useEffect(() => {
    const getArtikle = async () => {
      setLoading(true);
      setShowMessage(false); //dok se ne ucitaju podaci, nema prikazivanja poruke
      try {
        const { data } = await axios.get(
          `${process.env.REACT_APP_API_BACK}${GET_ARTIKLE_URL}`,
          {
            headers: {
              "Content-Type": "application/json",
              Authorization : `Bearer ${token}`
            },
          }
        );

        setArtikli(data);
        setLoading(false);
        setMessage({
          type: "positive",
          content: "Uspesno ucitani podaci",
        });
        setShowMessage(true);
      } catch (err) {
        setLoading(true);
        setShowMessage(false);
      }
    };
    getArtikle();
  }, []);

   //ovo moram jer pravi neki problem
  useEffect(() => {
    console.log(izabraniArtikli);
    //upisem u session storage
    //ovde je potrebno upisati u local storage, da bi se ya tog korisnika cuvao tacno ta porudzbina
    /*const korinsik = JSON.parse('korinsik')
    const korisnikId = korinsik.id;
    localStorage.setItem(`porudzbina ${korisnikId}`);*/
    sessionStorage.setItem("porudzbina", JSON.stringify(izabraniArtikli));
  }, [izabraniArtikli]);

  
  //napraviti dodatno dugme koje ce da stavlja niz artikala i kolicina u session storage i onda redirektuje na
  //dodavanje porudzbine

  //PROVERE, PROVERITI KOLICINU, I PISATI ALERTOVE KAD JE DODAT USPESNO ILI OBRISAN ARTIKAL
  //PROMENITI SA CARD NA TABLE

  const handleClickDodajArtikal = (e) => {
    //na ovaj nacin sam dobio unetu kolicinu ya taj konkretan element
    const artikalId = parseInt(e.target.id);
    const artikal = artikli.find((artikal) => artikal.id === artikalId);
    const unetaKolicina = document.getElementsByName(`input ${e.target.id}`)[0].value;

    //ako je nije uneta kolicina, onda se mora uneti
    if(unetaKolicina === ""){
      setMessage({
        type: "negative",
        content: "Morate uneti kolicinu",
      });
      alert("Morate uneti kolicinu")
    }
    else if(unetaKolicina <= artikal.kolicina){ //ako je uneta kolicina, proveri da li je manja od dostupne
      //na ovaj nacin omogucavam korisniku da moze da obrise artikal
      document.getElementsByName(`izbaci ${e.target.id}`)[0].className = "ui red labeled icon button";

      //proveravam da li postoji objekat
      const isFound = izabraniArtikli.some((artikal) => {
        return artikal.artikalId === artikalId;
      });

      //ako postoji azuriram
      if (isFound) {
        setIzabraniArtikli((izabraniArtikal) => {
          const noviIzabraniArtikli = izabraniArtikal.map((obj) => {
            if (obj.artikalId === artikalId) {
              return { ...obj, kolicina: parseInt(unetaKolicina) };
            }
            return obj;
          });
          return noviIzabraniArtikli;
        });
        setMessage({
          type: "positive",
          content: `Uspesno ste azurirali kolicinu artikla ${artikal.naziv}`,
        });
        alert(`Uspesno ste azurirali kolicinu artikla ${artikal.naziv}`)
      } else {
        //ako ne postoji onda ga dodajem
        const noviIzabraniArtikal = {
          artikalId: artikalId,
          kolicina: parseInt(unetaKolicina),
          naziv: artikal.naziv,
          cena: artikal.cena,
          cenaDostave: parseInt(artikal.cenaDostave)
        };
        setIzabraniArtikli([...izabraniArtikli, noviIzabraniArtikal]);
        setMessage({
          type: "positive",
          content: `Uspesno ste dodali artikal ${artikal.naziv} u Vasu porudzbinu`,
        });
        alert(`Uspesno ste dodali artikal ${artikal.naziv} u Vasu porudzbinu`)
      }
    } else {
      setMessage({
        type: "negative",
        content: "Nema dovoljnog broja artikala",
      });
      alert("Nema dovoljnog broja artikala")
    }
  };


 
  const handleClickIzbaciArtikal = (e) => {
    //kad korisnik hoce da izbaci artikal iz porudzbine, tad treba ponovo da disable ovo dugme
    document.getElementsByName(e.target.name)[0].className = "ui disabled red labeled icon button";

    //parsiram name da dobijem id
    const stringId = e.target.name.split(" ");
    const artikalId = parseInt(stringId[1]); //parsiram u int
    const artikal = artikli.find((artikal) => artikal.id === artikalId);

    //filter daje niz svih elemenata koji zadovoljavaju uslov
    //daje sve elemente koji ciji id nije tu i to je to
    setIzabraniArtikli(
      izabraniArtikli.filter(
        (izabraniArtikal) => izabraniArtikal.artikalId !== artikalId
      )
    );
    setMessage({
      type: "positive",
      content: `Uspesno ste izbacili artikal ${artikal.naziv} iz Vase porudzbine`,
    });
    document.getElementsByName(`input ${artikal.id}`)[0].value = "";
    alert(`Uspesno ste izbacili artikal ${artikal.naziv} iz Vase porudzbine`);
  };

  const handlePoruci = () => {
    navigate("/kupacDashboard/kupacPoruci");
  } 

  return (
    <div className="verification-container">
      {loading && (
        <div className="loader-container">
          <div className="ui active inverted dimmer">
            <div className="ui large text loader">Ucitavanje prodavaca</div>
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
                <th>Zeljena kolicina</th>
                <th>Dodajte/Izbacite</th>
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
                        <div className="sub header">Cena: {artikal.cena}</div>
                        <div className="sub header">Kolicina: {artikal.kolicina}</div>
                        <div className="sub header">Cena Dostave: {artikal.cenaDostave}</div>
                      </div>
                    </h4>
                  </td>
                  <td className="center aligned">{artikal.opis}</td>
                  <td className="center aligned">
                    <div className="field">
                      <input type="number"
                            placeholder="Unesite zeljenu kolicinu"
                            name={`input ${artikal.id}`}></input>
                    </div>
                  </td>
                  <td className="center aligned">
                    {/*stavicu dva dugmeta
                    jedno koje kad se klikne dodaje artikal u 
                    index ovog zelenog ce biti id artikla
                    dok ce ovog crvenog biti dugme + id artikla*/}
                    <button className="ui green labeled icon button"
                            id={artikal.id}
                            onClick={(e) => handleClickDodajArtikal(e)}>
                        <i className="check icon"></i>
                        Dodajte artikal
                    </button> <br/>
                    <button className="ui disabled red labeled icon button" 
                            name={`izbaci ${artikal.id}`}
                            onClick={(e) => handleClickIzbaciArtikal(e)}>
                        <i className="x icon"></i>
                        Izbacite artikal
                    </button>

                  </td>
                </tr>
              ))}
            </tbody>
          </table>
          <div className="poruci-btn">
          <button className={izabraniArtikli.length !== 0 ? "huge ui green right floated button" : "huge ui disabled green right floated button"}
                  onClick={handlePoruci}>
          <i className="icon shopping cart"></i>
            Porucite
          </button>
          </div>
         
          
        </div>
      )}
    </div>
  );
};

export default KupacDashboard;
