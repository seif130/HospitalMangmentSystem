namespace HospitalSystem.Application.Models;
public sealed class PaginatedList<T>
{
    public IReadOnlyList<T> Items { get; }
    public int TotalCount { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalPages => (int)Math.Ceiling(TotalCount/(double)PageSize);
    public bool HasPreviousPage => PageNumber > 1;
    public bool HasNextPage => PageNumber < TotalPages;
    public PaginatedList(IReadOnlyList<T> items,int totalCount,int pageNumber,int pageSize)
    {
        ArgumentNullException.ThrowIfNull(items);
        if(pageNumber<=0) 
            throw new ArgumentOutOfRangeException(nameof(pageNumber));

        if(pageSize<=0)
            throw new ArgumentOutOfRangeException(nameof(pageSize));
        Items=items;
        TotalCount=totalCount;
        PageNumber=pageNumber;
        PageSize=pageSize;
    }
    public static PaginatedList<T> Create(IEnumerable<T> source,int pageNumber,int pageSize)
    {
        ArgumentNullException.ThrowIfNull(source);
        if(pageNumber<=0)
            throw new ArgumentOutOfRangeException(nameof(pageNumber));
        if(pageSize<=0)
            throw new ArgumentOutOfRangeException(nameof(pageSize));
        var list=source.ToList();
        return new(list.Skip((pageNumber-1)*pageSize).Take(pageSize).ToList(),list.Count,pageNumber,pageSize);
    }
}
