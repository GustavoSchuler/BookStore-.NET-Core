
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CoreBookStore.Models;

namespace CoreBookStore
{
    public class WebsiteDbContext : IdentityDbContext<ApplicationUser>
    {
        public WebsiteDbContext(DbContextOptions<WebsiteDbContext> options)
            : base(options)
        {

        }

        //public DbSet<Article> Articles { get; set; }
    }
}