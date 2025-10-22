using Microsoft.AspNetCore.Mvc;
using Servces.Abstraction;
using Shared.DTos;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServiceManger serviceManger):ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResultDto>>> GetAllProductsAsync(ProductSortingOptions sort)
         =>Ok(await serviceManger.ProductService.GetAllProductsAsync(sort));

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandsAsync()
        => Ok(await serviceManger.ProductService.GetAllBrandsAsync());

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypesAsync()
        => Ok(await serviceManger.ProductService.GetAllTypesAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TypeResultDto>> GetProductByIdAsync(int id)
        => Ok(await serviceManger.ProductService.GetProductByIdAsync(id));


    }
}
