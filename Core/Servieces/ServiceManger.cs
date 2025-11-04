using AutoMapper;
using Domain.Contracts;
using Domain.Entites.IdentitiyModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Servces.Abstraction;
using Servieces;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManger(IUnitOfWork unitOfWork
        ,IMapper mapper
        ,IBasketRepository basketRepository
        ,ICacheRepository cacherepository
        ,UserManager<User> userManager
        ,IOptions<JwtOptions> options
        ) : IServiceManger
    {
        private readonly Lazy<IProductService> _productService
            = new Lazy<IProductService>(() => new ProductService( unitOfWork,mapper));
        private readonly Lazy<IBasketService> _basketService
            = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        private readonly Lazy<ICacheService> _cacheService
            = new Lazy<ICacheService>(() => new CacheService(cacherepository));
        private readonly Lazy<IAuthenticationService> _authenticationService
            = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager,options));
        private readonly Lazy<IOrderService>_orderService
            = new Lazy<IOrderService>(() => new OrderService(mapper,basketRepository,unitOfWork));
        public IProductService ProductService => _productService.Value;

        public IBasketService basketService => _basketService.Value;
         
        public ICacheService cacheService => _cacheService.Value;

        public IAuthenticationService authenticationService =>_authenticationService.Value;

        public IOrderService orderService => _orderService.Value;   
    }
}
