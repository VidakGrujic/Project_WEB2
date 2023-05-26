import React from "react";

const ProdavacDashboard = ({statusVerifikacije}) => {
    return (
        <>
            {statusVerifikacije === 'Procesuira_se' && 
                <>
                    <h1 className="ui yellow center aligned icon header">
                        <i className="hourglass half icon"></i>
                        Vas zahtev se jos uvek procesuira
                        <div className="sub header">
                            Ako imate neka pitanja, obratite se nasem centru
                        </div>
                    </h1>
                </>
            }
            {statusVerifikacije === 'Odbijen' &&
                <>
                   <h1 className="ui red center aligned icon header">
                        <i className="x icon"></i>
                        Vas zahtev je nazalost odbijen
                        <div className="sub header">
                            Ako imate neka pitanja, obratite se nasem centru
                        </div>
                    </h1> 
                </>
            }
            {statusVerifikacije === 'Prihvacen' && 
                <>
                <h1 className="ui green center aligned icon header">
                        <i className="check icon"></i>
                        Vas zahtev je prihvacen
                        <div className="sub header">
                            Za nastavak rada sajta, koristite navigacioni meni
                        </div>
                    </h1> 
                </>
            }

            
        </>
        
    );
}

export default ProdavacDashboard;