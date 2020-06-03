using System;
using System.Collections.Generic;
using System.Linq;
using CategoriseApi.Helpers;
using CategoriseApi.Models;

namespace CategoriseApi.Services
{
  public interface IUserService
  {
    User Authenticate (string email, string password);
    IEnumerable<User> GetUsers();
    User GetUserById(Guid id);
    User GetUserByEmail(string email);
    User CreateUser(User user, string password);
    void UpdateUser(User user, string password = null);
    void DeleteUser(Guid id);
  }

  public class UserService : IUserService
  {
    private CategoriseContext _context;

    public UserService(CategoriseContext context)
    {
      _context = context;
    }

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

    public IEnumerable<User> GetUsers()
    {
      return _context.Users;
    }

    public User GetUserById(Guid id)
    {
      return _context.Users.Find(id);
    }

    public User GetUserByEmail(string email)
    {
      return _context.Users.Where(u => u.Email == email).Single();
    }

    public User CreateUser(User user, string password)
    {
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