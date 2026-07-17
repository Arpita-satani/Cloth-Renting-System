using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace ClothRenting2
{
    public partial class Categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCategories();
            }
        }

        void LoadCategories()
        {
            SqlConnection con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString);

            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT CategoryID, CategoryName FROM Categories", con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            rptCategories.DataSource = dt;
            rptCategories.DataBind();
        }
    }
}