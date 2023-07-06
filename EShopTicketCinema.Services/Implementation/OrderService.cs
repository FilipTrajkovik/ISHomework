using EShopTicketCinema.Domain.DomainModels;
using EShopTicketCinema.Repository.Interface;
using EShopTicketCinema.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopTicketCinema.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public List<Order> getAllOrders()
        {
            return this._orderRepository.getAllOrders();
        }

        public Order getOrderDetails(Guid id)
        {
            return this._orderRepository.getOrderDetails(id);
        }
        public Order getOrderDetails(BaseEntity id)
        {
            return this._orderRepository.getOrderDetails(id);
        }
    }
}
