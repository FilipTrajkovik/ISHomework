using EShopTicketCinema.Domain.DomainModels;
using EShopTicketCinema.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Text;

namespace EShopTicketCinema.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {

        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }


        public List<Order> getAllOrders()
        {
            return entities
                .Include(z => z.User)
                .Include(z => z.TicketInOrders)
                .Include("TicketInOrders.OrderedTicket")
                .ToListAsync().Result;
        }

        public Order getOrderDetails(Guid id)
        {
            return entities
               .Include(z => z.User)
               .Include(z => z.TicketInOrders)
               .Include("TicketInOrders.OrderedTicket")
               .SingleOrDefaultAsync(z => z.Id == id).Result;
        }

        public Order getOrderDetails(BaseEntity model)
        {
            return entities
               .Include(z => z.User)
               .Include(z => z.TicketInOrders)
               .Include("TicketInOrders.OrderedTicket")
               .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }
    }
}
