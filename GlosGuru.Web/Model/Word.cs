namespace GlosGuru.Web.Model;

public class Word
{
    public int Id { get; set; }
    public string Lang1 { get; set; } = string.Empty;
    public string Lang2 { get; set; } = string.Empty;

    public int WordListId { get; set; }
}
