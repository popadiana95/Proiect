using Restaurant.Models;
using Restaurant.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Restaurant.Controllers
{
    public class ProductController : ApiController
    {
        // GET api/values
        [HttpGet]
        [Route("api/products")]
        public IEnumerable<Product> Get()
        {
            return ProductProvider.GetProductList();
        }

        [HttpPost]
        [Route("api/addOffert")]
        public void AddPost([FromBody]Product value)
        {
            ProductProvider.generateOffert();
        }
    }
    
}