using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication9.Models;

namespace WebApplication9.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{

    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }


    public DbSet<Villa> Villas { get; set; }
}
