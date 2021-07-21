using Microsoft.EntityFrameworkCore;
using MTR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MTR.Services;

namespace MTR.Repository
{
    public class SQLRepository : IETRRepository
    {
        const double VATRATE = 0.16;

        private MTRDbContext _dbContext { get; }
        private readonly IRandomCodeGenerator _randomCodeGenerator;
        private readonly object IDLock = new object();

        public SQLRepository(MTRDbContext dbContext, IRandomCodeGenerator randomCodeGenerator)
        {
            _dbContext = dbContext;
            _randomCodeGenerator = randomCodeGenerator;
        }

        public TaxPayer CreateTaxpayer(TaxPayer TaxPayerInfo)
        {
            TaxPayer objTaxPayer = GetTaxPayer(TaxPayerInfo.KRAPin);
            if (objTaxPayer == null)
            {
                objTaxPayer = new TaxPayer
                {
                    TaxPayerID = Guid.NewGuid().ToString(),
                    KRAPin = TaxPayerInfo.KRAPin,
                    RegistrationDate = DateTime.Now.ToString("MMM dd yyy"),
                    Name = TaxPayerInfo.Name,
                    Telephone = this.PrefixPlusTwoFiveFour(TaxPayerInfo.Telephone),
                    EmailAddress = TaxPayerInfo.EmailAddress,
                    Address = new Address
                    {
                        AddressID = Guid.NewGuid().ToString(),
                        POBox = TaxPayerInfo.Address.POBox,
                        PostalCode = TaxPayerInfo.Address.PostalCode,
                        CityTown = TaxPayerInfo.Address.CityTown,
                        County = TaxPayerInfo.Address.County
                    },
                    ETRs = new List<ETR>(),
                };
                _dbContext.TaxPayers.Add(objTaxPayer);
                _dbContext.SaveChanges();
            };
            return objTaxPayer;
        }

        public TaxPayer GetTaxPayer(string id)
        {
            TaxPayer objTaxPayer = _dbContext.TaxPayers
                .Include(tp => tp.Address)
                .Include(tp => tp.ETRs)
                    .ThenInclude(etr => etr.Sales)
                .Where(
                    tp => tp.KRAPin.Trim() == id.Trim()
                    || tp.TaxPayerID.Trim() == id.Trim()
                    )
                .FirstOrDefault<TaxPayer>()
                ;
            return objTaxPayer;
        }
        public IEnumerable<TaxPayer> GetAllTaxPayers()
        {
            IEnumerable<TaxPayer> objTaxPayers = _dbContext.TaxPayers
                .Include(tp => tp.Address)
                .Include(tp => tp.ETRs);
            return objTaxPayers;
        }

        public ETR RegisterETR(TaxPayer TaxPayerInfo)
        {
            TaxPayer objTaxPayer = GetTaxPayer(TaxPayerInfo.KRAPin);
            if (objTaxPayer == null)
            {
                objTaxPayer = CreateTaxpayer(TaxPayerInfo);
            };

            ETR objETR = objTaxPayer.ETRs.Find(etr => etr.Telephone == PrefixPlusTwoFiveFour(TaxPayerInfo.Telephone));

            if (objETR == null)
            {
                objETR = new ETR
                {
                    ETRID = GenerateID("KRA/ETR/"),
                    RegistrationDate = DateTime.Now.ToString("MMM dd yyyy"),
                    ConfirmationCode = _randomCodeGenerator.GenerateConfirmationCode(),
                    Telephone = PrefixPlusTwoFiveFour(TaxPayerInfo.Telephone),
                    TaxPayerID = objTaxPayer.KRAPin,
                    TaxPayer = objTaxPayer,
                    Sales = new List<Sale>(),
                };
                objTaxPayer.ETRs.Add(objETR);
                objETR.TaxPayer = objTaxPayer;
                _dbContext.ETRs.Add(objETR);
                _dbContext.SaveChanges();
            }
            return objETR;
        }
        public IEnumerable<ETR> GetAllETRs()
        {
            IEnumerable<ETR> objETRs = _dbContext.ETRs.ToList();
            //.Include(etr => etr.TaxPayer)
            //.Include(etr => etr.Sales);
            return objETRs;
        }
        public ETR GetETR(string ETRID)
        {
            ETR objETR = _dbContext.ETRs
                .Include(etr => etr.Sales)
                .FirstOrDefault(etr => etr.ETRID.Trim() == ETRID.Trim());
            return objETR;
        }


