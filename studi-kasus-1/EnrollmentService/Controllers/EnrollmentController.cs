using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using EnrollmentService.Data;
using EnrollmentService.Dtos;
using EnrollmentService.Helpers;
using EnrollmentService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;

namespace EnrollmentService.Controllers
{

  [ApiController]
  [Route("api/e/[controller]")]
  [Authorize(Roles = "admin, student")]
  public class EnrollmentController : ControllerBase
  {
    private IEnrollment _enrollment;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AppSettings _appSettings;
    private IMapper _mapper;
    public EnrollmentController(IEnrollment enrollment, IMapper mapper, IHttpClientFactory httpClientFactory, IOptions<AppSettings> appSettings)
    {
      _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
      _enrollment = enrollment;
      _httpClientFactory = httpClientFactory;
      _appSettings = appSettings.Value;
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
        if (result != null)
        {
          HttpClientHandler clientHandler = new HttpClientHandler();
          clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

          using (var client = new HttpClient(clientHandler))
          {
            string token = Request.Headers["Authorization"];
            string[] tokenWords = token.Split(' ');
            var payment = new PaymentInput
            {
              CourseId = result.CourseId,
              EnrollmentId = result.EnrollmentId,
              StudentId = result.StudentId
            };
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokenWords[1]);
            var json = JsonSerializer.Serialize(payment);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(_appSettings.PaymentUrl + "/api/p/Payment", data);
            response.EnsureSuccessStatusCode();
          }

        }
        return Ok(_mapper.Map<EnrollmentInput>(result));
      }
      catch (System.Exception ex)
      {
        Console.WriteLine(ex);
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
