using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MMABackend.DataAccessLayer;
using MMABackend.DomainModels.Common;

namespace MMABackend.Helpers.Common
{
    public static class DbContextExtensions
    {
        public static User GetUserByEmail(this UnitOfWork uow, string email)
        {
            return uow.Users.FirstOrDefault(x => x.Email == email) ??
                   throw new Exception("User not found by email");
        }
        
        public static string GetUserIdByEmailOrError(this UnitOfWork uow, string email)
        {
            return uow.Users.FirstOrDefault(x => x.Email == email)?.Id ??
                   throw new Exception("User not found by email");
        }
        
        public static T FirstOrError<T>(this IQueryable<T> collection, Predicate<T> predicate, string errorMessage = null)
        {
            return collection.FirstOrDefault(x=>predicate(x)) ??
                   throw new Exception(errorMessage ?? $"Not found entity {nameof(T)}");
        }
    }
}