using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ClothRenting2.Admin
{
    public partial class ViewFeedback : System.Web.UI.Page
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
                LoadFeedback();
        }

        void LoadFeedback()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT * FROM Feedback ORDER BY FeedbackID DESC", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvFeedback.DataSource = dt;
                gvFeedback.DataBind();
            }
        }

        protected void gvFeedback_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteFeedback")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(
                        "DELETE FROM Feedback WHERE FeedbackID=@id", con);

                    cmd.Parameters.AddWithValue("@id", id);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                LoadFeedback();
            }
        }
    }
}
