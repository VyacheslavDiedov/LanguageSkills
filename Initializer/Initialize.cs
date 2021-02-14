using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BusinessLogicLayer;
using InitializeDataBase.ViewModels;
using DataAccessLayer.DataBaseModels;
using OfficeOpenXml;

namespace InitializeDataBase
{
    public class Initialize
    {
        private ManageAccessToEntity _manageAccessToEntity = new ManageAccessToEntity();

        private readonly string _pathRoot = Path.GetFullPath(Path.Combine(
            Directory.GetCurrentDirectory(), @"..\..\..\..\LanguageSkills\wwwroot"));
        private bool _isData = true;
        private string _tempItem = "";
        private int _countStage = 0;

        private string GeneratePath(string directoryName, string fileName, string fileExtension)
        {
            //Generate a global path
            string path = @"\Dictionary\" + directoryName + fileName + fileExtension;
            return IsFileExist(path) ? path :
                RemoveCharactersFromStartAndEnd(@"\Dictionary\" + directoryName, fileName, fileExtension);
        }

        private bool IsFileExist(string path)
        {
            return File.Exists(_pathRoot + path);
        }

        private string RemoveCharactersFromStartAndEnd(string path, string fileName, string fileExtension)
        {
            if (IsFileExist(path + fileName.Trim() + fileExtension))
            {
                return path + fileName.Trim() + fileExtension;
            }
            else
            {
                Console.WriteLine("File doesn't exist " + _pathRoot + path + fileName);
                return null;
            }
        }

        private ExcelWorksheet GetDataFromFile(string path)
        {
            if (path != null)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(path);
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelPackage package = new ExcelPackage(fileInfo);
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    return worksheet;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                      $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                    _isData = false;
                    return null;
                }
            }
            else
            {
                _isData = false;
                return null;
            }
        }

