using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTos.OrderModule
{
    public record OrderResult
    {
        public Guid Id { get; init; }
        public string UserEmail { get; init; } = string.Empty;
        public AddressDto ShippingAddress { get; init; }
        public ICollection<OrderItemDto> orderItems { get; init; } = new List<OrderItemDto>();
        public String PaymentStatus { get; init; } = string.Empty;
        public string DeliveryMethod { get; init; }=string.Empty;
        public int? DeliveryMethodID { get; init; }

        public decimal Subtotal { get; init; }
        public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.UtcNow;

        public string PaymentIntentId { get; init; } = string.Empty;

        public decimal Total { get; init; }
    }
}
