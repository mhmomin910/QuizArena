using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ManageCategories : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Admin authentication check (important)
        if (Session["UserId"] == null || Session["IsAdmin"] == null || Convert.ToBoolean(Session["IsAdmin"]) == false)
        {
            Response.Redirect("Login.aspx"); // Non-admin users ko login page par redirect karein
        }

        if (!IsPostBack)
        {
            BindCategoriesGrid();
        }
    }
    private void BindCategoriesGrid()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT CategoryId, CategoryName FROM Categories ORDER BY CategoryId";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridCategories.DataSource = dt;
                gridCategories.DataBind();
            }
        }
    }

    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtCategoryName.Text.Trim()))
        {
            lblMessage.Text = "Category name cannot be empty.";
            lblMessage.CssClass = "text-danger";
            return;
        }

        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Categories (CategoryName) VALUES (@CategoryName)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@CategoryName", txtCategoryName.Text.Trim());
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    lblMessage.Text = "Category added successfully.";
                    lblMessage.CssClass = "text-success";
                    txtCategoryName.Text = ""; // Clear the textbox
                    BindCategoriesGrid(); // Grid ko refresh karein
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                    lblMessage.CssClass = "text-danger";
                }
            }
        }
    }

    // GridView events for editing and deleting
    protected void gridCategories_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridCategories.EditIndex = e.NewEditIndex;
        BindCategoriesGrid();
    }

    protected void gridCategories_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridCategories.EditIndex = -1;
        BindCategoriesGrid();
    }

    protected void gridCategories_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gridCategories.Rows[e.RowIndex];
        int categoryId = Convert.ToInt32(gridCategories.DataKeys[e.RowIndex].Value);
        TextBox txtEditCategoryName = (TextBox)row.FindControl("txtEditCategoryName");
        string newCategoryName = txtEditCategoryName.Text.Trim();

        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "UPDATE Categories SET CategoryName = @CategoryName WHERE CategoryId = @CategoryId";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@CategoryName", newCategoryName);
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    gridCategories.EditIndex = -1;
                    BindCategoriesGrid();
                    lblMessage.Text = "Category updated successfully.";
                    lblMessage.CssClass = "text-success";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error updating category: " + ex.Message;
                    lblMessage.CssClass = "text-danger";
                }
            }
        }
    }

    protected void gridCategories_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int categoryId = Convert.ToInt32(gridCategories.DataKeys[e.RowIndex].Value);

        string connectionString = ConfigurationManager.ConnectionStrings["QuizArena"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "DELETE FROM Categories WHERE CategoryId = @CategoryId";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    BindCategoriesGrid();
                    lblMessage.Text = "Category deleted successfully.";
                    lblMessage.CssClass = "text-success";
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error deleting category: " + ex.Message;
                    lblMessage.CssClass = "text-danger";
                }
            }
        }
    }
}