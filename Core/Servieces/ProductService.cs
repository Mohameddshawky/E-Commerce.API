using AutoMapper;
using Domain.Contracts;
using Domain.Entites.ProductModule;
using Servces.Abstraction;
using Services.Specifications;
using Shared;
using Shared.DTos;
using Shared.Enums;
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
            var specification = new ProductWithBrandAndTypeSpecifications(id);

            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specification);
            var res = _Mapper.Map<ProductResultDto>(product);
            return res;
        }

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameter parameter)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(parameter);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specifications);
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
