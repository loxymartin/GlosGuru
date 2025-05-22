namespace GlosGuru.Web.Model;

using System.ComponentModel.DataAnnotations;

public class WordList
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public List<Word> Words { get; } = [];
}
