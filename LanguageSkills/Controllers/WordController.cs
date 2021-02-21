using System.Collections.Generic;
using BusinessLogicLayer;
using BusinessLogicLayer.Translation;
using BusinessLogicLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanguageSkills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : Controller
    {
        private readonly ManageAccessToEntity _manageAccessToEntity = new ManageAccessToEntity();

        /// <summary>
        /// Get words with translations by subCategory Id
        /// </summary>
        /// <param name="subCategoryId">SubCategory selected by the user</param>
        /// <returns>response status "OK" and list of words with translations or status "NotFound" and error message</returns>
        [HttpGet("{subCategoryId:int}")]
        public ActionResult<List<ItemWithTranslation>> GetWords(int subCategoryId)
        {
            TranslationHandler translationHandler = new TranslationHandler(_manageAccessToEntity);
            var wordWithTranslationsBySubCategory = translationHandler.GetWordsWithTranslations(subCategoryId);
            if (wordWithTranslationsBySubCategory.Count != 0)
            {
                return Ok(wordWithTranslationsBySubCategory);
            }
            else
            {
                return NotFound("Words don't exist");
            }
        }
    }
}