
using System.Collections.Generic;
using System.Threading.Tasks;


namespace PaymentService.Data
{
  public interface IPayment<T>
  {
    Task<IEnumerable<T>> GetAll();
    Task<T> Insert(T obj);
    Task<T> GetById(int id);
  }
}
