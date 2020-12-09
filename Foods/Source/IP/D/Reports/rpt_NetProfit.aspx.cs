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
    public partial class rpt_NetProfit : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string FrmNetDat, ToNetdat, NetMon, NetYr;

        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];
            if (check != null)
            {
                FrmNetDat = Request.QueryString["FrmNetDat"];
                ToNetdat = Request.QueryString["ToNetdat"];
            
                NetMon = Request.QueryString["NetMon"];
                NetYr = Request.QueryString["NetYr"];

                lbl_dat.Text = DateTime.Now.ToShortDateString();

                if (FrmNetDat != null && ToNetdat != null)
                {
                    FillDAT(FrmNetDat, ToNetdat);
                }
                else if (NetMon != null && NetYr != null)
                {
                    FillMONYR(NetMon,NetYr);
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

        }
        public void FillDAT(string fdat, string tdat)
        {
            try
            {

                dt_ = new DataTable();
                
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand("sp_GetNetProtdattDat", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Fdat", fdat.Trim());
                cmd.Parameters.AddWithValue("@Ldat", tdat.Trim());

                adapter.SelectCommand = cmd;
                adapter.Fill(dt_);

                if (dt_.Rows.Count > 0)
                {
                    GVProf.DataSource = dt_;
                    GVProf.DataBind();
                }


                //For Details

                decimal diff = 0;

                for (int j = 0; j < GVProf.Rows.Count; j++)
                {
                    Label lbl_netprof = (Label)GVProf.Rows[j].FindControl("lbl_netprof");

                    diff += Convert.ToDecimal(lbl_netprof.Text);

                }

                lbl_proff.Text = diff.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void FillMONYR(string MON, string YR)
        {
            try
            {

                dt_ = new DataTable();

                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand("sp_GetNetProtmonyr", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@mon", MON.Trim());
                cmd.Parameters.AddWithValue("@yr", YR.Trim());

                adapter.SelectCommand = cmd;
                adapter.Fill(dt_);

                if (dt_.Rows.Count > 0)
                {
                    GVProf.DataSource = dt_;
                    GVProf.DataBind();
                }

                //For Details
                
                decimal diff = 0;

                for (int j = 0; j < GVProf.Rows.Count; j++)
                {
                    Label lbl_netprof = (Label)GVProf.Rows[j].FindControl("lbl_netprof");

                    diff += Convert.ToDecimal(lbl_netprof.Text);

                }

                lbl_proff.Text = diff.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}