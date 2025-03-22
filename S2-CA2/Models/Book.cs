using System.ComponentModel.DataAnnotations;

namespace S2_CA2.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Author Author { get; set; } = null!;

        [Required]
        [StringLength(60)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(17)]
        public string Isbn { get; set; } = null!;

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        public string Genre { get; set; } = null!;

        public List<Review> Reviews { get; set; } = [];
    }
}
