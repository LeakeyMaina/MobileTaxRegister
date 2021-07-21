using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTR.Models
{
    public class TaxPayer
    {
        [Key]
        [MaxLength(36)]
        public string TaxPayerID { get; set; }
        public string RegistrationDate { get; set; }

        [Required]
        [MaxLength(15)]
        public string KRAPin { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        public string Telephone { get; set; }
        [Required]
        [MaxLength(50)]
        public string EmailAddress { get; set; }

        public Address Address { get; set; }

        public List<ETR> ETRs { get; set; }

        public double CurrentMonthToDateSalesAmount { get; set; }
        public double CurrentMonthToDateVATAmount { get; set; }
        public double CurrentYearToDateSalesAmount { get; set; }
        public double CurrentYearToDateVATAmount { get; set; }
        public double CurrentDaySalesAmount { get; set; }
        public double CurrentDayVATAmount { get; set; }

    }
}