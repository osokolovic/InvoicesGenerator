using Invoices.Data;
using Invoices.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Invoices.Data
{
    public class InvoiceSeedData : CreateDatabaseIfNotExists<InvoiceEntities>
    {
        protected override void Seed(InvoiceEntities context)
        {
            GetClients().ForEach(c => context.Clients.Add(c));
            GetInvoices().ForEach(i => context.Invoices.Add(i));
            GetChargeNames().ForEach(cn => context.ChargeNames.Add(cn));
            GetCharges().ForEach(c => context.Charges.Add(c));

            context.Commit();
        }

        private static List<Client> GetClients()
        {
            return new List<Client>
            {
                new Client {
                    CompanyName = "BH Telekom",
                    City = "71000 Sarajevo",
                    Address = "Malta 5",
                    Contract = "C1",
                    Phone = "061111222",
                    Email = "bhtelecom@bih.net.ba",
                    ContactPerson = "Hamo Hamić",
                    ContractDateStart = new DateTime(2016, 1, 1),
                    ContractDateEnd = null,
                    Deleted = false,
                    Status = "Active",
                },
                new Client {
                    CompanyName = "Telekom Srpske",
                    City = "51000 Banja Luka",
                    Address = "Trg Krajine 8",
                    Contract = "B1",
                    Phone = "065333444",
                    Email = "info@mtel.ba",
                    ContactPerson = "Marko Marković",
                    ContractDateStart = new DateTime(2016, 6, 1),
                    ContractDateEnd = new DateTime(2019, 12, 31),
                    Deleted = false,
                    Status = "Active"
                },
                new Client {
                    CompanyName = "HT Mostar",
                    City = "36000 Mostar",
                    Address = "Zapadna strana 1",
                    Contract = "K1",
                    Phone = "063555666",
                    Email = "info@eronet.ba",
                    ContactPerson = "Anto Antić",
                    ContractDateStart = new DateTime(2015, 8, 1),
                    ContractDateEnd = new DateTime(2018, 5, 31),
                    Deleted = false,
                    Status = "Inactive"
                },
            };
        }

        private static List<Invoice> GetInvoices()
        {
            return new List<Invoice>
            {
                new Invoice {
                    InvoiceNumber = "A1",
                    InvoiceDate = new DateTime(2018, 2, 10),
                    StartDate = new DateTime(2018, 1, 1),
                    EndDate = new DateTime(2018, 1, 31),
                    CompanyName = "BH Telecom",
                    Deleted = false,
                    ClientId = 1,
                },
                new Invoice {
                    InvoiceNumber = "A2",
                    InvoiceDate = new DateTime(2018, 3, 11),
                    StartDate = new DateTime(2018, 2, 1),
                    EndDate = new DateTime(2018, 2, 28),
                    CompanyName = "BH Telecom",
                    Deleted = false,
                    ClientId = 1,
                },
                new Invoice {
                    InvoiceNumber = "B1",
                    InvoiceDate = new DateTime(2018, 2, 10),
                    StartDate = new DateTime(2018, 1, 1),
                    EndDate = new DateTime(2018, 1, 31),
                    CompanyName = "Telekom Srpske",
                    Deleted = false,
                    ClientId = 2,
                },
                new Invoice {
                    InvoiceNumber = "B1",
                    InvoiceDate = new DateTime(2018, 2, 10),
                    StartDate = new DateTime(2018, 1, 1),
                    EndDate = new DateTime(2018, 1, 31),
                    CompanyName = "HT Erotnet",
                    Deleted = false,
                    ClientId = 3,
                }
            };
        }

        private static List<ChargeName> GetChargeNames()
        {
            return new List<ChargeName>
            {
                new ChargeName {
                    Name = "Day",
                    Deleted = false
                },
                new ChargeName {
                    Name = "Night",
                    Deleted = false
                },
                new ChargeName {
                    Name = "Weekend",
                    Deleted = false
                },
            };
        }

        private static List<Charge> GetCharges()
        {
            return new List<Charge>
            {
                new Charge {
                    Rate = 0.2,
                    Units = 1289,
                    Amount = 257.8,
                    Tax = 43.83,
                    Total = 1486.24,
                    Deleted = false,
                    ChargeNameId = 1,
                    InvoiceId = 1
                },
                new Charge {
                    Rate = 0.15,
                    Units = 6567,
                    Amount = 985.05,
                    Tax = 167.46,
                    Total = 1486.24,
                    Deleted = false,
                    ChargeNameId = 2,
                    InvoiceId = 1
                },
                new Charge {
                    Rate = 0.28,
                    Units = 98,
                    Amount = 27.44,
                    Tax = 4.66,
                    Total = 1486.24,
                    Deleted = false,
                    ChargeNameId = 3,
                    InvoiceId = 1
                },
            };
        }
    }
}
