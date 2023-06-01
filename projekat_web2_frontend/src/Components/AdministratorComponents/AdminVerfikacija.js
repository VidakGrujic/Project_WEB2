import React, { useEffect, useState } from "react";
import axios from "../../api/axios";

const AdminVerifikacija = () => {
    
    const GET_PRODAVCE_URL = '/users/getProdavce'
    const VERIFY_PRODAVCA = '/users/verifyProdavca/';
    const [prodavci, setProdavci] = useState([]);
    const [loading, setLoading] = useState(false);
    const token = sessionStorage.getItem('token');


    useEffect(() => {
        const getProdavce = async () => {
            setLoading(true);
            try{
                const {data} = await axios.get(
                    `${process.env.REACT_APP_API_BACK}${GET_PRODAVCE_URL}`,
                    {
                        headers:{
                            'Content-Type' : 'application/json',
                            Authorization : `Bearer ${token}`
                        },
                    }
                );

                setProdavci(data);
                setLoading(false);
            }
            catch(err){
                alert('Nesto se desilo prilikom dobavljanja prodavaca');
                setLoading(true);
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
        
        //sada treba na backu napraviti funkciju verifikacije
        const url = `${process.env.REACT_APP_API_BACK}${VERIFY_PRODAVCA}${prodavacId}`
        try{
            const {data} = await axios.put(
                `${process.env.REACT_APP_API_BACK}${VERIFY_PRODAVCA}${prodavacId}`,
                buttonType,
                {
                    headers:{
                        'Content-Type' : 'application/json',
                        'Authorization' : `Bearer ${token}`
                    },
                    withCredentials: true
                }
            );
            setProdavci(data);
            alert(`Uspesno ste ${buttonType} prodavca.`)

        } catch(err){
            const result = err.response.data;
            alert(result);
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
                                            <img className="ui mini rounded image" src="https://staticg.sportskeeda.com/editor/2022/01/3daff-16432330593294-1920.jpg"></img>
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