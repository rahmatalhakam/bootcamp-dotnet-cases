using System;
using System.Linq;
using EnrollmentService.Models;

namespace EnrollmentService.Data
{
  public static class DbInitilizer
  {
    public static void Initilize(AppDbContext context)
    {
      context.Database.EnsureCreated();
      if (context.Students.Any())
      {
        return;
      }
      var res = context.Courses;
      if (res != null)
      {
        return;
      }
      var courses = new Course[]
      {
        new Course{Title="Cloud Fundamentals", Credits=3},
        new Course{Title="Microservices Architecture", Credits=3},
        new Course{Title="Frontend Programming", Credits=3},
        new Course{Title="Backend RESTful API", Credits=3},
        new Course{Title="Entity framework core", Credits=3},
      };

      foreach (var course in courses)
      {
        context.Courses.Add(course);
      }

      context.SaveChanges();
    }

  }
}
