using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Helpers;
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
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
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
                    return db.LanguageTranslations.FirstOrDefault(l => l.Id == id);
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
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
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
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
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
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
                    LanguageTranslation languageTranslation = db.LanguageTranslations.FirstOrDefault(l => l.Id == id);
                    if (languageTranslation != null)
                    {
                        db.LanguageTranslations.Remove(languageTranslation);
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
