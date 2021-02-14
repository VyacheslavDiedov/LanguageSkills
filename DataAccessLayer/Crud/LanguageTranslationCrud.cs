using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataAccessLayer.DataBaseModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Crud
{
    public class LanguageTranslationCrud
    {
        /// <summary>
        /// Get all language translations from table DB
        /// </summary>
        /// <returns>List of language translations</returns>
        public List<LanguageTranslation> GetAll()
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.LanguageTranslations.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                return new List<LanguageTranslation>();
            }
        }

        /// <summary>
        /// Get language translation by Id from table DB
        /// </summary>
        /// <param name="id">Language translation Id</param>
        /// <returns>Language translation</returns>
        public LanguageTranslation GetById(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.LanguageTranslations.Find(id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                return new LanguageTranslation();
            }
        }

        /// <summary>
        /// Add new range of language translations and save
        /// </summary>
        /// <param name="languageTranslations">List of language translations</param>
        public void AddRange(List<LanguageTranslation> languageTranslations)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.LanguageTranslations.AddRange(languageTranslations);
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
        /// Update the language translation and save
        /// </summary>
        /// <param name="languageTranslation">Language translation to update</param>
        public void Update(LanguageTranslation languageTranslation)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Entry(languageTranslation).State = EntityState.Modified;
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
        /// Delete language translation by Id from table DB
        /// </summary>
        /// <param name="id">Language translation Id to delete</param>
        public void Delete(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.LanguageTranslations.Remove(db.LanguageTranslations.Find(id));
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
