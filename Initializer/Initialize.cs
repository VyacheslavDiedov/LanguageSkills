using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private List<Language> _allLanguages = new List<Language>();

        private string _generatePath(string directoryName, string fileName, string fileExtension)
        {
            //Generate a global path
            string path = @"\Dictionary\" + directoryName + fileName + fileExtension;
            return _isFileExist(path) ? path : 
                _removeSpaces(@"\Dictionary\" + directoryName, fileName + fileExtension);
        }

        private bool _isFileExist(string path)
        {
            return File.Exists(_pathRoot + path);
        }

        private string _removeSpaces(string path, string fileName)
        {
            if (_isFileExist(path + fileName.Replace(" ", "")))
            {
                return path + fileName.Replace(" ", "");
            }
            else
            {
                Console.WriteLine("File doesn't exist " + _pathRoot + path + fileName);
                return null;
            }
        }

        public ExcelWorksheet GetDataFromFile(string path)
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
                    Console.WriteLine(e);
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

        public List<ParsedData> ParseData(ExcelWorksheet worksheet)
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
                    Console.WriteLine(e);
                }
            }

            if (parsedData.Count == 0)
            {
                _isData = false;
                Console.WriteLine("No data available");
            }
            return parsedData;
        }

        private void _writeLanguagesToDataBase()
        {
            //Get data from file
            List<ParsedData> languageData = ParseData(GetDataFromFile(
                _pathRoot + _generatePath("", "Languages", ".xlsx")));

            //Write data to languages
            for (int i = 0, j = 0; i < languageData.Count; i++)
            {
                if (_allLanguages.FirstOrDefault(l => l.FullName == languageData[i].Word) == null)
                {
                    _allLanguages.Add(new Language()
                    {
                        ShortName = languageData[i + j].Language,
                        FullName = languageData[i + j++].Word
                    });
                }
            }

            //Check whether there is such data in the database
            List<string> existingLanguages = _manageAccessToEntity.Languages.GetAll().Select(l => l.FullName).ToList();
            if (!_isData || new HashSet<string>(_allLanguages.Select(l => l.FullName)).SetEquals(existingLanguages))
            {
                Console.WriteLine("Languages exist");
                return;
            }
                
            //Take only new data and save data to dataBase
            _manageAccessToEntity.Languages.CreateRange(_allLanguages.Where(l =>
                !existingLanguages.Contains(l.FullName)).ToList());
            Console.WriteLine("{0}/8. Languages have added", ++_countStage);


            //Get all languages
            _allLanguages = _manageAccessToEntity.Languages.GetAll();
            //Write data to language translations
            var allLanguageTranslations = new List<LanguageTranslation>();
            foreach (var language in languageData)
            {
                try
                {
                    int idLanguageWord = _allLanguages.First(l => l.FullName == language.Word).Id;
                    int idLanguage = _allLanguages.First(l => l.ShortName == language.Language).Id;

                    allLanguageTranslations.Add(new LanguageTranslation()
                    {
                        LanguageTranslationName = language.Translation,
                        LanguageInitialId = idLanguageWord,
                        LanguageToTranslateId = idLanguage
                    });
                }
                catch (InvalidOperationException)
                {
                    _isData = false;
                    Console.WriteLine("Data doesn't exist");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            //Take only new data and save data to dataBase
            List<string> existingLanguageTranslations = _manageAccessToEntity.LanguageTranslations.
                GetAll().Select(l => l.LanguageTranslationName).ToList();
            _manageAccessToEntity.LanguageTranslations.CreateRange(allLanguageTranslations.Where(lt =>
                !existingLanguageTranslations.Contains(lt.LanguageTranslationName)).ToList());
            Console.WriteLine("{0}/8. Language translations have added", ++_countStage);
        }

        private void _writeTestsToDataBase()
        {
            //Get data from file
            List<ParsedData> testData = ParseData(GetDataFromFile(
                _pathRoot + _generatePath("", "TestsNames", ".xlsx")));

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
            List<string> existingTests = _manageAccessToEntity.Tests.GetAll().Select(l => l.TestName).ToList();
            if (!_isData || new HashSet<string>(allTests.Select(t => t.TestName))
                    .SetEquals(existingTests))
            {
                Console.WriteLine("Tests exist");
                return;
            }

            //Take only new data and save data to dataBase
            _manageAccessToEntity.Tests.CreateRange(allTests.Where(t => !existingTests.Contains(t.TestName)).ToList());
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
                    int idLanguage = _allLanguages.First(l => l.ShortName == testItem.Language).Id;
                    allTestTranslations.Add(new TestTranslation()
                    {
                        TestTranslationName = testItem.Translation,
                        TestId = idTest,
                        LanguageId = idLanguage
                    });
                }
                catch (InvalidOperationException)
                {
                    _isData = false;
                    Console.WriteLine("Data doesn't exist");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            //Take only new data and save data to dataBase
            List<string> existingTestTranslations = _manageAccessToEntity.TestTranslations.
                GetAll().Select(l => l.TestTranslationName).ToList();
            _manageAccessToEntity.TestTranslations.CreateRange(allTestTranslations.Where(t => !existingTestTranslations
                .Contains(t.TestTranslationName)).ToList());
            Console.WriteLine("{0}/8. Test translations have added", ++_countStage);
        }

        private void _writeCategoriesToDataBase()
        {
            //Get data from file
            List<ParsedData> categoryData = ParseData(GetDataFromFile(
                _pathRoot + _generatePath("", "CategoriesRoot", ".xlsx")));

            //Write data to categories
            List<Category> allCategories = new List<Category>();
            foreach (var categoryItem in categoryData.Where(categoryItem => _tempItem != categoryItem.Word))
            {
                _tempItem = categoryItem.Word;
                allCategories.Add(new Category()
                {
                    CategoryName = categoryItem.Word,
                    CategoryImagePath = _generatePath(@"pictures\", categoryItem.Word, ".jpg")
            });
            }

            //Check whether there is such data in the database
            List<string> existingCategories = _manageAccessToEntity.Categories.GetAll().Select(l => l.CategoryName).ToList();
            if (!_isData || new HashSet<string>(allCategories.Select(c => c.CategoryName))
                    .SetEquals(existingCategories))
            {
                Console.WriteLine("Categories exist");
                return;
            }

            //Take only new data and save data to dataBase
            _manageAccessToEntity.Categories.CreateRange(allCategories.Where(c => !existingCategories
                .Contains(c.CategoryName)).ToList());
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
                    int idLanguage = _allLanguages.First(l => l.ShortName == categoryItem.Language).Id;
                    allCategoryTranslations.Add(new CategoryTranslation()
                    {
                        CategoryTranslationName = categoryItem.Translation,
                        CategotyId = idCategory,
                        LanguageId = idLanguage
                    });
                }
                catch (InvalidOperationException)
                {
                    _isData = false;
                    Console.WriteLine("Data doesn't exist");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            //Take only new data and save data to dataBase
            List<string> existingCategoryTranslations = _manageAccessToEntity.CategoryTranslations.
                GetAll().Select(c => c.CategoryTranslationName).ToList();
            _manageAccessToEntity.CategoryTranslations.CreateRange(allCategoryTranslations.Where(c => !existingCategoryTranslations
                .Contains(c.CategoryTranslationName)).ToList());
            Console.WriteLine("{0}/8. Category translations have added", ++_countStage);
        }

        private void _writeSubCategoriesToDataBase()
        {
            //Get existing data
            List<string> existingSubCategories = _manageAccessToEntity.SubCategories.
                GetAll().Select(s => s.SubCategoryName).ToList();
            List<string> existingSubCategoryTranslations = _manageAccessToEntity.SubCategoryTranslations.
                GetAll().Select(s => s.SubCategoryTranslationName).ToList();
            bool isSubCategories = true;

            //Get all categories
            List<Category> allCategories = _manageAccessToEntity.Categories.GetAll();

            foreach (var category in allCategories)
            {
                //Get data from file
                List<ParsedData> subCategoryData = ParseData(GetDataFromFile(
                    _pathRoot + _generatePath(category.CategoryName + @"\", 
                        category.CategoryName, ".xlsx")));

                //Write data to subCategories
                List<SubCategory> allSubCategories = new List<SubCategory>();
                foreach (var subCategoryItem in subCategoryData.Where(subCategoryItem => _tempItem != subCategoryItem.Word))
                {
                    _tempItem = subCategoryItem.Word;

                    allSubCategories.Add(new SubCategory()
                    {
                        SubCategoryName = subCategoryItem.Word,
                        SubCategoryImagePath = _generatePath(category.CategoryName + @"\pictures\",
                            subCategoryItem.Word, ".jpg"),
                        CategoryId = category.Id
                });
                }

                //Take only new data and save data to dataBase
                List<SubCategory> newSubCategories = allSubCategories.Where(w => !existingSubCategories
                    .Contains(w.SubCategoryName)).ToList();
                if (!_isData || newSubCategories.Count != 0)
                {
                    _manageAccessToEntity.SubCategories.CreateRange(newSubCategories);
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
                        int idLanguage = _allLanguages.First(l => l.ShortName == subCategoryItem.Language).Id;
                        allSubCategoryTranslations.Add(new SubCategoryTranslation()
                        {
                            SubCategoryTranslationName = subCategoryItem.Translation,
                            SubCategoryId = idSubCategory,
                            LanguageId = idLanguage
                        });
                    }
                    catch (InvalidOperationException)
                    {
                        _isData = false;
                        Console.WriteLine("Data doesn't exist");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                //Take only new data and save data to dataBase
                List<SubCategoryTranslation> newSubCategoryTranslations = allSubCategoryTranslations.Where(s => 
                    !existingSubCategoryTranslations.Contains(s.SubCategoryTranslationName)).ToList();
                if (!_isData || newSubCategoryTranslations.Count != 0)
                {
                    _manageAccessToEntity.SubCategoryTranslations.CreateRange(newSubCategoryTranslations);
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

        private void _writeWordsToDataBase()
        {
            //Get existing data
            List<string> existingWords = _manageAccessToEntity.Words.
                GetAll().Select(w => w.WordName).ToList();
            List<string> existingWordTranslations = _manageAccessToEntity.WordTranslations.
                GetAll().Select(w => w.WordTranslationName).ToList();
            bool isWord = true;

            //Get all categories
            List<Category> allCategories = _manageAccessToEntity.Categories.GetAll();

            //Get all subCategories
            List<SubCategory> allSubCategories = _manageAccessToEntity.SubCategories.GetAll();

            foreach (var category in allCategories)
            {
                foreach (var subCategory in allSubCategories.Where(s => s.CategoryId == category.Id))
                {
                    //Get data from file
                    List<ParsedData> wordData = ParseData(GetDataFromFile(_pathRoot + _generatePath(
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
                            WordImagePath = _generatePath(category.CategoryName + @"\" + subCategory.SubCategoryName 
                                                        + @"\pictures\", word.Word, ".jpg"),
                            SubCategoryId = subCategory.Id
                        });
                    }

                    //Take only new data and save data to dataBase
                    List<Word> newWords = allWords.Where(w => !existingWords.Contains(w.WordName)).ToList();
                    if (newWords.Count != 0)
                    {
                        _manageAccessToEntity.Words.CreateRange(newWords);
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
                            int idLanguage = _allLanguages.First(l => l.ShortName == word.Language).Id;
                            allWordTranslations.Add(new WordTranslation()
                            {
                                WordTranslationName = word.Translation,
                                WordId = idWord,
                                LanguageId = idLanguage,
                                PronunciationPath = _generatePath(category.CategoryName + @"\" + subCategory.SubCategoryName 
                                                                + @"\pronounce\" + word.Language + @"\", 
                                                            word.Word, ".wav"),
                            });
                        }
                        catch (InvalidOperationException)
                        {
                            _isData = false;
                            Console.WriteLine("Data doesn't exist");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }

                    //Take only new data and save data to dataBase
                    List<WordTranslation> newWordTranslations = allWordTranslations
                        .Where(w => !existingWordTranslations.Contains(w.WordTranslationName)).ToList();
                    if (newWordTranslations.Count != 0)
                    {
                        _manageAccessToEntity.WordTranslations.CreateRange(newWordTranslations);
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
            Action initializeDataToDataBaseAction = _writeLanguagesToDataBase;
            initializeDataToDataBaseAction += _writeTestsToDataBase;
            initializeDataToDataBaseAction += _writeCategoriesToDataBase;
            initializeDataToDataBaseAction += _writeSubCategoriesToDataBase;
            initializeDataToDataBaseAction += _writeWordsToDataBase;
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