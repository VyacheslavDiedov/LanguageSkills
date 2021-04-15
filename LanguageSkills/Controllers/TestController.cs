using System.Collections.Generic;
using BusinessLogicLayer;
using BusinessLogicLayer.Translation;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.ViewModels.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace LanguageSkills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        private readonly ManageAccessToEntity _manageAccessToEntity = new ManageAccessToEntity();

        /// <summary>
        /// Get paged tests with translations
        /// </summary>
        /// <param name="pageNumber">Number of page</param>
        /// <param name="pageSize">Number of items on the page</param>
        /// <returns>response status "OK" and list of test with translations or status "NotFound" and error message</returns>
        [HttpGet]
        public ActionResult<PagedResult<ItemWithTranslation>> GetPagedTest(int pageNumber, int pageSize)
        {
            TranslationHandler translationHandler = new TranslationHandler(_manageAccessToEntity);
            PagedResult<ItemWithTranslation> pagedTestWithTranslations = translationHandler.GetTestsWithTranslations(pageNumber, pageSize);
            if (pagedTestWithTranslations.ItemsWithTranslations.Count != 0)
            {
                return Ok(pagedTestWithTranslations);
            }
            else
            {
                return NotFound("Tests don't exist");
            }
        }
    }
}