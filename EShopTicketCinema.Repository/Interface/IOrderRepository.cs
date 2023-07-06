using EShopTicketCinema.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopTicketCinema.Repository.Interface
{
    public interface IOrderRepository
    {
        List<Order> getAllOrders();
        Order getOrderDetails(Guid id);
        Order getOrderDetails(BaseEntity id);
    }
}
