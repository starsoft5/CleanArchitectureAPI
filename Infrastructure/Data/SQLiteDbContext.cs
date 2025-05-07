using Core.Entities;
    using Microsoft.EntityFrameworkCore;

    namespace Infrastructure.Data {
        public class SQLiteDbContext : DbContext {
            public SQLiteDbContext(DbContextOptions<SQLiteDbContext> options) : base(options) { }

            public DbSet<Product> Products { get; set; }
        }
    }