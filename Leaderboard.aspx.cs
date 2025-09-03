using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Leaderboard : System.Web.UI.Page
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
            BindQuizDropdown();
        }
    }
    private void BindQuizDropdown()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        string query = "SELECT QuizId, QuizTitle FROM Quizzes ORDER BY QuizTitle";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                try
                {
                    con.Open();
                    ddlQuiz.DataSource = cmd.ExecuteReader();
                    ddlQuiz.DataTextField = "QuizTitle";
                    ddlQuiz.DataValueField = "QuizId";
                    ddlQuiz.DataBind();

                    ddlQuiz.Items.Insert(0, new ListItem("-- Select a Quiz --", ""));

                    // Check if there are no quizzes in the dropdown
                    if (ddlQuiz.Items.Count <= 1)
                    {
                        pnlMessage.Visible = true;
                        lblMessage.Text = "No quizzes found. Please create a quiz first.";
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
    }
    protected void ddlQuiz_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ddlQuiz.SelectedValue))
        {
            int quizId = Convert.ToInt32(ddlQuiz.SelectedValue);
            pnlMessage.Visible = false;
            BindLeaderboard(quizId);
            
        }
        else
        {
            leaderboard_table.Visible = false;
            pnlMessage.Visible = true;
            lblMessage.Text = "Please select a quiz from the list.";
        }
    }
    private void BindLeaderboard(int quizId)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        string query = "SELECT TOP 10 U.FullName, R.Score, R.TotalQuestions, R.AttemptDate FROM Results R INNER JOIN Users U ON R.UserId = U.UserId WHERE R.QuizId = @QuizId ORDER BY R.Score DESC, R.AttemptDate ASC ";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@QuizId", quizId);
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        repeaterLeaderboard.DataSource = reader;
                        repeaterLeaderboard.DataBind();
                        leaderboard_table.Visible = true;
                        pnlMessage.Visible = false;
                    }
                    else
                    {
                        leaderboard_table.Visible = false;
                        pnlMessage.Visible = true;
                        lblMessage.Text = "No attempts have been made on this quiz yet.";
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    leaderboard_table.Visible = false;
                    pnlMessage.Visible = true;
                    lblMessage.Text = "An error occurred while loading the leaderboard.";
                }
            }
        }
    }
}