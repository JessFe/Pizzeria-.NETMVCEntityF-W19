using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PizzeriaInForno.Models
{
    public partial class ModelDbContext : DbContext
    {
        public ModelDbContext()
            : base("name=ModelDbContext1")
        {
        }

        public virtual DbSet<DettagliOrdini> DettagliOrdini { get; set; }
        public virtual DbSet<Ordini> Ordini { get; set; }
        public virtual DbSet<Prodotti> Prodotti { get; set; }
        public virtual DbSet<Utenti> Utenti { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ordini>()
                .HasMany(e => e.DettagliOrdini)
                .WithOptional(e => e.Ordini)
                .HasForeignKey(e => e.FK_IDOrdine);

            modelBuilder.Entity<Prodotti>()
                .Property(e => e.Prezzo)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Prodotti>()
                .HasMany(e => e.DettagliOrdini)
                .WithOptional(e => e.Prodotti)
                .HasForeignKey(e => e.FK_IDProdotto);

            modelBuilder.Entity<Utenti>()
                .HasMany(e => e.Ordini)
                .WithOptional(e => e.Utenti)
                .HasForeignKey(e => e.FK_IDUtente);
        }
    }
}
