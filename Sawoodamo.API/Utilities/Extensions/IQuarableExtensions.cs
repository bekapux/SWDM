namespace Sawoodamo.API.Utilities.Extensions;

public static class IQuarableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int itemsPerPage) where T : class
    {
        return query
            .Skip(itemsPerPage * (pageNumber - 1))
            .Take(itemsPerPage);
    }

    public static async Task<PaginatedListResult<TResult>> ToPaginatedListAsync<T, TResult>(
        this IQueryable<T> query,
        int pageNumber,
        int itemsPerPage,
        Func<T, TResult> mapper,
        CancellationToken cancellationToken)
    {
        int totalRecords = await query.CountAsync(cancellationToken);
        int totalPages = (int)Math.Ceiling((double)totalRecords / itemsPerPage);
        IQueryable<T> items = query
            .Skip((pageNumber - 1) * itemsPerPage)
            .Take(itemsPerPage);

        var resultList = await items.Select(x => mapper(x)).ToListAsync(cancellationToken: cancellationToken);

        return new PaginatedListResult<TResult>
        {
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            ResultList = resultList
        };
    }
}
