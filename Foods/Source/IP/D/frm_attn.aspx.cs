using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using Foods;

using NHibernate;
using NHibernate.Criterion;
using NHibernate.Cfg;

namespace Foods
{
    public partial class frm_attn : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        SqlCommand cmd = new SqlCommand();       
        SqlTransaction tran;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //TBTim.Text = DateTime.Now.TimeOfDay.ToString();
                DateTime test = DateTime.Now;
                TBTim.Text = test.ToShortTimeString();
                BindDll();
                TBTim.Enabled = false;
            }
        }

        public void BindDll()
        {
            try
            {
                //For Employee
                //Users
                string usrqy = "select  Username,CompanyId,BranchId  from Users where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                DataTable dtusr = new DataTable();
                dtusr = DataAccess.DBConnection.GetDataTable(usrqy);

                if (dtusr.Rows.Count > 0)
                {
                    DDL_Emp.DataSource = dtusr;
                    DDL_Emp.DataTextField = "employeeName";
                    DDL_Emp.DataValueField = "employeeID";
                    DDL_Emp.DataBind();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }

        private int Save()
        {
            int j = 1;

            //tbl_attn att = new tbl_attn();

            //att.attnid = "";
            //att.attn_dat = Convert.ToDateTime(DateTime.Today.ToShortDateString());
            //att.attn_tim = string.IsNullOrEmpty(TBTim.Text) ? null : TBTim.Text;
            //att.employeeID = string.IsNullOrEmpty(DDL_Emp.SelectedValue) ? null : DDL_Emp.SelectedValue;

            //tbl_attnManager attmanag = new tbl_attnManager(att);
            //attmanag.Save();

            return j;

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;

                i = Save();

                if (i < 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lbl_Heading.Text = "Saved!";
                    lblalert.Text = "Attendance has been Marked!";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }

        }

        protected void DDL_Emp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //DataTable dtempid = new DataTable();

                //dtempid = tbl_attnManager.GetEmpID(DDL_Emp.SelectedValue);

                //if (dtempid.Rows.Count > 0)
                //{
                //    lbl_empid.Text = dtempid.Rows[0]["employeeID"].ToString();
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }

    }
}