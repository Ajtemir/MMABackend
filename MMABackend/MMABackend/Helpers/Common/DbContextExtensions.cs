using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;

namespace MMABackend.Helpers.Common
{
    public static class DbContextExtensions
    {
        public static User GetUserByEmailOrError(this UnitOfWork uow, string email)
        {
            return uow.Users.FirstOrDefault(x => x.Email == email) ??
                   throw new Exception("User not found by email");
        }
        
        public static string GetUserIdByEmailOrError(this UnitOfWork uow, string email)
        {
            return uow.Users.FirstOrDefault(x => x.Email == email)?.Id ??
                   throw new Exception("User not found by email");
        }
        
        public static T FirstOrError<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, string errorMessage = null) where T: class
        {
            var elem = source.FirstOrDefault(predicate);
            if(elem == null) throw new Exception(errorMessage ?? $"Not found entity {nameof(T)}");
            return elem;
        }
    }
}