using System;
using System.Collections.Generic;
using System.Text;

namespace Invoices.Data.Infrastructure.Interfaces
{
    public interface IDbFactory: IDisposable
    {
        InvoiceEntities Init();
    }
}
