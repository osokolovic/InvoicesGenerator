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

        public IEnumerable<InvoiceChart> GetInvoiceChart(int Year, int Month)
        {
            using (var context = new InvoicesDBEntities()) {
                var chart = context.sp_GetMonthTotalPerChargeName(Year, Month).ToList();

                var result = new List<InvoiceChart>();
                foreach (var item in chart)
                {
                    result.Add(new InvoiceChart
                    {
                        Name = item.Name,
                        Total = item.Total ?? 0
                    }
                    );
                }

                return result;
            }
        }
    }
}
