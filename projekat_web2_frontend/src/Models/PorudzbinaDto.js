import ArtikalPorudzbineDto from '../Models/ArtikalPorudzbineDto';

export default class PorudzbinaDto{
    constructor(data){
        this.id = data.id;
        this.komentar = data.komentar;
        this.adresa = data.adresa;
        this.datumDostave = data.datumDostave;
        this.datumKreiranja = data.datumKreiranja;
        this.stanjePorudzbine = data.stanjePorudzbine;
        this.korisnikId = data.korisnikId;
        this.artikliPorudzbine = [];
        this.cenaPorudzbine = data.cenaPorudzbine;
    }


    addAllArtiklePorudzbine(artikliPorudzbine){
        for(var i = 0; i < artikliPorudzbine.length; i++){
            this.artikliPorudzbine.push(artikliPorudzbine[i]);
        }
    }
}