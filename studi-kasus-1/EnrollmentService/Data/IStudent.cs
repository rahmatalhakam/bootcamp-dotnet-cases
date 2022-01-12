using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnrollmentService.Models;

namespace EnrollmentService.Data
{
  public interface IStudent : ICrud<Student>
  {
    Task<IEnumerable<Student>> GetByCourseID(int id);
  }
}
