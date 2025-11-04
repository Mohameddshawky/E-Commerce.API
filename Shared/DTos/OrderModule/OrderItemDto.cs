using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTos.OrderModule
{
    public record OrderItemDto
    {
        public int ProductId { get; init; }
        public string ProductName { get; init; } = null!;
        public string PictureUrl { get; init; } = null!;

        public decimal Price { get; init; }
        public int Quantity { get; init; }
    }
}
