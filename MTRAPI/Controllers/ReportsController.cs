using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MTR.DTOs;
using MTR.Models;
using MTR.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MTR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IETRRepository _repository;
        private readonly IMapper _mapper;

        public ReportsController(IETRRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET api/Reports
        //[HttpGet]
        //public ActionResult<IEnumerable<SaleDTO>> GetSalesReport([FromBody] ReportRequest reportRequestInfo)
        //{
        //    IEnumerable<Sale> objSales = null;
        //    switch (reportRequestInfo.reportType)
        //    {
        //        case SaleReportTypes.DAILY:
        //            objSales = _repository.GetDailySalesReport(reportRequestInfo.ETRID);
        //            break;
        //        case SaleReportTypes.MONTHLY:
        //            objSales = _repository.GetMonthlySalesReport(reportRequestInfo.ETRID);
        //            break;
        //        case SaleReportTypes.YEARTODATE:
        //            objSales = _repository.GetYearToDateSalesReport(reportRequestInfo.ETRID);
        //            break;
        //        default:
        //            objSales = _repository.GetDailySalesReport(reportRequestInfo.ETRID);
        //            break;
        //    }
        //    if (objSales != null)
        //    {
        //        return Ok(_mapper.Map<IEnumerable<SaleDTO>>(objSales));
        //    }
        //    return NotFound();
        //}

        // GET api/Reports
        [HttpGet()]
        public ActionResult<IEnumerable<SaleDTO>> GetSalesReport(int reportType, string etrID)
        {
            IEnumerable<Sale> objSales = null;
            switch (reportType)
            {
                case (int)SaleReportTypes.DAILY:
                    objSales = _repository.GetDailySalesReport(etrID);
                    break;
                case (int)SaleReportTypes.MONTHLY:
                    objSales = _repository.GetMonthlySalesReport(etrID);
                    break;
                case (int)SaleReportTypes.YEARTODATE:
                    objSales = _repository.GetYearToDateSalesReport(etrID);
                    break;
                default:
                    objSales = _repository.GetDailySalesReport(etrID);
                    break;
            }
            if (objSales != null)
            {
                return Ok(_mapper.Map<IEnumerable<SaleDTO>>(objSales));
            }
            return NotFound();
        }

    }

    public class ReportRequest
    {
        public SaleReportTypes reportType { get; set; }
        public string ETRID { get; set; }
    }
    public enum SaleReportTypes
    {
        DAILY = 1,
        MONTHLY = 2,
        YEARTODATE = 3
    }
}
