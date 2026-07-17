using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClothRenting2.Admin
{
    public partial class ManageOrders : Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null) { Response.Redirect("~/Admin/AdminLogin.aspx"); return; }
            if (!IsPostBack) LoadOrders();
        }

        private void LoadOrders()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string sql = @"SELECT o.OrderID, u.FullName AS UserName, u.Email, o.StartDate, o.EndDate, o.TotalAmount, o.Status 
                             FROM Orders o 
                             LEFT JOIN Users u ON o.UserID = u.UserID 
                             ORDER BY o.OrderID DESC";
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvOrders.DataSource = dt;
                gvOrders.DataBind();
                lblTotal.Text = dt.Rows.Count.ToString();
            }
        }

        protected void gvOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // 1. Status Styling
                Label lbl = (Label)e.Row.FindControl("lblStatus");
                string st = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
                lbl.CssClass = "status-badge " + st.ToLower();

                // 2. Fetching Product Details (Dual Check Logic)
                int oid = Convert.ToInt32(gvOrders.DataKeys[e.Row.RowIndex].Value);
                Repeater rpt = (Repeater)e.Row.FindControl("rptItems");

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    // Check 1: OrderItems table se data uthao
                    string sql = @"SELECT oi.Quantity, oi.Size, p.ProductName FROM OrderItems oi 
                                 JOIN Products p ON oi.ProductID = p.ProductID WHERE oi.OrderID = @oid";

                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    da.SelectCommand.Parameters.AddWithValue("@oid", oid);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Check 2: Agar OrderItems khali hai, toh direct Orders table se join karo
                    if (dt.Rows.Count == 0)
                    {
                        string backupSql = @"SELECT '1' as Quantity, 'N/A' as Size, p.ProductName 
                                           FROM Orders o JOIN Products p ON o.ProductID = p.ProductID 
                                           WHERE o.OrderID = @oid";
                        SqlDataAdapter daB = new SqlDataAdapter(backupSql, con);
                        daB.SelectCommand.Parameters.AddWithValue("@oid", oid);
                        daB.Fill(dt);
                    }

                    rpt.DataSource = dt;
                    rpt.DataBind();
                }

                // 3. Set Dropdown selection
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlStatus");
                if (ddl != null) ddl.SelectedValue = st;
            }
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int oid = Convert.ToInt32(e.CommandArgument);
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                if (e.CommandName == "UpdateStatus")
                {
                    GridViewRow row = (GridViewRow)((Control)e.CommandSource).NamingContainer;
                    DropDownList ddl = (DropDownList)row.FindControl("ddlStatus");
                    new SqlCommand("UPDATE Orders SET Status='" + ddl.SelectedValue + "' WHERE OrderID=" + oid, con).ExecuteNonQuery();
                }
                else if (e.CommandName == "DeleteOrder")
                {
                    new SqlCommand("DELETE FROM OrderItems WHERE OrderID=" + oid, con).ExecuteNonQuery();
                    new SqlCommand("DELETE FROM Payments WHERE OrderID=" + oid, con).ExecuteNonQuery();
                    new SqlCommand("DELETE FROM Orders WHERE OrderID=" + oid, con).ExecuteNonQuery();
                }
            }
            LoadOrders();
        }
    }
}