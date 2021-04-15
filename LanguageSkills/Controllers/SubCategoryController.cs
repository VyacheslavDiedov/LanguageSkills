using BusinessLogicLayer;
using BusinessLogicLayer.Translation;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.ViewModels.Pagination;
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
        /// <param name="pageSize">Number of items on the page</param>
        /// <returns>response status "OK" and list of subCategories with translations or status "NotFound" and error message</returns>
        [HttpGet]
        public ActionResult<PagedResult<ItemWithTranslation>> GetSubCategories(int categoryId, int pageNumber, int pageSize)
        {
            TranslationHandler translationHandler = new TranslationHandler(_manageAccessToEntity);

            PagedResult<ItemWithTranslation> subCategoriesWithTranslationsByCategory = 
                translationHandler.GetSubCategoriesWithTranslations(categoryId, pageNumber, pageSize);
            if (subCategoriesWithTranslationsByCategory.ItemsWithTranslations.Count != 0)
            {

                return Ok(subCategoriesWithTranslationsByCategory);
            }

            return NotFound("SubCategories don't exist");
        }
    }
}