using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIDevSteamJau.Data
{
    public class APIContext : IdentityDbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        { }
        // DbSet

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Tabelas

        }
    }
}
