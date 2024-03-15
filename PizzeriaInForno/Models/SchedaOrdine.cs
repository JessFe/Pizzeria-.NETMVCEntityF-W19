using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PizzeriaInForno.Models
{
    // SchedaOrdine è utilizzata per raccogliere tutte le informazioni necessarie per completare un ordine.
    // Include una lista degli articoli selezionati per l'acquisto (DettaglioCarrello), insieme all'indirizzo di spedizione e alle note opzionali dell'ordine.
    public class SchedaOrdine
    {
        public List<DettaglioCarrello> Articoli { get; set; }

        [Display(Name = "Address")]
        public string Indirizzo { get; set; }

        [Display(Name = "Notes")]
        public string Note { get; set; }
    }
}