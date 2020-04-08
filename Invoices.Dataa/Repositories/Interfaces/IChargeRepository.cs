using Invoices.Data.Infrastructure.Interfaces;
using Invoices.Models;
using System.Collections.Generic;

namespace Invoices.Data.Repositories.Interfaces
{
    public interface IChargeRepository : IRepository<Charge>
    {
        IEnumerable<Charge> GetByInvoiceId(int invoiceId);
    }
}
