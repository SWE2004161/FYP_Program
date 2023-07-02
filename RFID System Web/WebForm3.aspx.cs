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

namespace RFID_System_Web
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        string aToken = "azBkNWQ0ZHF6ODpwYmlmQUtqeFpFV3JuV2hCbURGTGoxRUR3SVdraWdOYjZZdXpoM2loczJj";
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected async void Button2_Click(object sender, EventArgs e)
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
                    await GetAssets();
                }

            }

        }

        public async Task GetAssets()
        {
            
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




    }
}