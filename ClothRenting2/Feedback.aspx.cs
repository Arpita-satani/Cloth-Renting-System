using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace ClothRenting2
{
    public partial class Feedback : System.Web.UI.Page
    {
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            int userId = Convert.ToInt32(Session["UserID"]);

            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Feedback (UserID, Name, Email, FeedbackText, CreatedAt) VALUES (@UserID,@Name,@Email,@Feedback,GETDATE())",
                    con);

                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Feedback", txtFeedback.Text);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            lblMsg.Text = "✔ Thank you for your feedback!";
            lblMsg.ForeColor = System.Drawing.Color.Green;

            txtName.Text = "";
            txtEmail.Text = "";
            txtFeedback.Text = "";
        }
    }
}
