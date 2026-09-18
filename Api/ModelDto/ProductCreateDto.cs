using System.ComponentModel.DataAnnotations;

namespace Api.ModelDto
{
    public class ProductCreateDto
    {
        [Required] // безательный 
        [MaxLength(200)]//макс сивалов 
        public string Name { get; set; }//имя продукта 

        [Required]// безательный 
        [MaxLength(1000)]//макс сивалов 
        public string Description { get; set; }//описания

        [Required]
        [MaxLength(50)]//макс сивалов 
        public string SpecialTag { get; set; }// Специальный тег

        [Required]// безательный 
        [MaxLength(100)]//макс сивалов 
        public string Category { get; set; }//категоря товара 
        
        [Required]// безательный 
        [Range(1, double.MaxValue, ErrorMessage = "Price must be positive")]// не маеньше нуля 
        public double Price { get; set; }// цена

        public string? Image { get; set; }// картинка может и небыть
    }
}