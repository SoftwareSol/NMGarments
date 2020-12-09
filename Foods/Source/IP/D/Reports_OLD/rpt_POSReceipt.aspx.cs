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
    public partial class rpt_POSReceipt : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string posid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {
                    lbl_Comp.Text = Session["Company"].ToString();
                    lbl_dattim.Text = DateTime.Now.ToString();
                    lbl_usr.Text = Session["user"].ToString();
                    lbl_add.Text = "Shop # 2 Opposite Rafah-e-aam Post Office Malir Halt, Karachi";
                    lbl_no.Text = "0321-2010080";
                    posid = Request.QueryString["POSID"];
                    get_posal(posid);
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void get_posal(string POSID)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select tbl_mpos.BillNO,ProductID as [Description],ProQty as [Qty],Ttl as [Rate],Ttl as [Amount] from tbl_mpos inner join tbl_DPos on tbl_mpos.Mposid = tbl_DPos.Mposid where tbl_MPos.BillNO = '" + POSID + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtpos = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtpos);
                    if (dtpos.Rows.Count > 0)
                    {
                        lbl_bill.Text = dtpos.Rows[0]["BillNO"].ToString();

                        GVPOS.DataSource = dtpos;
                        GVPOS.DataBind();

                        //Get Total

                        double GTotal = 0;
                        double QGTotal = 0;
                        // Total
                        for (int j = 0; j < GVPOS.Rows.Count; j++)
                        {
                            Label total = (Label)GVPOS.Rows[j].FindControl("lbl_rat");
                            GTotal += Convert.ToDouble(total.Text);
                        }

                        //Quantity
                        for (int j = 0; j < GVPOS.Rows.Count; j++)
                        {
                            Label totalqty = (Label)GVPOS.Rows[j].FindControl("lbl_qty");
                            QGTotal += Convert.ToDouble(totalqty.Text);
                        }

                        lblitmcnt.Text = dtpos.Compute("count(" + dtpos.Columns[1].ColumnName + ")", null).ToString(); 
                        lbl_netamt.Text = GTotal.ToString();

                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}