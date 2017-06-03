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
    public class UserController : ApiController
    {
        // GET api/values
        [HttpGet]
        [Route("api/users")]
        public IEnumerable<User> Get()
        {
            return UserProvider.GetUsersList();
        }
        [HttpGet]
        [Route("api/clients")]
        public IEnumerable<User> GetClients()
        {
            return UserProvider.GetClientsList();
        }

        [HttpGet]
        [Route("api/waiters")]
        public IEnumerable<User> GetWaiters()
        {
            return UserProvider.GetWaitersList();
        }
        [HttpGet]
        [Route("api/getUser/{id}")]
        public User Get(int id)
        {
            return UserProvider.GetUser(id);
        }
        [HttpGet]
        [Route("api/getUserMail/{id}")]
        public Client GetMail(int id)
        {
            return UserProvider.GetUserMail(id);
        }
        // POST api/values
        // POST api/values
        [HttpPost]
        [Route("api/updateUser")]
        public void UpdatePost([FromBody]User value)
        {
            UserProvider.UpdateUser(value);
        }


        [HttpPost]
        [Route("api/addUser")]
        public void AddPost([FromBody]User value)
        {
            UserProvider.AddUser(value);
        }

        [HttpPost]
        [Route("api/deleteUser")]
        public void DeltePost([FromBody]User value)
        {
            UserProvider.DeleteUser(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}