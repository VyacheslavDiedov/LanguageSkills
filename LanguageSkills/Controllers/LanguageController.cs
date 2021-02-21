using System.Collections.Generic;
using BusinessLogicLayer;
using BusinessLogicLayer.Translation;
using BusinessLogicLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanguageSkills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : Controller
    {
        private readonly ManageAccessToEntity _manageAccessToEntity = new ManageAccessToEntity();
        /// <summary>
        /// Get list of Language
        /// </summary>
        /// <returns>response status "OK" and list of Language</returns>
        [HttpGet]
        public ActionResult<List<ItemWithTranslation>> GetLanguages()
        {
            var translationHandler = new TranslationHandler(_manageAccessToEntity);
            var languageTranslations = translationHandler.GetLanguagesWithTranslations();
            if (languageTranslations.Count != 0)
            {
                return Ok(languageTranslations);
            }
            else
            {
                return NotFound("Languages don't exist");
            }
        }
    }
}