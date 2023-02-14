using AzureWebAPIWithAuth.Interfaces;
using AzureWebAPIWithAuth.Models;
using System.Data.SqlClient;

namespace AzureWebAPIWithAuth.Services
{
    public class InventoryService : IInventoryService
    {
        private SqlConnection GetConnection()
        {

            string connectionString = "Server=tcp:anishwebappserver.database.windows.net,1433;Initial Catalog=AnishWebAppDB;Persist Security Info=False;User ID=anish;Password=Indu@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            return new SqlConnection(connectionString);
        }

        public async Task<List<Inventory>> GetInventoryList()
        {

            List<Inventory> InventoryList = new List<Inventory>();
            string statement = "SELECT InventoryID,InventoryName,Description,Price from Inventory";
            SqlConnection sqlConnection = GetConnection();

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(statement, sqlConnection);

            using (SqlDataReader sqlDataReader = sqlCommand.ExecuteReader())
            {
                while (sqlDataReader.Read())
                {
                    Inventory _product = new Inventory()
                    {
                        InventoryID = sqlDataReader.GetInt32(0),
                        InventoryName = sqlDataReader.GetString(1),
                        Description = sqlDataReader.GetString(2),
                        Price = sqlDataReader.GetDouble(3),
                    };

                    InventoryList.Add(_product);
                }
            }
            sqlConnection.Close();
            return InventoryList;
        }

        public async Task<int> AddInventory(Inventory inventory)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                string query = "Insert into Inventory (InventoryName, Description,Price) values(@inventoryName, @description , @price)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@inventoryName", inventory.InventoryName);
                cmd.Parameters.AddWithValue("@description", inventory.Description);
                cmd.Parameters.AddWithValue("@price", inventory.Price);
                return  cmd.ExecuteNonQuery();
            }
        }

        public async Task<int> UpdateInventory(Inventory inventory)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                string query = "Update  Inventory SET InventoryName = @inventoryName,Description=@description,Price= @price where InventoryID=@InventoryID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@InventoryID", inventory.InventoryID);
                cmd.Parameters.AddWithValue("@inventoryName", inventory.InventoryName);
                cmd.Parameters.AddWithValue("@description", inventory.Description);
                cmd.Parameters.AddWithValue("@price", inventory.Price);
                return cmd.ExecuteNonQuery();
            }
        }

        public async Task<int> DeleteInventory(int InventoryID)
        {
            using (SqlConnection con = GetConnection())
            {
                con.Open();
                string query = "Delete from Inventory where InventoryID=@InventoryID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@InventoryID", InventoryID);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
