using InfoSearch.Model;
using Microsoft.EntityFrameworkCore;

namespace InfoSearch.AppData
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Articles> Articles { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Words_MyStem> Words_MyStems { get; set; }
        public DbSet<Words_Porter> Words_Porters { get; set; }
        public DbSet<Article_Term> Article_Terms { get; set; }
        public DbSet<Term_List> Term_Lists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=articlesdb;Username=postgres;Password=password");
        }
    }
}