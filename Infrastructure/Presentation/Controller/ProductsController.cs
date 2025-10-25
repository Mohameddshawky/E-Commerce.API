using Microsoft.AspNetCore.Mvc;
using Servces.Abstraction;
using Shared;
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
    public class ProductsController(IServiceManger serviceManger):ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProductsAsync([FromQuery]ProductSpecificationsParameter parameter)
         =>Ok(await serviceManger.ProductService.GetAllProductsAsync(parameter));

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
