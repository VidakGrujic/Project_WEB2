import React, { useState } from "react";
import axios from "../../api/axios";
import { useNavigate } from "react-router-dom";

const Login = ({handleAuth, handleTipKorisnika}) => {
    const LOGIN_URL = '/users/login';

    const[email, setEmail] = useState('');
    const[lozinka, setLozinka] = useState('');
    const[error, setError] = useState(false);

    const navigate = useNavigate();

    

    const setInputsToEmpty = () => {
        setEmail('');
        setLozinka(''); 
    }

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
    
        if(email.length === 0 || lozinka.length === 0){
            setError(true)
            return;
        }

        try{
            const response = await axios.post(`${process.env.REACT_APP_API_BACK}${LOGIN_URL}`,
                JSON.stringify({email, lozinka}),
                {
                    headers:{'Content-Type' : 'application/json'},
                    withCredentials: true
                }
            );
            console.log(response.data);
            handleAuth(true);
            
            sessionStorage.setItem('token', JSON.stringify(response.data.token))
            sessionStorage.setItem('korisnik', JSON.stringify(response.data.korisnikDto));
            const tipKorisnika = response.data.korisnikDto.tipKorisnika; // propertiji su mala slova
            handleTipKorisnika(tipKorisnika);
            redirectTo(tipKorisnika);
            
        }
        catch(err){
            alert("Nesto se desilo");
            handleAuth(false);
            setInputsToEmpty();
        }

    }

    return (
        <div className="card">
            <form className="ui form" onSubmit={handleSubmit}>
                <h2 className="ui center aligned header">Log in</h2>
                <div className="field">
                    <label>Email</label>
                    <input type="email"
                           value={email} 
                           name="email" 
                           placeholder="Email" 
                           onChange={(e) => setEmail(e.target.value)}/>
                    {error && email.length === 0 ? <div className="ui pointing red basic label">Morate uneti email</div> : null}
                </div>
                <div className="field">
                    <label>Lozinka</label>
                    <input type="password" 
                           value={lozinka}
                           name="lozinka" 
                           placeholder="Lozinka"
                           onChange={(e) => setLozinka(e.target.value)}/>
                    
                   {error && lozinka.length === 0 ? <div className="ui pointing red basic label">Morate uneti lozinku</div> : null} 
                </div>
                <button className="ui button" type="submit">Log in</button>
            </form>
        </div>
    );
}

export default Login;