using DockerTestImage.Models;
using Microsoft.EntityFrameworkCore;

namespace DockerTestImage;

#nullable disable

/// <summary>
/// Create a Context for the API
/// </summary>
public class DatabaseHandle : DbContext
{
    /// <summary>
    /// Set of Posts
    /// </summary>
    public DbSet<Counter> Counters { get; set; }


    /// <summary>
    /// Set the DBPath to the ./database.db
    /// </summary>
    public DatabaseHandle(DbContextOptions<DatabaseHandle> Options) : base(Options)
    {
    }

    /// <summary>
    /// Initialization Method for the Database Handle.
    /// Initializes the DB with sample data in case the application is running in Dev mode.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}

#nullable restore
