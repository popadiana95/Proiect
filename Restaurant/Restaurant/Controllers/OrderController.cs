using Restaurant.Models;
using Restaurant.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Restaurant.Controllers
{
    public class OrderController : ApiController
    {
        // GET api/values

        [HttpGet]
        [Route("api/orders")]
        public IEnumerable<Order> GetOrders()
        {
            return OrderProvider.GetOrderList();
        }

        [HttpGet]
        [Route("api/orderDetails/{id}")]
        public IEnumerable<OrderDetails> GetOrderDetails(int id)
        {
            return OrderProvider.GetOrderDetailList(id);
        }
        [HttpPost]
        [Route("api/addOrder")]
        public void addOrder([FromBody]Order value)
        {
            OrderProvider.AddOrder(value);
        }
        [HttpPost]
        [Route("api/AddOrderDetails")]
        public void addOrderDetails([FromBody]OrderDetails value)
        {
            OrderProvider.AddOrderDetails(value);
        }

        [HttpPost]
        [Route("api/UpdateOrder")]
        public void UpdateOrder([FromBody]Order value)
        {
            OrderProvider.UpdateOrder(value);
        }
    }
}