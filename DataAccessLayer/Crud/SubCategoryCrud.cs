﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DataAccessLayer.DataBaseModels;
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
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
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
                    return db.SubCategories.Find(id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
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
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
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
                Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                  $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
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
                    db.SubCategories.Remove(db.SubCategories.Find(id));
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
