using System.Collections.Generic;

namespace BusinessLogicLayer.ViewModels.Pagination
{
    public class PagedResult<T>
    {
        public List<T> ItemsWithTranslations { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
