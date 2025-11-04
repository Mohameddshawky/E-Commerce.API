using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servces.Abstraction;
using Shared.DTos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    [Authorize]
    public class OrderController(IServiceManger serviceManger) : ApiController
    {
        [HttpPost]
    
        public async Task<ActionResult<OrderResult>> CreateOrderAsync(OrderRequest orderResult)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await serviceManger.orderService.CreateOrderAsync(orderResult, userEmail);
            return Ok(order);
        }
        [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods = await serviceManger.orderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<OrderResult>> GetOrderByIdAsync(Guid Id)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await serviceManger.orderService.GetOrderByIdAsync(Id);
            //if (order.UserEmail != userEmail)
            //{
            //    return Unauthorized();
            //}
            return Ok(order);

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetAllOrderByIdAsync()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await serviceManger.orderService.GetOrdersByEmailAsync(userEmail);
            return Ok(order);
        }
    }
}
