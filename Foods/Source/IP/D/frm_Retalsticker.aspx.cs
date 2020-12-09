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
    public partial class frm_Retalsticker : System.Web.UI.Page
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
            }

        }
        public void clear()
        {
            TBCompany.Text = "";            
            lblcategory.Text = "";
            TBProDesc.Text = "";
            TBSiz.Text = "";
            TBcolor.Text = "";
            HFSticker.Value = "";
        }

        public void FillGrid()
        {
            try
            {
                DataTable dt_ = new DataTable();
                //dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(ProductTypeID AS VARCHAR(200)) + ']-' + ProductTypeName ) as [ProductTypeName], ProductTypeID from tbl_producttype");
                dt_ = DBConnection.GetQueryData(" select stickerid,StickerName, ProDesc from tbl_Rsticker where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

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
                DataView dv = new DataView(_dt, "ProDesc LIKE '%" + TBSearchSticker.Text.Trim().ToUpper() + "%'", "[ProDesc] ASC", DataViewRowState.CurrentRows);
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

                    query = " select stickerid,StickerName, compnam,Barcod, prodesc, siz, color, Price from tbl_Rsticker where stickerid = '" + stickid + "' and CompanyId = '" 
                        + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                    dt_ = new DataTable();
                    dt_ = DBConnection.GetQueryData(query);
                    
                    if (dt_.Rows.Count > 0)
                    {
                        HFSticker.Value = dt_.Rows[0]["stickerid"].ToString();
                        TBCompany.Text = dt_.Rows[0]["compnam"].ToString();
                        TBBarcod.Text = dt_.Rows[0]["Barcod"].ToString();
                        TBProDesc.Text = dt_.Rows[0]["prodesc"].ToString();
                        TBSiz.Text = dt_.Rows[0]["siz"].ToString();
                        TBcolor.Text = dt_.Rows[0]["color"].ToString();
                        TBCompany.Text = dt_.Rows[0]["compnam"].ToString();
                        TBPric.Text = dt_.Rows[0]["Price"].ToString();
                    }

                }
                else if (e.CommandName == "Print")
                {
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    stickid = GVSticker.DataKeys[row.RowIndex].Values[0].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Rsticker.aspx?ID=Stick&StickId=" + stickid.Trim() + "','_blank','height=900px,width=1000px,scrollbars=1');", true);
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


                command.CommandText = "sp_Rinssticker";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@StickerName", TBCompany.Text);
                command.Parameters.AddWithValue("@CompNam", TBCompany.Text);
                command.Parameters.AddWithValue("@Barcod", TBBarcod.Text);
                command.Parameters.AddWithValue("@ProDesc", TBProDesc.Text);
                command.Parameters.AddWithValue("@siz", TBSiz.Text);
                command.Parameters.AddWithValue("@color", TBcolor.Text);
                command.Parameters.AddWithValue("@Price", TBPric.Text);                
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

                command.CommandText = "sp_updRsticker";
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@stickerid", HFSticker.Value.Trim());
                command.Parameters.AddWithValue("@StickerName", TBCompany.Text);
                command.Parameters.AddWithValue("@CompNam", TBCompany.Text);
                command.Parameters.AddWithValue("@Barcod", TBBarcod.Text);
                command.Parameters.AddWithValue("@ProDesc", TBProDesc.Text);
                command.Parameters.AddWithValue("@siz", TBSiz.Text);
                command.Parameters.AddWithValue("@color", TBcolor.Text);
                command.Parameters.AddWithValue("@Price", TBPric.Text);                
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
                SqlCommand cmd = new SqlCommand("sp_delRsticker", con);
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