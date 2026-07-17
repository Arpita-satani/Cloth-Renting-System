using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace ClothRenting2
{
    public partial class MyOrders : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Login Check
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                FetchUserOrders();
            }
        }

        private void FetchUserOrders()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                // Join query taaki Product ka naam dikh sake
                string query = @"SELECT O.OrderID, O.TotalAmount, O.Status, O.StartDate, O.EndDate, P.ProductName 
                               FROM Orders O 
                               LEFT JOIN Products P ON O.ProductID = P.ProductID 
                               WHERE O.UserID = @uid 
                               ORDER BY O.OrderID DESC";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@uid", Session["UserID"]);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                try
                {
                    sda.Fill(dt);
                    gvOrders.DataSource = dt;
                    gvOrders.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }
            }
        }

        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblStatus = (Label)e.Row.FindControl("lblStatus");
                string status = lblStatus.Text;

                // Status ke hisaab se CSS class badalna
                if (status == "Pending")
                    lblStatus.CssClass += " status-pending";
                else if (status == "Completed")
                    lblStatus.CssClass += " status-completed";
            }
        }
    }
}