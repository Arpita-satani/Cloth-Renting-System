using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ClothRenting2
{
    public partial class SubCategories : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["pid"] != null)
                {
                    int pid;
                    if (int.TryParse(Request.QueryString["pid"], out pid))
                    {
                        LoadSubCategories(pid);
                    }
                }
            }
        }

        void LoadSubCategories(int parentId)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT * FROM Categories WHERE ParentCategoryID=@pid", con);

                da.SelectCommand.Parameters.AddWithValue("@pid", parentId);

                DataTable dt = new DataTable();
                da.Fill(dt);

                rpSub.DataSource = dt;
                rpSub.DataBind();
            }
        }
    }
}