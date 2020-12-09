using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using Foods;
using DataAccess;


namespace Foods
{
    public partial class frm_VDsr : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        string DSR, DAT, Cust, Usr, DSRID;
        DBConnection db = new DBConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {   
                FillGrid();
            }
        }
        public void FillGrid()
        {
            try
            {
                string query = "";
                dt_ = new DataTable();

                query = " select distinct(convert(date, cast(dsrdat as date) ,103)) as [dsrdat],username,Isdon " +
                       " from tbl_Mdsr where tbl_Mdsr.CompanyId = '" + Session["CompanyID"] + "' and tbl_Mdsr.BranchId ='" + Session["BranchID"] + "' order by " +
                       " convert(date, cast(dsrdat as date) ,105) desc";
                    SqlCommand cmd = new SqlCommand(query, con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt_);

                    if (dt_.Rows.Count > 0)
                    {
                        GVDSR.DataSource = dt_;
                        GVDSR.DataBind();
                        ViewState["vdsr"] = dt_;
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         protected void SeacrhBtn_Click(object sender, EventArgs e)
        {
            SearchRecord();
        }

         private void SearchRecord()
         {
             try
             {
                 FillGrid();
                 DataTable _dt = (DataTable)ViewState["vdsr"];
                 DataView dv = new DataView(_dt, "Username LIKE '%" + TBSDSR.Text.Trim().ToUpper() + "%'", "[Username] ASC", DataViewRowState.CurrentRows);
                 DataTable dt_ = new DataTable();
                 dt_ = dv.ToTable();
                 GVDSR.DataSource = dt_;
                 GVDSR.DataBind();
                 ViewState["vdsr"] = dt_;
             }
             catch (Exception ex)
             {
                    throw;
                 //ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                 //lblalert.Text = ex.Message;
             }
         }

         protected void GVDSR_PageIndexChanging(object sender, GridViewPageEventArgs e)
         {

         }

         protected void GVDSR_RowCommand(object sender, GridViewCommandEventArgs e)
         {
             try
             {
                 GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                 string dsrid = GVDSR.DataKeys[row.RowIndex].Values[0].ToString();

                 string usrid = GVDSR.DataKeys[row.RowIndex].Values[1].ToString();

                 if (e.CommandName == "showdsr")
                 {
                     //frm_loadsheet_ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( '.aspx?DSRID=" +  + ",'_blank','height=600px,width=600px,scrollbars=1');", true);
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'GV_DSR.aspx?DSRID=" + dsrid + "&EID=" + usrid + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                 }
                 else if (e.CommandName == "loadsht")
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'frm_loadsheet_.aspx?LODSHT=" + dsrid + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                 }
                 else if (e.CommandName == "IsDon")
                 {
                     int upd = 0;

                     upd = Isdone(dsrid);

                     if (upd == 1)
                     {
                         lb_error.Text = "Your DSR has been authorized!!";
                         Response.Redirect("frm_VDsr.aspx");

                     }else
                     {
                         lb_error.Text = "Oh Ho !! Something is wrong please Contact your adminitrator!!";
                     }
                 }
             }
             catch (Exception ex)
             {
                  throw ex;                 
             }
         }

         private int Isdone( string vdsr)
         {
             int i = 1;

             try
             {
                 string query = "update tbl_Mdsr set Isdon = '1' where dsrdat='" + vdsr + "'";

                 con.Open();

                 using (SqlCommand cmd = new SqlCommand(query, con))
                 {

                     cmd.ExecuteNonQuery();

                 }
                 con.Close();

             }
             catch (Exception ex)
             {
                 throw;
             }
             return i;
         }

         private int _counter;

         protected void GVDSR_RowDataBound(object sender, GridViewRowEventArgs e)
         {
             if (e.Row.RowType == DataControlRowType.DataRow)
             {       
                //dt_ = (DataTable)ViewState["vdsr"];
                //if (dt_.Rows.Count > 0)
                //{
                //    for (int l = 0; l < dt_.Rows.Count; l++)
                //    {
                //        string lock_ = dt_.Rows[l]["Isdon"].ToString();
                //        if (lock_ == "1")
                //        {
                            //for (int i = 0; i < GVDSR.Rows.Count; i++)
                            //{
                            //LinkButton lnkDon = (LinkButton)GVDSR.Rows[i].Cells[0].FindControl("lnkDon"); hflock
                            LinkButton lnkDon = e.Row.FindControl("lnkDon") as LinkButton;
                            HiddenField hflock = e.Row.FindControl("hflock") as HiddenField;
                            if (hflock.Value == "1")
                            {
                                lnkDon.Visible = false;
                            }
                            //}
                            //_counter++;
                //        }
                //    }                        
                //}   
             }
         }
    }
}