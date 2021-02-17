using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Crud
{
    public class CategoryTranslationCrud
    {
        /// <summary>
        /// Get all category translations from table DB
        /// </summary>
        /// <returns>List of category translations</returns>
        public List<CategoryTranslation> GetAll()
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.CategoryTranslations.ToList();
                }
            }
            catch (Exception e)
            {
                HandleExceptions.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new List<CategoryTranslation>();
            }
        }

        /// <summary>
        /// Get category translation by Id from table DB
        /// </summary>
        /// <param name="id">Category translation Id</param>
        /// <returns>Category translation</returns>
        public CategoryTranslation GetById(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.CategoryTranslations.FirstOrDefault(c => c.Id == id);
                }
            }
            catch (Exception e)
            {
                HandleExceptions.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new CategoryTranslation();
            }
        }

        /// <summary>
        /// Add new range of category translations and save
        /// </summary>
        /// <param name="categoryTranslations">List of category translations</param>
        public void AddRange(List<CategoryTranslation> categoryTranslations)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.CategoryTranslations.AddRange(categoryTranslations);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                HandleExceptions.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
            }
        }

        /// <summary>
        /// Update the category translation and save
        /// </summary>
        /// <param name="categoryTranslation">Category translation to update</param>
        public void Update(CategoryTranslation categoryTranslation)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Entry(categoryTranslation).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                HandleExceptions.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
            }
        }

        /// <summary>
        /// Delete category translation by Id from table DB
        /// </summary>
        /// <param name="id">Category translation Id to delete</param>
        public void Delete(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    CategoryTranslation categoryTranslation = db.CategoryTranslations.FirstOrDefault(c => c.Id == id);
                    if (categoryTranslation != null)
                    {
                        db.CategoryTranslations.Remove(categoryTranslation);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                HandleExceptions.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
            }
        }
    }
}
