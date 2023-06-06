import './App.css';
import NavBar from './Components/NavBar';
import Login from './Components/AuthAndAutorizationComponents/Login';
import Registration from './Components/AuthAndAutorizationComponents/Registration';
import Home from './Components/Home';
import { Route, Routes } from 'react-router-dom';
import { useEffect, useState } from 'react';
import KupacDashboard from './Components/KupacComponents/KupacDashboard';
import KupacPorudzbine from './Components/KupacComponents/KupacPorudzbine';
import ProdavacDashboard from './Components/ProdavacComponents/ProdavacDashboard';
import ProdavacMojePorudzbine from './Components/ProdavacComponents/ProdavacMojePorudzbine';
import ProdavacDodajArtikal from './Components/ProdavacComponents/ProdavacDodajArtikal';
import ProdavacNovePorudzbine from './Components/ProdavacComponents/ProdavacNovePorudzbine'
import AdminDashboard from './Components/AdministratorComponents/AdminDashboard';
import AdminSvePorudzbine from './Components/AdministratorComponents/AdminSvePorudzbine';
import AdminVerifikacija from './Components/AdministratorComponents/AdminVerfikacija';
import Profil from './Components/ProfileComponents/Profil';
import KupacPoruci from './Components/KupacComponents/KupacPoruci';
import PrikazPorudzbine from './Components/PorudzbinaComponents/PrikazPorudzbina'
function App() {

  //da li je korisnik autentifikovan, on je atuentifikovan i posle registracije i posle logovanje
  const [isAuth, setIsAuth] = useState(false);
  const [tipKorisnika, setTipKorisnika] = useState('');
  const [statusVerifikacije, setStatusVerifikacije] = useState('');
  const [isKorisnikInfoGot, setIsKorisnikInfoGot] = useState(false);  //ovo govori da li smo dobili podatke o korisniku

  useEffect(() => {
    const getAuth = () => {
        if(sessionStorage.getItem('korisnik') !== null && sessionStorage.getItem('isAuth') !== null){
            setIsAuth(JSON.parse(sessionStorage.getItem('isAuth')))
            const korisnik = JSON.parse(sessionStorage.getItem('korisnik'))
            setTipKorisnika(korisnik.tipKorisnika);
            setStatusVerifikacije(korisnik.statusVerifikacije);
        }
    }
    getAuth();
  }, [isKorisnikInfoGot]); //kada dobijemo ove podatke, ova funkcija ce se rerenderovati i onda ce se azurirati stanja
                            //na taj nacin izqazvacemo ponovno azuriranje stranice i onda navbara, nadam se da je tako
  
  const handleKorisnikInfo = (gotKorisnikInfo) => {
    setIsKorisnikInfoGot(gotKorisnikInfo);
  }

  const routes = [
    {path: '/', element: <Home></Home>},
    {path: '/login', element: <Login handleKorisnikInfo={handleKorisnikInfo}></Login>},
    {path: '/registration', element: <Registration handleKorisnikInfo={handleKorisnikInfo}></Registration>},
    {path: '/kupacDashboard', element: <KupacDashboard></KupacDashboard>},
    {path: '/kupacDashboard/kupacPoruci' , element: <KupacPoruci></KupacPoruci>},
    {path: '/kupacPorudzbine', element: <KupacPorudzbine></KupacPorudzbine>},
    {path: '/kupacPorudzbine/PrikazPorudzbine/:id', element: <PrikazPorudzbine></PrikazPorudzbine>},
    {path: '/prodavacDashboard', element: <ProdavacDashboard statusVerifikacije={statusVerifikacije}></ProdavacDashboard>},
    {path: '/prodavacDodajArtikal', element: <ProdavacDodajArtikal></ProdavacDodajArtikal>},
    {path: '/prodavacNovePorudzbine', element: <ProdavacNovePorudzbine></ProdavacNovePorudzbine>},
    {path: '/prodavacMojePorudzbine', element: <ProdavacMojePorudzbine></ProdavacMojePorudzbine>},
    {path: '/prodavacNovePorudzbine/PrikazPorudzbine/:id', element: <PrikazPorudzbine></PrikazPorudzbine>},
    {path: '/adminDashboard', element: <AdminDashboard></AdminDashboard>},
    {path: '/adminVerifikacija', element: <AdminVerifikacija></AdminVerifikacija>},
    {path: '/adminSvePorudzbine', element: <AdminSvePorudzbine></AdminSvePorudzbine>},
    {path: '/profil', element: <Profil></Profil>}
  ]

  return (
    <div className='App'>
      <NavBar isAuth={isAuth} tipKorisnika = {tipKorisnika} statusVerifikacije={statusVerifikacije}/>
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
