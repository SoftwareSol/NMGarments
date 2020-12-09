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
    public partial class frm_Payments : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_ = null;
        DBConnection db = new DBConnection();
        string TBItms, lblItmpris, TBItmQty, lblcat, lblttl, HFDSal;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {
                    try
                    {
                        lbl_usr.Text = Session["Name"].ToString();
                        FillGrid();
                        lblerr.Text = "";
                    }

                    catch { Response.Redirect("~/Login.aspx"); }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void TBItmQty_TextChanged(object sender, EventArgs e)
        {
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetSearch(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select CustomerName from Customers_ where CustomerName like '" + prefixText + "%'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetCustMob(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select CellNo1 from Customers_ where CellNo1 like '" + prefixText + "%'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getpro(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select ProductName from Products where ProductName like '" + prefixText + "%'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetBill(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select BillNO from tbl_MPos where BillNO like '" + prefixText + "%'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }

        public void FillGrid()
        {
            try
            {
                //Sales Order
                using (SqlCommand cmd = new SqlCommand())
                {

                    /*cmd.CommandText = " select top 20 ROW_NUMBER() OVER(ORDER BY tbl_MPos.Mposid DESC) AS [ID],tbl_Mpos.Mposid,tbl_Mpos.BillNO,billdat,left(billtim,8) as [billtim], tbl_MPos.CustomerName,Customers_.CellNo1,tbl_DPos.productid, " +
                    " productname,proqty,grntttl, tbl_Mpos.createdby,convert(varchar, tbl_Mpos.createdat, 103) as [createdat] " +
                    " from tbl_Mpos inner join tbl_DPos on tbl_MPos.Mposid = tbl_DPos.Mposid INNER JOIN Customers_  on tbl_MPos.CellNo1 = Customers_.CellNo1 " +
                    " inner join Products on tbl_DPos.ProductID = Products.ProductID where tbl_Mpos.CompanyId = '" + Session["CompanyID"] + "' and tbl_Mpos.BranchId= '" + Session["BranchID"] + "' order by Mposid desc ";*/
                    cmd.CommandText = "SELECT top 20 ROW_NUMBER() OVER(ORDER BY SS.Mposid DESC) AS [ID], SS.Mposid,SS.BillNO,  SS.CustomerName,SS.CellNo1,adv,bal, grntttl,billdat,left(billtim,8) as [billtim], SS.createdby,convert(varchar, SS.createdat, 103) as [createdat] FROM tbl_Mpos SS inner join tbl_DPos on SS.Mposid = tbl_DPos.Mposid INNER JOIN Customers_  on SS.CellNo1 = Customers_.CellNo1 GROUP BY SS.Mposid, SS.CustomerName,SS.BillNO, billdat, billtim, SS.CellNo1,SS.createdby,SS.createdat, adv,bal, grntttl";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtchkcust_ = new DataTable();

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtchkcust_);

                    GVSrchCust.DataSource = dtchkcust_;
                    GVSrchCust.DataBind();

                    con.Close();
                }


            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
            }
        }

        protected void lnkbtn_Logout_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("~/Login.aspx");
        }

        protected void TB_SearchCust_TextChanged(object sender, EventArgs e)
        {
            try
            {
                /*string query = " select top 20 ROW_NUMBER() OVER(ORDER BY tbl_MPos.Mposid DESC) AS [ID],tbl_Mpos.Mposid,tbl_Mpos.BillNO,billdat,left(billtim,8) as [billtim], tbl_MPos.CustomerName,Customers_.CellNo1,tbl_DPos.productid, " +
                    " productname,proqty,grntttl, tbl_Mpos.createdby,convert(varchar, tbl_Mpos.createdat, 103) as [createdat] " +
                    " from tbl_Mpos inner join tbl_DPos on tbl_MPos.Mposid = tbl_DPos.Mposid INNER JOIN Customers_  on tbl_MPos.CellNo1 = Customers_.CellNo1 " +
                    " inner join Products on tbl_DPos.ProductID = Pr where tbl_Mpos.CompanyId = '" + Session["CompanyID"] + "' and tbl_Mpos.BranchId= '" + Session["BranchID"] + "' and tbl_MPos.CellNo1 ='" + TB_SearchCust.Text.Trim() + "' order by Mposid desc ";*/
                string query = "SELECT top 20 ROW_NUMBER() OVER(ORDER BY SS.Mposid DESC) AS [ID], SS.Mposid,SS.BillNO,  SS.CustomerName,SS.CellNo1,adv,bal, grntttl,billdat,left(billtim,8) as [billtim], SS.createdby,convert(varchar, SS.createdat, 103) as [createdat] FROM tbl_Mpos SS inner join tbl_DPos on SS.Mposid = tbl_DPos.Mposid INNER JOIN Customers_  on SS.CellNo1 = Customers_.CellNo1 where SS.CellNo1 = '" + TB_SearchCust.Text.Trim() + "' GROUP BY SS.Mposid, SS.CustomerName,SS.BillNO, billdat, billtim, SS.CellNo1,SS.createdby,SS.createdat, adv,bal, grntttl ";
                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt_ = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dt_);

                if (dt_.Rows.Count > 0)
                {
                    GVSrchCust.DataSource = dt_;
                    GVSrchCust.DataBind();

                    TB_SearchCust.Text = "";
                    lblerr.Text = "";
                }
                else
                {
                    lblerr.Text = "Sorry No Record Exits!";
                    FillGrid();
                }
            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
            }

        }

        protected void GVSrchCust_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }


        protected void GVSrchCust_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVSrchCust.EditIndex = e.NewEditIndex;
            FillGrid();
        }

        protected void GVSrchCust_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try {

                //Finding the controls from Gridview for the row which is going to update  
                Label id = GVSrchCust.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
                TextBox txt_Adv = GVSrchCust.Rows[e.RowIndex].FindControl("txt_Adv") as TextBox;
                TextBox txt_bal = GVSrchCust.Rows[e.RowIndex].FindControl("txt_bal") as TextBox;
                con.Open();
                //updating the record  
                SqlCommand cmd = new SqlCommand("Update tbl_Dpos set Adv='" + txt_Adv.Text.Trim() + "',bal='" + txt_bal.Text.Trim() + "' where Mposid=" + Convert.ToInt32(id.Text), con);
                cmd.ExecuteNonQuery();
                con.Close();
                //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                GVSrchCust.EditIndex = -1;
                //Call ShowData method for displaying updated data  
                FillGrid();
            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
            }
        }

        protected void GVSrchCust_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVSrchCust.EditIndex = -1;
            FillGrid();
        }

        protected void txt_Adv_TextChanged(object sender, EventArgs e)
        {
            try
            {
                    TextBox txt_Adv = ((GridViewRow)GVSrchCust.Rows[GVSrchCust.EditIndex]).FindControl("txt_Adv") as TextBox;
                    TextBox txt_bal = ((GridViewRow)GVSrchCust.Rows[GVSrchCust.EditIndex]).FindControl("txt_bal") as TextBox;
                    Label lbl_ettl = ((GridViewRow)GVSrchCust.Rows[GVSrchCust.EditIndex]).FindControl("lbl_ettl") as Label;

                
                    string adv = txt_Adv.Text.Trim();
                    string balc = txt_bal.Text.Trim();

                    string bal = (Convert.ToInt32(lbl_ettl.Text) - Convert.ToInt32(txt_Adv.Text.Trim())).ToString();

                    if (txt_Adv.Text == "0")
                    {
                        txt_bal.Text = "0";
                    }
                    else
                    {
                        txt_bal.Text = bal;
                    }
            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
            }
        }

    }
}