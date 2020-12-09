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
    public partial class rpt_Emp : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        string BOOkID, FDatEmp, EDatEmp, SALMNID;
        DBConnection db = new DBConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {
                    BOOkID = Request.QueryString["BOOkID"];
                    SALMNID = Request.QueryString["SALMNID"];
                    FDatEmp = Request.QueryString["FDatEmp"];
                    EDatEmp = Request.QueryString["EDatEmp"];

                    if (BOOkID != null && FDatEmp != null && EDatEmp != null)
                    {
                        getempsale(BOOkID, FDatEmp, EDatEmp);
                    }
                    else if (SALMNID != null && FDatEmp != null && EDatEmp != null)
                    {
                        getempsales(SALMNID, FDatEmp, EDatEmp);
                    }
                    else if (BOOkID != null)
                    {
                        getempsale(BOOkID);
                    }
                    else if (SALMNID != null)
                    {
                        getempsales(SALMNID);
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
                string FileName = "EmployeeList.xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

                GVEMP.GridLines = GridLines.Both;
                GVEMP.HeaderStyle.Font.Bold = true;

                GVEMP.RenderControl(htmltextwrtter);

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



        public void getempsale(string bookid)
        {
            try
            {
                string query = " select tbl_MSal.MSal_id,ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID], CreatedBy, DSal_ItmQty ,GTtl as [Total], replace (convert(NVARCHAR, CreatedAt, 101), '/', '/') as [CreatedAt],tbl_MSal.CompanyId, tbl_MSal.BranchId, Booker,SalMan from tbl_MSal inner join tbl_DSal on tbl_MSal.MSal_id = tbl_DSal.MSal_id where Booker = '" + bookid + "' and tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' and tbl_MSal.BranchId= '" + Session["BranchID"] + "' group by CreatedBy,DSal_ItmQty,DSal_ttl,CreatedAt,GTtl,tbl_MSal.CompanyId, tbl_MSal.BranchId,tbl_MSal.MSal_id,Booker,SalMan  order by MSal_id desc ";


                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    GVEMP.DataSource = dt_;
                    GVEMP.DataBind();
                }
                else
                {
                    //
                }

                string total = " select  tbl_MSal.MSal_id,ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID], CreatedBy, DSal_ItmQty ,sum(GTtl) as [Total Sales],  replace (convert(NVARCHAR, CreatedAt, 101), '/', '/') as [CreatedAt],tbl_MSal.CompanyId, tbl_MSal.BranchId, Booker,SalMan from tbl_MSal inner join tbl_DSal on tbl_MSal.MSal_id = tbl_DSal.MSal_id where Booker = '" + bookid + "' and tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' and tbl_MSal.BranchId= '" + Session["BranchID"] + "' group by CreatedBy,DSal_ItmQty,DSal_ttl,CreatedAt,GTtl,tbl_MSal.CompanyId, tbl_MSal.BranchId,tbl_MSal.MSal_id,Booker,SalMan ";
                    

                dt_ = DBConnection.GetQueryData(total);

                if (dt_.Rows.Count > 0)
                {
                    lbl_ttl.Text = dt_.Rows[0]["Total Sales"].ToString();
                }
                else
                {
                    // Code Error
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void getempsales(string salmanid)
        {
            try
            {
                string query = " select tbl_MSal.MSal_id,ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID], CreatedBy, DSal_ItmQty ,GTtl as [Total], replace (convert(NVARCHAR, CreatedAt, 101), '/', '/') as [CreatedAt],tbl_MSal.CompanyId, tbl_MSal.BranchId, Booker,SalMan from tbl_MSal inner join tbl_DSal on tbl_MSal.MSal_id = tbl_DSal.MSal_id where SalMan = '" + salmanid + "' and tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' and tbl_MSal.BranchId= '" + Session["BranchID"] + "' group by CreatedBy,DSal_ItmQty,DSal_ttl,CreatedAt,GTtl,tbl_MSal.CompanyId, tbl_MSal.BranchId,tbl_MSal.MSal_id,Booker,SalMan  order by MSal_id desc ";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    GVEMP.DataSource = dt_;
                    GVEMP.DataBind();
                }
                else
                {
                    //
                }

                string total = " select  tbl_MSal.MSal_id,ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID], CreatedBy, DSal_ItmQty ,sum(GTtl) as [Total Sales],  replace (convert(NVARCHAR, CreatedAt, 101), '/', '/') as [CreatedAt],tbl_MSal.CompanyId, tbl_MSal.BranchId, Booker,SalMan from tbl_MSal inner join tbl_DSal on tbl_MSal.MSal_id = tbl_DSal.MSal_id where SalMan = '" + salmanid + "' and tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' and tbl_MSal.BranchId= '" + Session["BranchID"] + "' group by CreatedBy,DSal_ItmQty,DSal_ttl,CreatedAt,GTtl,tbl_MSal.CompanyId, tbl_MSal.BranchId,tbl_MSal.MSal_id,Booker,SalMan ";

                dt_ = DBConnection.GetQueryData(total);

                if (dt_.Rows.Count > 0)
                {
                    lbl_ttl.Text = dt_.Rows[0]["Total Sales"].ToString();
                }
                else
                {
                    // Code Error
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void getempsale(string booker, string fdat, string tdat)
        {
            try
            {
                string query = " select tbl_MSal.MSal_id,ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID], CreatedBy, DSal_ItmQty ,GTtl as [Total], replace (convert(NVARCHAR, CreatedAt, 101), '/', '/') as [CreatedAt],tbl_MSal.CompanyId, tbl_MSal.BranchId, Booker,SalMan from tbl_MSal inner join tbl_DSal on tbl_MSal.MSal_id = tbl_DSal.MSal_id where booker = '" + booker + "' and CreatedAt between '" + fdat + "' and '" + tdat + "' and tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' and tbl_MSal.BranchId= '" + Session["BranchID"] + "' group by CreatedBy,DSal_ItmQty,DSal_ttl,CreatedAt,GTtl,tbl_MSal.CompanyId, tbl_MSal.BranchId,tbl_MSal.MSal_id,Booker,SalMan  order by MSal_id desc ";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    GVEMP.DataSource = dt_;
                    GVEMP.DataBind();
                }
                else
                {
                    //
                }
                string total = " select  tbl_MSal.MSal_id,ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID], CreatedBy, DSal_ItmQty ,sum(GTtl) as [Total Sales],  replace (convert(NVARCHAR, CreatedAt, 101), '/', '/') as [CreatedAt],tbl_MSal.CompanyId, tbl_MSal.BranchId, Booker,SalMan from tbl_MSal inner join tbl_DSal on tbl_MSal.MSal_id = tbl_DSal.MSal_id where booker = '" + booker + "' and CreatedAt between '" + fdat + "' and '" + tdat + "' and tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' and tbl_MSal.BranchId= '" + Session["BranchID"] + "' group by CreatedBy,DSal_ItmQty,DSal_ttl,CreatedAt,GTtl,tbl_MSal.CompanyId, tbl_MSal.BranchId,tbl_MSal.MSal_id,Booker,SalMan ";
                
                dt_ = DBConnection.GetQueryData(total);

                if (dt_.Rows.Count > 0)
                {
                    lbl_ttl.Text = dt_.Rows[0]["Total Sales"].ToString();
                }
                else
                {
                    // Code Error
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void getempsales(string salman, string fdat, string tdat)
        {
            try
            {
                string query = " select tbl_MSal.MSal_id,ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID], CreatedBy, DSal_ItmQty ,GTtl as [Total], replace (convert(NVARCHAR, CreatedAt, 101), '/', '/') as [CreatedAt],tbl_MSal.CompanyId, tbl_MSal.BranchId, Booker,SalMan from tbl_MSal inner join tbl_DSal on tbl_MSal.MSal_id = tbl_DSal.MSal_id where SalMan = '" + salman + "' and CreatedAt between '" + fdat + "' and '" + tdat + "' and  tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' and tbl_MSal.BranchId= '" + Session["BranchID"] + "' group by CreatedBy,DSal_ItmQty,DSal_ttl,CreatedAt,GTtl,tbl_MSal.CompanyId, tbl_MSal.BranchId,tbl_MSal.MSal_id,Booker,SalMan  order by MSal_id desc ";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    GVEMP.DataSource = dt_;
                    GVEMP.DataBind();
                }
                else
                {
                    //
                }

                string total = " select  tbl_MSal.MSal_id,ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID], CreatedBy, DSal_ItmQty ,sum(GTtl) as [Total Sales],  replace (convert(NVARCHAR, CreatedAt, 101), '/', '/') as [CreatedAt],tbl_MSal.CompanyId, tbl_MSal.BranchId, Booker,SalMan from tbl_MSal inner join tbl_DSal on tbl_MSal.MSal_id = tbl_DSal.MSal_id where SalMan = '" + salman + "' and CreatedAt between '" + fdat + "' and '" + tdat + "' and tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' and tbl_MSal.BranchId= '" + Session["BranchID"] + "' group by CreatedBy,DSal_ItmQty,DSal_ttl,CreatedAt,GTtl,tbl_MSal.CompanyId, tbl_MSal.BranchId,tbl_MSal.MSal_id,Booker,SalMan ";

                dt_ = DBConnection.GetQueryData(total);

                if (dt_.Rows.Count > 0)
                {
                    lbl_ttl.Text = dt_.Rows[0]["Total Sales"].ToString();
                }
                else
                {
                    // Code Error
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}