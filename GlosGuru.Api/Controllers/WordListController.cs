using GlosGuru.Api.DTOs;
using GlosGuru.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GlosGuru.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WordListController(IWordListService wordListService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WordListDto>>> GetAll()
    {
        var wordLists = await wordListService.GetAll();
        return Ok(wordLists);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WordListDto>> GetById(int id)
    {
        var wordList = await wordListService.GetById(id);
        
        if (wordList == null)
            return NotFound($"WordList with ID {id} not found");
            
        return Ok(wordList);
    }

    [HttpPost]
    public async Task<ActionResult<WordListDto>> Create(WordListDto wordListDto)
    {
        var createdWordList = await wordListService.Create(wordListDto);
        return CreatedAtAction(nameof(GetById), new { id = createdWordList.Id }, createdWordList);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<WordListDto>> Update(int id, WordListDto wordListDto)
    {
        var updatedWordList = await wordListService.Update(id, wordListDto);
        
        if (updatedWordList == null)
            return NotFound($"WordList with ID {id} not found");
            
        return Ok(updatedWordList);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await wordListService.Delete(id);
        
        if (!result)
            return NotFound($"WordList with ID {id} not found");
            
        return NoContent();
    }
}