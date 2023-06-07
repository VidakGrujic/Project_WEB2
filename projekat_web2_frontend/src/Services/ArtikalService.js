import axios from "../api/axios";


export const GetProdavceveArtikle = async(prodavacId, token) => {
    try{
        const {data} = await axios.get(
            `${process.env.REACT_APP_API_BACK}/products/getProdavceveArtikle/${prodavacId}`,
            {
                headers:{
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}` 
                },
                withCredential: true
            }
        );
        return data;
    }catch(err){
        console.log(err);
        alert('Nesto se desilo prilikom dobavljanja artikala');
        return null;
    }
}

export const GetArtikalById = async(artikalId, token) => {
    try{
        const {data} = await axios.get(
            `${process.env.REACT_APP_API_BACK}/products/${artikalId}`,
            {
                headers:{
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}` 
                },
                withCredential: true
            }
        );
        return data;
    }catch(err){
        console.log(err);
        alert('Nesto se desilo prilikom dobavljanja informacija o artiklu');
        return null;
    }
}

export const UpdateArtikal = async(artikalId, artikalDto, token) => {
    try{
        const {data} = await axios.put(
            `${process.env.REACT_APP_API_BACK}/products/${artikalId}`, 
            artikalDto,
            {
                headers:{
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}` 
                },
                withCredential: true
            }
        );
        return data;
    }catch(err){
        console.log(err);
        alert('Nesto se desilo prilikom azuriranja informacija o artiklu');
        return null;
    }
}

export const DeleteArtikal = async(artikalId, token) => {
    try{
        const {data} = axios.delete(
            `${process.env.REACT_APP_API_BACK}/products/${artikalId}`,
            {
                headers:{
                    'Content-Type': 'application/json',
                    Authorization: `Bearer ${token}` 
                },
                withCredential: true
            }
        );
        return data;
    }catch(err){
        console.log(err);
        alert("Nesto se desilo prilikom brisanja artikla");
        return null;
    }
}


