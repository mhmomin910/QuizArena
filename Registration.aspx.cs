using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnRegister_Click(object sender, EventArgs e)
    {
        // Password validation
        if (txtPassword.Text != txtConfirmPassword.Text)
        {
            lblMessage.Text = "Passwords do not match.";
            return;
        }

        string hashedPassword = PasswordHelper.HashPassword(txtPassword.Text);
        // Connection string from web.config
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Users (FullName, Username, Password, Email, RegistrationDate) VALUES (@FullName, @Username, @Password, @Email, @RegistrationDate)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Password", hashedPassword); // Hashed password use karein
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@RegistrationDate", DateTime.Now);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Registration successful! You can now login.";
                    lblMessage.CssClass = "text-success"; // Success message styling
                                                          // Clear the form fields
                    txtFullName.Text = "";
                    txtUsername.Text = "";
                    txtEmail.Text = "";
                    txtPassword.Text = "";
                    txtConfirmPassword.Text = "";
                }
                catch (SqlException ex)
                {
                    // Handle specific SQL errors (e.g., duplicate username or email)
                    if (ex.Number == 2627) // 2627 is a common error number for unique constraint violations
                    {
                        if (ex.Message.Contains("Username"))
                        {
                            lblMessage.Text = "This username is already taken. Please choose another one.";
                        }
                        else if (ex.Message.Contains("Email"))
                        {
                            lblMessage.Text = "This email is already registered. Please login or use a different email.";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "An error occurred during registration: " + ex.Message;
                    }
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}