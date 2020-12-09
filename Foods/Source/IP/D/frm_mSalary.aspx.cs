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
    public partial class frm_mSalary : System.Web.UI.Page
    {
        DataTable dt_;
        SqlConnection con = DataAccess.DBConnection.connection();
        DataAccess.DBConnection db = new DataAccess.DBConnection();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                FillGrid();
                BindDll();
                DDLEmp.Focus();
           }
        }

        public void FillGrid()
        {
            try
            {
                //DataTable dt_ = new DataTable();
                //dt_ = tbl_MSalaryManager.GetMSalList();
                //GVEmp.DataSource = dt_;
                //GVEmp.DataBind();
                //ViewState["Employee"] = dt_;
            }
            catch (Exception ex)
            {
                //throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }


        public void BindDll()
        {
            try
            {
                //DataTable dt_ = new DataTable();
                //dt_ = tbl_employeeManager.GetempList();

                //DDLEmp.DataSource = dt_;
                //DDLEmp.DataTextField = "employeeName";
                //DDLEmp.DataValueField = "employeeID";
                //DDLEmp.DataBind();
                //DDLEmp.Items.Insert(0, new ListItem("--Select Employee--", "0"));
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Save();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("frm_mSalary.aspx");
        }

        protected void GVEmp_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string id = GVEmp.DataKeys[e.RowIndex].Values[0] != null ? GVEmp.DataKeys[e.RowIndex].Values[0].ToString() : null;

                HFEmpSalID.Value = id;

                int j = 0;

                j = DelEmpSal();

                if (j == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Employee Salary Record has been deleted!";

                    Response.Redirect("frm_mSalary.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Some thing has wrong plzz contact Administrator!";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }           

        }

        protected void GVEmp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                if (e.CommandName == "Show")
                {
                    string RPTID = Server.HtmlDecode(GVEmp.Rows[row.RowIndex].Cells[0].Text.ToString());

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=Salaryrpt&RPTID=" + RPTID + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                }

                if (e.CommandName == "Select")
                {

                    //GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    string EmpID = GVEmp.DataKeys[row.RowIndex].Values[0].ToString();
                    HFEmpSalID.Value = EmpID;

                    dt_ = DBConnection.GetQueryData("select * from tbl_MSalary where MSal_ID = '" + HFEmpSalID.Value.Trim() + "'");

                    if (dt_.Rows.Count > 0)
                    {
                        DDLEmp.SelectedValue = dt_.Rows[0]["employeeID"].ToString();
                        HFMSalary_ID.Value = dt_.Rows[0]["MSal_ID"].ToString();
                        TB_BS.Text = dt_.Rows[0]["Basic_Sal"].ToString();
                        TB_INC.Text = dt_.Rows[0]["Incre"].ToString();
                        TB_SalPerDay.Text = dt_.Rows[0]["sal_per_day"].ToString();
                        TB_SalPerHR.Text = dt_.Rows[0]["sal_per_hr"].ToString();
                        TB_DyAbsnt.Text = dt_.Rows[0]["days_absnt"].ToString();
                        TB_DyAttn.Text = dt_.Rows[0]["days_attn"].ToString();

                        //Over Time
                        TBHRS.Text = dt_.Rows[0]["ot_hrs"].ToString();
                        TBDays.Text = dt_.Rows[0]["ot_days"].ToString();
                        TBOTAmt.Text = dt_.Rows[0]["ot_amt"].ToString();

                        //Deduction
                        TBAdvDTC.Text = dt_.Rows[0]["adv_deduction"].ToString();
                        TBLNDTC.Text = dt_.Rows[0]["loan_deduction"].ToString();

                        TBCrtnTyp.Text = dt_.Rows[0]["Ctns_typ"].ToString();
                        TBBtlsTyp.Text = dt_.Rows[0]["btls_typ"].ToString();
                        TBCrtnPric.Text = dt_.Rows[0]["rs_Ctns"].ToString();
                        TBBtlsPric.Text = dt_.Rows[0]["rs_btls"].ToString();

                        TB_BS.Focus();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                        lblalert.Text = "Not Record Found!";
                    }
                }
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }


        }

        protected void TBSearch_TextChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            try
            {
                FillGrid();
                DataTable _dt = (DataTable)ViewState["Employee"];
                DataView dv = new DataView(_dt, "employeeName LIKE '%" + TBSearch.Text.Trim().ToUpper() + "%'", "[employeeName] ASC", DataViewRowState.CurrentRows);
                DataTable dt_ = new DataTable();
                dt_ = dv.ToTable();
                GVEmp.DataSource = dt_;
                GVEmp.DataBind();
                TBSearch.Text = "";
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        protected void linkmodaldelete_Click(object sender, EventArgs e)
        { 

        }

        protected void btnalertOk_Click(object sender, EventArgs e)
        { 

        }

        private int DelEmpSal()
        {
            int i = 1;

            string sqlquery = " delete from  tbl_MSalary where MSal_ID = '" + HFEmpSalID.Value + "'";
            db.CRUDRecords(sqlquery);

            return i;

        }

        protected void DDLEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from tbl_MSalary where employeeID ='" + DDLEmp.SelectedValue.Trim() + "'";

                dt_ = DataAccess.DBConnection.GetDataTable(query);

                if (dt_.Rows.Count > 0)
                {
                    HFMSalary_ID.Value = dt_.Rows[0]["MSal_ID"].ToString();
                    TB_BS.Text = dt_.Rows[0]["Basic_Sal"].ToString();
                    TB_INC.Text = dt_.Rows[0]["Incre"].ToString();
                    TB_SalPerDay.Text = dt_.Rows[0]["sal_per_day"].ToString();
                    TB_SalPerHR.Text = dt_.Rows[0]["sal_per_hr"].ToString();
                    TB_DyAbsnt.Text = dt_.Rows[0]["days_absnt"].ToString();
                    TB_DyAttn.Text = dt_.Rows[0]["days_attn"].ToString();

                    //Over Time
                    TBHRS.Text = dt_.Rows[0]["ot_hrs"].ToString();
                    TBDays.Text = dt_.Rows[0]["ot_days"].ToString();
                    TBOTAmt.Text = dt_.Rows[0]["ot_amt"].ToString();

                    //Deduction
                    TBAdvDTC.Text = dt_.Rows[0]["adv_deduction"].ToString();
                    TBLNDTC.Text = dt_.Rows[0]["loan_deduction"].ToString();

                    TBCrtnTyp.Text = dt_.Rows[0]["Ctns_typ"].ToString();
                    TBBtlsTyp.Text = dt_.Rows[0]["btls_typ"].ToString();
                    TBCrtnPric.Text = dt_.Rows[0]["rs_Ctns"].ToString();
                    TBBtlsPric.Text = dt_.Rows[0]["rs_btls"].ToString();
                    
                    TB_BS.Focus();
                }
                else
                {
                    HFMSalary_ID.Value = "";
                    TB_BS.Text = "0";
                    TB_INC.Text = "0";
                    TB_SalPerDay.Text = "0";
                    TB_SalPerHR.Text = "0";
                    TB_DyAbsnt.Text = "0";
                    TB_DyAttn.Text = "0";

                    //Over Time
                    TBHRS.Text = "0";
                    TBDays.Text = "0";
                    TBOTAmt.Text = "0";

                    //Deduction
                    TBAdvDTC.Text = "0";
                    TBLNDTC.Text = "0";

                    TBCrtnTyp.Text = "-";
                    TBBtlsTyp.Text = "-";
                    TBCrtnPric.Text = "0";
                    TBBtlsPric.Text = "0";

                    TB_BS.Focus();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        private int SaveEmpSal()
        {
            int j = 1;
            //try
            //{

            //    if (DDLEmp.SelectedValue != "0")
            //    {
            //        tbl_MSalary msalary = new tbl_MSalary();

                    
            //        double Basic_Sal, Incre, sal_per_day, sal_per_hr, days_absnt, days_attn, ot_hrs, ot_days, ot_amt, adv_deduction, loan_deduction, rs_Ctns, rs_btls;
            //        string MSal_ID, employeeID,Ctns_typ, btls_typ;

            //        msalary.MSal_ID = string.IsNullOrEmpty(HFMSalary_ID.Value) ? null : HFMSalary_ID.Value;
            //        Basic_Sal = Convert.ToDouble(string.IsNullOrEmpty(TB_BS.Text) ? null : TB_BS.Text);
            //        Incre = Convert.ToDouble(string.IsNullOrEmpty(TB_INC.Text) ? null : TB_INC.Text);
            //        sal_per_day = Convert.ToDouble(string.IsNullOrEmpty(TB_SalPerDay.Text) ? null : TB_SalPerDay.Text);
            //        sal_per_hr = Convert.ToDouble(string.IsNullOrEmpty(TB_SalPerHR.Text) ? null : TB_SalPerHR.Text);
            //        days_absnt = Convert.ToDouble(string.IsNullOrEmpty(TB_DyAbsnt.Text) ? null : TB_DyAbsnt.Text);
            //        days_attn =  Convert.ToDouble(string.IsNullOrEmpty(TB_DyAttn.Text) ? null : TB_DyAttn.Text);
            //        ot_hrs =  Convert.ToDouble(string.IsNullOrEmpty(TBHRS.Text) ? null : TBHRS.Text);
            //        ot_days = Convert.ToDouble(string.IsNullOrEmpty(TBDays.Text) ? null : TBDays.Text);
            //        ot_amt = Convert.ToDouble(string.IsNullOrEmpty(TBOTAmt.Text) ? null : TBOTAmt.Text);
            //        adv_deduction = Convert.ToDouble(string.IsNullOrEmpty(TBAdvDTC.Text) ? null : TBAdvDTC.Text);
            //        loan_deduction = Convert.ToDouble(string.IsNullOrEmpty(TBLNDTC.Text) ? null : TBLNDTC.Text);
            //        rs_Ctns = Convert.ToDouble(string.IsNullOrEmpty(TBCrtnPric.Text) ? null : TBCrtnPric.Text);
            //        rs_btls = Convert.ToDouble(string.IsNullOrEmpty(TBBtlsPric.Text) ? null : TBBtlsPric.Text);
            //        Ctns_typ = string.IsNullOrEmpty(TBCrtnTyp.Text) ? null : TBCrtnTyp.Text;
            //        btls_typ = string.IsNullOrEmpty(TBBtlsTyp.Text) ? null : TBBtlsTyp.Text;
            //        employeeID =string.IsNullOrEmpty(DDLEmp.SelectedValue) ? null : DDLEmp.SelectedValue;
                    
            //        //msalary.MSal_ID = MSal_ID.ToString();
            //        msalary.Basic_Sal = Basic_Sal.ToString();
            //        msalary.Incre = Incre.ToString();
            //        msalary.sal_per_day = sal_per_day.ToString();
            //        msalary.sal_per_hr = sal_per_hr.ToString();
            //        msalary.days_absnt = days_absnt.ToString();
            //        msalary.days_attn = days_attn.ToString();

            //        //Over Time
            //        msalary.ot_hrs = ot_hrs.ToString();
            //        msalary.ot_days = ot_days.ToString();
            //        msalary.ot_amt = ot_amt.ToString();

            //        //Deduction
            //        msalary.adv_deduction = adv_deduction.ToString();
            //        msalary.loan_deduction = loan_deduction.ToString();
            //        msalary.create_by = Session["user"].ToString();
            //        msalary.craete_at = DateTime.Now;
            //        msalary.employeeID = employeeID.ToString();

            //        //Filling Sections
            //        msalary.Ctns_typ = Ctns_typ;
            //        msalary.btls_typ = btls_typ;
            //        msalary.rs_Ctns = rs_Ctns.ToString();
            //        msalary.rs_btls = rs_btls.ToString();


            //        //Gross Salary
            //        double grss_sal =   sal_per_day + sal_per_hr + ot_hrs + ot_days + ot_amt  + rs_Ctns + rs_btls;

            //        msalary.grss_sal = grss_sal.ToString();//string.IsNullOrEmpty(TBPhone.Value) ? null : TBPhone.Value;

            //        //Net Total

            //        double Net_sal = grss_sal + rs_Ctns + rs_btls - (adv_deduction + loan_deduction);

            //        msalary.Net_sal = Net_sal.ToString();//string.IsNullOrEmpty(TBAddress.Value) ? null : TBAddress.Value;

            //        //Sub Total
            //        //msalary.sub_ttl = "345.8";//string.IsNullOrEmpty(TBRelign.Value) ? null : TBRelign.Value;

            //        //Grand Total                
            //        //msalary.grd_ttl = "456.8";//string.IsNullOrEmpty(TBBSal.Value) ? null : TBBSal.Value;


            //        tbl_MSalaryManager msalmanag = new tbl_MSalaryManager(msalary);
            //        msalmanag.Save();
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
            //        lblalert.Text = "Please Select the Employees!";
            //        DDLEmp.Focus();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
            //    lblalert.Text = ex.Message;

            //}
            return j;
        }

        private void Save()
        {
            int a;
            a = SaveEmpSal();


            if (a == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = "Employee Salary Sheet Has Been Saved!";

                Response.Redirect("frm_mSalary.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = "Opps! Some thing is wrong Call the Administrator!!";
            }

        }
    }
}