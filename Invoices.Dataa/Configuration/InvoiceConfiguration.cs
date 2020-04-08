using Invoices.Models;
using System.Data.Entity.ModelConfiguration;

namespace Invoices.Data.Configuration
{
    public class InvoiceConfiguration: EntityTypeConfiguration<Invoice>
    {
        public InvoiceConfiguration()
        {
            ToTable("Invoice");
            Property(c => c.CompanyName).HasMaxLength(450);
            Property(c => c.InvoiceNumber).HasMaxLength(450);
        }
    }
}
