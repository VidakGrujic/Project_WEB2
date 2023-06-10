import axios from "../api/axios";
import ArtikalDto from '../Models/ArtikalDto';

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
        const prodavceviArtikli = data.map(artikal => {
            return new ArtikalDto(artikal);
        })
        return prodavceviArtikli;
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
        const artikal = new ArtikalDto(data);
        return artikal;
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
        const updateArtikal = new ArtikalDto(data);
        return updateArtikal;
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

export const GetArtikle = async(token) => {
    const GET_ARTIKLE_URL = "/products";
    try{
        const { data } = await axios.get(
            `${process.env.REACT_APP_API_BACK}${GET_ARTIKLE_URL}`,
                {
                    headers: {
                    "Content-Type": "application/json",
                    Authorization : `Bearer ${token}`
                    },
                }
            );
        const artikli = data.map(artikal => {
            return new ArtikalDto(artikal);
        })   
        return artikli;
    }catch(err){
        alert("Nesto se desilo prilikom dobavljanja artikala");
        return null;
    }
}


export const AddArtikal = async(artikalDto, token) => {
    const ADD_ARTIKAL_URL = '/products/addArtikal';
    try{
        const {data} = await axios.post(`${process.env.REACT_APP_API_BACK}${ADD_ARTIKAL_URL}`,
            artikalDto,
            {
                headers:{
                    'Content-Type' : 'application/json',
                    Authorization : `Bearer ${token}`
                },
                withCredentials: true
            }
        );
        const newArtikal = new ArtikalDto(data);
        return newArtikal;
    }catch(err){
        alert("Nesto se desilo prilikom dodavanja artikla")
        return null;
    }
}