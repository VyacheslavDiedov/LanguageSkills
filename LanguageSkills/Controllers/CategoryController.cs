using BusinessLogicLayer;
using BusinessLogicLayer.Translation;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.ViewModels.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace LanguageSkills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ManageAccessToEntity _manageAccessToEntity = new ManageAccessToEntity();

        /// <summary>
        /// Get Categories with translations by native language and by language to learn
        /// </summary>
        /// <param name="languageToLearnId">User language for learning</param>
        /// <param name="nativeLanguageId">User native language</param>
        /// <param name="pageNumber">Number of page</param>
        /// <param name="pageSize">Number of items on the page</param>
        /// <returns>response status "OK" and list of categories with translation or status "NotFound" and error message</returns>
        [HttpGet]
        public ActionResult<PagedResult<ItemWithTranslation>> GetCategories(int languageToLearnId, 
            int nativeLanguageId, int pageNumber,  int pageSize)
        {
            TranslationHandler translationHandler = new TranslationHandler(_manageAccessToEntity);

            PagedResult<ItemWithTranslation> allCategoriesWithTranslations = translationHandler
                .GetCategoriesWithTranslations(nativeLanguageId, languageToLearnId, pageNumber, pageSize);

            if (allCategoriesWithTranslations.ItemsWithTranslations.Count != 0)
            {
                return Ok(allCategoriesWithTranslations);
            }
            
            return NotFound("Categories don't exist");
        }
    }
}