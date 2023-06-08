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
        return null;
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
        return null;
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


export const GetNoveProdavcevePorudzbine = async (id, token) => {
    try{
        const {data} = await axios.get(
            `${process.env.REACT_APP_API_BACK}/orders/getProdavceveNovePorudzbine/${id}`,
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
        alert("Nesto se desilo prilikom dobavljanja informacija novim prodavcevim porudzbinama")
        return null;
    }
}

export const GetProdavcevePrethodnePorudzbine = async (id, token) => {
    try{
        const {data} = await axios.get(
            `${process.env.REACT_APP_API_BACK}/orders/getProdavcevePrethodnePorudzbine/${id}`,
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
        alert("Nesto se desilo prilikom dobavljanja informacija prethodnim prodavcevim porudzbinama")
        return null;
    }
}

export const GetSvePorudzbine = async (token) => {
    try{
        const {data} = await axios.get(
            `${process.env.REACT_APP_API_BACK}/orders`,
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
        alert("Nesto se desilo prilikom dobavljanja informacija o svim porudybinama")
        return null;
    }
}




export const AddPorudzbina = async(porudzbinaDto, token) =>{
    const ADD_PORUDZBINA_URL = "/orders/addPorudzbina";
    try{
        const {data} = await axios.post(`${process.env.REACT_APP_API_BACK}${ADD_PORUDZBINA_URL}`,
            porudzbinaDto, 
            {
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${token}`,
            },
            withCredentials: true,
            }
        );
        return data;
    } catch(err){
        console.log(err);
         alert("Nesto se desilo prilikom pravljenja porudzbine");
         return null;
    }    
}



