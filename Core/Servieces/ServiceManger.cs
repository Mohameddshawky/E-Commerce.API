using AutoMapper;
using Domain.Contracts;
using Servces.Abstraction;
using Servieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManger(IUnitOfWork unitOfWork
        ,IMapper mapper,IBasketRepository basketRepository) : IServiceManger
    {
        private readonly Lazy<IProductService> _productService
            = new Lazy<IProductService>(() => new ProductService( unitOfWork,mapper));
        private readonly Lazy<IBasketService> _basketService
            = new Lazy<IBasketService>(() => new BasketService(basketRepository, mapper));
        public IProductService ProductService => _productService.Value;

        public IBasketService basketService => _basketService.Value;
    }
}
