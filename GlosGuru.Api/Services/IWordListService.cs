using GlosGuru.Api.DTOs;

namespace GlosGuru.Api.Services;

public interface IWordListService
{
    Task<IEnumerable<WordListDto>> GetAll();
    Task<WordListDto?> GetById(int id);
    Task<WordListDto> Create(WordListDto wordListDto);
    Task<WordListDto?> Update(int id, WordListDto wordListDto);
    Task<bool> Delete(int id);
}