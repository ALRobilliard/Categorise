using System;
using System.Collections.Generic;
using System.Linq;
using CategoriseApi.Helpers;
using CategoriseApi.Models;

namespace CategoriseApi.Services
{
    /// <summary>
    /// Service for exposing common actions for User.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Authenticates a user using email and password.
        /// </summary>
        User Authenticate(string email, string password);

        /// <summary>
        /// Retrieve all users.
        /// </summary>    
        IEnumerable<User> GetUsers();

        /// <summary>
        /// Retrieve a single user by user unique identifier.
        /// </summary>
        User GetUserById(Guid id);

        /// <summary>
        /// Retrieve a single user by user email.
        /// </summary>
        User GetUserByEmail(string email);

        /// <summary>
        /// Creates a single user.
        /// </summary>
        User CreateUser(User user, string password);

        /// <summary>
        /// Updates a single user.
        /// </summary>
        void UpdateUser(User user, string password = null);

        /// <summary>
        /// Deletes a single user.
        /// </summary>
        void DeleteUser(Guid id);
    }

    /// <summary>
    /// Service for exposing common actions for User.
    /// </summary>
    public class UserService : IUserService
    {
        private CategoriseContext _context;
        private ConfigSettingService _configSettingService;

        /// <summary>
        /// Constructor for UserService.
        /// </summary>
        public UserService(CategoriseContext context)
        {
            _context = context;
            _configSettingService = new ConfigSettingService(context);
        }

        /// <summary>
        /// Authenticates a user using email and password.
        /// </summary>
        public User Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = _context.Users.SingleOrDefault(u => u.Email == email);

            // Check if user email exists.
            if (user == null)
            {
                return null;
            }

            // Check if password is correct.
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            // Authentication successful.
            return user;
        }

        /// <summary>
        /// Retrieve all users.
        /// </summary>    
        public IEnumerable<User> GetUsers()
        {
            return _context.Users;
        }

        /// <summary>
        /// Retrieve a single user by user unique identifier.
        /// </summary>
        public User GetUserById(Guid id)
        {
            return _context.Users.Find(id);
        }

        /// <summary>
        /// Retrieve a single user by user email.
        /// </summary>
        public User GetUserByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).Single();
        }

        /// <summary>
        /// Creates a single user.
        /// </summary>
        public User CreateUser(User user, string password)
        {
            // Check global ConfigSetting.
            var allowRegistrations = _configSettingService.GetConfigSettingByName("AllowRegistrations");
            if (allowRegistrations == null || allowRegistrations.Value != "true")
            {
                throw new AppException("We are not currently accepting new users.");
            }

            // Validation
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password is required.");
            }

            if (_context.Users.Any(u => u.Email == user.Email))
            {
                throw new AppException($"Email \"{user.Email}\" is already in use by another account.");
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        /// <summary>
        /// Updates a single user.
        /// </summary>
        public void UpdateUser(User userParam, string password = null)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
            {
                throw new AppException("User not found.");
            }

            if (userParam.Email != user.Email)
            {
                // Email has changed so check if the new email is available.
                if (_context.Users.Any(u => u.Email == userParam.Email))
                {
                    throw new AppException($"Email \"{user.Email}\" is already in use by another account.");
                }
            }

            // Update user properties.
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Email = userParam.Email;

            // Update password if it was entered.
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        /// <summary>
        /// Deletes a single user.
        /// </summary>
        public void DeleteUser(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}