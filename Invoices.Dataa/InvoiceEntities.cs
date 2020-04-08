using Invoices.Data.Configuration;
using Invoices.Models;
using System.Data.Entity;

namespace Invoices.Data
{
    public class InvoiceEntities : DbContext
    {
        public InvoiceEntities() : base("name=InvoicesDB") 
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Charge> Charges { get; set; }
        public DbSet<ChargeName> ChargeNames { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClientConfiguration());
            modelBuilder.Configurations.Add(new InvoiceConfiguration());
            modelBuilder.Configurations.Add(new ChargeConfiguration());
            modelBuilder.Configurations.Add(new ChargeNameConfiguration());

            modelBuilder.Entity<Invoice>()
            .HasIndex(i => new { i.CompanyName, i.InvoiceNumber })
            .IsUnique(true);
        }
    }
}
