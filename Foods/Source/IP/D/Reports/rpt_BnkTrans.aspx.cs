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
    public partial class rpt_BnkTrans : System.Web.UI.Page
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
                //DateTime CheqDat =Convert.ToDateTime(Request.QueryString["CheqDat"]);
                string CheqDat =Request.QueryString["CheqDat"];
                int Bnk =Convert.ToInt32(Request.QueryString["Bnk"]);
                string CheqCust = Request.QueryString["CheqCust"];
                string CheqEmp = Request.QueryString["CheqEmp"];
                string CheqSup = Request.QueryString["CheqSup"];
                string CheqExp = Request.QueryString["CheqExp"];
                string date = "";

                lbl_dat.Text = DateTime.Now.ToShortDateString();

                if (CheqDat != null && Bnk != null && CheqCust != null)
                {
                    Cheqbankcust(CheqDat , Bnk , CheqCust);
                }
                else if (CheqDat != null && Bnk != null && CheqEmp != null)
                {
                    Cheqbankemp(CheqDat, Bnk, CheqEmp);
                }
                else if (CheqDat != null && Bnk != null && CheqSup != null)
                {
                    Cheqbanksup(CheqDat, Bnk, CheqSup);
                }
                else if (CheqDat != null && Bnk != null && CheqExp != null)
                {
                    Cheqbankexp(CheqDat, Bnk, CheqExp);
                }

                else if (Bnk != null && CheqExp != null)
                {
                    BnkExp(Bnk, CheqExp);
                }
                else if (CheqDat != null && CheqExp != null)
                {
                    Chequeexp(CheqDat, CheqExp);
                }
                else if (Bnk != null && CheqSup != null)
                {
                    BnkSup(Bnk, CheqSup);
                }
                else if (CheqDat != null && CheqSup != null)
                {
                    ChequeSup(CheqDat, CheqSup);
                }



                else if (Bnk != null && CheqEmp != null)
                {
                    BnkEmp(Bnk, CheqEmp);
                }
                else if (CheqDat != null && CheqEmp != null)
                {
                    ChequeEmp(CheqDat, CheqEmp);
                }



                else if (Bnk != null && CheqCust != null)
                {
                    BnkCust(Bnk, CheqCust);
                }
                else if (CheqDat != null && Bnk != null && CheqCust != null)
                {
                    ChequeCust(CheqDat, CheqCust);
                }
                //if (CheqDat != null && Bnk != 0 && CheqCust != null && CheqEmp != null && CheqSup != null && CheqExp != null)
                //{
                //    FillGrid(CheqDat, Bnk, CheqCust, CheqEmp, CheqSup, CheqExp);
                //}
                //else if (CheqDat != null && Bnk != 0 && CheqCust != null && CheqEmp != null && CheqSup != null)
                //{
                //    FillGrid(CheqDat, Bnk, CheqCust, CheqEmp, CheqSup);
                //}
                //else if (CheqDat != null && Bnk != 0 && CheqCust != null && CheqEmp != null)
                //{
                //    FillGrid(CheqDat, Bnk, CheqCust, CheqEmp);
                //}

                else if (CheqDat != null && Bnk != 0)
                {
                    FillGrid(CheqDat, Bnk);
                }
                
                else if (Bnk != 0)
                {
                    FillGrid(Bnk);
                }
                else if (CheqExp != null)
                {
                    FillGrid(CheqExp);
                }
                else if (CheqSup != null)
                {
                    FillGrid(CheqSup);
                }
                else if (CheqEmp != null)
                {
                    FillGrid(CheqEmp);
                }
                else if (CheqCust != null)
                {
                    FillGrid(CheqCust);
                }
                //else if (CheqDat != null && date)
                //{
                //    FillGrid(CheqDat);
                //}
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
                
                GVBnkTrans.GridLines = GridLines.Both;
                GVBnkTrans.HeaderStyle.Font.Bold = true;

                GVBnkTrans.RenderControl(htmltextwrtter);

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

        public void FillGrid(string CheqDat, int Bnk, string CheqCust, string CheqEmp, string CheqSup, string CheqExp)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and CashBnk_id='" + Bnk + "' and accno IN ('" + CheqCust + "','" + CheqEmp + "','" + CheqSup + "','" + CheqExp + "') and Chqdat ='" + CheqDat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";


                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillGrid(string CheqDat, int Bnk, string CheqCust, string CheqEmp, string CheqSup)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and CashBnk_id='" + Bnk + "' and accno IN ('" + CheqCust + "','" + CheqEmp + "','" + CheqSup + "') and Chqdat ='" + CheqDat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";


                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillGrid(string CheqDat, int Bnk, string CheqCust, string CheqEmp)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and CashBnk_id='" + Bnk + "' and accno IN ('" + CheqCust + "','" + CheqEmp + "') and Chqdat ='" + CheqDat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";


                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Cheqbankcust(string CheqDat, int Bnk, string CheqCust)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and CashBnk_id='" + Bnk + "' and accno ='" + CheqCust + "' and Chqdat ='" + CheqDat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChequeCust(string CheqDat, string CheqCust)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and accno ='" + CheqCust + "' and Chqdat ='" + CheqDat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BnkCust(int Bnk, string CheqCust)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and CashBnk_id='" + Bnk +
                    "' and accno ='" + CheqCust + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Cheqbankemp(string CheqDat, int Bnk, string cheqemp)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and CashBnk_id='" + Bnk +
                    "' and accno ='" + cheqemp + "' and Chqdat ='" + CheqDat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChequeEmp(string CheqDat, string cheqemp)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and accno ='" + cheqemp +
                    "' and Chqdat ='" + CheqDat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BnkEmp(int Bnk, string cheqemp)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and CashBnk_id='" + Bnk +
                    "' and accno ='" + cheqemp + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Cheqbanksup(string CheqDat, int Bnk, string cheqsup)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and CashBnk_id='" + Bnk +
                    "' and accno ='" + cheqsup + "' and Chqdat ='" + CheqDat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChequeSup(string CheqDat, string cheqsup)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and accno ='" + cheqsup +
                    "' and Chqdat ='" + CheqDat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BnkSup(int Bnk, string cheqsup)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and CashBnk_id='" + Bnk +
                    "' and accno ='" + cheqsup + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void Cheqbankexp(string CheqDat, int Bnk, string cheqexp)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and CashBnk_id='" + Bnk +
                    "' and accno ='" + cheqexp + "' and Chqdat ='" + CheqDat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Chequeexp(string CheqDat, string cheqexp)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and accno ='" + cheqexp +
                    "' and Chqdat ='" + CheqDat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BnkExp(int Bnk, string cheqexp)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and CashBnk_id='" + Bnk +
                    "' and accno ='" + cheqexp + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillGrid(string CheqDat, int Bnk)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and CashBnk_id='" + Bnk + "' and Chqdat ='" + CheqDat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public void FillGrid(string CheqDat, string date)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and  Chqdat ='" + CheqDat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillGrid(string bakacc)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and accno ='" + bakacc + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";


                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

                    lbl_dat.Text = dt_.Rows[0]["expensesdat"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillGrid(int bakid)
        {
            try
            {
                //q = " select * from  v_diffRat  where MPurDate <= '" + dat + "' and MPurID ='" + mpurid + "' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                q = "select * from v_bnkTrans where typeofpay in ('Cheque','Both') and CashBnk_id ='" + bakid + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";


                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(q);

                if (dt_.Rows.Count > 0)
                {
                    GVBnkTrans.DataSource = dt_;
                    GVBnkTrans.DataBind();

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