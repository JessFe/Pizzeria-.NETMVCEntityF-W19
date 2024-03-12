namespace PizzeriaInForno.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prodotti")]
    public partial class Prodotti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Prodotti()
        {
            DettagliOrdini = new HashSet<DettagliOrdini>();
        }

        [Key]
        public int IDProdotto { get; set; }

        [Required]
        [StringLength(100)]
        public string NomeProd { get; set; }

        public string Foto { get; set; }

        public decimal Prezzo { get; set; }

        public int ConsMin { get; set; }

        public string Ingredienti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettagliOrdini> DettagliOrdini { get; set; }
    }
}
