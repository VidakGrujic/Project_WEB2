export default class ArtikalDto{
    constructor(data){
        this.id = data.id;
        this.naziv = data.naziv;
        this.cena = data.cena;
        this.cenaDostave = data.cenaDostave;
        this.kolicina = data.kolicina;
        this.opis = data.opis;
        this.fotografija = data.fotografija;
        this.prodavacId = data.prodavacId;
    }

}