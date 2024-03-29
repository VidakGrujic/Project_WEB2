import React, {useEffect, useState} from "react";
import CountdownTimer from "../Other Components/CountdownTimer";
import { GetKupcevePorudzbine, OtkaziPorudzbinu } from "../../Services/PorudzbinaService";
import { useNavigate } from "react-router-dom";

const KupacPorudzbine = () => {
    
    const [kupcevePorudzbine, setKupcevePorudzbine] = useState([]);
    const [loading, setLoading] = useState(true);

    const navigate = useNavigate();

    useEffect(() => {
        const getPorudzbine = async () =>{
            const kupac = JSON.parse(sessionStorage.getItem('korisnik'));
            const kupacId = kupac.id;
            const token = sessionStorage.getItem('token'); 
            const response = await GetKupcevePorudzbine(kupacId, token);   
        
            if(response !== null){
                setKupcevePorudzbine(response);
                console.log(response)
                setLoading(false);
            }         
        } 
        getPorudzbine();
    }, [])


  
    const handleButtonCell = (datumKreiranja, id) => {
        //pronadjem da li je od ovog trenutka proslo vise od sat vremena
        var fiveMinutes = 5*60*1000;
        var datumKreiranjaObject = new Date(datumKreiranja);
        var currentTime = new Date();
        var difference = currentTime.getTime() - datumKreiranjaObject.getTime()  
    
        if( difference < fiveMinutes){
            return <td className="center aligned">
                        <button className="mini ui red labeled icon button"
                                id={id} 
                                onClick={(e) => handleOtkaziDugmeClick(e)}>
                            <i className="x icon"></i>
                            Otkazite porudzbinu
                </button>
            </td> 
           
        }
        else {
            return <td className="center aligned positive">
                <i className="icon checkmark"></i>
                Porudzbina je privacena
            </td>
        }
    }

    

    const handleOtkaziDugmeClick = async (e) => {
        e.preventDefault();
        const id = e.target.id;

        const token = sessionStorage.getItem('token'); 
        const response = await OtkaziPorudzbinu(id, token)

        if(response !== null){
            alert(response.message);
            navigate('/kupacDashboard');
        }


    }

    return (
    <div className="verification-container">
        {loading && (
        <div className="loader-container">
          <div className="ui active inverted dimmer">
            <div className="ui large text loader">Ucitavanje porudzbina</div>
          </div>
        </div>
        )}
        {!loading && (<table className="ui fixed blue celled table">
            <thead>
                <tr>
                    <th>Id porudzbine</th>
                    <th>Adresa dostave</th>
                    <th>Cena porudzbine</th>
                    <th>Vreme ostalo do isporuke</th>
                    <th>Otkazite porudzbinu</th>
                </tr>
            </thead>
            <tbody>
                {kupcevePorudzbine.map((kupcevaPorudzbina) => (
                   <tr>
                        <td>
                            <h4 className="ui header">
                                <a href={`/kupacPorudzbine/PrikazPorudzbine/${kupcevaPorudzbina.id}`}>Porudzbina br {kupcevaPorudzbina.id}</a>
                            </h4>
                        </td>
                        <td>
                            {kupcevaPorudzbina.adresa}
                        </td>
                        <td className="center aligned"> 
                            {kupcevaPorudzbina.cenaPorudzbine} $
                        </td>
                        <td>
                            <CountdownTimer targetDate={kupcevaPorudzbina.datumDostave}/>
                        </td>
                        {handleButtonCell(kupcevaPorudzbina.datumKreiranja, kupcevaPorudzbina.id)}
                   </tr>
                ))}
            </tbody>
        </table>)}
    </div>
   );
};

export default KupacPorudzbine;
