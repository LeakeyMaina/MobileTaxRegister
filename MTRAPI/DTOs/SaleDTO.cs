using System;
namespace MTR.DTOs
{
    public class SaleDTO
    {
        public string SaleID { get; set; }
        public string SaleDate { get; set; }
        public double SaleAmount { get; set; }
        public double VATAmount { get; set; }
        public string ETRID { get; set; }
        public string Telephone
        {
            get;
            set;
        }
        public string ETRReceiptID { get; set; }
        public ETRReceiptDTO Receipt { get; set; }

    }
}
