using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S2_CA2.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        public string Genre { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
