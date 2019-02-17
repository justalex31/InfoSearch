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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=articlesdb;Username=postgres;Password=password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.2-servicing-10034");

            modelBuilder.Entity<Articles>(entity =>
            {
                entity.ToTable("articles");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.Keywords)
                    .HasColumnName("keywords")
                    .HasMaxLength(256);

                entity.Property(e => e.StudentId).HasColumnName("student_id");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(256);

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(256);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Articles)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("articles_student_id_fkey");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.ToTable("students");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Mygroup)
                    .HasColumnName("mygroup")
                    .HasMaxLength(6);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(32);

                entity.Property(e => e.Surname)
                    .HasColumnName("surname")
                    .HasMaxLength(32);
            });
        }
    }
}