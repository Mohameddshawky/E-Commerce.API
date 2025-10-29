using AutoMapper;
using Domain.Contracts;
using Domain.Entites.BasketModule;
using Domain.Exceptions;
using Servces.Abstraction;
using Shared.DTos.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketService (IBasketRepository basketRepository,IMapper mapper): IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var basketm=mapper.Map<CustomerBasket>(basket);
            var CorUBasket=await basketRepository.CreateOrUpdateBasketAsync(basketm);
            return CorUBasket is null? throw new Exception("Problem with create or update basket"):
                       mapper.Map<BasketDto>(CorUBasket);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        =>await basketRepository.DeleteBasketAsync(id);

        public async Task<BasketDto> GetBasketAsync(string id)
        {
            var basket= await basketRepository.GetBasketAsync(id);
            return basket is null? throw new BasketNotFoundException(id):
                       mapper.Map<BasketDto>(basket);

        }
    }
}
