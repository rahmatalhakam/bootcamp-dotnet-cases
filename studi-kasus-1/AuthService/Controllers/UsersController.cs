using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Data;
using AuthService.Dtos;
using AuthService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
  [ApiController]
  [Route("api/a/[controller]")]
  public class UsersController : ControllerBase
  {
    private IUser _user;

    public UsersController(IUser user)
    {
      _user = user;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Registration([FromBody] RegisterInput user)
    {
      try
      {
        await _user.Registration(user);
        return Ok($"Registrasi user {user.Username} berhasil");
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet]
    public ActionResult<UsernameOutput> GetAll()
    {
      try
      {
        var results = _user.GetAllUser();
        return Ok(results);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("Role")]
    public async Task<ActionResult> AddRole([FromBody] RoleOutput rolename)
    {
      try
      {
        await _user.AddRole(rolename.Rolename);
        return Ok($"Role {rolename.Rolename} berhasil ditambahkan");
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet("Role")]
    public ActionResult<IEnumerable<RoleOutput>> GetAllRole()
    {
      try
      {
        return Ok(_user.GetAllRole());
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("UserInRole")]
    public async Task<ActionResult> AddRoleForUser(UserRole input)
    {
      try
      {
        await _user.AddRoleForUser(input);
        return Ok($"Data {input.Username} dan {input.Rolename} berhaisl ditambahkan");
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpGet("/{username}/Role")]
    public async Task<ActionResult<List<string>>> GetRolesFromUser(string username)
    {
      try
      {
        var results = await _user.GetRolesFromUser(username);
        return Ok(results);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [AllowAnonymous]
    [HttpPost("Authentication")]
    public async Task<ActionResult<User>> Authentication(LoginInput input)
    {
      try
      {
        var user = await _user.Authenticate(input.Username, input.Password);
        if (user == null) return BadRequest("username/password tidak tepat");
        return Ok(user);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);

      }
    }
  }
}