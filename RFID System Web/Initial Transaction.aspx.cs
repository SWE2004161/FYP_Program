using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Data.SqlClient;

namespace RFID_System_Web
{
    public partial class Initial_Transaction : System.Web.UI.Page
    {
        private string aToken;
        protected void Page_Load(object sender, EventArgs e)
        {
            Site2 masterPage = this.Master as Site2;

            if (masterPage != null)
            {
                // Access the AToken variable
                aToken = masterPage.AToken;

                // Use the aToken value as needed
                // ...
            }

            string company = "";
            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
            {
                string selectQuery = "SELECT [company] FROM [UserAccounts] WHERE [user_id] = @UserId";

                using (SqlCommand comm = new SqlCommand(selectQuery, con))
                {
                    comm.Parameters.AddWithValue("@UserId", GetUserIdFromSession());

                    con.Open();
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            company = reader["company"].ToString(); // Assign the company value
                        }
                    }
                }
            }


            Company.Enabled = false;
            Company.Text = company;

        }
        protected async void Transactions_Click(object sender, EventArgs e)
        {
            string readerIP = ReaderIP.Text;
            string productName = ProductName.Text;
            string productID = ProductID.Text;
            string location = Location.Text;
            string company = Company.Text;
            string job = Job.Text;
            string sensor = Sensor.Text;
            string sensorData = SensorData.Text;

            // Check if any text field is empty
            if (string.IsNullOrEmpty(readerIP) || string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(productID)
                || string.IsNullOrEmpty(location) || string.IsNullOrEmpty(company) || string.IsNullOrEmpty(job) || string.IsNullOrEmpty(sensor)
                || string.IsNullOrEmpty(sensorData))
            {
                Label1.Text = "Please fill in all the required fields.";
                return;
            }
            else
            {

                string signer = "";
                using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
                {
                    string selectQuery = "SELECT [Signer].[name] AS Signer " +
                   "FROM [UserSigner] " +
                   "INNER JOIN [Signer] ON [UserSigner].[signer_id] = [Signer].[signer_id] " +
                   "WHERE [UserSigner].[user_id] = @UserId";

                    using (SqlCommand comm = new SqlCommand(selectQuery, con))
                    {
                        comm.Parameters.AddWithValue("@UserId", GetUserIdFromSession());

                        con.Open();
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                signer = reader["Signer"].ToString(); // Assign the signer value
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(signer))
                {
                    Label1.Text = "You don't have a signer.";
                    return; // Exit the method if signer is null or empty
                }
                else
                {



                    var blockHashes = await GetBlockHashes();
                    var currentBlockHash = blockHashes[0];
                    var previousBlockHash = blockHashes[1];
                    var UniqueID = previousBlockHash.Substring(0, 6);

                    var client = new HttpClient();
                    var uri = new Uri("https://k0hjjba8ds-k0jn7882ap-connect.kr0-aws-ws.kaleido.io/transactions");
                    var authToken = aToken;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                    var json = new JObject
                    {
                        ["headers"] = new JObject
                        {
                            ["type"] = "CreateAsset",
                            ["signer"] = signer,
                            ["channel"] = "default-channel",
                            ["chaincode"] = "asset_transfer"
                        },
                        ["func"] = "CreateAsset",
                        ["args"] = new JArray
                    {
                        UniqueID,
                        TagID.Text,
                        ReaderIP.Text,
                        ProductName.Text,
                        ProductID.Text,
                        Location.Text,
                        Company.Text,
                        Job.Text,
                        Sensor.Text,
                        SensorData.Text,
                    },
                        ["init"] = false
                    };
                    var content = new StringContent(json.ToString(), System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(uri, content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var responseObject = JObject.Parse(responseContent);

                    string[] blockHashes1 = await GetBlockHashes();

                    // Update Label1 with the current block hash
                    Label1.Text = "Hash Value: " + blockHashes1[0];
                }
            }
        }
        private int GetUserIdFromSession()
        {
            int userId = 0; // Default value or any appropriate default ID

            if (Session["UserId"] != null)
            {
                userId = (int)Session["UserId"];
            }

            return userId;
        }

        public async Task<string[]> GetBlockHashes()
        {
            var client = new HttpClient();
            var uri = new Uri("https://k0hjjba8ds-k0jn7882ap-connect.kr0-aws-ws.kaleido.io/chaininfo?fly-channel=default-channel&fly-signer=user2");
            var authToken = aToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            var blockInfo = JObject.Parse(responseJson)["result"];
            var currentBlockHash = (string)blockInfo["current_block_hash"];
            var previousBlockHash = (string)blockInfo["previous_block_hash"];

            return new[] { currentBlockHash, previousBlockHash };
        }

    }
}