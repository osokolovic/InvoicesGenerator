using System;
using System.Collections.Generic;
using System.Text;

namespace Invoices.Data.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
