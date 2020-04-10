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
        void UpdateInvoice(Invoice invoice);
        void DeleteInvoice(Invoice invoice);
        IEnumerable<Invoice> SortInvoicesByParam(IEnumerable<Invoice> invoices, string sortOrder);
        IEnumerable<Invoice> FilterByCompanyName(IEnumerable<Invoice> invoices, string companyName);
        IEnumerable<InvoiceChart> GetInvoiceChart(int year, int month);
    }
}
