using Kata.BusinessModel.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kata.ORM.EntityFramework
{
    public class DataDbContext : DbContext
    {
        // Field for Db connection string
        private string _connString;
        DataDbContext()
        { }

        // Contructor passing Db connection
        public DataDbContext(string connString)
        {
            _connString = connString;
        }

        // Configuring the DbContext by Fluent API
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connString);
            base.OnConfiguring(optionsBuilder);
        }

        // Mapping to Db Entities        
        public DbSet<Driver> Driver { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<Trip> Trip { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            // -------------Driver-------------------

            // Entity
            modelBuilder.Entity<Driver>().ToTable("Driver");
            // PK
            modelBuilder.Entity<Driver>()
               .HasKey(c => new { c.DriverId });
            // Fields
            modelBuilder.Entity<Driver>().Property(t => t.DriverId).HasColumnName("DriverId");
            modelBuilder.Entity<Driver>().Property(t => t.DriverName).HasColumnName("DriverName");
            // PK
            modelBuilder.Entity<Driver>().Property(t => t.FileId).HasColumnName("FileId");
            // Navegation Properties  
            modelBuilder.Entity<Driver>().HasOne(t => t.File);
            modelBuilder.Entity<Driver>().HasMany(t => t.Trips).WithOne(t => t.Driver);



            // -------------File-------------------

            // Entity
            modelBuilder.Entity<File>().ToTable("File");
            // PK
            modelBuilder.Entity<File>()
               .HasKey(c => new { c.FileId });
            // Fields
            modelBuilder.Entity<File>().Property(t => t.FileId).HasColumnName("FileId");
            modelBuilder.Entity<File>().Property(t => t.FileName).HasColumnName("FileName");
            // Navegation Properties            
            modelBuilder.Entity<File>().HasMany(t => t.Drivers).WithOne(t => t.File);
            modelBuilder.Entity<File>().HasMany(t => t.Trips).WithOne(t => t.File);



            // -------------Trip-------------------

            // Entity
            modelBuilder.Entity<Trip>().ToTable("Trip");
            // PK
            modelBuilder.Entity<Trip>()
               .HasKey(c => new { c.TripId });
            // Fields
            modelBuilder.Entity<Trip>().Property(t => t.TripId).HasColumnName("TripId");
            modelBuilder.Entity<Trip>().Property(t => t.StartDate).HasColumnName("StartDate");
            modelBuilder.Entity<Trip>().Property(t => t.EndDate).HasColumnName("EndDate");
            modelBuilder.Entity<Trip>().Property(t => t.Miles).HasColumnName("Miles");
            modelBuilder.Entity<Trip>().Property(t => t.AvgMph).HasColumnName("AvgMph");
            modelBuilder.Entity<Trip>().Property(t => t.TripDuration).HasColumnName("TripDuration");
            // Foreign Keys            
            modelBuilder.Entity<Trip>().Property(t => t.DriverId).HasColumnName("DriverId");
            modelBuilder.Entity<Trip>().Property(t => t.FileId).HasColumnName("FileId");
            // Navegation Properties            
            modelBuilder.Entity<Trip>().HasOne(t => t.Driver);
            modelBuilder.Entity<Trip>().HasOne(t => t.File);


        }
    }
}
