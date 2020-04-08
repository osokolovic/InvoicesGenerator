using Invoices.Models;
using System.Collections.Generic;

namespace Invoices.Services.Interfaces
{
    public interface IChargeService
    {
        IEnumerable<Charge> GetCharges();
        IEnumerable<Charge> GetInvoiceCharges(int invoiceId);
        void CreateCharge(Charge charge);
        void SaveCharge();
    }
}
