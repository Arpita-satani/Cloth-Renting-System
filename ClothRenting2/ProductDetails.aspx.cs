using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace ClothRenting2
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    LoadProduct(id);
                }
            }
        }

        void LoadProduct(int productId)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT ProductName, Price, Image, Description FROM Products WHERE ProductID=@id", con);

                cmd.Parameters.AddWithValue("@id", productId);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    lblName.InnerText = dr["ProductName"].ToString();
                    lblPrice.InnerText = dr["Price"].ToString();
                    lblDescription.InnerText = dr["Description"].ToString();

                    imgProduct.ImageUrl = "Admin/Images/" + dr["Image"].ToString();
                }

                con.Close();
            }
        }
    }
}