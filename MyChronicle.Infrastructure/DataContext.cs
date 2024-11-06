using Microsoft.EntityFrameworkCore;
using MyChronicle.Domain;

namespace MyChronicle.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<FamilyTree> FamilyTree { get; set; }

        public DbSet<Person> Persons { get; set; }
        public DbSet<AkcessToken> AkcessTokens { get; set; }
        public DbSet<FamilyTreePermision> FamilyTreePermisions { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Relations> Relations { get; set; }
        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Relations>()
                .HasOne(r => r.Person_1)
                .WithMany(p => p.RelationsAsPerson1)
                .HasForeignKey(r => r.PersonId_1)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Relations>()
                .HasOne(r => r.Person_2)
                .WithMany(p => p.RelationsAsPerson2)
                .HasForeignKey(r => r.PersonId_2)
                .OnDelete(DeleteBehavior.Restrict); 
        }

    }
}
