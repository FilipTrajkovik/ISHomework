using EShopTicketCinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopTicketCinema.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<TicketCinemaUser> GetAll();
        TicketCinemaUser Get(string id);
        void Insert(TicketCinemaUser entity);
        void Update(TicketCinemaUser entity);
        void Delete(TicketCinemaUser entity);
    }
}
