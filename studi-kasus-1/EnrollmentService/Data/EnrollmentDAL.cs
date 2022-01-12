using System;
using System.Collections.Generic;
using System.Linq;
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

    public async Task Delete(string id)
    {
      var result = await GetById(id);
      if (result == null)
        throw new Exception("Data tidak ditemukan");
      try
      {
        _db.Enrollments.Remove(result);
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateException ex)
      {
        throw new Exception($"Error: {ex.Message}");
      }
    }

    public async Task<IEnumerable<Enrollment>> GetAll()
    {
      var results = await _db.Enrollments.Include(e => e.Course).Include(e => e.Student).AsNoTracking().ToListAsync();
      return results;
    }

    public async Task<Enrollment> GetById(string id)
    {
      var result = await _db.Enrollments.Where(e => e.EnrollmentId == Int16.Parse(id)).Include(e => e.Course).Include(e => e.Student).AsNoTracking().SingleAsync();
      return result;
    }

    public async Task<Enrollment> Insert(Enrollment obj)
    {
      try
      {
        var result = await _db.Enrollments.AddAsync(obj);
        await _db.SaveChangesAsync();
        return result.Entity;
      }
      catch (System.Exception ex)
      {

        throw new Exception($"Error: {ex.Message}");
      }
    }

    public async Task<Enrollment> Update(string id, Enrollment obj)
    {
      try
      {
        var result = await GetById(id);
        if (result == null)
          throw new Exception($"Data id={id} tidak ditemukan");

        result.CourseId = obj.CourseId;
        result.StudentId = obj.StudentId;
        _db.Enrollments.Update(result);
        await _db.SaveChangesAsync();
        return result;
      }
      catch (System.Exception ex)
      {
        throw new Exception($"Error: {ex.Message}");
      }
    }
  }
}
