using Domain.Entites.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShippingAddress=Domain.Entites.OrderModule.Address;                                   
namespace Domain.Entites.OrderModule
{
    public class Order:BaseEntity<Guid>
    {
        public string UserEmail { get; set; } = string.Empty;
        public ShippingAddress ShippingAddress { get; set; }
        public ICollection<OrderItem> orderItems { get; set; } = new List<OrderItem>();
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;

        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodID { get; set; }

        public decimal Subtotal { get; set; }
        public DateTimeOffset TimeOffset { get; set; } = DateTimeOffset.UtcNow;

        public string PaymentIntentId { get; set; } = string.Empty;
       // public decimal GetTotal()=> Subtotal + (DeliveryMethod?.Price ?? 0);
        
    }

}
