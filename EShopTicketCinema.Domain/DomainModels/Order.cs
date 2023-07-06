using EShopTicketCinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopTicketCinema.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public TicketCinemaUser User { get; set; }

        public IEnumerable<TicketInOrder> TicketInOrders { get; set; }
    }
}
