using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataAccessLayer.DataBaseModels;
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
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
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
                    return db.SubCategoryTranslations.Find(id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                return new SubCategoryTranslation();
            }
        }

        /// <summary>
        /// Add new range of subCategory translations and save
        /// </summary>
        /// <param name="items">List of subCategory translations</param>
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
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
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
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
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
                    db.SubCategoryTranslations.Remove(db.SubCategoryTranslations.Find(id));
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
            }
        }
    }
}
