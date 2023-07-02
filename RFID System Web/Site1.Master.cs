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
    public partial class Site1 : System.Web.UI.MasterPage
    {
        public string AToken { get; } = "azBpdzFkZzQ5MDpKRENvb3BJNWFYMW9YRWk5TEQ3SWhFQ0J0OEJFTnRCUmthcWhJNm81VUdF";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int userId = GetUserIdFromSession();
                string connectionString = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string query = "SELECT name FROM UserAccounts WHERE user_id = @userId";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblUserName.Text = reader["name"].ToString();
                        }
                    }
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

    }
}