using DataAccessLayer.Repositories.Implementation;

namespace BusinessLogicLayer
{
    public class ManageAccessToEntity
    {
        private CategoryRepository _categoryRepository;
        private CategoryTranslationRepository _categoryTranslationRepository;
        private SubCategoryRepository _subCategoryRepository;
        private SubCategoryTranslationRepository _subCategoryTranslationRepository;
        private WordRepository _wordRepository;
        private WordTranslationRepository _wordTranslationRepository;
        private TestRepository _testRepository;
        private TestTranslationRepository _testTranslationRepository;
        private LanguageRepository _languageRepository;
        private LanguageTranslationRepository _languageTranslationRepository;

        public CategoryRepository Categories
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository();
                return _categoryRepository;
            }
        }

        public CategoryTranslationRepository CategoryTranslations
        {
            get
            {
                if (_categoryTranslationRepository == null)
                    _categoryTranslationRepository = new CategoryTranslationRepository();
                return _categoryTranslationRepository;
            }
        }

        public SubCategoryRepository SubCategories
        {
            get
            {
                if (_subCategoryRepository == null)
                    _subCategoryRepository = new SubCategoryRepository();
                return _subCategoryRepository;
            }
        }

        public SubCategoryTranslationRepository SubCategoryTranslations
        {
            get
            {
                if (_subCategoryTranslationRepository == null)
                    _subCategoryTranslationRepository = new SubCategoryTranslationRepository();
                return _subCategoryTranslationRepository;
            }
        }

        public WordRepository Words
        {
            get
            {
                if (_wordRepository == null)
                    _wordRepository = new WordRepository();
                return _wordRepository;
            }
        }

        public WordTranslationRepository WordTranslations
        {
            get
            {
                if (_wordTranslationRepository == null)
                    _wordTranslationRepository = new WordTranslationRepository();
                return _wordTranslationRepository;
            }
        }
        public TestRepository Tests
        {
            get
            {
                if (_testRepository == null)
                    _testRepository = new TestRepository();
                return _testRepository;
            }
        }

        public TestTranslationRepository TestTranslations
        {
            get
            {
                if (_testTranslationRepository == null)
                    _testTranslationRepository = new TestTranslationRepository();
                return _testTranslationRepository;
            }
        }

        public LanguageRepository Languages
        {
            get
            {
                if (_languageRepository == null)
                    _languageRepository = new LanguageRepository();
                return _languageRepository;
            }
        }

        public LanguageTranslationRepository LanguageTranslations
        {
            get
            {
                if (_languageTranslationRepository == null)
                    _languageTranslationRepository = new LanguageTranslationRepository();
                return _languageTranslationRepository;
            }
        }
    }
}
