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
    public partial class rpt_stk : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string q, Siz, ITMSIZ, procatid, proid, dat, STCK;
        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];
            if (check != null)
            {
                procatid = Request.QueryString["Protyp"];
                proid = Request.QueryString["ProID"];
                dat = Request.QueryString["dat"];
                Siz = Request.QueryString["ITMSIZ"];
                STCK = Request.QueryString["STK"];

                lbl_rptnam.Text = "Stock Summary";

                lbl_dat.Text = DateTime.Now.ToShortDateString();

                if (procatid != null)
                {
                    FillProcat(procatid);
                    //calttlProcat(procatid, dat);
                }
                else if (proid != null && dat != null && Siz != null)
                {
                    FillPro(proid, Siz, dat);
                    //calttPro(proid, Siz, dat);
                }
                else if (proid != null)
                {
                    FillPro(proid, dat);
                    //calttPro(proid, dat);
                }
                else if (Siz != null)
                {
                    FillGrid();
                    //calttlsiz();
                }
                else
                {
                    FillGrid(dat);
                    //calttl();
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
        public void FillGrid(string dat)
        {
            try
            {
                q = " select * from  v_stk  where returntyp not in ('Defected') and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);


                GVStk.DataSource = dt_;
                GVStk.DataBind();
                float GTotal = 0;
                for (int k = 0; k < GVStk.Rows.Count; k++)
                {
                    Label total = (Label)GVStk.Rows[k].FindControl("tb_stkval");
                    GTotal += Convert.ToSingle(total.Text);
                }

                lbl_ttlStkVal.Text = GTotal.ToString();


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
                q = " select * from  v_stk  where Dstk_unt = '" + Siz + "' and returntyp not in ('Defected') and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);


                GVStk.DataSource = dt_;
                GVStk.DataBind();

                float GTotal = 0;
                for (int k = 0; k < GVStk.Rows.Count; k++)
                {
                    Label total = (Label)GVStk.Rows[k].FindControl("tb_stkval");
                    GTotal += Convert.ToSingle(total.Text);
                }

                lbl_ttlStkVal.Text = GTotal.ToString();



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillPro(string proid, string siz, string dat)
        {
            try
            {
                dt_ = new DataTable();


                q = "select ProductID from Products where ProductType = '" + proid + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    proid = dt_.Rows[0]["ProductID"].ToString();
                }

                q = " select * from  v_stk  where ProductID ='" + proid +
                    "' and Dstk_unt = '" + Siz + "' and returntyp not in ('Defected') and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                dt_ = DBConnection.GetQueryData(q);

                GVStk.DataSource = dt_;
                GVStk.DataBind();

                float GTotal = 0;
                for (int k = 0; k < GVStk.Rows.Count; k++)
                {
                    Label total = (Label)GVStk.Rows[k].FindControl("tb_stkval");
                    GTotal += Convert.ToSingle(total.Text);
                }

                lbl_ttlStkVal.Text = GTotal.ToString();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillPro(string proid, string dat)
        {
            try
            {
                //dt_ = new DataTable();


                //q = "select ProductID from Products where ProductType = '" + proid + "'";

                //dt_ = DBConnection.GetQueryData(q);

                //if (dt_.Rows.Count > 0)
                //{
                //    proid = dt_.Rows[0]["ProductID"].ToString();
                //}

                q = " select * from  v_stk  where  ProductID ='" + proid
                    + "' and returntyp not in ('Defected') and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                dt_ = DBConnection.GetQueryData(q);

                GVStk.DataSource = dt_;
                GVStk.DataBind();

                float GTotal = 0;
                for (int k = 0; k < GVStk.Rows.Count; k++)
                {
                    Label total = (Label)GVStk.Rows[k].FindControl("tb_stkval");
                    GTotal += Convert.ToSingle(total.Text);
                }

                lbl_ttlStkVal.Text = GTotal.ToString();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void FillProcat(string protypid)
        {
            try
            {
                q = " select * from  v_stk  where ProductType ='" + protypid +
                    "' and returntyp not in ('Defected') and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                GVStk.DataSource = dt_;
                GVStk.DataBind();

                float GTotal = 0;
                for (int k = 0; k < GVStk.Rows.Count; k++)
                {
                    Label total = (Label)GVStk.Rows[k].FindControl("tb_stkval");
                    GTotal += Convert.ToSingle(total.Text);
                }

                lbl_ttlStkVal.Text = GTotal.ToString();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void calttPro(string proid, string siz, string dat)
        {
            try
            {
                string quer = "select sum(Dstk_purrat) as ttl_purrat, sum(Dstk_salrat)  as ttl_salrat,  " +
                    " sum(Dstk_salrat - Dstk_purrat ) as [Prft], " +
                    " sum(Dstk_Qty * Dstk_purrat) as [ttlStkVal] from tbl_Dstk " +
                    " inner join Products on tbl_Dstk.ProductID = Products.ProductID " +
                    " where tbl_Dstk.ProductID = '" + proid + "' and Dstk_unt='" + siz +
                    "' and returntyp not in ('Defected') and Products.CompanyId = '" + Session["CompanyID"] + "' and Products.BranchId= '" + Session["BranchID"] + "'";
                dt_ = new DataTable();

                dt_ = DBConnection.GetQueryData(quer);

                if (dt_.Rows.Count > 0)
                {
                    lbl_ttlStkVal.Text = dt_.Rows[0]["ttlStkVal"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void calttPro(string proid, string dat)
        {
            try
            {
                string quer = "select sum(Dstk_purrat) as ttl_purrat, sum(Dstk_salrat)  as ttl_salrat,  " +
                    " sum(Dstk_salrat - Dstk_purrat ) as [Prft], " +
                    " sum(Dstk_Qty * Dstk_purrat) as [ttlStkVal] from tbl_Dstk " +
                    " inner join Products on tbl_Dstk.ProductID = Products.ProductID " +
                    " where tbl_Dstk.ProductID = '" + proid + "' and returntyp not in ('Defected') and Products.CompanyId = '" + Session["CompanyID"] + "' and Products.BranchId= '" + Session["BranchID"] + "'";
                dt_ = new DataTable();

                dt_ = DBConnection.GetQueryData(quer);

                if (dt_.Rows.Count > 0)
                {
                    lbl_ttlStkVal.Text = dt_.Rows[0]["ttlStkVal"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void calttl()
        {
            try
            {

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(" select sum(Dstk_rat)  as ttl_salrat, " +
                    " sum(Dstk_Qty * Dstk_rat) as [ttlStkVal] from tbl_Dstk where returntyp not in ('Defected') and and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                if (dt_.Rows.Count > 0)
                {
                    lbl_ttlStkVal.Text = dt_.Rows[0]["ttlStkVal"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void calttlsiz()
        {
            try
            {

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(" select sum(Dstk_rat)  as ttl_salrat, " +
                    " sum(Dstk_Qty * Dstk_rat) as [ttlStkVal] from tbl_Dstk where Dstk_unt='" + Siz
                    + "' and returntyp not in ('Defected') and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                if (dt_.Rows.Count > 0)
                {
                    lbl_ttlStkVal.Text = dt_.Rows[0]["ttlStkVal"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private void calttlProcat(string procatid, string dat)
        {
            try
            {
                string query = " select sum(Dstk_rat)  as ttl_salrat, " +
                    "   sum(Dstk_Qty * Dstk_rat) as [ttlStkVal] from tbl_Dstk  " +
                    "  inner join Products on tbl_Dstk.ProductID = Products.ProductID where  Products.ProductType = '"
                    + procatid + "' and returntyp not in ('Defected') and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    lbl_ttlStkVal.Text = dt_.Rows[0]["ttlStkVal"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}