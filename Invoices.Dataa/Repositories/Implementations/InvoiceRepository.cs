using Invoices.Data.Infrastructure.Implementations;
using Invoices.Data.Infrastructure.Interfaces;
using Invoices.Data.Repositories.Interfaces;
using Invoices.Models;
using System.Collections.Generic;
using System.Linq;

namespace Invoices.Data.Repositories.Implementations
{
    public class InvoiceRepository : RepositoryBase<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Invoice> GetInvoiceByClientId(int clientId)
        {
            return this.DbContext.Invoices.Where(i => i.ClientId == clientId).ToList();
        }

        public Invoice GetInvoiceByInvoiceNumber(string invoiceNumber)
        {
            return this.DbContext.Invoices.Where(i => i.InvoiceNumber == invoiceNumber).FirstOrDefault();
        }
    }
}
