using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ClothRenting2
{
    public partial class Products : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadParentCategories();
            }
        }

        void LoadParentCategories()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT * FROM Categories WHERE ParentCategoryID IS NULL", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                rpParent.DataSource = dt;
                rpParent.DataBind();
            }
        }
    }
}