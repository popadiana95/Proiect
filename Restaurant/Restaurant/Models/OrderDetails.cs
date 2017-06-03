using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models
{
    public class OrderDetails
    {
        public int idOrder { get; set; }
        public int idDish { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
    }
}