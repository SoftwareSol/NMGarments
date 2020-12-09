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
        string posid, cust, query;
        float GTotal;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {

                    //lbl_Comp.Text = Session["Company"].ToString();
                    lbl_dattim.Text = DateTime.Now.ToString();
                    lbl_usr.Text = Session["user"].ToString();
                    //lbl_add.Text = "Shop # 2 Opposite Rafah-e-aam Post Office Malir Halt, Karachi";
                    //lbl_no.Text = "0321-2010080";
                    posid = Request.QueryString["POSID"];
                    cust = Request.QueryString["cust"];
                    lblprebal.Text = "0";
                    get_posal(posid);
                    getbranchData();
                    bill_no.Text = posid;
                    //Imglogo.ImageUrl = "~/img/blank-img.jpg";
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        private void getbranchData() 
        {
            try
            {
                query = "select * from tbl_branches where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    Imglogo.ImageUrl = "~/img/branchlogo/" + dt_.Rows[0]["logo"].ToString();
                    lbl_Add.Text = dt_.Rows[0]["Address"].ToString();
                    lbl_Ph.Text = dt_.Rows[0]["TelephoneNo"].ToString();
                    lbl_Mob.Text = dt_.Rows[0]["MobileNo"].ToString();
                    lbl_Terminal.Text = dt_.Rows[0]["Name"].ToString();
                    lbl_usr.Text = dt_.Rows[0]["ContactPerson"].ToString();

                    if (cust == "00118")
                    {
                        cust = "Walk-In";
                        lbl_servto.Text = cust;
                        lblprebal.Text = "0";
                    }
                    else if (cust != "00118")
                    {
                        query = "select * from SubHeadCategories where SubHeadCategoriesGeneratedID='" + cust + "'";

                        dt_ = new DataTable();
                        dt_ = DBConnection.GetQueryData(query);

                        if (dt_.Rows.Count > 0)
                        {
                            lbl_servto.Text = dt_.Rows[0]["SubHeadCategoriesName"].ToString();
                        }
                    }
                }
                else
                {
                    lbl_servto.Text = cust;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void get_posal(string POSID)
        {
            try
            {
                query = "select * from tbl_salcredit where CustomerID = '" + cust + "'";
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    lblprebal.Text = dt_.Rows[0]["CredAmt"].ToString();
                    cust = dt_.Rows[0]["CustomerID"].ToString();

                    if (cust == "00118")
                    {
                        cust = "Walk-In";
                        lbl_servto.Text = cust;
                        lblprebal.Text = "0";
                    }
                    else if (cust != "00118")
                    {
                        query = "select * from SubHeadCategories where SubHeadCategoriesGeneratedID='" + cust + "'";

                        dt_ = new DataTable();
                        dt_ = DBConnection.GetQueryData(query);

                        if (dt_.Rows.Count > 0)
                        {
                            lbl_servto.Text = dt_.Rows[0]["SubHeadCategoriesName"].ToString();
                        }
                    }
                }
                else
                {
                    lbl_servto.Text = cust;
                }


                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "  select tbl_mcpos.BillNO,ProductName as [Description],ProQty as [Qty],salprice as [Rate],disc, " +
                        " Amt as [Amount],Adv, Ttl, ABS(tbl_DCPos.bal) AS [bal]  from tbl_mcpos  inner join tbl_DCPos on tbl_mcpos.MCposid = tbl_DCPos.MCposid " +
                        " inner join Products on tbl_DCPos.ProductID = Products.ProductID where tbl_mcpos.BillNO = '" + POSID + "' and tbl_mcpos.CompanyId='" +
                        Session["CompanyID"] + "' and tbl_mcpos.BranchId='" + Session["BranchID"] + "'";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtpos = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtpos);
                    if (dtpos.Rows.Count > 0)
                    {
                        GVPOS.DataSource = dtpos;
                        GVPOS.DataBind();

                        //Get Total

                        double GTotal = 0;
                        double QGTotal = 0;
                        // Total
                        for (int j = 0; j < GVPOS.Rows.Count; j++)
                        {
                            Label total = (Label)GVPOS.Rows[j].FindControl("lbl_amt");
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

                        string adv = dtpos.Rows[0]["Adv"].ToString();
                        string disc = dtpos.Rows[0]["disc"].ToString();

                        //if (adv == "0")
                        //{
                        //    lbl_cshrec.Text = dtpos.Rows[0]["Ttl"].ToString();
                        //}
                        //else if (adv != "0")
                        //{
                        lbl_cshrec.Text = dtpos.Rows[0]["Adv"].ToString();
                        //}
                       

                        #region Real Logic of Discount
                            //if (disc != "0")
                            //{
                            //    lbl_dscamt.Text = (Convert.ToDecimal(lbl_netamt.Text.Trim()) * (Convert.ToDecimal(disc) / 100)).ToString();
                            //}
                            //else
                            //{
                            //    lbl_dscamt.Text = "0";
                            //}
                        #endregion

                        #region For NMGarments

                            lbl_dscamt.Text = disc.Trim();

                        #endregion
                        lbl_grssamt.Text = (Convert.ToDecimal(lbl_netamt.Text.Trim()) - Convert.ToDecimal(lbl_dscamt.Text)).ToString();

                        lbl_bal.Text = dtpos.Rows[0]["bal"].ToString();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GVPOS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_qty = (Label)e.Row.FindControl("lbl_qty");

                GTotal += Convert.ToSingle(lbl_qty.Text);

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_ttlqry = (Label)e.Row.FindControl("lbl_ttlqry");

                lbl_ttlqry.Text = GTotal.ToString();
            }
        }

        //private void get_posal(string POSID)
        //{
        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.CommandText = "  select tbl_mcpos.BillNO,ProductName as [Description],ProQty as [Qty],salprice as [Rate], " +
        //                " Amt as [Amount],Adv, Ttl, tbl_DCPos.bal  from tbl_mcpos  inner join tbl_DCPos on tbl_mcpos.MCposid = tbl_DCPos.MCposid " +
        //                " inner join Products on tbl_DCPos.ProductID = Products.ProductID where tbl_mcpos.BillNO = '" + POSID + "' and tbl_mcpos.CompanyId='" +
        //                Session["CompanyID"] + "' and tbl_mcpos.BranchId='" + Session["BranchID"] + "'";

        //            cmd.Connection = con;
        //            con.Open();

        //            DataTable dtpos = new DataTable();
        //            SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //            adp.Fill(dtpos);
        //            if (dtpos.Rows.Count > 0)
        //            {

        //                GVPOS.DataSource = dtpos;
        //                GVPOS.DataBind();

        //                //Get Total

        //                double GTotal = 0;
        //                double QGTotal = 0;
        //                // Total
        //                for (int j = 0; j < GVPOS.Rows.Count; j++)
        //                {
        //                    Label total = (Label)GVPOS.Rows[j].FindControl("lbl_amt");
        //                    GTotal += Convert.ToDouble(total.Text);
        //                }

        //                //Quantity
        //                for (int j = 0; j < GVPOS.Rows.Count; j++)
        //                {
        //                    Label totalqty = (Label)GVPOS.Rows[j].FindControl("lbl_qty");
        //                    QGTotal += Convert.ToDouble(totalqty.Text);
        //                }

        //                lblitmcnt.Text = dtpos.Compute("count(" + dtpos.Columns[1].ColumnName + ")", null).ToString(); 
        //                lbl_netamt.Text = GTotal.ToString();

        //                string adv = dtpos.Rows[0]["Adv"].ToString();
        //                //
        //                if (adv == "0")
        //                { 
        //                    lbl_cshrec.Text = dtpos.Rows[0]["Ttl"].ToString(); ;
        //                }
        //                else if (adv != "0")
        //                {
        //                    lbl_cshrec.Text = dtpos.Rows[0]["Adv"].ToString(); ;
        //                }

        //                lbl_bal.Text = dtpos.Rows[0]["bal"].ToString(); ;
        //            }
        //            con.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}