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
  public class StudentsController : ControllerBase
  {
    private IStudent _student;
    private IMapper _mapper;
    public StudentsController(IStudent student, IMapper mapper)
    {
      _student = student ?? throw new ArgumentNullException(nameof(student));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentOutput>>> Get()
    {
      var students = await _student.GetAll();
      var dtos = _mapper.Map<IEnumerable<StudentOutput>>(students);
      return Ok(dtos);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "student")]
    public async Task<ActionResult<StudentOutput>> Get(string id)
    {
      var results = await _student.GetById(id);
      if (results == null)
        return NotFound();
      return Ok(_mapper.Map<StudentOutput>(results));

    }

    [HttpPost]
    public async Task<ActionResult<StudentOutput>> Post([FromBody] StudentInput student)
    {
      try
      {
        var dtos = _mapper.Map<Student>(student);
        var result = await _student.Insert(dtos);
        return Ok(_mapper.Map<StudentOutput>(result));
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<StudentOutput>> Put(int id, [FromBody] StudentInput student)
    {
      try
      {
        var result = await _student.Update(id.ToString(), _mapper.Map<Student>(student));
        return Ok(_mapper.Map<StudentOutput>(result));
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
        await _student.Delete(id.ToString());
        return Ok($"Data student {id} berhasil didelete");
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet("{id}/course")]
    [Authorize(Roles = "student")]
    public async Task<IEnumerable<Student>> GetByCourseId(int id)
    {
      var results = await _student.GetByCourseID(id);
      return results;
    }
  }
}
