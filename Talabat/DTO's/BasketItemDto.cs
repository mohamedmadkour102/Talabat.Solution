using System.ComponentModel.DataAnnotations;

namespace Talabat.DTO_s
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(0.1 , double.MaxValue , ErrorMessage ="Price Must Be Greater Than Zero")]
        public decimal Price { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Quantity Must Be at Least One!")]
        public int Quantity { get; set; }
    }
}
