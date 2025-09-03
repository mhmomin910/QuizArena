using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EditQuiz : System.Web.UI.Page
{
    private int quizId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        if (Request.QueryString["quizId"] == null || !int.TryParse(Request.QueryString["quizId"], out quizId))
        {
            Response.Redirect("Dashboard.aspx");
            return;
        }

        if (!IsPostBack)
        {
            LoadQuizDetails();
            BindQuestionsGrid();
        }
    }
    private void LoadQuizDetails()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        string query = "SELECT QuizTitle, QuizDescription, CreatorId FROM Quizzes WHERE QuizId = @QuizId";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@QuizId", quizId);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Check if the current user is the creator of the quiz
                    if (Convert.ToInt32(reader["CreatorId"]) != Convert.ToInt32(Session["UserId"]))
                    {
                        Response.Redirect("Dashboard.aspx"); // Redirect if not the creator
                    }

                    lblQuizTitle.Text = reader["QuizTitle"].ToString();
                    txtEditQuizTitle.Text = reader["QuizTitle"].ToString();
                    txtEditQuizDescription.Text = reader["QuizDescription"].ToString();
                }
                else
                {
                    Response.Redirect("Dashboard.aspx");
                }
            }
        }
    }
    private void BindQuestionsGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        string query = "SELECT QuestionId, QuestionText FROM Questions WHERE QuizId = @QuizId";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@QuizId", quizId);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridQuestions.DataSource = dt;
                gridQuestions.DataBind();
            }
        }
    }
    protected void btnUpdateQuizDetails_Click(object sender, EventArgs e)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        string query = "UPDATE Quizzes SET QuizTitle = @QuizTitle, QuizDescription = @QuizDescription WHERE QuizId = @QuizId";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@QuizTitle", txtEditQuizTitle.Text);
                cmd.Parameters.AddWithValue("@QuizDescription", txtEditQuizDescription.Text);
                cmd.Parameters.AddWithValue("@QuizId", quizId);
                con.Open();
                cmd.ExecuteNonQuery();
                lblQuizTitle.Text = txtEditQuizTitle.Text;
                lblMessage.Text = "Quiz details updated successfully.";
                lblMessage.CssClass = "text-success";
            }
        }
    }
    protected void gridQuestions_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridQuestions.EditIndex = e.NewEditIndex;
        BindQuestionsGrid();
    }
    protected void gridQuestions_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridQuestions.EditIndex = -1;
        BindQuestionsGrid();
    }

    protected void gridQuestions_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int questionId = Convert.ToInt32(gridQuestions.DataKeys[e.RowIndex].Value);
        GridViewRow row = gridQuestions.Rows[e.RowIndex];
        TextBox txtEditQuestionText = (TextBox)row.FindControl("txtEditQuestionText");
        string newQuestionText = txtEditQuestionText.Text;

        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        string query = "UPDATE Questions SET QuestionText = @QuestionText WHERE QuestionId = @QuestionId";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@QuestionText", newQuestionText);
                cmd.Parameters.AddWithValue("@QuestionId", questionId);
                con.Open();
                cmd.ExecuteNonQuery();
                gridQuestions.EditIndex = -1;
                BindQuestionsGrid();
                lblMessage.Text = "Question updated successfully.";
                lblMessage.CssClass = "text-success";
            }
        }
    }

    protected void gridQuestions_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int questionId = Convert.ToInt32(gridQuestions.DataKeys[e.RowIndex].Value);
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        string query = "DELETE FROM Questions WHERE QuestionId = @QuestionId";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@QuestionId", questionId);
                con.Open();
                cmd.ExecuteNonQuery();
                BindQuestionsGrid();
                lblMessage.Text = "Question deleted successfully.";
                lblMessage.CssClass = "text-success";
            }
        }
    }
}