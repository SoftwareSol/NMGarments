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
    public partial class rpt_vencred : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string fdat, ldat;

        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];
            if (check != null)
            {
                fdat = Request.QueryString["FDAT"];
                ldat = Request.QueryString["LDAT"];
                get_vencre();
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
                string FileName = "VendorCreditList.xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                
                GV_VenCre.GridLines = GridLines.Both;
                GV_VenCre.HeaderStyle.Font.Bold = true;

                GV_VenCre.RenderControl(htmltextwrtter);

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
        private void get_vencre()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select * from v_crestvenwis";
                cmd.Connection = con;
                con.Open();

                DataTable dtvencre = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtvencre);

                if (dtvencre.Rows.Count > 0)
                {
                    GV_VenCre.DataSource = dtvencre;
                    GV_VenCre.DataBind();
                    //Get Total

                    double GTotal = 0;
                    double QGTotal = 0;

                    // Out Standing
                    for (int j = 0; j < GV_VenCre.Rows.Count; j++)
                    {
                        Label total = (Label)GV_VenCre.Rows[j].FindControl("lbl_outstand");
                        GTotal += Convert.ToDouble(total.Text);
                    }

                    //Cash Amount
                    for (int j = 0; j < GV_VenCre.Rows.Count; j++)
                    {
                        Label totalqty = (Label)GV_VenCre.Rows[j].FindControl("lbl_cshamt");
                        QGTotal += Convert.ToDouble(totalqty.Text);
                    }
                    ttl_qty.Text = QGTotal.ToString();
                    lbl_ttl.Text = GTotal.ToString();
                }
                con.Close();
            }
        }

    }
}