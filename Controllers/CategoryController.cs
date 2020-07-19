using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Linq;
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
using CategoriseApi.Extensions;

namespace CategoriseApi.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class CategoryController : ControllerBase
  {
    private CategoriseContext _context;
    private CategoryService _categoryService;
    private IMapper _mapper;

    public CategoryController(CategoriseContext context, IMapper mapper)
    {
      _context = context;
      _categoryService = new CategoryService(context);
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
      ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
      Guid? userId = identity.GetUserId();

      if (userId.HasValue) 
      {
        return Ok(_categoryService.GetCategories(userId.Value));
      }
      return BadRequest("User ID unable to be retrieved from token.");
    }

    [HttpGet("{id}")]
    public IActionResult GetCategory(Guid id)
    {
      ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
      Guid? userId = identity.GetUserId();

      if (userId.HasValue)
      {
        Category category = _categoryService.GetCategory(id, userId.Value);
      
        if (category == null)
        {
          return NotFound();
        }
        return Ok(category);
      }
      return BadRequest("User ID unable to be retrieved from token.");
    }

    [HttpPost]
    public IActionResult CreateCategory(CategoryDto categoryDto)
    {
      ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
      Guid? userId = identity.GetUserId();

      if (userId.HasValue)
      {
        Category category = _mapper.Map<Category>(categoryDto);
        category = _categoryService.CreateCategory(category, userId.Value);
        return Ok(category);
      }
      return BadRequest("User ID unable to be retrieved from token.");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(Guid categoryId, Category categoryParam)
    {
      if (categoryId != categoryParam.Id) return BadRequest();

      ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
      Guid? userId = identity.GetUserId();

      if (userId.HasValue)
      {
        bool updateRes = _categoryService.UpdateCategory(categoryParam, userId.Value);
        if (updateRes)
        {
          return Ok();
        }
        else
        {
          return NotFound();
        }
      }
      return BadRequest("User ID unable to be retrieved from token.");
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(Guid id)
    {
      ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
      Guid? userId = identity.GetUserId();

      if (userId.HasValue) 
      {
        bool deleteRes = _categoryService.DeleteCategory(id, userId.Value);
        if (deleteRes) 
        {
          return Ok();
        }
        else
        {
          return NotFound();
        }
      }
      return BadRequest("User ID unable to be retrieved from token.");
    }

    [HttpGet]
    [Route("search")]
    public IActionResult SearchCategories(string categoryName)
    {
      ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
      Guid? userId = identity.GetUserId();
      
      if (userId.HasValue) {
        IEnumerable<Category> categories = _categoryService.SearchCategories(categoryName, userId.Value);
        return Ok(categories);
      }
      return BadRequest("User ID unable to be retrieved from token.");
    }
  }
}