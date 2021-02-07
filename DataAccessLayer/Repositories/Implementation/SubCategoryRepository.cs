﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class SubCategoryRepository : ICrudRepository<SubCategory>
    {
        public List<SubCategory> GetAll()
        {
            using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
            {
                return db.SubCategories.ToList();
            }
        }

        public SubCategory Get(int id)
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
                Console.WriteLine(e);
                return new SubCategory();
            }
        }

        public void CreateRange(List<SubCategory> subCategories)
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
                Console.WriteLine(e);
            }
        }

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
                Console.WriteLine(e);
            }
        }

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
                Console.WriteLine(e);
            }
        }
    }
}
