using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Basket
    {
        public Basket()
        {
            Items = new List<Item>();
        }

        [Column("id")]
        public int Id { get; set; }

        [Column("customer_name")]
        public string CustomerName { get; set; }

        [Column("paysVAT")]
        public bool PaysVAT { get; set; }

        [Column("is_closed")]
        public bool IsClosed { get; set; }

        [Column("is_payed")]
        public bool IsPayed { get; set; }

        public List<Item> Items { get; set; }
    }
}
