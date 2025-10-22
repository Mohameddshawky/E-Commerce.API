using Domain.Entites.ProductModule;
using Shared;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithBrandAndTypeSpecifications(ProductSpecificationsParameter parameter) : 
            base(
                p=> (!parameter.TypeId.HasValue || p.TypeId == parameter.TypeId) &&
                     (!parameter.BrandId.HasValue || p.BrandId == parameter.BrandId)
            )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            switch(parameter.sort)
            {
                case ProductSortingOptions.nameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.nameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case ProductSortingOptions.priceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.priceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                default:                  
                    break;
            }
        }
        public ProductWithBrandAndTypeSpecifications(int id):base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
