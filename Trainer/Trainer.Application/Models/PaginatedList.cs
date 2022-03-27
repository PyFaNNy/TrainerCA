namespace Trainer.Application.Models;

using Microsoft.EntityFrameworkCore;

public class PaginatedList<T>
{
    public List<T> Items
    {
        get;
        set;
    }

    public int PageIndex
    {
        get;
        set;
    }

    public int TotalPages
    {
        get;
        set;
    }

    public int TotalCount
    {
        get;
        set;
    }

    public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        this.PageIndex = pageIndex;
        this.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        this.TotalCount = count;
        this.Items = items;
    }


    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }

    public static PaginatedList<T> CreateFromIEnumerable(IEnumerable<T> source, int pageIndex, int pageSize)
    {
        var count = source.Count();
        var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }

    public static PaginatedList<T> Create(List<T> source, int pageIndex, int pageSize)
    {
        var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        return new PaginatedList<T>(items, source.Count, pageIndex, pageSize);
    }
}
