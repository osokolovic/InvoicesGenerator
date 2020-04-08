using System.Collections.Generic;

namespace Invoices.Models
{
    public class ChargeName
    {
        public int ChargeNameId { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public virtual List<Charge> Charges { get; set; }
    }
}