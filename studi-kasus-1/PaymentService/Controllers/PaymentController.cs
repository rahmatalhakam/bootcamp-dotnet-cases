using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Data;
using PaymentService.Dtos;
using PaymentService.Models;

namespace PaymentService.Controllers
{

  [ApiController]
  [Route("api/p/[controller]")]
  [Authorize(Roles = "admin")]
  public class PaymentController : ControllerBase
  {

    private IMapper _mapper;
    private readonly IPayment<Payment> _payment;

    public PaymentController(IPayment<Payment> payment, IMapper mapper)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _payment = payment;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PaymentOutput>>> Get()
    {
      var results = await _payment.GetAll();
      return Ok(_mapper.Map<IEnumerable<PaymentOutput>>(results));
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<ActionResult<PaymentOutput>> Get(int id)
    {
      try
      {
        var results = await _payment.GetById(id);
        if (results == null)
          return NotFound();
        return Ok(_mapper.Map<PaymentOutput>(results));
      }
      catch (System.Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    // POST api/values
    [HttpPost]
    public async Task<ActionResult<PaymentOutput>> Post([FromBody] PaymentInput input)
    {
      try
      {
        var dtos = _mapper.Map<Payment>(input);
        var result = await _payment.Insert(dtos);
        return Ok(_mapper.Map<PaymentOutput>(result));
      }
      catch (System.Exception ex)
      {

        return BadRequest(ex.Message);
      }
    }



  }
}
