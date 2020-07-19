using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CheuwoAPI.Models;

namespace CheuwoAPI.Models
{
    public class ApiContext : IdentityDbContext<User>
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<CheuwoAPI.Models.Offer> Offer { get; set; }
    }
}
