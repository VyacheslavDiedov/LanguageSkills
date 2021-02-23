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
    public class SubCategoryController : Controller
    {
        private readonly ManageAccessToEntity _manageAccessToEntity = new ManageAccessToEntity();

        /// <summary>
        /// Get SubCategories with translations by category Id
        /// </summary>
        /// <param name="categoryId">Category selected by the user</param>
        /// <param name="pageNumber">Number of page</param>
        /// <returns>response status "OK" and list of subCategories with translations or status "NotFound" and error message</returns>
        [HttpGet]
        public ActionResult<PagedResult<ItemWithTranslation>> GetSubCategories(int categoryId, int pageNumber)
        {
            TranslationHandler translationHandler = new TranslationHandler(_manageAccessToEntity);
            PaginationFilter<ItemWithTranslation> paginationFilter = new PaginationFilter<ItemWithTranslation>();

            //Count of items on the page
            const int pageSize = 15;

            var subCategoriesWithTranslationsByCategory = translationHandler.GetSubCategoriesWithTranslations(categoryId);
            if (subCategoriesWithTranslationsByCategory.Count != 0)
            {

                return Ok(paginationFilter.GetPagedItems(pageNumber, pageSize, subCategoriesWithTranslationsByCategory));
            }
            else
            {
                return NotFound("SubCategories don't exist");
            }
        }
    }
}