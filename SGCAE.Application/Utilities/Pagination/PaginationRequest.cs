namespace SGCAE.Application.Utilities.Pagination;


public sealed class PaginationRequest
{
    public const int DefaultPageSize = 15;
    public const int MaxPageSize = 50;

    public int PageNumber { get; }
    public int PageSize { get; }

   
    public int Skip => (PageNumber - 1) * PageSize;

    
    public PaginationRequest(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber < 1 ? 1 : pageNumber;
        PageSize = pageSize < 1 ? DefaultPageSize : Math.Min(pageSize, MaxPageSize);
    }

    
    public static PaginationRequest Default() => new(1, DefaultPageSize);
}