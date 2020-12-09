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
    public partial class rpt_Pur : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string fdat, ldat, PRO, MON, YR;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {
                    fdat = Request.QueryString["FDAT"];
                    ldat = Request.QueryString["LDAT"];
                    PRO = Request.QueryString["PRO"];
                    MON = Request.QueryString["MON"];
                    YR = Request.QueryString["YR"];

                    if (fdat != null && ldat != null && PRO != null)
                    {
                        get_pur(fdat, ldat, PRO);

                    }
                    else if (YR != null && MON != null && PRO != null)
                    {
                        get_Monpur(YR, MON, PRO);

                    }

                    else if (fdat != null && ldat != null)
                    {
                        get_pur(fdat, ldat);
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
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
                string FileName = "PurchaseList.xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

                GV_Pur.GridLines = GridLines.Both;
                GV_Pur.HeaderStyle.Font.Bold = true;

                GV_Pur.RenderControl(htmltextwrtter);

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

        private void get_pur(string Fdat, string Ldat)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select * from v_DailyPur where  CreatedAt between '" + Fdat + "' and '" + Ldat + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                cmd.Connection = con;
                con.Open();

                DataTable dtsal = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtsal);

                if (dtsal.Rows.Count > 0)
                {
                    GV_Pur.DataSource = dtsal;
                    GV_Pur.DataBind();

                    //Get Total

                    //double GTotal = 0;
                    //double QGTotal = 0;
                    //// Total
                    //for (int j = 0; j < GV_Pur.Rows.Count; j++)
                    //{
                    //    Label total = (Label)GV_Pur.Rows[j].FindControl("lbl_GTtl");
                    //    GTotal += Convert.ToDouble(total.Text);
                    //}

                    //Quantity
                    //for (int j = 0; j < GV_Pur.Rows.Count; j++)
                    //{
                    //    Label totalqty = (Label)GV_Pur.Rows[j].FindControl("lbl_Qty");
                    //    QGTotal += Convert.ToDouble(totalqty.Text);
                    //}
                    //ttl_qty.Text = QGTotal.ToString();
                    //lbl_ttl.Text = GTotal.ToString();
                }
                con.Close();
            }
        }

        private void get_pur(string Fdat, string Ldat, string proid)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select * from v_DailyPur where productID='" + proid + "' and  CreatedAt between '" + Fdat + "' and '" + Ldat + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                cmd.Connection = con;
                con.Open();

                DataTable dtsal = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtsal);

                if (dtsal.Rows.Count > 0)
                {
                    GV_Pur.DataSource = dtsal;
                    GV_Pur.DataBind();

                    //Get Total

                    //double GTotal = 0;
                    //double QGTotal = 0;
                    //// Total
                    //for (int j = 0; j < GV_Pur.Rows.Count; j++)
                    //{
                    //    Label total = (Label)GV_Pur.Rows[j].FindControl("lbl_GTtl");
                    //    GTotal += Convert.ToDouble(total.Text);
                    //}

                    //Quantity
                    //for (int j = 0; j < GV_Pur.Rows.Count; j++)
                    //{
                    //    Label totalqty = (Label)GV_Pur.Rows[j].FindControl("lbl_Qty");
                    //    QGTotal += Convert.ToDouble(totalqty.Text);
                    //}
                    //ttl_qty.Text = QGTotal.ToString();
                    //lbl_ttl.Text = GTotal.ToString();
                }
                con.Close();
            }
        }
        private void get_Monpur(string yr, string mon, string proid)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select * from v_DailyPur where productID='" + proid + "' and  [Month]='" + mon + "' and [Years]='"+ yr +"' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                cmd.Connection = con;
                con.Open();

                DataTable dtsal = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtsal);

                if (dtsal.Rows.Count > 0)
                {
                    GV_Pur.DataSource = dtsal;
                    GV_Pur.DataBind();

                    //Get Total

                    //double GTotal = 0;
                    //double QGTotal = 0;
                    //// Total
                    //for (int j = 0; j < GV_Pur.Rows.Count; j++)
                    //{
                    //    Label total = (Label)GV_Pur.Rows[j].FindControl("lbl_GTtl");
                    //    GTotal += Convert.ToDouble(total.Text);
                    //}

                    //Quantity
                    //for (int j = 0; j < GV_Pur.Rows.Count; j++)
                    //{
                    //    Label totalqty = (Label)GV_Pur.Rows[j].FindControl("lbl_Qty");
                    //    QGTotal += Convert.ToDouble(totalqty.Text);
                    //}
                    //ttl_qty.Text = QGTotal.ToString();
                    //lbl_ttl.Text = GTotal.ToString();
                }
                con.Close();
            }
        }
    }
}