using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using cmsSystem.Models; //inkluderar modeller

namespace cmsSystem.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

     public DbSet<News> News { get; set; }  = default!;
     public DbSet<About> About { get; set; }  = default!;
     public DbSet<Comment> Comment { get; set; }  = default!;
     public DbSet<Footer> Footer { get; set; }  = default!;
     public DbSet<Header> Header { get; set; }  = default!;
    public DbSet<Message> Message { get; set; }  = default!;
    public DbSet<Service> Service { get; set; }  = default!;
     public DbSet<Socials> Socials { get; set; }  = default!;
    public DbSet<cmsSystem.Models.Start> Start { get; set; } = default!;
    public DbSet<cmsSystem.Models.Widget> Widget { get; set; } = default!;
    public DbSet<cmsSystem.Models.Staff> Staff { get; set; } = default!;
}
