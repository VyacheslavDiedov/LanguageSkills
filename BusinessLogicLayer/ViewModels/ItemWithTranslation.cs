
namespace BusinessLogicLayer.ViewModels
{
    public class ItemWithTranslation
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemImagePath { get; set; }
        public string ItemTranslationNativeName { get; set; }
        public string ItemTranslationNativePronunciationPath { get; set; }
        public string ItemTranslationLearnedName { get; set; }
        public string ItemTranslationLearnedPronunciationPath { get; set; }
    }
}
