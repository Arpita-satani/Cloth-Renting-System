using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace ClothRenting2
{
    public partial class RentNow : Page
    {
        string cs = ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                // User name set karna
                if (Session["UserName"] != null)
                    txtName.Text = Session["UserName"].ToString();

                // Product ID check karna QueryString se
                if (Request.QueryString["id"] == null)
                {
                    lblMsg.Text = "❌ Product not selected";
                    btnRentNow.Enabled = false;
                    return;
                }

                int productId = Convert.ToInt32(Request.QueryString["id"]);
                ViewState["ProductID"] = productId;

                // Product ki details (Price) fetch karna
                FetchProductDetails(productId);
            }
        }

        private void FetchProductDetails(int productId)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SELECT ProductName, Price FROM Products WHERE ProductID=@id", con);
                cmd.Parameters.AddWithValue("@id", productId);

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        decimal price = Convert.ToDecimal(dr["Price"]);
                        ViewState["Price"] = price;
                        txtPrice.Text = price.ToString("0.00");
                        txtTotal.Text = price.ToString("0.00");
                        // Agar aapke pas lblProductName hai toh:
                        // lblProductName.Text = dr["ProductName"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = "Error: " + ex.Message;
                }
            }
        }

        protected void DateChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        protected void ddlQty_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            DateTime fromDate, toDate;

            if (DateTime.TryParse(txtFromDate.Text, out fromDate) &&
                DateTime.TryParse(txtToDate.Text, out toDate))
            {
                int days = (toDate - fromDate).Days + 1;

                if (days > 0)
                {
                    if (ViewState["Price"] != null)
                    {
                        decimal price = Convert.ToDecimal(ViewState["Price"]);
                        int qty = ddlQty.SelectedValue != "" ? Convert.ToInt32(ddlQty.SelectedValue) : 1;

                        decimal total = price * days * qty;
                        txtTotal.Text = total.ToString("0.00");
                        lblMsg.Text = "";
                    }
                }
                else
                {
                    lblMsg.Text = "❌ 'To Date' cannot be before 'From Date'";
                    txtTotal.Text = "0.00";
                }
            }
        }

        protected void btnRentNow_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFromDate.Text) || string.IsNullOrWhiteSpace(txtToDate.Text))
            {
                lblMsg.Text = "❌ Please select rent dates";
                return;
            }

            try
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                int productId = Convert.ToInt32(ViewState["ProductID"]);
                string mobile = txtMobile.Text;
                DateTime startDate = Convert.ToDateTime(txtFromDate.Text);
                DateTime endDate = Convert.ToDateTime(txtToDate.Text);
                decimal total = Convert.ToDecimal(txtTotal.Text);

                int orderId;

                using (SqlConnection con = new SqlConnection(cs))
                {
                    // Update: ProductID column add kiya gaya hai query mein
                    string query = @"INSERT INTO Orders(UserID, ProductID, Mobile, StartDate, EndDate, TotalAmount, Status) 
                                   VALUES(@uid, @pid, @mobile, @start, @end, @total, 'Pending'); 
                                   SELECT SCOPE_IDENTITY();";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@pid", productId);
                    cmd.Parameters.AddWithValue("@mobile", mobile);
                    cmd.Parameters.AddWithValue("@start", startDate);
                    cmd.Parameters.AddWithValue("@end", endDate);
                    cmd.Parameters.AddWithValue("@total", total);

                    con.Open();
                    orderId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Payment page par redirect karna naye OrderID ke saath
                Response.Redirect("Payment.aspx?OrderID=" + orderId);
            }
            catch (Exception ex)
            {
                lblMsg.Text = "❌ Error saving order: " + ex.Message;
            }
        }
    }
}