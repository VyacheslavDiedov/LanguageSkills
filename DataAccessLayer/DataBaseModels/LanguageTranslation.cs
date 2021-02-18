using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccessLayer.DataBaseModels
{
    public partial class LanguageTranslation
    {
        public int Id { get; set; }
        public string LanguageTranslationName { get; set; }
        public int LanguageInitialId { get; set; }
        public int LanguageToTranslateId { get; set; }

        public virtual Language LanguageInitial { get; set; }
        public virtual Language LanguageToTranslate { get; set; }
    }
}
