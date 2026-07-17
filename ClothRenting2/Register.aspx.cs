using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Xml.Linq;

namespace ClothRenting2
{
    public partial class Register : Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString))
            {
                con.Open();

                // Check if email exists
                SqlCommand checkCmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Users WHERE Email=@Email", con);
                checkCmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                int exists = (int)checkCmd.ExecuteScalar();
                if (exists > 0)
                {
                    lblMsg.Text = "❌ Email already registered";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Insert user
                SqlCommand insertCmd = new SqlCommand(@"
                    INSERT INTO Users
                    (FullName, Email, Mobile, Password, IsActive, CreatedDate)
                    VALUES
                    (@FullName, @Email, @Mobile, @Password, 1, GETDATE())", con);

                insertCmd.Parameters.AddWithValue("@FullName", txtName.Text);
                insertCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                insertCmd.Parameters.AddWithValue("@Mobile", txtMobile.Text);
                insertCmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                insertCmd.ExecuteNonQuery();

                Response.Redirect("Login.aspx");
            }
        }
    }
}