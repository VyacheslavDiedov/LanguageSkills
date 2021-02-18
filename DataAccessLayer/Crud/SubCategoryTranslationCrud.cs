using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Crud
{
    public class SubCategoryTranslationCrud 
    {
        /// <summary>
        /// Get all subCategory translations from table DB
        /// </summary>
        /// <returns>List of subCategory translations</returns>
        public List<SubCategoryTranslation> GetAll()
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.SubCategoryTranslations.ToList();
                }
            }
            catch (Exception e)
            {
                HandleExceptions.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new List<SubCategoryTranslation>();
            }
        }

        /// <summary>
        /// Get subCategory translation by Id from table DB
        /// </summary>
        /// <param name="id">SubCategory translation Id</param>
        /// <returns>SubCategory translation</returns>
        public SubCategoryTranslation GetById(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.SubCategoryTranslations.FirstOrDefault(s => s.Id == id);
                }
            }
            catch (Exception e)
            {
                HandleExceptions.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new SubCategoryTranslation();
            }
        }

        /// <summary>
        /// Add new range of subCategory translations and save
        /// </summary>
        /// <param name="subCategoryTranslations">List of subCategory translations</param>
        public void AddRange(List<SubCategoryTranslation> subCategoryTranslations)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.SubCategoryTranslations.AddRange(subCategoryTranslations);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                HandleExceptions.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
            }
        }

        /// <summary>
        /// Update the subCategory translation and save
        /// </summary>
        /// <param name="subCategoryTranslation">SubCategory translations to update</param>
        public void Update(SubCategoryTranslation subCategoryTranslation)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Entry(subCategoryTranslation).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                HandleExceptions.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
            }
        }

        /// <summary>
        /// Delete subCategory translation by Id from table DB
        /// </summary>
        /// <param name="id">SubCategory translation Id to delete</param>
        public void Delete(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    SubCategoryTranslation subCategoryTranslation = db.SubCategoryTranslations.FirstOrDefault(s => s.Id == id);
                    if (subCategoryTranslation != null)
                    {
                        db.SubCategoryTranslations.Remove(subCategoryTranslation);
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
