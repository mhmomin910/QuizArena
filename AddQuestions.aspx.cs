using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddQuestions : System.Web.UI.Page
{
    private int quizId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            // Agar user logged in nahi hai, to login page par redirect karein
            Response.Redirect("Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            // URL se quizId ko retrieve karein
            if (Request.QueryString["quizId"] != null && int.TryParse(Request.QueryString["quizId"], out quizId))
            {
                Session["CurrentQuizId"] = quizId;
                LoadQuizTitle(quizId);
            }
            else
            {
                lblMessage.Text = "Invalid Quiz ID.";
                btnFinish.Visible = false; // Hide the finish button
                btnAddQuestion.Visible = false; // Hide the add question button
            }
        }
        else
        {
            // PostBack par quizId session se retrieve karein
            if (Session["CurrentQuizId"] != null)
            {
                quizId = Convert.ToInt32(Session["CurrentQuizId"]);
            }
        }
    }
    private void LoadQuizTitle(int id)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT QuizTitle FROM Quizzes WHERE QuizId = @QuizId";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@QuizId", id);
                try
                {
                    con.Open();
                    object title = cmd.ExecuteScalar();
                    if (title != null)
                    {
                        lblQuizTitle.Text = title.ToString();
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error loading quiz title: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
    //protected void btnAddQuestion_Click(object sender, EventArgs e)
    //{
        
    //}
    //protected void btnFinish_Click(object sender, EventArgs e)
    //{
        
    //}

    protected void btnAddQuestion_Click1(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtQuestionText.Text) || string.IsNullOrEmpty(ddlCorrectOption.SelectedValue))
        {
            lblMessage.Text = "Please fill out the question and select the correct option.";
            return;
        }

        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Questions (QuizId, QuestionText, OptionA, OptionB, OptionC, OptionD, OptionE, OptionF, CorrectOption) VALUES (@QuizId, @QuestionText, @OptionA, @OptionB, @OptionC, @OptionD, @OptionE, @OptionF, @CorrectOption)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@QuizId", quizId);
                cmd.Parameters.AddWithValue("@QuestionText", txtQuestionText.Text);
                cmd.Parameters.AddWithValue("@OptionA", txtOptionA.Text);
                cmd.Parameters.AddWithValue("@OptionB", txtOptionB.Text);
                cmd.Parameters.AddWithValue("@OptionC", txtOptionC.Text);
                cmd.Parameters.AddWithValue("@OptionD", txtOptionD.Text);
                cmd.Parameters.AddWithValue("@OptionE", string.IsNullOrEmpty(txtOptionE.Text) ? (object)DBNull.Value : txtOptionE.Text);
                cmd.Parameters.AddWithValue("@OptionF", string.IsNullOrEmpty(txtOptionF.Text) ? (object)DBNull.Value : txtOptionF.Text);
                cmd.Parameters.AddWithValue("@CorrectOption", ddlCorrectOption.SelectedValue);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    // Form ko clear karein taaki user naya question add kar sake
                    txtQuestionText.Text = "";
                    txtOptionA.Text = "";
                    txtOptionB.Text = "";
                    txtOptionC.Text = "";
                    txtOptionD.Text = "";
                    txtOptionE.Text = "";
                    txtOptionF.Text = "";
                    ddlCorrectOption.SelectedIndex = 0; // Dropdown ko reset karein

                    lblMessage.Text = "Question added successfully! Add another one.";
                    lblMessage.CssClass = "text-success";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "An error occurred while adding the question: " + ex.Message;
                    lblMessage.CssClass = "text-danger";
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }

    protected void btnFinish_Click1(object sender, EventArgs e)
    {
        // Jab questions add ho jaayen, to user ko dashboard par redirect karein
        Session.Remove("CurrentQuizId"); // Session se quizId hata dein
        Response.Redirect("Dashboard.aspx");
    }
}