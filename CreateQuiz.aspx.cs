using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateQuiz : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            BindCategories();
        }
    }
    private void BindCategories()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT CategoryId, CategoryName FROM Categories ORDER BY CategoryName";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                ddlCategory.DataSource = cmd.ExecuteReader();
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();
                ddlCategory.Items.Insert(0, new ListItem("-- Select Category --", ""));
            }
        }
    }

    protected void btnCreateQuiz_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ddlCategory.SelectedValue))
        {
            lblMessage.Text = "Please select a category for the quiz.";
            return;
        }

        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        int creatorId = Convert.ToInt32(Session["UserId"]);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Quizzes (QuizTitle, QuizDescription, CategoryId, CreatedDate, CreatorId, TimeLimitInMinutes,NegativeMarks) VALUES (@QuizTitle, @QuizDescription, @CategoryId, @CreatedDate, @CreatorId, @TimeLimitInMinutes,@NegativeMarks); SELECT SCOPE_IDENTITY();";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@QuizTitle", txtQuizTitle.Text);
                cmd.Parameters.AddWithValue("@QuizDescription", txtQuizDescription.Text);
                cmd.Parameters.AddWithValue("@CategoryId", ddlCategory.SelectedValue);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreatorId", creatorId);
                cmd.Parameters.AddWithValue("@TimeLimitInMinutes", Convert.ToInt32(txtTimeLimit.Text));
                cmd.Parameters.AddWithValue("@NegativeMarks", Convert.ToDecimal(txtNegativeMarks.Text));

                try
                {
                    con.Open();
                    int newQuizId = Convert.ToInt32(cmd.ExecuteScalar());
                    Response.Redirect("AddQuestions.aspx?quizId=" + newQuizId);
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred while creating the quiz: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}