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
    public partial class ManageRequest : System.Web.UI.Page
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
                string selectQuery = "SELECT r.request_id AS RequestId, u.name AS Username, r.description AS Description, r.status AS Status, r.title AS Title FROM Requests r INNER JOIN UserAccounts u ON r.user_id = u.user_id WHERE r.status = 'Pending' AND r.title <> 'RESigner'";

                using (SqlCommand comm = new SqlCommand(selectQuery, con))
                {
                    con.Open();
                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        GridViewData.DataSource = reader;
                        GridViewData.DataBind();
                    }
                }
            }
        }

        protected string GetShortDescription(string description)
        {
            const int maxLength = 20;
            if (description.Length > maxLength)
            {
                return description.Substring(0, maxLength) + "...";
            }
            return description;
        }

        protected void GridViewData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowDetails")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewData.Rows[rowIndex];

                // Extract the request details from the selected row
                string requestId = row.Cells[0].Text;

                // Retrieve the complete details of the request from the database
                RequestDetails requestDetails = GetRequestDetails(requestId);

                // Update the panel with the retrieved request details
                lblSelectedRequest.Text = requestId;
                lblSelectedTitle.Text = requestDetails.Title;
                lblSelectedDescription.Text = requestDetails.Description;
                lblSelectedStatus.Text = requestDetails.Status;
                ddlSelectedStatus.SelectedValue = requestDetails.Status;

                // Show the panel
                pnlRequestDetails.Visible = true;
            }
            else if (e.CommandName == "UpdateStatus")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewData.Rows[rowIndex];

                // Extract the request details from the selected row
                string requestId = row.Cells[0].Text;
                string updatedStatus = ddlSelectedStatus.SelectedValue;

                // Update the status in the database
                UpdateRequestStatus(requestId, updatedStatus);

                // Hide the panel
                pnlRequestDetails.Visible = false;

                // Refresh the GridView
                PopulatePendingRequestsGrid();
            }
        }
        private RequestDetails GetRequestDetails(string requestId)
        {
            // Create an instance of RequestDetails to hold the retrieved details
            RequestDetails requestDetails = new RequestDetails();

            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
            {
                string selectQuery = "SELECT title, description, status FROM Requests WHERE request_id = @RequestId";

                using (SqlCommand comm = new SqlCommand(selectQuery, con))
                {
                    comm.Parameters.AddWithValue("@RequestId", requestId);
                    con.Open();

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate the requestDetails object with the retrieved values
                            requestDetails.Title = reader["title"].ToString();
                            requestDetails.Description = reader["description"].ToString();
                            requestDetails.Status = reader["status"].ToString();
                        }
                    }
                }
            }

            return requestDetails;
        }

        public class RequestDetails
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string Status { get; set; }
        }

        protected void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            string requestId = lblSelectedRequest.Text;
            string updatedStatus = ddlSelectedStatus.SelectedValue;

            UpdateRequestStatus(requestId, updatedStatus);

            // Hide the panel after updating the status
            pnlRequestDetails.Visible = false;

            // Refresh the GridView
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
