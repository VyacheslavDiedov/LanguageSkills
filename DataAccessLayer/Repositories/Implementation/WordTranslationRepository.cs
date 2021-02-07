using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class WordTranslationRepository : ICrudRepository<WordTranslation>
    {
        public List<WordTranslation> GetAll()
        {
            using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
            {
                return db.WordTranslations.ToList();
            }
        }

        public WordTranslation Get(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.WordTranslations.Find(id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new WordTranslation();
            }
            
        }

        public void CreateRange(List<WordTranslation> wordTranslations)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.WordTranslations.AddRange(wordTranslations);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(WordTranslation wordTranslation)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Entry(wordTranslation).State = EntityState.Modified;
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
                    db.WordTranslations.Remove(db.WordTranslations.Find(id));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
