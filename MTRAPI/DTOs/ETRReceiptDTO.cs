using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTR.DTOs
{
    public class ETRReceiptDTO
    {
        public string ETRReceiptID { get; set; }
        public string ETRID { get; set; }
        public string SaleID { get; set; }
        public string SaleDate { get; set; }
        public double SaleAmount { get; set; }
        public double VATAmount { get; set; }

        //public SaleDTO Sale { get; set; }
        
        public ETRDTO ETR { get; set; }

        //public double CurrentDaySalesAmount { get; set; }
        //public double CurrentDayVATAmount { get; set; }

        //public double CurrentMonthToDateSalesAmount { get; set; }
        //public double CurrentMonthToDateVATAmount { get; set; }

        //public double CurrentYearToDateSalesAmount { get; set; }
        //public double CurrentYearToDateVATAmount { get; set; }
    }
}