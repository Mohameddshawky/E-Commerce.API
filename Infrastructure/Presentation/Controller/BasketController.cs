using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servces.Abstraction;
using Shared.DTos.BasketModule;
using Shared.DTos.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [Authorize]
    public class BasketController(IServiceManger serviceManger):ApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]

        public async Task<ActionResult<BasketDto>> GetBasketAsync(string id)
        => Ok(await serviceManger.basketService.GetBasketAsync(id));

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket)
            => Ok(await serviceManger.basketService.CreateOrUpdateBasketAsync(basket));
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasketAsync(string id)
         {  await serviceManger.basketService.DeleteBasketAsync(id);
            return NoContent();//204

        }
    }
}
