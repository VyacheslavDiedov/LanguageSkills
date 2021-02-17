using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Crud
{
    public class WordTranslationCrud
    {
        /// <summary>
        /// Get all word translations from table DB
        /// </summary>
        /// <returns>List of word translations</returns>
        public List<WordTranslation> GetAll()
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.WordTranslations.ToList();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new List<WordTranslation>();
            }
        }

        /// <summary>
        /// Get word translation by Id from table DB
        /// </summary>
        /// <param name="id">Word translation Id</param>
        /// <returns>Word translation</returns>
        public WordTranslation GetById(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.WordTranslations.FirstOrDefault(w => w.Id == id);
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new WordTranslation();
            }
            
        }

        /// <summary>
        /// Add new range of word translations and save
        /// </summary>
        /// <param name="wordTranslations">List of word translations</param>
        public void AddRange(List<WordTranslation> wordTranslations)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.WordTranslations.AddRange(wordTranslations);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
            }
        }

        /// <summary>
        /// Update the word translation and save
        /// </summary>
        /// <param name="wordTranslation">Word translation to update</param>
        public void Update(WordTranslation wordTranslation)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Entry(wordTranslation).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
            }
        }

        /// <summary>
        /// Delete word translation by Id from table DB
        /// </summary>
        /// <param name="id">Word translation Id to delete</param>
        public void Delete(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    WordTranslation wordTranslation = db.WordTranslations.FirstOrDefault(w => w.Id == id);
                    if (wordTranslation != null)
                    {
                        db.WordTranslations.Remove(wordTranslation);
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
