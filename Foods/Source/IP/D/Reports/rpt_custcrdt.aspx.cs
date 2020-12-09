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
    public partial class rpt_custcrdt : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db = new DBConnection();
        string fdat, ldat, CreBKR, CreSal, CreCust, CreArea, CreBill;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {

                    fdat = Request.QueryString["FRMDat"];
                    ldat = Request.QueryString["TDat"];
                    CreBKR = Request.QueryString["CreBKR"];
                    CreSal = Request.QueryString["CreSal"];
                    CreCust = Request.QueryString["CreCust"];
                    CreArea = Request.QueryString["CreArea"];
                    CreBill = Request.QueryString["CreBill"];

                    if (CreCust != null && fdat != null && ldat != null)
                    {
                        get_custcre(CreCust, fdat, ldat);

                    }
                    else if (CreSal != null && fdat != null && ldat != null)
                    {
                        get_Supcre(CreSal, fdat, ldat);
                    }
                    else if (CreCust != null && fdat != null)
                    {
                        get_custcre(CreCust, fdat);
                    }
                    else if (CreSal != null && fdat != null)
                    {
                        get_Supcre(CreSal, fdat);
                    }
                    else if (CreCust != null && ldat != null)
                    {
                        get_custlcre(CreCust, ldat);
                    }
                    else if (CreSal != null && ldat != null)
                    {
                        get_suplcre(CreSal, ldat);
                    }
                    //if (fdat != null && ldat != null && CreBKR != null && CreSal != null && CreCust != "1" && CreArea != null && CreBill != null)
                    //{
                    //    get_custcre(fdat, ldat, CreBKR, CreSal, CreCust, CreArea, CreBill);

                    //}
                    //else if (fdat != null && ldat != null && CreBKR != null && CreSal != null && CreCust != "1" && CreArea != null)
                    //{
                    //    get_custcre(fdat, ldat, CreBKR, CreSal, CreCust, CreArea);

                    //}
                    //else if (fdat != null && ldat != null && CreBKR != null && CreSal != null && CreCust != "1")
                    //{
                    //    get_custcre(fdat, ldat, CreBKR, CreSal, CreCust);

                    //}
                    //else if (fdat != null && ldat != null && CreBKR != null && CreSal != null)
                    //{
                    //    get_custcre(fdat, ldat, CreBKR, CreSal);

                    //}
                    //else if (fdat != null && ldat != null && CreCust != null)
                    //{
                    //    //get_custcre(fdat, ldat, CreCust);

                    //}
                    //else if (fdat != null && ldat != null && CreBKR != null)
                    //{
                    //    get_custcre(fdat, ldat);

                    //}
                    //else if (fdat != null && ldat != null)
                    //{
                    //    get_custcre(fdat, ldat);

                    //}
                    //else if (CreBKR != null)
                    //{
                    //    get_custcre();
                    //}
                    //else if (CreSal != null)
                    //{
                    //    get_custcre();
                    //}
                    //else if (CreArea != null)
                    //{
                    //    get_custcre();
                    //}
                    //else if (CreBill != null)
                    //{
                    //    get_custcre();
                    //}
                    //else if (CreCust != "1")
                    //{
                    //    get_custcre();
                    //}

                    //else if (CreCust == "1")
                    //{
                    //    get_custcre();
                    //}
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }


        protected void LinkBtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "CustomerCreditList.xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);

                GV_CustCre.GridLines = GridLines.Both;
                GV_CustCre.HeaderStyle.Font.Bold = true;

                GV_CustCre.RenderControl(htmltextwrtter);

                Response.Write(strwritter.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                throw;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //lblalert.Text = ex.Message;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

        private void get_custcre(string crecust, string fdat, string ldat)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "	select * from v_credtcustwis where CustomerID='" + crecust + "' and  MPurDate between '" + fdat +
                    "' and '" + ldat + "' and CompanyId = '" + Session["CompanyID"] +
                    "' and BranchId= '" + Session["BranchID"] + "'";

                cmd.Connection = con;
                con.Open();

                DataTable dtvencre = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtvencre);

                if (dtvencre.Rows.Count > 0)
                {
                    lbl_Cust.Text = dtvencre.Rows[0]["CustomerName"].ToString();

                    GV_CustCre.DataSource = dtvencre;
                    GV_CustCre.DataBind();

                    //Get Total
                    double QGTotal = 0;

                    
                    //Cash Amount
                    for (int j = 0; j < GV_CustCre.Rows.Count; j++)
                    {
                        Label totalqty = (Label)GV_CustCre.Rows[j].FindControl("lbl_cshamt");
                        if (totalqty.Text != "")
                        {
                            QGTotal += Convert.ToDouble(totalqty.Text);
                        }
                    }
                    ttl_qty.Text = QGTotal.ToString();
                    lbl_ttl.Text = dtvencre.Rows[0]["CredAmt"].ToString();
                }
                con.Close();
            }
        }
        private void get_Supcre(string supcust, string fdat, string ldat)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "	select * from v_credtsupwis where PayAcc='" + supcust +
                    "' and  MPurDate between '" + fdat +
                    "' and '" + ldat + "' and CompanyId = '" + Session["CompanyID"] +
                    "' and BranchId= '" + Session["BranchID"] + "'";

                cmd.Connection = con;
                con.Open();

                DataTable dtvencre = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtvencre);

                if (dtvencre.Rows.Count > 0)
                {
                    lbl_Cust.Text = dtvencre.Rows[0]["suppliername"].ToString();

                    GV_CustCre.DataSource = dtvencre;
                    GV_CustCre.DataBind();

                    //Get Total
                    double QGTotal = 0;


                    //Cash Amount
                    for (int j = 0; j < GV_CustCre.Rows.Count; j++)
                    {
                        Label totalqty = (Label)GV_CustCre.Rows[j].FindControl("lbl_cshamt");
                        if (totalqty.Text != "")
                        {
                            QGTotal += Convert.ToDouble(totalqty.Text);
                        }
                    }
                    ttl_qty.Text = QGTotal.ToString();
                    lbl_ttl.Text = dtvencre.Rows[0]["CredAmt"].ToString();
                }
                con.Close();
            }
        }


        private void get_Supcre(string supcust, string fdat)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "	select * from v_credtsupwis where PayAcc='" + supcust +
                    "' and  MPurDate ='" + fdat + "' and CompanyId = '" + Session["CompanyID"] +
                    "' and BranchId= '" + Session["BranchID"] + "'";

                cmd.Connection = con;
                con.Open();

                DataTable dtvencre = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtvencre);

                if (dtvencre.Rows.Count > 0)
                {
                    lbl_Cust.Text = dtvencre.Rows[0]["suppliername"].ToString();

                    GV_CustCre.DataSource = dtvencre;
                    GV_CustCre.DataBind();

                    //Get Total
                    double QGTotal = 0;


                    //Cash Amount
                    for (int j = 0; j < GV_CustCre.Rows.Count; j++)
                    {
                        Label totalqty = (Label)GV_CustCre.Rows[j].FindControl("lbl_cshamt");
                        if (totalqty.Text != "")
                        {
                            QGTotal += Convert.ToDouble(totalqty.Text);
                        }
                    }
                    ttl_qty.Text = QGTotal.ToString();
                    lbl_ttl.Text = dtvencre.Rows[0]["CredAmt"].ToString();
                }
                con.Close();
            }
        }

        private void get_custcre(string crecust, string fdat)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "	select * from v_credtcustwis where CustomerID='" + crecust + "' and  MPurDate ='" + fdat +
                "' and CompanyId = '" + Session["CompanyID"] +
                    "' and BranchId= '" + Session["BranchID"] + "'";

                cmd.Connection = con;
                con.Open();

                DataTable dtvencre = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtvencre);

                if (dtvencre.Rows.Count > 0)
                {
                    lbl_Cust.Text = dtvencre.Rows[0]["CustomerName"].ToString();

                    GV_CustCre.DataSource = dtvencre;
                    GV_CustCre.DataBind();

                    //Get Total
                    double QGTotal = 0;


                    //Cash Amount
                    for (int j = 0; j < GV_CustCre.Rows.Count; j++)
                    {
                        Label totalqty = (Label)GV_CustCre.Rows[j].FindControl("lbl_cshamt");
                        if (totalqty.Text != "")
                        {
                            QGTotal += Convert.ToDouble(totalqty.Text);
                        }
                    }
                    ttl_qty.Text = QGTotal.ToString();
                    lbl_ttl.Text = dtvencre.Rows[0]["CredAmt"].ToString();
                }
                con.Close();
            }
        }

        private void get_suplcre(string supcust, string ldat)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "	select * from v_credtsupwis where PayAcc='" + supcust +
                    "' and  MPurDate = '" + ldat + "' and CompanyId = '" + Session["CompanyID"] +
                    "' and BranchId= '" + Session["BranchID"] + "'";

                cmd.Connection = con;
                con.Open();

                DataTable dtvencre = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtvencre);

                if (dtvencre.Rows.Count > 0)
                {
                    lbl_Cust.Text = dtvencre.Rows[0]["suppliername"].ToString();

                    GV_CustCre.DataSource = dtvencre;
                    GV_CustCre.DataBind();

                    //Get Total
                    double QGTotal = 0;


                    //Cash Amount
                    for (int j = 0; j < GV_CustCre.Rows.Count; j++)
                    {
                        Label totalqty = (Label)GV_CustCre.Rows[j].FindControl("lbl_cshamt");
                        if (totalqty.Text != "")
                        {
                            QGTotal += Convert.ToDouble(totalqty.Text);
                        }
                    }
                    ttl_qty.Text = QGTotal.ToString();
                    lbl_ttl.Text = dtvencre.Rows[0]["CredAmt"].ToString();
                }
                con.Close();
            }
        }
        private void get_custlcre(string crecust, string ldat)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "	select * from v_credtcustwis where CustomerID='" + crecust +
                    "' and  MPurDate = '" + ldat + "' and CompanyId = '" + Session["CompanyID"] +
                   "' and BranchId= '" + Session["BranchID"] + "'";

                cmd.Connection = con;
                con.Open();

                DataTable dtvencre = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtvencre);

                if (dtvencre.Rows.Count > 0)
                {
                    lbl_Cust.Text = dtvencre.Rows[0]["CustomerName"].ToString();

                    GV_CustCre.DataSource = dtvencre;
                    GV_CustCre.DataBind();

                    //Get Total
                    double QGTotal = 0;


                    //Cash Amount
                    for (int j = 0; j < GV_CustCre.Rows.Count; j++)
                    {
                        Label totalqty = (Label)GV_CustCre.Rows[j].FindControl("lbl_cshamt");
                        if (totalqty.Text != "")
                        {
                            QGTotal += Convert.ToDouble(totalqty.Text);
                        }
                    }
                    ttl_qty.Text = QGTotal.ToString();
                    lbl_ttl.Text = dtvencre.Rows[0]["CredAmt"].ToString();
                }
                con.Close();
            }
        }


        private void get_custcre()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                /*CreBKR = Request.QueryString["CreBKR"];
                    CreSal = Request.QueryString["CreSal"];
                    CreCust = Request.QueryString["CreCust"];
                    CreArea = Request.QueryString["CreArea"];
                    CreBill = Request.QueryString["CreBill"];*/
                if (CreCust == "1")
                {
                    cmd.CommandText = " select ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID],tbl_MSal.CustomerID, CustomerName,Customers_.areaid, area_, Booker,SalMan,MSal_sono, CONVERT(varchar, MSal_dat, 103) as [MPurDate], isnull((OutStanding + 0.01),0.01) as [OutStanding],(GTtl + 0.01) as [GTtl],  Amt,CredAmt, ([Recovery] + 0.01) as [Recovery],  Balance = CASE WHEN LEN((Outstanding - [Recovery])) > 0    THEN '0' ELSE (Outstanding - [Recovery]) END, '' as [ChqNo],    '' as [ChqAmt], tbl_MSal.CreatedAt, tbl_MSal.CreatedBy from tbl_MSal inner join Customers_ on tbl_MSal.CustomerID = Customers_.CustomerID inner join tbl_DSal on tbl_DSal.MSal_id = tbl_MSal.MSal_id inner join tbl_area on Customers_.areaid = tbl_area.areaid inner join tbl_Salcredit on Customers_.CustomerID = tbl_Salcredit.CustomerID  where  outstanding > 0.01 and tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' and tbl_MSal.BranchId= '" + Session["BranchID"] + "'";
                }
                else if (CreCust != null)
                {
                    cmd.CommandText = " select ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID],tbl_MSal.CustomerID, CustomerName,Customers_.areaid, area_, Booker,SalMan,MSal_sono, CONVERT(varchar, MSal_dat, 103) as [MPurDate], isnull((OutStanding + 0.01),0.01) as [OutStanding],(GTtl + 0.01) as [GTtl],  Amt,CredAmt, ([Recovery] + 0.01) as [Recovery],  Balance = CASE WHEN LEN((Outstanding - [Recovery])) > 0    THEN '0' ELSE (Outstanding - [Recovery]) END, '' as [ChqNo],    '' as [ChqAmt], tbl_MSal.CreatedAt, tbl_MSal.CreatedBy from tbl_MSal inner join Customers_ on tbl_MSal.CustomerID = Customers_.CustomerID inner join tbl_DSal on tbl_DSal.MSal_id = tbl_MSal.MSal_id inner join tbl_area on Customers_.areaid = tbl_area.areaid inner join tbl_Salcredit on Customers_.CustomerID = tbl_Salcredit.CustomerID where CustomerID = '" + CreCust + "' and  outstanding > 0.01 and tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' and tbl_MSal.BranchId= '" + Session["BranchID"] + "'";
                }
                else if (CreBKR != null)
                {
                    cmd.CommandText = " select ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID],tbl_MSal.CustomerID, CustomerName,Customers_.areaid, area_, Booker,SalMan,MSal_sono, CONVERT(varchar, MSal_dat, 103) as [MPurDate], isnull((OutStanding + 0.01),0.01) as [OutStanding],(GTtl + 0.01) as [GTtl],  Amt,CredAmt, ([Recovery] + 0.01) as [Recovery],  Balance = CASE WHEN LEN((Outstanding - [Recovery])) > 0    THEN '0' ELSE (Outstanding - [Recovery]) END, '' as [ChqNo],    '' as [ChqAmt], tbl_MSal.CreatedAt, tbl_MSal.CreatedBy from tbl_MSal inner join Customers_ on tbl_MSal.CustomerID = Customers_.CustomerID inner join tbl_DSal on tbl_DSal.MSal_id = tbl_MSal.MSal_id inner join tbl_area on Customers_.areaid = tbl_area.areaid inner join tbl_Salcredit on Customers_.CustomerID = tbl_Salcredit.CustomerID where Booker = '" + CreBKR + "' and  outstanding > 0.01 and tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' and tbl_MSal.BranchId= '" + Session["BranchID"] + "'";
                }
                else if (CreSal != null)
                {
                    cmd.CommandText = " select ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID],tbl_MSal.CustomerID, CustomerName,Customers_.areaid, area_, Booker,SalMan,MSal_sono, CONVERT(varchar, MSal_dat, 103) as [MPurDate], isnull((OutStanding + 0.01),0.01) as [OutStanding],(GTtl + 0.01) as [GTtl],  Amt,CredAmt, ([Recovery] + 0.01) as [Recovery],  Balance = CASE WHEN LEN((Outstanding - [Recovery])) > 0    THEN '0' ELSE (Outstanding - [Recovery]) END, '' as [ChqNo],    '' as [ChqAmt], tbl_MSal.CreatedAt, tbl_MSal.CreatedBy from tbl_MSal inner join Customers_ on tbl_MSal.CustomerID = Customers_.CustomerID inner join tbl_DSal on tbl_DSal.MSal_id = tbl_MSal.MSal_id inner join tbl_area on Customers_.areaid = tbl_area.areaid inner join tbl_Salcredit on Customers_.CustomerID = tbl_Salcredit.CustomerID where SalMan = '" + CreSal + "' and  outstanding > 0.01 and tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' and tbl_MSal.BranchId= '" + Session["BranchID"] + "'";
                }
                else if (CreArea != null)
                {
                    cmd.CommandText = " select ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID],tbl_MSal.CustomerID, CustomerName,Customers_.areaid, area_, Booker,SalMan,MSal_sono, CONVERT(varchar, MSal_dat, 103) as [MPurDate], isnull((OutStanding + 0.01),0.01) as [OutStanding],(GTtl + 0.01) as [GTtl],  Amt,CredAmt, ([Recovery] + 0.01) as [Recovery],  Balance = CASE WHEN LEN((Outstanding - [Recovery])) > 0    THEN '0' ELSE (Outstanding - [Recovery]) END, '' as [ChqNo],    '' as [ChqAmt], tbl_MSal.CreatedAt, tbl_MSal.CreatedBy from tbl_MSal inner join Customers_ on tbl_MSal.CustomerID = Customers_.CustomerID inner join tbl_DSal on tbl_DSal.MSal_id = tbl_MSal.MSal_id inner join tbl_area on Customers_.areaid = tbl_area.areaid inner join tbl_Salcredit on Customers_.CustomerID = tbl_Salcredit.CustomerID where Customers_.areaid = '" + CreArea + "' and  outstanding > 0.01 and tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' and tbl_MSal.BranchId= '" + Session["BranchID"] + "'";
                }
                else if (CreBill != null)
                {
                    cmd.CommandText = " select ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID],tbl_MSal.CustomerID, CustomerName,Customers_.areaid, area_, Booker,SalMan,MSal_sono, CONVERT(varchar, MSal_dat, 103) as [MPurDate], isnull((OutStanding + 0.01),0.01) as [OutStanding],(GTtl + 0.01) as [GTtl],  Amt,CredAmt, ([Recovery] + 0.01) as [Recovery],  Balance = CASE WHEN LEN((Outstanding - [Recovery])) > 0    THEN '0' ELSE (Outstanding - [Recovery]) END, '' as [ChqNo],    '' as [ChqAmt], tbl_MSal.CreatedAt, tbl_MSal.CreatedBy from tbl_MSal inner join Customers_ on tbl_MSal.CustomerID = Customers_.CustomerID inner join tbl_DSal on tbl_DSal.MSal_id = tbl_MSal.MSal_id inner join tbl_area on Customers_.areaid = tbl_area.areaid inner join tbl_Salcredit on Customers_.CustomerID = tbl_Salcredit.CustomerID where MSal_sono = '" + CreBill + "' and  outstanding > 0.01 and tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' and tbl_MSal.BranchId= '" + Session["BranchID"] + "'";
                }
                cmd.Connection = con;
                con.Open();

                DataTable dtvencre = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtvencre);

                if (dtvencre.Rows.Count > 0)
                {
                    if (CreCust != "1")
                    {
                        lbl_Cust.Text = dtvencre.Rows[0]["CustomerName"].ToString();
                    }
                    else if (CreCust == "1")
                    {
                        lbl_Cust.Text = "All Customers"; 
                    }
                    GV_CustCre.DataSource = dtvencre;
                    GV_CustCre.DataBind();

                    //Get Total
                    double QGTotal = 0;


                    //Cash Amount
                    for (int j = 0; j < GV_CustCre.Rows.Count; j++)
                    {
                        Label totalqty = (Label)GV_CustCre.Rows[j].FindControl("lbl_cshamt");
                        if (totalqty.Text != "")
                        {
                            QGTotal += Convert.ToDouble(totalqty.Text);
                        }
                    }
                    ttl_qty.Text = QGTotal.ToString();
                    lbl_ttl.Text = dtvencre.Rows[0]["CredAmt"].ToString();
                }
                con.Close();
            }
        }

