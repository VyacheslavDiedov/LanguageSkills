using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class LanguageRepository : ICrudRepository<Language>
    {
        public List<Language> GetAll()
        {
            using LanguageSkillsDBContext db = new LanguageSkillsDBContext();
            return db.Languages.ToList();
        }

        public Language Get(int id)
        {
            try
            {
                using LanguageSkillsDBContext db = new LanguageSkillsDBContext();
                return db.Languages.Find(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Language();
            }
        }

        public void CreateRange(List<Language> languages)
        {
            try
            {
                using LanguageSkillsDBContext db = new LanguageSkillsDBContext();
                db.Languages.AddRange(languages);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(Language language)
        {
            try
            {
                using LanguageSkillsDBContext db = new LanguageSkillsDBContext();
                db.Entry(language).State = EntityState.Modified;
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
                db.Languages.Remove(db.Languages.Find(id));
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
