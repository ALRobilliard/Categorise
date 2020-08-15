namespace CategoriseApi.Dtos
{
  /// <summary>
  /// Data transfer unit for User Authentication.
  /// </summary>
  public class UserAuthDto
  {
    /// <summary>
    /// Gets or sets the User email address.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the User password.
    /// </summary>
    public string Password { get; set; }
  }

  /// <summary>
  /// Data transfer unit for User Registration.
  /// </summary>
  public class UserRegisterDto
  {
    /// <summary>
    /// Gets or sets the User first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the User last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the User email address.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the User password.
    /// </summary>
    public string Password { get; set; }
  }
}