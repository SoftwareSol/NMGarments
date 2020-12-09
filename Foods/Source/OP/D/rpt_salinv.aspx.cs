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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string SalID = Request.QueryString["SalID"];
                FillGrid();
            }
        }

        public void FillGrid()
        {
            try
            {

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(" SELECT * FROM [v_salrecipt]  ");


                //lbl_intro

                string Customer = dt_.Rows[0]["Customer"].ToString(); //lbl_intro
                lbl_intro.Text = Customer;


                //GVSal.DataSource = dt_;
                //GVSal.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}