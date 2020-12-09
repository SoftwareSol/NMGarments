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
    public partial class rpt_ProfitSheet : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string FrmDat, Todat, Pro, query, proid ;

        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];
            if (check != null)
            {
                FrmDat = Request.QueryString["FrmDat"];
                Todat = Request.QueryString["Todat"];
                Pro = Request.QueryString["Pro"];

                if (FrmDat != null && Todat != null && Pro != null)
                {
                    FillGrid(FrmDat, Todat, Pro);
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

        public void FillGrid(string fdat, string tdat, string Pronam)
        {
            try
            {
                dt_ = new DataTable();

                query = " select * from Products where ProductName='" + Pronam + "'";
                dt_ = DBConnection.GetQueryData(query);
                if (dt_.Rows.Count > 0)
                {
                    proid = dt_.Rows[0]["ProductID"].ToString();
                }

                query = " SELECT * FROM v_proft where ProductID='" + proid + "' and MSal_dat between '" + fdat + "' and '" + tdat + "'";

                dt_ = DBConnection.GetQueryData(query);

                lblfrmdat.Text = FrmDat;
                lbltodat.Text = Todat;

                if (dt_.Rows.Count > 0)
                {
                    GVProf.DataSource = dt_;
                    GVProf.DataBind();
                }

                //For Details

                decimal disc = 0;
                decimal salrat = 0;
                decimal discamt = 0;
                decimal diff = 0;

                for (int j = 0; j < GVProf.Rows.Count; j++)
                {
                    Label lblsatrat = (Label)GVProf.Rows[j].FindControl("lbl_satrat");
                    Label lbdisc = (Label)GVProf.Rows[j].FindControl("lbl_disc");
                    Label lbdiscamt = (Label)GVProf.Rows[j].FindControl("lb_discamt");
                    Label lb_diff = (Label)GVProf.Rows[j].FindControl("lbl_diff");

                    salrat += Convert.ToDecimal(lblsatrat.Text);
                    disc += Convert.ToDecimal(lbdisc.Text);

                    discamt += Convert.ToDecimal(lbdiscamt.Text);
                    diff += Convert.ToDecimal(lb_diff.Text);

                }


                lblttl.Text = salrat.ToString();
                lbldisc.Text = disc.ToString();
                lbl_discamt.Text = discamt.ToString();
                lbl_diff.Text = diff.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void FillGrid(string fdat, string tdat)
        {
            try
            {
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(" SELECT * FROM v_proft where MSal_dat between '" + fdat + "' and '" + tdat + "'");

                if (dt_.Rows.Count > 0)
                {
                    GVProf.DataSource = dt_;
                    GVProf.DataBind();
                }

                lblfrmdat.Text = FrmDat;
                lbltodat.Text = Todat;

                //For Details

                decimal disc = 0;
                decimal salrat = 0;
                decimal discamt = 0;
                decimal diff = 0;

                for (int j = 0; j < GVProf.Rows.Count; j++)
                {
                    Label lblsatrat = (Label)GVProf.Rows[j].FindControl("lbl_satrat");
                    Label lbdisc = (Label)GVProf.Rows[j].FindControl("lbl_disc");
                    Label lbdiscamt = (Label)GVProf.Rows[j].FindControl("lb_discamt");
                    Label lb_diff = (Label)GVProf.Rows[j].FindControl("lbl_diff");

                    salrat += Convert.ToDecimal(lblsatrat.Text);
                    disc += Convert.ToDecimal(lbdisc.Text);

                    discamt += Convert.ToDecimal(lbdiscamt.Text);
                    diff += Convert.ToDecimal(lb_diff.Text);

                }


                lblttl.Text = salrat.ToString();
                lbldisc.Text = disc.ToString();
                lbl_discamt.Text = discamt.ToString();
                lbl_diff.Text = diff.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GVProf_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row;

                if (e.CommandName == "Detail")
                {
                    row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    string msalID = GVProf.DataKeys[row.RowIndex].Values[0].ToString();
                    string proID = GVProf.DataKeys[row.RowIndex].Values[2].ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'rpt_Profit.aspx?ID=PROF&SALID=" + msalID + "&PROID=" + proID + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}