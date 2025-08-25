using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Entity.Contexts
{
    public class ExternalDbContext : DbContext
    {
        public ExternalDbContext(DbContextOptions<ExternalDbContext> options)
            : base(options) { }

        public DbSet<ExternalItem> ExternalItems => Set<ExternalItem>();
    }

}
