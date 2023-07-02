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
    public partial class ClientQuery : System.Web.UI.Page
    {
        private string aToken;
        protected void Page_Load(object sender, EventArgs e)
        {
            Site3 masterPage = this.Master as Site3;

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
            var client = new HttpClient();
            var uri = new Uri("https://k0hjjba8ds-k0jn7882ap-connect.kr0-aws-ws.kaleido.io/blocks/" + TextBox2.Text + "?fly-channel=default-channel&fly-signer=user2");
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
                    await GetAssets();
                }

            }

        }

        public async Task GetAssets()
        {

            var client = new HttpClient();
            var uri = new Uri("https://k0hjjba8ds-k0jn7882ap-connect.kr0-aws-ws.kaleido.io/query");
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

            var filteredArray = new JArray();
            var textBoxValue = TextBox1.Text.Trim();

            if (!string.IsNullOrEmpty(textBoxValue))
            {
                pnlCardTag.Visible = true;
                detail.Visible = true;
                foreach (var item in resultArray)
                {
                    var tagId = item["tagId"].ToString();
                    if (tagId == textBoxValue)
                    {
                        filteredArray.Add(item);
                        lblTagID.Text = item["tagId"].ToString();
                        lblProductName.Text = item["productName"].ToString();
                        lblProductID.Text = item["productId"].ToString();
                    }
                    
                }
            }

            // Set the result array as the data source for GridViewData
            GridViewData.DataSource = filteredArray;
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
    }
}
