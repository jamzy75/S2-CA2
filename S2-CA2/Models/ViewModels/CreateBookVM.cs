namespace S2_CA2.Models.ViewModels;

public class CreateBookVM
{
    public int AuthorId { get; set; }

    public string Title { get; set; }

    public string Isbn { get; set; }

    public DateTime PublishedDate { get; set; }

    public string Genre { get; set; }
}