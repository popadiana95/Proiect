using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant.Models
{
    public class Product
    {
        public int idDish { get; set; }
        public string dish { get; set; }
        public string description { get; set; }
        public float price { get; set; }
        public int tip { get; set; }
    }
}