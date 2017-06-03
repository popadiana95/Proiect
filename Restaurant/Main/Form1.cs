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
using Client;
using System.Threading;
using Restaurant.Models;

namespace Main
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

        private void button1_Click(object sender, EventArgs e)
        {
            User user = new User();
            int id = 0;
            MessageBox.Show(textBox1.Text);
            id = Convert.ToInt32(textBox1.Text);
            user.idUser = id;
            try
            {
                HttpResponseMessage response = client.GetAsync("api/getUser/" + id).Result;
                // MessageBox.Show(response.ToString());
                if (response.IsSuccessStatusCode)
                {
                    var userLoged = response.Content.ReadAsAsync<User>().Result;
                    user = userLoged;
                    Security secure = new Security();
                    if (secure.VerifyHash(textBox2.Text, user.password))
                    {

                        MessageBox.Show("Welcome");
                        if (user.tip.Equals("c"))
                        {
                            Form FormC = new Client.Form1(id);
                            FormC.Show();
                        }
                        else
                            if (user.tip.Equals("w"))
                        {
                            Form FormW = new Waiter.Form1(id);
                            FormW.Show();
                            /*ThreadStart threadDelagate = new ThreadStart(DoctorClient.Program.getNotify);
                            Thread secThread = new Thread(threadDelagate);
                            secThread.Start();*/
                        }
                        else if(user.tip.Equals("s"))
                        {
                            Form FormS = new Chef.Form1();
                            FormS.Show();
                        }
                        else
                        {
                            Form FormA = new Administrator.Form1();                                                       
                            FormA.Show();
                        }

                    }
                    else
                        MessageBox.Show("Wrong password");
                }
                else
                {
                    MessageBox.Show("sorry");
                }
            }
            catch (AggregateException exe)
            {
                //MessageBox.Show(exe.Message);
            }
        }
    }
}
