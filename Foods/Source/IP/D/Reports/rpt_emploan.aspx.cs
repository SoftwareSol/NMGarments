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
    public partial class rpt_emploan : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string fdat, ldat, EmpLoan, query, accno;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {
                    fdat = Request.QueryString["FRMDat"];
                    EmpLoan = Request.QueryString["EmpLoan"];

                    if (fdat != null && EmpLoan != null)
                    {
                        get_Empcre(fdat);
                    }
                    else
                    {
                        get_Empcre();
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
                string FileName = "EmployeeLoanList.xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

                GV_EmpCre.GridLines = GridLines.Both;
                GV_EmpCre.HeaderStyle.Font.Bold = true;

                GV_EmpCre.RenderControl(htmltextwrtter);

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

        private void get_Empcre(string fdat)
        {
            dt_ = new DataTable();

            query = " select * from SubHeadCategories where  SubHeadCategoriesName = '" + EmpLoan + "' and SubHeadGeneratedID = '0023'";

            dt_ = DBConnection.GetQueryData(query);

            if (dt_.Rows.Count > 0)
            {
                accno = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
            }

            query = " select * from v_emploan where AccNo = '" + accno + "' and LoanDat  >= '" + fdat + "' and SubHeadGeneratedID = '0023'";

            dt_ = DBConnection.GetQueryData(query);

            if (dt_.Rows.Count > 0)
            {
                lbl_Emp.Text = dt_.Rows[0]["SubHeadCategoriesName"].ToString();

                GV_EmpCre.DataSource = dt_;
                GV_EmpCre.DataBind();
            }

            float GTotal = 0;
            for (int k = 0; k < GV_EmpCre.Rows.Count; k++)
            {
                Label total = (Label)GV_EmpCre.Rows[k].FindControl("lbl_lonamt");

                GTotal += Convert.ToSingle(total.Text);
            }

            lbl_ttl.Text = GTotal.ToString();

            

        }


        private void get_Empcre()
        {
            dt_ = new DataTable();

            query = " select * from SubHeadCategories where  SubHeadCategoriesName = '" + EmpLoan + "'";

            dt_ = DBConnection.GetQueryData(query);

            if (dt_.Rows.Count > 0)
            {
                accno = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
            }

            query = " select * from v_emploan where AccNo = '" + accno + "'";

            dt_ = DBConnection.GetQueryData(query);

            if (dt_.Rows.Count > 0)
            {
                GV_EmpCre.DataSource = dt_;
                GV_EmpCre.DataBind();
            }


        }
    }
}