using Invoices.Data.Infrastructure.Implementations;
using Invoices.Data.Infrastructure.Interfaces;
using Invoices.Dataa.Repositories.Interfaces;
using Invoices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Dataa.Repositories.Implementations
{
    public class ChargeRepository : RepositoryBase<Charge>, IChargeRepository
    {
        public ChargeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
