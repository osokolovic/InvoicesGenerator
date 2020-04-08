using Invoices.Models;
using System.Data.Entity.ModelConfiguration;

namespace Invoices.Data.Configuration
{
    public class InvoiceConfiguration: EntityTypeConfiguration<Invoice>
    {
        public InvoiceConfiguration()
        {
            ToTable("Invoice");
        }
    }
}
