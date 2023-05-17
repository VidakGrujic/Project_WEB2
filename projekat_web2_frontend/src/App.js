import './App.css';
import NavBar from './Components/NavBar';
import Login from './Components/AuthAndAutorizationComponents/Login';
import Registration from './Components/AuthAndAutorizationComponents/Registration';
import Home from './Components/Home';
import { Route, Routes } from 'react-router-dom';
import { useState } from 'react';
import KupacDashboard from './Components/KupacComponents/KupacDashboard';


function App() {

  //da li je korisnik autentifikovan, on je atuentifikovan i posle registracije i posle logovanje
  const [isAuth, setIsAuth] = useState(false);
  const [tipKorisnika, setTipKorisnika] = useState('');
 

  const handleAuth = (autentifikovan) => {
    setIsAuth(autentifikovan);
  }

  const handleTipKorisnika = (tipKorisnika) => {
    setTipKorisnika(tipKorisnika);
  }



  return (
    <div className='App'>
      <NavBar isAuth={isAuth} tipKorisnika={tipKorisnika}/>
      <div className='container'>
        <Routes>
          <Route path='/' element={<Home/>}/>
          <Route path="/login" element={<Login handleAuth={handleAuth} handleTipKorisnika={handleTipKorisnika}/>}/>
          <Route path="/registration" element={<Registration handleAuth={handleAuth} handleTipKorisnika={handleTipKorisnika}/>}/>
          <Route path='/kupacDashboard' element={<KupacDashboard/>} />
        </Routes>
      </div>
    </div>
  );
}

export default App;
