using System;
using System.Collections.Generic;

namespace Invoices.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CompanyName { get; set; }
        
        public int ClientId { get; set; }
        public Client Client { get; set; }
        
        public virtual List<Charge> Charges { get; set; }
    }
}