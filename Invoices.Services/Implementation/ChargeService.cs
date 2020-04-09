using Invoices.Data.Infrastructure.Interfaces;
using Invoices.Data.Repositories.Interfaces;
using Invoices.Models;
using Invoices.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Invoices.Services.Implementation
{
    public class ChargeService : IChargeService
    {
        private readonly IChargeRepository chargeRepository;
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IUnitOfWork unitOfWork;

        public ChargeService(IChargeRepository chargeRepository, IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
        {
            this.chargeRepository = chargeRepository;
            this.invoiceRepository = invoiceRepository;
            this.unitOfWork = unitOfWork;
        }

        public Dictionary<string, double> CalculateCharge(int invoiceId)
        {
            var dbCharges = GetInvoiceCharges(invoiceId).ToList();

            var ratePerChargeName = GetRates(dbCharges);
            var unitsPerChargeName = GetUnits(dbCharges);

            var Total = (unitsPerChargeName["Day"] * ratePerChargeName["Day"]) + (unitsPerChargeName["Night"] * ratePerChargeName["Night"]) + (unitsPerChargeName["Weekend"] * ratePerChargeName["Weekend"]);
            var Tax = dbCharges.Select(x => x.Tax).Sum();
            var GrandTotal = Total + Tax;

            return new Dictionary<string, double>()
            {
                { "Total", Total },
                { "Tax", Tax},
                { "GrandTotal", GrandTotal }
            };
        }

        public void CreateCharge(Charge charge)
        {
            chargeRepository.Add(charge);
        }

        public IEnumerable<Charge> GetCharges()
        {
            var charges = chargeRepository.GetAll();
            return charges;
        }

        public IEnumerable<Charge> GetInvoiceCharges(int invoiceId)
        {
            var charges = chargeRepository.GetByInvoiceId(invoiceId);
            return charges;
        }

        public Dictionary<string, double> GetRates(List<Charge> charges)
        {
            var ratePerChargeName = new Dictionary<string, double>
            {
                { "Day", charges.Where(x => x.ChargeNameId == 1).Select(x => x.Rate).FirstOrDefault() },
                { "Night", charges.Where(x => x.ChargeNameId == 2).Select(x => x.Rate).FirstOrDefault() },
                { "Weekend", charges.Where(x => x.ChargeNameId == 3).Select(x => x.Rate).FirstOrDefault() }
            };
            return ratePerChargeName;
        }

        public Dictionary<string, int> GetUnits(List<Charge> charges)
        {
            var unitsPerChargeName = new Dictionary<string, int>
            {
                { "Day", charges.Where(x => x.ChargeNameId == 1).Select(x => x.Units).FirstOrDefault() },
                { "Night", charges.Where(x => x.ChargeNameId == 2).Select(x => x.Units).FirstOrDefault() },
                { "Weekend", charges.Where(x => x.ChargeNameId == 3).Select(x => x.Units).FirstOrDefault() }
            };
            return unitsPerChargeName;
        }

        public void SaveCharge()
        {
            unitOfWork.Commit();
        }
    }
}
