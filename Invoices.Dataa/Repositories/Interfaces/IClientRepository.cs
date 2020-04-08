using Invoices.Data.Infrastructure.Interfaces;
using Invoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Data.Repositories.Interfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Client GetClientByCompanyName(string companyName);
    }
}
