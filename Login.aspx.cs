using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Clear();
            Session.Abandon();
        }

    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;

        string hashedPassword = PasswordHelper.HashPassword(txtPassword.Text);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT UserId, FullName, Password, IsAdmin FROM Users WHERE Username = @Username";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Database se stored password fetch karein
                        string storedPasswordHash = reader["Password"].ToString();

                        // User ke diye gaye password hash ko database ke hash se compare karein
                        if (storedPasswordHash == hashedPassword)
                        {
                            // Login successful
                            Session["UserId"] = reader["UserId"].ToString();
                            Session["FullName"] = reader["FullName"].ToString();
                            Session["IsAdmin"] = Convert.ToBoolean(reader["IsAdmin"]);

                            lblMessage.Text = "Login successful!";
                            lblMessage.CssClass = "text-success";
                            Response.Redirect("Dashboard.aspx");
                        }
                        else
                        {
                            // Password match nahi hua
                            lblMessage.Text = "Invalid username or password.";
                        }
                    }
                    else
                    {
                        // Username exist hi nahi karta
                        lblMessage.Text = "Invalid username or password.";
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}