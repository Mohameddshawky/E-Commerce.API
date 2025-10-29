using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Servces.Abstraction;
using Shared;
using Shared.DTos.ProductModule;
using Shared.Enums;
using Shared.Error_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
   
    public class ProductsController(IServiceManger serviceManger):ApiController
    {


        [ProducesResponseType(typeof(IEnumerable<ProductResultDto>), StatusCodes.Status200OK)]

        [HttpGet]
        [Cache(100)]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProductsAsync([FromQuery]ProductSpecificationsParameter parameter)
         =>Ok(await serviceManger.ProductService.GetAllProductsAsync(parameter));



        [ProducesResponseType(typeof(IEnumerable<BrandResultDto>), StatusCodes.Status200OK)]

        [HttpGet("Brands")]
        [Cache(100)]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandsAsync()
        => Ok(await serviceManger.ProductService.GetAllBrandsAsync());




        [ProducesResponseType(typeof(IEnumerable<TypeResultDto>), StatusCodes.Status200OK)]

        [HttpGet("Types")]
        [Cache(100)]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypesAsync()
        => Ok(await serviceManger.ProductService.GetAllTypesAsync());


        [ProducesResponseType(typeof(TypeResultDto),StatusCodes.Status200OK)]

        [HttpGet("{id:int}")]
        [Cache(100)]
        public async Task<ActionResult<TypeResultDto>> GetProductByIdAsync(int id)
        => Ok(await serviceManger.ProductService.GetProductByIdAsync(id));


    }
}