        public ETRReceipt RegisterSale(Sale SaleInfo)
        {
            ETRReceipt objReceipt;
            using (_dbContext)
            {
                Sale objSale = new Sale
                {
                    SaleID = GenerateID(""),
                    SaleDate = DateTime.Now.ToString("MMM dd yyyy"),
                    SaleAmount = SaleInfo.SaleAmount,
                    ETRID = SaleInfo.ETRID,
                    VATAmount = SaleInfo.SaleAmount * VATRATE,
                };

                ETR objETR = GetETR(objSale.ETRID);
                objSale.ETR = objETR;

                _dbContext.Sales.Add(objSale);
                _dbContext.SaveChanges();

                //Recompute ETR Total Sales and Tax Figures for the ETR
                IQueryable<Sale> objSales = GetSalesFromETR(objETR);

                objETR.CurrentDaySalesAmount = GetCurrentDaySalesAmount(objSales);
                objETR.CurrentDayVATAmount = GetCurrentDayVATAmount(objSales);
                objETR.CurrentMonthToDateSalesAmount = GetCurrentMonthToDateSalesAmount(objSales);
                objETR.CurrentMonthToDateVATAmount = GetCurrentMonthToDateVATAmount(objSales);
                objETR.CurrentYearToDateSalesAmount = GetCurrentYearToDateSalesAmount(objSales);
                objETR.CurrentYearToDateVATAmount = GetCurrentYearToDateVATAmount(objSales);

                objReceipt = new ETRReceipt
                {
                    ETRReceiptID = Guid.NewGuid().ToString(),
                    SaleID = objSale.SaleID,
                    ETRID = objSale.ETRID,

                    SaleAmount = objSale.SaleAmount,
                    VATAmount = objSale.VATAmount,
                    SaleDate = objSale.SaleDate,
                    Sale = objSale,
                    ETR = objETR,
                };

                //objSale.ETRReceiptID = objReceipt.ETRReceiptID;
                objSale.ETRReceipt = objReceipt;

                _dbContext.ETRReceipts.Add(objReceipt);

                _dbContext.SaveChanges();
            }
            return objReceipt;
        }
        private IQueryable<Sale> GetSalesFromETR(ETR eTR)
        {
            return _dbContext.Sales
                .Where(sale => sale.ETRID == eTR.ETRID);
        }
        public Sale GetSaleBySerialNumber(string SerialNumber)
        {
            return _dbContext.Sales
                .FirstOrDefault(sale => sale.SaleID.Trim() == SerialNumber.Trim());
        }

        public ETRReceipt GetReceiptBySaleID(string SaleID)
        {
            ETRReceipt objReceipt = new ETRReceipt();
            return objReceipt;
        }

        public IEnumerable<Sale> GetDailySalesReport(string etrID)
        {
            return _dbContext.Sales.AsEnumerable()
                .Where(sale => IsToday(DateTime.Parse(sale.SaleDate)) && sale.ETRID == etrID);
        }

        public IEnumerable<Sale> GetMonthlySalesReport(string etrID)
        {
            return _dbContext.Sales.AsEnumerable()
                .Where(sale => IsThisMonth(DateTime.Parse(sale.SaleDate)) && sale.ETRID == etrID);
        }
        public IEnumerable<Sale> GetYearToDateSalesReport(string etrID)
        {
            return _dbContext.Sales.AsEnumerable()
                .Where(sale => IsThisYear(DateTime.Parse(sale.SaleDate)) && sale.ETRID == etrID);
        }

        private double GetCurrentDaySalesAmount(IQueryable<Sale> sales)
        {
            return sales
                .AsEnumerable()
                .Where(sale => IsToday(DateTime.Parse(sale.SaleDate)))
                .Sum(sale => sale.SaleAmount)
                ;
        }
        private double GetCurrentDayVATAmount(IQueryable<Sale> sales)
        {
            return sales
                .AsEnumerable()
                .Where(sale => IsToday(DateTime.Parse(sale.SaleDate)))
                .Sum(sale => sale.VATAmount)
                ;
        }

        private double GetCurrentMonthToDateSalesAmount(IQueryable<Sale> sales)
        {
            return sales
                .AsEnumerable()
                .Where(sale => IsThisMonth(DateTime.Parse(sale.SaleDate)))
                .Sum(sale => sale.SaleAmount)
             ;
        }
        private double GetCurrentMonthToDateVATAmount(IQueryable<Sale> sales)
        {
            return sales
                .AsEnumerable()
                .Where(sale => IsThisMonth(DateTime.Parse(sale.SaleDate)))
                .Sum(sale => sale.VATAmount)
                ;
        }

        private double GetCurrentYearToDateSalesAmount(IQueryable<Sale> sales)
        {
            return sales
                .AsEnumerable()
                .Where(sale => IsThisYear(DateTime.Parse(sale.SaleDate)))
                .Sum(sale => sale.SaleAmount)
                ;
        }
        private double GetCurrentYearToDateVATAmount(IQueryable<Sale> sales)
        {
            return sales
                .AsEnumerable()
                .Where(sale => IsThisYear(DateTime.Parse(sale.SaleDate)))
                .Sum(sale => sale.VATAmount)
                ;
        }

        private bool IsToday(DateTime dt)
        {
            return (dt.Date == DateTime.Now.Date);
        }
        private bool IsThisMonth(DateTime dt)
        {
            return ((dt.Month == DateTime.Now.Month) && (IsThisYear(dt)));
        }
        private bool IsThisYear(DateTime dt)
        {
            return dt.Year == DateTime.Now.Year;
        }

        private string PrefixPlusTwoFiveFour(string phoneNumber)
        {
            if (phoneNumber.StartsWith("0"))
            {
                phoneNumber = "+254" + phoneNumber.Substring(1);
            }
            if (phoneNumber.StartsWith("+") == false)
            {
                phoneNumber = "+" + phoneNumber;
            }
            return phoneNumber;
        }

        private string GenerateID(string prefix)              
        {
            lock (IDLock)
            {
                string dateToday = DateTime.Now.ToString("MMddyyyy");
                string timeNow = DateTime.Now.TimeOfDay.ToString().Replace(":", "").Replace(".", "");
                string ID = prefix + dateToday + "/" + timeNow;
                return ID;
            }
        }
    }
}
