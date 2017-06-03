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
    public class ProductProvider
    {
        public static string connString;

        public static IEnumerable<Product> GetProductList()
        {
            List<Product> ProductsList = new List<Product>();
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            Console.Write("m-am conectat");
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM menu;";
                MySqlCommand cmd = new MySqlCommand(statement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Product product = new Models.Product();
                        product.idDish = reader.GetInt32("idDish");
                        product.dish = reader.GetString("dish");
                        product.description = reader.GetString("description");
                        product.price = reader.GetFloat("price");
                        product.tip = reader.GetInt32("tip");
                        ProductsList.Add(product);
                    }
                }
                conn.Close();
            }

            return ProductsList;
        }


        public static void AddProduct(Product product)
        {
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddProduct";

                cmd.Parameters.Add(new MySqlParameter("idDish", product.idDish));
                cmd.Parameters.Add(new MySqlParameter("dish", product.dish));
                cmd.Parameters.Add(new MySqlParameter("description", product.description));
                cmd.Parameters.Add(new MySqlParameter("price", product.price));
                cmd.Parameters.Add(new MySqlParameter("tip", product.tip));

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void generateOffert()
        {
            List<Product> allProducts = new List<Product>();
            allProducts = (List <Product>) GetProductList();
            List<Product> type1Products = new List<Product>();
            List<Product> type2Products = new List<Product>();
            List<Product> type3Products = new List<Product>();
            for (int i = 0; i < allProducts.Count(); i++)
                if (allProducts[i].tip == 1)
                    type1Products.Add(allProducts[i]);
                else
                    if (allProducts[i].tip == 2)
                        type2Products.Add(allProducts[i]);
                    else
                    if (allProducts[i].tip == 3)
                        type3Products.Add(allProducts[i]);

            int n1 = type1Products.Count();
            int n2 = type2Products.Count();
            int n3 = type3Products.Count();
            Random rnd = new Random();
            int x1 = rnd.Next(1, n1);
            int x2 = rnd.Next(1, n2);
            int x3 = rnd.Next(1, n3);

            Product p = new Product();
            p.idDish = allProducts.Count()+1;
            p.dish = "Oferta zilei";
            p.description = type1Products[x1].description + type2Products[x2].description + type3Products[x3].description;
            p.price = type1Products[x1].price + type2Products[x2].price + type3Products[x3].price - 5;
            p.tip = '4';

            AddProduct(p);
        }
    }
}