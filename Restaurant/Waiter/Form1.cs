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
using Restaurant.Models;
using System.Threading;

namespace Waiter
{
    public partial class Form1 : Form
    {
        public HttpClient client;
        public int idWaiter;
        public Form1(int idWaiter)
        {
            this.idWaiter = idWaiter;         

            client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:65268/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ThreadStart threadDelagate = new ThreadStart(CheckNewOrder);
            Thread secThread = new Thread(threadDelagate);
            secThread.Start();
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            int idOrder = Convert.ToInt32(textBoxIdOrder.Text);
            Order order = new Order();
            order.idOrder = idOrder;
            order.status = 2;
            MessageBox.Show("update try" + idOrder);

            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync("api/UpdateOrder", order).Result;
                MessageBox.Show(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Update status: success");

                }

            }
            catch (AggregateException exe)
            {
                MessageBox.Show(exe.Message);
            }
        }

        private void CheckNewOrder()
        {
            MessageBox.Show("yes check");
            try
            {
               
                HttpResponseMessage response = client.GetAsync("api/orders").Result;
                MessageBox.Show(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var orders = response.Content.ReadAsAsync<IEnumerable<Order>>().Result;
                    List<Order> orderList = (List<Order>)orders;
                    for (int i = 0; i < orderList.Count; i++)
                        if (orderList[i].idWaiter == this.idWaiter && orderList[i].status == 1)
                            MessageBox.Show("A sosit comanda " + orderList[i].idOrder);
                }
            }
            catch (AggregateException exe)
            {
                MessageBox.Show(exe.Message);
            }
            System.Threading.Thread.Sleep(100);
        }
    }
    
}
