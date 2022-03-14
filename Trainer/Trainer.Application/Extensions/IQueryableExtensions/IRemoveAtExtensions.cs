using Trainer.Domain.Interfaces;

namespace Trainer.Application.Extensions.IQueryableExtensions
{
    public static class IRemoveAtExtensions
    {
        public static IQueryable<T> NotRemoved<T>(this IQueryable<T> query) where T : IRemovedAt
        {
            return query.Where(x => !x.RemovedAt.HasValue);
        }
    }
}
