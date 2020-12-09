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
    public partial class frm_MaintainInven : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_ = null;
        DBConnection db = new DBConnection();
        string TBItms, lblItmpris, TBItmQty, lblcat, lblttl, HFDSal, txt_pro, lbl_Proid, query;
        public static string branch, company;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {
                    try
                    {
                        branch = Session["BranchID"].ToString();
                        company = Session["CompanyID"].ToString();

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
            string str = "select CustomerName from Customers_ where CustomerName like '" + prefixText + "%' and CompanyId='" + company + "' and BranchId='" + branch + "'";
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
            string str = "select CellNo1 from Customers_ where CellNo1 like '" + prefixText + "%' and CompanyId='" + company + "' and BranchId='" + branch + "'";
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
            string str = " select distinct(ProductName)  from tbl_Dstk inner join Products  " +
                " on tbl_Dstk.ProductID = Products.ProductID where ProductName like '" + prefixText + "%'  and Products.CompanyId='" + company + "' and Products.BranchId='" + branch + "'";

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
            string str = "select BillNO from tbl_MPos where BillNO like '" + prefixText + "%' and CompanyId='" + company + "' and BranchId='" + branch + "'";
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
                    cmd.CommandText = "select tbl_Dstk.productid, ProductName, Dstk_id,Dstk_Qty,Dstk_rat,Dstk_unt from tbl_dstk inner join Products on tbl_Dstk.ProductID = Products.ProductID where  tbl_Dstk.CompanyId='" + company + "' and tbl_Dstk.BranchId='" + branch + "'";

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
                //string query = "SELECT top 20 ROW_NUMBER() OVER(ORDER BY SS.Mposid DESC) AS [ID], SS.Mposid,SS.BillNO,  SS.CustomerName,SS.CellNo1,adv,bal, grntttl,billdat,left(billtim,8) as [billtim], SS.createdby,convert(varchar, SS.createdat, 103) as [createdat] FROM tbl_Mpos SS inner join tbl_DPos on SS.Mposid = tbl_DPos.Mposid INNER JOIN Customers_  on SS.CellNo1 = Customers_.CellNo1 where SS.CellNo1 = '" + TB_SearchCust.Text.Trim() + "' GROUP BY SS.Mposid, SS.CustomerName,SS.BillNO, billdat, billtim, SS.CellNo1,SS.createdby,SS.createdat, adv,bal, grntttl ";

                string query = " select distinct(tbl_Dstk.ProductID),Dstk_id, ProductName, Dstk_rat,Dstk_Qty, Dstk_unt from tbl_Dstk inner join Products " +
                    " on tbl_Dstk.ProductID = Products.ProductID where ProductName='" + TB_SearchCust.Text.Trim() + "' and Products.CompanyId='" + Session["CompanyID"] + "' and Products.BranchId='" + Session["BranchID"] + "'";

                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt_ = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dt_);

                if (dt_.Rows.Count > 0)
                {
                    GVSrchCust.DataSource = dt_;
                    GVSrchCust.DataBind();


                    ViewState["getnum"] = dt_;
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
            if (ViewState["getnum"] != null)
            {
                DataTable dt_ = (DataTable)ViewState["getnum"];
                if (dt_.Rows.Count > 0)
                {
                    GVSrchCust.DataSource = dt_;
                    GVSrchCust.DataBind();

                    DropDownList DDL_Pro = GVSrchCust.Rows[e.NewEditIndex].FindControl("DDL_Pro") as DropDownList;
                    Label lbl_Proid = GVSrchCust.Rows[e.NewEditIndex].FindControl("lbl_Proid") as Label;

                    DDL_Pro.SelectedValue = lbl_Proid.Text.Trim();
                }
            }
            else
            {
                FillGrid();

                DropDownList DDL_Pro = GVSrchCust.Rows[e.NewEditIndex].FindControl("DDL_Pro") as DropDownList;
                Label lbl_Proid = GVSrchCust.Rows[e.NewEditIndex].FindControl("lbl_Proid") as Label;

                DDL_Pro.SelectedValue = lbl_Proid.Text.Trim();
            }
        }

        protected void GVSrchCust_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                //Finding the controls from Gridview for the row which is going to update  
                string dstkqty = "";
                string lbitmqty = "";

                string id = GVSrchCust.DataKeys[e.RowIndex].Values[0].ToString();
                Label lbl_Proid = GVSrchCust.Rows[e.RowIndex].FindControl("lbl_Proid") as Label;
                DropDownList DDL_Pro = GVSrchCust.Rows[e.RowIndex].FindControl("DDL_Pro") as DropDownList;

                TextBox txt_rat = GVSrchCust.Rows[e.RowIndex].FindControl("txt_rat") as TextBox;
                TextBox txt_qty = GVSrchCust.Rows[e.RowIndex].FindControl("txt_qty") as TextBox;
                TextBox tb_size = GVSrchCust.Rows[e.RowIndex].FindControl("tb_size") as TextBox;

                string ans = " select Dstk_Qty, ProductID, Dstk_unt from tbl_Dstk where Dstk_id='" + id + "' and ProductID = '" + DDL_Pro.SelectedValue.Trim() + "' and tbl_Dstk.CompanyId='" + Session["CompanyID"] + "' and tbl_Dstk.BranchId='" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(ans);
                if (dt_.Rows.Count > 0)
                {
                    dstkqty = dt_.Rows[0]["Dstk_Qty"].ToString();
                }

                con.Open();

                if (Convert.ToDecimal(dstkqty) > Convert.ToDecimal(txt_qty.Text.Trim()))
                {
                    lbitmqty = (Convert.ToDecimal(dstkqty) - Convert.ToDecimal(txt_qty.Text.Trim())).ToString();

                    if (lbitmqty == "0")
                    {
                        lbitmqty = txt_qty.Text.Trim();
                    }

                    lbitmqty = (Convert.ToDecimal(dstkqty) - Convert.ToDecimal(lbitmqty.Trim())).ToString();


                    //updating the record  
                    query = " update tbl_Dstk set ProductID = '" + DDL_Pro.SelectedValue.Trim()
                        + "', Dstk_rat = '" + txt_rat.Text.Trim() + "', Dstk_Qty = '" + lbitmqty.Trim() + "', Dstk_unt = '" + tb_size.Text.Trim() + "' " +
                        " where Dstk_id = '" + id + "' and tbl_Dstk.CompanyId='" + Session["CompanyID"] + "' and tbl_Dstk.BranchId='" + Session["BranchID"] + "'";

                }
                else if (Convert.ToDecimal(dstkqty) < Convert.ToDecimal(txt_qty.Text.Trim()))
                {
                    lbitmqty = (Convert.ToDecimal(txt_qty.Text.Trim()) - Convert.ToDecimal(dstkqty)).ToString();


                    if (lbitmqty == "0")
                    {
                        lbitmqty = txt_qty.Text.Trim();
                    }

                    lbitmqty = (Convert.ToDecimal(dstkqty) + Convert.ToDecimal(lbitmqty.Trim())).ToString();

                    //updating the record  
                    query = " update tbl_Dstk set ProductID = '" + DDL_Pro.SelectedValue.Trim()
                        + "', Dstk_rat = '" + txt_rat.Text.Trim() + "', Dstk_Qty = '" + lbitmqty.Trim() + "', Dstk_unt = '" + tb_size.Text.Trim() + "' " +
                        " where Dstk_id = '" + id + "' and tbl_Dstk.CompanyId='" + Session["CompanyID"] + "' and tbl_Dstk.BranchId='" + Session["BranchID"] + "'";

                }
                else
                {
                    query = " update tbl_Dstk set ProductID = '" + DDL_Pro.SelectedValue.Trim()
                    + "', Dstk_rat = '" + txt_rat.Text.Trim() + "', Dstk_Qty = '" + txt_qty.Text.Trim() + "', Dstk_unt = '" + tb_size.Text.Trim() + "' " +
                    " where Dstk_id = '" + id + "' and tbl_Dstk.CompanyId='" + Session["CompanyID"] + "' and tbl_Dstk.BranchId='" + Session["BranchID"] + "'";
                }

                //SqlCommand cmd = new SqlCommand("Update tbl_Dpos set Adv='" + txt_Adv.Text.Trim() + "',bal='" + txt_bal.Text.Trim() + "' where Mposid=" + Convert.ToInt32(id.Text), con);
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.ExecuteNonQuery();
                con.Close();

                //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
                GVSrchCust.EditIndex = -1;
                //Call ShowData method for displaying updated data
                if (ViewState["getnum"] != null)
                {
                    ViewState["getnum"] = null;
                    FillGrid();
                }
                else
                {
                    FillGrid();
                }
            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
            }
        }

        protected void GVSrchCust_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVSrchCust.EditIndex = -1;
            if (ViewState["getnum"] != null)
            {
                DataTable dt_ = (DataTable)ViewState["getnum"];
                if (dt_.Rows.Count > 0)
                {
                    GVSrchCust.DataSource = dt_;
                    GVSrchCust.DataBind();
                }
            }
            else
            {
                FillGrid();
            }
        }

        protected void txt_pro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow g1 in GVSrchCust.Rows)
                {
                    lbl_Proid = (g1.FindControl("lbl_Proid") as Label).Text;
                    txt_pro = (g1.FindControl("txt_pro") as TextBox).Text;

                    string qur = "select * from products where ProductName = '" + txt_pro + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                    dt_ = new DataTable();
                    dt_ = DBConnection.GetQueryData(qur);

                    if (dt_.Rows.Count > 0)
                    {
                        lbl_Proid = dt_.Rows[0]["ProductID"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
            }
        }

        protected void GVSrchCust_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVSrchCust.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void GVSrchCust_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    string v = " select distinct(tbl_Dstk.ProductID), ProductName  from tbl_Dstk inner join Products  " +
                    " on tbl_Dstk.ProductID = Products.ProductID where tbl_Dstk.CompanyId='" + Session["CompanyID"] + "' and tbl_Dstk.BranchId='" + Session["BranchID"] + "'";

                    dt_ = new DataTable();
                    dt_ = DBConnection.GetQueryData(v);
                    if (dt_.Rows.Count > 0)
                    {

                        DropDownList DDL_Pro = (DropDownList)e.Row.FindControl("DDL_Pro");
                        // bind DropDown manually
                        DDL_Pro.DataSource = dt_;
                        DDL_Pro.DataTextField = "ProductName";
                        DDL_Pro.DataValueField = "ProductID";
                        DDL_Pro.DataBind();
                        DDL_Pro.Items.Insert(0, new ListItem("--Select Product--", "0"));

                        //DataRowView dr = e.Row.DataItem as DataRowView;
                        //DDL_Pro.SelectedValue = value; // you can use e.Row.DataItem to get the value
                    }
                }
            }
        }
    }
}