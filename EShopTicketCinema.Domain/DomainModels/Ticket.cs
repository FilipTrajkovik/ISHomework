using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EShopTicketCinema.Domain.DomainModels
{
    public class Ticket : BaseEntity
    {
        [Required]
        public string MovieName { get; set; }
        [Required]
        public string MovieDescription { get; set; }
        [Required]
        public string MovieImage { get; set; }
        [Required]
        public int MoviePrice { get; set; }
        [Required]
        public double MovieRating { get; set; }
        [Required]
        public int MovieDuration { get; set; }
        [Required]
        public string MovieGenre { get; set; }
        public virtual ICollection<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public IEnumerable<TicketInOrder> TicketInOrders { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime DateValid { get; set; }

    }
}
