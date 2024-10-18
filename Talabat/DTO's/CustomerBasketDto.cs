using System.ComponentModel.DataAnnotations;

namespace Talabat.DTO_s
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get;}
    }
}
