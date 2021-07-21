using MTR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTR.Repository
{
    public class MockRepository : IETRRepository
    {
        const double VATRATE = 0.16;
        public ETR RegisterETR(TaxPayer TaxPayerInfo)
        {
            ETR objETR = new ETR
            {
                ETRID = Guid.NewGuid().ToString().ToUpper(),
                TaxPayerID = TaxPayerInfo.KRAPin,
                RegistrationDate = DateTime.Now.ToString("MMM dd yyyy")
            };

            TaxPayerInfo.ETRs.Add(objETR);
            objETR.TaxPayer = TaxPayerInfo;

            return objETR;
        }

        public ETRReceipt RegisterSale(Sale SaleInfo)
        {
            Sale objSale = new Sale
            {
                SaleID = Guid.NewGuid().ToString(),
                SaleDate = DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss"),
                SaleAmount = SaleInfo.SaleAmount,
                ETRID = SaleInfo.ETRID,
                VATAmount = SaleInfo.SaleAmount * VATRATE,
            };

            ETRReceipt objReceipt = new ETRReceipt();
            objReceipt.ETRReceiptID = Guid.NewGuid().ToString();
            objReceipt.SaleID = objSale.SaleID;

            objSale.ETRReceipt = objReceipt;
            return objReceipt;
        }



        public IEnumerable<ETR> GetAllETRs()
        {
            var objETRs = new List<ETR>
            {
                new ETR(),
                new ETR(),
                new ETR(),
                new ETR(),
                new ETR(),
                new ETR(),
                new ETR(),
                new ETR(),
                new ETR(),
                new ETR(),
                new ETR()
            };
            return objETRs;
        }

        public ETR GetETRBySerialNumber(string SerialNumber)
        {
            ETR objETR = new ETR();
            objETR.ETRID = SerialNumber;
            return objETR;
        }



        public ETRReceipt GetReceiptBySaleID(string SaleID)
        {
            ETRReceipt objReceipt = new ETRReceipt();
            return objReceipt;
        }


        public Sale GetSaleBySerialNumber(string SerialNumber)
        {
            Console.WriteLine("GetSaleBySerialNumber");
            return new Sale();
        }

        public IEnumerable<Sale> GetDailySalesReport(string etrID)
        {
            Console.WriteLine("GetDailySales");
            return GenerateMockSales();
        }

        public IEnumerable<Sale> GetMonthlySalesReport(string etrID)
        {
            Console.WriteLine("GetMonthlySales");
            return GenerateMockSales();
        }

        public IEnumerable<Sale> GetYearToDateSalesReport(string etrID)
        {
            Console.WriteLine("GetYearToDateSales");
            return GenerateMockSales();
        }

        private IEnumerable<Sale> GenerateMockSales()
        {
            var objSales = new List<Sale>
            {
                new Sale(),
                new Sale(),
                new Sale(),
                new Sale(),
                new Sale(),
                new Sale(),
                new Sale(),
                new Sale(),
                new Sale(),
                new Sale(),
                new Sale(),
            };
            return objSales;
        }

        public TaxPayer GetTaxPayer(string id)
        {
            throw new NotImplementedException();
        }

        public TaxPayer CreateTaxpayer(TaxPayer TaxpayerInfo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TaxPayer> GetAllTaxPayers()
        {
            throw new NotImplementedException();
        }

    }

}
