import logo from './logo.svg';
import './App.css';
import NavBar from './Components/NavBar';
import Login from './Components/Login';
import Registration from './Components/Registration';
import Home from './Components/Home';
import { Route, Routes } from 'react-router-dom';
import { useState } from 'react';

function App() {

  const [isLogged, setIsLogged] = useState(false);

  const onLoggedUser = (logged) => {
    setIsLogged(logged)
    console.log(isLogged)
  }

  return (
    <div className='App'>
      <NavBar/>
      <div className='container'>
        <Routes>
          <Route path='/' element={<Home/>}/>
          <Route path="/login" element={<Login/>}/>
          <Route path="/registration" element={<Registration/>}/>
        </Routes>
      </div>
    </div>
  );
}

export default App;
