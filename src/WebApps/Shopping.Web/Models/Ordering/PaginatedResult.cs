namespace Shopping.Web.Models.Ordering
{
    public class PaginatedResult<TEnity>(int pageIndex, int pageSize, long count, IEnumerable<TEnity> data)
    {
        public int PageIndex { get; } = pageIndex;
        public int PageSize { get; } = pageSize;
        public long Count { get; } = count;
        public IEnumerable<TEnity> Data { get; } = data;
    }
}
