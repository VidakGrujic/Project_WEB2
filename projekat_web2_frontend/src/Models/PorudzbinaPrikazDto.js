export default class PorudzbinaPrikazDto {
    constructor(data){
        this.id = data.id;
        this.adresa = data.adresa;
        this.komentar = data.komentar;
        this.datumKreiranja = data.datumKreiranja;
        this.datumDostave = data.datumDostave;
        this.cenaPorudzbine = data.cenaPorudzbine;
        this.imenaArtikala = [];
    }

    addAllImenaArtikala(imenaArtikala){
        for(var i = 0; i < imenaArtikala.length; i++){
            this.imenaArtikala.push(imenaArtikala[i]);
        }
    }
}