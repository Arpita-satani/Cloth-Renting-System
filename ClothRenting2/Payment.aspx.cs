using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace ClothRenting2
{
    public partial class Payment : System.Web.UI.Page
    {

        string connStr = ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {

                string orderId = Request.QueryString["OrderID"];

                if (orderId == null) return;

                lblOrderID.Text = orderId;

                LoadSummary(orderId);

            }
        }

        void LoadSummary(string oid)
        {

            using (SqlConnection con = new SqlConnection(connStr))
            {

                string sql = @"SELECT O.TotalAmount,O.StartDate,O.EndDate,
                               P.ProductName,P.Price
                               FROM Orders O
                               LEFT JOIN Products P
                               ON O.ProductID=P.ProductID
                               WHERE O.OrderID=@oid";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@oid", oid);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {

                    lblAmount.Text = Convert.ToDecimal(dr["TotalAmount"]).ToString("0.00");

                    lblProductName.Text = dr["ProductName"].ToString();

                    lblPrice.Text = Convert.ToDecimal(dr["Price"]).ToString("0.00");

                    DateTime s = Convert.ToDateTime(dr["StartDate"]);
                    DateTime e = Convert.ToDateTime(dr["EndDate"]);

                    int days = (e - s).Days;

                    if (days <= 0) days = 1;

                    lblDays.Text = days.ToString();

                }

            }

        }

        protected void ddlMethod_SelectedIndexChanged(object sender, EventArgs e)
        {

            string m = ddlMethod.SelectedValue;

            pnlCard.Visible = (m == "Credit Card" || m == "Debit Card");

            pnlUPI.Visible = (m == "UPI");

        }

        protected void btnPay_Click(object sender, EventArgs e)
        {

            string method = ddlMethod.SelectedValue;

            if (method == "")
            {
                Alert("Select payment method");
                return;
            }

            if (method.Contains("Card"))
            {

                if (!Regex.IsMatch(txtCard.Text, @"^\d{16}$"))
                {
                    Alert("Invalid Card Number");
                    return;
                }

                if (!Regex.IsMatch(txtCVV.Text, @"^\d{3}$"))
                {
                    Alert("Invalid CVV");
                    return;
                }

                if (!Regex.IsMatch(txtExpiry.Text, @"^(0[1-9]|1[0-2])\/([0-9]{2})$"))
                {
                    Alert("Expiry format MM/YY");
                    return;
                }

                string[] exp = txtExpiry.Text.Split('/');

                int month = Convert.ToInt32(exp[0]);

                int year = Convert.ToInt32("20" + exp[1]);

                DateTime expiry = new DateTime(year, month, DateTime.DaysInMonth(year, month));

                if (expiry < DateTime.Now)
                {
                    Alert("Card Expired");
                    return;
                }

            }

            ExecutePayment();

        }

        void ExecutePayment()
        {

            using (SqlConnection con = new SqlConnection(connStr))
            {

                con.Open();

                SqlTransaction trans = con.BeginTransaction();

                try
                {

                    SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Payments(OrderID,PaymentMethod,Amount,PaymentStatus,PaymentDate) VALUES(@oid,@m,@a,'Paid',GETDATE())",
                    con, trans);

                    cmd.Parameters.AddWithValue("@oid", lblOrderID.Text);
                    cmd.Parameters.AddWithValue("@m", ddlMethod.SelectedValue);
                    cmd.Parameters.AddWithValue("@a", lblAmount.Text);

                    cmd.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand(
                    "UPDATE Orders SET Status='Completed' WHERE OrderID=@oid",
                    con, trans);

                    cmd2.Parameters.AddWithValue("@oid", lblOrderID.Text);

                    cmd2.ExecuteNonQuery();

                    trans.Commit();

                    Response.Redirect("PaymentSuccess.aspx");

                }

                catch (Exception ex)
                {

                    trans.Rollback();

                    Alert("Payment Failed: " + ex.Message);

                }

            }

        }

        void Alert(string msg)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "a", "alert('" + msg + "')", true);

        }

    }
}