        private List<ParsedData> ParseData(ExcelWorksheet worksheet)
        {
            var parsedData = new List<ParsedData>();

            if (worksheet != null)
            {
                // get number of rows and columns in the sheet
                int rows = worksheet.Dimension.Rows;
                int columns = worksheet.Dimension.Columns;

                try
                {
                    //Loop through the worksheet rows and columns
                    //Started counting from two, because the first row has the name of word 
                    for (int i = 2; i <= rows; i++)
                    {
                        //Started counting from two, because the first column have the name of language
                        for (int j = 2; j <= columns; j++)
                        {
                            if (worksheet.Cells[i, j].Value != null && worksheet.Cells[i, 1].Value != null &&
                                worksheet.Cells[1, j].Value != null)
                            {
                                var data = new ParsedData
                                {
                                    //Get the word
                                    Word = worksheet.Cells[i, 1].Value.ToString(),
                                    //Get the language of word
                                    Language = worksheet.Cells[1, j].Value.ToString(),
                                    //Get the translation of word
                                    Translation = worksheet.Cells[i, j].Value.ToString()
                                };
                                parsedData.Add(data);
                            }
                            else
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                      $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                }
            }

            if (parsedData.Count == 0)
            {
                _isData = false;
                Console.WriteLine("No data available");
            }
            return parsedData;
        }

        private void WriteLanguagesToDataBase()
        {
            //Get data from file
            List<ParsedData> languageData = ParseData(GetDataFromFile(
                _pathRoot + GeneratePath("", "Languages", ".xlsx")));
            List<Language> allLanguages = new List<Language>();

            //Write data to languages
            for (int i = 0, j = 0; i < languageData.Count; i++)
            {
                if (allLanguages.FirstOrDefault(l => l.FullName == languageData[i].Word) == null)
                {
                    allLanguages.Add(new Language()
                    {
                        ShortName = languageData[i + j].Language,
                        FullName = languageData[i + j++].Word
                    });
                }
            }

            //Check whether there is such data in the database
            List<string> existingLanguageNames = _manageAccessToEntity.Languages.GetAll().Select(l => l.FullName).ToList();
            List<Language> newLanguages =
                allLanguages.Where(l => !existingLanguageNames.Contains(l.FullName)).ToList();
            if (!_isData || newLanguages.Count == 0)
            {
                Console.WriteLine("Languages exist");
                return;
            }
                
            //Take only new data and save data to dataBase
            _manageAccessToEntity.Languages.AddRange(newLanguages);
            Console.WriteLine("{0}/8. Languages have added", ++_countStage);


            //Get all languages
            allLanguages = _manageAccessToEntity.Languages.GetAll();
            //Write data to language translations
            var allLanguageTranslations = new List<LanguageTranslation>();
            foreach (var language in languageData)
            {
                try
                {
                    int idLanguageWord = allLanguages.First(l => l.FullName == language.Word).Id;
                    int idLanguage = allLanguages.First(l => l.ShortName == language.Language).Id;

                    allLanguageTranslations.Add(new LanguageTranslation()
                    {
                        LanguageTranslationName = language.Translation,
                        LanguageInitialId = idLanguageWord,
                        LanguageToTranslateId = idLanguage
                    });
                }
                catch (InvalidOperationException invalidOperationException)
                {
                    _isData = false;
                    Console.WriteLine("Data doesn't exist");
                    Console.WriteLine($"ClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                      $"\nException message: {invalidOperationException.Message}. \n" +
                                      $"Exception stack trace: {invalidOperationException.StackTrace}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                      $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                }
            }

            //Take only new data and save data to dataBase
            List<string> existingLanguageTranslationNames = _manageAccessToEntity.LanguageTranslations.
                GetAll().Select(l => l.LanguageTranslationName).ToList();
            List<LanguageTranslation> newLanguageTranslations = allLanguageTranslations.Where(lt =>
                !existingLanguageTranslationNames.Contains(lt.LanguageTranslationName)).ToList();
            if (newLanguageTranslations.Count != 0)
            {
                _manageAccessToEntity.LanguageTranslations.AddRange(newLanguageTranslations);
                Console.WriteLine("{0}/8. Language translations have added", ++_countStage);
            }
            else
            {
                Console.WriteLine("Language translations exist");
            }
        }

        private void WriteTestsToDataBase()
        {
            //Get data from file
            List<ParsedData> testData = ParseData(GetDataFromFile(
                _pathRoot + GeneratePath("", "TestsNames", ".xlsx")));

            //Get all language
            List<Language> allLanguages = _manageAccessToEntity.Languages.GetAll();

        //Write data to tests
        List<Test> allTests = new List<Test>();
            foreach (var testItem in testData.Where(testItem => _tempItem != testItem.Word))
            {
                _tempItem = testItem.Word;

                allTests.Add(new Test()
                {
                    TestName = testItem.Word
                });
            }

            //Check whether there is such data in the database
            List<string> existingTestNames = _manageAccessToEntity.Tests.GetAll().Select(l => l.TestName).ToList();
            List<Test> newTests = allTests.Where(t => !existingTestNames.Contains(t.TestName)).ToList();
            if (!_isData || newTests.Count == 0)
            {
                Console.WriteLine("Tests exist");
                return;
            }

            //Take only new data and save data to dataBase
            _manageAccessToEntity.Tests.AddRange(newTests);
            Console.WriteLine("{0}/8. Tests have added", ++_countStage);


            //Get all tests
            allTests = _manageAccessToEntity.Tests.GetAll();
            List<TestTranslation> allTestTranslations = new List<TestTranslation>();
            //Write test translation
            foreach (var testItem in testData)
            {
                try
                {
                    int idTest = allTests.First(l => l.TestName == testItem.Word).Id;
                    int idLanguage = allLanguages.First(l => l.ShortName == testItem.Language).Id;
                    allTestTranslations.Add(new TestTranslation()
                    {
                        TestTranslationName = testItem.Translation,
                        TestId = idTest,
                        LanguageId = idLanguage
                    });
                }
                catch (InvalidOperationException invalidOperationException)
                {
                    _isData = false;
                    Console.WriteLine("Data doesn't exist");
                    Console.WriteLine($"ClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                      $"\nException message: {invalidOperationException.Message}. \n" +
                                      $"Exception stack trace: {invalidOperationException.StackTrace}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                      $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                }
            }

            //Take only new data and save data to dataBase
            List<string> existingTestTranslationNames = _manageAccessToEntity.TestTranslations.
                GetAll().Select(l => l.TestTranslationName).ToList();
            List<TestTranslation> newTestTranslations = allTestTranslations.Where(t => !existingTestTranslationNames
                .Contains(t.TestTranslationName)).ToList();
            if (newTestTranslations.Count != 0)
            {
                _manageAccessToEntity.TestTranslations.AddRange(newTestTranslations);
                Console.WriteLine("{0}/8. Test translations have added", ++_countStage);
            }
            else
            {
                Console.WriteLine("Test translations exist");
            }
        }

        private void WriteCategoriesToDataBase()
        {
            //Get data from file
            List<ParsedData> categoryData = ParseData(GetDataFromFile(
                _pathRoot + GeneratePath("", "CategoriesRoot", ".xlsx")));

            //Get all language
            List<Language> allLanguages = _manageAccessToEntity.Languages.GetAll();

            //Write data to categories
            List<Category> allCategories = new List<Category>();
            foreach (var categoryItem in categoryData.Where(categoryItem => _tempItem != categoryItem.Word))
            {
                _tempItem = categoryItem.Word;
                allCategories.Add(new Category()
                {
                    CategoryName = categoryItem.Word,
                    CategoryImagePath = GeneratePath(@"pictures\", categoryItem.Word, ".jpg")
            });
            }

            //Check whether there is such data in the database
            List<string> existingCategoryNames = _manageAccessToEntity.Categories.GetAll().Select(l => l.CategoryName).ToList();
            List<Category> newCategories = allCategories.Where(c => !existingCategoryNames
                .Contains(c.CategoryName)).ToList();
            if (!_isData || newCategories.Count == 0)
            {
                Console.WriteLine("Categories exist");
                return;
            }

            //Take only new data and save data to dataBase
            _manageAccessToEntity.Categories.AddRange(newCategories);
            Console.WriteLine("{0}/8. Categories have added", ++_countStage);


            //Get all categories
            allCategories = _manageAccessToEntity.Categories.GetAll();
            List<CategoryTranslation> allCategoryTranslations = new List<CategoryTranslation>();
            //Write category translation
            foreach (var categoryItem in categoryData)
            {
                try
                {
                    int idCategory = allCategories.First(l => l.CategoryName == categoryItem.Word).Id;
                    int idLanguage = allLanguages.First(l => l.ShortName == categoryItem.Language).Id;
                    allCategoryTranslations.Add(new CategoryTranslation()
                    {
                        CategoryTranslationName = categoryItem.Translation,
                        CategotyId = idCategory,
                        LanguageId = idLanguage
                    });
                }
                catch (InvalidOperationException invalidOperationException)
                {
                    _isData = false;
                    Console.WriteLine("Data doesn't exist");
                    Console.WriteLine($"ClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                      $"\nException message: {invalidOperationException.Message}. \n" +
                                      $"Exception stack trace: {invalidOperationException.StackTrace}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                      $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                }
            }

            //Take only new data and save data to dataBase
            List<string> existingCategoryTranslationNames = _manageAccessToEntity.CategoryTranslations.
                GetAll().Select(c => c.CategoryTranslationName).ToList();
            List<CategoryTranslation> newCategoryTranslations = allCategoryTranslations.Where(c =>
                !existingCategoryTranslationNames.Contains(c.CategoryTranslationName)).ToList();
            if (newCategoryTranslations.Count != 0)
            {
                _manageAccessToEntity.CategoryTranslations.AddRange(newCategoryTranslations);
                Console.WriteLine("{0}/8. Category translations have added", ++_countStage);
            }
            else
            {
                Console.WriteLine("Category translations exist");
            }
        }

        private void WriteSubCategoriesToDataBase()
        {
            //Get existing data
            List<string> existingSubCategoryNames = _manageAccessToEntity.SubCategories.
                GetAll().Select(s => s.SubCategoryName).ToList();
            List<string> existingSubCategoryTranslationNames = _manageAccessToEntity.SubCategoryTranslations.
                GetAll().Select(s => s.SubCategoryTranslationName).ToList();
            bool isSubCategories = true;

            //Get all language
            List<Language> allLanguages = _manageAccessToEntity.Languages.GetAll();

            //Get all categories
            List<Category> allCategories = _manageAccessToEntity.Categories.GetAll();

            foreach (var category in allCategories)
            {
                //Get data from file
                List<ParsedData> subCategoryData = ParseData(GetDataFromFile(
                    _pathRoot + GeneratePath(category.CategoryName + @"\", 
                        category.CategoryName, ".xlsx")));

                //Write data to subCategories
                List<SubCategory> allSubCategories = new List<SubCategory>();
                foreach (var subCategoryItem in subCategoryData.Where(subCategoryItem => _tempItem != subCategoryItem.Word))
                {
                    _tempItem = subCategoryItem.Word;

                    allSubCategories.Add(new SubCategory()
                    {
                        SubCategoryName = subCategoryItem.Word,
                        SubCategoryImagePath = GeneratePath(category.CategoryName + @"\pictures\",
                            subCategoryItem.Word, ".jpg"),
                        CategoryId = category.Id
                });
                }

                //Take only new data and save data to dataBase
                List<SubCategory> newSubCategories = allSubCategories.Where(w => !existingSubCategoryNames
                    .Contains(w.SubCategoryName)).ToList();
                if (!_isData || newSubCategories.Count != 0)
                {
                    _manageAccessToEntity.SubCategories.AddRange(newSubCategories);
                }
                else
                {
                    isSubCategories = false;
                }

                //Get all subCategories
                allSubCategories = _manageAccessToEntity.SubCategories.GetAll();
                List<SubCategoryTranslation> allSubCategoryTranslations = new List<SubCategoryTranslation>();
                //Write subCategory translation
                foreach (var subCategoryItem in subCategoryData)
                {
                    try
                    {
                        int idSubCategory = allSubCategories.First(l => l.SubCategoryName == subCategoryItem.Word).Id;
                        int idLanguage = allLanguages.First(l => l.ShortName == subCategoryItem.Language).Id;
                        allSubCategoryTranslations.Add(new SubCategoryTranslation()
                        {
                            SubCategoryTranslationName = subCategoryItem.Translation,
                            SubCategoryId = idSubCategory,
                            LanguageId = idLanguage
                        });
                    }
                    catch (InvalidOperationException invalidOperationException)
                    {
                        _isData = false;
                        Console.WriteLine("Data doesn't exist");
                        Console.WriteLine($"ClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                          $"\nException message: {invalidOperationException.Message}. \n" +
                                          $"Exception stack trace: {invalidOperationException.StackTrace}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                          $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                    }
                }

                //Take only new data and save data to dataBase
                List<SubCategoryTranslation> newSubCategoryTranslations = allSubCategoryTranslations.Where(s => 
                    !existingSubCategoryTranslationNames.Contains(s.SubCategoryTranslationName)).ToList();
                if (!_isData || newSubCategoryTranslations.Count != 0)
                {
                    _manageAccessToEntity.SubCategoryTranslations.AddRange(newSubCategoryTranslations);
                }
            }

            if (isSubCategories)
            {
                Console.WriteLine("{0}/8. SubCategories and subCategory translation have added", ++_countStage);
            }
            else
            {
                Console.WriteLine("Subcategories and subCategory translation exist. Added only new data");
            }
        }

        private void WriteWordsToDataBase()
        {
            //Get existing data
            List<string> existingWordNames = _manageAccessToEntity.Words.
                GetAll().Select(w => w.WordName).ToList();
            List<string> existingWordTranslationNames = _manageAccessToEntity.WordTranslations.
                GetAll().Select(w => w.WordTranslationName).ToList();
            bool isWord = true;

            //Get all language
            List<Language> allLanguages = _manageAccessToEntity.Languages.GetAll();

            //Get all categories
            List<Category> allCategories = _manageAccessToEntity.Categories.GetAll();

            //Get all subCategories
            List<SubCategory> allSubCategories = _manageAccessToEntity.SubCategories.GetAll();

            foreach (var category in allCategories)
            {
                foreach (var subCategory in allSubCategories.Where(s => s.CategoryId == category.Id))
                {
                    //Get data from file
                    List<ParsedData> wordData = ParseData(GetDataFromFile(_pathRoot + GeneratePath(
                            category.CategoryName + @"\" + subCategory.SubCategoryName + @"\",
                            subCategory.SubCategoryName, ".xlsx")));

                    //Write data to word table in dataBase
                    List<Word> allWords = new List<Word>();
                    foreach (var word in wordData.Where(word => _tempItem != word.Word))
                    {
                        _tempItem = word.Word;

                        allWords.Add(new Word()
                        {
                            WordName = word.Word,
                            WordImagePath = GeneratePath(category.CategoryName + @"\" + subCategory.SubCategoryName 
                                                        + @"\pictures\", word.Word, ".jpg"),
                            SubCategoryId = subCategory.Id
                        });
                    }

                    //Take only new data and save data to dataBase
                    List<Word> newWords = allWords.Where(w => !existingWordNames.Contains(w.WordName)).ToList();
                    if (newWords.Count != 0)
                    {
                        _manageAccessToEntity.Words.AddRange(newWords);
                    }
                    else
                    {
                        isWord = false;
                    }
                    
                    //Get all words
                    allWords = _manageAccessToEntity.Words.GetAll();
                    List<WordTranslation> allWordTranslations = new List<WordTranslation>();

                    //Write subCategory translation
                    foreach (var word in wordData)
                    {
                        try
                        {
                            int idWord = allWords.First(w => w.WordName == word.Word).Id;
                            int idLanguage = allLanguages.First(l => l.ShortName == word.Language).Id;
                            allWordTranslations.Add(new WordTranslation()
                            {
                                WordTranslationName = word.Translation,
                                WordId = idWord,
                                LanguageId = idLanguage,
                                PronunciationPath = GeneratePath(category.CategoryName + @"\" + subCategory.SubCategoryName 
                                                                + @"\pronounce\" + word.Language + @"\", 
                                                            word.Word, ".wav"),
                            });
                        }
                        catch (InvalidOperationException invalidOperationException)
                        {
                            _isData = false;
                            Console.WriteLine("Data doesn't exist");
                            Console.WriteLine($"ClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                              $"\nException message: {invalidOperationException.Message}. \n" +
                                              $"Exception stack trace: {invalidOperationException.StackTrace}");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"\nClassName - {this.GetType().Name}\nMethodName - {MethodBase.GetCurrentMethod().Name}" +
                                              $"\nException message: {e.Message}. \nException stack trace: {e.StackTrace}");
                        }
                    }

                    //Take only new data and save data to dataBase
                    List<WordTranslation> newWordTranslations = allWordTranslations
                        .Where(w => !existingWordTranslationNames.Contains(w.WordTranslationName)).ToList();
                    if (newWordTranslations.Count != 0)
                    {
                        _manageAccessToEntity.WordTranslations.AddRange(newWordTranslations);
                    }
                }
            }

            if (isWord)
            {
                Console.WriteLine("{0}/8. Words and word translations have added", ++_countStage);
            }
            else
            {
                Console.WriteLine("Words and word translations exist. Added only new data");
            }
        }

        public void Initializer()
        {
            Action initializeDataToDataBaseAction = WriteLanguagesToDataBase;
            initializeDataToDataBaseAction += WriteTestsToDataBase;
            initializeDataToDataBaseAction += WriteCategoriesToDataBase;
            initializeDataToDataBaseAction += WriteSubCategoriesToDataBase;
            initializeDataToDataBaseAction += WriteWordsToDataBase;
            initializeDataToDataBaseAction += () =>
            {
                if (_isData && _countStage == 8)
                {
                    Console.WriteLine("Data successfully added to DataBase");
                }
                else
                {
                    Console.WriteLine("Something went wrong. Not all steps were taken");
                }
            };

            initializeDataToDataBaseAction();
        }
    }
}