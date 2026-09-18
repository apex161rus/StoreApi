
using System.ComponentModel.DataAnnotations;

namespace Api.Model
{
    public class Product
    {
        [Key]
        public int Id {get;set;}

        [Required]
        [MaxLength(200)]
        public string Name {get;set;}

        [Required]
        [MaxLength(1000)]
        public string Description {get; set;}
        
        [Required]
        [MaxLength(50)]
        public string SpecialTag {get;set;}

        [Required]
        [MaxLength(100)]
        public string Category {get;set;}
        
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be positive")]
        public double Price {get;set;}

        public string? Image {get;set;}
    }
}