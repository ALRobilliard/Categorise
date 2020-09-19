using System.ComponentModel.DataAnnotations;

namespace CategoriseApi.Dtos
{
    /// <summary>
    /// Data transfer object for User Authentication.
    /// </summary>
    public class UserAuthDto
    {
        /// <summary>
        /// Gets or sets the User email address.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the User password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }

    /// <summary>
    /// Data transfer object for User Registration.
    /// </summary>
    public class UserRegisterDto
    {
        /// <summary>
        /// Gets or sets the User first name.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the User last name.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the User email address.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the User password.
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}