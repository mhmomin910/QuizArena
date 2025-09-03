using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

// Ek class bana rahe hain jismein question ki details store karenge
[Serializable]
public class QuizQuestion
{
    public int QuestionId { get; set; }
    public string QuestionText { get; set; }
    public string OptionA { get; set; }
    public string OptionB { get; set; }
    public string OptionC { get; set; }
    public string OptionD { get; set; }
    public string OptionE { get; set; }
    public string OptionF { get; set; }
    public string CorrectOption { get; set; }
    public string UserAnswer { get; set; } // User ke answer ko store karne ke liye
}
public partial class Result : System.Web.UI.Page
{
    string conn = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            // Agar user logged in nahi hai ya session expired ho gaya, to login page par redirect karein
            Response.Redirect("Login.aspx");
            return;
        }

        if (!IsPostBack)
        {
            CalculateAndDisplayResults();
        }
    }
    private void CalculateAndDisplayResults()
    {
        int quizId = Convert.ToInt32(Session["CurrentQuizId"]);
        decimal negativeMarks = 0;

        using (SqlConnection con = new SqlConnection(conn))
        {
            string query = "SELECT NegativeMarks FROM Quizzes WHERE QuizId = @QuizId";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@QuizId", quizId);
                con.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    negativeMarks = Convert.ToDecimal(result);
                }
            }
        }

        if (Session["QuizQuestions"] != null && Session["CurrentQuizId"] != null)
        {
#pragma warning disable CS0436 // Type conflicts with imported type
            List<QuizQuestion> questions = (List<QuizQuestion>)Session["QuizQuestions"];
            decimal finalScore = 0;

            foreach (var question in questions)
            {
                if (question.UserAnswer == question.CorrectOption)
                {
                    finalScore++;
                }
                else
                {
                    finalScore -= negativeMarks;
                }
            }

            // Score display karein
            lblScore.Text = $"{finalScore} / {questions.Count}";

            // Results ko Repeater se bind karein
            repeaterResults.DataSource = questions;
            repeaterResults.DataBind();

            // Score ko database mein save karein
            SaveResultToDatabase(finalScore, questions.Count);

            // Session variables ko clear karein
            Session.Remove("QuizQuestions");
            Session.Remove("CurrentQuestionIndex");
            Session.Remove("CurrentQuizId");
        }
        else
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
    private void SaveResultToDatabase(decimal score, int totalQuestions)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        int userId = Convert.ToInt32(Session["UserId"]);
        int quizId = Convert.ToInt32(Session["CurrentQuizId"]);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Results (UserId, QuizId, Score, TotalQuestions, AttemptDate) VALUES (@UserId, @QuizId, @Score, @TotalQuestions, @AttemptDate)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@QuizId", quizId);
                cmd.Parameters.AddWithValue("@Score", score);
                cmd.Parameters.AddWithValue("@TotalQuestions", totalQuestions);
                cmd.Parameters.AddWithValue("@AttemptDate", DateTime.Now);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle the error (e.g., log it)
                    System.Diagnostics.Debug.WriteLine("Error saving result: " + ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}