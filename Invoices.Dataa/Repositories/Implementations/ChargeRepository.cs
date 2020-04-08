using Invoices.Data.Infrastructure.Implementations;
using Invoices.Data.Infrastructure.Interfaces;
using Invoices.Data.Repositories.Interfaces;
using Invoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Invoices.Data.Repositories.Implementations
{
    public class ChargeRepository : RepositoryBase<Charge>, IChargeRepository
    {
        public ChargeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Charge> GetByInvoiceId(int invoiceId)
        {
            return this.DbContext.Charges.Where(c => c.InvoiceId == invoiceId).ToList();
        }
    }
}
