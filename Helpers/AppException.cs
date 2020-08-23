using System;
using System.Globalization;

namespace CategoriseApi.Helpers
{
  /// <summary>
  /// Custom class for app exceptions.
  /// </summary>
  public class AppException : Exception
  {
    /// <summary>
    /// AppException constructor.
    /// </summary>
    public AppException() : base() { }

    /// <summary>
    /// AppException constructor.
    /// </summary>
    public AppException(string message) : base(message) { }

    /// <summary>
    /// AppException constructor.
    /// </summary>
    public AppException(string message, params object[] args)
      : base(String.Format(CultureInfo.CurrentCulture, message, args)) { }
  }
}