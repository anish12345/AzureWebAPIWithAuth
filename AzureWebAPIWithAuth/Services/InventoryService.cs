using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AzureWebAPIWithAuth.Helper;
using AzureWebAPIWithAuth.Interfaces;
using AzureWebAPIWithAuth.Models;
using System.Data.SqlClient;

namespace AzureWebAPIWithAuth.Services
{
    public class InventoryService : IInventoryService
    {

        public async Task<List<Inventory>> GetInventoryList()
        {
            SqlConnection sqlConnection = SqlHelper.Getconnectionstring();
            sqlConnection.Open();
            try
            {
                List<Inventory> InventoryList = new List<Inventory>();
                string statement = "SELECT InventoryID,InventoryName,Description,Price from Inventory";

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
                return InventoryList;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public async Task<int> AddInventory(Inventory inventory)
        {
            SqlConnection sqlConnection = SqlHelper.Getconnectionstring();
            sqlConnection.Open();
            try
            {
                    string query = "Insert into Inventory (InventoryName, Description,Price) values(@inventoryName, @description , @price)";
                    SqlCommand cmd = new SqlCommand(query, sqlConnection);
                    cmd.Parameters.AddWithValue("@inventoryName", inventory.InventoryName);
                    cmd.Parameters.AddWithValue("@description", inventory.Description);
                    cmd.Parameters.AddWithValue("@price", inventory.Price);
                    return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public async Task<int> UpdateInventory(Inventory inventory)
        {
            SqlConnection sqlConnection = SqlHelper.Getconnectionstring();
            sqlConnection.Open();
            try
            {
                string query = "Update  Inventory SET InventoryName = @inventoryName,Description=@description,Price= @price where InventoryID=@InventoryID";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@InventoryID", inventory.InventoryID);
                cmd.Parameters.AddWithValue("@inventoryName", inventory.InventoryName);
                cmd.Parameters.AddWithValue("@description", inventory.Description);
                cmd.Parameters.AddWithValue("@price", inventory.Price);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public async Task<int> DeleteInventory(int InventoryID)
        {
            SqlConnection sqlConnection = SqlHelper.Getconnectionstring();
            sqlConnection.Open();
            try
            {
                string query = "Delete from Inventory where InventoryID=@InventoryID";
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@InventoryID", InventoryID);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
