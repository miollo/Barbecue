using BarbecueSpace.Models;
using Microsoft.EntityFrameworkCore;

namespace BarbecueSpace.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> opt) : base(opt)
        {
 
        }

        public DbSet<Barbecue> Barbecues { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<BarbecuePerson> BarbecuePeople { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeamento das chaves estrangeiras
            modelBuilder.Entity<BarbecuePerson>()
                .HasKey(bp => new { bp.BarbecueId, bp.PersonId });

            modelBuilder.Entity<BarbecuePerson>()
                .HasOne(bp => bp.Barbecue)
                .WithMany(b => b.BarbecuePeople)
                .HasForeignKey(bp => bp.BarbecueId);

            modelBuilder.Entity<BarbecuePerson>()
                .HasOne(bp => bp.Person)
                .WithMany(p => p.BarbecuePeople)
                .HasForeignKey(bp => bp.PersonId);
        }
    }
}
