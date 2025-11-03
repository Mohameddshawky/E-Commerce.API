using Domain.Entites.ProductModule;

namespace Domain.Entites.OrderModule
{
    public class OrderItem:BaseEntity<Guid>                                      
    {
        public OrderItem(ProductInOrderItem product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }
        public OrderItem()
        {
            
        }

        public ProductInOrderItem Product { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}