using Microsoft.EntityFrameworkCore;
using UnitOfWorkDemo.Models;

namespace UnitOfWorkDemo.Data
{
    public class ApplicationDbContext : DbContext
    {

        public virtual DbSet<User> Users {get; set;}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        // This method helps bootstrap the tables in the in memory db
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}