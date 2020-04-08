using Invoices.Data.Infrastructure.Interfaces;
using Invoices.Data.Repositories.Interfaces;
using Invoices.Models;
using Invoices.Services.Interfaces;
using System.Collections.Generic;

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

        public void SaveCharge()
        {
            unitOfWork.Commit();
        }
    }
}
