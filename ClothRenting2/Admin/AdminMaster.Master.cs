using System;
using System.Web.UI;

namespace ClothRenting2.Admin
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
      protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Response.Clear();
                Response.Redirect("~/Admin/AdminLogin.aspx", true);
            }
        }


    }
}
