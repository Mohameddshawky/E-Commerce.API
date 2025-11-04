using Domain.Entites.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class OrderWithIncludeSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderWithIncludeSpecifications(Guid id) 
            : base(a=> a.Id== id)
        {
            AddInclude(o => o.orderItems);
            AddInclude(o => o.DeliveryMethod);
        }
        public OrderWithIncludeSpecifications( string UserEmail)
         : base(a => a.UserEmail == UserEmail
         )
        {
            AddInclude(o => o.orderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderBy(o => o.OrderDate);
        }


    }
}
