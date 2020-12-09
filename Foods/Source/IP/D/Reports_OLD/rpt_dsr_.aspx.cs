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
    public partial class rpt_dsr_ : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        string DSRID, DAT, Cust, Usr;
        DBConnection db = new DBConnection();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {
                    DSRID = Request.QueryString["DSRID"];
                    getdsr(DSRID);
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
                string FileName = "DSRList.xls";
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


        public void getdsr(string dsrid)
        {
            try
            {
                string query = " select tbl_Mdsr.dsrid,ROW_NUMBER() OVER(ORDER BY tbl_Mdsr.dsrid DESC) AS [ID],tbl_Mdsr.CustomerID,CustomerName,ProductName,salrat,(salrat * Qty) as [Sale Rate],Qty as [Qty],tbl_ddsr.outstan, '' as [Return], " +
                              " dsrrmk,tbl_Mdsr.CreateBy,salrturn,username,recvry, replace (convert(NVARCHAR, dsrdat, 101), '/', '/') as [dsrdat],tbl_Mdsr.CompanyId,tbl_Mdsr.BranchId " +
                              " from tbl_Mdsr inner join tbl_ddsr on tbl_Mdsr.dsrid = tbl_ddsr.dsrid " +
                              " inner join Customers_ on tbl_Mdsr.CustomerID = Customers_.CustomerID " +
                              " inner join Products on tbl_ddsr.ProductID = Products.ProductID where tbl_Mdsr.dsrid= '" + dsrid + "' and tbl_Mdsr.CompanyId = '" + Session["CompanyID"] + "' and tbl_Mdsr.BranchId= '" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                GVDSR.DataSource = dt_;
                GVDSR.DataBind();
            
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}