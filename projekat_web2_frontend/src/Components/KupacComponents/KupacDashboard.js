import React, { useState, useEffect } from "react";
import axios from "../../api/axios";

const KupacDashboard = () => {
  const [artikli, setArtikli] = useState([]);
  const [izabraniArtikli, setIzabraniArtikli] = useState([]); //ovo su artikli koji sadrze id artikla i kolicinu koju korisnik hoce da poruic
  const GET_ARTIKLE_URL = "/products/getAll";

  useEffect(() => {
    const getArtikle = async () => {
      try {
        const { data } = await axios.get(
          `${process.env.REACT_APP_API_BACK}${GET_ARTIKLE_URL}`,
          {
            headers: {
              "Content-Type": "application/json",
              //Authorization : `Bearer ${token}`
            },
          }
        );

        setArtikli(data);
      } catch (err) {
        alert("Nesto se desilo prilikom dobavljanja artikala");
      }
    };
    getArtikle();
  }, []);

  /*napravim klik event, kodi dodaje element u niz recnika gde ima id artikla i vrednost    
        e sad ovako, kad se klikne na to dugme dodaj artikal, aktivira se event, indeks dugmeta je indeks artikla
        onda se uzima taj id i trazi se da se dobije vrednost input elementa na osnovu njegovog imena,
        Ime input elementa odredjeno je input + id artikla.
        Potom kad smo dobavili te vrednosti, onda treba da ih sacuvamo u taj niz  
      
      */

  //napraviti dodatno dugme koje ce da stavlja niz artikala i kolicina u session storage i onda redirektuje na
  //dodavanje porudzbine

  //PROVERE, PROVERITI KOLICINU, I PISATI ALERTOVE KAD JE DODAT USPESNO ILI OBRISAN ARTIKAL
  //PROMENITI SA CARD NA TABLE

  const handleClickDodajArtikal = (e) => {
    //na ovaj nacin sam dobio unetu kolicinu ya taj konkretan element
    const artikalId = parseInt(e.target.id);
    const unetaKolicina = parseInt(
      document.getElementsByName(`input ${e.target.id}`)[0].value
    );
    if (unetaKolicina !== 0) {
      //na ovaj nacin omogucavam korisniku da moze da obrise artikal
      const izbacivanjeDugmeClass = (document.getElementsByName(
        `button ${e.target.id}`
      )[0].className = "ui red labeled icon button");
      //ovde ide logika za ubacivanje elementa u niz gde se cuva id artikla i porudzbina
      /*
                logika je, napraviti niz objekata gde ce objekat biti
            {
                id: idArtikla (odnosno dobijenog iz e.target.id)
                kolicina : unetaKolicina
            }

            potrebno je prethodno proveriti da li se objekat sa istim id-jem nalazi u nizu.
            Ako se nalazi, onda je potrebno azurirati vrednost, ako ne onda ga dodati
        */

      //proveravam da li postoji objekat
      const isFound = izabraniArtikli.some((artikal) => {
        return artikal.artikalId === artikalId;
      });

      //ako postoji azuriram
      if (isFound) {
        setIzabraniArtikli((izabraniArtikal) => {
          const noviIzabraniArtikli = izabraniArtikal.map((obj) => {
            if (obj.artikalId === artikalId) {
              return { ...obj, kolicina: unetaKolicina };
            }
            return obj;
          });
          return noviIzabraniArtikli;
        });
      } else {
        //ako ne postoji onda ga dodajem
        const noviIzabraniArtikal = {
          artikalId: artikalId,
          kolicina: unetaKolicina,
        };
        setIzabraniArtikli([...izabraniArtikli, noviIzabraniArtikal]);
      }
    } else {
      alert("Morate uneti kolicinu");
    }
  };

  //ovo moram jer pravi neki problem
  useEffect(() => {
    console.log(izabraniArtikli);
    //upisem u session storage
    sessionStorage.setItem("Porudzbina", JSON.stringify(izabraniArtikli));
  }, [izabraniArtikli]);

  const handleClickIzbaciArtikal = (e) => {
    //kad korisnik hoce da izbaci artikal iz porudzbine, tad treba ponovo da disable ovo dugme
    const izbacivanjeDugmeClass = (document.getElementsByName(
      e.target.name
    )[0].className = "ui disabled red labeled icon button");

    //ovde ide logika za izbacivanje elementa iz onog niza gde cuvam id artikla i kolicina
    /*
            logika je sledeca
            parsiramo id iz e.target.name i potom pokusamo da pronadjemo element u nizu ertikala i kolicina
            ako ga ima, brisemo ga, ako ga nema bacamo gresku
        */

    //parsiram name da dobijem id
    const stringId = e.target.name.split(" ");
    const artikalId = parseInt(stringId[1]); //parsiram u int

    //filter daje niz svih elemenata koji zadovoljavaju uslov
    //daje sve elemente koji ciji id nije tu i to je to
    setIzabraniArtikli(
      izabraniArtikli.filter(
        (izabraniArtikal) => izabraniArtikal.artikalId !== artikalId
      )
    );
  };

  return (
    <div className="verification-container">
      <div className="ui blue segment">
        <div className="ui divided items">
          {artikli.map((artikal) => (
            <div className="item">
              <div className="ui small image">
                <img className="ui fluid image" src={artikal.fotografija}></img>
              </div>
              <div className="content">
                <div className="header">{artikal.naziv}</div>
                <div className="meta">
                  <span className="price">Cena artikla: {artikal.cena}</span>
                </div>
                <div className="meta">
                  <span className="stay">
                    Dostupna kolicina: {artikal.kolicina}
                  </span>
                </div>
                <div className="description">{artikal.opis}</div>
              </div>
              <div className="content">
                <div className="header">Unesite zeljenu kolicinu</div>
                <div className="description">
                  <div className="ui left labeled input">
                    <input
                      type="number"
                      placeholder="Unesite kolicinu"
                      /*name ce biti input + id artikla*/
                      name={`input ${artikal.id}`}
                    ></input>
                  </div>
                </div>
              </div>
              <div className="content">
                {/*stavicu dva dugmeta
                jedno koje kad se klikne dodaje artikal u 
                index ovog zelenog ce biti id artikla
                dok ce ovog crvenog biti dugme + id artikla*/}
                <div className="extra">
                  <button
                    className="ui green labeled icon button"
                    id={artikal.id}
                    onClick={(e) => handleClickDodajArtikal(e)}
                  >
                    <i className="check icon"></i>
                    Dodajte artikal {artikal.id}
                  </button>
                </div>
                <div className="extra">
                  <button
                    className="ui disabled red labeled icon button"
                    name={`button ${artikal.id}`}
                    onClick={(e) => handleClickIzbaciArtikal(e)}
                  >
                    <i className="x icon"></i>
                    Izbacite artikal
                  </button>
                </div>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default KupacDashboard;
