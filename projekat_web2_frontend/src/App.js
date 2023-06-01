import './App.css';
import NavBar from './Components/NavBar';
import Login from './Components/AuthAndAutorizationComponents/Login';
import Registration from './Components/AuthAndAutorizationComponents/Registration';
import Home from './Components/Home';
import { Route, Routes } from 'react-router-dom';
import { useState } from 'react';
import KupacDashboard from './Components/KupacComponents/KupacDashboard';
import KupacPrethodnePorudzbine from './Components/KupacComponents/KupacPrethodnePorudzbine';
import ProdavacDashboard from './Components/ProdavacComponents/ProdavacDashboard';
import ProdavacMojePorudzbine from './Components/ProdavacComponents/ProdavacMojePorudzbine';
import ProdavacDodajArtikal from './Components/ProdavacComponents/ProdavacDodajArtikal';
import ProdavacNovePorudzbine from './Components/ProdavacComponents/ProdavacNovePorudzbine'
import AdminDashboard from './Components/AdministratorComponents/AdminDashboard';
import AdminSvePorudzbine from './Components/AdministratorComponents/AdminSvePorudzbine';
import AdminVerifikacija from './Components/AdministratorComponents/AdminVerfikacija';
import Profil from './Components/ProfileComponents/Profil';
import KupacPoruci from './Components/KupacComponents/KupacPoruci';
function App() {

  //da li je korisnik autentifikovan, on je atuentifikovan i posle registracije i posle logovanje
  const [isAuth, setIsAuth] = useState(false);
  const [tipKorisnika, setTipKorisnika] = useState('');
  const [statusVerifikacije, setStatusVerifikacije] = useState('');

  const handleAuth = (autentifikovan) => {
    setIsAuth(autentifikovan);
  }

  const handleTipKorisnika = (tipKorisnika) => {
    setTipKorisnika(tipKorisnika);
  }

  const handleStatusVerifikacije = (statusVerifikacije) => {
    setStatusVerifikacije(statusVerifikacije);
  }

  const routes = [
    {path: '/', element: <Home></Home>},
    {path: '/login', element: <Login handleAuth={handleAuth} handleTipKorisnika={handleTipKorisnika} handleStatusVerifikacije={handleStatusVerifikacije}></Login>},
    {path: '/registration', element: <Registration handleAuth={handleAuth} handleTipKorisnika={handleTipKorisnika} handleStatusVerifikacije={handleStatusVerifikacije}></Registration>},
    {path: '/kupacDashboard', element: <KupacDashboard></KupacDashboard>},
    {path: '/kupacDashboard/kupacPoruci' , element: <KupacPoruci></KupacPoruci>},
    {path: '/kupacPrethodnePorudzbine', element: <KupacPrethodnePorudzbine></KupacPrethodnePorudzbine>},
    {path: '/prodavacDashboard', element: <ProdavacDashboard statusVerifikacije={statusVerifikacije}></ProdavacDashboard>},
    {path: '/prodavacDodajArtikal', element: <ProdavacDodajArtikal></ProdavacDodajArtikal>},
    {path: '/prodavacNovePorudzbine', element: <ProdavacNovePorudzbine></ProdavacNovePorudzbine>},
    {path: '/prodavacMojePorudzbine', element: <ProdavacMojePorudzbine></ProdavacMojePorudzbine>},
    {path: '/adminDashboard', element: <AdminDashboard></AdminDashboard>},
    {path: '/adminVerifikacija', element: <AdminVerifikacija></AdminVerifikacija>},
    {path: '/adminSvePorudzbine', element: <AdminSvePorudzbine></AdminSvePorudzbine>},
    {path: '/profil', element: <Profil></Profil>}
  ]

  return (
    <div className='App'>
      <NavBar isAuth={isAuth} tipKorisnika={tipKorisnika} statusVerifikacije={statusVerifikacije}/>
      <div className='container'>
        <Routes>
          {
            routes.map((route) => (
              <Route path={route.path} element={route.element}></Route>
            ))
          }
        </Routes>
      </div>
    </div>
  );
}

export default App;
