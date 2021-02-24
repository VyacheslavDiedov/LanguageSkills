using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Helpers;
using DataAccessLayer.ViewModels.Pagination;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Crud
{
    public class CategoryCrud 
    {
        /// <summary>
        /// Get all categories from table DB
        /// </summary>
        /// <returns>List of categories</returns>
        public List<Category> GetAll()
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.Categories.ToList();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new List<Category>();
            }
        }

        /// <summary>
        /// Get the page of categories with translations
        /// </summary>
        /// <param name="pageNumber">Number of page</param>
        /// <param name="pageSize">Size of page</param>
        /// <returns>List categories with translations and info about pagination</returns>
        public PagedResult<Category> GetPagedCategoriesWithTranslations(int pageNumber, int pageSize)
        {
            PaginationFilter<Category> paginationFilter = new PaginationFilter<Category>();
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    List<Category> categoriesWithTranslation =
                        db.Categories.Include(c => c.CategoryTranslations).ToList();
                    return paginationFilter.GetPagedItems(pageNumber, pageSize, categoriesWithTranslation);
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new PagedResult<Category>();
            }
        }

        /// <summary>
        /// Get category by Id from table DB
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns>Category</returns>
        public Category GetById(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.Categories.FirstOrDefault(c => c.Id == id);
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new Category();
            }
        }

        /// <summary>
        /// Add new range of categories and save
        /// </summary>
        /// <param name="categories">List of categories</param>
        public void AddRange(List<Category> categories)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Categories.AddRange(categories);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
            }
        }

        /// <summary>
        /// Update the category and save
        /// </summary>
        /// <param name="category">Category to update</param>
        public void Update(Category category)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Entry(category).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
            }
        }

        /// <summary>
        /// Delete category by Id from table DB
        /// </summary>
        /// <param name="id">Category Id to delete</param>
        public void Delete(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    Category category = db.Categories.FirstOrDefault(c => c.Id == id);
                    if (category != null)
                    {
                        db.Categories.Remove(category);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
            }
        }
    }
}
