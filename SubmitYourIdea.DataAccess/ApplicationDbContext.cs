using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SubmitYourIdea.DataAccess.Entities;

namespace SubmitYourIdea.DataAccess;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext(options)
{
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Idea> Ideas { get; set; }
        public DbSet<Category> Categories { get; set; }
}
