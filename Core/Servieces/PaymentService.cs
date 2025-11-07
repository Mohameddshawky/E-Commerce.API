using AutoMapper;
using Domain.Contracts;
using Domain.Entites.OrderModule;
using Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using Services.Abstraction;
using Shared.DTos.BasketModule;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IProduct=Domain.Entites.ProductModule.Product; 
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
            StripeConfiguration.ApiKey = " ";//configuration.GetSection("StripeSettings")["SecretKey"];

            var Basket = await basketRepository.GetBasketAsync(BasketId)??throw new BasketNotFoundException(BasketId);

            foreach (var item in Basket.BasketItems)
            {
                 var product=    await unitOfWork.GetRepository<IProduct,int>().GetByIdAsync(item.Id)
                    ??throw new ProductNotFoundException(item.Id);

                item.Price = product.Price;

            }

            if (!Basket.DeliveryMethodId.HasValue)
                throw new Exception("No Delivery Method Selected");

            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(Basket.DeliveryMethodId.Value)??throw new DeliveryMethodNotFoundException(Basket.DeliveryMethodId.Value);

            Basket.ShippingPrice = deliveryMethod.Price;

            var total =(long)( Basket.BasketItems.Sum(i => i.Quantity * i.Price)+Basket.ShippingPrice)*100;

            var StripeService = new PaymentIntentService();

            
            if (String.IsNullOrEmpty(Basket.PaymentIntentId) )
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount=total
                    ,Currency="USD"
                    ,PaymentMethodTypes = ["Card"]
                    
                    
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
    }
}
