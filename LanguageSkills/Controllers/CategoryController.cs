using BusinessLogicLayer;
using BusinessLogicLayer.Helpers;
using BusinessLogicLayer.Translation;
using BusinessLogicLayer.ViewModels;
using BusinessLogicLayer.ViewModels.Pagination;
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
        /// <param name="nativeLanguageId">User native language</param>
        /// <param name="languageToLearnId">User language for learning</param>
        /// <param name="pageNumber">Number of page</param>
        /// <returns>response status "OK" and list of categories with translation or status "NotFound" and error message</returns>
        [HttpGet("nativeLanguageId={nativeLanguageId}&languageToLearnId={languageToLearnId}&pageNumber={pageNumber}")]
        public ActionResult<PagedResult<ItemWithTranslation>> GetCategories(int nativeLanguageId, int languageToLearnId, int pageNumber)
        {
            TranslationHandler translationHandler = new TranslationHandler(_manageAccessToEntity);
            PaginationFilter<ItemWithTranslation> paginationFilter = new PaginationFilter<ItemWithTranslation>();

            //Count of items on the page
            int pageSize = 15;

            var allCategoriesWithTranslations = translationHandler
                .GetCategoriesWithTranslations(nativeLanguageId, languageToLearnId);
            if (allCategoriesWithTranslations.Count != 0)
            {
                
                return Ok(paginationFilter.GetPagedItems(pageNumber, pageSize, allCategoriesWithTranslations));
            }
            else
            {
                return NotFound("Categories don't exist");
            }
        }
    }
}