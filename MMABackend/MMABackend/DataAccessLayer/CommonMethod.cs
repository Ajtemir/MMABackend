using MMABackend.DomainModels.Common;
using MMABackend.Helpers.Common;

namespace MMABackend.DataAccessLayer
{
    public static partial class DataSeeding
    {
        private static void Execute<T>(this UnitOfWork uow, params T[] parameters) where T : class
        {
            uow.Set<T>().AddRange(parameters);
            uow.SaveChangesWithIdentityInsert<T>();
        }
    }
}