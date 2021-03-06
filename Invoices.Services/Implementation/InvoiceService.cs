﻿using Invoices.Data.Infrastructure.Interfaces;
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

        public IEnumerable<Invoice> GetClientInvoices(int clientId)
        {
            var invoices = invoicesRepository.GetInvoiceByClientId(clientId);

            return invoices;
        }

        public void SaveInvoice()
        {
            unitOfWork.Commit();
        }

        public void DeleteInvoice(Invoice invoice)
        {
            var dbInvoice = invoicesRepository.GetById(invoice.InvoiceId);

            if (dbInvoice != null)
            {
                invoicesRepository.Delete(dbInvoice);
            }
        }

        public void UpdateInvoice(Invoice invoice)
        {
            invoicesRepository.Update(invoice);
        }

        public IEnumerable<Invoice> SortInvoicesByParam(IEnumerable<Invoice> invoices, string sortOrder)
        {
            IEnumerable<Invoice> sorted;
            switch (sortOrder)
            {
                case "invoiceNumber":
                    sorted = invoices.OrderBy(o => o.InvoiceNumber);
                    break;
                case "companyName":
                    sorted = invoices.OrderBy(o => o.CompanyName);
                    break;
                case "invoiceDate":
                    sorted = invoices.OrderBy(o => o.InvoiceDate);
                    break;
                case "totalCharge":
                    sorted = invoices.OrderBy(o => o.Charges.First().Total);
                    break;
                default:
                    sorted = invoices.OrderBy(o => o.CompanyName);
                    break;
            }

            return sorted;
        }

        public IEnumerable<Invoice> FilterByCompanyName(IEnumerable<Invoice> invoices, string companyName)
        {
            if (String.IsNullOrEmpty(companyName) == false)
            {
                invoices = invoices.Where(i => i.CompanyName.ToUpper().Contains(companyName.ToUpper()));
            }

            return invoices;
        }

        public IEnumerable<InvoiceChart> GetInvoiceChart(int year, int month)
        {
            return invoicesRepository.GetInvoiceChart(year, month);
        }
    }
}
