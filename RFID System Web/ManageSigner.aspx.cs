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
    public partial class ManageSigner : System.Web.UI.Page
    {
        private string aToken;
        protected void Page_Load(object sender, EventArgs e)
        {
            Site1 masterPage = this.Master as Site1;

            if (masterPage != null)
            {
                // Access the AToken variable
                aToken = masterPage.AToken;

                // Use the aToken value as needed
                // ...
            }
        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            var url = "https://k0hjjba8ds-k0jn7882ap-connect.kr0-aws-ws.kaleido.io/identities";
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
                    GridView1.DataSource = jsonArray;
                    GridView1.DataBind();
                }
                else
                {
                    Label1.Text = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
        }
        protected async void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox1.Text))
            {
                Label3.Text = "Please enter a valid name.";
                return;
            }
            else
            {
                var client = new HttpClient();
                var uri = new Uri("https://k0hjjba8ds-k0jn7882ap-connect.kr0-aws-ws.kaleido.io/identities");
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


                if (response.IsSuccessStatusCode)
                {
                    var obj = JObject.Parse(result);
                    string secret = obj["secret"].ToString();
                    Label3.Text = secret;
                    RegisterFabric(secret);




                }
                else
                {
                    var errorObj = JObject.Parse(result);
                    string errorMessage = errorObj["error"].ToString();
                    Label3.Text =errorMessage;
                }
                


            }
        }

        protected async void RegisterFabric(string secret)
        {
            var client = new HttpClient();
            var uri = new Uri("https://k0hjjba8ds-k0jn7882ap-connect.kr0-aws-ws.kaleido.io/identities/" + TextBox1.Text+ "/enroll");
            var authToken = aToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            var json = new JObject
            {
                ["secret"] = secret,
            };
            var content = new StringContent(json.ToString(), System.Text.Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, content);
            var result = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var obj = JObject.Parse(result);
                Label3.Text = obj["success"].ToString();

                using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
                {
                    string query = "INSERT INTO Signer (Name) VALUES (@name)";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@name", TextBox1.Text);

                            con.Open();
                            cmd.ExecuteNonQuery();
                    }
                }

            }
            else
            {
                var errorObj = JObject.Parse(result);
                string errorMessage = errorObj["error"].ToString();
                Label3.Text = errorMessage;
            }

        }



        protected void btnShowData_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
            {
                string query = "SELECT [UserAccounts].[user_id] AS UserId, [UserAccounts].[name] AS Username, [Signer].[name] AS Signer " +
                               "FROM [UserAccounts] " +
                               "INNER JOIN [UserSigner] ON [UserAccounts].[user_id] = [UserSigner].[user_id] " +
                               "INNER JOIN [Signer] ON [UserSigner].[signer_id] = [Signer].[signer_id]";

                string filter = "";
                List<SqlParameter> parameters = new List<SqlParameter>();

                // Add the filter conditions based on the selected value
                if (DropDownListFilterBy.SelectedValue == "Username")
                {
                    if (!string.IsNullOrEmpty(TextBoxUsername.Text))
                    {
                        filter = "[UserAccounts].[name] LIKE '%' + @Username + '%'";
                        parameters.Add(new SqlParameter("@Username", TextBoxUsername.Text));
                    }
                }
                else if (DropDownListFilterBy.SelectedValue == "Signer")
                {
                    if (!string.IsNullOrEmpty(TextBoxSigner.Text))
                    {
                        filter = "[Signer].[name] LIKE '%' + @Signer + '%'";
                        parameters.Add(new SqlParameter("@Signer", TextBoxSigner.Text));
                    }
                }

                // Add the filter conditions to the query if any
                if (!string.IsNullOrEmpty(filter))
                {
                    query += " WHERE " + filter;
                }

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();

                    // Add parameter values if any
                    foreach (SqlParameter parameter in parameters)
                    {
                        command.Parameters.Add(parameter);
                    }

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
            TextBoxSigner.Text = "";
            TextBoxUsername.Text = "";
        }

        protected void DropDownListFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = DropDownListFilterBy.SelectedValue;

            usernameContainer.Visible = selectedValue == "Username";
            usernameFilterContainer.Visible = selectedValue == "Username";
            signerContainer.Visible = selectedValue == "Signer";
            signerFilterContainer.Visible = selectedValue == "Signer";
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