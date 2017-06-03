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
    public class UserProvider
    {
        public static string connString;

        public static IEnumerable<User> GetUsersList()
        {
            List<User> UsersList = new List<User>();
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM users ";
                MySqlCommand cmd = new MySqlCommand(statement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new Models.User();
                        user.idUser = Convert.ToInt32(reader.GetString("idUser"));
                        user.password = reader.GetString("password");
                        user.tip = reader.GetString("tip");
                      
                            UsersList.Add(user);
                    }
                }
                conn.Close();
            }

            return UsersList;
        }
        public static IEnumerable<User> GetClientsList()
        {
            List<User> UsersList = new List<User>();
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM users ";
                MySqlCommand cmd = new MySqlCommand(statement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new Models.User();
                        user.idUser = Convert.ToInt32(reader.GetString("idUser"));
                        user.password = reader.GetString("password");
                        user.tip = reader.GetString("tip");
                        if(user.tip.Equals("c"))
                            UsersList.Add(user);
                    }
                }
                conn.Close();
            }

            return UsersList;
        }
        public static IEnumerable<User> GetWaitersList()
        {
            List<User> UsersList = new List<User>();
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM users ";
                MySqlCommand cmd = new MySqlCommand(statement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        User user = new Models.User();
                        user.idUser = Convert.ToInt32(reader.GetString("idUser"));
                        user.password = reader.GetString("password");
                        user.tip = reader.GetString("tip");
                        if (user.tip.Equals("w"))
                            UsersList.Add(user);
                    }
                }
                conn.Close();
            }

            return UsersList;
        }
        public static User GetUser(int idUser)
        {
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM users where idUser = " + idUser + ";";

                MySqlCommand cmd = new MySqlCommand(statement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    {
                        User user = new User();
                        user.idUser = Convert.ToInt32(reader.GetString("idUser"));
                        user.password = reader.GetString("password");
                        user.tip = reader.GetString("tip");
                        return user;
                    }
                }
            }

            return null;
        }
        public static Client GetUserMail(int idUser)
        {
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                string statement = "SELECT * FROM clients where idClient = " + idUser + ";";

                MySqlCommand cmd = new MySqlCommand(statement, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    {
                        Client user = new Client();
                        user.id = Convert.ToInt32(reader.GetString("idClient"));
                        user.mail = reader.GetString("email");                       
                        return user;
                    }
                }
            }

            return null;
        }
        public static void AddUser(User user)
        {
            Security secure = new Security();
            user.password = secure.HashSHA1(user.password);
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AddUser";

                cmd.Parameters.Add(new MySqlParameter("idUser", user.idUser));
                cmd.Parameters.Add(new MySqlParameter("newPassword", user.password));
                cmd.Parameters.Add(new MySqlParameter("tip", user.tip));

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public static void UpdateUser(User user)
        {
            Security secure = new Security();
            user.password = secure.HashSHA1(user.password);
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateUser";

                cmd.Parameters.Add(new MySqlParameter("idUser", user.idUser));
                cmd.Parameters.Add(new MySqlParameter("newPassword", user.password));
                cmd.Parameters.Add(new MySqlParameter("tip", user.tip));

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public static void DeleteUser(User user)
        {
            Security secure = new Security();
            user.password = secure.HashSHA1(user.password);
            connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteUser";

                cmd.Parameters.Add(new MySqlParameter("idUser", user.idUser));


                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        public static User Login(int userId, string password)
        {
            // connString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
            User user = GetUser(userId);
            if (user != null)
            {
                Security secure = new Security();
                if (secure.VerifyHash(password, user.password))
                {
                    return user;
                }
            }
            return null;
        }
    }
}