using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AuthService.Dtos;
using AuthService.Models;
using Microsoft.AspNetCore.Identity;
namespace AuthService.Data
{
  public interface IUser
  {
    IEnumerable<UsernameOutput> GetAllUser();
    Task Registration(RegisterInput user);
    Task AddRole(string rolename);
    IEnumerable<RoleOutput> GetAllRole();
    Task AddRoleForUser(UserRole input);
    Task<List<string>> GetRolesFromUser(string username);
    Task<User> Authenticate(string username, string password);
  }
}
