using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


public partial class Site : System.Web.UI.MasterPage
{
    //protected HtmlGenericControl liViewQuizzes;
    //protected HtmlGenericControl liLeaderboard;
    //protected HtmlGenericControl liDashboard;
    //protected HtmlGenericControl liLogout;
    //protected HtmlGenericControl liRegister;
    //protected HtmlGenericControl liLogin;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] != null)
        {
            // User logged in hai
            liViewQuizzes.Visible = true;
            liLeaderboard.Visible = true;
            liDashboard.Visible = true;
            liLogout.Visible = true;
            liProfile.Visible = true;
            liRegister.Visible = false;
            liLogin.Visible = false;

            if (Session["IsAdmin"] != null && Convert.ToBoolean(Session["IsAdmin"]) == true)
            {
                lnkAdminDashboard.Visible = true;
                lnkManageCategories.Visible = true;
            }
            else
            {
                lnkAdminDashboard.Visible = false;
                lnkManageCategories.Visible = false;
            }
        }
        else
        {
            // User logged out hai
            liProfile.Visible = false;
            liViewQuizzes.Visible = false;
            liLeaderboard.Visible = false;
            liDashboard.Visible = false;
            liLogout.Visible = false;
            liRegister.Visible = true;
            liLogin.Visible = true;
            lnkManageCategories.Visible = false;
        }
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.Abandon();
        Response.Cookies.Clear();
        Response.Redirect("Login.aspx");
    }
}
