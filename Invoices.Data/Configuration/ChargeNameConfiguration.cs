using Invoices.Models;
using System.Data.Entity.ModelConfiguration;

namespace Invoices.Data.Configuration
{
    public class ChargeNameConfiguration: EntityTypeConfiguration<ChargeName>
    {
        public ChargeNameConfiguration()
        {
            ToTable("Name");
            Property(cn => cn.Name).IsRequired();
        }
    }
}
