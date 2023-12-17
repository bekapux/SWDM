namespace Sawoodamo.API.Utilities.Extensions;

public static class IQuarableExtensions
{
    public static IQueryable<T> Paginate<T> (this IQueryable<T> query, int pageNumber, int itemsPerPage) where T : class
    {
        return query.Skip(pageNumber - 1).Take(itemsPerPage);
    }

    public static async Task<PaginatedListResult<TResult>> ToPaginatedListAsync<T, TResult>(
        this IQueryable<T> query,
        int pageNumber,
        int itemsPerPage,
        Func<T, TResult> mapper,
        CancellationToken cancellationToken)
    {
        var totalRecords = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling((double)totalRecords / itemsPerPage);
        var items = await query.Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToListAsync(cancellationToken);

        var resultList = items.Select(mapper).ToList();

        return new PaginatedListResult<TResult>
        {
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            ResultList = resultList
        };
    }
}
