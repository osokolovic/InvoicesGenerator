using System;

namespace Invoices.Models
{
    public class Charge
    {
        public int ChargeId { get; set; }
        public double Rate { get; set; }
        public int Units { get; set; }
        public double Amount { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
        
        public int ChargeNameId { get; set; }
        public ChargeName ChargeName { get; set; }
        
        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}