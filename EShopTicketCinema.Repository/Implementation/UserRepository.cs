using EShopTicketCinema.Domain.Identity;
using EShopTicketCinema.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

namespace EShopTicketCinema.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<TicketCinemaUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<TicketCinemaUser>();
        }
        public IEnumerable<TicketCinemaUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public TicketCinemaUser Get(string id)
        {
            return entities
               .Include(z => z.UserCart)
               .Include("UserCart.TicketInShoppingCarts")
               .Include("UserCart.TicketInShoppingCarts.Ticket")
               .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(TicketCinemaUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(TicketCinemaUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(TicketCinemaUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
