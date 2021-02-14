using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataAccessLayer.DataBaseModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Crud
{
    public class TestTranslationCrud
    {
        /// <summary>
        /// Get all test translations from table DB
        /// </summary>
        /// <returns>List of test translations</returns>
        public List<TestTranslation> GetAll()
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.TestTranslations.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                return new List<TestTranslation>();
            }
        }

        /// <summary>
        /// Get test translation by Id from table DB
        /// </summary>
        /// <param name="id">Test translation Id</param>
        /// <returns>Test translation</returns>
        public TestTranslation GetById(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.TestTranslations.Find(id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                return new TestTranslation();
            }
        }

        /// <summary>
        /// Add new range of test translations and save
        /// </summary>
        /// <param name="testTranslations">List of test translations</param>
        public void AddRange(List<TestTranslation> testTranslations)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.TestTranslations.AddRange(testTranslations);
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
        /// Update the test translation and save
        /// </summary>
        /// <param name="testTranslation">Test translation to update</param>
        public void Update(TestTranslation testTranslation)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Entry(testTranslation).State = EntityState.Modified;
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
        /// Delete test translation by Id from table DB
        /// </summary>
        /// <param name="id">Test translation Id to delete</param>
        public void Delete(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.TestTranslations.Remove(db.TestTranslations.Find(id));
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
