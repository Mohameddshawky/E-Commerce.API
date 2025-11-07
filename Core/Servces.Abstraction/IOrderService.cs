using Shared.DTos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IOrderService
    {
        Task<OrderResult>GetOrderByIdAsync(Guid orderId);
        Task<IEnumerable<OrderResult>>GetOrdersByEmailAsync(string userEmail);

        Task<OrderResult>CreateOrderAsync(OrderRequest order,string userEmil);
        Task<IEnumerable<DeliveryMethodResult>>GetDeliveryMethodsAsync();   

    }
}

