using Invoices.Data.Infrastructure.Interfaces;
using Invoices.Data.Repositories.Interfaces;
using Invoices.Models;
using Invoices.Services.Interfaces;
using System.Collections.Generic;

namespace Invoices.Services.Implementation
{
    public class ChargeNameService : IChargeNameService
    {
        private readonly IChargeNameRepository chargeNameRepository;
        private readonly IUnitOfWork unitOfWork;

        public ChargeNameService(IChargeNameRepository chargeNameRepository, IUnitOfWork unitOfWork)
        {
            this.chargeNameRepository = chargeNameRepository;
            this.unitOfWork = unitOfWork;
        }

        public ChargeName GetChargeName(string name)
        {
            var chargeName = chargeNameRepository.GetChargeByName(name);
            return chargeName;
        }

        public ChargeName GetChargeName(int chargeNameId)
        {
            var chargeName = chargeNameRepository.GetById(chargeNameId);
            return chargeName;
        }

        public IEnumerable<ChargeName> GetChargeNames()
        {
            return chargeNameRepository.GetAll();
        }
    }
}
