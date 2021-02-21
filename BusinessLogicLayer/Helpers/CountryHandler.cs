using System.Collections.Generic;
using DataAccessLayer.DataBaseModels;

namespace BusinessLogicLayer.Helpers
{
    public class CountryHandler
    {
        private readonly List<Language> _allLanguages;

        public CountryHandler(List<Language> allLanguages)
        {
            _allLanguages = allLanguages ;
        }
        public Dictionary<string, string> TakeCountryByLanguage()
        {
            var languageInCountry = new Dictionary<string, string>();
            string country = "";
            foreach (var language in _allLanguages)
            {
                switch (language.FullName)
                {
                    case "Ukrainian": country = "Ukraine";
                        break;
                    case "Russian":
                        country = "Russia";
                        break;
                    case "English":
                        country = "United Kingdom";
                        break;
                    case "German":
                        country = "Germany";
                        break;
                    case "Chinese":
                        country = "China";
                        break;
                    case "Portuguese":
                        country = "Portugal";
                        break;
                    case "Spanish":
                        country = "Spain";
                        break;
                    case "Polish":
                        country = "Poland";
                        break;
                }
                languageInCountry.Add(language.FullName, country);
            }

            return languageInCountry;
        }
    }
}
