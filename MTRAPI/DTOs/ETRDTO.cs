using System;
using System.Collections.Generic;

namespace MTR.DTOs
{
    public class ETRDTO
    {
        public string ETRID { get; set; }
        public string RegistrationDate { get; set; }
        public string Telephone { get; set; }
        public string TaxPayerID { get; set; }

        public string ConfirmationCode { get; set; }

        public double CurrentDaySalesAmount { get; set; }
        public double CurrentDayVATAmount { get; set; }
        public double CurrentMonthToDateSalesAmount { get; set; }
        public double CurrentMonthToDateVATAmount { get; set; }
        public double CurrentYearToDateSalesAmount { get; set; }
        public double CurrentYearToDateVATAmount { get; set; }



        public TaxPayerDTO TaxPayer { get; set; }

        public List<SaleDTO> Sales { get; set; }

    }
}