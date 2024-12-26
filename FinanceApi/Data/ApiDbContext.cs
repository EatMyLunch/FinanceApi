using FinanceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
