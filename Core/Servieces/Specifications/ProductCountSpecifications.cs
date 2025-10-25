using Domain.Entites.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class ProductCountSpecifications: BaseSpecifications<Product, int>
    {
        public ProductCountSpecifications(ProductSpecificationsParameter parameter)
            : base(
                p => (!parameter.TypeId.HasValue || p.TypeId == parameter.TypeId) &&
                     (!parameter.BrandId.HasValue || p.BrandId == parameter.BrandId)
                && (string.IsNullOrEmpty(parameter.Search) || p.Name.ToLower().Contains(parameter.Search.ToLower()))
            )
        {
        }       
    }
}
