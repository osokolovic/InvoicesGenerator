using Invoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Dataa.Repositories.Interfaces
{
    interface IClientRepository
    {
        Client GetClientByCompanyName(string companyName);
    }
}
