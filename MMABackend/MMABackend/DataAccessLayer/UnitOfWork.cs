using Microsoft.EntityFrameworkCore;

namespace MMABackend.DataAccessLayer
{
    public partial class UnitOfWork : DbContext
    {
        public UnitOfWork(DbContextOptions<UnitOfWork> options) : base(options) {}
    }
}