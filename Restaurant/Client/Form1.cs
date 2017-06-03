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
using System.Threading;
using Restaurant.Models;
using Restaurant.Controllers;

namespace Client

{
    public partial class Form1 : Form
    {
        public HttpClient client;
        public int idClient;
        public int idOrder;
        public Form1(int idClient)
        {
            DateTime dt = DateTime.Now;
           this.idClient = 1;
           this.idOrder = dt.Year *100000 + dt.Month *1020 + dt.Day *10 + idClient;

            InitializeComponent();
            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:65268/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            generateNewOrder();
            

        }
        public void generateNewOrder()
        {
            MessageBox.Show("noua comanda");
            Order order = new Order();
            order.idOrder = this.idOrder;
            order.idClient = this.idClient;
            order.total = 0;
            order.date = DateTime.Now;
            order.idWaiter = 1;
            try
            {

                HttpResponseMessage response = client.PostAsJsonAsync("api/addOrder", order).Result;
                MessageBox.Show(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Insert status: success");
                }
                
            }
            catch (AggregateException exe)
            {
                MessageBox.Show(exe.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonAddProductToOrder_Click(object sender, EventArgs e)
        {
            if (dataGridViewProducts.SelectedRows.Count > 0)
            {
                Product product = dataGridViewProducts.SelectedRows[0].DataBoundItem as Product;
                int quant = 1;
                OrderDetails order = new OrderDetails();
                order.idOrder = this.idOrder;
                order.idDish = product.idDish;
                order.price = (int)product.price;
                order.quantity = quant;
                try
                {

                    HttpResponseMessage response = client.PostAsJsonAsync("api/AddOrderDetails", order).Result;
                    MessageBox.Show("insert");
                    MessageBox.Show(response.ToString());
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Insert status: success");
                        PopulateOrderDetails();
                    }

                }
                catch (AggregateException exe)
                {
                    MessageBox.Show(exe.Message);
                }
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
        private void PopulateOrderDetails()
        {
            MessageBox.Show("yes products");
            try
            {
                MessageBox.Show("incerc raspuns produse");
                HttpResponseMessage response = client.GetAsync("api/orderdetails/"+this.idOrder).Result;
                MessageBox.Show(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var orders = response.Content.ReadAsAsync<IEnumerable<OrderDetails>>().Result;
                    dataGridViewOrderDetails.DataSource = orders;

                }
            }
            catch (AggregateException exe)
            {
                MessageBox.Show(exe.Message);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateProducts();
            PopulateOrderDetails();
           // PopulateDailyOffer();
        }
    }
}
