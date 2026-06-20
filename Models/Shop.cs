using System.ComponentModel.DataAnnotations;

namespace MyMvcApp.Models
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }
        [Required (ErrorMessage = "Name is required")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        public string? Name { get; set; }
        [Required (ErrorMessage = "Category is required")]
        public string? Category { get; set; }
        [Required (ErrorMessage = "Location is required")]
        public string? Location { get; set; }
        public string? manager_name { get; set; }

        [MaxLength(200, ErrorMessage = "Description must be at most 200 characters long")]
        public string? description { get; set; }  
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}