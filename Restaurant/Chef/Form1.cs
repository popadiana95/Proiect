
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

namespace Chef
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

        private void buttonready_Click(object sender, EventArgs e)
        {
            int idOrder = Convert.ToInt32(textBoxIdOrder.Text);
            Order order = new Order();
            order.idOrder = idOrder;
            order.status = 1;
            MessageBox.Show("update try" +idOrder);

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
    }
}
