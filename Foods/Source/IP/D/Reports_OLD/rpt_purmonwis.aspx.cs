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
    public partial class rpt_purmonwis : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string monid, yr;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {
                    monid = Request.QueryString["MONID"];
                    yr = Request.QueryString["YRID"];
                    get_monpur(monid, yr);
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void get_monpur(string Monid, string year)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from v_DailyPur where [Month] = '" + Monid + "' and YEAR(CreatedAt)= '" + year + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtmonsal = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtmonsal);
                    if (dtmonsal.Rows.Count > 0)
                    {
                        lbl_mon.Text = dtmonsal.Rows[0]["Months"].ToString();
                        lbl_yr.Text = yr;

                        GV_Month.DataSource = dtmonsal;
                        GV_Month.DataBind();

                        //Get Total

                        double GTotal = 0;
                        double QGTotal = 0;
                        // Total
                        for (int j = 0; j < GV_Month.Rows.Count; j++)
                        {
                            Label total = (Label)GV_Month.Rows[j].FindControl("lbl_GTtl");
                            GTotal += Convert.ToDouble(total.Text);
                        }

                        //Quantity
                        for (int j = 0; j < GV_Month.Rows.Count; j++)
                        {
                            Label totalqty = (Label)GV_Month.Rows[j].FindControl("lbl_Qty");
                            QGTotal += Convert.ToDouble(totalqty.Text);
                        }
                        ttl_qty.Text = QGTotal.ToString();
                        lbl_ttl.Text = GTotal.ToString();

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
                string FileName = "PurchaseMonthwise.xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

                GV_Month.GridLines = GridLines.Both;
                GV_Month.HeaderStyle.Font.Bold = true;

                GV_Month.RenderControl(htmltextwrtter);

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