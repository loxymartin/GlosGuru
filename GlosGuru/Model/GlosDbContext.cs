using Microsoft.EntityFrameworkCore;

namespace GlosGuru.Model;

public class GlosDbContext : DbContext
{
    public DbSet<WordList> WordLists { get; set; }
    public DbSet<Word> Words { get; set; }

    public string DbPath { get; }

    public GlosDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "glosguru.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
