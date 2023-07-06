using EShopTicketCinema.Domain.DomainModels;
using EShopTicketCinema.Domain.DTO;
using EShopTicketCinema.Repository.Interface;
using EShopTicketCinema.Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShopTicketCinema.Services.Implementation
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IRepository<TicketInShoppingCart> _ticketInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<TicketService> _logger;
        public TicketService(IRepository<Ticket> ticketRepository, ILogger<TicketService> logger, IRepository<TicketInShoppingCart> ticketInShoppingCartRepository, IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
            _ticketInShoppingCartRepository = ticketInShoppingCartRepository;
            _logger = logger;
        }

        public bool AddToShoppingCart(AddToShoppingCardDto item, string userID)
        {

            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.UserCart;

            if (item.TicketId != null && userShoppingCard != null)
            {
                var ticket = this.GetDetailsForTicket(item.TicketId);

                if (ticket != null)
                {
                    TicketInShoppingCart itemToAdd = new TicketInShoppingCart
                    {
                        Id = Guid.NewGuid(),
                        Ticket = ticket,
                        TicketId = ticket.Id,
                        ShoppingCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    this._ticketInShoppingCartRepository.Insert(itemToAdd);
                    _logger.LogInformation("Ticket was successfully added into ShoppingCart");
                    return true;
                }
                return false;
            }
            _logger.LogInformation("Something was wrong. TicketId or UserShoppingCard may be unaveliable!");
            return false;
        }

        public List<Ticket> GetAllTicketsWithGenre(string Genre)
        {
            return _ticketRepository.GetAll().Where(z => z.MovieGenre.Equals(Genre)).ToList();
        }

        public List<Ticket> GetAllTicketsByDate(DateTime date)
        {
            return _ticketRepository.GetAll().Where(z => z.DateValid.CompareTo(date) > 0).ToList();
        }

        public void CreateNewTicket(Ticket t)
        {
            this._ticketRepository.Insert(t);
        }

        public void DeleteTicket(Guid id)
        {
            var ticket = this.GetDetailsForTicket(id);
            this._ticketRepository.Delete(ticket);
        }

        public List<Ticket> GetAllTickets()
        {
            _logger.LogInformation("GetAllTicket was called!");
            return this._ticketRepository.GetAll().ToList();
        }

        public Ticket GetDetailsForTicket(Guid? id)
        {
            return this._ticketRepository.Get(id);
        }
        public Ticket GetDetailsForTicket(Guid id)
        {
            return this._ticketRepository.Get(id);
        }

        public AddToShoppingCardDto GetShoppingCartInfo(Guid? id)
        {
            var ticket = this.GetDetailsForTicket(id);
            AddToShoppingCardDto model = new AddToShoppingCardDto
            {
                SelectedTicket = ticket,
                TicketId = ticket.Id,
                Quantity = 1
            };

            return model;
        }

        public void UpdeteExistingTicket(Ticket t)
        {
            this._ticketRepository.Update(t);
        }
    }
}
