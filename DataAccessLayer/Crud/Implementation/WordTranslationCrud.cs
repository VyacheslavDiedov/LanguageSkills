using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Crud.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Crud.Implementation
{
    public class WordTranslationCrud : ICrudDictionary<WordTranslation>
    {
        public List<WordTranslation> GetAll()
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.WordTranslations.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<WordTranslation>();
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
