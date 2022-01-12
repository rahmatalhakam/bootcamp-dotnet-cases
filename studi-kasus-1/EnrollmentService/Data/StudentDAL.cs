using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentService.Models;
using Microsoft.EntityFrameworkCore;

namespace EnrollmentService.Data
{
  public class StudentDAL : IStudent
  {
    private AppDbContext _db;
    public StudentDAL(AppDbContext db)
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
        _db.Students.Remove(result);
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateException ex)
      {
        throw new Exception($"Error: {ex.Message}");
      }

    }

    public async Task<IEnumerable<Student>> GetAll()
    {
      var results = await _db.Students.Include(s => s.Enrollment).ToListAsync();
      return results;
    }

    public async Task<IEnumerable<Student>> GetByCourseID(int id)
    {
      var query = from student in _db.Students
                  join enrollment in _db.Enrollments on student.Id equals enrollment.StudentId
                  join course in _db.Courses on enrollment.CourseId equals course.CourseId
                  where course.CourseId == id
                  select student;
      var results = await query.ToListAsync();
      return results;
    }

    public async Task<Student> GetById(string id)
    {
      var results = await _db.Students.Where(s => s.Id == Convert.ToInt32(id)).SingleOrDefaultAsync();
      if (results != null)
        return results;
      else
        throw new Exception("Data tidak ditemukan.");
    }

    public async Task<Student> Insert(Student obj)
    {
      try
      {
        _db.Students.Add(obj);
        await _db.SaveChangesAsync();
        return obj;
      }
      catch (DbUpdateException ex)
      {
        throw new Exception($"Error: {ex.Message}");
      }
    }

    public async Task<Student> Update(string id, Student obj)
    {
      try
      {
        var result = await GetById(id);
        result.FirstName = obj.FirstName;
        result.LastName = obj.LastName;
        result.EnrollmentDate = obj.EnrollmentDate;
        await _db.SaveChangesAsync();
        obj.Id = result.Id;
        return obj;

      }
      catch (DbUpdateException ex)
      {
        throw new Exception($"Error: {ex.Message}");
      }
    }
  }
}
