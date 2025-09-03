using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            lblUserFullName.Text = Session["FullName"].ToString();
            if (!IsPostBack)
            {
                BindMyQuizzes();
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        int quizId = Convert.ToInt32(btn.CommandArgument);

        // Delete quiz and its questions
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            // First, delete questions from the Questions table
            string deleteQuestionsQuery = "DELETE FROM Questions WHERE QuizId = @QuizId";

            // Then, delete the quiz from the Quizzes table
            string deleteQuizQuery = "DELETE FROM Quizzes WHERE QuizId = @QuizId AND CreatorId = @CreatorId";

            using (SqlCommand cmdQuestions = new SqlCommand(deleteQuestionsQuery, con))
            {
                cmdQuestions.Parameters.AddWithValue("@QuizId", quizId);
                con.Open();
                cmdQuestions.ExecuteNonQuery();
            }

            using (SqlCommand cmdQuiz = new SqlCommand(deleteQuizQuery, con))
            {
                cmdQuiz.Parameters.AddWithValue("@QuizId", quizId);
                cmdQuiz.Parameters.AddWithValue("@CreatorId", Convert.ToInt32(Session["UserId"]));
                cmdQuiz.ExecuteNonQuery();
            }

            con.Close();
        }

        // Refresh the quiz list
        BindMyQuizzes();
    }
    private void BindMyQuizzes()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        int userId = Convert.ToInt32(Session["UserId"]);

        string query = "SELECT QuizId, QuizTitle, CreatedDate FROM Quizzes WHERE CreatorId = @CreatorId ORDER BY CreatedDate DESC";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@CreatorId", userId);
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    repeaterMyQuizzes.DataSource = reader;
                    repeaterMyQuizzes.DataBind();

                    if (repeaterMyQuizzes.Items.Count == 0)
                    {
                        lblNoQuizzesFound.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
    }
    protected void btnCreateQuiz_Click(object sender, EventArgs e)
    {
        // CreateQuiz page par redirect karein
        Response.Redirect("CreateQuiz.aspx");
    }
}