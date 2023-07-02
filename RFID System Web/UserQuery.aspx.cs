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
    public partial class UserQuery : System.Web.UI.Page
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
        }

        protected async void Button2_Click(object sender, EventArgs e)
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
                var selectedCompany ="";


                var client = new HttpClient();
                var uri = new Uri("https://k0hjjba8ds-k0jn7882ap-connect.kr0-aws-ws.kaleido.io/query");
                var authToken = aToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

                var json = new JObject
                {
                    ["headers"] = new JObject
                    {
                        ["signer"] = signer,
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

                var filteredArray = new JArray();
                var textBoxValue = TextBoxTagID.Text.Trim();
                var locationValue = TextBoxLocation.Text.Trim();
                var readerIpValue = TextBoxReaderIP.Text.Trim();
                var sensorValue = TextBoxSensor.Text.Trim();

                using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
                {
                    string selectQuery = "SELECT [UserAccounts].[company] AS Company " +
                                         "FROM [UserAccounts] " +
                                         "WHERE [UserAccounts].[user_id] = @UserId";

                    using (SqlCommand comm = new SqlCommand(selectQuery, con))
                    {
                        comm.Parameters.AddWithValue("@UserId", GetUserIdFromSession());

                        con.Open();
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                               selectedCompany = reader["Company"].ToString(); // Assign the company value
                            }
                        }
                    }
                }


                if (!string.IsNullOrEmpty(textBoxValue))
                {
                    foreach (var item in resultArray)
                    {
                        var tagId = item["tagId"].ToString();
                        var company = item["company"].ToString();
                        if (tagId == textBoxValue && company == selectedCompany)
                        {
                            filteredArray.Add(item);
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(locationValue))
                {
                    foreach (var item in resultArray)
                    {
                        var location = item["location"].ToString();
                        var company = item["company"].ToString();
                        if (location == locationValue && company == selectedCompany)
                        {
                            filteredArray.Add(item);
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(readerIpValue))
                {
                    foreach (var item in resultArray)
                    {
                        var readerIp = item["readerIp"].ToString();
                        var company = item["company"].ToString();
                        if (readerIp == readerIpValue && company == selectedCompany)
                        {
                            filteredArray.Add(item);
                        }
                    }
                }
                else if (!string.IsNullOrEmpty(sensorValue))
                {
                    foreach (var item in resultArray)
                    {
                        var sensor = item["sensor"].ToString();
                        var company = item["company"].ToString();
                        if (sensor == sensorValue && company == selectedCompany)
                        {
                            filteredArray.Add(item);
                        }
                    }
                }
                else
                {
                    foreach (var item in resultArray)
                    {
                        var company = item["company"].ToString();
                        if (company == selectedCompany)
                        {
                            filteredArray.Add(item);
                        }
                    }
                }


                GridViewData.DataSource = filteredArray;
                GridViewData.DataBind();

                TextBoxTagID.Text = "";
                TextBoxLocation.Text = "";
                TextBoxReaderIP.Text = "";
                TextBoxSensor.Text = "";

            }
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

        protected void DropDownListFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedValue = DropDownListFilterBy.SelectedValue;

            tagIdContainer.Visible = selectedValue == "TagID";
            tagIdFilterContainer.Visible = selectedValue == "TagID";
            locationContainer.Visible = selectedValue == "Location";
            locationFilterContainer.Visible = selectedValue == "Location";
            readerIpContainer.Visible = selectedValue == "ReaderIP";
            readerIpFilterContainer.Visible = selectedValue == "ReaderIP";
            sensorContainer.Visible = selectedValue == "Sensor";
            sensorFilterContainer.Visible = selectedValue == "Sensor";
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

    }
}