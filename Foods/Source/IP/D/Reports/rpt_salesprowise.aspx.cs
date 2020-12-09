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
    public partial class rpt_salesprowise : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string proid, FDAT, LDAT, MON, YR;

        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];
            if (check != null)
            {
                proid = Request.QueryString["PROID"];
                FDAT = Request.QueryString["FDAT"];
                LDAT = Request.QueryString["LDAT"];
                MON = Request.QueryString["MON"];
                YR = Request.QueryString["YR"];

                if (FDAT != null && LDAT != null && proid != null)
                {
                    get_ptrosal(FDAT, LDAT, proid);
                }
                else if (MON != null && YR != null && proid != null)
                {
                    get_ptromonyrsal(MON, YR, proid);
                }
                else if (MON != null && proid != null)
                {
                    get_ptromonsal(MON, proid);
                }
                else if (YR != null && proid != null)
                {
                    get_ptroyrsal(YR, proid);
                }
                else if (FDAT != null && proid != null)
                {
                    get_ptroFdatsal(FDAT, proid);
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
                string FileName = "SaleProductWiseList.xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

                GV_ProSale.GridLines = GridLines.Both;
                GV_ProSale.HeaderStyle.Font.Bold = true;

                GV_ProSale.RenderControl(htmltextwrtter);

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


        private void get_ptrosal(string fdat, string ldat, string Proid)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from v_rptSal where ProductID = " + Proid +
                        " and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] +
                        "' and Saldat  between '" + fdat + "' and '" + ldat + "' ";
                    //cmd.CommandText = "select * from v_rptDSal where  ProductID = '" + Proid +
                      ///  "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "' and Saldat between '" + fdat + "' and '" + ldat + "' ";
                    
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtprosal = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtprosal);
                    if (dtprosal.Rows.Count > 0)
                    {
                        lbl_pro.Text = dtprosal.Rows[0]["ProductName"].ToString();
                        GV_ProSale.DataSource = dtprosal;
                        GV_ProSale.DataBind();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void get_ptroFdatsal(string fdat, string Proid)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from v_rptSal where ProductID = " + Proid + " and CompanyId='" +
                        Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + 
                        "' and Saldat = '" + fdat + "'";
                    //cmd.CommandText = "select * from v_rptDSal where  ProductID = '" + Proid +
                      //  "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] +
                       // "' and Saldat = '" + fdat + "'";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtprosal = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtprosal);
                    if (dtprosal.Rows.Count > 0)
                    {
                        lbl_pro.Text = dtprosal.Rows[0]["ProductName"].ToString();
                        GV_ProSale.DataSource = dtprosal;
                        GV_ProSale.DataBind();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void get_ptromonyrsal(string mon, string yr, string Proid)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from v_rptSal where ProductID = " + Proid +
                        " and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] +
                        "' and [Month]='" + MON + "' and [YEAR]='" + YR + "'";
                    //cmd.CommandText = "select * from v_rptDSal where  ProductID = '" + Proid +
                      //  "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "' and [Month]='" + MON + "' and [YEAR]='" + YR + "'";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtprosal = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtprosal);
                    if (dtprosal.Rows.Count > 0)
                    {
                        lbl_pro.Text = dtprosal.Rows[0]["ProductName"].ToString();
                        GV_ProSale.DataSource = dtprosal;
                        GV_ProSale.DataBind();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void get_ptromonsal(string mon, string Proid)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from v_rptSal where ProductID = " + Proid + 
                        " and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'" +
                    //cmd.CommandText = "select * from v_rptDSal where  ProductID = '" + Proid +
                      //  "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + 
                    "' and [Month]='" + MON + "'";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtprosal = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtprosal);
                    if (dtprosal.Rows.Count > 0)
                    {
                        lbl_pro.Text = dtprosal.Rows[0]["ProductName"].ToString();
                        GV_ProSale.DataSource = dtprosal;
                        GV_ProSale.DataBind();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void get_ptroyrsal(string yr, string Proid)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from v_rptSal where ProductID = " + Proid +
                        " and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'" +
                    //cmd.CommandText = "select * from v_rptDSal where  ProductID = '" + Proid +
                      //  "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] +
                        "' and [YEAR]='" + YR + "'";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtprosal = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtprosal);
                    if (dtprosal.Rows.Count > 0)
                    {
                        lbl_pro.Text = dtprosal.Rows[0]["ProductName"].ToString();
                        GV_ProSale.DataSource = dtprosal;
                        GV_ProSale.DataBind();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}