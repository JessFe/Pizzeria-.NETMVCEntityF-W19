namespace PizzeriaInForno.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Utenti")]
    public partial class Utenti
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Utenti()
        {
            Ordini = new HashSet<Ordini>();
        }

        [Key]
        public int IDUtente { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        // validazione password (per la modifica nell'edit utente)
        // "NotMapped" indica che la proprietà non è mappata a una colonna del database, risolve l'errore in fase login
        //[NotMapped]
        //[DataType(DataType.Password)]
        //[Display(Name = "Conferma password")]
        //[Compare("Password", ErrorMessage = "La password e la conferma password non corrispondono.")]
        //public string ConfirmPassword { get; set; }

        public bool? IsAdmin { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [StringLength(50)]
        public string Cognome { get; set; }

        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Tel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordini> Ordini { get; set; }
    }
}
