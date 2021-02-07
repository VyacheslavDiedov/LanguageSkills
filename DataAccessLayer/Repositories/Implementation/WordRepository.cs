using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class WordRepository : ICrudRepository<Word>
    {
        public List<Word> GetAll()
        {
            using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
            {
                return db.Words.ToList();
            }
        }

        public Word Get(int id)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    return db.Words.Find(id);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new Word();
            }
        }

        public void CreateRange(List<Word> words)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Words.AddRange(words);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void Update(Word word)
        {
            try
            {
                using (LanguageSkillsDBContext db = new LanguageSkillsDBContext())
                {
                    db.Entry(word).State = EntityState.Modified;
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
                    db.Words.Remove(db.Words.Find(id));
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
