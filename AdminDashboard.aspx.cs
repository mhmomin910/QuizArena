using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminDashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Check if the user is an admin
        if (Session["UserId"] == null || Session["IsAdmin"] == null || !Convert.ToBoolean(Session["IsAdmin"]))
        {
            Response.Redirect("Dashboard.aspx"); // Redirect to a regular user dashboard
        }

        if (!IsPostBack)
        {
            BindUsersGrid();
            BindQuizzesGrid();
        }
    }
    // --- User Management ---
    private void BindUsersGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        string query = "SELECT UserId, FullName, Email, IsAdmin FROM Users ORDER BY FullName";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridUsers.DataSource = dt;
                gridUsers.DataBind();
            }
        }
    }

    protected void gridUsers_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridUsers.EditIndex = e.NewEditIndex;
        BindUsersGrid();
    }

    protected void gridUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridUsers.EditIndex = -1;
        BindUsersGrid();
    }

    protected void gridUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gridUsers.Rows[e.RowIndex];
        int userId = Convert.ToInt32(gridUsers.DataKeys[e.RowIndex].Value);
        TextBox txtFullName = (TextBox)row.Cells[1].Controls[0];
        TextBox txtEmail = (TextBox)row.Cells[2].Controls[0];
        CheckBox chkEditIsAdmin = (CheckBox)row.FindControl("chkEditIsAdmin");

        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        string query = "UPDATE Users SET FullName = @FullName, Email = @Email, IsAdmin = @IsAdmin WHERE UserId = @UserId";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@IsAdmin", chkEditIsAdmin.Checked);
                cmd.Parameters.AddWithValue("@UserId", userId);
                con.Open();
                cmd.ExecuteNonQuery();
                gridUsers.EditIndex = -1;
                BindUsersGrid();
            }
        }
    }

    protected void gridUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int userId = Convert.ToInt32(gridUsers.DataKeys[e.RowIndex].Value);
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            // Delete user's quizzes and results first to avoid foreign key errors
            string deleteResultsQuery = "DELETE FROM Results WHERE UserId = @UserId";
            string deleteQuizzesQuery = "DELETE FROM Quizzes WHERE CreatorId = @UserId";
            string deleteUserQuery = "DELETE FROM Users WHERE UserId = @UserId";

            con.Open();
            using (SqlCommand cmd = new SqlCommand(deleteResultsQuery, con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand(deleteQuizzesQuery, con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand(deleteUserQuery, con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.ExecuteNonQuery();
            }


            BindUsersGrid();
        }
    }

    // --- Quiz Management ---
    private void BindQuizzesGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        string query = "SELECT Q.QuizId, Q.QuizTitle, Q.CreatedDate, U.FullName AS CreatorName FROM Quizzes Q INNER JOIN Users U ON Q.CreatorId = U.UserId ORDER BY Q.CreatedDate DESC";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridQuizzes.DataSource = dt;
                gridQuizzes.DataBind();
            }
        }
    }

    protected void gridQuizzes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int quizId = Convert.ToInt32(gridQuizzes.DataKeys[e.RowIndex].Value);
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            // Delete associated results and questions first
            string deleteResultsQuery = "DELETE FROM Results WHERE QuizId = @QuizId";
            string deleteQuestionsQuery = "DELETE FROM Questions WHERE QuizId = @QuizId";
            string deleteQuizQuery = "DELETE FROM Quizzes WHERE QuizId = @QuizId";

            con.Open();
            using (SqlCommand cmd = new SqlCommand(deleteResultsQuery, con))
            {
                cmd.Parameters.AddWithValue("@QuizId", quizId);
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand(deleteQuestionsQuery, con))
            {
                cmd.Parameters.AddWithValue("@QuizId", quizId);
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand cmd = new SqlCommand(deleteQuizQuery, con))
            {
                cmd.Parameters.AddWithValue("@QuizId", quizId);
                cmd.ExecuteNonQuery();
            }


            BindQuizzesGrid();
        }
    }
}