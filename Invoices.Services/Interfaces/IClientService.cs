using Invoices.Models;
using System.Collections.Generic;

namespace Invoices.Services.Interfaces
{
    public interface IClientService
    {
        IEnumerable<Client> GetClients(string companyName = null);
        Client GetClient(int clientId);
        Client GetClient(string companyName);
        void CreateClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(Client client);
        void SaveClient();
        IEnumerable<Client> SortClientsByParam(IEnumerable<Client> clients, string sortOrder);
    }
}
