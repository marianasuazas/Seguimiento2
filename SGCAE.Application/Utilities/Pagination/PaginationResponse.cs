namespace SGCAE.Application.Utilities.Pagination;


public sealed class PaginationResponse<T>
{
    public IReadOnlyList<T> Items { get; }
    public int TotalCount { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPages { get; }
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;

    private PaginationResponse(IReadOnlyList<T> items, int totalCount, PaginationRequest request)
    {
        Items = items;
        TotalCount = totalCount;
        PageNumber = request.PageNumber;
        PageSize = request.PageSize;
        TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize);
    }

    public static PaginationResponse<T> Create(IReadOnlyList<T> items, int totalCount, PaginationRequest request)
    {
        return new PaginationResponse<T>(items, totalCount, request);
    }
}