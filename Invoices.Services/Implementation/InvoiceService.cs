using Invoices.Data.Infrastructure.Interfaces;
using Invoices.Data.Repositories.Interfaces;
using Invoices.Models;
using Invoices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Invoices.Services.Implementation
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository invoicesRepository;
        private readonly IClientRepository clientRepository;
        private readonly IUnitOfWork unitOfWork;

        public InvoiceService(IInvoiceRepository invoicesRepository, IClientRepository clientRepository, IUnitOfWork unitOfWork)
        {
            this.invoicesRepository = invoicesRepository;
            this.clientRepository = clientRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateInvoice(Invoice invoice)
        {
            invoicesRepository.Add(invoice);
        }

        public IEnumerable<Invoice> GetClientInvoices(string companyName, string invoiceNumber = null)
        {
            var client = clientRepository.GetClientByCompanyName(companyName);
            var invoices = client.Invoices.Where(i => i.InvoiceNumber.ToUpper().Contains(invoiceNumber.ToUpper().Trim()));

            return invoices;
        }

        public Invoice GetInvoice(int invoiceId)
        {
            var invoice = invoicesRepository.GetById(invoiceId);
            return invoice;
        }

        public IEnumerable<Invoice> GetInvoices()
        {
            var invoices = invoicesRepository.GetAll();
            return invoices;
        }

        public void SaveInvoice()
        {
            unitOfWork.Commit();
        }
    }
}
