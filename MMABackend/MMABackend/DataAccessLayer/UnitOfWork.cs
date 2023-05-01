using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MMABackend.DomainModels.Common;

namespace MMABackend.DataAccessLayer
{
    public partial class UnitOfWork : IdentityDbContext<User>
    {
        public UnitOfWork(DbContextOptions<UnitOfWork> options) : base(options) {}
    }
}