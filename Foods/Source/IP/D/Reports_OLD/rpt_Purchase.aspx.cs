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
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Cfg;
using Foods;
using System.Globalization;
using DataAccess;


namespace Foods
{
    public partial class rpt_Purchase : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        string Id,PurId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {
                    Id = Request.QueryString["PR"];
                    PurId = Request.QueryString["PURID"];

                    FillGrid();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

        }

        

        public void FillGrid()
        {
            try
            {
                if (Id == null)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = " select * from v_rptpur where MPurID='" + PurId + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                        cmd.Connection = con;
                        con.Open();

                        DataTable dt_ = new DataTable();
                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        adp.Fill(dt_);

                        if (dt_.Rows.Count > 0)
                        {
                            
                            lbl_vouc.Text = dt_.Rows[0]["MPurID"].ToString();
                            lbl_dat.Text = dt_.Rows[0]["MPurDate"].ToString();
                            //lbl_Fright.Text = dt_.Rows[0]["frieght"].ToString();
                            LBLShpTo.Text = dt_.Rows[0]["suppliername"].ToString();
                            lbladd.Text = dt_.Rows[0]["Address"].ToString();
                            //lbloth.Text = dt_.Rows[0]["Otheramt"].ToString();
                            lbl_usr.Text = Session["user"].ToString();
             
                            GVShowpurItms.DataSource = dt_;
                            GVShowpurItms.DataBind();

                            float GTotal = 0;

                            for (int j = 0; j < GVShowpurItms.Rows.Count; j++)
                            {
                                Label total = (Label)GVShowpurItms.Rows[j].FindControl("lblamt");
                                GTotal += Convert.ToSingle(total.Text);
                            }

                            lblgrssttl.Text = GTotal.ToString();
                            //lblgrssttl.Text = (Convert.ToDecimal(dt_.Rows[0]["allcharges"]) + (Convert.ToDecimal(GTotal))).ToString();
                        }

                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;               
            }
        }
    }
}