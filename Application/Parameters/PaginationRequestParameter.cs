namespace Application.Filters
{
    public class PaginationRequestParameter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationRequestParameter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationRequestParameter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }
    }
}
