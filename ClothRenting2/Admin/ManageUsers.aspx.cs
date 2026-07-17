using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ClothRenting2.Admin
{
    public partial class ManageUsers : System.Web.UI.Page
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
                LoadUsers();
            }
        }

        private void LoadUsers()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string sql = "SELECT UserID, FullName, Email, Mobile, IsActive, CreatedDate FROM Users ORDER BY UserID DESC";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);

                DataTable dt = new DataTable();

                da.Fill(dt);

                gvUsers.DataSource = dt;

                gvUsers.DataBind();
            }
        }

        protected void gvUsers_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {

            if (e.CommandName == "DeleteUser")
            {
                int userId = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    try
                    {
                        SqlCommand cmd = new SqlCommand("DELETE FROM Users WHERE UserID=@uid", con);

                        cmd.Parameters.AddWithValue("@uid", userId);

                        cmd.ExecuteNonQuery();

                        Response.Write("<script>alert('User Deleted Successfully')</script>");
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('Delete Error: " + ex.Message.Replace("'", "") + "')</script>");
                    }
                }

                LoadUsers();
            }

            if (e.CommandName == "BlockUser")
            {
                int userId = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE Users SET IsActive = CASE WHEN IsActive=1 THEN 0 ELSE 1 END WHERE UserID=@uid", con);

                    cmd.Parameters.AddWithValue("@uid", userId);

                    cmd.ExecuteNonQuery();
                }

                LoadUsers();
            }
        }
    }
}