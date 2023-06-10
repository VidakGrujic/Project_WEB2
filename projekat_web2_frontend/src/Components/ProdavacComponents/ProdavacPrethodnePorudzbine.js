import React, {useState, useEffect} from "react";
import CountdownTimer from "../Other Components/CountdownTimer";
import { GetProdavcevePrethodnePorudzbine } from "../../Services/PorudzbinaService";

const ProdavacPrethodnePorudzbine = () => {
    const [prodavceveMojePorudzbine, setProdavceveMojePorudzbine] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const getPorudzbine = async () =>{
            const prodavac = JSON.parse(sessionStorage.getItem('korisnik'));
            const prodavacId = prodavac.id;
            const token = sessionStorage.getItem('token'); 
            const response = await GetProdavcevePrethodnePorudzbine(prodavacId, token);

            if(response !== null){
                setProdavceveMojePorudzbine(response);
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
                    {prodavceveMojePorudzbine.map((prodavcevaMojaPorudzbina) => (
                       <tr>
                            <td>
                                <h4 className="ui header">
                                    <a href={`/prodavacPrethodnePorudzbine/PrikazPorudzbine/${prodavcevaMojaPorudzbina.id}`}>Porudzbina br {prodavcevaMojaPorudzbina.id}</a>
                                </h4>
                            </td>
                            <td>
                                {prodavcevaMojaPorudzbina.adresa}
                            </td>
                            <td className="center aligned"> 
                                {prodavcevaMojaPorudzbina.cenaPorudzbine} $
                            </td>
                            <td>
                                <CountdownTimer targetDate={prodavcevaMojaPorudzbina.datumDostave}/>
                            </td>
                       </tr>
                    ))}
                </tbody>
            </table>)}
        </div>
       );
}

export default ProdavacPrethodnePorudzbine;