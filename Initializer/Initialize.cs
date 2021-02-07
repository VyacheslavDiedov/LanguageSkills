﻿using System;
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
                Console.WriteLine("File isn't exist " + _pathRoot + path + fileName);
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
            List<Language> allLanguages = new List<Language>();
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

            if (!_isData)
                return;
            //Save data to dataBase
            _manageAccessToEntity.Languages.CreateRange(allLanguages);
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
                catch (InvalidOperationException)
                {
                    _isData = false;
                    Console.WriteLine("Data isn't exist");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            //Save data to dataBase
            _manageAccessToEntity.LanguageTranslations.CreateRange(allLanguageTranslations);
            Console.WriteLine("{0}/8. Language translations have added", ++_countStage);
        }

        private void _writeTestsToDataBase()
        {
            //Get data from file
            List<ParsedData> testData = ParseData(GetDataFromFile(
                _pathRoot + _generatePath("", "TestsNames", ".xlsx")));

            //Get all list of language
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

            if (!_isData)
                return;
            //Save data to dataBase
            _manageAccessToEntity.Tests.CreateRange(allTests);
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
                catch (InvalidOperationException)
                {
                    _isData = false;
                    Console.WriteLine("Data isn't exist");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            //Save data to dataBase
            _manageAccessToEntity.TestTranslations.CreateRange(allTestTranslations);
            Console.WriteLine("{0}/8. Test translations have added", ++_countStage);
        }

        private void _writeCategoriesToDataBase()
        {
            //Get data from file
            List<ParsedData> categoryData = ParseData(GetDataFromFile(
                _pathRoot + _generatePath("", "CategoriesRoot", ".xlsx")));

            //Get all list of language
            List<Language> allLanguages = _manageAccessToEntity.Languages.GetAll();

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

            if (!_isData)
                return;
            //Save data to dataBase
            _manageAccessToEntity.Categories.CreateRange(allCategories);
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
                catch (InvalidOperationException)
                {
                    _isData = false;
                    Console.WriteLine("Data isn't exist");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            //Save data to dataBase
            _manageAccessToEntity.CategoryTranslations.CreateRange(allCategoryTranslations);
            Console.WriteLine("{0}/8. Category translations have added", ++_countStage);
        }

        private void _writeSubCategoriesToDataBase()
        {
            //Get all categories
            List<Category> allCategories = _manageAccessToEntity.Categories.GetAll();

            //Get all list of language
            List<Language> allLanguages = _manageAccessToEntity.Languages.GetAll();

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

                if (!_isData)
                    return;
                //Save data to dataBase
                _manageAccessToEntity.SubCategories.CreateRange(allSubCategories);

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
                    catch (InvalidOperationException)
                    {
                        _isData = false;
                        Console.WriteLine("Data isn't exist");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                //Save data to dataBase
                _manageAccessToEntity.SubCategoryTranslations.CreateRange(allSubCategoryTranslations);
            }
            Console.WriteLine("{0}/8. SubCategories and subCategory translation have added", ++_countStage);
        }

        private void _writeWordsToDataBase()
        {
            //Get all categories
            List<Category> allCategories = _manageAccessToEntity.Categories.GetAll();

            //Get all subCategories
            List<SubCategory> allSubCategories = _manageAccessToEntity.SubCategories.GetAll();

            //Get all list of language
            List<Language> allLanguages = _manageAccessToEntity.Languages.GetAll();

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

                    if (!_isData)
                        return;
                    //Save data to dataBase
                    _manageAccessToEntity.Words.CreateRange(allWords);

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
                                PronunciationPath = _generatePath(category.CategoryName + @"\" + subCategory.SubCategoryName 
                                                                + @"\pronounce\" + word.Language + @"\", 
                                                            word.Word, ".wav"),
                            });
                        }
                        catch (InvalidOperationException)
                        {
                            _isData = false;
                            Console.WriteLine("Data isn't exist");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }

                    //Save data to dataBase
                    _manageAccessToEntity.WordTranslations.CreateRange(allWordTranslations);
                }
            }
            Console.WriteLine("{0}/8. Words and word translations have added", ++_countStage);
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
                    Console.WriteLine("Something went wrong");
                }
            };

            initializeDataToDataBaseAction();
        }
    }
}