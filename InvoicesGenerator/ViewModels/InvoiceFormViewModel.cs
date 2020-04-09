using Invoices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoicesGenerator.ViewModels
{
    public class InvoiceFormViewModel
    {
        public int InvoiceId { get; set; }
        [Required]
        public string InvoiceNumber { get; set; }
        [Required]
        public DateTime InvoiceDate { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public int ClientId { get; set; }
        public int DayChargeId { get; set; }
        [Required]
        public double DayChargeRate { get; set; }
        [Required]
        public int DayChargeUnits { get; set; }
        [Required]
        public double DayChargeAmount { get; set; }
        [Required]
        public double DayChargeTax { get; set; }
        [Required]
        public double DayChargeTotal { get; set; }
        public int DayChargeNameId { get; set; }
        public int NightChargeId { get; set; }
        [Required]
        public double NightChargeRate { get; set; }
        [Required]
        public int NightChargeUnits { get; set; }
        [Required]
        public double NightChargeAmount { get; set; }
        [Required]
        public double NightChargeTax { get; set; }
        [Required]
        public double NightChargeTotal { get; set; }
        public int NightChargeNameId { get; set; }
        [Required]
        public int WeekendChargeId { get; set; }
        [Required]
        public double WeekendChargeRate { get; set; }
        [Required]
        public int WeekendChargeUnits { get; set; }
        [Required]
        public double WeekendChargeAmount { get; set; }
        [Required]
        public double WeekendChargeTax { get; set; }
        [Required]
        public double WeekendChargeTotal { get; set; }
        public int WeekendChargeNameId { get; set; }

        public static implicit operator InvoiceFormViewModel(Invoice invoice)
        {
            var dayCharge = invoice.Charges.Where(i => i.ChargeName.Name == "Day").FirstOrDefault();
            var nightCharge = invoice.Charges.Where(i => i.ChargeName.Name == "Night").FirstOrDefault();
            var weekendCharge = invoice.Charges.Where(i => i.ChargeName.Name == "Weekend").FirstOrDefault();

            return new InvoiceFormViewModel
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceNumber = invoice.InvoiceNumber,
                InvoiceDate = invoice.InvoiceDate,
                CompanyName = invoice.CompanyName,
                StartDate = invoice.StartDate,
                EndDate = invoice.EndDate,
                ClientId = invoice.ClientId,
                DayChargeId = dayCharge.ChargeId,
                DayChargeRate = dayCharge.Rate,
                DayChargeUnits = dayCharge.Units,
                DayChargeAmount = dayCharge.Amount,
                DayChargeTax = dayCharge.Tax,
                DayChargeTotal = dayCharge.Total,
                DayChargeNameId = dayCharge.ChargeNameId,
                NightChargeId = nightCharge.ChargeId,
                NightChargeRate = nightCharge.Rate,
                NightChargeUnits = nightCharge.Units,
                NightChargeAmount = nightCharge.Amount,
                NightChargeTax = nightCharge.Tax,
                NightChargeTotal = nightCharge.Total,
                NightChargeNameId = nightCharge.ChargeNameId,
                WeekendChargeId = weekendCharge.ChargeId,
                WeekendChargeRate = weekendCharge.Rate,
                WeekendChargeUnits = weekendCharge.Units,
                WeekendChargeAmount = weekendCharge.Amount,
                WeekendChargeTax = weekendCharge.Tax,
                WeekendChargeTotal = weekendCharge.Total,
                WeekendChargeNameId = weekendCharge.ChargeNameId
            };
        }

        public static implicit operator Invoice(InvoiceFormViewModel vm)
        {
            var dayCharge = new Charge
            {
                ChargeId = vm.DayChargeId,
                Rate = vm.DayChargeRate,
                Units = vm.DayChargeUnits,
                Amount = vm.DayChargeAmount,
                Tax = vm.DayChargeTax,
                Total = vm.DayChargeTotal,
                ChargeNameId = vm.DayChargeNameId
            };

            var nightCharge = new Charge
            {
                ChargeId = vm.NightChargeId,
                Rate = vm.NightChargeRate,
                Units = vm.NightChargeUnits,
                Amount = vm.NightChargeAmount,
                Tax = vm.NightChargeTax,
                Total = vm.NightChargeTotal,
                ChargeNameId = vm.NightChargeNameId
            };

            var weekendCharge = new Charge
            {
                ChargeId = vm.WeekendChargeId,
                Rate = vm.WeekendChargeRate,
                Units = vm.WeekendChargeUnits,
                Amount = vm.WeekendChargeAmount,
                Tax = vm.WeekendChargeTax,
                Total = vm.WeekendChargeTotal,
                ChargeNameId = vm.WeekendChargeNameId
            };

            return new InvoiceFormViewModel
            {
                InvoiceId = vm.InvoiceId,
                InvoiceNumber = vm.InvoiceNumber,
                InvoiceDate = vm.InvoiceDate,
                CompanyName = vm.CompanyName,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                ClientId = vm.ClientId,
                DayChargeId = dayCharge.ChargeId,
                DayChargeRate = dayCharge.Rate,
                DayChargeUnits = dayCharge.Units,
                DayChargeAmount = dayCharge.Amount,
                DayChargeTax = dayCharge.Tax,
                DayChargeTotal = dayCharge.Total,
                DayChargeNameId = dayCharge.ChargeNameId,
                NightChargeId = nightCharge.ChargeId,
                NightChargeRate = nightCharge.Rate,
                NightChargeUnits = nightCharge.Units,
                NightChargeAmount = nightCharge.Amount,
                NightChargeTax = nightCharge.Tax,
                NightChargeTotal = nightCharge.Total,
                NightChargeNameId = nightCharge.ChargeNameId,
                WeekendChargeId = weekendCharge.ChargeId,
                WeekendChargeRate = weekendCharge.Rate,
                WeekendChargeUnits = weekendCharge.Units,
                WeekendChargeAmount = weekendCharge.Amount,
                WeekendChargeTax = weekendCharge.Tax,
                WeekendChargeTotal = weekendCharge.Total,
                WeekendChargeNameId = weekendCharge.ChargeNameId
            };
        }
    }
}