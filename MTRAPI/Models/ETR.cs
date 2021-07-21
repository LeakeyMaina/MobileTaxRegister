using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTR.Models
{
    public class ETR
    {
        [Key]
        [MaxLength(30)]
        public string ETRID { get; set; }
        
        [Required]
        public string RegistrationDate { get; set; }

        [Required]
        public string ConfirmationCode { get; set; }

        [Required]
        public string Telephone { get; set; }

        [Required]
        public string TaxPayerID { get; set; }
        public TaxPayer TaxPayer { get; set; }


        public List<Sale> Sales { get; set; }

        public double CurrentDaySalesAmount { get; set; }
        public double CurrentDayVATAmount { get; set; }
        public double CurrentMonthToDateSalesAmount { get; set; }
        public double CurrentMonthToDateVATAmount { get; set; }
        public double CurrentYearToDateSalesAmount { get; set; }
        public double CurrentYearToDateVATAmount { get; set; }
        
    }
}
