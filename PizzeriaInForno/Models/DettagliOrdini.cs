namespace PizzeriaInForno.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DettagliOrdini")]
    public partial class DettagliOrdini
    {
        [Key]
        public int IDDettaglioOrd { get; set; }

        public int? FK_IDOrdine { get; set; }

        public int? FK_IDProdotto { get; set; }

        public int Quantita { get; set; }

        public virtual Ordini Ordini { get; set; }

        public virtual Prodotti Prodotti { get; set; }
    }
}
