
using Restaurant.Models;
using Restaurant.Controllers;
using Restaurant.Providers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Administrator
{
    public partial class Form1 : Form
    {
        public HttpClient client;
        public Form1()
        {
            InitializeComponent();
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:65268/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        
        public User getUser()
        {
            User user = new User();
            user.idUser = Convert.ToInt32(this.textBoxIdUser.Text);
            user.password = this.textBoxPassUser.Text;
            user.tip = this.textBoxTypeUser.Text;
            return user;
        }

        private void PopulateUsers()
        {
            MessageBox.Show("yes");
           try
            {
                MessageBox.Show("incerc raspuns");
                HttpResponseMessage response = client.GetAsync("api/users").Result;
                MessageBox.Show(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var users = response.Content.ReadAsAsync<IEnumerable<User>>().Result;
                    dataGridViewUsers.DataSource = users;

                }
            }
            catch (AggregateException exe)
            {
                MessageBox.Show(exe.Message);
           }
        }

        private void PopulateClients()
        {
            MessageBox.Show("yes client");
            try
            {
                MessageBox.Show("incerc raspuns client");
                HttpResponseMessage response = client.GetAsync("api/clients").Result;
                MessageBox.Show(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var users = response.Content.ReadAsAsync<IEnumerable<User>>().Result;

                    dataGridViewClients.DataSource = users;

                }
            }
            catch (AggregateException exe)
            {
                MessageBox.Show(exe.Message);
            }
        }
        private void PopulateWaiters()
        {
            MessageBox.Show("yes waiter");
            try
            {
                MessageBox.Show("incerc raspuns waiter");
                HttpResponseMessage response = client.GetAsync("api/waiters").Result;
                MessageBox.Show(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var users = response.Content.ReadAsAsync<IEnumerable<User>>().Result;

                    dataGridViewWaiters.DataSource = users;

                }
            }
            catch (AggregateException exe)
            {
                MessageBox.Show(exe.Message);
            }
        }
        private void PopulateProducts()
        {
            MessageBox.Show("yes products");
            try
            {
                MessageBox.Show("incerc raspuns produse");
                HttpResponseMessage response = client.GetAsync("api/products").Result;
                MessageBox.Show(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                    dataGridViewProducts.DataSource = products;

                }
            }
            catch (AggregateException exe)
            {
                MessageBox.Show(exe.Message);
            }
        }
        private void buttonAddUser_Click(object sender, EventArgs e)
        {

                User user = this.getUser();
                try
                {

                    HttpResponseMessage response = client.PostAsJsonAsync("api/addUser", user).Result;
                    MessageBox.Show(response.ToString());
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Insert status: success");
                    }
                    PopulateUsers();
                }
                catch (AggregateException exe)
                {
                    MessageBox.Show(exe.Message);
                }
            MessageBox.Show("ai apasat");
            PopulateUsers();
        }

        private void buttonUpdateUser_Click(object sender, EventArgs e)
        {

            User user = this.getUser();
            MessageBox.Show("update try"+user.idUser);
                      
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/updateUser", user).Result;
                MessageBox.Show(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Update status: success");
                    PopulateUsers();
                }
                
            }
            catch (AggregateException exe)
            {
                MessageBox.Show(exe.Message);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            
            User user = this.getUser();
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/deleteUser", user).Result;
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Update status: success");
                }
                PopulateUsers();
            }
            catch (AggregateException exe)
            {
                MessageBox.Show(exe.Message);
            }
        }

        private void dataGridViewUsers_CellContentClick(object sender, EventArgs e)
        {
            
            if (dataGridViewUsers.SelectedRows.Count > 0)
            {
                User user = dataGridViewUsers.SelectedRows[0].DataBoundItem as User;
                if (user != null)
                {

                    this.textBoxIdUser.Text = user.idUser.ToString();
                    this.textBoxPassUser.Text = user.password;
                    this.textBoxTypeUser.Text = user.tip;

                }
            }
        }
        private List<Order> getOrdersForClient(int idClient)
        {
            List<Order> ordersList = new List<Order>();
            try
            {
                MessageBox.Show("incerc sa obtin comenzile");
                HttpResponseMessage response = client.GetAsync("api/orders").Result;
                MessageBox.Show(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var orders = response.Content.ReadAsAsync<IEnumerable<Order>>().Result;
                    ordersList = (List<Order>)orders;
                }
            }
            catch (AggregateException exe)
            {
                MessageBox.Show(exe.Message);
            }
            List<Order> ordersForClient = new List<Order>();
            for (int i = 0; i < ordersList.Count(); i++)
                if (ordersList[i].idClient == idClient)
                    ordersForClient.Add(ordersList[i]);
            return ordersForClient;
        }
        private List<Order> getOrdersForWaiters(int idWaiter)
        {
            List<Order> ordersList = new List<Order>();
            try
            {
                MessageBox.Show("incerc sa obtin comenzile");
                HttpResponseMessage response = client.GetAsync("api/orders").Result;
                MessageBox.Show(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var orders = response.Content.ReadAsAsync<IEnumerable<Order>>().Result;
                    ordersList = (List<Order>)orders;
                }
            }
            catch (AggregateException exe)
            {
                MessageBox.Show(exe.Message);
            }
            List<Order> ordersForWaiter = new List<Order>();
            for (int i = 0; i < ordersList.Count(); i++)
                if (ordersList[i].idWaiter == idWaiter)
                    ordersForWaiter.Add(ordersList[i]);
            return ordersForWaiter;
        }
        private void dataGridViewClients_CellContentClick(object sender, EventArgs e)
        {

            if (dataGridViewClients.SelectedRows.Count > 0)
            {
                User user = dataGridViewClients.SelectedRows[0].DataBoundItem as User;
                if (user != null)
                {
                    dataGridViewOrdersForClient.DataSource = this.getOrdersForClient(user.idUser);

                }
            }
        }
        private void dataGridViewWaiters_CellContentClick(object sender, EventArgs e)
        {

            if (dataGridViewWaiters.SelectedRows.Count > 0)
            {
                User user = dataGridViewWaiters.SelectedRows[0].DataBoundItem as User;
                if (user != null)
                {
                    dataGridViewOrdersForWaiter.DataSource = this.getOrdersForWaiters(user.idUser);

                }
            }
        }
        private void Form1_LoadUsers(object sender, EventArgs e)
        {
            PopulateUsers();
        }
        
        private List<Order> getOrdersForWaiter(string idWaiter, List<Order> ordersList)
        {
            List<Order> ordersForClient = new List<Order>();
            for (int i = 0; i < ordersList.Count(); i++)
                if (ordersList[i].idWaiter.Equals( idWaiter))
                    ordersForClient.Add(ordersList[i]);
            return ordersForClient;
        }

        private void dataGridViewClients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          /*  if (dataGridViewClients.SelectedRows.Count > 0)
            {
                User user = dataGridViewClients.SelectedRows[0].DataBoundItem as User;
                if (user != null)
                {
                    try
                    {
                        MessageBox.Show("incerc raspuns");
                        HttpResponseMessage response = client.GetAsync("api/orders").Result;
                        MessageBox.Show(response.ToString());
                        if (response.IsSuccessStatusCode)
                        {
                            var orders = response.Content.ReadAsAsync<IEnumerable<Order>>().Result;
                            List<Order> ordersForClient = this.getOrdersForClient(user.idUser, (List<Order>)orders);
                            dataGridViewOrdersForClient.DataSource = ordersForClient;

                        }
                    }
                    catch (AggregateException exe)
                    {
                        MessageBox.Show(exe.Message);
                    }
                }
            }*/
        }
        private void buttonSend_Click(object sender, EventArgs e)
        {
            MessageBox.Show("mail");
            if (dataGridViewClients.SelectedRows.Count > 0)
            {
                User user = dataGridViewClients.SelectedRows[0].DataBoundItem as User;
                MessageBox.Show(user.idUser.ToString());
                if (user != null)
                {
                    try
                    {
                        HttpResponseMessage response = client.GetAsync("api/getUserMail/" + user.idUser).Result;
                        MessageBox.Show(response.ToString());
                        if (response.IsSuccessStatusCode)
                        {
                            var userLoged = response.Content.ReadAsAsync<Client>().Result;
                            string emailTo = userLoged.mail;
                            Console.Write(emailTo);
                            try
                            {
                                MailMessage mail = new MailMessage();
                                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                                mail.From = new MailAddress("diana.m.popa95@gmail.com");
                                mail.To.Add(emailTo);
                                mail.Subject = "Test Mail";
                                Random rand = new Random();
                                int x = rand.Next(1, 999999);
                                mail.Body = "Felicitari. Ai primit un card cadou la DD Restaurant. Codul de folosire este " + x;

                                SmtpServer.Port = 587;
                                SmtpServer.Credentials = new System.Net.NetworkCredential("diana.m.popa95@gmail.com", "pass");
                                SmtpServer.EnableSsl = true;

                                SmtpServer.Send(mail);
                                MessageBox.Show("mail Send");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                    catch (AggregateException exe)
                    {
                        MessageBox.Show(exe.Message);
                    }
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hello");
            try
            {
              
                HttpResponseMessage response = client.GetAsync("api/getorders").Result;
                MessageBox.Show(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var users = response.Content.ReadAsAsync<IEnumerable<Order>>().Result;
                    dataGridViewProducts.DataSource = users;

                }
            }
            catch (AggregateException exe)
            {
                MessageBox.Show(exe.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateUsers();
            PopulateClients();
            PopulateProducts();
            PopulateWaiters();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Product p = new Product();
                HttpResponseMessage response = client.PostAsJsonAsync("api/addOffert", p).Result;
                MessageBox.Show(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Insert status: success");
                }
                PopulateProducts();
            }
            catch (AggregateException exe)
            {
                MessageBox.Show(exe.Message);
            }
        }
    }
}
