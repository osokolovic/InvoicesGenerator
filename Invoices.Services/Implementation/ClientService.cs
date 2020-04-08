using Invoices.Data.Infrastructure.Interfaces;
using Invoices.Data.Repositories.Interfaces;
using Invoices.Models;
using Invoices.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Invoices.Services.Implementation
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository clientRepository;
        private readonly IUnitOfWork unitOfWork;

        public ClientService(IClientRepository clientRepository, IUnitOfWork unitOfWork)
        {
            this.clientRepository = clientRepository;
            this.unitOfWork = unitOfWork;
        }

        public void CreateClient(Client client)
        {
            clientRepository.Add(client);
        }

        public Client GetClient(int clientId)
        {
            var client = clientRepository.GetById(clientId);
            return client;
        }

        public Client GetClient(string companyName)
        {
            var client = clientRepository.GetClientByCompanyName(companyName);
            return client;
        }

        public IEnumerable<Client> GetClients(string companyName = null)
        {
            var clients = clientRepository.GetAll();

            if (string.IsNullOrEmpty(companyName) == false)
                clients = clients.Where(c => c.CompanyName == companyName);

            return clients;

        }

        public void SaveClient()
        {
            unitOfWork.Commit();
        }
    }
}
