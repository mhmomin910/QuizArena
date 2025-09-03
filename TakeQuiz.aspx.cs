using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class TakeQuiz : System.Web.UI.Page
{
    private int quizTimeLimit;
    private int quizId;
    private int currentQuestionIndex = 0;

    // Declare the HTML controls here to avoid NullReferenceException
    //protected HtmlGenericControl question_text;
    //protected HtmlGenericControl options_container;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["CurrentQuestionIndex"] != null)
        {
            int currentIndex = Convert.ToInt32(Session["CurrentQuestionIndex"]);
            if (currentIndex > 0 && !IsPostBack)
            {
                lblMessage.Text = "Warning: You cannot go back to previous questions!";
                btnNext.Visible = false;
                btnFinish.Visible = false;
            }
        }

        if (Session["UserId"] == null)
        {
            Response.Redirect("Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["quizId"] != null && int.TryParse(Request.QueryString["quizId"], out quizId))
            {
                Session["CurrentQuizId"] = quizId;
                LoadQuizDetails(quizId);
                LoadAllQuestions(quizId);
                DisplayQuestion(currentQuestionIndex);
            }
            else
            {
                lblMessage.Text = "Invalid Quiz ID.";
            }
        }
        else
        {
            if (Session["CurrentQuizId"] != null)
            {
                quizId = Convert.ToInt32(Session["CurrentQuizId"]);
            }
        }
    }
    private void LoadQuizDetails(int id)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT QuizTitle, TimeLimitInMinutes FROM Quizzes WHERE QuizId = @QuizId";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@QuizId", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblQuizTitle.Text = reader["QuizTitle"].ToString();
                    quizTimeLimit = Convert.ToInt32(reader["TimeLimitInMinutes"]);
                }
            }
        }
    }

    public int GetTimeLimit()
    {
        return quizTimeLimit;
    }
    private void LoadAllQuestions(int id)
    {
        List<QuizQuestion> questions = new List<QuizQuestion>();
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Questions WHERE QuizId = @QuizId";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@QuizId", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    questions.Add(new QuizQuestion
                    {
                        QuestionId = Convert.ToInt32(reader["QuestionId"]),
                        QuestionText = reader["QuestionText"].ToString(),
                        OptionA = reader["OptionA"].ToString(),
                        OptionB = reader["OptionB"].ToString(),
                        OptionC = reader["OptionC"].ToString(),
                        OptionD = reader["OptionD"].ToString(),
                        OptionE = reader["OptionE"].ToString(),
                        OptionF = reader["OptionF"].ToString(),
                        CorrectOption = reader["CorrectOption"].ToString()
                    });
                }
            }
        }
        // Store the list of questions in Session
        Session["QuizQuestions"] = questions;
        Session["CurrentQuestionIndex"] = 0;
    }
    private void DisplayQuestion(int index)
    {
        if (Session["QuizQuestions"] != null)
        {
            List<QuizQuestion> questions = (List<QuizQuestion>)Session["QuizQuestions"];
            if (index < questions.Count)
            {
                QuizQuestion q = questions[index];

                // Frontend HTML controls ko update karein
                question_text.InnerHtml = (index + 1) + ". " + q.QuestionText;
                options_container.InnerHtml = ""; // Clear previous options

                // Dynamically options ko create karein
                CreateOptionControl("OptionA", q.OptionA);
                CreateOptionControl("OptionB", q.OptionB);
                CreateOptionControl("OptionC", q.OptionC);
                CreateOptionControl("OptionD", q.OptionD);

                // Check karein aur agar value hai to hi Option E aur F ko create karein
                if (!string.IsNullOrEmpty(q.OptionE))
                {
                    CreateOptionControl("OptionE", q.OptionE);
                }
                if (!string.IsNullOrEmpty(q.OptionF))
                {
                    CreateOptionControl("OptionF", q.OptionF);
                }

                // Button visibility
                if (index == questions.Count - 1)
                {
                    btnNext.Visible = false;
                    btnFinish.Visible = true;
                }
                else
                {
                    btnNext.Visible = true;
                    btnFinish.Visible = false;
                }
            }
            else
            {
                lblMessage.Text = "Quiz finished! Click 'Finish Quiz' to see your results.";
                btnNext.Visible = false;
                btnFinish.Visible = true;
            }
        }
    }
    private void CreateOptionControl(string optionValue, string optionText)
    {
        // Create a custom control for each option
        // Instead of using a control, we will use a simple HTML radio button
        string html = $"<div class='list-group-item'><label class='mb-0'><input type='radio' name='quiz_option' value='{optionValue}' /> {optionText}</label></div>";
        options_container.InnerHtml += html;
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        string selectedOption = Request.Form["quiz_option"];
        if (string.IsNullOrEmpty(selectedOption))
        {
            lblMessage.Text = "Please select an answer before proceeding.";
            return;
        }

        List<QuizQuestion> questions = (List<QuizQuestion>)Session["QuizQuestions"];
        currentQuestionIndex = Convert.ToInt32(Session["CurrentQuestionIndex"]);
        questions[currentQuestionIndex].UserAnswer = selectedOption;

        currentQuestionIndex++;
        Session["CurrentQuestionIndex"] = currentQuestionIndex;

        DisplayQuestion(currentQuestionIndex);
    }

    protected void btnFinish_Click(object sender, EventArgs e)
    {
        string selectedOption = Request.Form["quiz_option"];
        if (!string.IsNullOrEmpty(selectedOption))
        {
            List<QuizQuestion> questions = (List<QuizQuestion>)Session["QuizQuestions"];
            currentQuestionIndex = Convert.ToInt32(Session["CurrentQuestionIndex"]);
            questions[currentQuestionIndex].UserAnswer = selectedOption;
        }

        Response.Redirect("Result.aspx");
    }
}