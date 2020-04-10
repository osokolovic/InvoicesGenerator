using Invoices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InvoicesGenerator.ViewModels
{
    public class InvoiceChartFormViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Total { get; set; }

    }
}