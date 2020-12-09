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
    public partial class frm_Chkcust : System.Web.UI.Page
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
                    cmd.CommandText = "SELECT top 20 ROW_NUMBER() OVER(ORDER BY SS.Mposid DESC) AS [ID], SS.Mposid,SS.BillNO,  SS.CustomerName, (SELECT ',' + US.ProductID  FROM tbl_Dpos US WHERE US.Mposid = SS.Mposid   FOR XML PATH('')) [Items], SS.CellNo1,billdat,left(billtim,8) as [billtim], SS.createdby,convert(varchar, SS.createdat, 103) as [createdat] FROM tbl_Mpos SS  INNER JOIN Customers_  on SS.CellNo1 = Customers_.CellNo1 GROUP BY SS.Mposid, SS.CustomerName,SS.BillNO, billdat, billtim, SS.CellNo1,SS.createdby,SS.createdat";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtchkcust_ = new DataTable();

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtchkcust_);

                    GVSerachCust.DataSource = dtchkcust_;
                    GVSerachCust.DataBind();

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
                string query = " SELECT top 20 ROW_NUMBER() OVER(ORDER BY SS.Mposid DESC) AS [ID], SS.Mposid,SS.BillNO,  SS.CustomerName, (SELECT ',' + US.ProductID  FROM tbl_Dpos US WHERE US.Mposid = SS.Mposid   FOR XML PATH('')) [Items], SS.CellNo1,billdat,left(billtim,8) as [billtim], SS.createdby,convert(varchar, SS.createdat, 103) as [createdat] FROM tbl_Mpos SS  INNER JOIN Customers_  on SS.CellNo1 = Customers_.CellNo1 where SS.CompanyId = '" + Session["CompanyID"] + "' and SS.BranchId= '" + Session["BranchID"] + "' and SS.CellNo1 ='" + TB_SearchCust.Text.Trim() + "' GROUP BY SS.Mposid, SS.CustomerName,SS.BillNO, billdat, billtim, SS.CellNo1,SS.createdby,SS.createdat";
                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt_ = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dt_);

                if (dt_.Rows.Count > 0)
                {
                    GVSerachCust.DataSource = dt_;
                    GVSerachCust.DataBind();

                    TB_SearchCust.Text = "";
                    lblerr.Text = "";

                }
                else
                {
                    lblerr.Text = "Sorry No Record Exits!";
                }
            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
            }

        }

        protected void GVSerachCust_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }

        protected void GVSerachCust_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                string BillNO = GVSerachCust.DataKeys[row.RowIndex].Values[0].ToString();

                if (e.CommandName == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_POSReceipt.aspx?ID=POS&POSID=" + BillNO + "','_blank','height=600px,width=400px,scrollbars=1');", true);
                }
                else if (e.CommandName == "Delete")
                {
                    try
                    {
                        //string BillNO = Server.HtmlDecode(GVSerachCust.Rows[e.RowIndex].Cells[1].Text.ToString());

                        SqlCommand cmd = new SqlCommand();

                        cmd = new SqlCommand("sp_del_POS", con);
                        cmd.Parameters.Add("@BillNO", SqlDbType.VarChar).Value = BillNO;
                        cmd.Parameters.Add("@CompanyId", SqlDbType.VarChar).Value = Session["CompanyID"];
                        cmd.Parameters.Add("@BranchId", SqlDbType.VarChar).Value = Session["BranchID"];
                        cmd.CommandType = CommandType.StoredProcedure;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();


                        Response.Redirect("frm_Chkcust.aspx");

                    }
                    catch (Exception ex)
                    {
                        lblerr.Text = ex.Message;
                    }


                }
            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
            }
        }
    }
}