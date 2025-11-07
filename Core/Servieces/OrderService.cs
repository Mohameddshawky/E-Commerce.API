using AutoMapper;
using Domain.Contracts;
using Domain.Entites.BasketModule;
using Domain.Entites.OrderModule;
using Domain.Entites.ProductModule;
using Domain.Exceptions;
using Services.Abstraction;
using Services.Specifications;
using Shared.DTos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class OrderService(
        IMapper mapper
        , IBasketRepository basketRepository
        ,IUnitOfWork unitOfWork                                                                                                 
        ) : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequest order, string userEmil)
        {
            var Address = mapper.Map<Address>(order.ShipToAddress);

            var Basket =await basketRepository.GetBasketAsync(order.BasketId)
                ?? throw new BasketNotFoundException(order.BasketId);
            
            var orderItems = new List<OrderItem>();
            foreach (var item in Basket.Items)
            {
                var product =await unitOfWork.GetRepository<Product, int>()
                    .GetByIdAsync(item.Id)??throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(product, item));  
            }

            var OrderRepo = unitOfWork.GetRepository<Order, Guid>();

            var deliverymethod=await unitOfWork.GetRepository<DeliveryMethod,int>()
                .GetByIdAsync(order.DeliveryMethodID)
                ??throw new DeliveryMethodNotFoundException(order.DeliveryMethodID);

            var orderExist =await OrderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecification(Basket.PaymentIntentId));

            if (orderExist != null)
            {
                OrderRepo.Delete(orderExist);
                

            }
            decimal subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            var Order=new Order(userEmil,Address,orderItems,deliverymethod,subtotal,Basket.PaymentIntentId);
            await OrderRepo.AddAsync(Order);
            await unitOfWork.SaveChangesAsync(); 
            var orderResult = mapper.Map<OrderResult>(Order);
            return orderResult;

        }

        private OrderItem CreateOrderItem(Product product, BasketItem item)
        {
            ProductInOrderItem productInOrder = new ProductInOrderItem(product.Id, product.Name, product.PictureUrl);
            OrderItem orderItem = new OrderItem
            {
                Product = productInOrder,
                Price = product.Price,
                Quantity = item.Quantity
            };
            return orderItem;
        }

        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods=await unitOfWork.GetRepository<DeliveryMethod,int>()
                .GetAllAsync();
            var ans = mapper.Map < IEnumerable < DeliveryMethodResult >> (DeliveryMethods);
            return ans;
        }

        public async Task<OrderResult> GetOrderByIdAsync(Guid orderId)
        {
            var Specification = new OrderWithIncludeSpecifications(orderId);
            var order = await unitOfWork.GetRepository<Order, Guid>()
                .GetByIdAsync(Specification)??throw new OrderNotFoundException(orderId);
            return mapper.Map<OrderResult>( order);
        }

        public async Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string userEmail)
        {
            var Specification = new OrderWithIncludeSpecifications(userEmail);
            var order = await unitOfWork.GetRepository<Order, Guid>()
                .GetAllAsync(Specification);
            return mapper.Map<IEnumerable<OrderResult>>(order);
        }
    }
}
