using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnrollmentService.Data;
using EnrollmentService.Dtos;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentService.Controllers
{

  [ApiController]
  [Route("api/e/[controller]")]
  [Authorize(Roles = "admin")]
  public class EnrollmentController : ControllerBase
  {
    private IEnrollment _enrollment;
    private IMapper _mapper;
    public EnrollmentController(IEnrollment enrollment, IMapper mapper)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _enrollment = enrollment;
    }

    [HttpGet]
    public async Task<IEnumerable<Enrollment>> Get()
    {
      var results = await _enrollment.GetAll();
      return results;
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Enrollment>> Get(int id)
    {
      try
      {
        var results = await _enrollment.GetById(id.ToString());
        if (results == null)
          return NotFound();
        return Ok((results));
      }
      catch (System.Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    // POST api/values
    [HttpPost]
    public async Task<ActionResult<EnrollmentInput>> Post([FromBody] EnrollmentInput input)
    {
      try
      {
        var dtos = _mapper.Map<Enrollment>(input);
        var result = await _enrollment.Insert(dtos);
        return Ok(_mapper.Map<EnrollmentInput>(result));
      }
      catch (System.Exception ex)
      {

        return BadRequest(ex.Message);
      }
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public async Task<ActionResult<EnrollmentInput>> Put(int id, [FromBody] EnrollmentInput input)
    {
      try
      {
        var dtos = _mapper.Map<Enrollment>(input);
        var result = await _enrollment.Update(id.ToString(), dtos);
        return Ok(_mapper.Map<EnrollmentInput>(result));
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        await _enrollment.Delete(id.ToString());
        return Ok($"Data enrollment {id} berhasil didelete");
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

  }
}
