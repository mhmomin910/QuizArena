using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserProfile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadUserProfile();
            LoadQuizHistory();
        }
    }
    private void LoadUserProfile()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        int userId = Convert.ToInt32(Session["UserId"]);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            // User details fetch karne ke liye query
            string query = "SELECT FullName, Email FROM Users WHERE UserId = @UserId";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        lblFullName.Text = reader["FullName"].ToString();
                        lblEmail.Text = reader["Email"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error loading user profile: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }

    private void LoadQuizHistory()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        int userId = Convert.ToInt32(Session["UserId"]);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            // Quiz history fetch karne ke liye query
            string query = @"
                    SELECT R.Score, R.TotalQuestions, R.AttemptDate, Q.QuizTitle
                    FROM Results R
                    INNER JOIN Quizzes Q ON R.QuizId = Q.QuizId
                    WHERE R.UserId = @UserId
                    ORDER BY R.AttemptDate DESC";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    if (dt.Rows.Count > 0)
                    {
                        lblTotalQuizzesAttempted.Text = dt.Rows.Count.ToString();
                        repeaterHistory.DataSource = dt;
                        repeaterHistory.DataBind();
                        pnlHistory.Visible = true;
                    }
                    else
                    {
                        lblTotalQuizzesAttempted.Text = "0";
                        pnlHistory.Visible = false; // Agar history nahi hai toh hide kar do
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error loading quiz history: " + ex.Message);
                    lblTotalQuizzesAttempted.Text = "0";
                    pnlHistory.Visible = false;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}