using GlosGuru.Api.Model;

namespace GlosGuru.Api.Repositories;

public interface IWordListRepository
{
    Task<IEnumerable<WordList>> GetAll();
    Task<WordList?> GetById(int id);
    Task<WordList> Create(WordList wordList);
    Task<WordList?> Update(WordList wordList);
    Task<bool> Delete(int id);
}