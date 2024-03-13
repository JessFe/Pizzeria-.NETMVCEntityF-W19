using System.Collections.Generic;

namespace PizzeriaInForno.Models
{
    // SchedaOrdine è utilizzata per raccogliere tutte le informazioni necessarie per completare un ordine.
    // Include una lista degli articoli selezionati per l'acquisto (DettaglioCarrello), insieme all'indirizzo di spedizione e alle note opzionali dell'ordine.
    public class SchedaOrdine
    {
        public List<DettaglioCarrello> Articoli { get; set; }
        public string Indirizzo { get; set; }
        public string Note { get; set; }
    }
}