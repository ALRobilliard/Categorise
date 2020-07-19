using System;
using System.Linq;
using System.Collections.Generic;
using CategoriseApi.Models;

namespace CategoriseApi.Services
{
  public interface ICategoryService
  {
    IEnumerable<Category> GetCategories(Guid userId);
    Category GetCategory(Guid id, Guid userId);
    Category CreateCategory(Category category, Guid userId);
    bool UpdateCategory(Category category, Guid userId);
    bool DeleteCategory(Guid id, Guid userId);
    IEnumerable<Category> SearchCategories(string categoryName, Guid userId);
  }

  public class CategoryService : ICategoryService
  {
    private CategoriseContext _context;

    public CategoryService(CategoriseContext context)
    {
      _context = context;
    }

    public IEnumerable<Category> GetCategories(Guid userId)
    {
      return _context.Categories.Where(c => c.UserId == userId).ToList();
    }

    public Category GetCategory(Guid id, Guid userId)
    {
      return _context.Categories
        .Where(c => c.Id == id && c.UserId == userId).FirstOrDefault();
    }

    public Category CreateCategory(Category category, Guid userId)
    {
      category.UserId = userId;
      _context.Categories.Add(category);
      _context.SaveChanges();
      
      return category;
    }

    public bool UpdateCategory(Category categoryParam, Guid userId)
    {
      Category category = _context.Categories.Find(categoryParam.Id);

      if (category != null && category.UserId == userId)
      {
        // Update category properties.
        category.CategoryName = categoryParam.CategoryName;

        _context.Categories.Update(category);
        _context.SaveChanges();
        return true;
      }
      return false;
    }

    public bool DeleteCategory(Guid id, Guid userId)
    {
      var category = _context.Categories.Find(id);
      if (category != null && category.UserId == userId)
      {
        _context.Categories.Remove(category);
        _context.SaveChanges();

        return true;
      }
      return false;
    }

    public IEnumerable<Category> SearchCategories(string categoryName, Guid userId)
    {
      if (string.IsNullOrEmpty(categoryName))
      {
        return new List<Category>();
      }

      List<Category> categories = _context.Categories
        .Where(c => c.CategoryName.StartsWith(categoryName) && c.UserId == userId).ToList();
      return categories;
    }
  }
}