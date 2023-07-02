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
    public partial class WebForm2 : System.Web.UI.Page
    {

        string aToken = "azBkNWQ0ZHF6ODpwYmlmQUtqeFpFV3JuV2hCbURGTGoxRUR3SVdraWdOYjZZdXpoM2loczJj";
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected async void Button2_Click(object sender, EventArgs e)
        {
            var blockHashes = await GetBlockHashes();
            var currentBlockHash = blockHashes[0];
            var previousBlockHash = blockHashes[1];

            var client = new HttpClient();
            var uri = new Uri("https://k0wodb26tu-k0bj5s1gvi-connect.kr0-aws-ws.kaleido.io/query");
            var authToken = aToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            var json = new JObject
            {
                ["headers"] = new JObject
                {
                    ["signer"] = "user2",
                    ["channel"] = "default-channel",
                    ["chaincode"] = "asset_transfer"
                },
                ["func"] = "GetAllAssets",
                ["args"] = new JArray("GetAllAssets"),
                ["strongread"] = true
            };
            var content = new StringContent(json.ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();

            var jsonObject = JObject.Parse(result);
            var resultArray = JArray.FromObject(jsonObject["result"]);

            // Set the result array as the data source for GridViewData
            GridViewData.DataSource = resultArray;
            GridViewData.DataBind();
        }
        protected void GridViewData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Handle the row command event here
            if (e.CommandName == "DeleteRow")
            {
                // Perform the delete operation
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewData.DeleteRow(rowIndex);

                // Refresh the grid view or perform any other necessary actions
            }
        }

        protected async void Transactions_Click(object sender, EventArgs e)
        {
            var blockHashes = await GetBlockHashes();
            var currentBlockHash = blockHashes[0];
            var previousBlockHash = blockHashes[1];
            var UniqueID = previousBlockHash.Substring(0, 6);

            var client = new HttpClient();
            var uri = new Uri("https://k0wodb26tu-k0bj5s1gvi-connect.kr0-aws-ws.kaleido.io/transactions");
            var authToken = aToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            var json = new JObject
            {
                ["headers"] = new JObject
                {
                    ["type"] = "SendTransaction",
                    ["signer"] = "user2",
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

            // Do something with the response object
            // For example, you can display the transaction ID and status in a label
            var transactionID = responseObject["transactionID"].ToString();
            var status = responseObject["status"].ToString();
            Label1.Text = $"Transaction ID: {transactionID}, Status: {status}";
        }
        public async Task<string[]> GetBlockHashes()
        {
            var client = new HttpClient();
            var uri = new Uri("https://k0wodb26tu-k0bj5s1gvi-connect.kr0-aws-ws.kaleido.io/chaininfo?fly-channel=default-channel&fly-signer=user2");
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

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            string description = txtDescription.Value;

            // Insert the request into the database
            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
            {
                string insertQuery = "INSERT INTO Requests (user_id, description, status) VALUES (@UserId, @Description, @Status)";

                using (SqlCommand comm = new SqlCommand(insertQuery, con))
                {
                    comm.Parameters.AddWithValue("@UserId", GetUserIdFromSession()); // Assuming you have a way to get the user's ID
                    comm.Parameters.AddWithValue("@Description", description);
                    comm.Parameters.AddWithValue("@Status", "Pending");

                    con.Open();
                    comm.ExecuteNonQuery();
                }
            }

            // Clear the description field after submission
            txtDescription.Value = string.Empty;
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

        protected async void Button3_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var uri = new Uri("https://k0wodb26tu-k0bj5s1gvi-connect.kr0-aws-ws.kaleido.io/blocks/" + TextBox2.Text + "?fly-channel=default-channel&fly-signer=user2");
            var authToken = aToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            var response = await client.GetAsync(uri);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var responseObject = JObject.Parse(responseContent);

                var argsArray = responseObject["result"]["block"]["transactions"][0]["actions"][0]["input"]["args"];
                var argsList = argsArray.ToObject<List<string>>();
                var tagId = argsList[2];
                if (TextBox1.Text == tagId)
                {
                    TagID.Enabled = true;
                    TagID.Text = tagId;
                    ReaderIP.Enabled = true;
                    ProductName.Enabled = true;
                    ProductID.Enabled = true;
                    Location.Enabled = true;
                    Company.Enabled = true;
                    Job.Enabled = true;
                    Sensor.Enabled = true;
                    SensorData.Enabled = true;
                }

            }

        }







    }
}