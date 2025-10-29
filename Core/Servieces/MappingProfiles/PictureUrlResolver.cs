using AutoMapper;
using Domain.Entites.ProductModule;
using Microsoft.Extensions.Configuration;
using Shared.DTos.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    internal class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductResultDto, string>
    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if(string.IsNullOrEmpty(source.PictureUrl))  return null!;

            return $"{configuration.GetSection("URLS")["BaseURL"]}{source.PictureUrl}";
        }
    }
}
