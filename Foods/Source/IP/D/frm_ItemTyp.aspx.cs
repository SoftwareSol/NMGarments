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
    public partial class frm_ItemTyp : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DBConnection db = new DBConnection();
        string query;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {
                    try
                    {
                        lbl_usr.Text = Session["Name"].ToString();
                        FillGrid();
                    }

                    catch { Response.Redirect("~/Login.aspx"); }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void SearchRecord()
        {
            try
            {
                FillGrid();
                DataTable _dt = (DataTable)ViewState["ItemTyp"];
                DataView dv = new DataView(_dt, "ProductTypeName LIKE '%" + TBSearchCAtegory.Text.Trim().ToUpper() + "%'", "[ProductTypeName] ASC", DataViewRowState.CurrentRows);
                DataTable dt_ = new DataTable();
                dt_ = dv.ToTable();
                GVCategory.DataSource = dt_;
                GVCategory.DataBind();
                ViewState["ItemTyp"] = dt_;
            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
            }
        }

        protected void lnkbtn_Logout_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("~/Login.aspx");
        }

        public void FillGrid()
        {
            try
            {
                DataTable dt_ = new DataTable();

                dt_ = DBConnection.GetQueryData("select ProductTypeID, ProductTypeName from tbl_producttype where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                GVCategory.DataSource = dt_;
                GVCategory.DataBind();
                ViewState["ItemTyp"] = dt_;
            }
            catch (Exception ex)
            {
                lbl_mssg.Text = ex.Message;
            }
        }

        private int Save()
        {
            int j = 1;
            //query = " select top 1 isnull(max(cast(ProductTypeID as int)),0) as [ProductTypeID]  from tbl_producttype where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' order by ProductTypeID desc ";
            query = " select top 1 ProductTypeID as [ProductTypeID]  from tbl_producttype where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' order by ProductTypeID desc ";

            DataTable dt_ = new DataTable();            
            dt_ = DBConnection.GetQueryData(query);

            string ProductTypeID = dt_.Rows[0]["ProductTypeID"].ToString();

            //int procatid = Convert.ToInt32(ProductTypeID) + 1;
            string procat = ProductTypeID + 1;
            
            query = " INSERT INTO [dbo].[tbl_producttype] " +
                            " ([ProductTypeID], [ProductTypeName],[CreateBy],[CreatedAt],[IsActive],[CompanyId],[BranchId]) VALUES('" + procat + "','" + tb_itmtyp.Text.Trim() + "','" + Session["user"].ToString() +
                            " ','" + DateTime.Now + "','true','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
            con.Open();

            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                cmd.ExecuteNonQuery();

            }
            con.Close();

            return j;
        }

        private int update()
        {
            int k = 1;  
            query = " update tbl_producttype set ProductTypeName = '" + tb_itmtyp.Text + "', CreateBy='" + Session["user"].ToString() + "', CreatedAt='" + DateTime.Now + "' where  ProductTypeID='" + HFItmTypID.Value + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

            con.Open();

            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                cmd.ExecuteNonQuery();

            }
            con.Close();
            return k;
        }
        public void clear()
        {
            tb_itmtyp.Text = "";
            HFItmTypID.Value = "";
        }

        protected void BSave_Click(object sender, EventArgs e)
        {
            int o;

            if (HFItmTypID.Value == "")
            {
                o = Save();

                if (o == 1)
                {
                    lblerr.Text = "Product Category Has been Saved!..";
                }
                else
                {
                    lblerr.Text = "Some thing is Wrong please Contact Administrator!..";
                }

            }
            else
            {
                o = update();

                if (o == 1)
                {
                    lblerr.Text = "Product Category Has been Updated!..";
                }
                else
                {
                    lblerr.Text = "Some thing is Wrong please Contact Administrator!..";
                }
            }

            clear();
            FillGrid();
        }

        protected void BReset_Click(object sender, EventArgs e)
        {
            clear();
            ModalPopupExtender1.Show();
        }
        protected void BtnCancelCategory_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {

        }

        protected void GVCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVCategory.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void GVCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    ModalPopupExtender1.Show();
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    HFItmTypID.Value = GVCategory.DataKeys[row.RowIndex].Values[0].ToString();
                    tb_itmtyp.Text = Server.HtmlDecode(row.Cells[1].Text);
                }
            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
            }
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
                    lblerr.Text = "The Product Category As been Deleted..";
                }
                else
                {
                    lblerr.Text = "Some thing is Wrong please Contact Administrator!..";
                }


            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
            }
        }

        private int delete(string HFCategoryID)
        {

            string sqlquery = "Delete from tbl_producttype where ProductTypeID = '" + HFCategoryID + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
            SqlCommand cmd = new SqlCommand(sqlquery, con);

            con.Open();
            cmd.ExecuteNonQuery();
            FillGrid();
            con.Close();
            clear();

            return 1;
        }



        protected void SeacrhBtn_Click(object sender, EventArgs e)
        {
            SearchRecord();
        }
        
    }
}