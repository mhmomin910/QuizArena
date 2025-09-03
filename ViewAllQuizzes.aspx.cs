using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewAllQuizzes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        if (!IsPostBack)
        {
            BindCategories();
            BindQuizzes();
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
                ddlCategoryFilter.DataSource = cmd.ExecuteReader();
                ddlCategoryFilter.DataTextField = "CategoryName";
                ddlCategoryFilter.DataValueField = "CategoryId";
                ddlCategoryFilter.DataBind();
                ddlCategoryFilter.Items.Insert(0, new ListItem("All Categories", ""));
            }
        }
    }

    private void BindQuizzes(string searchKeyword = "", string categoryId = "")
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        string query = "SELECT Q.QuizId, Q.QuizTitle, Q.QuizDescription, Q.CreatedDate, C.CategoryName FROM Quizzes Q INNER JOIN Categories C ON Q.CategoryId = C.CategoryId WHERE 1=1";

        if (!string.IsNullOrEmpty(searchKeyword))
        {
            query += " AND (Q.QuizTitle LIKE '%' + @Search + '%' OR Q.QuizDescription LIKE '%' + @Search + '%')";
        }
        if (!string.IsNullOrEmpty(categoryId))
        {
            query += " AND Q.CategoryId = @CategoryId";
        }
        query += " ORDER BY Q.CreatedDate DESC";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                if (!string.IsNullOrEmpty(searchKeyword))
                {
                    cmd.Parameters.AddWithValue("@Search", searchKeyword);
                }
                if (!string.IsNullOrEmpty(categoryId))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                }
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    repeaterQuizzes.DataSource = reader;
                    repeaterQuizzes.DataBind();

                    if (repeaterQuizzes.Items.Count == 0)
                    {
                        lblNoQuizzesFound.Visible = true;
                    }
                    else
                    {
                        lblNoQuizzesFound.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    lblNoQuizzesFound.Visible = true;
                }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindQuizzes(txtSearch.Text.Trim(), ddlCategoryFilter.SelectedValue);
    }

    protected void ddlCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindQuizzes(txtSearch.Text.Trim(), ddlCategoryFilter.SelectedValue);
    }
}