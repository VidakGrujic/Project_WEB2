import React, { useState } from "react";
import axios from "../../api/axios";

const ProdavacDodajArtikal = () => {

    const [naziv, setNaziv] = useState('');
    const [cena, setCena] = useState(0);
    const [kolicina, setKolicina] = useState(0); 
    const [opis, setOpis] = useState('');
    const [fotografija, setFotografija] = useState("https://staticg.sportskeeda.com/editor/2022/01/3daff-16432330593294-1920.jpg");
    
    const ADD_ARTIKAL_URL = '/products/addArtikal';

    const [error, setError] = useState(false);

    const handleImageLoad = () =>{ 

    }

    const setInputsToEmpty = () => {
        setNaziv('');
        setCena(0);
        setKolicina(0);
        setOpis('');
    }


    const handleSubmit = async (e) => {
        e.preventDefault()
        console.log(naziv, cena, kolicina, opis);
        const prodavac = JSON.parse(sessionStorage.getItem('korisnik'));
        const token = sessionStorage.getItem('token');
        const prodavacId = prodavac.id;
        const cenaDostave = prodavac.cenaDostave;

        if(naziv.length === 0 || cena === 0 || Math.floor(kolicina) === 0
        || opis.length === 0 || fotografija.length === 0){
            setError(true);
            return;
        }

        const artikalDto = JSON.stringify({
            naziv, cena, kolicina, opis, fotografija, prodavacId, cenaDostave
        });

        const url = `${process.env.REACT_APP_API_BACK}${ADD_ARTIKAL_URL}`;
        try{
            const response = await axios.post(url,
                artikalDto,
                {
                    headers:{
                        'Content-Type' : 'application/json',
                        Authorization : `Bearer ${token}`
                    },
                    withCredentials: true
                }
            );
            setInputsToEmpty();
            alert("Artikal je uspesno dodat")
            console.log(response)
        }catch(err){
            const result = err.response.data;
            alert(result);
            setInputsToEmpty();
        }
    }

    return (
        <div className="card">
            <form className="ui form" onSubmit={handleSubmit} >
                <h2 className="ui center aligned header">Unos novog artikla</h2>
                <div className="two fields">
                    <div className="field">
                        <img className="ui medium image" src={fotografija}></img>
                        {error && fotografija.length === 0 ?  <div className="ui pointing red basic label">Morate odabrati fotografiju</div> : null}
                    </div>
                    <div className="field">
                        <div className="load-picture-button">
                            <button className="ui blue button" onClick={handleImageLoad}>Ucitajte sliku</button>
                        </div>
                    </div>
                </div>
               
                <div className="field">
                    <label>Naziv artikla</label>
                    <input  type="text" 
                            name="naziv"
                            placeholder="Naziv artikla"
                            value={naziv}
                            onChange={(e) => setNaziv(e.target.value)}
                            />
                    {error && naziv.length === 0 ? <div className="ui pointing red basic label">Morate uneti naziv artikla</div> : null}
                </div>
                <div className="two fields">
                    <div className="field">
                        <label>Cena artikla</label>
                        <input  type="number" 
                                step="any"
                                name="cena"
                                value={cena}
                                onChange={(e) => setCena(e.target.value)}
                                placeholder="Cena artikla"
                                />
                        {error && cena === 0 ? <div className="ui pointing red basic label">Morate cenu artikla</div> : null}
                    </div>
                    <div className="field">
                        <label>Kolicina artikla</label>
                        <input  type="number"
                                step="1"
                                name="kolicina"
                                value={kolicina}
                                onChange={(e) => setKolicina(e.target.value)}
                                placeholder="Kolicina artikla"
                        />
                        {error && Math.floor(kolicina) === 0 ? <div className="ui pointing red basic label">Morate uneti kolicinu artikla</div> : null}
                    </div>
                </div>
                <div className="field">
                    <label>Opis</label>
                    <textarea className="textarea-resize"
                              rows="6" 
                              placeholder="Unesite opis artikla"
                              value={opis}
                              onChange={(e) => setOpis(e.target.value)}
                    /> 
                    {error && opis.length === 0 ? <div className="ui pointing red basic label">Morate opis artikla</div> : null}
                </div>
                <button className="ui blue button" type="submit">Dodajte artikal</button>
            </form>
        </div>
    );
}

export default ProdavacDodajArtikal;