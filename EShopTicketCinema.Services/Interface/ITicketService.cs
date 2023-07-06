using EShopTicketCinema.Domain.DomainModels;
using EShopTicketCinema.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopTicketCinema.Services.Interface
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        List<Ticket> GetAllTicketsWithGenre(string Genre);
        List<Ticket> GetAllTicketsByDate(DateTime Date);
        Ticket GetDetailsForTicket(Guid? id);
        void CreateNewTicket(Ticket p);
        void UpdeteExistingTicket(Ticket p);
        AddToShoppingCardDto GetShoppingCartInfo(Guid? id);
        void DeleteTicket(Guid id);
        bool AddToShoppingCart(AddToShoppingCardDto item, string userID);
        
    }
}
