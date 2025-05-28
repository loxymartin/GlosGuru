using GlosGuru.Api.DTOs;
using GlosGuru.Api.Model;
using GlosGuru.Api.Repositories;

namespace GlosGuru.Api.Services;

public class WordListService(IWordListRepository repository) : IWordListService
{
    public async Task<IEnumerable<WordListDto>> GetAll()
    {
        var wordLists = await repository.GetAll();
        return wordLists.Select(MapToDto);
    }

    public async Task<WordListDto?> GetById(int id)
    {
        var wordList = await repository.GetById(id);
        return wordList == null ? null : MapToDto(wordList);
    }

    public async Task<WordListDto> Create(WordListDto wordListDto)
    {
        var wordList = MapToEntity(wordListDto);
        var createdWordList = await repository.Create(wordList);
        return MapToDto(createdWordList);
    }

    public async Task<WordListDto?> Update(int id, WordListDto wordListDto)
    {
        wordListDto.Id = id;
        // Ensure all Words have the correct WordListId
        if (wordListDto.Words != null)
        {
            foreach (var word in wordListDto.Words)
            {
                word.WordListId = id;
            }
        }
        var wordList = MapToEntity(wordListDto);
        var updatedWordList = await repository.Update(wordList);
        return updatedWordList == null ? null : MapToDto(updatedWordList);
    }

    public async Task<bool> Delete(int id)
    {
        return await repository.Delete(id);
    }

    private static WordListDto MapToDto(WordList wordList)
    {
        return new WordListDto
        {
            Id = wordList.Id,
            Name = wordList.Name,
            Words = wordList.Words?.Select(w => new WordDto
            {
                Id = w.Id,
                Lang1 = w.Lang1,
                Lang2 = w.Lang2,
                WordListId = w.WordListId
            }).ToList()
        };
    }

    private static WordList MapToEntity(WordListDto dto)
    {
        return new WordList
        {
            Id = dto.Id,
            Name = dto.Name,
            Words = dto.Words?.Select(w => new Word
            {
                Id = w.Id,
                Lang1 = w.Lang1,
                Lang2 = w.Lang2,
                WordListId = w.WordListId
            }).ToList() ?? []
        };
    }
}
