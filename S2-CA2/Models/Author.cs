using System.ComponentModel.DataAnnotations;

namespace S2_CA2.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; } = null!;

        [StringLength(1000)]
        public string? Bio { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        public virtual List<Book> Books { get; set; } = [];
    }
}
