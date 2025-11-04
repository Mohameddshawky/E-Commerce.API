using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servces.Abstraction
{
    public interface IServiceManger
    {
        public IProductService ProductService { get; }
        public IBasketService  basketService { get; }
        public ICacheService cacheService { get; }
        public IAuthenticationService authenticationService { get; }
        public IOrderService orderService { get; }


    }
}
