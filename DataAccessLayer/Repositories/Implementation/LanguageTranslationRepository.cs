using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class LanguageTranslationRepository : ICrudRepository<LanguageTranslation>
    {
        public List<LanguageTranslation> GetAll()
        {
            using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
            {
                return db.LanguageTranslations.ToList();
            }
        }

        public LanguageTranslation Get(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.LanguageTranslations.Find(id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new LanguageTranslation();
            }
        }

        public void CreateRange(List<LanguageTranslation> languageTranslations)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.LanguageTranslations.AddRange(languageTranslations);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(LanguageTranslation languageTranslation)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Entry(languageTranslation).State = EntityState.Modified;
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
                    db.LanguageTranslations.Remove(db.LanguageTranslations.Find(id));
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
