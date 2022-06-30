using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class BasketItem
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("basket_id")]
        public int BasketId { get; set; }

        [Column("item_id")]
        public int ItemId { get; set; }
    }
}
