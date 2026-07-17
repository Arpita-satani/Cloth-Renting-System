using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ClothRenting2.Admin
{
    public partial class ViewMessages : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Response.Redirect("~/Admin/AdminLogin.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LoadMessages();
            }
        }

        void LoadMessages()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT * FROM ContactMessages ORDER BY MessageID DESC", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvMessages.DataSource = dt;
                gvMessages.DataBind();
            }
        }

        protected void gvMessages_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteMsg")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM ContactMessages WHERE MessageID=@id", con);

                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadMessages();
            }
        }
    }
}
