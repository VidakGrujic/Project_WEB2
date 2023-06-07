import React,{useState, useEffect} from "react";
import { GetSvePorudzbine } from "../../Services/ComponentService";
import CountdownTimer from "../Other Components/CountdownTimer";

const AdminSvePorudzbine = () => {
    const [adminovePorudzbine, setAdminovePorudzbine] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const getPorudzbine = async () =>{
            
            const token = sessionStorage.getItem('token'); 
            const response = await GetSvePorudzbine(token);

            if(response !== null){
                setAdminovePorudzbine(response);
                console.log(response)
                setLoading(false);
            }
          
        } 
        getPorudzbine();
    }, [])

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
                    </tr>
                </thead>
                <tbody>
                    {adminovePorudzbine.map((adminovaPorudzbina) => (
                       <tr>
                            <td>
                                <h4 className="ui header">
                                    <a href={`/prodavacNovePorudzbine/PrikazPorudzbine/${adminovaPorudzbina.id}`}>Porudzbina br {adminovaPorudzbina.id}</a>
                                </h4>
                            </td>
                            <td>
                                {adminovaPorudzbina.adresa}
                            </td>
                            <td className="center aligned"> 
                                {adminovaPorudzbina.cenaPorudzbine}
                            </td>
                            <td>
                                <CountdownTimer targetDate={adminovaPorudzbina.datumDostave}/>
                            </td>
                       </tr>
                    ))}
                </tbody>
            </table>)}
        </div>
       );
}


export default AdminSvePorudzbine;
