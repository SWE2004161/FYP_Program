using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;


namespace RFID_System_Web
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Value;
            string password = txtPassword.Value;
            string email = txtEmail.Value;
            string company = txtCompany.Value;
            string role = "user";

            if (IsUsernameTaken(username))
            {
                // Username is already taken
                // Handle the error, display a message to the user, or take appropriate action
                lblError.Text = "Username is already taken. Please choose a different username.";
                return;
            }
            else
            {

                if (password.Length < 5 || !Regex.IsMatch(password, @"[A-Z]") || !Regex.IsMatch(password, @"[\W_]"))
                {
                    // Password does not meet the requirements
                    // Handle the error, display a message to the user, or take appropriate action
                    lblError.Text = "Password must be at least 5 characters long, contain at least one uppercase letter, and at least one special character.";
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(company))
                    {
                        // One or more text fields are empty
                        // Handle the error, display a message to the user, or take appropriate action
                        lblError.Text = "Please fill in all the required fields.";
                        return;
                    }
                    else
                    {


                        // Generate a random salt
                        byte[] saltBytes = GenerateSalt();

                        // Combine the password and salt
                        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                        byte[] passwordWithSaltBytes = new byte[passwordBytes.Length + saltBytes.Length];
                        Array.Copy(passwordBytes, 0, passwordWithSaltBytes, 0, passwordBytes.Length);
                        Array.Copy(saltBytes, 0, passwordWithSaltBytes, passwordBytes.Length, saltBytes.Length);

                        // Hash the combined password and salt
                        byte[] hashedPasswordBytes = ComputeHash(passwordWithSaltBytes, saltBytes);

                        // Convert the byte arrays to base64 strings for storage in the database
                        string salt = Convert.ToBase64String(saltBytes);
                        string passwordHash = Convert.ToBase64String(hashedPasswordBytes);

                        using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
                        {
                            string insertQuery = "INSERT INTO UserAccounts (name, salt, password_hash, email, company, role) " +
                                "VALUES (@Name, @Salt, @PasswordHash, @Email, @Company, @Role)";

                            using (SqlCommand comm = new SqlCommand(insertQuery, con))
                            {
                                comm.Parameters.AddWithValue("@Name", username);
                                comm.Parameters.AddWithValue("@Salt", salt);
                                comm.Parameters.AddWithValue("@PasswordHash", passwordHash);
                                comm.Parameters.AddWithValue("@Email", email);
                                comm.Parameters.AddWithValue("@Company", company);
                                comm.Parameters.AddWithValue("@Role", role);

                                con.Open();
                                comm.ExecuteNonQuery();
                            }
                        }

                        Response.Redirect("User Login.aspx");
                    }
                }
            }
            
        }

        // Generate a random salt
        private byte[] GenerateSalt()
        {
            const int saltSize = 16; // 16 bytes = 128 bits
            byte[] saltBytes = new byte[saltSize];
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(saltBytes);
            }
            return saltBytes;
        }

        // Compute the hash value of the input bytes using PBKDF2
        private byte[] ComputeHash(byte[] inputBytes, byte[] saltBytes)
        {
            const int iterations = 10000; // Adjust the number of iterations as per your requirements
            const int derivedKeySize = 32; // 32 bytes = 256 bits
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(inputBytes, saltBytes, iterations))
            {
                pbkdf2.IterationCount = iterations;
                byte[] derivedKey = pbkdf2.GetBytes(derivedKeySize);
                return derivedKey;
            }
        }

        private bool IsUsernameTaken(string username)
        {
            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
            {
                string query = "SELECT COUNT(*) FROM UserAccounts WHERE name = @Name";

                using (SqlCommand comm = new SqlCommand(query, con))
                {
                    comm.Parameters.AddWithValue("@Name", username);

                    con.Open();
                    int count = (int)comm.ExecuteScalar();

                    return count > 0;
                }
            }
        }

    }
}