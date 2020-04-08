using Invoices.Data.Infrastructure.Implementations;
using Invoices.Data.Infrastructure.Interfaces;
using Invoices.Dataa.Repositories.Interfaces;
using Invoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Dataa.Repositories.Implementations
{
    public class InvoiceRepository : RepositoryBase<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Invoice GetInvoiceByInvoiceNumber(string invoiceNumber)
        {
            return this.DbContext.Invoices.Where(i => i.InvoiceNumber == invoiceNumber).FirstOrDefault();
        }
    }
}
