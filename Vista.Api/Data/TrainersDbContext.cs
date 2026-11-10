using Microsoft.EntityFrameworkCore;
namespace Vista.Api.Data
{
    public class TrainersDbContext : DbContext
    {
        // Notes
        // - DbSet defines the database table.
        // - the class name is defined as part of the data model
        // - the instance/object name is normally plural
        // - by default, the instance name will become the table name
        public DbSet<Category> Categories { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<TrainerCategory> TrainerCategories { get; set; }
        public DbSet<Session> Sessions { get; set; }
        private string DbPath { get; set; } = string.Empty;
        // Constructor to set-up the database path & name
        public TrainersDbContext()
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "vista.trainers.db");
        }
        // OnConfiguring to specify that the SQLite database engine will be used
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Composite (Compound) Key
            builder.Entity<TrainerCategory>()
            .HasKey(ts => new { ts.TrainerId, ts.CategoryCode });
            // EF Core can work out most relationships from the navigation properties
            // One "Category" to many "TrainerCategories" needs to be defined because it does
            // NOT have a “conventional” primary column name (i.e. CategoryId) and is based
            // on a Composite key
            builder.Entity<Category>()
            .HasMany(c => c.TrainerCategories)
            .WithOne(tr => tr.Category)
            .HasForeignKey(tr => tr.CategoryCode)
            .OnDelete(DeleteBehavior.Restrict);
            // The OnDelete prevents categories from being deleted if there are
            // corresponding entities within the TrainerCategories
            // Manually define the Trainer to TrainerCategories relationship to
            // enforce the delete rule.
            builder.Entity<Trainer>()
            .HasMany(c => c.TrainerCategories)
            .WithOne(tr => tr.Trainer)
            .OnDelete(DeleteBehavior.Restrict);
            // Insert Seed/Test Data
            builder.Entity<Category>().HasData(
            new Category { CategoryCode = "IT", CategoryName = "Information Technology" },
            new Category { CategoryCode = "HS", CategoryName = "Health and Safety" },
            new Category { CategoryCode = "LM", CategoryName = "Leadership and Management" },
            new Category { CategoryCode = "ED", CategoryName = "Equality and Diversity" },
            new Category { CategoryCode = "MAR", CategoryName = "Marketing" }
            );
        }
    }
}