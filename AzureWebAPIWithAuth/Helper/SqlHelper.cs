using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System.Data.SqlClient;

namespace AzureWebAPIWithAuth.Helper
{
    public class SqlHelper
    {
        public static SqlConnection Getconnectionstring()
        {
            string tenantId = "dd393912-1b31-48f1-b206-c7273175d5b8";
            string clientId = "5350486d-d5fa-4e9d-86b3-54d1a0ce9ce2";
            string clientSecret = "MhU8Q~Yt6bm8~IrhUYCUgXuhM7zXe-_b7hkrhaVa";

            string keyvaultUrl = "https://anishkeyvault786.vault.azure.net/";
            string secretName = "dbconnectionstring";

            ClientSecretCredential clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);
            SecretClient secretClient = new SecretClient(new Uri(keyvaultUrl), clientSecretCredential);

            var secret = secretClient.GetSecret(secretName);

            string connectionString = secret.Value.Value;

            return new SqlConnection(connectionString);
        }
    }
}
