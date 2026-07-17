using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace ClothRenting2
{
    public partial class Login : Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    @"SELECT UserID, FullName 
                      FROM Users 
                      WHERE Email=@E 
                      AND Password=@P 
                      AND IsActive=1", con);

                cmd.Parameters.AddWithValue("@E", txtEmail.Text);
                cmd.Parameters.AddWithValue("@P", txtPassword.Text);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    Session["UserID"] = dr["UserID"].ToString();
                    Session["UserName"] = dr["FullName"].ToString();

                    string returnUrl = Request.QueryString["ReturnUrl"];

                    if (!string.IsNullOrEmpty(returnUrl))
                        Response.Redirect(returnUrl);
                    else
                        Response.Redirect("Default.aspx");
                }
                else
                {
                    lblMsg.Text = "Invalid Email or Password";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }

                con.Close();
            }
        }
    }
}