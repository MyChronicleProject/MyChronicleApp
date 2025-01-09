using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyChronicle.Domain;

namespace MyChronicle.Infrastructure
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<FamilyTree> FamilyTrees { get; set; }

        public DbSet<Person> Persons { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<FamilyTreePermision> FamilyTreePermisions { get; set; }
        public DbSet<Domain.File> Files { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Relation> Relations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Relation>()
                .HasOne(r => r.Person_1)
                .WithMany(p => p.RelationsAsPerson1)
                .HasForeignKey(r => r.PersonId_1)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Relation>()
                .HasOne(r => r.Person_2)
                .WithMany(p => p.RelationsAsPerson2)
                .HasForeignKey(r => r.PersonId_2)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
                .Property(p => p.Gender)
                .HasDefaultValue(Gender.Unspecified);
        }

    }
}
