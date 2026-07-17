using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ClothRenting2
{
    public partial class ProductList : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int categoryId = 0;

                if (Request.QueryString["cid"] != null)
                {
                    int.TryParse(Request.QueryString["cid"], out categoryId);
                }

                LoadProducts(categoryId);
            }
        }

        void LoadProducts(int categoryId)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da;

                if (categoryId > 0)
                {
                    da = new SqlDataAdapter("SELECT * FROM Products WHERE CategoryID=@cid", con);
                    da.SelectCommand.Parameters.AddWithValue("@cid", categoryId);
                }
                else
                {
                    da = new SqlDataAdapter("SELECT * FROM Products", con);
                }

                DataTable dt = new DataTable();
                da.Fill(dt);

                rpProducts.DataSource = dt;
                rpProducts.DataBind();
            }
        }
    }
}