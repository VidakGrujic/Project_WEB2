import PorudzbinaDto from "./PorudzbinaDto";

export default class ResponsePorudzbinaDto{
    constructor(data){
        this.message = data.message;
        this.porudzbinaDto = new PorudzbinaDto(data.porudzbinaDto);
    }
}