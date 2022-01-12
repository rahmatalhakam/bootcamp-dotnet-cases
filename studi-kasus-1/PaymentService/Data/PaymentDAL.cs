using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PaymentService.Models;

namespace PaymentService.Data
{
  public class PaymentDAL : IPayment<Payment>
  {
    private AppDbContext _db;
    public PaymentDAL(AppDbContext db)
    {
      _db = db;
    }

    public Task<Payment> GetById(int id)
    {
      throw new NotImplementedException();
    }

    public Task<Payment> Insert(Payment obj)
    {
      throw new NotImplementedException();
    }
  }
}
