using Invoices.Data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoices.Data.Infrastructure.Implementations
{
    public class DbFactory : Disposable, IDbFactory
    {
        InvoiceEntities dbContext;

        public InvoiceEntities Init()
        {
            return dbContext ?? (dbContext = new InvoiceEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }


    }
}
