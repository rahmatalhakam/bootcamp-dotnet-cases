using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using EnrollmentService.Data;
using EnrollmentService.Models;

namespace EnrollmentService.Data
{
  public class CourseDAL : ICourse
  {
    private AppDbContext _db;

    public CourseDAL(AppDbContext db)
    {
      _db = db;
    }

    public async Task Delete(string id)
    {
      try
      {
        var result = await GetById(id);
        if (result == null)
          throw new Exception($"Data id={id} tidak ditemukan");
        _db.Courses.Remove(result);
        await _db.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        throw new Exception($"Error: {ex.Message}");
      }
    }

    public async Task<IEnumerable<Course>> GetAll()
    {
      //access to database
      var results = await (from c in _db.Courses select c).AsNoTracking().ToListAsync();
      return results;
    }

    public async Task<Course> GetById(string id)
    {
      var result = await (from c in _db.Courses where c.CourseId == Convert.ToInt32(id) select c).SingleAsync();
      if (result == null)
        throw new Exception("Data tidak ditemukan");
      return result;
    }

    public async Task<IEnumerable<Course>> GetByTitle(string title)
    {
      var results = await _db.Courses.Where(c => c.Title.Contains(title.ToLower())).ToListAsync();
      if (results == null)
        throw new Exception("Data tidak ditemukan");
      return results;
    }

    public async Task<IEnumerable<Course>> GetByStudentID(int id)
    {
      var query = from course in _db.Courses
                  join enrollment in _db.Enrollments on course.CourseId equals enrollment.CourseId
                  join student in _db.Students on enrollment.StudentId equals student.Id
                  where student.Id == id
                  select course;
      var results = await query.ToListAsync();
      return results;
    }

    public async Task<Course> Insert(Course obj)
    {
      try
      {
        _db.Courses.Add(obj);
        await _db.SaveChangesAsync();
        return obj;

      }
      catch (Exception ex)
      {
        throw new Exception($"Error: {ex.Message}");
      }

    }

    public async Task<Course> Update(string id, Course obj)
    {
      try
      {
        var result = await GetById(id);
        if (result == null)
          throw new Exception($"Data id={id} tidak ditemukan");
        result.Title = obj.Title;
        result.Credits = obj.Credits;
        await _db.SaveChangesAsync();
        return result;
      }
      catch (Exception ex)
      {
        throw new Exception($"Error: {ex.Message}");
      }
    }



  }
}
