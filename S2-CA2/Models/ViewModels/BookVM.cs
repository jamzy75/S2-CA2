using Microsoft.AspNetCore.Mvc.Rendering;

namespace S2_CA2.Models.ViewModels;

public class BookVM : Book
{
    public BookVM()
    {
        AuthorsList = null;
    }

    public BookVM(Book book, SelectList authorsList)
    {
        Id = book.Id;
        Author = book.Author;
        Title = book.Title;
        Isbn = book.Isbn;
        PublishedDate = book.PublishedDate;
        Genre = book.Genre;
        AuthorsList = authorsList;
    }

    public SelectList? AuthorsList { get; set; }

    public int AuthorId { get; set; }
}