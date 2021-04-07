using System.Collections.Generic;
using BusinessLogicLayer;
using BusinessLogicLayer.Translation;
using BusinessLogicLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanguageSkills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly ManageAccessToEntity _manageAccessToEntity = new ManageAccessToEntity();

        /// <summary>
        /// Get tests with translations
        /// </summary>
        /// <returns>response status "OK" and list of test with translations or status "NotFound" and error message</returns>
        [HttpGet]
        public ActionResult<List<ItemWithTranslation>> GetTest()
        {
            TranslationHandler translationHandler = new TranslationHandler(_manageAccessToEntity);
            List<ItemWithTranslation> testWithTranslations = translationHandler.GetTestsWithTranslations();
            if (testWithTranslations.Count != 0)
            {
                return Ok(testWithTranslations);
            }
            else
            {
                return NotFound("Words don't exist");
            }
        }
    }
}