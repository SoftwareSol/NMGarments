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
    public partial class rpt_IncomProf : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        DataTable dt_ = null;
        string FrmDat, Todat, LEDG;

        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];

            if (check != null)
            {
                FrmDat = Request.QueryString["FDAT"];
                Todat = Request.QueryString["TDAT"];
                LEDG = Request.QueryString["LEDG"];
                
                lbl_ttl.Text = "Available Balance:";

                if ((FrmDat != null) && (Todat != null) && (LEDG != null))
                {
                    FillGrid(FrmDat, Todat, LEDG);

                }else if ((FrmDat != null) && (Todat != null))
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

                string query = " SELECT * FROM v_incomexp where expensesdat between '" + fdat + "' and '" + tdat + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' order by expenceid asc ";
                //where expensesdat between '" + fdat + "' and '" + tdat + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    lblfrmdat.Text = FrmDat;
                    lbltodat.Text = Todat;

                    GVIncomExp.DataSource = dt_;
                    GVIncomExp.DataBind();

                    //For Details
                    //float GTotal = 0;
                    Label total= null;

                    for (int j = 0; j < GVIncomExp.Rows.Count; j++)
                    {
                        total = (Label)GVIncomExp.Rows[j].FindControl("lbl_availbal");

                        //GTotal += Convert.ToSingle(total.Text);
                    }

                    lblttl.Text = total.Text.Trim();

                }
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
                //string str = "SELECT * FROM v_incomexp where accno ='" + acc + "' and expensesdat between '" + fdat + "' and '" + tdat + "'  order by expenceid asc";


                ////dt_ = DBConnection.GetQueryData(" SELECT * FROM v_profloss where accno = " + acc + " and Date between '" + fdat + "' and '" + tdat + "'");
                //dt_ = DBConnection.GetQueryData(str);

               
                using (var cmd = new SqlCommand("sp_incomexp", con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@accno", acc.Trim());
                    cmd.Parameters.AddWithValue("@fdat ", fdat.Trim());
                    cmd.Parameters.AddWithValue("@tdat ", tdat.Trim());
                    cmd.Parameters.AddWithValue("@companyid ", Session["CompanyID"].ToString());
                    cmd.Parameters.AddWithValue("@branchid ", Session["BranchID"].ToString());

                    da.Fill(dt_);
                }

                if (dt_.Rows.Count > 0)
                {
                    lblfrmdat.Text = FrmDat;
                    lbltodat.Text = Todat;


                    GVIncomExp.DataSource = dt_;
                    GVIncomExp.DataBind();

                    //For Details
                    float GTotals = 0;
                    for (int j = 0; j < GVIncomExp.Rows.Count; j++)
                    {
                        Label lbl_overallbal = (Label)GVIncomExp.Rows[j].FindControl("lbl_availbal");

                        GTotals += Convert.ToSingle(lbl_overallbal.Text);
                        
                    }

                    lbl_ttl.Text = "Total Balance:";
                    lblttl.Text = GTotals.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}