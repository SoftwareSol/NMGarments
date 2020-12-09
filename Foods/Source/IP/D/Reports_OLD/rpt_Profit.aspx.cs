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
    public partial class rpt_Profit : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string PROID, SALID;

        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];
            if (check != null)
            {
                SALID = Request.QueryString["SALID"];
                PROID = Request.QueryString["PROID"];
                lbl_dat.Text = DateTime.Now.ToShortDateString();

                if (PROID != null)
                {
                    FillGrid(PROID, SALID);
                }

            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }

        }
        public void FillGrid(string proid, string msalid)
        {
            try
            {
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(" SELECT * FROM v_proft where ProductID = '" + proid + "' and MSal_id = '"+ msalid +"'");

                GVProf.DataSource = dt_;
                GVProf.DataBind();

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

    }
}