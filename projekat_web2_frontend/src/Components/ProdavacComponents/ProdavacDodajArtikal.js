import React, { useState } from "react";

const ProdavacDodajArtikal = () => {

    const [naziv, setNaziv] = useState('');
    const [cena, setCena] = useState('');
    const [kolicina, setKolicina] = useState(''); 
    const [opis, setOpis] = useState('');
    
    const handleImageLoad = () =>{ 

    }

    const handleSubmit = (e) => {
        e.preventDefault()
        console.log(naziv, cena, kolicina, opis);
        
    }

    return (
        <div className="card">
            <form className="ui form" onSubmit={handleSubmit} >
                <h2 className="ui center aligned header">Unos novog artikla</h2>
                <div className="two fields">
                    <div className="field">
                        <img className="ui medium image" src="https://staticg.sportskeeda.com/editor/2022/01/3daff-16432330593294-1920.jpg"></img>
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
                </div>
                <button className="ui blue button" type="submit">Submit</button>
            </form>
        </div>
    );
}

export default ProdavacDodajArtikal;