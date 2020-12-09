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
using DataAccess;

using NHibernate;
using NHibernate.Criterion;

namespace Foods
{
    public partial class rpt_Expence : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        DataTable dt_ = null;
        string FrmDat, Todat, EXP;

        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];

            if (check != null)
            {
                FrmDat = Request.QueryString["FDAT"];
                Todat = Request.QueryString["TDAT"];
                EXP = Request.QueryString["EXP"];

                if (FrmDat != null && Todat != null && EXP != null)
                {
                    FillGrid(FrmDat, Todat, EXP);

                }
                else if (FrmDat != null && Todat != null)
                {
                    FillGrid(FrmDat, Todat);
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

        }

        public void FillGrid(string fdat, string tdat)
        {
            try
            {
                dt_ = new DataTable();

                string query = " SELECT * FROM v_expence where expensesdat between '" + fdat + "' and '" + tdat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                
                dt_ = DBConnection.GetQueryData(query);

                lblfrmdat.Text = FrmDat;
                lbltodat.Text = Todat;

                GVProf.DataSource = dt_;
                GVProf.DataBind();

                //For Details
                float GTotal = 0;
                for (int j = 0; j < GVProf.Rows.Count; j++)
                {
                    Label total = (Label)GVProf.Rows[j].FindControl("lbl_amt");

                    GTotal += Convert.ToSingle(total.Text);
                }

                lblttl.Text = GTotal.ToString();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void FillGrid(string fdat, string tdat, string acc)
        {
            try
            {
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(" SELECT * FROM v_expence where accno = '" + acc + "' and expensesdat between '" + fdat + "' and '" + tdat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                lblfrmdat.Text = FrmDat;
                lbltodat.Text = Todat;

                GVProf.DataSource = dt_;
                GVProf.DataBind();

                //For Details
                float GTotals = 0;
                for (int j = 0; j < GVProf.Rows.Count; j++)
                {
                    Label total = (Label)GVProf.Rows[j].FindControl("lbl_amt");

                    GTotals += Convert.ToSingle(total.Text);
                }

                lblttl.Text = GTotals.ToString();
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}