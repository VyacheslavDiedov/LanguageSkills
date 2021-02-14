using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataAccessLayer.DataBaseModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Crud
{
    public class TestCrud
    {
        /// <summary>
        /// Get all tests from table DB
        /// </summary>
        /// <returns>List of tests</returns>
        public List<Test> GetAll()
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.Tests.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                return new List<Test>();
            }
        }

        /// <summary>
        /// Get test by Id from table DB
        /// </summary>
        /// <param name="id">Test Id</param>
        /// <returns>Test</returns>
        public Test GetById(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.Tests.Find(id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                return new Test();
            }
        }

        /// <summary>
        /// Add new range of tests and save
        /// </summary>
        /// <param name="tests">List of tests</param>
        public void AddRange(List<Test> tests)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Tests.AddRange(tests);
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
        /// Update the test and save
        /// </summary>
        /// <param name="test">Test to update</param>
        public void Update(Test test)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Entry(test).State = EntityState.Modified;
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
        /// Delete test by Id from table DB
        /// </summary>
        /// <param name="id">Test Id to delete</param>
        public void Delete(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Tests.Remove(db.Tests.Find(id));
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
