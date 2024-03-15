using System.ComponentModel.DataAnnotations;

namespace PizzeriaInForno.Models
{
    // Questa classe rappresenta un singolo articolo nel carrello.
    // Ogni istanza contiene informazioni sull'ID del prodotto, la quantità selezionata, il nome e il prezzo del prodotto.
    public class DettaglioCarrello
    {
        public int IDProdotto { get; set; }

        [Display(Name = "Quantity")]
        public int Quantita { get; set; }
        [Display(Name = "Pizza")]
        public string NomeProd { get; set; }
        [Display(Name = "Price")]
        public decimal Prezzo { get; set; }
    }
}