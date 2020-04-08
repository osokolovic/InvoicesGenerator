using Invoices.Models;
using System.Data.Entity.ModelConfiguration;

namespace Invoices.Data.Configuration
{
    public class ChargeConfiguration: EntityTypeConfiguration<Charge>
    {
        public ChargeConfiguration()
        {
            ToTable("Charge");
        }
    }
}
