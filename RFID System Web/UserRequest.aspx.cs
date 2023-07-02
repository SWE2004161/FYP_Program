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
    public partial class UserRequest : System.Web.UI.Page
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
            int userId = GetUserIdFromSession();
            PopulateRequestsGrid(userId);
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Value;
            string description = txtDescription.Value;

            // Check if title or description is empty
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description))
            {
                // One or both fields are empty
                // Handle the error, display a message to the user, or take appropriate action
                lblError.Text = "Please fill in both the title and description fields.";
                return;
            }
            else
            {
                // Insert the request into the database
                using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
                {
                    string insertQuery = "INSERT INTO Requests (user_id, title, description, status) VALUES (@UserId, @Title, @Description, @Status)";

                    using (SqlCommand comm = new SqlCommand(insertQuery, con))
                    {
                        comm.Parameters.AddWithValue("@UserId", GetUserIdFromSession()); // Assuming you have a way to get the user's ID
                        comm.Parameters.AddWithValue("@Title", title);
                        comm.Parameters.AddWithValue("@Description", description);
                        comm.Parameters.AddWithValue("@Status", "Pending");

                        con.Open();
                        comm.ExecuteNonQuery();
                    }
                }

                // Clear the input fields after submission
                txtTitle.Value = string.Empty;
                txtDescription.Value = string.Empty;
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

        private void PopulateRequestsGrid(int userId)
        {
            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
            {
                string selectQuery = "SELECT title AS Title, description AS Description, status AS Status FROM Requests WHERE user_id = @UserId";

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
                        else
                        {
                            talbe.Visible = false; 
                        }
                    }
                }
            }
        }

        protected void GridViewData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdateStatus")
            {
                
                // Get the row index of the clicked button
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Find the GridView row based on the row index
                GridViewRow row = GridViewData.Rows[rowIndex];

                // Extract the necessary data from the row
                string requestId = row.Cells[0].Text;
                string title = row.Cells[1].Text;
                string description = row.Cells[2].Text;
                string status = row.Cells[3].Text;

                // Update the status in the database or perform other actions

                // Refresh the GridView to reflect the updated status
                int userId = GetUserIdFromSession(); // Assuming you have a way to get the user's ID
                PopulateRequestsGrid(userId);
            }
        }


    }
}