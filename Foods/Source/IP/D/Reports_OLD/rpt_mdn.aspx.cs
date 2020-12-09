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
    public partial class rpt_mdn : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string MDNID;

        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];
            if (check != null)
            {
                MDNID = Request.QueryString["MDNID"];
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
                dt_ = DBConnection.GetQueryData(" SELECT * FROM v_rptmdn where Mdn_id='" + MDNID + "'");

                if (dt_.Rows.Count > 0)
                {
                    lblbilno.Text = dt_.Rows[0]["Mdn_id"].ToString();
                    lblmdndat.Text = dt_.Rows[0]["Mdn_dat"].ToString();
                    lblsalNo.Text = dt_.Rows[0]["MSal_id"].ToString();
                    lbl_saldat.Text = dt_.Rows[0]["Mdn_SalDat"].ToString();
                    lbl_intro.Text = dt_.Rows[0]["SubHeadCategoriesName"].ToString();
                    lbladd.Text = dt_.Rows[0]["Address"].ToString();
                    lblph.Text = dt_.Rows[0]["PhoneNo"].ToString();

                    GVSal.DataSource = dt_;
                    GVSal.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
