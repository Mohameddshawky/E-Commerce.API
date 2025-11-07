using Domain.Entites.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class OrderWithPaymentIntentIdSpecification
        :BaseSpecifications<Order,Guid>
    {
        public OrderWithPaymentIntentIdSpecification(string paymentid)
            :base(e=>e.PaymentIntentId==paymentid)
        {
            
        }
    }
}
