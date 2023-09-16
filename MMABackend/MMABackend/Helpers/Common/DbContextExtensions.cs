using System;
using System.Collections;
using System.Collections.Generic;
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
                   throw new ApplicationException("User not found by email");
        }
        
        public static string GetUserIdByEmailOrError(this UnitOfWork uow, string email)
        {
            return uow.Users.FirstOrDefault(x => x.Email == email)?.Id ??
                   throw new ApplicationException("User not found by email");
        }
        
        public static T FirstOrError<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, string errorMessage = null) where T: class
        {
            var elem = source.FirstOrDefault(predicate);
            if(elem == null) throw new ApplicationException(errorMessage ?? $"Not found entity {nameof(T)}");
            return elem;
        }
        
        public static void ErrorIfExists<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, string errorMessage = null) where T: class
        {
            var elem = source.FirstOrDefault(predicate);
            if(elem != null) throw new ApplicationException(errorMessage ?? $"Entity exists {nameof(T)}");
        }
        
        public static void ErrorIfNotExists<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, string errorMessage = null) where T: class
        {
            var elem = source.FirstOrDefault(predicate);
            if(elem == null) throw new ApplicationException(errorMessage ?? $"Entity exists {nameof(T)}");
        }
        
        public static bool Exists<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, string errorMessage = null) where T: class
        {
            var elem = source.FirstOrDefault(predicate);
            return elem != null;
        }
        
        public static bool Exists<T>(this IEnumerable<T> source, Func<T, bool> predicate, string errorMessage = null) where T: class
        {
            var elem = source.FirstOrDefault(predicate);
            return elem != null;
        }

        public static Category GetCategoryPropertyAndValuesById(this UnitOfWork uow, int categoryId)
        {
            return uow.Categories
                .Include(x => x.PropertyKeys)
                .ThenInclude(x => x.PropertyValues)
                .FirstOrError(x => x.Id == categoryId);
        }
    }
}