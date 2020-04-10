using Invoices.Models;
using System.Collections.Generic;

namespace Invoices.Services.Interfaces
{
    public interface IInvoiceService
    {
        IEnumerable<Invoice> GetInvoices();
        IEnumerable<Invoice> GetClientInvoices(string companyName, string invoiceNumber = null);
        Invoice GetInvoice(int invoiceId);
        void CreateInvoice(Invoice invoice);
        IEnumerable<Invoice> GetClientInvoices(int clientId);
        void SaveInvoice();
        void DeleteInvoice(Invoice invoice);
    }
}
