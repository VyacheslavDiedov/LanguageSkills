using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.ViewModels.Pagination;

namespace DataAccessLayer.Helpers
{
    public class PaginationFilter<T>
    {
        public PagedResult<T> GetPagedItems(int pageNumber, int pageSize, List<T> allItems)
        {
            List<T> itemsPerPages = allItems.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            PageInfo pageInfo = new PageInfo { PageNumber = pageNumber, PageSize = pageSize, TotalItems = allItems.Count };
            PagedResult<T> pagedResult = new PagedResult<T> { PageInfo = pageInfo, ItemsWithTranslations = itemsPerPages };
            return pagedResult;
        }
    }
}
