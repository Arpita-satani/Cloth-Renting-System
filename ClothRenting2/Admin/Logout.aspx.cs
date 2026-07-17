using System;

namespace ClothRenting2.Admin
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("AdminLogin.aspx");
        }
    }
}