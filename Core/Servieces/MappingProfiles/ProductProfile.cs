using AutoMapper;
using Domain.Entites.ProductModule;
using Services.MappingProfiles;
using Shared.DTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servieces.MappingProfiles
{
    internal class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductType, TypeResultDto>();
            CreateMap<ProductBrand, BrandResultDto>();

            CreateMap<Product, ProductResultDto>().
                ForMember(p => p.BrandName,
                o=>o.MapFrom(s=>s.ProductBrand.Name)).
                ForMember(p => p.TypeName,
                o => o.MapFrom(s => s.ProductType.Name)).
                ForMember(p=>p.PictureUrl,o=>o.MapFrom<PictureUrlResolver>());
        }
    }
}
