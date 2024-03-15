namespace PizzeriaInForno.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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

        [Display(Name = "Pizza")]
        [Required]
        [StringLength(100)]
        public string NomeProd { get; set; }

        [Display(Name = "Pic")]
        public string Foto { get; set; }

        [Display(Name = "Price")]
        public decimal Prezzo { get; set; }

        [Display(Name = "Time")]
        public int ConsMin { get; set; }

        [Display(Name = "Ingredients")]
        public string Ingredienti { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettagliOrdini> DettagliOrdini { get; set; }
    }
}
