using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Crud
{
    public class WordCrud
    {
        /// <summary>
        /// Get all words from table DB
        /// </summary>
        /// <returns>List of words</returns>
        public List<Word> GetAll()
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.Words.ToList();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new List<Word>();
            }
        }

        /// <summary>
        /// Get word by Id from table DB
        /// </summary>
        /// <param name="id">Word Id</param>
        /// <returns>Word</returns>
        public Word GetById(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.Words.FirstOrDefault(w => w.Id == id);
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new Word();
            }
        }

        /// <summary>
        /// Get words by SubCategoryId from table DB
        /// </summary>
        /// <param name="subCategoryId">SubCategory Id</param>
        /// <returns>List of Words</returns>
        public List<Word> GetWordsBySubCategoryId(int subCategoryId)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.Words.Where(w => w.SubCategoryId == subCategoryId).ToList();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new List<Word>();
            }
        }

        /// <summary>
        /// Add new range of words and save
        /// </summary>
        /// <param name="words">List of words</param>
        public void AddRange(List<Word> words)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Words.AddRange(words);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
            }
        }

        /// <summary>
        /// Update the word and save
        /// </summary>
        /// <param name="word">Word to update</param>
        public void Update(Word word)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Entry(word).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
            }
        }

        /// <summary>
        /// Delete word by Id from table DB
        /// </summary>
        /// <param name="id">Word Id to delete</param>
        public void Delete(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    Word word = db.Words.FirstOrDefault(w => w.Id == id);
                    if (word != null)
                    {
                        db.Words.Remove(word);
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
