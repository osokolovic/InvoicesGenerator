using Invoices.Data.Infrastructure.Implementations;
using Invoices.Data.Infrastructure.Interfaces;
using Invoices.Data.Repositories.Interfaces;
using Invoices.Models;
using System;
using System.Linq;

namespace Invoices.Data.Repositories.Implementations
{
    public class ChargeNameRepository : RepositoryBase<ChargeName>, IChargeNameRepository
    {
        public ChargeNameRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public ChargeName GetChargeByName(string name)
        {
            return this.DbContext.ChargeNames.Where(cn => cn.Name == name).FirstOrDefault();
        }
    }
}
