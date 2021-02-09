using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Crud.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Crud.Implementation
{
    public class CategoryCrud : ICrudDictionary<Category>
    {
        public List<Category> GetAll()
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.Categories.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<Category>();
            }
        }
            

        public Category Get(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.Categories.Find(id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Category();
            }
        }

        public void CreateRange(List<Category> categories)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Categories.AddRange(categories);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(Category category)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Entry(category).State = EntityState.Modified;
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
                    db.Categories.Remove(db.Categories.Find(id));
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
