using System;
using System.Collections.Generic;

namespace MTR.DTOs
{
    public class TaxPayerDTO
    {
        public string TaxPayerID { get; set; }
        public string RegistrationDate { get; set; }
        public string KRAPin { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public AddressDTO Address { get; set; }

        public List<ETRDTO> ETRs { get; set; }

        public double CurrentMonthToDateSalesAmount { get; set; }
        public double CurrentMonthToDateVATAmount { get; set; }

        public double CurrentYearToDateSalesAmount { get; set; }
        public double CurrentYearToDateVATAmount { get; set; }
        public double CurrentDaySalesAmount { get; set; }
        public double CurrentDayVATAmount { get; set; }


    }
}