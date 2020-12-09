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
    public partial class rpt_BillSumm : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string SalmanID;

        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];
            if (check != null)
            {
                SalmanID = Request.QueryString["MDNID"];
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
                dt_ = DBConnection.GetQueryData(" select * from v_billsumm where CreatedBy ='arif'");

                if (dt_.Rows.Count > 0)
                {
                    //lblbilno.Text = dt_.Rows[0]["Mdn_id"].ToString();
                    //lblmdndat.Text = dt_.Rows[0]["Mdn_dat"].ToString();
                    //lblsalNo.Text = dt_.Rows[0]["MSal_id"].ToString();
                    //lbl_saldat.Text = dt_.Rows[0]["Mdn_SalDat"].ToString();
                    //lbl_intro.Text = dt_.Rows[0]["SubHeadCategoriesName"].ToString();
                    //lbladd.Text = dt_.Rows[0]["Address"].ToString();
                    //lblph.Text = dt_.Rows[0]["PhoneNo"].ToString();

                    
                    Repeater1.DataSource = dt_;
                    Repeater1.DataBind();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                GridView gv = (GridView)e.Item.FindControl("GVCashMemo");
                if (gv != null)
                {
                    dt_ = new DataTable();
                    dt_ = DBConnection.GetQueryData(" select * from v_billsumm where CreatedBy ='arif'");

                    if (dt_.Rows.Count > 0)
                    {

                        DataRowView drv = (DataRowView)e.Item.DataItem;
                        gv.DataSource = dt_; //(Convert.ToInt32(drv["ID"]));
                        gv.DataBind();
                    }
                }
            }
        }
    }
}