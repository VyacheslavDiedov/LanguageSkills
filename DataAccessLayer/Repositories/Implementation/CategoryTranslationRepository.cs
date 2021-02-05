using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class CategoryTranslationRepository : ICrudRepository<CategoryTranslation>
    {

        public List<CategoryTranslation> GetAll()
        {
            using LanguageSkillsDBContext db = new LanguageSkillsDBContext();
            return db.CategoryTranslations.ToList();
        }

        public CategoryTranslation Get(int id)
        {
            try
            {
                using LanguageSkillsDBContext db = new LanguageSkillsDBContext();
                return db.CategoryTranslations.Find(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new CategoryTranslation();
            }
        }

        public void CreateRange(List<CategoryTranslation> categoryTranslations)
        {
            try
            {
                using LanguageSkillsDBContext db = new LanguageSkillsDBContext();
                db.CategoryTranslations.AddRange(categoryTranslations);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(CategoryTranslation categoryTranslation)
        {
            try
            {
                using LanguageSkillsDBContext db = new LanguageSkillsDBContext();
                db.Entry(categoryTranslation).State = EntityState.Modified;
                db.SaveChanges();
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
                using LanguageSkillsDBContext db = new LanguageSkillsDBContext();
                db.CategoryTranslations.Remove(db.CategoryTranslations.Find(id));
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
