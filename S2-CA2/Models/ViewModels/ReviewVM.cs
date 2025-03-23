namespace S2_CA2.Models.ViewModels;

public class ReviewVM
{
    public int BookId { get; set; }

    public string Content { get; set; } = string.Empty;

    public int Rating { get; set; }
}