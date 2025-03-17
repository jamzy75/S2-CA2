using System.ComponentModel.DataAnnotations;

namespace S2_CA2.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Bio { get; set; }

        public DateTime Birthdate { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();
    }
}
