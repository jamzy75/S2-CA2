using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace S2_CA2.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Book Book { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public IdentityUser User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}