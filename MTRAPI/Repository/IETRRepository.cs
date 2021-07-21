using MTR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTR.Repository
{
    public interface IETRRepository
    {

        TaxPayer CreateTaxpayer(TaxPayer TaxpayerInfo);
        IEnumerable<TaxPayer> GetAllTaxPayers();

        ETR RegisterETR(TaxPayer TaxPayerInfo);

        IEnumerable<ETR> GetAllETRs();

        ETR GetETR(string id);

        ETRReceipt RegisterSale(Sale SaleInfo);


        Sale GetSaleBySerialNumber(string serialNumber);

        IEnumerable<Sale> GetDailySalesReport(string etrID);
        IEnumerable<Sale> GetMonthlySalesReport(string etrID);
        IEnumerable<Sale> GetYearToDateSalesReport(string etrID);
        TaxPayer GetTaxPayer(string id);
    }
}
