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
    public partial class rpt_venwise : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string venid, FDAT, LDAT, MON, YER;

        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];
            if (check != null)
            {
                venid = Request.QueryString["VENID"];
                FDAT = Request.QueryString["FDAT"];
                LDAT = Request.QueryString["LDAT"];
                MON = Request.QueryString["MON"];
                YER = Request.QueryString["YER"];

                if (venid != null && FDAT != null && LDAT != null)
                {
                    get_vensal(venid, FDAT, LDAT);
                }
                else if (venid != null && MON != null && YER != null)
                {
                    get_venssal(venid, MON, YER);
                }
                else
                {
                    get_vensal(venid);
                }

            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void get_vensal(string Venid)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from v_DailyPur where PayAcc = " + Venid + " and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtvenpur = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtvenpur);
                    if (dtvenpur.Rows.Count > 0)
                    {
                        lbl_ven.Text = dtvenpur.Rows[0]["suppliername"].ToString();
                        lbl_add.Text = dtvenpur.Rows[0]["addressOne"].ToString();
                        lbl_phno.Text = dtvenpur.Rows[0]["phoneno"].ToString();

                        GV_VenPur.DataSource = dtvenpur;
                        GV_VenPur.DataBind();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
                string FileName = "VendorPurList.xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

                GV_VenPur.GridLines = GridLines.Both;
                GV_VenPur.HeaderStyle.Font.Bold = true;

                GV_VenPur.RenderControl(htmltextwrtter);

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

        private void get_vensal(string Venid, string FDAT, string ldat)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from v_DailyPur where PayAcc = " + Venid + " and CreatedAt between '" + FDAT + "' and '" + ldat + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtvenpur = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtvenpur);
                    if (dtvenpur.Rows.Count > 0)
                    {
                        lbl_ven.Text = dtvenpur.Rows[0]["suppliername"].ToString();
                        lbl_add.Text = dtvenpur.Rows[0]["addressOne"].ToString();
                        lbl_phno.Text = dtvenpur.Rows[0]["phoneno"].ToString();

                        GV_VenPur.DataSource = dtvenpur;
                        GV_VenPur.DataBind();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void get_venssal(string Venid, string mon, string yr)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from v_DailyPur where PayAcc = '" + Venid + "' and [Month] = '" + mon + "'  and year(CreatedAt) = '" + yr + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtvenpur = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtvenpur);
                    if (dtvenpur.Rows.Count > 0)
                    {
                        lbl_ven.Text = dtvenpur.Rows[0]["suppliername"].ToString();
                        lbl_add.Text = dtvenpur.Rows[0]["addressOne"].ToString();
                        lbl_phno.Text = dtvenpur.Rows[0]["phoneno"].ToString();

                        GV_VenPur.DataSource = dtvenpur;
                        GV_VenPur.DataBind();
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