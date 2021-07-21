using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MTR.DTOs;
using MTR.Models;
using MTR.Repository;
using MTR.Services;

namespace MTRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly IETRRepository _repository;
        private readonly IMapper _mapper;
        private readonly ISMSService _smsService;

        public SalesController(IETRRepository repository, IMapper mapper, ISMSService smsService)
        {
            _repository = repository;
            _mapper = mapper;
            _smsService = smsService;
        }

        // GET api/Sale/5
        [HttpGet("{id}")]
        public ActionResult<SaleDTO> GetSaleBySerialNumber(string id)
        {
            var objSale = _repository.GetSaleBySerialNumber(id);
            if (objSale != null)
            {
                return Ok(_mapper.Map<SaleDTO>(objSale));
            }
            return NotFound();
        }

        // POST api/Sale
        [HttpPost]
        public ActionResult<ETRReceiptDTO> RegisterSale([FromBody] SaleDTO SaleInfo)
        {
            ETRReceipt objSaleReceipt = _repository.RegisterSale(_mapper.Map<Sale>(SaleInfo));

            string SMSMessage = $"KRA-{objSaleReceipt.ETRReceiptID.ToUpper()} " +
            $"{objSaleReceipt.SaleDate} " +
            $"A sale for {string.Format("Ksh {0:N2}", objSaleReceipt.SaleAmount)}. " +
            $"has been made on your Mobile Tax Register. The VAT Amount due on this sale is {string.Format("Ksh {0:N2}", objSaleReceipt.VATAmount)}";

            string telephone = SaleInfo.Telephone ?? objSaleReceipt.ETR.Telephone;

            _smsService.SendSMS(SMSMessage, telephone);

            ETRReceiptDTO receiptDTO = _mapper.Map<ETRReceiptDTO>(objSaleReceipt);

            return Ok(receiptDTO);
        }
    }
}
