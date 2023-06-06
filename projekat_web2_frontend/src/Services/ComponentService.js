import axios from "../api/axios";

export const GetKupcevePorudzbine = async(kupacId, token) => {
    //[HttpGet("getKupcevePorudzbine/{id}")]
    try{
        const {data} = await axios.get(`${process.env.REACT_APP_API_BACK}/orders/getKupcevePorudzbine/${kupacId}`,
        {
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${token}` 
            },
            withCredential: true
        });
        return data;
    } catch(err){
        console.log(err);
        alert("Nesto se desilo prilikom dobavljanja kupcevih porudzbina")
        return [];
    }
}

export const GetPorudzbinaById = async (id, token) => {
    try{
        const {data} = await axios.get(
            `${process.env.REACT_APP_API_BACK}/orders/${id}`,
            {
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`
                }, 
                withCredentials: true
            }
        );
        return data;
    }catch(err){
        console.log(err);
        alert("Nesto se desilo prilikom dobavljanja informacija o porudzbini")
        return err;
    }
}

//u slucaju greske postavi da se vraca null


export const OtkaziPorudzbinu = async (id, token) => {
    try{
        const {data} = await axios.put(
            `${process.env.REACT_APP_API_BACK}/orders/otkaziPorudzbinu/${id}`,
            'Otkazana',
            {
                headers: {
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}`
                }, 
                withCredentials: true
            }
        );
        return data;
    }catch(err){
        console.log(err);
        alert('Neesto se desilo prilikom otkazivanja porudzbine');
        return null;
    }
}





