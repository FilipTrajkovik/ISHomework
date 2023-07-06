using EShopTicketCinema.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopTicketCinema.Domain.DTO
{
    public class AddToShoppingCardDto
    {
        public Ticket SelectedTicket { get; set; }
        public Guid TicketId { get; set; }
        public int Quantity { get; set; }
    }
}
