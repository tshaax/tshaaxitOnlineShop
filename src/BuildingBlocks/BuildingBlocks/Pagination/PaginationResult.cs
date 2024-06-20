
namespace BuildingBlocks.Pagination
{
    public  class PaginationResult<TEnity>(int pageIndex, int pageSize, long count, IEnumerable<TEnity> data)
        where TEnity : class
    {
        public int PageIndex { get; set; } = pageIndex;
        public int PageSize { get; set; } = pageSize;
        public long Count { get; set; } = count;
        public IEnumerable<TEnity> Data { get; set; } = data;
    }
}
