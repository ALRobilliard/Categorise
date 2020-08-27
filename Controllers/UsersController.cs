using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using CategoriseApi.Dtos;
using CategoriseApi.Helpers;
using CategoriseApi.Models;
using CategoriseApi.Services;

namespace CategoriseApi.Controllers
{
  /// <summary>
  /// This controller exposes management actions for the Users table.
  /// </summary>
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class UsersController : ControllerBase
  {
    private IUserService _userService;
    private IConfiguration _config;
    private IMapper _mapper;

    /// <summary>
    /// Constructor for the UsersController.
    /// </summary>
    public UsersController(IUserService userService, IConfiguration configuration, IMapper mapper)
    {
      _userService = userService;
      _config = configuration;
      _mapper = mapper;
    }

    /// <summary>
    /// Endpoint for user authentication. When passed valid parameters, returns a JWT token for the user.
    /// </summary>
    /// <param name="userDto">Data transfer object for the user to authenticate.</param>
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody]UserAuthDto userDto)
    {
      var user = _userService.Authenticate(userDto.Email, userDto.Password);

      if (user == null) 
      {
        return BadRequest(new { message = "Email or password is incorrect."});
      }

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_config["APP_SECRET"]);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
        {
          new Claim(ClaimTypes.Name, user.Email),
          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        }),
        Expires = DateTime.Now.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      var tokenString = tokenHandler.WriteToken(token);

      // Set last login.
      user.LastLogin = DateTime.Now;
      _userService.UpdateUser(user);

      // Return basic user info (without password) and token to store client side.
      return  Ok(new
      {
        Id = user.Id,
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Token = tokenString
      });
    }

    /// <summary>
    /// Endpoint for user registration.
    /// </summary>
    /// <param name="userDto">Data transfer object for the user to register.</param>
    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register([FromBody]UserRegisterDto userDto)
    {
      // Map dto to entity.
      var user = _mapper.Map<User>(userDto);

      try
      {
        // Save.
        _userService.CreateUser(user, userDto.Password);
        return Ok();
      }
      catch (AppException ex)
      {
        return BadRequest(new { message = ex.Message });
      }
    }
  }
}