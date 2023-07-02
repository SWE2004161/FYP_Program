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
    public partial class WebForm1 : System.Web.UI.Page
    {
        string aToken = "azBkNWQ0ZHF6ODpwYmlmQUtqeFpFV3JuV2hCbURGTGoxRUR3SVdraWdOYjZZdXpoM2loczJj";
        private string connectionString = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRequestID_Click(object sender, EventArgs e)
        {
            string requestIdString = txtRequestId.Value;
            int requestId = int.Parse(requestIdString);
            PopulateRequestDetails(requestId);
            
        }
        private void PopulateRequestDetails(int requestId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string selectQuery = "SELECT description FROM Requests WHERE request_id = @RequestId";

                using (SqlCommand comm = new SqlCommand(selectQuery, con))
                {
                    comm.Parameters.AddWithValue("@RequestId", requestId);

                    con.Open();
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string description = reader["description"].ToString();
                            txtDescription.Value = description;
                        }
                    }
                }
            }
        }
        protected async void Button1_Click(object sender, EventArgs e)
        {
            var url = "https://k0wodb26tu-k0bj5s1gvi-connect.kr0-aws-ws.kaleido.io/identities";
            var authToken = aToken;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    JArray jsonArray = JArray.Parse(responseContent);
                    List<string> names = new List<string>();
                    foreach (var item in jsonArray)
                    {
                        names.Add(item["name"].ToString());
                    }
                    Label1.Text = string.Join(", ", names);
                }
                else
                {
                    Label1.Text = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
        }


        protected async void Button2_Click(object sender, EventArgs e)
        {

            var client = new HttpClient();
            var uri = new Uri("https://k0wodb26tu-k0bj5s1gvi-connect.kr0-aws-ws.kaleido.io/identities");
            var authToken = aToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            var json = new JObject
            {
                ["name"] = TextBox1.Text,
                ["type"] = "client",
                ["maxEnrollments"] = 0,
                ["attributes"] = new JObject()
            };
            var content = new StringContent(json.ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();

            var obj = JObject.Parse(result);
            Label3.Text = obj["secret"].ToString();
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            int requestId;
            if (int.TryParse(txtRequestId.Value, out requestId))
            {
                // Update the status of the request to "Accepted" in the database
                UpdateRequestStatus(requestId, "Accepted");
            }

            ClearForm();
        }

        protected void btnDecline_Click(object sender, EventArgs e)
        {
            int requestId;
            if (int.TryParse(txtRequestId.Value, out requestId))
            {
                // Update the status of the request to "Declined" in the database
                UpdateRequestStatus(requestId, "Declined");
            }

            ClearForm();
        }

        private void UpdateRequestStatus(int requestId, string status)
        {
            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
            {
                string updateQuery = "UPDATE Requests SET status = @Status WHERE request_id = @RequestId";

                using (SqlCommand comm = new SqlCommand(updateQuery, con))
                {
                    comm.Parameters.AddWithValue("@Status", status);
                    comm.Parameters.AddWithValue("@RequestId", requestId);

                    con.Open();
                    comm.ExecuteNonQuery();
                }
            }

            // Display a success message or perform any other necessary actions
        }

        private void ClearForm()
        {
            txtRequestId.Value = string.Empty;
            txtDescription.Value = string.Empty;
        }

        protected void btnShowData_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
            {
                string query = "SELECT [UserAccounts].[user_id] AS UserId, [UserAccounts].[name] AS Username, [Signer].[name] AS Signer " +
                               "FROM [UserAccounts] " +
                               "INNER JOIN [UserSigner] ON [UserAccounts].[user_id] = [UserSigner].[user_id] " +
                               "INNER JOIN [Signer] ON [UserSigner].[signer_id] = [Signer].[signer_id]";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // Bind the data reader to the GridView
                            GridViewData.DataSource = reader;
                            GridViewData.DataBind();

                            // Display the GridView
                            GridViewData.Visible = true;
                            lblNoData.Visible = false;
                        }
                        else
                        {
                            // No data found
                            GridViewData.Visible = false;
                            lblNoData.Visible = true;
                        }
                    }
                }
            }
        }

        protected void btnSaveData_Click(object sender, EventArgs e)
        {
            string username = txtNewUsername.Text.Trim();
            string signer = txtNewSigner.Text.Trim();

            // Get the user_id for the specified username
            int userId = GetUserIdByUsername(username);

            if (userId != -1)
            {
                // Insert the data into the UserSigner table
                using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
                {
                    string insertQuery = "INSERT INTO [UserSigner] ([user_id], [signer_id]) VALUES (@UserId, @SignerId)";

                    using (SqlCommand command = new SqlCommand(insertQuery, con))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@SignerId", GetSignerIdByName(signer));

                        con.Open();
                        command.ExecuteNonQuery();
                    }
                }

                // Clear the input fields
                txtNewUsername.Text = string.Empty;
                txtNewSigner.Text = string.Empty;
            }
        }

        private int GetUserIdByUsername(string username)
        {
            int userId = -1;

            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
            {
                string query = "SELECT [user_id] FROM [UserAccounts] WHERE [name] = @Username";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    con.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
            }

            return userId;
        }

        private int GetSignerIdByName(string signerName)
        {
            int signerId = -1;

            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
            {
                string query = "SELECT [signer_id] FROM [Signer] WHERE [name] = @SignerName";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@SignerName", signerName);

                    con.Open();

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        signerId = Convert.ToInt32(result);
                    }
                }
            }

            return signerId;
        }

        protected void GridViewData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteRow")
            {
                int userId = Convert.ToInt32(e.CommandArgument);

                // Call a method to delete the row from the database based on the userId
                DeleteRow(userId);

            }
        }

        private void DeleteRow(int userId)
        {
            // Delete the row from the database based on the userId
            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
            {
                string deleteQuery = "DELETE FROM [UserSigner] WHERE [user_id] = @UserId";

                using (SqlCommand command = new SqlCommand(deleteQuery, con))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}