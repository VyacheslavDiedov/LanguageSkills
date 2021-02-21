using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.ViewModels;

namespace BusinessLogicLayer.Translation
{
    public class TranslationHandler
    {
        private readonly ManageAccessToEntity _manageAccessToEntity;
        private static int _languageNativeTranslationId = 0;
        private static int _languageToLearnId = 0;
        public TranslationHandler(ManageAccessToEntity manageAccessToEntity)
        {
            _manageAccessToEntity = manageAccessToEntity;
        }

        public List<ItemWithTranslation> GetLanguagesWithTranslations()
        {
            var languageWithTranslation = new List<ItemWithTranslation>();
            var allLanguage = _manageAccessToEntity.Languages.GetAll();
            var countryHandler = new CountryHandler(allLanguage);
            var languageInCountry = countryHandler.TakeCountryByLanguage();
            var allWords = _manageAccessToEntity.Words.GetAll();

            foreach (var language in allLanguage)
            {
                languageWithTranslation.Add(new ItemWithTranslation()
                {
                    ItemId = language.Id,
                    ItemName = language.FullName,
                    ItemImagePath = allWords
                            .FirstOrDefault(l => l.WordName == languageInCountry[language.FullName])?.WordImagePath,
                    ItemTranslationLearnedName = _manageAccessToEntity.LanguageTranslations.GetAll()
                            .FirstOrDefault(l => l.LanguageInitialId == language.Id 
                                                 && l.LanguageToTranslateId == language.Id)?.LanguageTranslationName
                });
            }

            return languageWithTranslation;
        }

        public List<ItemWithTranslation> GetCategoriesWithTranslations(int languageNativeTranslationId, int languageToLearnId)
        {
            _languageNativeTranslationId = languageNativeTranslationId;
            _languageToLearnId = languageToLearnId;
            var categoriesWithTranslation = new List<ItemWithTranslation>();
            var allCategories = _manageAccessToEntity.Categories.GetAll();
            var categoryTranslationsByLanguages = _manageAccessToEntity.CategoryTranslations.GetAll()
                .Where(c => c.LanguageId == _languageNativeTranslationId || c.LanguageId == _languageToLearnId).ToList();
            if (categoryTranslationsByLanguages.Count != 0)
            {
                foreach (var category in allCategories)
                {
                    categoriesWithTranslation.Add(new ItemWithTranslation()
                    {
                        ItemId = category.Id,
                        ItemName = category.CategoryName,
                        ItemImagePath = category.CategoryImagePath,
                        ItemTranslationNativeName = categoryTranslationsByLanguages.FirstOrDefault(c => c.CategotyId == category.Id
                                                                                                        && c.LanguageId == _languageNativeTranslationId)?.CategoryTranslationName,
                        ItemTranslationLearnedName = categoryTranslationsByLanguages.FirstOrDefault(c => c.CategotyId == category.Id
                                                                                                         && c.LanguageId == _languageToLearnId)?.CategoryTranslationName
                    });
                }
            }

            return categoriesWithTranslation;
        }

        public List<ItemWithTranslation> GetSubCategoriesWithTranslations(int categoryId)
        {
            var subCategoriesWithTranslation = new List<ItemWithTranslation>();
            var subCategoriesByCategory = _manageAccessToEntity.SubCategories.GetAll().Where(s => s.CategoryId == categoryId);
            var subCategoryTranslationsByLanguages = _manageAccessToEntity.SubCategoryTranslations.GetAll()
                .Where(c => c.LanguageId == _languageNativeTranslationId || c.LanguageId == _languageToLearnId).ToList();
            foreach (var subCategory in subCategoriesByCategory)
            {
                subCategoriesWithTranslation.Add(new ItemWithTranslation()
                {
                    ItemId = subCategory.Id,
                    ItemName = subCategory.SubCategoryName,
                    ItemImagePath = subCategory.SubCategoryImagePath,
                    ItemTranslationNativeName = subCategoryTranslationsByLanguages.FirstOrDefault(c => 
                        c.SubCategoryId == subCategory.Id && c.LanguageId == _languageNativeTranslationId)?.SubCategoryTranslationName,
                    ItemTranslationLearnedName = subCategoryTranslationsByLanguages.FirstOrDefault(c => 
                        c.SubCategoryId == subCategory.Id && c.LanguageId == _languageToLearnId)?.SubCategoryTranslationName
                });
            }
            
            return subCategoriesWithTranslation;
        }

        public List<ItemWithTranslation> GetWordsWithTranslations(int subCategoryId)
        {
            var wordsWithTranslation = new List<ItemWithTranslation>();
            var wordsBySubCategory = _manageAccessToEntity.Words.GetWordsBySubCategoryId(subCategoryId);
            var wordsTranslationsByWordIds = _manageAccessToEntity.WordTranslations
                .GetWordTranslationsByWordIds(wordsBySubCategory.Select(w => w.Id).ToList());
            foreach (var word in wordsBySubCategory)
            {
                wordsWithTranslation.Add(new ItemWithTranslation()
                {
                    ItemId = word.Id,
                    ItemName = word.WordName,
                    ItemImagePath = word.WordImagePath,
                    ItemTranslationNativeName = wordsTranslationsByWordIds.FirstOrDefault(w =>
                        w.WordId == word.Id && w.LanguageId == _languageNativeTranslationId)?.WordTranslationName,
                    ItemTranslationNativePronunciationPath = wordsTranslationsByWordIds.FirstOrDefault(w =>
                        w.WordId == word.Id && w.LanguageId == _languageNativeTranslationId)?.PronunciationPath,
                    ItemTranslationLearnedName = wordsTranslationsByWordIds.FirstOrDefault(w =>
                        w.WordId == word.Id && w.LanguageId == _languageToLearnId)?.WordTranslationName,
                    ItemTranslationLearnedPronunciationPath = wordsTranslationsByWordIds.FirstOrDefault(w =>
                        w.WordId == word.Id && w.LanguageId == _languageToLearnId)?.PronunciationPath
                });
            }

            return wordsWithTranslation;
        }
    }
}
