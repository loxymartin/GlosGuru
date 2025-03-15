using Microsoft.EntityFrameworkCore;

namespace GlosGuru.Api.Model;

public class GlosGuruContext(DbContextOptions<GlosGuruContext> options) : DbContext(options)
{
    public DbSet<WordList> WordLists { get; set; }
    public DbSet<Word> Words { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<Word>()
            .HasOne(w => w.WordList)
            .WithMany(wl => wl.Words)
            .HasForeignKey(w => w.WordListId);
    }
}