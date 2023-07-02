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
    public partial class UserRequestSigner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int userId = GetUserIdFromSession(); // Assuming you have a way to get the user's ID from the session


                PopulateSignerGrid(userId);
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

        private void PopulateSignerGrid(int userId)
        {
            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
            {
                string selectQuery = "SELECT S.name AS Name, UA.email AS Email, UA.company AS Company, " +
                                     "(CASE WHEN US.user_id IS NULL THEN 'No' ELSE 'Yes' END) AS Validated " +
                                     "FROM Signer S " +
                                     "LEFT JOIN UserSigner US ON US.signer_id = S.signer_id " +
                                     "LEFT JOIN UserAccounts UA ON UA.user_id = US.user_id " +
                                     "WHERE UA.user_id = @UserId";

                using (SqlCommand comm = new SqlCommand(selectQuery, con))
                {
                    comm.Parameters.AddWithValue("@UserId", userId);

                    con.Open();
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            GridViewData.DataSource = reader;
                            GridViewData.DataBind();
                        }
                    }
                }
            }

            // Check if there are no signers assigned and show the panel if necessary
            if (GridViewData.Rows.Count == 0)
            {
                pnlNoSigner.Visible = true;
                pnlSigner.Visible = false;

                // Retrieve user details from the database
                using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
                {
                    string selectUserQuery = "SELECT [name], email, company FROM UserAccounts WHERE user_id = @UserId";

                    using (SqlCommand comm = new SqlCommand(selectUserQuery, con))
                    {
                        comm.Parameters.AddWithValue("@UserId", userId);

                        con.Open();
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader.HasRows && reader.Read())
                            {
                                lblUserName.Text = reader["name"].ToString();
                                lblUserEmail.Text = reader["email"].ToString();
                                lblUserCompany.Text = reader["company"].ToString();
                            }
                        }
                    }
                }
            }
            else
            {
                pnlNoSigner.Visible = false;
                pnlSigner.Visible = true;
            }
        }

        protected void GridViewData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "YourCommandName")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewData.Rows[rowIndex];

                // Extract data from the clicked row
                string name = row.Cells[0].Text;
                string email = row.Cells[1].Text;
                string company = row.Cells[2].Text;
                string validated = row.Cells[3].Text;

                // Perform any desired actions with the extracted data
            }
        }

        protected void btnSendRequest_Click(object sender, EventArgs e)
        {
            // Perform the desired actions when the button is clicked
            // For example, you can insert a new request into the database

            int userId = GetUserIdFromSession(); ;
            string title = "RESigner";
            string status = "pending";

            // Insert the request into the database
            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
            {
                string insertQuery = "INSERT INTO Requests (user_id, description, status, title) VALUES (@UserId, @Description, @Status, @Title)";

                using (SqlCommand comm = new SqlCommand(insertQuery, con))
                {
                    comm.Parameters.AddWithValue("@UserId", userId);
                    comm.Parameters.AddWithValue("@Description", "Sample description"); // Provide the actual description value
                    comm.Parameters.AddWithValue("@Status", status);
                    comm.Parameters.AddWithValue("@Title", title);

                    con.Open();
                    comm.ExecuteNonQuery();
                }
            }

            // Optionally, you can perform any additional actions or display a success message
        }

    }
}