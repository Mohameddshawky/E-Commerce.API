using AutoMapper;
using Domain.Contracts;
using Domain.Entites.ProductModule;
using Servces.Abstraction;
using Shared.DTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servieces
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _Mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
          var Brands=await  _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var res=_Mapper.Map<IEnumerable<BrandResultDto>>(Brands); 
            return res;
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            var res = _Mapper.Map<ProductResultDto>(product);
            return res;
        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            var res = _Mapper.Map<IEnumerable<ProductResultDto>>(products);
            return res;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var res = _Mapper.Map<IEnumerable<TypeResultDto>>(types);
            return res;
        }
    }
}
