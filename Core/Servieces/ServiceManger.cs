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
    public class ServiceManger(IUnitOfWork unitOfWork,IMapper mapper) : IServiceManger
    {
        private readonly Lazy<IProductService> _productService
            = new Lazy<IProductService>(() => new ProductService( unitOfWork,mapper));
        public IProductService ProductService => _productService.Value;
    }
}
