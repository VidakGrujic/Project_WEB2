import React, {useState, useEffect} from "react";
import CountdownTimer from "../Other Components/CountdownTimer";
import { GetNoveProdavcevePorudzbine } from "../../Services/PorudzbinaService";

const ProdavacNovePorudzbine = () => {
    
    const [prodavceveNovePorudzbine, setProdavceveNovePorudzbine] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const getPorudzbine = async () =>{
            const prodavac = JSON.parse(sessionStorage.getItem('korisnik'));
            const prodavacId = prodavac.id;
            const token = sessionStorage.getItem('token'); 
            const response = await GetNoveProdavcevePorudzbine(prodavacId, token);

            if(response !== null){
                setProdavceveNovePorudzbine(response);
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
                    {prodavceveNovePorudzbine.map((prodavcevaNovaPorudzbina) => (
                       <tr>
                            <td>
                                <h4 className="ui header">
                                    <a href={`/prodavacNovePorudzbine/PrikazPorudzbine/${prodavcevaNovaPorudzbina.id}`}>Porudzbina br {prodavcevaNovaPorudzbina.id}</a>
                                </h4>
                            </td>
                            <td>
                                {prodavcevaNovaPorudzbina.adresa}
                            </td>
                            <td className="center aligned"> 
                                {prodavcevaNovaPorudzbina.cenaPorudzbine}
                            </td>
                            <td>
                                <CountdownTimer targetDate={prodavcevaNovaPorudzbina.datumDostave}/>
                            </td>
                       </tr>
                    ))}
                </tbody>
            </table>)}
        </div>
       );
}

export default ProdavacNovePorudzbine;