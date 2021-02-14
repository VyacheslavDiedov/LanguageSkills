using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataAccessLayer.DataBaseModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Crud
{
    /// <summary>
    /// Get all languages from table DB
    /// </summary>
    /// <returns>List of languages</returns>
    public class LanguageCrud 
    {
        public List<Language> GetAll()
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.Languages.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                return new List<Language>();
            }
        }

        /// <summary>
        /// Get language by Id from table DB
        /// </summary>
        /// <param name="id">Language Id</param>
        /// <returns>Language</returns>
        public Language GetById(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.Languages.Find(id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                return new Language();
            }
        }

        /// <summary>
        /// Add new range of languages and save
        /// </summary>
        /// <param name="languages">Language of items</param>
        public void AddRange(List<Language> languages)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Languages.AddRange(languages);
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
        /// Update the language and save
        /// </summary>
        /// <param name="language">Language to update</param>
        public void Update(Language language)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Entry(language).State = EntityState.Modified;
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
        /// Delete language by Id from table DB
        /// </summary>
        /// <param name="id">Language Id to delete</param>
        public void Delete(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Languages.Remove(db.Languages.Find(id));
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
