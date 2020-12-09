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
    public partial class rpt_CashTrans : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string q;
        float GTotal;

        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];
            if (check != null)
            {
                string FrmDat = Request.QueryString["FrmDat"];
                string Todat = Request.QueryString["Todat"];
                string CashCust = Request.QueryString["CashCust"];
                string CashEmp = Request.QueryString["CashEmp"];
                string CashSup = Request.QueryString["CashSup"];
                string CashExp = Request.QueryString["CashExp"];

                lbl_dat.Text = DateTime.Now.ToShortDateString();

                if (FrmDat != null && Todat != null && CashCust != null)
                {
                    getFrmToAcc(FrmDat, Todat, CashCust);
                }
                else if (FrmDat != null && CashCust != null)
                {
                    getFrmAcc(FrmDat,CashCust); //rpt_CashTrans
                }
                else if (FrmDat != null && Todat != null && CashEmp != null )
                {
                    getFrmToAcc(FrmDat, Todat, CashEmp);

                }
                else if (FrmDat != null  && CashEmp != null)
                {
                    getFrmAcc(FrmDat, CashEmp);

                }
                else if (FrmDat != null && Todat != null && CashSup != null)
                {
                    getFrmToAcc(FrmDat, Todat, CashSup);

                }
                else if (FrmDat != null && CashSup != null)
                {
                    getFrmAcc(FrmDat, CashSup);

                }
                else if (FrmDat != null && Todat != null && CashExp != null)
                {
                    getFrmToAcc(FrmDat, Todat, CashExp);

                }
                else if (FrmDat != null && CashExp != null)
                {
                    getFrmAcc(FrmDat, CashExp);

                }







                //if (FrmDat != null && Todat != null && CashCust != null && CashEmp != null && CashSup != null && CashExp != null)
                //{
                //    FillGrid(FrmDat, Todat, CashCust, CashEmp, CashSup, CashExp);
                //}
                //else if (FrmDat != null && Todat != null && CashCust != null && CashEmp != null && CashSup != null)
                //{
                //    FillGrid(FrmDat, Todat, CashCust, CashEmp, CashSup);
                //}
                //else if (FrmDat != null && Todat != null && CashCust != null && CashEmp != null)
                //{
                //    FillGrid(FrmDat, Todat, CashCust, CashEmp);
                //}
                //else if (FrmDat != null && Todat != null && CashCust != null)
                //{
                //    FillGrid(FrmDat, Todat, CashCust);

                //}
                //else if (FrmDat != null && Todat != null)
                //{
                //    FillGrid(FrmDat, Todat);

                //}
                //else if (CashExp != null)
                //{
                //    FillGrid(CashExp);
                //}
                //else if (CashSup != null)
                //{
                //    FillGrid(CashSup);
                //}
                //else if (CashEmp != null)
                //{
                //    FillGrid(CashEmp);
                //}
                //else if (CashCust != null)
                //{
                //    FillGrid(CashCust);
                //}
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }



        public void getFrmToAcc(string frmdat, string todat, string cashacc)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_cashTrans where typeofpay = 'cash' and accno ='" + cashacc + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and expensesdat between '" + frmdat + "' and '" + todat + "'";


                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVCashTrans.DataSource = dt_;
                    GVCashTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void getFrmAcc(string frmdat, string cashacc)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_cashTrans where typeofpay = 'cash' and accno ='" + cashacc + "' and expensesdat ='" +
                    frmdat + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";


                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVCashTrans.DataSource = dt_;
                    GVCashTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
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
                string FileName = "StockList.xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

                GVCashTrans.GridLines = GridLines.Both;
                GVCashTrans.HeaderStyle.Font.Bold = true;

                GVCashTrans.RenderControl(htmltextwrtter);

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


        public void FillGrid(string frmdat, string todat, string cashcust, string cashemp, string cashsup, string cashexp)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_cashTrans where typeofpay = 'cash' and accno IN ('" + cashcust + "','" + cashemp + "','" + cashsup + "','" + cashexp + "')  and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and expensesdat between '" + frmdat + "' and '" + todat + "'";                
                
                
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVCashTrans.DataSource = dt_;
                    GVCashTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillGrid(string frmdat, string todat, string cashcust, string cashemp, string cashsup)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_cashTrans where typeofpay = 'cash' and accno IN ('" + cashcust + "','" + cashemp + "','" + cashsup + "') and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and expensesdat between '" + frmdat + "' and '" + todat + "'";


                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVCashTrans.DataSource = dt_;
                    GVCashTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillGrid(string frmdat, string todat, string cashcust, string cashemp)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_cashTrans where typeofpay = 'cash' and accno IN ('" + cashcust + "','" + cashemp + "')  and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and expensesdat between '" + frmdat + "' and '" + todat + "'";


                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVCashTrans.DataSource = dt_;
                    GVCashTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillGrid(string frmdat, string todat, string cashcust)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_cashTrans where typeofpay = 'cash' and accno ='" + cashcust + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and expensesdat between '" + frmdat + "' and '" + todat + "'";


                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVCashTrans.DataSource = dt_;
                    GVCashTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillGrid(string frmdat, string todat)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_cashTrans where typeofpay = 'cash' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and expensesdat between '" + frmdat + "' and '" + todat + "'";


                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVCashTrans.DataSource = dt_;
                    GVCashTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillGrid(string cashacc)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_cashTrans where typeofpay = 'cash' and accno ='" + cashacc + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";


                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVCashTrans.DataSource = dt_;
                    GVCashTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}