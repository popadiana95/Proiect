using MySql.Data.MySqlClient;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace Restaurant.Providers
{
    public class OrderProvider
    {
        public static string connString;

        public static IEnumerable<Order> GetOrderList()
        {
            List<Order> OrdersList = new List<Order>();
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM orders";
                MySqlCommand cmd = new MySqlCommand(statement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Order order = new Models.Order();
                        order.idOrder = reader.GetInt32("idOrder");
                        order.idClient = reader.GetInt32("idClient");
                        order.idWaiter = reader.GetInt32("idWaiter");
                        order.date = reader.GetDateTime("date");
                        order.total = reader.GetFloat("total");
                        order.status = reader.GetInt32("status");
                        OrdersList.Add(order);
                    }
                }
                conn.Close();
            }

            return OrdersList;
        }
        
        
        public static void AddOrder(Order order)
        {

            
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddOrder";
                
                cmd.Parameters.Add(new MySqlParameter("idOrder",order.idOrder));
                cmd.Parameters.Add(new MySqlParameter("idClient", order.idClient));
                cmd.Parameters.Add(new MySqlParameter("idWaiter", order.idWaiter));
                cmd.Parameters.Add(new MySqlParameter("date", order.date));
                cmd.Parameters.Add(new MySqlParameter("total", order.total));

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public static void AddOrderDetails(OrderDetails order)
        {


            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddOrderDetails";
                cmd.Parameters.Add(new MySqlParameter("idOrder", order.idOrder));
                cmd.Parameters.Add(new MySqlParameter("idDish", order.idDish));
                cmd.Parameters.Add(new MySqlParameter("quat", order.quantity));
                cmd.Parameters.Add(new MySqlParameter("price", order.price));
                
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public static void UpdateOrder(Order order)
        {


            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateOrder";

                cmd.Parameters.Add(new MySqlParameter("idOrder", order.idOrder));
               
                cmd.Parameters.Add(new MySqlParameter("statusO", order.status));

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public static void DeleteOrder(Order order)
        {


            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteOrder";

                cmd.Parameters.Add(new MySqlParameter("idOrder", order.idOrder));
                cmd.Parameters.Add(new MySqlParameter("idClient", order.idClient));
                cmd.Parameters.Add(new MySqlParameter("idWaiter", order.idWaiter));
                cmd.Parameters.Add(new MySqlParameter("date", order.date));
                cmd.Parameters.Add(new MySqlParameter("total", order.total));

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public static IEnumerable<OrderDetails> GetOrderDetailList(int idOrder)
        {
            List<OrderDetails> OrdersList = new List<OrderDetails>();
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM orderdetails WHERE idOrder = "+idOrder;
                MySqlCommand cmd = new MySqlCommand(statement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderDetails order = new Models.OrderDetails();
                        order.idOrder = reader.GetInt32("idOrder");
                        order.idDish = reader.GetInt32("idDish");
                        order.quantity = reader.GetInt32("quantity");
                        OrdersList.Add(order);
                    }
                }
                conn.Close();
            }

            return OrdersList;
        }
    }
}