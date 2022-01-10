using System;
using HotChocolate;

namespace GraphQLAuth.Helper
{
  public class GraphQLErrorFilter : IErrorFilter
  {
    public IError OnError(IError error)
    {
      if (error.Exception is UserNotFoundException ex)
        return error.WithMessage($"Invalid username or password.");
      if (error.Exception is DuplicateUsername e)
        return error.WithMessage($"Username already used.");
      if (error.Exception is DataNotFound dnf)
        return error.WithMessage($"Data tidak ditemukan");
      return error;
    }

  }

  public class UserNotFoundException : Exception
  {
    public int BookId { get; internal set; }
  }

  public class DuplicateUsername : Exception { }

  public class DataNotFound : Exception { }
}