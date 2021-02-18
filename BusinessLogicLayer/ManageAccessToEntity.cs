using DataAccessLayer.Crud;

namespace BusinessLogicLayer
{
    public class ManageAccessToEntity
    {
        private CategoryCrud _categoryCrud;
        private CategoryTranslationCrud _categoryTranslationCrud;
        private SubCategoryCrud _subCategoryCrud;
        private SubCategoryTranslationCrud _subCategoryTranslationCrud;
        private WordCrud _wordCrud;
        private WordTranslationCrud _wordTranslationCrud;
        private TestCrud _testCrud;
        private TestTranslationCrud _testTranslationCrud;
        private LanguageCrud _languageCrud;
        private LanguageTranslationCrud _languageTranslationCrud;

        public CategoryCrud Categories
        {
            get
            {
                if (_categoryCrud == null)
                    _categoryCrud = new CategoryCrud();
                return _categoryCrud;
            }
        }

        public CategoryTranslationCrud CategoryTranslations
        {
            get
            {
                if (_categoryTranslationCrud == null)
                    _categoryTranslationCrud = new CategoryTranslationCrud();
                return _categoryTranslationCrud;
            }
        }

        public SubCategoryCrud SubCategories
        {
            get
            {
                if (_subCategoryCrud == null)
                    _subCategoryCrud = new SubCategoryCrud();
                return _subCategoryCrud;
            }
        }

        public SubCategoryTranslationCrud SubCategoryTranslations
        {
            get
            {
                if (_subCategoryTranslationCrud == null)
                    _subCategoryTranslationCrud = new SubCategoryTranslationCrud();
                return _subCategoryTranslationCrud;
            }
        }

        public WordCrud Words
        {
            get
            {
                if (_wordCrud == null)
                    _wordCrud = new WordCrud();
                return _wordCrud;
            }
        }

        public WordTranslationCrud WordTranslations
        {
            get
            {
                if (_wordTranslationCrud == null)
                    _wordTranslationCrud = new WordTranslationCrud();
                return _wordTranslationCrud;
            }
        }
        public TestCrud Tests
        {
            get
            {
                if (_testCrud == null)
                    _testCrud = new TestCrud();
                return _testCrud;
            }
        }

        public TestTranslationCrud TestTranslations
        {
            get
            {
                if (_testTranslationCrud == null)
                    _testTranslationCrud = new TestTranslationCrud();
                return _testTranslationCrud;
            }
        }

        public LanguageCrud Languages
        {
            get
            {
                if (_languageCrud == null)
                    _languageCrud = new LanguageCrud();
                return _languageCrud;
            }
        }

        public LanguageTranslationCrud LanguageTranslations
        {
            get
            {
                if (_languageTranslationCrud == null)
                    _languageTranslationCrud = new LanguageTranslationCrud();
                return _languageTranslationCrud;
            }
        }
    }
}
