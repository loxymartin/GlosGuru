namespace GlosGuru.Api.DTOs;

public class WordListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<WordDto>? Words { get; set; }
}