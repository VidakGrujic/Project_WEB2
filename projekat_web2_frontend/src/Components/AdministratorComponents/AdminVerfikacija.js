import React, { useEffect, useState } from "react";
import { GetAllProdavce, VerifyProdavca } from "../../Services/KorisnikService";

const AdminVerifikacija = () => {
    
    
    
    const [prodavci, setProdavci] = useState([]);
    const [loading, setLoading] = useState(true);
    const token = sessionStorage.getItem('token');


    useEffect(() => {
        const getProdavce = async () => {
            setLoading(true);

            const data = await GetAllProdavce(token);
            
            if(data !== null){
                setProdavci(data);
                setLoading(false);
            }
        }
        getProdavce();
    }, []);

    
 
    const handlePrikazDugmadi = (prodavac) => {
        if(prodavac.statusVerifikacije === 'Procesuira_se'){
            return <td className="center aligned">
                        <button className="ui green labeled icon button" value={'Prihvacen'} id={prodavac.id} onClick={(e) => handleClick(e)}>
                            <i className="check icon"></i>
                            Prihvati 
                        </button>
                        <button className="ui red labeled icon button" value={'Odbijen'} id={prodavac.id} onClick={(e) => handleClick(e)}>
                            <i className="x icon"></i>
                            Odbij
                        </button>                         
                    </td> 
        }
        else if (prodavac.statusVerifikacije === 'Prihvacen'){
            return  <td className="positive center aligned">
                        <i className="icon checkmark"></i>
                        Prihvacen
                    </td>
        }
    }


    const handleClick = async (e) => {
      
        const prodavacId = Number(e.target.id);
        const buttonType = e.target.value; //da li je kliknuto dugme sa value 'prihvati' ili 'odbij'
    
        const data = await VerifyProdavca(prodavacId, buttonType, token);
        if(data !== null){
            setProdavci(data);
            alert(`Uspesno ste ${buttonType} prodavca.`)
        }
    }

    return (
        <div className="verification-container">
            {loading && 
            <div className="loader-container">
                <div className="ui active inverted dimmer">
                    <div className="ui large text loader">
                        Ucitavanje prodavaca
                    </div>
                </div>
            </div>
                
            }
            {!loading && (  
                <div>
                    <h2>Prikaz prodavaca</h2>
                    <table className="ui blue celled table">
                        <thead>
                            <tr>
                                <th>Prodavac</th>
                                <th>Status verifikacije</th>
                                <th>Akcija</th>
                            </tr>
                        </thead>
                        <tbody>
                            {prodavci.map((prodavac) => (
                                <tr>
                                    <td>
                                        <h4 className="ui image header">
                                            <img className="ui mini rounded image" src={prodavac.slika}></img>
                                            <div className="content">
                                                {prodavac.korisnickoIme}
                                                <div className="sub header">{prodavac.ime} {prodavac.prezime}</div>
                                                <div className="sub header">{prodavac.email}</div>
                                            </div>
                                        </h4> 
                                    </td>
                                    <td className="center aligned">
                                        {prodavac.statusVerifikacije}
                                    </td>
                                    {handlePrikazDugmadi(prodavac)}
                                </tr>
                            ))}
                        </tbody>
                    </table>  
                </div> 
            )}
        </div>
    )   
}

export default AdminVerifikacija;