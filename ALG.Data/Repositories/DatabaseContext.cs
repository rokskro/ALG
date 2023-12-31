using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// import the Entities (database models representing structure of tables in database)
using ALG.Data.Entities; 

namespace ALG.Data.Repositories
{
    // The Context is How EntityFramework communicates with the database
    // We define DbSet properties for each table in the database
    public class DatabaseContext : DbContext
    {
        public DbSet<Algorithm> Algorithms { get; set; }  
        //public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        //{
        //}

        // Configure the context with logging - remove in production
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder
            .UseSqlite("Filename=data.db")
            //.LogTo(Console.WriteLine, LogLevel.Information)
            ;            
        }

        public static DbContextOptionsBuilder<DatabaseContext> OptionsBuilder => new ();

        // Convenience method to recreate the database thus ensuring the new database takes 
        // account of any changes to Models or DatabaseContext. ONLY to be used in development
        public void Initialise()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

    }
}
