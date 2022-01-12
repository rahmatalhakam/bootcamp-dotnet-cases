using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EnrollmentService.Data;
using EnrollmentService.Dtos;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnrollmentService.Controllers
{
  [Route("/api/e/[controller]")]
  [ApiController]
  [Authorize]
  public class CoursesController : ControllerBase
  {
    private ICourse _course;
    private IMapper _mapper;
    public CoursesController(ICourse course, IMapper mapper)
    {
      _course = course ?? throw new ArgumentNullException(nameof(course));
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseOutput>>> Get()
    {
      var courses = await _course.GetAll();
      var dtos = _mapper.Map<IEnumerable<CourseOutput>>(courses);
      return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseOutput>> Get(string id)
    {
      var course = await _course.GetById(id);
      if (course == null)
        return NotFound();
      var dto = _mapper.Map<CourseOutput>(course);
      return Ok(dto);
    }

    [HttpGet("bytitle")]
    public async Task<IEnumerable<Course>> GetByTitle(string title)
    {
      return await _course.GetByTitle(title);
    }

    [HttpPost]
    public async Task<ActionResult<CourseOutput>> Post([FromBody] CourseInput course)
    {
      try
      {
        var result = await _course.Insert(_mapper.Map<Course>(course));
        var dto = _mapper.Map<CourseOutput>(result);
        return Ok(dto);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CourseOutput>> Put(int id, [FromBody] CourseInput course)
    {
      try
      {
        var result = await _course.Update(id.ToString(), _mapper.Map<Course>(course));
        return Ok(_mapper.Map<CourseOutput>(result));
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
        await _course.Delete(id.ToString());
        return Ok($"Data student {id} berhasil didelete");
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet("bystudentid")]
    public async Task<IEnumerable<Course>> GetByStudentID(int id)
    {
      return await _course.GetByStudentID(id);
    }

  }
}
