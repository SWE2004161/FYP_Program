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
    public partial class RequestSigner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PopulatePendingRequestsGrid();
            }
        }
        private void PopulatePendingRequestsGrid()
        {
            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
            {
                string selectQuery = "SELECT r.request_id AS RequestId, u.name AS Username, u.email AS Email, u.company AS Company FROM Requests r INNER JOIN UserAccounts u ON r.user_id = u.user_id WHERE r.title = 'RESigner' AND r.status = 'Pending'";
                using (SqlCommand comm = new SqlCommand(selectQuery, con))
                {
                    con.Open();
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        GridViewSigner.DataSource = reader;
                        GridViewSigner.DataBind();
                    }
                }
            }
        }

        protected void GridViewSigner_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName == "Accept")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewSigner.Rows[rowIndex];
                string requestId = row.Cells[0].Text;

                string status = "Accepted";
                UpdateRequestStatus(requestId, status);
            }
            else if (e.CommandName == "Decline")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewSigner.Rows[rowIndex];
                string requestId = row.Cells[0].Text;

                string status = "Declined"; 
                UpdateRequestStatus(requestId, status);
            }

            // Rebind the grid view after processing the command
            PopulatePendingRequestsGrid();


        }


        private void UpdateRequestStatus(string requestId, string updatedStatus)
        {
            string connectionString = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True";
            string updateQuery = "UPDATE Requests SET status = @Status WHERE request_id = @RequestId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Status", updatedStatus);
                    command.Parameters.AddWithValue("@RequestId", requestId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
