using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;


namespace RFID_System_Web
{
    public partial class WebForm5 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Value;
            string password = txtPassword.Value;

            using (SqlConnection con = new SqlConnection("Data Source=MSI\\SQLEXPRESS;Initial Catalog=RFIDUserDatabase;Integrated Security=True;"))
            {
                string selectQuery = "SELECT salt, password_hash, user_id, role FROM UserAccounts WHERE name = @Name";

                using (SqlCommand comm = new SqlCommand(selectQuery, con))
                {
                    comm.Parameters.AddWithValue("@Name", username);

                    con.Open();

                    using (SqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string saltBase64 = reader.GetString(0);
                            string passwordHashBase64 = reader.GetString(1);
                            string userrole = reader.GetString(3);

                            // Decode the Base64 encoded salt and password hash back to byte arrays
                            byte[] saltBytes = Convert.FromBase64String(saltBase64);
                            byte[] passwordHashBytes = Convert.FromBase64String(passwordHashBase64);

                            // Combine the entered password and stored salt
                            byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(password);
                            byte[] enteredPasswordWithSaltBytes = new byte[enteredPasswordBytes.Length + saltBytes.Length];
                            Array.Copy(enteredPasswordBytes, 0, enteredPasswordWithSaltBytes, 0, enteredPasswordBytes.Length);
                            Array.Copy(saltBytes, 0, enteredPasswordWithSaltBytes, enteredPasswordBytes.Length, saltBytes.Length);

                            // Hash the combined entered password and salt
                            byte[] enteredPasswordHashBytes = ComputeHash(enteredPasswordWithSaltBytes, saltBytes);

                            // Compare the entered password hash with the stored password hash
                            bool passwordMatch = true;
                            if (enteredPasswordHashBytes.Length != passwordHashBytes.Length)
                            {
                                passwordMatch = false;
                            }
                            else
                            {
                                for (int i = 0; i < enteredPasswordHashBytes.Length; i++)
                                {
                                    if (enteredPasswordHashBytes[i] != passwordHashBytes[i])
                                    {
                                        passwordMatch = false;
                                        break;
                                    }
                                }
                            }

                            if (passwordMatch)
                            {
                                if (int.TryParse(reader.GetValue(2).ToString(), out int userId))
                                {
                                    Session["UserId"] = userId;  // Store the user ID in the session
                                    Response.Write("Login successful!"); // Login successful
                                    if (userrole =="user")
                                    {
                                        Response.Redirect("UserTransaction.aspx");
                                    }
                                    else if (userrole =="admin")
                                    {

                                        Response.Redirect("ManageSigner.aspx");
                                    }

                                    
                                }



                            }
                            else
                            {
                                // Invalid password
                                Response.Write("Invalid password.");
                            }
                        }
                        else
                        {
                            // User not found
                            Response.Write("User not found.");
                        }
                    }
                }
            }
        }
        // Compute the hash value of the input string
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
    }
    }
