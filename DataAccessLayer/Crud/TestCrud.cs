using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Helpers;
using DataAccessLayer.ViewModels.Pagination;
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
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new List<Test>();
            }
        }

        /// <summary>
        /// Get all tests with translations from table DB
        /// </summary>
        /// <returns>List of tests with translations</returns>
        public List<Test> GetAllTestWithTranslations()
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.Tests.Include(l => l.TestTranslations).ToList();
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new List<Test>();
            }
        }

        /// <summary>
        /// Get the page of tests with translations
        /// </summary>
        /// <param name="pageNumber">Number of page</param>
        /// <param name="pageSize">Size of page</param>
        /// <returns>List of tests with translations and info about pagination</returns>
        public PagedResult<Test> GetPagedTestsWithTranslations(int pageNumber, int pageSize)
        {
            PaginationFilter<Test> paginationFilter = new PaginationFilter<Test>();
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    List<Test> testsWithTranslation =
                        db.Tests.Include(l => l.TestTranslations).ToList();
                    return paginationFilter.GetPagedItems(pageNumber, pageSize, testsWithTranslation);
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new PagedResult<Test>();
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
                    return db.Tests.FirstOrDefault(t => t.Id == id);
                }
            }
            catch (Exception e)
            {
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
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
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
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
                ExceptionHandler.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
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
                    Test test = db.Tests.FirstOrDefault(t => t.Id == id);
                    if (test != null)
                    {
                        db.Tests.Remove(test);
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
