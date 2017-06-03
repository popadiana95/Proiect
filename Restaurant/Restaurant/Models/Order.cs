using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models
{
    public class Order
    {
        public int idOrder { get; set; }
        public int idClient { get; set; }
        public int idWaiter { get; set; }
        public DateTime date { get; set; }
        public float total { get; set; }
        public int status { get; set; }
    }
}