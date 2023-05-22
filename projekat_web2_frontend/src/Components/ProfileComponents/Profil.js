import React, { useState } from "react";
import {  useNavigate } from "react-router-dom";
import "react-datepicker/dist/react-datepicker.css";
import DatePicker from "react-datepicker";
import axios from "../../api/axios";
const Profil = () => {

    const korisnik = JSON.parse(sessionStorage.getItem('korisnik'));

    const id = korisnik.id;
    const tipKorisnika = korisnik.tipKorisnika;
    const statusVerifikacije = korisnik.statusVerifikacije;
    const [korisnickoIme, setKorisnickoIme] = useState(korisnik.korisnickoIme);
    const [email, setEmail] = useState(korisnik.email);
    const [lozinka, setLozinka] = useState(korisnik.lozinka);
    const [ime, setIme] = useState(korisnik.ime);
    const [prezime, setPrezime] = useState(korisnik.prezime);
    const [datumRodjenja, setDatumRodjenja] = useState(new Date(korisnik.datumRodjenja));
    const [adresa, setAdresa] = useState(korisnik.adresa);
    
    const navigate = useNavigate();
    const UPDATE_URL = "/users/" + id ; //treba da dobije id isto

    const [error, setError] = useState(false);


    const redirectTo = (tipKorisnika) => {
        if(tipKorisnika === 'Admin'){
            navigate('/adminDashboard');
        }
        else if(tipKorisnika === 'Kupac'){
            navigate('/kupacDashboard');
        }
        else if(tipKorisnika === 'Prodavac'){
            navigate('/prodavacDashboard');
        }
    }

    const handleSubmit = async (event) => {
        event.preventDefault();

        if(korisnickoIme.length === 0 || email.length === 0 || lozinka.length === 0 
            || ime.length === 0 || prezime.length === 0 || datumRodjenja === null || adresa.length === 0){
                setError(true);
                return;
            }

            const updatedKorisnikJSON = JSON.stringify({
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

            try{
                const respones = await axios.put(
                    `${process.env.REACT_APP_API_BACK}${UPDATE_URL}`,
                    updatedKorisnikJSON,
                    {
                        headers: {'Content-Type' : 'application/json'},
                        withCredentials: true
                    }
                );
                
                sessionStorage.setItem('korisnik', JSON.stringify(respones.data));
                alert("Uspesno ste izmenili podatke")
                redirectTo(tipKorisnika); 

            }
            catch (err){
                alert("Nesto se desilo prilikom izmene profila");
            }
    }
    
    return(
        <div className="card">
            <form className="ui form" onSubmit={handleSubmit}>
            <h2 className="ui center aligned header">Izmenite Profil</h2>
                <div className="field">
                    <label>Korisnicko ime</label>
                    <input  type="text" 
                            name="korisnickoIme"  
                            placeholder="Korisnicko ime"
                            value={korisnickoIme}
                            onChange={(e) => setKorisnickoIme(e.target.value)}
                            />
                    {error && korisnickoIme.length === 0 ? <div className="ui pointing red basic label">Morate uneti korisnickoIme</div> : null}
                </div>
                <div className="field">
                    <label>Email</label>
                    <input disabled
                           type="email"
                           name="email" 
                           placeholder="Email"
                           value={email}
                           onChange={(e) => setEmail(e.target.value)}/>
                    {error && email.length === 0 ? <div className="ui pointing red basic label">Morate uneti email</div> : null}
                </div>
                <div className="field">
                    <label>Lozinka</label>
                    <input  type="password" 
                            name="lozinka" 
                            placeholder="Lozinka"
                            value={lozinka}
                            onChange={(e) => setLozinka(e.target.value)}/>
                    {error && lozinka.length === 0 ? <div className="ui pointing red basic label">Morate uneti lozinku</div> : null}
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
                            {error && ime.length === 0 ? <div className="ui pointing red basic label">Morate uneti ime</div> : null}
                        </div>
                        <div className="field">
                            <label>Prezime</label>
                            <input  type="text" 
                                    name="prezime" 
                                    placeholder="Prezime"
                                    value={prezime}
                                    onChange={(e) => setPrezime(e.target.value)}
                                    />
                            {error && prezime.length === 0 ? <div className="ui pointing red basic label">Morate uneti prezime</div> : null}
                        </div>
                    </div>
                </div>
                <div className="field">
                    <label>Datum Rodjenja</label>
                    <DatePicker 
                        showIcon
                        selected={datumRodjenja}
                        onChange={datum => setDatumRodjenja(datum)}
                        dateFormat="dd/MM/yyyy"
                        showYearDropdown
                        scrollableMonthYearDropdown
                    />
                    {error && datumRodjenja === null ? <div className="ui pointing red basic label">Morate izabrati datum rodjenja</div> : null}
                </div>
                <div className="field">
                    <label>Adresa</label>
                    <input  type="text" 
                            name="adresa" 
                            placeholder="Adresa"
                            value={adresa}
                            onChange={(e) => setAdresa(e.target.value)}
                            />
                    {error && prezime.length === 0 ? <div className="ui pointing red basic label">Morate uneti adresu</div> : null}
                </div>
                <button className="ui button" type="submit">Submit</button>
            </form>
        </div>
    ) 
    


     
}

export default Profil;