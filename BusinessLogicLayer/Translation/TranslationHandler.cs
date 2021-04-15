using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.DataBaseModels;
using DataAccessLayer.ViewModels.Pagination;

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
            List<Language> allLanguagesWithTranslations = _manageAccessToEntity.Languages.GetAllLanguagesWithTranslations();
            CountryHandler countryHandler = new CountryHandler(allLanguagesWithTranslations);
            Dictionary<string, string> languageInCountry = countryHandler.TakeCountryByLanguage();
            List<Word> allWords = _manageAccessToEntity.Words.GetAll();

            MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Language, ItemWithTranslation>()
                .ForMember("ItemId", opt => opt.MapFrom(src => src.Id))
                .ForMember("ItemName", opt => opt.MapFrom(src => src.FullName))
                .ForMember("ItemImagePath", opt => opt.MapFrom(src => allWords
                    .FirstOrDefault(w => w.WordName == languageInCountry[src.FullName]).WordImagePath))
                .ForMember("ItemTranslationLearnedName", opt => opt.MapFrom(src =>
                    src.LanguageTranslationLanguageInitials.FirstOrDefault(lt => lt.LanguageInitialId == src.Id
                                         && lt.LanguageToTranslateId == src.Id).LanguageTranslationName)));

            Mapper mapper = new Mapper(config);
            List<ItemWithTranslation> languagesWithTranslations = mapper.Map<List<Language>, List<ItemWithTranslation>>(allLanguagesWithTranslations);

            return languagesWithTranslations;
        }

        public PagedResult<ItemWithTranslation> GetCategoriesWithTranslations(int languageNativeTranslationId, int languageToLearnId,
            int pageNumber, int pageSize)
        {
            //Initialized native language and learn language
            _languageNativeTranslationId = languageNativeTranslationId;
            _languageToLearnId = languageToLearnId;

            PagedResult<Category> pagedCategoriesWithTranslations = 
                _manageAccessToEntity.Categories.GetPagedCategoriesWithTranslations(pageNumber, pageSize);
            MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Category, ItemWithTranslation>()
                    .ForMember("ItemId", opt => opt.MapFrom(src => src.Id))
                    .ForMember("ItemName", opt => opt.MapFrom(src => src.CategoryName))
                    .ForMember("ItemImagePath", opt => opt.MapFrom(src => src.CategoryImagePath))
                    .ForMember("ItemTranslationNativeName", opt => opt.MapFrom(src => src.CategoryTranslations
                            .FirstOrDefault(ct => ct.CategotyId == src.Id && ct.LanguageId == _languageNativeTranslationId).CategoryTranslationName))
                    .ForMember("ItemTranslationLearnedName", opt => opt.MapFrom(src => src.CategoryTranslations
                        .FirstOrDefault(ct => ct.CategotyId == src.Id && ct.LanguageId == _languageToLearnId).CategoryTranslationName)));
            Mapper mapper = new Mapper(config);

            return new PagedResult<ItemWithTranslation>() 
            { 
                PageInfo = pagedCategoriesWithTranslations.PageInfo, 
                ItemsWithTranslations = 
                    mapper.Map<List<Category>, List<ItemWithTranslation>>(pagedCategoriesWithTranslations.ItemsWithTranslations)
            };
        }

        public PagedResult<ItemWithTranslation> GetSubCategoriesWithTranslations(int categoryId, int pageNumber, int pageSize)
        {
            PagedResult<SubCategory> pagedSubCategoriesWithTranslationsByCategory =
                _manageAccessToEntity.SubCategories.GetPagedCategoriesWithTranslationsByCategory(categoryId, pageNumber, pageSize);

            MapperConfiguration config = new MapperConfiguration(cfg => cfg
                .CreateMap<SubCategory, ItemWithTranslation>()
                .ForMember("ItemId", opt => opt.MapFrom(src => src.Id))
                .ForMember("ItemName", opt => opt.MapFrom(src => src.SubCategoryName))
                .ForMember("ItemImagePath", opt => opt.MapFrom(src => src.SubCategoryImagePath))
                .ForMember("ItemTranslationNativeName", opt => opt.MapFrom(src => src.SubCategoryTranslations
                    .FirstOrDefault(st => st.SubCategoryId == src.Id && st.LanguageId == _languageNativeTranslationId)
                    .SubCategoryTranslationName))
                .ForMember("ItemTranslationLearnedName", opt => opt.MapFrom(src => src.SubCategoryTranslations
                    .FirstOrDefault(st => st.SubCategoryId == src.Id && st.LanguageId == _languageToLearnId)
                    .SubCategoryTranslationName)));
            Mapper mapper = new Mapper(config);

            return new PagedResult<ItemWithTranslation>()
            {
                PageInfo = pagedSubCategoriesWithTranslationsByCategory.PageInfo,
                ItemsWithTranslations =
                    mapper.Map<List<SubCategory>, List<ItemWithTranslation>>(pagedSubCategoriesWithTranslationsByCategory.ItemsWithTranslations)
            };
        }

        public List<ItemWithTranslation> GetWordsWithTranslations(int subCategoryId)
        {
            List<Word> wordsBySubCategory = _manageAccessToEntity.Words.GetWordsBySubCategoryId(subCategoryId);

            MapperConfiguration config = new MapperConfiguration(cfg => cfg.CreateMap<Word, ItemWithTranslation>()
                .ForMember("ItemId", opt => opt.MapFrom(src => src.Id))
                .ForMember("ItemName", opt => opt.MapFrom(src => src.WordName))
                .ForMember("ItemImagePath", opt => opt.MapFrom(src => src.WordImagePath))
                .ForMember("ItemTranslationNativeName", opt => opt.MapFrom(src => src.WordTranslations
                    .FirstOrDefault(wt => wt.WordId == src.Id && wt.LanguageId == _languageNativeTranslationId).WordTranslationName))
                .ForMember("ItemTranslationNativePronunciationPath", opt => opt.MapFrom(src => src.WordTranslations
                    .FirstOrDefault(wt => wt.WordId == src.Id && wt.LanguageId == _languageNativeTranslationId).PronunciationPath))
                .ForMember("ItemTranslationLearnedName", opt => opt.MapFrom(src => src.WordTranslations
                    .FirstOrDefault(wt => wt.WordId == src.Id && wt.LanguageId == _languageToLearnId).WordTranslationName))
                .ForMember("ItemTranslationLearnedPronunciationPath", opt => opt.MapFrom(src => src.WordTranslations
                    .FirstOrDefault(wt => wt.WordId == src.Id && wt.LanguageId == _languageToLearnId).PronunciationPath)));

            Mapper mapper = new Mapper(config);
            List<ItemWithTranslation> wordsWithTranslations =
                mapper.Map<List<Word>, List<ItemWithTranslation>>(wordsBySubCategory);

            return wordsWithTranslations;
        }

        public PagedResult<ItemWithTranslation> GetTestsWithTranslations(int pageNumber, int pageSize)
        {
            PagedResult<Test> pagedTestsWithTranslations =
                _manageAccessToEntity.Tests.GetPagedTestsWithTranslations(pageNumber, pageSize);

            MapperConfiguration config = new MapperConfiguration(cfg => cfg
                .CreateMap<Test, ItemWithTranslation>()
                .ForMember("ItemId", opt => opt.MapFrom(src => src.Id))
                .ForMember("ItemName", opt => opt.MapFrom(src => src.TestName))
                .ForMember("ItemTranslationNativeName", opt => opt.MapFrom(src => src.TestTranslations
                    .FirstOrDefault(st => st.LanguageId == _languageNativeTranslationId).TestTranslationName))
                .ForMember("ItemTranslationLearnedName", opt => opt.MapFrom(src => src.TestTranslations
                    .FirstOrDefault(st => st.LanguageId == _languageToLearnId).TestTranslationName)));
            Mapper mapper = new Mapper(config);

            return new PagedResult<ItemWithTranslation>()
            {
                PageInfo = pagedTestsWithTranslations.PageInfo,
                ItemsWithTranslations =
                    mapper.Map<List<Test>, List<ItemWithTranslation>>(pagedTestsWithTranslations.ItemsWithTranslations)
            };
        }
    }
}
