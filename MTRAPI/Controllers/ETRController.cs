using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTR.DTOs;
using MTR.Models;
using MTR.Repository;
using MTR.Services;

namespace MTRAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ETRController : ControllerBase
    {
        private readonly IETRRepository _repository;
        private readonly IMapper _mapper;
        private readonly ISMSService _smsService;
       
        public ETRController(IETRRepository repository,IMapper mapper,ISMSService smsService)
        {
            _repository = repository;
            _mapper = mapper;
            _smsService = smsService;
           
        }


        /// <summary>
        /// Registers  a new MTR for an existing Taxpayer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "id": 1,
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        ///
        /// </remarks>
        /// <param name="TaxpayerInfo"></param>
        /// <returns>A newly created ETR</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
        // POST api/<ETRController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ETRDTO> RegisterETR([FromBody] TaxPayerDTO TaxpayerInfo)
        {
            var objETR = _repository.RegisterETR(_mapper.Map<TaxPayer>(TaxpayerInfo));
            if (objETR != null)
            {
                string SMSMessage = $"{objETR.ETRID} " +
                    $"{objETR.RegistrationDate} " +
                    $"Your Mobile Tax Register has now been activated. " +
                    $"Please use {objETR.ConfirmationCode} for the Confirmation Code";

                _smsService.SendSMS(SMSMessage, objETR.Telephone);

                return Ok(_mapper.Map<ETRDTO>(objETR));
            }
            return NotFound();
        }

        ////  api/<ETRController>
        //[HttpPost]
        //public ActionResult<ETRDTO> ConfirmETRCode([FromBody] ETRDTO ETRInfo)
        //{
        //    var objETR = _repository.GetETRBySerialNumber(ETRInfo.ETRID);
        //    if (objETR != null)
        //    {
        //        if (ETRInfo.ConfirmationCode == objETR.ConfirmationCode)
        //        {
        //            return Ok(_mapper.Map<ETRDTO>(objETR));
        //        };
        //    }
        //    return NotFound();
        //}

       
        // GET api/ETR/5
        [HttpGet("{id}")]
        public ActionResult<ETRDTO> GetETR(string id)
        {
            var objETR = _repository.GetETR(id);
            if (objETR != null)
            {
                return Ok(_mapper.Map<ETRDTO>(objETR));
            }
            return NotFound();

        }

        // GET: api/ETR/
        [HttpGet]
        public ActionResult<IEnumerable<ETRDTO>> GetAllETRs()
        {
            var objETRs = _repository.GetAllETRs();
            if (objETRs != null)
            {
                return Ok(_mapper.Map<IEnumerable<ETRDTO>>(objETRs));
            }
            return NotFound();
        }

    }
}
