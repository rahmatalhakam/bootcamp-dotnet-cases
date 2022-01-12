using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentService.Data;
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
    public EnrollmentController(IEnrollment enrollment)
    {
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
    public string Get(int id)
    {
      return "value";
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }

  }
}
