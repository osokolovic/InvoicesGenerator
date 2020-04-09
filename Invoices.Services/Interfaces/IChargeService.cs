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
        Dictionary<string, double> CalculateCharge(int invoiceId);
        Dictionary<string, double> GetRates(List<Charge> charges);
        Dictionary<string, int> GetUnits(List<Charge> charges);
    }
}
