using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace ClothRenting2.Admin
{
    public partial class ManageCategories : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Response.Redirect("~/Admin/AdminLogin.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (!IsPostBack)
            {
                LoadParentCategories();
                LoadGrid();
            }
        }

        // Load Parent Categories
        void LoadParentCategories()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                "SELECT CategoryId, CategoryName FROM Categories WHERE ParentCategoryID IS NULL", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlParent.DataSource = dt;
                ddlParent.DataTextField = "CategoryName";
                ddlParent.DataValueField = "CategoryId";
                ddlParent.DataBind();

                ddlParent.Items.Insert(0,
                new System.Web.UI.WebControls.ListItem("No Parent (Main Category)", "0"));
            }
        }

        // Load Categories Grid
        void LoadGrid()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"SELECT c.CategoryId,
                                c.CategoryName,
                                ISNULL(p.CategoryName,'Main Category') AS ParentName,
                                c.CategoryImage
                                FROM Categories c
                                LEFT JOIN Categories p
                                ON c.ParentCategoryID = p.CategoryId";

                SqlDataAdapter da = new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvCategories.DataSource = dt;
                gvCategories.DataBind();
            }
        }

        // Save Category
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int parentID = ddlParent.SelectedValue == "0" ? 0 : Convert.ToInt32(ddlParent.SelectedValue);

            string imgName = "";

            if (fuImage.HasFile)
            {
                imgName = Guid.NewGuid().ToString() + Path.GetFileName(fuImage.FileName);

                string folderPath = Server.MapPath("~/Admin/Images/");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                fuImage.SaveAs(folderPath + imgName);
            }

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd;

                // INSERT
                if (hfCategoryID.Value == "")
                {
                    cmd = new SqlCommand(
                    "INSERT INTO Categories(CategoryName,ParentCategoryID,CategoryImage) VALUES(@name,@parent,@img)", con);

                    cmd.Parameters.AddWithValue("@name", txtCategoryName.Text);
                    cmd.Parameters.AddWithValue("@parent", parentID == 0 ? (object)DBNull.Value : parentID);
                    cmd.Parameters.AddWithValue("@img", imgName);
                }
                else // UPDATE
                {
                    if (imgName != "")
                    {
                        cmd = new SqlCommand(
                        "UPDATE Categories SET CategoryName=@name,ParentCategoryID=@parent,CategoryImage=@img WHERE CategoryId=@id", con);

                        cmd.Parameters.AddWithValue("@img", imgName);
                    }
                    else
                    {
                        cmd = new SqlCommand(
                        "UPDATE Categories SET CategoryName=@name,ParentCategoryID=@parent WHERE CategoryId=@id", con);
                    }

                    cmd.Parameters.AddWithValue("@id", hfCategoryID.Value);
                    cmd.Parameters.AddWithValue("@name", txtCategoryName.Text);
                    cmd.Parameters.AddWithValue("@parent", parentID == 0 ? (object)DBNull.Value : parentID);
                }

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            txtCategoryName.Text = "";
            hfCategoryID.Value = "";

            LoadParentCategories();
            LoadGrid();
        }

        // GridView Commands
        protected void gvCategories_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            // EDIT
            if (e.CommandName == "EditRow")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM Categories WHERE CategoryId=@id", con);

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    hfCategoryID.Value = dt.Rows[0]["CategoryId"].ToString();
                    txtCategoryName.Text = dt.Rows[0]["CategoryName"].ToString();

                    string parentID = dt.Rows[0]["ParentCategoryID"] == DBNull.Value ? "0" : dt.Rows[0]["ParentCategoryID"].ToString();

                    // SAFE SelectedValue
                    if (ddlParent.Items.FindByValue(parentID) != null)
                        ddlParent.SelectedValue = parentID;
                    else
                        ddlParent.SelectedValue = "0";
                }
            }

            // DELETE
            if (e.CommandName == "DeleteRow")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();

                    // Check child categories
                    SqlCommand checkChild = new SqlCommand(
                    "SELECT COUNT(*) FROM Categories WHERE ParentCategoryID=@cid", con);

                    checkChild.Parameters.AddWithValue("@cid", id);
                    int childCount = (int)checkChild.ExecuteScalar();

                    if (childCount > 0)
                    {
                        Response.Write("<script>alert('Delete nahi ho sakta. Is category ke andar sub categories hain.')</script>");
                        return;
                    }

                    // Check products
                    SqlCommand checkProduct = new SqlCommand(
                    "SELECT COUNT(*) FROM Products WHERE CategoryID=@cid", con);

                    checkProduct.Parameters.AddWithValue("@cid", id);
                    int productCount = (int)checkProduct.ExecuteScalar();

                    if (productCount > 0)
                    {
                        Response.Write("<script>alert('Delete nahi ho sakta. Is category me products available hain.')</script>");
                        return;
                    }

                    // Check SubCategories
                    SqlCommand checkSub = new SqlCommand(
                    "SELECT COUNT(*) FROM SubCategories WHERE CategoryID=@cid", con);

                    checkSub.Parameters.AddWithValue("@cid", id);
                    int subCount = (int)checkSub.ExecuteScalar();

                    if (subCount > 0)
                    {
                        Response.Write("<script>alert('Delete nahi ho sakta. Is category me subcategories hain.')</script>");
                        return;
                    }

                    // Delete
                    SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Categories WHERE CategoryId=@cid", con);

                    cmd.Parameters.AddWithValue("@cid", id);
                    cmd.ExecuteNonQuery();

                    Response.Write("<script>alert('Category Deleted Successfully')</script>");
                }

                LoadGrid();
            }
        }
    }
}