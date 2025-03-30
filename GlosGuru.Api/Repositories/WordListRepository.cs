namespace GlosGuru.Api.Repositories;

using GlosGuru.Api.Model;
using Microsoft.EntityFrameworkCore;

public class WordListRepository(GlosGuruContext context) : IWordListRepository
{
    public async Task<IEnumerable<WordList>> GetAll() => await context.WordLists
            .Include(wl => wl.Words)
            .ToListAsync();

    public async Task<WordList?> GetById(int id) => await context.WordLists
            .Include(wl => wl.Words)
            .FirstOrDefaultAsync(wl => wl.Id == id);

    public async Task<WordList> Create(WordList wordList)
    {
        context.WordLists.Add(wordList);
        await context.SaveChangesAsync();
        return wordList;
    }

    public async Task<WordList?> Update(WordList wordList)
    {
        var existingWordList = await context.WordLists.FindAsync(wordList.Id);
        if (existingWordList == null)
        {
            return null;
        }

        context.Entry(existingWordList).CurrentValues.SetValues(wordList);
        await context.SaveChangesAsync();
        return existingWordList;
    }

    public async Task<bool> Delete(int id)
    {
        var wordList = await context.WordLists.FindAsync(id);
        if (wordList == null)
        {
            return false;
        }

        context.WordLists.Remove(wordList);
        await context.SaveChangesAsync();
        return true;
    }
}
