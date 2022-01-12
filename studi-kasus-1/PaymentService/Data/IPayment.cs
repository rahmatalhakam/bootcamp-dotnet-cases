
using System.Threading.Tasks;


namespace PaymentService.Data
{
  public interface IPayment<T>
  {
    Task<T> Insert(T obj);
    Task<T> GetById(int id);
  }
}
