using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.DTos.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    public class PaymentsController(IServiceManger serviceManger) : ApiController
    {
        [HttpPost("{Basketid}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string Basketid)
        =>Ok( await serviceManger.PaymentService.CreateOrUpdatePaymentIntentAsync(Basketid));

        [HttpPost("WebHook")]
        public async Task<IActionResult> WebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
           
           
            var signatureHeader = Request.Headers["Stripe-Signature"];

            await serviceManger.PaymentService.UpdatePaymentStatusAsync(json, signatureHeader);

            return new EmptyResult();

        }
    }
}
