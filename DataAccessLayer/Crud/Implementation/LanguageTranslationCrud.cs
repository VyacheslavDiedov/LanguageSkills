using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Crud.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Crud.Implementation
{
    public class LanguageTranslationCrud : ICrudDictionary<LanguageTranslation>
    {
        public List<LanguageTranslation> GetAll()
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.LanguageTranslations.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<LanguageTranslation>();
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
