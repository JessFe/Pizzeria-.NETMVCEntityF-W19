namespace PizzeriaInForno.Models
{
    // Questa classe rappresenta un singolo articolo nel carrello.
    // Ogni istanza contiene informazioni sull'ID del prodotto, la quantità selezionata, il nome e il prezzo del prodotto.
    public class DettaglioCarrello
    {
        public int IDProdotto { get; set; }
        public int Quantita { get; set; }
        public string NomeProd { get; set; }
        public decimal Prezzo { get; set; }
    }
}