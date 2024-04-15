using System;
using System.Linq;
using System.Linq.Expressions;

namespace MMABackend.DataAccessLayer.Extensions
{
    public static partial class DbContextExtensions
    {
        public static void RemoveWhere<T>(this UnitOfWork uow, Expression<Func<T, bool>> expression) where T : class
        {
            var list = uow.Set<T>().Where(expression).ToList();
            uow.RemoveRange(list);
            uow.SaveChanges();
        }
    }
}