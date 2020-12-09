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
    public partial class rpt_salinv : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string SalID;
        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];
            if (check != null)
            {
                SalID = Request.QueryString["SalID"];
                FillGrid();
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        public void FillGrid()
        {
            try
            {
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(" SELECT * FROM v_salrecipt where MSal_id='" + SalID + "'");

                if (dt_.Rows.Count > 0)
                {
                    lbl_intro.Text = dt_.Rows[0]["Customer"].ToString();
                    lb_preout.Text = dt_.Rows[0]["Outstanding"].ToString();
                    lblbilno.Text = dt_.Rows[0]["Bill"].ToString();
                    lblsaldat.Text = dt_.Rows[0]["SalDat"].ToString();
                    lbladd.Text = dt_.Rows[0]["Address"].ToString();
                    lblph.Text = dt_.Rows[0]["PhoneNo"].ToString();
                    lbldisc.Text = dt_.Rows[0]["Dis"].ToString();
                    lbldiscamt.Text = dt_.Rows[0]["DisAmt"].ToString();
                    lbl_grrsttl.Text = dt_.Rows[0]["GTtl"].ToString();
                    lbl_salreturn.Text = dt_.Rows[0]["isreturn"].ToString();
                }
                //For Details

                DataTable dtdetail_ = new DataTable();
                dtdetail_ = DBConnection.GetQueryData(" SELECT * FROM v_saldrecipt where MSal_id='" + SalID + "'");
                if (dtdetail_.Rows.Count > 0)
                {
                    GVSal.DataSource = dtdetail_;
                    GVSal.DataBind();

                    decimal GTotal = 0;

                    for (int j = 0; j < GVSal.Rows.Count; j++)
                    {
                        Label total = (Label)GVSal.Rows[j].FindControl("lbl_afterdis");
                        GTotal += Convert.ToDecimal(total.Text);
                    }

                    lb_currnetpay.Text = (Convert.ToDecimal(lbl_grrsttl.Text) - Convert.ToDecimal( lbldiscamt.Text)).ToString();//dtdetail_.Rows[0]["NetAmount"].ToString(); //GTotal.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}