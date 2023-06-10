export default class PorudzbinaPrikazDto {
    constructor(data){
        this.id = data.id;
        this.adresa = data.adresa;
        this.komentar = data.komentar;
        this.datumKreiranja = data.datumKreiranja;
        this.datumDostave = data.datumDostave;
        this.cenaPorudzbine = data.cenaPorudzbine;
        this.imenaArtikala = data.imenaArtikala.map(imeArtikla => {
            return imeArtikla
        });
    }
}