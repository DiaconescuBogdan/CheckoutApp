using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class Item
    {
        /*public Item()
        {
            Baskets = new List<Basket>();
        }*/

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        /*public List<Basket> Baskets { get; set; }*/
    }
}
