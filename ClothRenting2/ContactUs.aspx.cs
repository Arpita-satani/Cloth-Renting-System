using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ClothRenting2
{
    public partial class ContactUs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO ContactMessages (Name, Email, Message, CreatedAt) VALUES (@Name,@Email,@Message,GETDATE())",
                    con);

                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Message", txtMessage.Text);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            lblMsg.Text = "✔ Thank you! Your message has been sent.";
            lblMsg.ForeColor = System.Drawing.Color.Green;

            txtName.Text = txtEmail.Text = txtMessage.Text = "";
        }
    }
}
