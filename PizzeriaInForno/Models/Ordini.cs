namespace PizzeriaInForno.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ordini")]
    public partial class Ordini
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ordini()
        {
            DettagliOrdini = new HashSet<DettagliOrdini>();
        }

        [Key]
        public int IDOrdine { get; set; }

        public int? FK_IDUtente { get; set; }

        public DateTime? DataOrdine { get; set; }

        [Required]
        public string Indirizzo { get; set; }

        public string Note { get; set; }

        public bool? Evaso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DettagliOrdini> DettagliOrdini { get; set; }

        public virtual Utenti Utenti { get; set; }
    }
}
