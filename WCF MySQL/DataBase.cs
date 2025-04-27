using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace WCF_MySQL
{
    public struct User
    {
        public string id;
        public string name;
        public string cnic;
        public string email;
        public string password;
        public string address;
    }

    public struct Feedback
    {
        public string c_id;
        public string rating;
        public string comment;
    }

    public struct RawMaterial
    {
        public string id;
        public string name;
        public string description;
        public string unit;
        public int quantity;
    }

    public struct Product
    {
        public string id;
        public string name;
        public string description;
        public string price;
    }

    public struct Order
    {
        public string order_id;
        public string customer_id;
        public string product_id;
        public string quantity;
        public string status;
        public string order_time;
    }

    public class DataBase
    {
        private static string connectionString = "server=localhost;database=sda_lab;user=root;password=password123;";
        private static MySqlConnection conn = new MySqlConnection(connectionString);
        public DataBase() { }

        public static string AddCustomer(User user)
        {
            try
            {
                conn.Open();

                string query = "SELECT c_id FROM customers ORDER BY CAST(SUBSTRING(c_id, 6) AS UNSIGNED) DESC LIMIT 1;";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    string topId = "1";
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string lastId = result.ToString();

                        if (lastId.Length > 5 && lastId.StartsWith("CUST-"))
                        {
                            string numberPart = lastId.Substring(5);
                            if (int.TryParse(numberPart, out int temp))
                            {
                                temp++;
                                topId = temp.ToString();
                            }
                        }
                    }

                    user.id = "CUST-" + topId;
                }

                string insertQuery = "INSERT INTO customers (c_id, name, cnic, email, password, address) " +
                                     "VALUES (@id, @name, @cnic, @email, @password, @address)";

                using (MySqlCommand cmd1 = new MySqlCommand(insertQuery, conn))
                {
                    cmd1.Parameters.AddWithValue("@id", user.id);
                    cmd1.Parameters.AddWithValue("@name", user.name);
                    cmd1.Parameters.AddWithValue("@cnic", user.cnic);
                    cmd1.Parameters.AddWithValue("@email", user.email);
                    cmd1.Parameters.AddWithValue("@password", user.password);
                    cmd1.Parameters.AddWithValue("@address", user.address);

                    int rowsAffected = cmd1.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return user.id;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return "";
        }

        public static string AddManufacturer(User user)
        {
            try
            {
                conn.Open();

                string query = "SELECT m_id FROM manufacturers ORDER BY CAST(SUBSTRING(m_id, 5) AS UNSIGNED) DESC LIMIT 1;";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    string topId = "1";
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string lastId = result.ToString();

                        if (lastId.Length > 4 && lastId.StartsWith("MFR-"))
                        {
                            string numberPart = lastId.Substring(4);
                            if (int.TryParse(numberPart, out int temp))
                            {
                                temp++;
                                topId = temp.ToString();
                            }
                        }
                    }

                    user.id = "MFR-" + topId;
                }

                string insertQuery = "INSERT INTO manufacturers (m_id, name, cnic, email, password, address) " +
                                     "VALUES (@id, @name, @cnic, @email, @password, @address)";

                using (MySqlCommand cmd1 = new MySqlCommand(insertQuery, conn))
                {
                    cmd1.Parameters.AddWithValue("@id", user.id);
                    cmd1.Parameters.AddWithValue("@name", user.name);
                    cmd1.Parameters.AddWithValue("@cnic", user.cnic);
                    cmd1.Parameters.AddWithValue("@email", user.email);
                    cmd1.Parameters.AddWithValue("@password", user.password);
                    cmd1.Parameters.AddWithValue("@address", user.address);

                    int rowsAffected = cmd1.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return user.id;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return "";
        }

        public static bool CustomerLogin(string id, string password)
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM customers WHERE c_id = @id AND password = @password";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@password", password);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return false;
        }

        public static bool ManufacturerLogin(string id, string password)
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM manufacturers WHERE m_id = @id AND password = @password";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@password", password);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return false;
        }

        public static bool AdminLogin(string id, string password)
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM admins WHERE a_id = @id AND password = @password";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@password", password);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return false;
        }

        public static List<User> GetManufacturers()
        {
            List<User> manufacturers = new List<User>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM manufacturers";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                id = reader["m_id"].ToString(),
                                name = reader["name"].ToString(),
                                cnic = reader["cnic"].ToString(),
                                email = reader["email"].ToString(),
                                password = reader["password"].ToString(),
                                address = reader["address"].ToString()
                            };
                            manufacturers.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return manufacturers;
        }

        public static bool RemoveManufacturer(string id)
        {
            try
            {
                conn.Open();
                string query = "DELETE FROM manufacturers WHERE m_id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return false;
        }

        public static List<Feedback> GetFeedbacks()
        {
            List<Feedback> feedbacks = new List<Feedback>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM feedbacks";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Feedback feedback = new Feedback
                            {
                                c_id = reader["c_id"].ToString(),
                                rating = reader["rating"].ToString(),
                                comment = reader["comment"].ToString()
                            };
                            feedbacks.Add(feedback);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return feedbacks;
        }

        public static string AddRawMaterial(RawMaterial material)
        {
            try
            {
                conn.Open();
                string query = "SELECT m_id FROM raw_materials ORDER BY CAST(SUBSTRING(m_id, 5) AS UNSIGNED) DESC LIMIT 1;";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    string topId = "1";
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        string lastId = result.ToString();
                        if (lastId.Length > 4 && lastId.StartsWith("RMT-"))
                        {
                            string numberPart = lastId.Substring(4);
                            if (int.TryParse(numberPart, out int temp))
                            {
                                temp++;
                                topId = temp.ToString();
                            }
                        }
                    }
                    material.id = "RMT-" + topId;

                    string insertQuery = "INSERT INTO raw_materials (m_id, name, description, unit, quantity) " +
                                         "VALUES (@id, @name, @description, @unit, @quantity)";
                    using (MySqlCommand cmd1 = new MySqlCommand(insertQuery, conn))
                    {
                        cmd1.Parameters.AddWithValue("@id", material.id);
                        cmd1.Parameters.AddWithValue("@name", material.name);
                        cmd1.Parameters.AddWithValue("@description", material.description);
                        cmd1.Parameters.AddWithValue("@unit", material.unit);
                        cmd1.Parameters.AddWithValue("@quantity", material.quantity);
                        int rowsAffected = cmd1.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return material.id;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return material.id;
        }

        public static bool RemoveRawMaterial(string id)
        {
            try
            {
                conn.Open();
                string query = "DELETE FROM raw_materials WHERE m_id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return false;
        }

        public static bool UpdateRawMaterial(RawMaterial material)
        {
            try
            {
                conn.Open();
                string query = "UPDATE raw_materials SET name = @name, description = @description, unit = @unit, quantity = @quantity WHERE m_id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", material.id);
                    cmd.Parameters.AddWithValue("@name", material.name);
                    cmd.Parameters.AddWithValue("@description", material.description);
                    cmd.Parameters.AddWithValue("@unit", material.unit);
                    cmd.Parameters.AddWithValue("@quantity", material.quantity);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return false;
        }

        public static List<RawMaterial> GetRawMaterials()
        {
            List<RawMaterial> materials = new List<RawMaterial>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM raw_materials";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RawMaterial material = new RawMaterial
                            {
                                id = reader["m_id"].ToString(),
                                name = reader["name"].ToString(),
                                description = reader["description"].ToString(),
                                unit = reader["unit"].ToString(),
                                quantity = int.Parse(reader["quantity"].ToString())
                            };
                            materials.Add(material);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return materials;
        }

        public static string AddProduct(Product material)
        {
            try
            {
                conn.Open();
                string query = "SELECT p_id FROM products ORDER BY CAST(SUBSTRING(p_id, 5) AS UNSIGNED) DESC LIMIT 1;";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    string topId = "1";
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        string lastId = result.ToString();
                        if (lastId.Length > 4 && lastId.StartsWith("PRD-"))
                        {
                            string numberPart = lastId.Substring(4);
                            if (int.TryParse(numberPart, out int temp))
                            {
                                temp++;
                                topId = temp.ToString();
                            }
                        }
                    }
                    material.id = "PRD-" + topId;

                    string insertQuery = "INSERT INTO products (p_id, name, description, price) " +
                                         "VALUES (@id, @name, @description, @price)";
                    using (MySqlCommand cmd1 = new MySqlCommand(insertQuery, conn))
                    {
                        cmd1.Parameters.AddWithValue("@id", material.id);
                        cmd1.Parameters.AddWithValue("@name", material.name);
                        cmd1.Parameters.AddWithValue("@description", material.description);
                        cmd1.Parameters.AddWithValue("@price", material.price);
                        int rowsAffected = cmd1.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            return material.id;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return material.id;
        }

        public static bool RemoveProduct(string id)
        {
            try
            {
                conn.Open();
                string query = "DELETE FROM products WHERE p_id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return false;
        }

        public static bool UpdateProduct(Product material)
        {
            try
            {
                conn.Open();
                string query = "UPDATE products SET name = @name, description = @description, price = @price WHERE p_id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", material.id);
                    cmd.Parameters.AddWithValue("@name", material.name);
                    cmd.Parameters.AddWithValue("@description", material.description);
                    cmd.Parameters.AddWithValue("@price", material.price);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return false;
        }

        public static List<Product> GetProducts()
        {
            List<Product> materials = new List<Product>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM products";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product material = new Product
                            {
                                id = reader["p_id"].ToString(),
                                name = reader["name"].ToString(),
                                description = reader["description"].ToString(),
                                price = reader["price"].ToString()
                            };
                            materials.Add(material);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return materials;
        }

        public static Product GetAProduct(string id)
        {
            Product material = new Product();
            try
            {
                conn.Open();
                string query = "SELECT * FROM products WHERE p_id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            material.id = reader["p_id"].ToString();
                            material.name = reader["name"].ToString();
                            material.description = reader["description"].ToString();
                            material.price = reader["price"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return material;
        }

        public static string PlaceOrder(Order order)
        {
            conn.Open();
            string query = "SELECT o_id FROM orders ORDER BY CAST(SUBSTRING(o_id, 5) AS UNSIGNED) DESC LIMIT 1;";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                string topId = "1";
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    string lastId = result.ToString();
                    if (lastId.Length > 4 && lastId.StartsWith("ORD-"))
                    {
                        string numberPart = lastId.Substring(4);
                        if (int.TryParse(numberPart, out int temp))
                        {
                            temp++;
                            topId = temp.ToString();
                        }
                    }
                }
                order.order_id = "ORD-" + topId;

                string insertQuery = "INSERT INTO orders (o_id, c_id, p_id, quantity) " +
                                     "VALUES (@order_id, @customer_id, @product_id, @quantity)";
                using (MySqlCommand cmd1 = new MySqlCommand(insertQuery, conn))
                {
                    cmd1.Parameters.AddWithValue("@order_id", order.order_id);
                    cmd1.Parameters.AddWithValue("@customer_id", order.customer_id);
                    cmd1.Parameters.AddWithValue("@product_id", order.product_id);
                    cmd1.Parameters.AddWithValue("@quantity", order.quantity);
                    int rowsAffected = cmd1.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return order.order_id;
                    }
                }
            }
            return order.order_id;
        }

        public static List<Order> GetOrders()
        {
            List<Order> materials = new List<Order>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM orders";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order material = new Order
                            {
                                order_id = reader["o_id"].ToString(),
                                customer_id = reader["c_id"].ToString(),
                                product_id = reader["p_id"].ToString(),
                                quantity = reader["quantity"].ToString(),
                                status = reader["status"].ToString(),
                                order_time = reader["o_time"].ToString()
                            };
                            materials.Add(material);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return materials;
        }

        public static List<Order> GetOrdersbyCustomer(string id)
        {
            List<Order> materials = new List<Order>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM orders where c_id = @id ORDER BY status DESC";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order material = new Order
                            {
                                order_id = reader["o_id"].ToString(),
                                customer_id = reader["c_id"].ToString(),
                                product_id = reader["p_id"].ToString(),
                                quantity = reader["quantity"].ToString(),
                                status = reader["status"].ToString(),
                                order_time = reader["o_time"].ToString()
                            };
                            materials.Add(material);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return materials;
        }

        public static List<Order> GetOrdersbyMonth(string month)
        {
            List<Order> materials = new List<Order>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM orders where substring(o_time, 1, 7) == @month";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@month", month);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order material = new Order
                            {
                                order_id = reader["o_id"].ToString(),
                                customer_id = reader["c_id"].ToString(),
                                product_id = reader["p_id"].ToString(),
                                quantity = reader["quantity"].ToString(),
                                status = reader["status"].ToString(),
                                order_time = reader["o_time"].ToString()
                            };
                            materials.Add(material);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return materials;
        }

        public static List<Order> GetUnfulfilledOrders()
        {
            List<Order> materials = new List<Order>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM orders where status = 'Pending Approval' OR status = 'In Production'";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Order material = new Order
                            {
                                order_id = reader["o_id"].ToString(),
                                customer_id = reader["c_id"].ToString(),
                                product_id = reader["p_id"].ToString(),
                                quantity = reader["quantity"].ToString(),
                                status = reader["status"].ToString(),
                                order_time = reader["o_time"].ToString()
                            };
                            materials.Add(material);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return materials;
        }

        public static bool UpdateOrderStatus(string order_id, string new_status)
        {
            try
            {
                conn.Open();
                string query = "UPDATE orders SET status = @status WHERE o_id = @id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", order_id);
                    cmd.Parameters.AddWithValue("@status", new_status);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return false;
        }

        public static bool GiveFeedback(Feedback feedback)
        {
            try
            {
                conn.Open();
                string query = "INSERT INTO feedbacks (c_id, rating, comment) VALUES (@c_id, @rating, @comment)";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@c_id", feedback.c_id);
                    cmd.Parameters.AddWithValue("@rating", feedback.rating);
                    cmd.Parameters.AddWithValue("@comment", feedback.comment);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return false;
        }

        public static List<string> GetOrdersMonths()
        {
            List<string> years = new List<string>();
            try
            {
                conn.Open();
                string query = "select distinct substring(o_time, 1, 7) from orders";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            years.Add(reader[0].ToString());
                        }
                        return years;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return years;
        }

        public static Dictionary<string, int> GetOrderStatusData(string month)
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            try
            {
                conn.Open();
                string query = "SELECT status, COUNT(status) FROM orders where substring(o_time, 1, 7) = @month group by status";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@month", month);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string status = reader["status"].ToString();
                            int count = Convert.ToInt32(reader["COUNT(status)"]); // Explicit conversion added here  
                            data[status] = count;
                        }
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return data;
        }

        public static Dictionary<string, int> GetOrderProductsData(string month)
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            try
            {
                conn.Open();
                string query = "SELECT p_id, COUNT(p_id) FROM orders where substring(o_time, 1, 7) = @month group by p_id";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@month", month);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string status = reader["p_id"].ToString();
                            int count = Convert.ToInt32(reader["COUNT(p_id)"]); // Explicit conversion added here  
                            data[status] = count;
                        }
                        return data;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
            return data;
        }

    }
}