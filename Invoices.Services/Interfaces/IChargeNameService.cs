using Invoices.Models;
using System.Collections.Generic;

namespace Invoices.Services.Interfaces
{
    public interface IChargeNameService
    {
        IEnumerable<ChargeName> GetChargeNames();
        ChargeName GetChargeName(string name);
        ChargeName GetChargeName(int chargeNameId);

    }
}
