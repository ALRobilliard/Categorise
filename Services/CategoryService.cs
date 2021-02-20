using System;
using System.Linq;
using System.Collections.Generic;
using Categorise.Data;

namespace Categorise.Services
{
    /// <summary>
    /// Service for exposing common actions for Category.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Retrieve all categories for the specified user.
        /// </summary>
        IEnumerable<Category> GetCategories(string userId);

        /// <summary>
        /// Retrieve a single category for the specified user by category unique identifier.
        /// </summary>
        Category GetCategory(Guid id, string userId);

        /// <summary>
        /// Creates a single category for the specified user.
        /// </summary>
        Category CreateCategory(Category category, string userId);

        /// <summary>
        /// Updates a category owned by the specified user.
        /// </summary>
        bool UpdateCategory(Category category, string userId);

        /// <summary>
        /// Deletes a category owned by the specified user.
        /// </summary>
        bool DeleteCategory(Guid id, string userId);

        /// <summary>
        /// Returns a list of categories for the authenticated user, based on the search parameter.
        /// </summary>
        IEnumerable<Category> SearchCategories(string categoryName, string userId);
    }

    /// <summary>
    /// Service for exposing common actions for Category.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private CategoriseContext _context;

        /// <summary>
        /// Constructor for the CategoryService.
        /// </summary>
        public CategoryService(CategoriseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieve all categories for the specified user, and global categories.
        /// </summary>
        public IEnumerable<Category> GetCategories(string userId)
        {
            return _context.Categories.Where(c => c.UserId == userId || c.IsGlobal).ToList();
        }

        /// <summary>
        /// Retrieve a single category for the specified user by category unique identifier.
        /// </summary>
        public Category GetCategory(Guid id, string userId)
        {
            return _context.Categories
              .Where(c => c.Id == id && c.UserId == userId).FirstOrDefault();
        }

        /// <summary>
        /// Creates a single category for the specified user.
        /// </summary>
        public Category CreateCategory(Category category, string userId)
        {
            category.UserId = userId;
            _context.Categories.Add(category);
            _context.SaveChanges();

            return category;
        }

        /// <summary>
        /// Updates a category owned by the specified user.
        /// </summary>
        public bool UpdateCategory(Category categoryParam, string userId)
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

        /// <summary>
        /// Deletes a category owned by the specified user.
        /// </summary>
        public bool DeleteCategory(Guid id, string userId)
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

        /// <summary>
        /// Returns a list of categories for the authenticated user, based on the search parameter.
        /// </summary>
        public IEnumerable<Category> SearchCategories(string categoryName, string userId)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                return new List<Category>();
            }

            List<Category> categories = _context.Categories
              .Where(c => c.CategoryName.StartsWith(categoryName) && c.UserId == userId ||
                c.CategoryName.StartsWith(categoryName) && c.IsGlobal).ToList();
            return categories;
        }
    }
}