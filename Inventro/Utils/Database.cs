using Inventro.View.UserControls;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;

namespace Inventro.Utils
{
    public class Database
    {
        private const string connectionString = "datasource=localhost;port=3306;Database=hardware_db;username=Madhusha;password=abc123";
        private static MySqlConnection connection;
        private static void connect()
        {
            connection = new MySqlConnection(connectionString);
        }
        private static void disconnect()
        {
            connection.Close();
        }

        public static void addProduct(string productCode, string productName)
        {
            connect();

            string sql = "INSERT INTO products (product_code, product_name) VALUES (@productCode, @productName)";

            MySqlCommand command = new(sql, connection);

            command.Parameters.AddWithValue("@productCode", productCode);
            command.Parameters.AddWithValue("@productName", productName);

            connection.Open();

            command.ExecuteNonQuery();

            disconnect();
        }
        public static DataTable getProducts()
        {
            connect();

            string sql = "SELECT * FROM products";

            MySqlCommand command = new(sql, connection);

            MySqlDataAdapter adapter = new()
            {
                SelectCommand = command
            };

            DataTable table = new();
            adapter.Fill(table);

            disconnect();

            return table;
        }
        public static ObservableCollection<Product> getProductsObservable()
        {
            DataTable table = getProducts();

            ObservableCollection<Product> products = [];
            foreach (DataRow row in table.Rows)
            {
                int id = Convert.ToInt32(row["ID"]);
                string productCode = row["product_code"].ToString();
                string productName = row["product_name"].ToString();

                decimal dailyRate;
                try
                {
                    dailyRate = Convert.ToDecimal(row["daily_rate"].ToString());
                }
                catch (Exception)
                {
                    dailyRate = 0;
                }

                products.Add(new Product(id, productCode, productName, dailyRate));
            }

            return products;
        }
        public static void updateProductDailyRate(Dictionary<int, decimal> dict)
        {
            connect();
            connection.Open();

            foreach (KeyValuePair<int, decimal> entry in dict)
            {
                string sql = "UPDATE products SET daily_rate = @dailyRate WHERE id = @id";
                MySqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@dailyRate", entry.Value);
                command.Parameters.AddWithValue("@id", entry.Key);
                command.ExecuteNonQuery();
            }
            
            disconnect();
        }

        public static int addNewTransaction(ArrayList billItems, decimal total)
        {
            connect();

            string sql = "INSERT INTO transactions (total) VALUES (@Value)";
            MySqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Value", total);
            connection.Open();
            command.ExecuteNonQuery();

            int transactionID = (int)command.LastInsertedId;

            foreach (var item in billItems)
            {
                if (item is GeneralBillItemView generalItem)
                {
                    sql = "INSERT INTO bought_items (transaction_id, product_id, measured_weight, container_weight, minus_weight)" +
                    "VALUES (@transactionID, @productID, @measuredWeight, @containerWeight, @minusWeight)";
                    MySqlCommand cmd = new(sql, connection);
                    cmd.Parameters.AddWithValue("@transactionID", transactionID);
                    cmd.Parameters.AddWithValue("@productID", generalItem.GetProduct().getId());
                    cmd.Parameters.AddWithValue("@measuredWeight", generalItem.getMeasuredWeight());
                    cmd.Parameters.AddWithValue("@containerWeight", generalItem.getContainerWeight());
                    cmd.Parameters.AddWithValue("@minusWeight", generalItem.getMinusWeight());

                    cmd.ExecuteNonQuery();
                }
                else if (item is PolKuudaBillItemView polkudaBillItemView)
                {
                    sql = "INSERT INTO bought_items (transaction_id, product_id, measured_weight, container_weight, minus_weight)" +
                    "VALUES (@transactionID, @productID, @measuredWeight, @containerWeight, @minusWeight)";
                    MySqlCommand cmd = new(sql, connection);
                    cmd.Parameters.AddWithValue("@transactionID", transactionID);
                    cmd.Parameters.AddWithValue("@productID", polkudaBillItemView.GetProduct().getId());
                    cmd.Parameters.AddWithValue("@measuredWeight", polkudaBillItemView.GetMeasuredWeight());
                    cmd.Parameters.AddWithValue("@containerWeight", polkudaBillItemView.GetContainerWeight());
                    cmd.Parameters.AddWithValue("@minusWeight", polkudaBillItemView.GetMinusWeight());

                    cmd.ExecuteNonQuery();
                }
            }

            disconnect();

            return transactionID;
        }


        public static DataTable getTransactions()
        {
            connect();

            string sql = "SELECT * FROM transactions";

            MySqlCommand command = new(sql, connection);

            MySqlDataAdapter adapter = new()
            {
                SelectCommand = command
            };

            DataTable table = new();
            adapter.Fill(table);

            disconnect();

            return table;
        }

        public static void DeleteTransaction(int transactionID)
        {
            connect();

            string sql = "DELETE FROM transactions WHERE id = " + transactionID;

            MySqlCommand command = new(sql, connection);
            connection.Open();
            command.ExecuteNonQuery();

            disconnect();
        }

        public static DataTable getBoughtItems(int transactionID)
        {
            connect();

            string sql = "SELECT * FROM bought_items WHERE transaction_id = @transactionID";

            MySqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@transactionID", transactionID);

            MySqlDataAdapter adapter = new()
            {
                SelectCommand = command
            };

            DataTable table = new();
            adapter.Fill(table);
            disconnect();

            table.Columns.Add("product_name");
            Dictionary<int, string> products = [];
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            foreach (DataRow row in table.Rows)
            {
                if (products.TryGetValue((int)row["product_id"], out string productName))
                {
                    row["product_name"] = productName;
                }
                else
                {
                    int productID = (int)row["product_id"];

                    sql = "SELECT product_name FROM products WHERE id = " + productID;
                    MySqlCommand cmd = new(sql, conn);

                    string productName1 = cmd.ExecuteScalar().ToString();


                    products[productID] = productName1;

                    row["product_name"] = productName1;
                }
            }
            conn.Close();

            return table;
        }

        public static decimal getTotalSaleForDay()
        {
            connect();

            DateTime today = DateTime.Now.Date;

            string sql = "SELECT total FROM transactions WHERE DATE(created_at) = '" + today.Year + "-" + today.Month + "-" + today.Day + "'";
            MySqlCommand command = new(sql, connection);
            connection.Open();
            MySqlDataReader results = command.ExecuteReader();

            decimal total = 0;
            while (results.Read())
            {
                total += Convert.ToDecimal(results["total"]);
            }

            disconnect();

            return total;
        }

        public static int[] GetKiloNGramSteps()
        {
            connect();
            string sql = "SELECT * FROM system_data";
            MySqlCommand command = new(sql, connection);
            connection.Open();
            MySqlDataReader results = command.ExecuteReader();

            int[] arr = [1, 100];

            while (results.Read())
            {
                if (Convert.ToInt32(results["id"]) == 1)
                {
                    arr[0] = Convert.ToInt32(results["value"]);
                } else if (Convert.ToInt32(results["id"]) == 2)
                {
                    arr[1] = Convert.ToInt32(results["value"]);
                }
            }
            disconnect();

            return arr;
        }
        public static void UpdateKiloNGramSteps(int id, int value)
        {
            connect();
            string sql = "UPDATE system_data SET value = " + value + " WHERE id = " + id;
            MySqlCommand command = new(sql, connection);
            connection.Open();
            command.ExecuteNonQuery();
            disconnect();
        }
    }
}
