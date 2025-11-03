using AutoMapper;
using Domain.Entites.OrderModule;
using Shared.DTos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    internal class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Address,AddressDto>().ReverseMap();   
            CreateMap<DeliveryMethod,DeliveryMethodResult>(); 
            
            CreateMap<OrderItem,OrderItemDto>()
                .ForMember(d=>d.ProductId,o=>o.MapFrom(s=>s.Product.ProductId))
                .ForMember(d=>d.PictureUrl,o=>o.MapFrom(s=>s.Product.PictureUrl))
                .ForMember(d=>d.ProductName,o=>o.MapFrom(s=>s.Product.ProductName))
                .ReverseMap();

            CreateMap<Order, OrderResult>()
                .ForMember(d => d.PaymentStatus, o => o.MapFrom(s => s.PaymentStatus.ToString()))
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.Total, o => o.MapFrom(s => s.Subtotal + s.DeliveryMethod.Price));

        }

    }
}
