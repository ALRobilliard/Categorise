using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using CategoriseApi.Dtos;
using CategoriseApi.Helpers;
using CategoriseApi.Models;
using CategoriseApi.Services;

namespace CategoriseApi.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class UsersController : ControllerBase
  {
    private IUserService _userService;
    private IConfiguration _config;
    private IMapper _mapper;

    public UsersController(IUserService userService, IConfiguration configuration, IMapper mapper)
    {
      _userService = userService;
      _config = configuration;
      _mapper = mapper;
    }

    // POST: api/users/authenticate
    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody]UserDto userDto)
    {
      var user = _userService.Authenticate(userDto.Email, userDto.Password);

      if (user == null) 
      {
        return BadRequest(new { message = "Email or password is incorrect."});
      }

      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_config["AppSecret"]);
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

    // POST: api/users/register
    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register([FromBody]UserDto userDto)
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