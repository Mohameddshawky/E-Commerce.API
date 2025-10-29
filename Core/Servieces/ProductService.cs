using AutoMapper;
using Domain.Contracts;
using Domain.Entites.ProductModule;
using Domain.Exceptions;
using Servces.Abstraction;
using Services.Specifications;
using Shared;
using Shared.DTos.ProductModule;
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
            //var res = _Mapper.Map<ProductResultDto>(product);
            //return res;
            return product is null? throw new ProductNotFoundException(id):
                _Mapper.Map<ProductResultDto>(product);
        }

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParameter parameter)
        {
            var ProductRepo=_unitOfWork.GetRepository<Product, int>();
            var specifications = new ProductWithBrandAndTypeSpecifications(parameter);
            var products = await ProductRepo.GetAllAsync(specifications);
            var res = _Mapper.Map<IEnumerable<ProductResultDto>>(products);
            var count= await ProductRepo.CountAsync(new ProductCountSpecifications(parameter));
            return new PaginatedResult<ProductResultDto>(parameter.PageIndex,res.Count(),count,res);
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var res = _Mapper.Map<IEnumerable<TypeResultDto>>(types);
            return res;
        }
    }
}
