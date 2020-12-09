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

using NHibernate;
using NHibernate.Criterion;
using Foods;
using DataAccess;

namespace Foods
{
    public partial class rpt_Diff : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string q;
        float GTotal;

        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];
            if (check != null)
            {
                string PURID = Request.QueryString["PURID"];
                string ID = Request.QueryString["ID"];
                string dat = Request.QueryString["dat"];
                lbl_dat.Text = DateTime.Now.ToShortDateString();

                if (PURID != null)
                {
                    FillGrid(PURID, dat);
                }
                else
                {
                    FillGrid(dat);
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
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
                string FileName = "StockList.xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

                GVDIff.GridLines = GridLines.Both;
                GVDIff.HeaderStyle.Font.Bold = true;

                GVDIff.RenderControl(htmltextwrtter);

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
        public void FillGrid(string dat)
        {
            try
            {
                q = " select * from  v_diffRat  where MPurDate = '" + dat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVDIff.DataSource = dt_;
                    GVDIff.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["MPurDate"].ToString();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void FillGrid(string mpurid, string dat)
        {
            try
            {
                q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVDIff.DataSource = dt_;
                    GVDIff.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["MPurDate"].ToString();
                    lbl_trans.Text = dt_.Rows[0]["Transport"].ToString();
                    lblotheramt.Text = dt_.Rows[0]["Otheramt"].ToString();
                    lbl_exchrat.Text = dt_.Rows[0]["Exchange_Rat"].ToString();

                    // Rate in PK

                    //GTotal = 0;

                    //for (int j = 0; j < GVDIff.Rows.Count; j++)
                    //{
                    //    Label total = (Label)GVDIff.Rows[j].FindControl("lb_ratinpk");
                    //    GTotal += Convert.ToSingle(total.Text);
                    //}


                    // Rate in Other Currencies

                    //GTotal = 0;

                    //for (int j = 0; j < GVDIff.Rows.Count; j++)
                    //{
                    //    Label total = (Label)GVDIff.Rows[j].FindControl("lb_purpric");
                    //    GTotal += Convert.ToSingle(total.Text);
                    //}

                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}