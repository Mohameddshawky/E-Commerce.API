using System.ComponentModel.DataAnnotations;

namespace Shared.DTos.BasketModule
{
    public record BasketItemDto
    {
        public int Id { get; init; }
        public string ProductName { get; init; } = null!;
        public string PictureUrl { get; init; } = null!;
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]   
        public decimal Price { get; init; }
        [Range(1, 99, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; init; }
    }
}