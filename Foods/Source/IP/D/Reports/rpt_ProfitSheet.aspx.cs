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
        string FrmDat, Todat;

        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];
            if (check != null)
            {
                FrmDat = Request.QueryString["FrmDat"];
                Todat = Request.QueryString["Todat"];

                FillGrid(FrmDat, Todat);
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
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                dt_ = new DataTable();
                //dt_ = DBConnection.GetQueryData(" SELECT * FROM v_proft where MSal_dat between '" + fdat + "' and '" + tdat + "' order by MSal_id desc ");

                SqlDataAdapter adapter = new SqlDataAdapter();
                //SqlCommand cmd = new SqlCommand("usp_GetABCD", con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //adapter.SelectCommand = cmd;
                //adapter.Fill(dt_);
                con.Open();
                command.Parameters.Clear();
                command.CommandText = "sp_proft";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@frmdat", fdat);
                command.Parameters.AddWithValue("@todat", tdat);

                command.ExecuteNonQuery();

                adapter.SelectCommand = command;
                adapter.Fill(dt_);

                lblfrmdat.Text = FrmDat;
                lbltodat.Text = Todat;

                GVProf.DataSource = dt_;
                GVProf.DataBind();

                //For Details

                decimal GTotal = 0;

                // Balance
                for (int j = 0; j < GVProf.Rows.Count; j++)
                {
                    Label total = (Label)GVProf.Rows[j].FindControl("lbl_diff");
                    if (total.Text != "")
                    {
                        GTotal += Convert.ToDecimal(total.Text);
                    }
                }

                lbl_diff.Text = GTotal.ToString();
                con.Close();
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