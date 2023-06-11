import React from "react";
import { NavLink, useNavigate } from "react-router-dom";

const NavBar = ({isAuth, tipKorisnika, statusVerifikacije, handleLogout}) => {

    const active = (isActive) =>{
        if(isActive)
            return "item active"
        else
            return "item"
    }

    

    return (
        <div className="ui inverted blue secondary menu">
            {/*nelogovani i neregistrovani korisnici
            oni vide home, register i login. Posle cu dodavati za role, da li je korisnik ovakav ili onakav*/}
            {isAuth ? null : <NavLink className={({isActive}) => active(isActive)} to="/" >Home page</NavLink> }
            {isAuth ? null : <NavLink className={({isActive}) => active(isActive)} to="/login">Log in</NavLink> }
            {isAuth ? null : <NavLink className={({isActive}) => active(isActive)} to="/registration">Registration</NavLink> }
            
            {/* logovani korisnik koji je kupac, dodati proveru za role*/}
            {isAuth && tipKorisnika === 'Kupac' ? <NavLink className={({isActive}) => active(isActive)} to="/kupacDashboard">Kupac Dashboard</NavLink> : null}  
            {/*isAuth && tipKorisnika === 'Kupac' ? <NavLink className={({isActive}) => active(isActive)} to="/kupacPoruci"></NavLink> : null*/}
            {isAuth && tipKorisnika === 'Kupac' ?  <NavLink className={({isActive}) => active(isActive)} to="/kupacPorudzbine">Kupac porudzbine</NavLink> : null}
            

            {/*logovani korisnik koji je prodavac, dodati proveru za role, dodati da li je korisnik verifikovan*/}
            {isAuth && tipKorisnika === 'Prodavac' ? <NavLink className={({isActive}) => active(isActive)} to="/prodavacDashboard">Prodavac dashboard</NavLink> : null}
            {isAuth && tipKorisnika === 'Prodavac' && statusVerifikacije === 'Prihvacen' ? <NavLink className={({isActive}) => active(isActive)} to="/prodavacDodajArtikal">Prodavac dodaj artikal</NavLink> : null}
            {isAuth && tipKorisnika === 'Prodavac' && statusVerifikacije === 'Prihvacen' ? <NavLink className={({isActive}) => active(isActive)} to="/prodavacNovePorudzbine">Prodavac nove porudzbine</NavLink> : null}
            {isAuth && tipKorisnika === 'Prodavac' && statusVerifikacije === 'Prihvacen' ? <NavLink className={({isActive}) => active(isActive)} to="/prodavacPrethodnePorudzbine">Prodavac prethodne porudzbine</NavLink> : null}
            {isAuth && tipKorisnika === 'Prodavac' && statusVerifikacije === 'Prihvacen' ? <NavLink className={({isActive}) => active(isActive)} to="/prodavacPregledArtikala">Pregled Artikala</NavLink> : null}

            {/*logovani korisnik koji je admin, dodati proveru za role*/}
            {isAuth && tipKorisnika === 'Administrator' ? <NavLink className={({isActive}) => active(isActive)} to="/adminDashboard">Admin dashboard</NavLink> : null}
            {isAuth && tipKorisnika === 'Administrator' ? <NavLink className={({isActive}) => active(isActive)} to="/adminVerifikacija">Admin Verifikaicja</NavLink> : null}
            {isAuth && tipKorisnika === 'Administrator' ? <NavLink className={({isActive}) => active(isActive)} to="/adminSvePorudzbine">Admin sve porudzbine</NavLink> : null}
           

            {/*logovani korisnik bez obzira na ulogu*/}
            {isAuth && statusVerifikacije === 'Prihvacen' ? <NavLink className={({isActive}) => active(isActive)} to="/profil">Profil</NavLink> : null}
            {isAuth ? <NavLink className={({isActive}) => active(isActive)} onClick={handleLogout} to="/">Logout</NavLink> : null}


        </div>
    )
}


export default NavBar;