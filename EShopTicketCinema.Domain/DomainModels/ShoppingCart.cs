using EShopTicketCinema.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShopTicketCinema.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity 
    { 
    
        public string OwnerId { get; set; }
        public TicketCinemaUser Owner { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
    }
}
