using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Crud
{
    public class SubCategoryCrud
    {
        /// <summary>
        /// Get all subCategories from table DB
        /// </summary>
        /// <returns>List of subCategories</returns>
        public List<SubCategory> GetAll()
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.SubCategories.ToList();
                }
            }
            catch (Exception e)
            {
                HandleExceptions.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new List<SubCategory>();
            }
        }

        /// <summary>
        /// Get subCategory by Id from table DB
        /// </summary>
        /// <param name="id">SubCategory Id</param>
        /// <returns>SubCategory</returns>
        public SubCategory GetById(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.SubCategories.FirstOrDefault(s => s.Id == id);
                }
            }
            catch (Exception e)
            {
                HandleExceptions.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
                return new SubCategory();
            }
        }

        /// <summary>
        /// Add new range of subCategories and save
        /// </summary>
        /// <param name="subCategories">List of subCategories</param>
        public void AddRange(List<SubCategory> subCategories)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.SubCategories.AddRange(subCategories);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                HandleExceptions.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
            }
        }

        /// <summary>
        /// Update the subCategory and save
        /// </summary>
        /// <param name="subCategory">SubCategory to update</param>
        public void Update(SubCategory subCategory)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Entry(subCategory).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                HandleExceptions.ShowInConsole(this.GetType().Name, MethodBase.GetCurrentMethod().Name, e);
            }
        }

        /// <summary>
        /// Delete subCategory by Id from table DB
        /// </summary>
        /// <param name="id">SubCategory Id to delete</param>
        public void Delete(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    SubCategory subCategory = db.SubCategories.FirstOrDefault(s => s.Id == id);
                    if (subCategory != null)
                    {
                        db.SubCategories.Remove(subCategory);
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
