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
    public partial class frm_sticker : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DBConnection db = new DBConnection();
        DataTable dt_;
        string query;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Category categor = new Category();

            if (!this.IsPostBack)
            {
                FillGrid();
                BindDDL();
            }

        }
        public void clear()
        {
            TBCompany.Text = "";            
            lblcategory.Text = "";
            DDL_ProDesc.SelectedValue = "0";
            TBSiz.Text = "";
            TBcolor.Text = "";
            HFSticker.Value = "";
        }

        public void BindDDL()
        {
            //Items Name
            try
            {
                query = " select distinct(tbl_Dstk.ProductID),ProductName from tbl_Dstk inner join Products on tbl_Dstk.ProductID = Products.ProductID where tbl_Dstk.CompanyId='" + Session["CompanyID"] + "' and tbl_Dstk.BranchId='" + Session["BranchID"] + "'";
                    //" where tbl_Dstk.CompanyId='" + Session["CompanyID"] + "' and tbl_Dstk.BranchId='" + Session["BranchID"] + "'";
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(query);
                
                if (dt_.Rows.Count > 0)
                {
                    DDL_ProDesc.DataSource = dt_;
                    DDL_ProDesc.DataTextField = "ProductName";
                    DDL_ProDesc.DataValueField = "ProductID";
                    DDL_ProDesc.DataBind();
                    DDL_ProDesc.Items.Insert(0, new ListItem("--Select Product--", "0"));
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void FillGrid()
        {
            try
            {
                DataTable dt_ = new DataTable();
                //dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(ProductTypeID AS VARCHAR(200)) + ']-' + ProductTypeName ) as [ProductTypeName], ProductTypeID from tbl_producttype");
                dt_ = DBConnection.GetQueryData(" select stickerid,ProductName from tbl_sticker " +
                    " inner join Products on tbl_sticker.ProductID= Products.ProductID where tbl_sticker.CompanyId = '" +
                    Session["CompanyID"] + "' and tbl_sticker.BranchId= '" + Session["BranchID"] + "'");

                GVSticker.DataSource = dt_;
                GVSticker.DataBind();
                ViewState["Sticker"] = dt_;
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
                DataTable _dt = (DataTable)ViewState["Sticker"];
                DataView dv = new DataView(_dt, "ProductName LIKE '%" + TBSearchSticker.Text.Trim().ToUpper() + "%'", "[ProductName] ASC", DataViewRowState.CurrentRows);
                DataTable dt_ = new DataTable();
                dt_ = dv.ToTable();
                GVSticker.DataSource = dt_;
                GVSticker.DataBind();
                ViewState["Sticker"] = dt_;
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

        protected void GVSticker_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string stickid = "";
                if (e.CommandName == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "ModalPopUp();", true);
                    
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    stickid  = GVSticker.DataKeys[row.RowIndex].Values[0].ToString();

                    query = " select stickerid,StickerName, compnam, ProductID, siz, color from tbl_sticker where stickerid = '" + stickid + "' and CompanyId = '" 
                        + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                    dt_ = new DataTable();
                    dt_ = DBConnection.GetQueryData(query);
                    
                    if (dt_.Rows.Count > 0)
                    {
                        HFSticker.Value = dt_.Rows[0]["stickerid"].ToString();
                        TBCompany.Text = dt_.Rows[0]["compnam"].ToString();
                        DDL_ProDesc.SelectedValue = dt_.Rows[0]["ProductID"].ToString();
                        TBSiz.Text = dt_.Rows[0]["siz"].ToString();
                        TBcolor.Text = dt_.Rows[0]["color"].ToString();
                        TBCompany.Text = dt_.Rows[0]["compnam"].ToString();
                    }

                }
                else if (e.CommandName == "Print")
                {
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    stickid = GVSticker.DataKeys[row.RowIndex].Values[0].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_sticker.aspx?ID=Stick&StickId=" + stickid.Trim() + "','_blank','height=900px,width=1000px,scrollbars=1');", true);
                }

                else if (e.CommandName == "barcode")
                {
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    stickid = GVSticker.DataKeys[row.RowIndex].Values[0].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_BarCod.aspx?ID=Stick&StickId=" + stickid.Trim() + "','_blank','height=900px,width=1000px,scrollbars=1');", true);
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

        private int Save()
        {
            int j = 1;

            string MSticId = "";

            con.Open();

            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            // Start a local transaction.
            transaction = con.BeginTransaction("SalesTrans");

            // Must assign both transaction object and connection 
            // to Command object for a pending local transaction
            command.Connection = con;
            command.Transaction = transaction;

            try
            {


                command.CommandText = "sp_inssticker";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StickerName", TBCompany.Text);
                command.Parameters.AddWithValue("@CompNam", TBCompany.Text);
                command.Parameters.AddWithValue("@ProDesc", DDL_ProDesc.SelectedValue.Trim());
                command.Parameters.AddWithValue("@Price", TBPric.Text);
                command.Parameters.AddWithValue("@siz", TBSiz.Text);
                command.Parameters.AddWithValue("@color", TBcolor.Text);
                command.Parameters.AddWithValue("@createdby", Session["Username"]);
                command.Parameters.AddWithValue("@createat",  DateTime.Now.ToString());
                command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                command.Parameters.AddWithValue("@BranchId",Session["BranchID"]);
                command.ExecuteNonQuery();


                // Attempt to commit the transaction.
                transaction.Commit();

            }
            catch (Exception ex)
            {
                lblalert.Text = "Commit Exception Type: {0}" + ex.GetType();
                lblalert.Text = "Message: {0}" + ex.Message;

                // Attempt to roll back the transaction. 
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred 
                    // on the server that would cause the rollback to fail, such as 
                    // a closed connection.
                    lblalert.Text = "Rollback Exception Type: {0}" + ex2.GetType();
                    lblalert.Text = "Message: {0}" + ex2.Message;
                }
            }
            finally
            {
                con.Close();
                //Response.Redirect("frm_PSal.aspx");
                //clear();
            }

            return j;
        }


        private int update()
        {
            int j = 1;

            string MSticId = "";

            con.Open();

            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            // Start a local transaction.
            transaction = con.BeginTransaction("SalesTrans");

            // Must assign both transaction object and connection 
            // to Command object for a pending local transaction
            command.Connection = con;
            command.Transaction = transaction;

            try
            {

                command.CommandText = "sp_updsticker";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@stickerid", HFSticker.Value.Trim());
                command.Parameters.AddWithValue("@StickerName", TBCompany.Text);
                command.Parameters.AddWithValue("@CompNam", TBCompany.Text);
                command.Parameters.AddWithValue("@ProDesc", DDL_ProDesc.SelectedValue.Trim());
                command.Parameters.AddWithValue("@Price", TBPric.Text);
                command.Parameters.AddWithValue("@siz", TBSiz.Text);
                command.Parameters.AddWithValue("@color", TBcolor.Text);
                command.Parameters.AddWithValue("@updatby", Session["Username"]);
                command.Parameters.AddWithValue("@updatat", DateTime.Now.ToString());
                command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                command.Parameters.AddWithValue("@BranchId", Session["BranchID"]);
                command.ExecuteNonQuery();


                // Attempt to commit the transaction.
                transaction.Commit();

            }
            catch (Exception ex)
            {
                lblalert.Text = "Commit Exception Type: {0}" + ex.GetType();
                lblalert.Text = "Message: {0}" + ex.Message;

                // Attempt to roll back the transaction. 
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred 
                    // on the server that would cause the rollback to fail, such as 
                    // a closed connection.
                    lblalert.Text = "Rollback Exception Type: {0}" + ex2.GetType();
                    lblalert.Text = "Message: {0}" + ex2.Message;
                }
            }
            finally
            {
                con.Close();
                //Response.Redirect("frm_PSal.aspx");
                //clear();
            }

            return j;
        }
        protected void BtnCreatesticker_Click(object sender, EventArgs e)
        {
            int o;

            if (HFSticker.Value == "")
            {   
                o = Save();

                if (o == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Sticker Has been Saved!..";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Some thing is Wrong please Contact Administrator!..";
                }

            }
            else
            {
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
            //Category catgory = new Category();


            //catgory.CategoryId = HFCategory.Value;
            //catgory.Categorytype = string.IsNullOrEmpty(TBCategoryType.Text) ? null : TBCategoryType.Text;
            //catgory.Subtype = string.IsNullOrEmpty(TBSubType.Text) ? null : TBSubType.Text;
            //CategoryManager catmanager = new CategoryManager(catgory);
            //catmanager.Save();
            clear();
            FillGrid();
            
        }

        protected void BtnCancelsticker_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void GVSticker_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVSticker.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void GVSticker_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try 
            {
                HiddenField HFStickerID = (HiddenField)GVSticker.Rows[e.RowIndex].FindControl("HFStickerID");

                int h;

                h = delete(HFStickerID.Value);

                if (h == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "The Sticker As been Deleted..";
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

        private int delete(string HFStickerID)
        {
                SqlCommand cmd = new SqlCommand("sp_delsticker", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@stickerid", HFStickerID.Trim());
                cmd.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                cmd.Parameters.AddWithValue("@BranchId", Session["BranchID"]);

                con.Open();
                cmd.ExecuteNonQuery();
                FillGrid();
                con.Close();
                clear();
                return 1;
            
        }
    }
}