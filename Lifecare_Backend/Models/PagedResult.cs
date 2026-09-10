using System.Collections.Generic;

namespace Lifecare_Backend.Models
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
        public int TotalPages => PageSize == 0 ? 0 : (int)((TotalCount + PageSize - 1) / PageSize);
    }
}
