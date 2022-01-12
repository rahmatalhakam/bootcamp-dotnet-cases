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

    public async Task<IEnumerable<Payment>> GetAll()
    {
      var result = await _db.Payments.AsNoTracking().ToListAsync();
      return result;
    }

    public async Task<Payment> GetById(int id)
    {
      var result = await _db.Payments.Where(e => e.PaymentId == id).AsNoTracking().SingleAsync();
      return result;
    }

    public async Task<Payment> Insert(Payment obj)
    {
      try
      {
        var result = await _db.Payments.AddAsync(obj);
        await _db.SaveChangesAsync();
        return result.Entity;
      }
      catch (System.Exception ex)
      {
        throw new Exception($"Error: {ex.Message}");
      }
    }
  }
}
