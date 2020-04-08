using System;
using System.Collections.Generic;

namespace Invoices.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Contract { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public DateTime? ContractDateStart { get; set; }
        public DateTime? ContractDateEnd{ get; set; }
        public string Status { get; set; }
        public bool Deleted { get; set; }
        
        public virtual List<Invoice> Invoices { get; set; }
    }
}