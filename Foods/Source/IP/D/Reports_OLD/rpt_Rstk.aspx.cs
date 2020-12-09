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
    public partial class rpt_Rstk : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string q, RSTK, REJSTK, REJ, D;
        DateTime dat;
        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];
            if (check != null)
            {
                dat =Convert.ToDateTime(Request.QueryString["dat"]);
                RSTK = Request.QueryString["RSTK"];
                REJSTK = Request.QueryString["REJSTK"];
                REJ = Request.QueryString["REJ"];


                lbl_rptnam.Text = "Defected Stock Report";

                lbl_dat.Text = DateTime.Now.ToShortDateString();

                if (REJ != null && REJSTK != null)
                {
                    FillGrid(REJ, REJSTK);
                    //calttlsiz(REJ, REJSTK, dat);
                    float GTotal = 0;
                    for (int j = 0; j < GVStk.Rows.Count; j++)
                    {
                        Label total = (Label)GVStk.Rows[j].FindControl("lbl_stkval");

                        GTotal += Convert.ToSingle(total.Text);
                    }
                    lbl_ttlStkVal.Text = GTotal.ToString();

                }
                else if (REJ != null)
                {
                    FillGrid(REJ, dat);
                    //calttlsiz(REJSTK, dat);
                    float GTotal = 0;
                    for (int j = 0; j < GVStk.Rows.Count; j++)
                    {
                        Label total = (Label)GVStk.Rows[j].FindControl("lbl_stkval");

                        GTotal += Convert.ToSingle(total.Text);
                    }
                    lbl_ttlStkVal.Text = GTotal.ToString();
                }
                else if (REJSTK != null)
                {
                    FillGrid(REJ); 
                    //calttlsiz(REJ, dat);
                    float GTotal = 0;
                    for (int j = 0; j < GVStk.Rows.Count; j++)
                    {
                        Label total = (Label)GVStk.Rows[j].FindControl("lbl_stkval");

                        GTotal += Convert.ToSingle(total.Text);
                    }
                    lbl_ttlStkVal.Text = GTotal.ToString();
                }                
                else
                {
                    FillGrid();
                    //calttlsiz();
                    float GTotal = 0;
                    for (int j = 0; j < GVStk.Rows.Count; j++)
                    {
                        Label total = (Label)GVStk.Rows[j].FindControl("lbl_stkval");

                        GTotal += Convert.ToSingle(total.Text);
                    }
                    lbl_ttlStkVal.Text = GTotal.ToString();
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
                string FileName = "Rejected_StockList.xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                
                GVStk.GridLines = GridLines.Both;
                GVStk.HeaderStyle.Font.Bold = true;

                GVStk.RenderControl(htmltextwrtter);

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

   

        public void FillGrid(string rej, string stkrej)
        {
            try
            {
                q = " select * from  v_stk  where Mstk_dat <= '" + dat + "' and ProductID= '" + rej + "' and Dstk_unt ='" + stkrej + "' and returntyp='Defected'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);


                GVStk.DataSource = dt_;
                GVStk.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillGrid(string rej, DateTime date)
        {
            try
            {
                q = " select * from  v_stk  where Mstk_dat <= '" + date + "' and ProductID='" + rej + "' and returntyp='Defected' and dstkdef <> 0";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);


                GVStk.DataSource = dt_;
                GVStk.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillGrid(string REJSTK)
        {
            try
            {
                q = " select * from  v_stk  where Mstk_dat <= '" + dat + "' and Dstk_unt='" + REJSTK + "' and returntyp='Defected'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);


                GVStk.DataSource = dt_;
                GVStk.DataBind();

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
                q = " select * from  v_stk  where Mstk_dat <= '" + dat + "' and returntyp='Defected'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);


                GVStk.DataSource = dt_;
                GVStk.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }    
}