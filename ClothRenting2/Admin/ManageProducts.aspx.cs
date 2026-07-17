using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI;

namespace ClothRenting2.Admin
{
    public partial class ManageProducts : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["ClothDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Admin"] == null)
            {
                Response.Redirect("~/Admin/AdminLogin.aspx");
                return;
            }
            if (!IsPostBack)
            {
                LoadSubCategories();
                LoadProducts();
            }
        }

        // Sub-categories load karne ke liye
        void LoadSubCategories()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlDataAdapter da = new SqlDataAdapter(
                    "SELECT CategoryId, CategoryName FROM Categories WHERE ParentCategoryID IS NOT NULL", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlCategory.DataSource = dt;
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataBind();

                ddlCategory.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Sub Category", "0"));
            }
        }

        // GridView mein products load karne ke liye
        void LoadProducts()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"
                SELECT p.ProductID, p.ProductName, p.Price, p.Image, p.Description,
                       c.CategoryName
                FROM Products p
                INNER JOIN Categories c ON p.CategoryID = c.CategoryId
                ORDER BY p.ProductID DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
        }

        // Save aur Update logic (Conversion Error Fix ke saath)
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string imageName = "";

            // 1. Image Upload Logic
            if (fuImage.HasFile)
            {
                imageName = Guid.NewGuid().ToString() + Path.GetFileName(fuImage.FileName);
                string folderPath = Server.MapPath("~/Admin/Images/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                fuImage.SaveAs(folderPath + imageName);
            }

            // 2. Numeric Conversion Fix (nvarchar to numeric error solution)
            decimal priceValue = 0;
            string cleanPrice = txtPrice.Text.Replace("₹", "").Replace(",", "").Trim();

            if (!decimal.TryParse(cleanPrice, out priceValue))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please enter a valid price (Numbers only)!');", true);
                return;
            }

            if (ddlCategory.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please select a category!');", true);
                return;
            }

            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                // 3. Query selection (Insert vs Update)
                if (string.IsNullOrEmpty(hfProductID.Value))
                {
                    cmd.CommandText = @"INSERT INTO Products(ProductName, Description, Price, Image, CategoryID) 
                                      VALUES(@name, @desc, @price, @image, @cat)";
                    cmd.Parameters.AddWithValue("@image", imageName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(imageName))
                    {
                        cmd.CommandText = @"UPDATE Products SET ProductName=@name, Description=@desc, Price=@price, 
                                          Image=@image, CategoryID=@cat WHERE ProductID=@id";
                        cmd.Parameters.AddWithValue("@image", imageName);
                    }
                    else
                    {
                        cmd.CommandText = @"UPDATE Products SET ProductName=@name, Description=@desc, Price=@price, 
                                          CategoryID=@cat WHERE ProductID=@id";
                    }
                    cmd.Parameters.AddWithValue("@id", hfProductID.Value);
                }

                // 4. Common Parameters with Explicit Types
                cmd.Parameters.AddWithValue("@name", txtProductName.Text.Trim());
                cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim());

                // Sabse important change: Explicitly numeric type batana
                cmd.Parameters.Add("@price", SqlDbType.Decimal).Value = priceValue;

                cmd.Parameters.AddWithValue("@cat", ddlCategory.SelectedValue);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Product saved successfully!');", true);
                    ClearForm();
                    LoadProducts();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Database Error: " + ex.Message.Replace("'", "") + "');", true);
                }
            }
        }

        protected void gvProducts_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "EditRow")
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Products WHERE ProductID=@id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        hfProductID.Value = id.ToString();
                        txtProductName.Text = dr["ProductName"].ToString();
                        txtPrice.Text = Convert.ToDecimal(dr["Price"]).ToString("0.00");
                        txtDescription.Text = dr["Description"].ToString();
                        ddlCategory.SelectedValue = dr["CategoryID"].ToString();
                    }
                    con.Close();
                }
            }

            if (e.CommandName == "DeleteRow")
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE ProductID=@id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                LoadProducts();
            }
        }

        void ClearForm()
        {
            hfProductID.Value = "";
            txtProductName.Text = "";
            txtPrice.Text = "";
            txtDescription.Text = "";
            ddlCategory.SelectedIndex = 0;
        }
    }
}