using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs;

public class CustomerBasketDto
{
    [Required]
    public string Id { get; set; }

    [Required]
    public List<BasketItemDto> Items { get; set; }
}
