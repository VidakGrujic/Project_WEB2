import React, { useState } from "react";
import axios from "../api/axios";

const Registration = () => {
    const [korisnickoIme, setKorisnickoIme] = useState('');
    const [email, setEmail] = useState('');
    const [lozinka, setLozinka] = useState('');
    const [lozinka2, setLozinka2] = useState('');
    const [ime, setIme] = useState('');
    const [prezime, setPrezime] = useState('');
    const [datumRodjenja, setDatumRodjenja] = useState('');
    const [tipKorisnika, setTipKorisnika] = useState('Kupac');
    const [adresa, setAdresa] = useState('')
    const [statusVerifikacije, setStatusVerifikacije] = useState('Prihvacen');
  
    const REGISTRATION_URL = "/users/registration";

    const handleSubmit = async (event) => {
        event.preventDefault();
        /*console.log(korisnickoIme);
        console.log(email);
        console.log(lozinka);
        console.log(lozinka2);
        console.log(ime);
        console.log(prezime);
        console.log(datumRodjenja);
        console.log(tipKorisnika);
        console.log(adresa);
        console.log(statusVerifikacije);*/

        //uraditi provere za lozinke, tj. da li se prva i druga poklapaju i da li su uneta stva polja
        if(lozinka === lozinka2){
            const korisnikJSON = JSON.stringify({
                korisnickoIme,
                email,
                lozinka,
                ime,
                prezime,
                datumRodjenja,
                tipKorisnika,
                adresa,
                statusVerifikacije
            });

            try {
                const response = await axios.post(`${process.env.REACT_APP_API_BACK}${REGISTRATION_URL}`,
                    korisnikJSON,
                    {
                        headers:{'Content-Type' : 'application/json'},
                        withCredentials: true
                    }
                );
                console.log(response.data);
            } catch (err) { 
                alert("Nesto se desilo prilikom registracije");
            }
        }
        
        


    }

    return (
      <div className="card">
            <form className="ui form" onSubmit={handleSubmit}>
            <h2 className="ui center aligned header">Registration</h2>
                <div className="field">
                    <label>Korisnicko ime</label>
                    <input  type="text" 
                            name="korisnickoIme"  
                            placeholder="Korisnicko ime"
                            value={korisnickoIme}
                            onChange={(e) => setKorisnickoIme(e.target.value)}
                            />
                </div>
                <div className="field">
                    <label>Email</label>
                    <input type="email"
                           name="email" 
                           placeholder="Email"
                           value={email}
                           onChange={(e) => setEmail(e.target.value)}/>
                </div>
                <div className="field">
                    <div className="two fields"> 
                        <div className="field">
                            <label>Lozinka</label>
                            <input  type="password" 
                                    name="lozinka" 
                                    placeholder="Lozinka"
                                    value={lozinka}
                                    onChange={(e) => setLozinka(e.target.value)}/>
                        </div>
                        <div className="field">
                            <label>Potvrdite lozinku</label>
                            <input  type="password" 
                                    name="lozinka2" 
                                    placeholder="Potvrdite lozinku"
                                    value={lozinka2}
                                    onChange={(e) => setLozinka2(e.target.value)}
                                    />
                        </div>
                    </div>
                </div>
                <div className="field">
                    <div className="two fields">
                        <div className="field">
                            <label>Ime</label>
                            <input  type="text"
                                    name="ime" 
                                    placeholder="Ime"
                                    value={ime}
                                    onChange={(e) => setIme(e.target.value)}
                                    />
                        </div>
                        <div className="field">
                            <label>Prezime</label>
                            <input  type="text" 
                                    name="prezime" 
                                    placeholder="Prezime"
                                    value={prezime}
                                    onChange={(e) => setPrezime(e.target.value)}
                                    />
                        </div>
                    </div>
                </div>
                <div className="fields">
                    <div className="six wide field">
                        <label>Datum Rodjenja</label>
                        <input type="date"
                               name="datumRodjenja" 
                               placeholder="Datum"
                               value={datumRodjenja}
                               onChange={(e) => setDatumRodjenja(e.target.value)}
                               />
                    </div>
                    <div className="ten wide field">
                        <label>Tip korisnika</label>
                        <select value={tipKorisnika} className="ui fluid dropdown" onChange={(e) => setTipKorisnika(e.target.value)}>
                            <option value="Kupac">Kupac</option>
                            <option value="Prodavac">Prodavac</option>
                        </select>
                    </div>
                </div>
                <div className="field">
                    <label>Adresa</label>
                    <input  type="text" 
                            name="adresa" 
                            placeholder="Adresa"
                            value={adresa}
                            onChange={(e) => setAdresa(e.target.value)}
                            />
                </div>
                <button className="ui button" type="submit">Submit</button>
            </form>
        </div>
    );
}

export default Registration;