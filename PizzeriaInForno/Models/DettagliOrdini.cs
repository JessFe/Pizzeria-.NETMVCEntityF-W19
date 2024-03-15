namespace PizzeriaInForno.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DettagliOrdini")]
    public partial class DettagliOrdini
    {
        [Key]
        public int IDDettaglioOrd { get; set; }

        public int? FK_IDOrdine { get; set; }

        public int? FK_IDProdotto { get; set; }

        [Display(Name = "Quantity")]
        public int Quantita { get; set; }

        public virtual Ordini Ordini { get; set; }

        public virtual Prodotti Prodotti { get; set; }
    }
}
