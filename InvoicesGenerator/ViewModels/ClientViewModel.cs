using System;

namespace InvoicesGenerator.ViewModels
{
    public class ClientViewModel
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
        public DateTime? ContractDateEnd { get; set; }
        public string Status { get; set; }

    }
}