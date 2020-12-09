using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using NHibernate;
using NHibernate.Criterion;
using Foods;
using DataAccess;

namespace Foods
{
    public partial class Category_ : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DBConnection db = new DBConnection();
        string query;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Category categor = new Category();

            if (!this.IsPostBack)
            {
                FillGrid();
            }

        }
        public void clear()
        {
            TBCategoryType.Text = "";            
            lblcategory.Text = "";
        }

        public void FillGrid()
        {
            try
            {
                DataTable dt_ = new DataTable();
                //dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(ProductTypeID AS VARCHAR(200)) + ']-' + ProductTypeName ) as [ProductTypeName], ProductTypeID from tbl_producttype");
                dt_ = DBConnection.GetQueryData("select ProductTypeID, ProductTypeName from tbl_producttype where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                GVCategory.DataSource = dt_;
                GVCategory.DataBind();
                ViewState["Category"] = dt_;
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        private void SearchRecord()
        {
            try
            {
                FillGrid();
                DataTable _dt = (DataTable)ViewState["Category"];
                DataView dv = new DataView(_dt, "ProductTypeName LIKE '%" + TBSearchCAtegory.Text.Trim().ToUpper() + "%'", "[ProductTypeName] ASC", DataViewRowState.CurrentRows);
                DataTable dt_ = new DataTable();
                dt_ = dv.ToTable();
                GVCategory.DataSource = dt_;
                GVCategory.DataBind();
                ViewState["Entity"] = dt_;
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        protected void SeacrhBtn_Click(object sender, EventArgs e)
        {
            SearchRecord();
        }

        protected void GVCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "ModalPopUp();", true);

                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    HFCategory.Value = GVCategory.DataKeys[row.RowIndex].Values[0].ToString();
                    TBCategoryType.Text = Server.HtmlDecode(row.Cells[1].Text);
                }
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
        
        }

        public int RandomNumber()
        {
            int min;
            int max;
            min = 0;
            max = 10000;
            Random random = new Random();
            return random.Next(min, max);
        }

        private int Save()
        {
            int j = 1;

            try
            {

                

                string procatid = RandomNumber().ToString();

                query = " INSERT INTO [dbo].[tbl_producttype] " +
                                " ([ProductTypeID],[ProductTypeName],[CreateBy],[CreatedAt],[IsActive],[CompanyId],[BranchId]) VALUES('" + procatid + "','" + TBCategoryType.Text.Trim() + "','" + Session["user"].ToString() +
                                " ','" + DateTime.Now.ToString("yyyy/MM/dd") + "','true','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.ExecuteNonQuery();

                }

                con.Close();

            }
            catch (Exception ex)
            {
                lblalert.Text = ex.Message;
            }
            return j;

        }

        private int update()
        {
            int k = 1;
            query = " update tbl_producttype set ProductTypeName = '" + TBCategoryType.Text + "', CreateBy='" + Session["user"].ToString() + "', CreatedAt='" + DateTime.Now + "' where  ProductTypeID='" + HFCategory.Value + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

            con.Open();

            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                cmd.ExecuteNonQuery();

            }
            con.Close();
            return k;
        }
        protected void BtnCreateCategory_Click(object sender, EventArgs e)
        {
            int o;
            //con.Close();
            //con.Open();
            //SqlCommand cm = new SqlCommand("select ProductTypeName from tbl_producttype where ProductTypeName='" + TBCategoryType.Text.Trim() + "'", con);
            //SqlDataReader dr = cm.ExecuteReader();
            //if (dr.Read())
            //{
            //    v_category.Text = "Already Exist";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "ModalPopUp();", true);
            //}
            //else 
            if (TBCategoryType.Text == "")
            {
                v_category.Text = "Please Fill Category Type!";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "ModalPopUp();", true);
            }
            else
            {
                if (HFCategory.Value == "")
                {
                    con.Close();
                    v_category.Text = "";
                    o = Save();

                    if (o == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                        lblalert.Text = "Product Category Has been Saved!..";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                        lblalert.Text = "Some thing is Wrong please Contact Administrator!..";
                    }

                }
                else
                {
                    con.Close();
                    v_category.Text = "";
                    o = update();

                    if (o == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                        lblalert.Text = "Product Category Has been Updated!..";
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                        lblalert.Text = "Some thing is Wrong please Contact Administrator!..";
                    }
                }
                clear();
                FillGrid();
            }
        }

        protected void BtnCancelCategory_Click(object sender, EventArgs e)
        {
            v_category.Text = "";
            clear();
        }

        protected void GVCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVCategory.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void GVCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                HiddenField HFCategoryID = (HiddenField)GVCategory.Rows[e.RowIndex].FindControl("HFCategoryID");


                int h;

                h = delete(HFCategoryID.Value);

                if (h == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "The Product Category As been Deleted..";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Some thing is Wrong please Contact Administrator!..";
                }
                

            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;

            }
        }

        private int delete(string HFCategoryID)
        {   

            string sqlquery = "Delete from tbl_producttype where ProductTypeID = '" + HFCategoryID  + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
            SqlCommand cmd = new SqlCommand(sqlquery, con);

            con.Open();
            cmd.ExecuteNonQuery();
            FillGrid();
            con.Close();
            clear();

             return 1;
        }
    }
}