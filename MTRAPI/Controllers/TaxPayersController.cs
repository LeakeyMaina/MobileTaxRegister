using System;
using System.Collections.Generic;
using System.Globalization;
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
    [Route("api/[controller]")]
    [ApiController]
    public class TaxPayersController : ControllerBase
    {
        private readonly IETRRepository _repository;
        private readonly IMapper _mapper;
        private readonly ISMSService _smsService;

        public TaxPayersController(IETRRepository repository, IMapper mapper, ISMSService smsService)
        {
            _repository = repository;
            _mapper = mapper;
            _smsService = smsService;
        }

        /// <summary>
        /// Registers  a new Taxpayer.
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
        // POST api/<TaxPayers>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<TaxPayerDTO> RegisterTaxpayer([FromBody] TaxPayerDTO TaxpayerInfo)
        {
            var objTaxPayer= _repository.CreateTaxpayer(_mapper.Map<TaxPayer>(TaxpayerInfo));
            if (objTaxPayer != null)
            {
                return Ok(_mapper.Map<TaxPayerDTO>(objTaxPayer));
            }
            return NotFound();
        }

        // GET api/TaxPayers/5
        [HttpGet("{id}")]
        public ActionResult<TaxPayerDTO> GetTaxPayer(string id)
        {
            var objTaxPayer = _repository.GetTaxPayer(id);
            if (objTaxPayer != null)
            {
                return Ok(_mapper.Map<TaxPayerDTO>(objTaxPayer));
            }
            return NotFound();
        }

        // GET api/TaxPayers/
        [HttpGet]
        public ActionResult<IEnumerable<TaxPayerDTO>> GetAllTaxPayers()
        {
            var objTaxPayers = _repository.GetAllTaxPayers();
            if (objTaxPayers != null)
            {
                return Ok(_mapper.Map<IEnumerable<TaxPayerDTO>>(objTaxPayers));
               
            }
            return NotFound();
        }
    }
}
