using Invoices.Data.Infrastructure.Implementations;
using Invoices.Data.Infrastructure.Interfaces;
using Invoices.Dataa.Repositories.Interfaces;
using Invoices.Models;
using System.Linq;

namespace Invoices.Dataa.Repositories.Implementations
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Client GetClientByCompanyName(string companyName)
        {
            return this.DbContext.Clients.Where(c => c.CompanyName == companyName).FirstOrDefault();
        }
    }
}
