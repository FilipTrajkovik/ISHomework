using EShopTicketCinema.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopTicketCinema.Services.Interface
{
    public interface IOrderService
    {
        List<Order> getAllOrders();

        Order getOrderDetails(Guid id);
        Order getOrderDetails(BaseEntity id);
    }
}
