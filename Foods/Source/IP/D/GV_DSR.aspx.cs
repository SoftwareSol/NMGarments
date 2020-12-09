using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using Foods;
using DataAccess;

namespace Foods
{
    public partial class GV_DSR : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        string DSRID, EID, DSRFDat, DSRTDat, query, Amt, dis;
        float GTotal = 0;
        float gTotalAmt = 0;
        DataTable dt_;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    lblcom.Text = "NM Garments";

                    DSRID = Request.QueryString["DSRID"];
                    EID = Request.QueryString["EID"];
                    DSRFDat = Request.QueryString["DSRFDat"];
                    DSRTDat = Request.QueryString["DSRTDat"];
                    getbranchData();
                    lbl_dat.Text = DSRFDat + " to " + DSRTDat;

                    if (con.State != ConnectionState.Open)
                    {
                        con.Close();                        
                    }
                    con.Open();

                    //SqlDataAdapter da = new SqlDataAdapter(" select distinct(Adv) as Adv ,ttl as Amt,tbl_DCPos.bal as bal,RecieverNam,tbl_MCPos.BillNo   from tbl_MCPos inner join tbl_DCPos on tbl_MCPos.MCposid = tbl_DCPos.MCposid " +
                      //  " inner join Products on tbl_DCPos.ProductID = Products.ProductID where billdat ='" + DSRDat + "' and Products.CompanyId='" + Session["CompanyID"] + "' and Products.BranchId='" + Session["BranchID"] + "' and Iscancel <> 1 ", con);
                    SqlDataAdapter da = new SqlDataAdapter(" select distinct(Adv) as Adv ,ttl as Amt,tbl_DCPos.bal as bal,RecieverNam,tbl_MCPos.BillNo   from tbl_MCPos inner join tbl_DCPos on tbl_MCPos.MCposid = tbl_DCPos.MCposid " +
                        " inner join Products on tbl_DCPos.ProductID = Products.ProductID where billdat between'" + DSRFDat + "' and '" + DSRTDat + "' and Products.CompanyId='" + Session["CompanyID"] + "' and Products.BranchId='" + Session["BranchID"] + "' and Iscancel <> 1", con);


                    DataSet ds = new DataSet();
                    da.Fill(ds, "tbl_MCPos");
                    if (ds.Tables["tbl_MCPos"].Rows.Count > 0)
                    {
                        Amt = ds.Tables["tbl_MCPos"].Rows[0]["Amt"].ToString();
                        //dis = ds.Tables["tbl_MCPos"].Rows[0]["dis"].ToString();

                        //if (dis != "0" || dis != "" || dis != "NULL")
                        //{ 

                        //}
                        GVDSR.DataSource = ds.Tables[0];
                        GVDSR.DataBind();
                    }
                    con.Close();
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private void getbranchData()
        {
            try
            {
                query = "select * from tbl_branches where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    Imglogo.ImageUrl = "~/img/branchlogo/" + dt_.Rows[0]["logo"].ToString();
                    lbl_brnam.Text = dt_.Rows[0]["Name"].ToString();
                    lbl_Add.Text = dt_.Rows[0]["Address"].ToString();
                    lbl_Ph.Text = dt_.Rows[0]["TelephoneNo"].ToString();
                    lbl_Mob.Text = dt_.Rows[0]["MobileNo"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void GVDSR_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_amt = (Label)e.Row.FindControl("lbl_amts");

                GTotal += Convert.ToSingle(lbl_amt.Text);

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_ttlamts = (Label)e.Row.FindControl("lbl_ttlamts");

                lbl_ttlamts.Text = GTotal.ToString();
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_amt = (Label)e.Row.FindControl("lbl_amt");

                gTotalAmt += Convert.ToSingle(lbl_amt.Text);

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblttlamt = (Label)e.Row.FindControl("lblttlamt");

                lblttlamt.Text = gTotalAmt.ToString();
            }

            //lblttlamt

        }

        protected void LinkBtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "DailySalesReport_" + DSRFDat + '_' + DSRTDat + ".xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

                GVDSR.GridLines = GridLines.Both;
                GVDSR.HeaderStyle.Font.Bold = true;

                GVDSR.RenderControl(htmltextwrtter);

                Response.Write(strwritter.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                throw;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //lblalert.Text = ex.Message;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

    }
}