/*        private void get_custcre()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select * from v_credtcustwis";
                cmd.Connection = con;
                con.Open();

                DataTable dtvencre = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtvencre);

                if (dtvencre.Rows.Count > 0)
                {
                    lbl_Cust.Text = dtvencre.Rows[0]["CustomerName"].ToString();

                    GV_CustCre.DataSource = dtvencre;
                    GV_CustCre.DataBind();
                    //Get Total

                    double GTotal = 0;
                    double QGTotal = 0;

                    // Out Standing
                    for (int j = 0; j < GV_CustCre.Rows.Count; j++)
                    {
                        Label total = (Label)GV_CustCre.Rows[j].FindControl("lbl_outstand");
                        if (total.Text != "")
                        {
                            GTotal += Convert.ToDouble(total.Text);
                        }
                    }

                    //Cash Amount
                    for (int j = 0; j < GV_CustCre.Rows.Count; j++)
                    {
                        Label totalqty = (Label)GV_CustCre.Rows[j].FindControl("lbl_cshamt");
                        if (totalqty.Text != "")
                        {
                            QGTotal += Convert.ToDouble(totalqty.Text);
                        }
                    }
                    ttl_qty.Text = QGTotal.ToString();
                    lbl_ttl.Text = GTotal.ToString();
                }
                con.Close();
            }
        }*/
    }
}