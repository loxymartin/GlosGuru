using Microsoft.EntityFrameworkCore;

namespace GlosGuru.Model;

public class GlosContext : DbContext
{
    public DbSet<WordList> WordLists { get; set; }
    public DbSet<Word> Words { get; set; }

    public string DbPath { get; }

    public GlosContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "glosguru.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
