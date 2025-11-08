using AutoMapper;
using Domain.Contracts;
using Domain.Entites.OrderModule;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction;
using Services.Specifications;
using Shared.DTos.BasketModule;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using IProduct=Domain.Entites.ProductModule.Product; 
using Order=Domain.Entites.OrderModule.Order;
namespace Services
{
    public class PaymentService
        (
        IConfiguration configuration
        ,IBasketRepository basketRepository
        ,IUnitOfWork unitOfWork
        ,IMapper mapper
        ) 
        : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string BasketId)
        {
            StripeConfiguration.ApiKey =configuration.GetSection("StripeSettings")["SecretKey"];

            var Basket = await basketRepository.GetBasketAsync(BasketId)??throw new BasketNotFoundException(BasketId);

            foreach (var item in Basket.Items)
            {
                 var product=    await unitOfWork.GetRepository<IProduct,int>().GetByIdAsync(item.Id)
                    ??throw new ProductNotFoundException(item.Id);

                item.Price = product.Price;
                item.ProductName = product.Name;    
                item.PictureUrl= product.PictureUrl;    
               

            }

            if (!Basket.DeliveryMethodId.HasValue)
                throw new Exception("No Delivery Method Selected");

            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(Basket.DeliveryMethodId.Value)??throw new DeliveryMethodNotFoundException(Basket.DeliveryMethodId.Value);

            Basket.ShippingPrice = deliveryMethod.Price;

            var total =(long)( Basket.Items.Sum(i => i.Quantity * i.Price)+Basket.ShippingPrice)*100;

            var StripeService = new PaymentIntentService();

            
            if (String.IsNullOrEmpty(Basket.PaymentIntentId) )
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount=total
                    ,Currency="USD",

                    PaymentMethodTypes = new List<string> { "card" }


                };

               var  paymentIntent= await StripeService.CreateAsync(options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;

            }
            else
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = total
                };

                var paymentIntent = await StripeService.UpdateAsync(Basket.PaymentIntentId,options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            await basketRepository.CreateOrUpdateBasketAsync(Basket);
            return mapper.Map<BasketDto>(Basket);

            //check
        }

        public async Task UpdatePaymentStatusAsync(string json, string signtaureHeader)
        {
              string endpointSecret = configuration.GetSection("StripeSettings")["endpointSecret"]!;
           
                var stripeEvent = EventUtility.ParseEvent(json,throwOnApiVersionMismatch:false);
                stripeEvent = EventUtility.ConstructEvent(json, signtaureHeader, endpointSecret, throwOnApiVersionMismatch: false);
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                // Handle the event
                // If on SDK version < 46, use class Events instead of EventTypes
                if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
                {
                await UpdatePaymentStatusRecievedAsync(paymentIntent.Id);
                 
                }
                else if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
                {
                 await UpdatePaymentStatusFailedAsync(paymentIntent.Id);

                 }
            // ... handle other event types
            else
                {
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
     
        }

        private async Task UpdatePaymentStatusFailedAsync(string id)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(new OrderWithPaymentIntentIdSpecification(id));
            if(order is not null)
            {
                order.PaymentStatus = OrderPaymentStatus.Failed;
                unitOfWork.GetRepository<Order, Guid>().Update(order);
                await unitOfWork.SaveChangesAsync();
            }    
        }

        private async Task UpdatePaymentStatusRecievedAsync(string id)
        {
            var order = await unitOfWork.GetRepository<Order, Guid>()
               .GetByIdAsync(new OrderWithPaymentIntentIdSpecification(id));
            if (order is not null)
            {
                order.PaymentStatus = OrderPaymentStatus.Completed;
                unitOfWork.GetRepository<Order, Guid>().Update(order);
                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}
