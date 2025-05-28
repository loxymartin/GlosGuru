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
        var existing = await context.WordLists
            .Include(wl => wl.Words)
            .FirstOrDefaultAsync(wl => wl.Id == wordList.Id);

        if (existing == null)
            return null;

        existing.Name = wordList.Name;

        // Update Words: Remove deleted, update existing, add new
        // Remove words not in the updated list
        var updatedWordIds = wordList.Words.Select(w => w.Id).ToHashSet();
        var wordsToRemove = existing.Words.Where(w => !updatedWordIds.Contains(w.Id)).ToList();
        foreach (var word in wordsToRemove)
        {
            context.Words.Remove(word);
        }

        // Update or add words
        foreach (var word in wordList.Words)
        {
            var existingWord = existing.Words.FirstOrDefault(w => w.Id == word.Id && word.Id != 0);
            if (existingWord != null)
            {
                existingWord.Lang1 = word.Lang1;
                existingWord.Lang2 = word.Lang2;
            }
            else
            {
                // New word
                existing.Words.Add(new Word
                {
                    Lang1 = word.Lang1,
                    Lang2 = word.Lang2,
                    WordListId = existing.Id
                });
            }
        }

        await context.SaveChangesAsync();
        return existing;
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
