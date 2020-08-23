using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CategoriseApi.Dtos;
using CategoriseApi.Models;
using CategoriseApi.Services;
using CategoriseApi.Extensions;

namespace CategoriseApi.Controllers
{
  /// <summary>
  /// This controller exposes CRUD actions for the Category table.
  /// </summary>
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class CategoriesController : ControllerBase
  {
    private CategoriseContext _context;
    private CategoryService _categoryService;
    private IMapper _mapper;

    /// <summary>
    /// Constructor for the CategoriesController.
    /// </summary>
    public CategoriesController(CategoriseContext context, IMapper mapper)
    {
      _context = context;
      _categoryService = new CategoryService(context);
      _mapper = mapper;
    }

    /// <summary>
    /// Returns all categories for the authenticated user.
    /// </summary>
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

    /// <summary>
    /// Returns the specified category for the authenticated user.
    /// </summary>
    /// <param name="id">Unique identifier of the requested category.</param>
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

    /// <summary>
    /// Creates a category owned by the authenticated user.
    /// </summary>
    /// <param name="categoryDto">Data transfer object for the category to be created.</param>
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

    /// <summary>
    /// Updates a category owned by the authenticated user.
    /// </summary>
    /// <param name="categoryId">Unique identifier of the requested category.</param>
    /// <param name="categoryParam">Category model to be updated.</param>
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

    /// <summary>
    /// Deletes a category owned by the authenticated user.
    /// </summary>
    /// <param name="id">Unique identifier of the requested category.</param>
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

    /// <summary>
    /// Returns a list of categories for the authenticated user, based on the search parameter.
    /// </summary>
    /// <param name="categoryName">Category search term.</param>
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