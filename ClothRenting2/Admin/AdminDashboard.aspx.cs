using System;
using System.Data.SqlClient;
using System.Configuration;

namespace ClothRenting2.Admin
{
    public partial class AdminDashboard : System.Web.UI.Page
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
                LoadCounts();
            }
        }

        void LoadCounts()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                // USERS
                try
                {
                    SqlCommand cmdUsers = new SqlCommand("SELECT COUNT(*) FROM Users", con);
                    lblUsers.Text = cmdUsers.ExecuteScalar().ToString();
                }
                catch
                {
                    lblUsers.Text = "0";
                }

                // PRODUCTS
                SqlCommand cmdProducts = new SqlCommand("SELECT COUNT(*) FROM Products", con);
                lblProducts.Text = cmdProducts.ExecuteScalar().ToString();

                // ORDERS & REVENUE (future ready)
                try
                {
                    SqlCommand cmdOrders = new SqlCommand("SELECT COUNT(*) FROM Orders", con);
                    lblOrders.Text = cmdOrders.ExecuteScalar().ToString();

                    SqlCommand cmdRevenue = new SqlCommand("SELECT ISNULL(SUM(TotalAmount),0) FROM Orders", con);
                    lblRevenue.Text = cmdRevenue.ExecuteScalar().ToString();
                }
                catch
                {
                    lblOrders.Text = "0";
                    lblRevenue.Text = "0";
                }

                con.Close();
            }
        }
    }
}
