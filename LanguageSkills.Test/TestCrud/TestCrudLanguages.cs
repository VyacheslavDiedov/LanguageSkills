using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BusinessLogicLayer;
using DataAccessLayer.DataBaseModels;
using Xunit;
using Xunit.Abstractions;

namespace LanguageSkills.Test.TestCrud
{
    public class TestCrudLanguages
    {
        private readonly ManageAccessToEntity _manageAccessToEntity = new ManageAccessToEntity();

        [Fact]
        public void Task_1_AddRange_Languages()
        {
            //Arrange  
            var languages = new List<Language>();
            languages.Add(new Language()
            {
                FullName = "Test language 1",
                ShortName = "TS1"
            });
            languages.Add(new Language()
            {
                FullName = "Test language 2",
                ShortName = "TS2"
            });

            //Act  
            _manageAccessToEntity.Languages.AddRange(languages);

            //Assert  
            Debug.Assert(_manageAccessToEntity.Languages.GetAll()
                             .FirstOrDefault(l => l.FullName == "Test language 1" && l.ShortName == "TL1") != null);
            Debug.Assert(_manageAccessToEntity.Languages.GetAll()
                             .FirstOrDefault(l => l.FullName == "Test language 2" && l.ShortName == "TL2") != null);
        }

        [Fact]
        public void Task_2_GetById_Language()
        {
            //Arrange  
            var test1 = _manageAccessToEntity.Languages.GetAll()
                             .FirstOrDefault(l => l.FullName == "Test language 1" && l.ShortName == "TL1");
            var test2 = _manageAccessToEntity.Languages.GetAll()
                             .FirstOrDefault(l => l.FullName == "Test language 2" && l.ShortName == "TL2U");

            //Act  
            Language result1 = new Language();
            Language result2 = new Language();
            if (test1 != null && test2 != null)
            {
                result1 = _manageAccessToEntity.Languages.GetById(test1.Id);
                result2 = _manageAccessToEntity.Languages.GetById(test2.Id);
            }

            //Assert  
            Debug.Assert(test1 != null && test1.Id.Equals(result1.Id) && test1.FullName.Equals(result1.FullName) && 
                         test1.ShortName.Equals(result1.ShortName));
            Debug.Assert(test2 != null && test2.Id.Equals(result2.Id) && test2.FullName.Equals(result2.FullName) &&
                         test2.ShortName.Equals(result2.ShortName));
        }

        [Fact]
        public void Task_3_Update_Language()
        {
            //Arrange  
            var test1 = _manageAccessToEntity.Languages.GetAll()
                .FirstOrDefault(l => l.FullName == "Test language 1" && l.ShortName == "TL1");
            var test2 = _manageAccessToEntity.Languages.GetAll()
                .FirstOrDefault(l => l.FullName == "Test language 2" && l.ShortName == "TL2");

            //Act  
            var updated1 = test1;
            var updated2 = test2;
            if (test1 != null && test2 != null)
            {
                updated1.FullName = "Test language 1 updated";
                updated1.ShortName = "TL1U";
                updated2.FullName = "Test language 2 updated";
                updated2.ShortName = "TL2U";
                _manageAccessToEntity.Languages.Update(updated1);
                _manageAccessToEntity.Languages.Update(updated2);
            }

            //Assert  
            var result1 = _manageAccessToEntity.Languages.GetById(test1.Id);
            var result2 = _manageAccessToEntity.Languages.GetById(test2.Id);
            Debug.Assert(updated1 != null && result1 != null && updated1.FullName.Equals(result1.FullName) &&
                                              updated1.ShortName.Equals(result1.ShortName));
            Debug.Assert(updated2 != null && result1 != null && updated2.FullName.Equals(result2.FullName) &&
                                              updated2.ShortName.Equals(result2.ShortName));
        }

