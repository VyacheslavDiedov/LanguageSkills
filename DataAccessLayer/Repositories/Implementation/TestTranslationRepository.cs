using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class TestTranslationRepository : ICrudRepository<TestTranslation>
    {
        public List<TestTranslation> GetAll()
        {
            using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
            {
                return db.TestTranslations.ToList();
            }
        }

        public TestTranslation Get(int id)
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
                Console.WriteLine(e);
                return new TestTranslation();
            }
        }

        public void CreateRange(List<TestTranslation> testTranslations)
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
                Console.WriteLine(e);
            }
        }

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
                Console.WriteLine(e);
            }
        }

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
                Console.WriteLine(e);
            }
        }
    }
}
