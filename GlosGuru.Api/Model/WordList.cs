namespace GlosGuru.Api.Model;

public class WordList
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Word> Words { get; set; } = [];
}