        [Fact]
        public void Task_4_AddRange_LanguageTranslation()
        {
            //Arrange  
                var languageTranslations = new List<LanguageTranslation>();
                languageTranslations.Add(new LanguageTranslation()
                {
                    LanguageTranslationName = "Test language translation 1",
                    LanguageInitialId = _manageAccessToEntity.Languages.GetAll()
                        .FirstOrDefault(l => l.FullName == "Test language 1 updated" && l.ShortName == "TL1U").Id,
                    LanguageToTranslateId = _manageAccessToEntity.Languages.GetAll()
                        .FirstOrDefault(l => l.FullName == "Test language 2 updated" && l.ShortName == "TL2U").Id
                });
                languageTranslations.Add(new LanguageTranslation()
                {
                    LanguageTranslationName = "Test language translation 2",
                    LanguageInitialId = _manageAccessToEntity.Languages.GetAll()
                        .FirstOrDefault(l => l.FullName == "Test language 2 updated" && l.ShortName == "TL2U").Id,
                    LanguageToTranslateId = _manageAccessToEntity.Languages.GetAll()
                        .FirstOrDefault(l => l.FullName == "Test language 1 updated" && l.ShortName == "TL1U").Id
                });

                //Act  
                _manageAccessToEntity.LanguageTranslations.AddRange(languageTranslations);

                //Assert  
                Debug.Assert(_manageAccessToEntity.LanguageTranslations.GetAll()
                                 .FirstOrDefault(l => l.LanguageTranslationName == "Test language translation 1") != null);
                Debug.Assert(_manageAccessToEntity.LanguageTranslations.GetAll()
                                 .FirstOrDefault(l => l.LanguageTranslationName == "Test language translation 2") != null);
        }


        //todo Finish it
        //[Fact]
        //public void Task_5_GetById_LanguageTranslation()
        //{
        //    //Arrange  
        //    var test1 = _manageAccessToEntity.LanguageTranslations.GetAll()
        //                     .FirstOrDefault(l => l.LanguageTranslationName == "Test language translation 1");
        //    var test2 = _manageAccessToEntity.LanguageTranslations.GetAll()
        //                     .FirstOrDefault(l => l.LanguageTranslationName == "Test language translation 2");

        //    //Act  
        //    Language result1 = new Language();
        //    Language result2 = new Language();
        //    if (test1 != null && test2 != null)
        //    {
        //        result1 = _manageAccessToEntity.Languages.GetById(test1.Id);
        //        result2 = _manageAccessToEntity.Languages.GetById(test2.Id);
        //    }

        //    //Assert  
        //    Debug.Assert(test1 != null && test1.Id.Equals(result1.Id) && test1.FullName.Equals(result1.FullName) &&
        //                 test1.ShortName.Equals(result1.ShortName));
        //    Debug.Assert(test2 != null && test2.Id.Equals(result2.Id) && test2.FullName.Equals(result2.FullName) &&
        //                 test2.ShortName.Equals(result2.ShortName));
        //}

        //[Fact]
        //public void Task_3_Update_Language()
        //{
        //    //Arrange  
        //    var test1 = _manageAccessToEntity.Languages.GetAll()
        //        .FirstOrDefault(l => l.FullName == "Test language 1" && l.ShortName == "TS1");
        //    var test2 = _manageAccessToEntity.Languages.GetAll()
        //        .FirstOrDefault(l => l.FullName == "Test language 2" && l.ShortName == "TS2");

        //    //Act  
        //    var updated1 = test1;
        //    var updated2 = test2;
        //    if (test1 != null && test2 != null)
        //    {
        //        updated1.FullName = "Test language 1 updated";
        //        updated1.ShortName = "TL1U";
        //        updated2.FullName = "Test language 2 updated";
        //        updated2.ShortName = "TL2U";
        //        _manageAccessToEntity.Languages.Update(updated1);
        //        _manageAccessToEntity.Languages.Update(updated2);
        //    }

        //    //Assert  
        //    var result1 = _manageAccessToEntity.Languages.GetById(test1.Id);
        //    var result2 = _manageAccessToEntity.Languages.GetById(test2.Id);
        //    Debug.Assert(updated1 != null && result1 != null && updated1.FullName.Equals(result1.FullName) &&
        //                                      updated1.ShortName.Equals(result1.ShortName));
        //    Debug.Assert(updated2 != null && result1 != null && updated2.FullName.Equals(result2.FullName) &&
        //                                      updated2.ShortName.Equals(result2.ShortName));
        //}












        //[Fact] public void Task_14_Delete_Language()
        //{
        //    //Arrange  
        //    var test1 = _manageAccessToEntity.Languages.GetAll()
        //        .FirstOrDefault(l => l.FullName == "Test language 1 updated" && l.ShortName == "TL1U");
        //    var test2 = _manageAccessToEntity.Languages.GetAll()
        //        .FirstOrDefault(l => l.FullName == "Test language 2 updated" && l.ShortName == "TL2U");

        //    //Act  
            
        //    if (test1 != null && test2 != null)
        //    {
        //        _manageAccessToEntity.Languages.Delete(test1.Id);
        //        _manageAccessToEntity.Languages.Delete(test2.Id);
        //    }

        //    //Assert  
        //    Debug.Assert(test1 != null && test2 != null && _manageAccessToEntity.Languages.GetById(test1.Id) == null &&
        //                     _manageAccessToEntity.Languages.GetById(test2.Id) == null);
        //}
    }
}
