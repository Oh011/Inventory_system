namespace Shared.Results
{
    public class PaginatedResult<T>
    {

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public IEnumerable<T> Items { get; set; }

        public PaginatedResult(int pageIndex, int pageSize, int totalCount, IEnumerable<T> items)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            Items = items;
        }
    }
}
