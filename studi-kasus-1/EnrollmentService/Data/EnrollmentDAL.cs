using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnrollmentService.Data;
using EnrollmentService.Models;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentService.Data
{
  public class EnrollmentDAL : IEnrollment
  {
    private AppDbContext _db;
    public EnrollmentDAL(AppDbContext db)
    {
      _db = db;
    }

    public Task Delete(string id)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<Enrollment>> GetAll()
    {
      var results = await _db.Enrollments.Include(e => e.Course).Include(e => e.Student).AsNoTracking().ToListAsync();
      return results;
    }

    public Task<Enrollment> GetById(string id)
    {
      throw new NotImplementedException();
    }

    public Task<Enrollment> Insert(Enrollment obj)
    {
      throw new NotImplementedException();
    }

    public Task<Enrollment> Update(string id, Enrollment obj)
    {
      throw new NotImplementedException();
    }
  }
}
