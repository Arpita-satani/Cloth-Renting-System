using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ClothRenting2.Admin
{
    public partial class AdminLogin : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Admins WHERE Email=@E AND Password=@P", con);

                cmd.Parameters.AddWithValue("@E", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@P", txtPassword.Text.Trim());

                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count == 1)
                {
                    Session["Admin"] = txtEmail.Text;
                    Response.Redirect("AdminDashboard.aspx");
                }
                else
                {
                    lblMsg.Text = "Invalid Admin Credentials";
                }
            }
        }
    }
}