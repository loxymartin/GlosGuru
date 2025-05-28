namespace GlosGuru.Web.Dto;

using System.ComponentModel.DataAnnotations;

public class WordListDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;

    public List<WordDto> Words { get; set; } = [];
}
