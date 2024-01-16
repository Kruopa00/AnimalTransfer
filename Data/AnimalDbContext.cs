using Microsoft.EntityFrameworkCore;

public class AnimalDbContext : DbContext
{
    public AnimalDbContext(DbContextOptions<AnimalDbContext> options) : base(options){}
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Enclosure> Enclosures { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>().HasKey(x => x.Id);
        modelBuilder.Entity<Enclosure>().HasKey(x => x.Id);
        modelBuilder.Entity<AnimalEnclosure>().HasKey(x => new{x.EnclosureId, x.AnimalId});
        modelBuilder.Entity<EnclosureObject>().HasKey(x => x.EnclosureId);

        modelBuilder.Entity<EnclosureObject>().ToTable("Objects");
        modelBuilder.Entity<AnimalEnclosure>().ToTable("AnimalEnclosures");

        modelBuilder.Entity<EnclosureObject>().HasOne(x => x.Enclosure).WithMany(x => x.Objects).HasForeignKey(x => x.EnclosureId);
    }

}