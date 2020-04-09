using Invoices.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace InvoicesGenerator.ViewModels
{
    public class ClientFormViewModel
    {
        public int ClientId { get; set; }
        [Required]
        [MinLength(3)]
        public string CompanyName { get; set; }
        [Required]
        [MinLength(3)]
        public string City { get; set; }
        [Required]
        [MinLength(3)]
        public string Address { get; set; }
        [Required]
        public string Contract { get; set; }
        [RegularExpression(@"[0-9]*", ErrorMessage = "Phone number is not valid.")]
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public DateTime? ContractDateStart { get; set; }
        public DateTime? ContractDateEnd { get; set; }
        [Required]
        public string Status { get; set; }

        public static implicit operator ClientFormViewModel(Client client)
        {
            return new ClientFormViewModel
            {
                ClientId = client.ClientId,
                CompanyName = client.CompanyName,
                City = client.City,
                Address = client.Address,
                Contract = client.Contract,
                Phone = client.Phone,
                Email = client.Email,
                ContactPerson = client.ContactPerson,
                ContractDateStart = client.ContractDateStart,
                ContractDateEnd = client.ContractDateEnd,
                Status = client.Status
            };
        }

        public static implicit operator Client(ClientFormViewModel vm)
        {
            return new Client
            {
                ClientId = vm.ClientId,
                CompanyName = vm.CompanyName,
                City = vm.City,
                Address = vm.Address,
                Contract = vm.Contract,
                Phone = vm.Phone,
                Email = vm.Email,
                ContactPerson = vm.ContactPerson,
                ContractDateStart = vm.ContractDateStart,
                ContractDateEnd = vm.ContractDateEnd,
                Status = vm.Status
            };
        }

    }
}