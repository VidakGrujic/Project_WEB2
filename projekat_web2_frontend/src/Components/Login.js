import React, { useState } from "react";
import axios from "../api/axios";

const Login = () => {

    const LOGIN_URL = '/users/login';

    const[email, setEmail] = useState('');
    const[lozinka, setLozinka] = useState('');


    const handleSubmit = async (event) => {
        event.preventDefault();
    
        try{
            const response = await axios.post(`${process.env.REACT_APP_API_BACK}${LOGIN_URL}`,
                JSON.stringify({email, lozinka}),
                {
                    headers:{'Content-Type' : 'application/json'},
                    withCredentials: true
                }
            );
            console.log(response.data);
            setEmail('');
            setLozinka('');
        }
        catch(err){
            alert("Nesto se desilo");
        }

    }
    /*
        KAD SE RADI LOGIN, TREBA PROVERITI KUPCEV STATUS VERIFIKACIJE I NA OSNOVU TOGA GA REDIREKTOVATI
    */

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
                </div>
                <div className="field">
                    <label>Lozinka</label>
                    <input type="password" 
                           value={lozinka}
                           name="lozinka" 
                           placeholder="Lozinka"
                           onChange={(e) => setLozinka(e.target.value)}/>
                </div>
                <button className="ui button" type="submit">Log in</button>
            </form>
        </div>
    );
}

export default Login;