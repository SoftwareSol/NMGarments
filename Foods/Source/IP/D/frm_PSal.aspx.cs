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
    public partial class frm_PSal : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_ = null;
        DBConnection db = new DBConnection();
        string TBItms, tbItmpris, TBUnit, TBItmQty, lblcat, lblttl, HFDSal, tbbal, tbadv, DDL_PROTYPID,
            proid, tbsalpris, tbfitpric, str, stkqty, acctitle, accno, query, custid, UID;
        public static string branch, company; 
        string openingbal = "0";
        string bill = "";
        string prodtid = "";
        string salqty = "";
        string salrate = "";
        decimal avapre;
        decimal ttlcre;
        decimal openbal;
        int i;
        
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {
                    try
                    {   //string UID = Request.QueryString["UID"];
                        branch = Session["BranchID"].ToString();
                        company = Session["CompanyID"].ToString();
                        Session["chek"] = "0";
                        lbl_BillNo.Text = "";
                        lbl_usr.Text = Session["Name"].ToString();
                        UID = Session["Name"].ToString();  
                        ShowAccount();
                        lnkbtn_del.Visible = false;
                        lbl_dat.Text = DateTime.Now.ToShortDateString();
                        SetInitRowPuritm();
                        data();
                        loaditm();
                        lblmssg.Text = "";
                        lb_custtyp.Text = "0";
                        lb_custtyp.Visible = false;
                        pnl_extra.Visible = true;
                        TBCust.Text = "walk-in";
                        TextBox TBItms = (TextBox)GV_POS.Rows[0].Cells[0].FindControl("TBItms");
                        TBItms.Focus();
                        AutoID();
                        btn_POScancl.Visible = false;
                        lbl_Acc.Text = "00118";
                        OpeningBal();
                        lbl_openbalance1.Visible = false;
                        lbl_Openbalance.Visible = false;
                        TBREC.Text = UID;
                        TBREC.Enabled = false;
                        TBREC.Width = 50;
                        TBCust.Width = 100;
                        getunts();
                        for (int i = 0; i < GV_POS.Rows.Count; i++)
                        {
                            DropDownList ddlUnit = (DropDownList)GV_POS.Rows[i].Cells[1].FindControl("ddlUnit");
                            TextBox TBUnit = (TextBox)GV_POS.Rows[i].Cells[1].FindControl("TBUnit");
                            ddlUnit.Width = 100;
                            TBUnit.Visible = false;
                        }
                    }

                    catch { Response.Redirect("~/Login.aspx"); }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void OpeningBal()
        {
            try
            {
                string currbal;
                string dat;
                string OpenBal;

                //query = " select * from v_cshbook where expensesdat ='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
                //query = " select * from v_cshbook where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                //dt_ = new DataTable();
                //dt_ = DBConnection.GetQueryData(query);

                //if (dt_.Rows.Count > 0)
                //{
                 DataTable dt_ = new DataTable();
                 using (var cmd = new SqlCommand("sp_cshbook", con))
                 {
                     //cmd.CommandText = " select * from v_rptpur where MPurID='" + PurId + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                     using (var da = new SqlDataAdapter(cmd))
                     {
                         cmd.CommandText = "sp_cshbook";
                         cmd.CommandType = CommandType.StoredProcedure;
                         cmd.Parameters.AddWithValue("@companyid", Session["CompanyID"].ToString());
                         cmd.Parameters.AddWithValue("@branchid", Session["BranchID"].ToString());
                         da.Fill(dt_);
                     }

                     if (dt_.Rows.Count > 0)
                     {

                         for (int i = 0; i < dt_.Rows.Count; i++)
                         {
                             dat = dt_.Rows[i]["dat"].ToString();

                             if (dat == DateTime.Now.ToString("dd-MM-yyyy"))
                             {
                                 lbl_Openbalance.Text = dt_.Rows[i]["opening_balance"].ToString();
                             }
                             else
                             {
                                 lbl_Openbalance.Text = dt_.Rows[i]["OpenBal"].ToString();
                                 lbl_openbalance1.Text = dt_.Rows[i]["OpenBal"].ToString();
                             }
                             lbl_openbalance1.Text = dt_.Rows[i]["OpenBal"].ToString();

                         }
                     }
                 }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        public void ShowAccount()
        {
            try
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                //string str = "select count(MCposid) as [MCposid],BillNO from tbl_MCPos  where CompanyId='" + Session["CompanyID"] +
                  // "' and BranchId='" + Session["BranchID"] + "'  order by MCposid desc";
                string str = " select BillNO as [MCposid] from tbl_MCPos  where CompanyId='" + Session["CompanyID"] +
                  "' and BranchId='" + Session["BranchID"] + "'";
                // and Ishold <> '1'
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                DataTable dt_ = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt_);
                if (dt_.Rows.Count <= 0)
                {
                    lbl_BillNo.Text = "1";
                }
                else
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (lbl_BillNo.Text == "")
                        {
                            int v = Convert.ToInt32(reader["MCposid"].ToString());
                            //int mcid = Convert.ToInt32(reader["MCposid"].ToString());

                            int b = v + 1;

                            lbl_BillNo.Text = b.ToString();
                            int id = Convert.ToInt32(lbl_BillNo.Text.Trim());

                            if (v == id && v != 0)
                            {
                                b = b + 1;
                                lbl_BillNo.Text = b.ToString();
                            }
                        }
                        else
                        {
                            int v = Convert.ToInt32(reader["MCposid"].ToString());
                            int b = v + 1;
    
                            lbl_BillNo.Text = b.ToString();
                            int id = Convert.ToInt32(lbl_BillNo.Text.Trim());

                            if (v == id && v != 0)
                            {
                                b = b + 1;
                                lbl_BillNo.Text = b.ToString();
                            }
                        }
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> Getunts(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select untnam from tbl_unts where untnam like '%" + prefixText + "%' and CompanyId = '" + company + "' and BranchId='"+ branch +"'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }

        private void loaditm()
        {
            //str = " select * FROM Products ";
            str = " select distinct(tbl_Dstk.ProductID) ,ProductName, Dstk_rat, Dstk_Qty, Dstk_unt, RetalPrice from Products " +
               "   inner join tbl_Dstk on Products.ProductID = tbl_Dstk.ProductID where Dstk_Qty <> 0 and Products.CompanyId = '" + company + "' and Products.BranchId='" + branch + "'";

            dt_ = new DataTable();
            dt_ = DBConnection.GetQueryData(str);

            if (dt_.Rows.Count > 0)
            {
                GVRemanItms.DataSource = dt_;
                GVRemanItms.DataBind();
            }


        }
        private void data()
        {
            try
            {
                str = "  select distinct(tbl_mcpos.BillNO),billdat,CustomerName from tbl_mcpos  " +
                    "  inner join tbl_dcpos on tbl_mcpos.MCposid = tbl_dcpos.MCposid where " +
                    "  Iscancel <> '1' and Ishold = '1' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(str);

                if (dt_.Rows.Count > 0)
                {
                    GVHoldList.DataSource = dt_;
                    GVHoldList.DataBind();
                }
                else
                {
                    GVHoldList.DataSource = null;
                    GVHoldList.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }
        public int RandomNumber()
        {
            int min;
            int max;
            min = 0;
            max = 10000;
            Random random = new Random();
            return random.Next(min, max);
        }

        protected void TBUnit_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < GV_POS.Rows.Count; i++)
            {
                TextBox TBUnit = (TextBox)GV_POS.Rows[i].Cells[5].FindControl("TBUnit");
                LinkButton lnkbtnadd = (LinkButton)GV_POS.Rows[i].Cells[5].FindControl("lnkbtnadd");
                
                lnkbtnadd.Focus();
            }
        }

        private void SetInitRowPuritm()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Items", typeof(string)));
            dt.Columns.Add(new DataColumn("ItemDesc", typeof(string)));
            dt.Columns.Add(new DataColumn("ProductID", typeof(string)));
            dt.Columns.Add(new DataColumn("salpric", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));
            dt.Columns.Add(new DataColumn("UNIT", typeof(string)));            
            dt.Columns.Add(new DataColumn("TTL", typeof(string)));
            dt.Columns.Add(new DataColumn("Dposid", typeof(string)));
            
            dr = dt.NewRow();

            dr["Items"] = string.Empty;
            dr["ItemDesc"] = string.Empty;
            dr["ProductID"] = "0";
            dr["salpric"] = "0.00";
            dr["QTY"] = "0";
            dr["UNIT"] = "0";            
            dr["TTL"] = "0.00";
            dr["Dposid"] = "0";
            
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["dt_adItm"] = dt;

            GV_POS.DataSource = dt;
            GV_POS.DataBind();
        }

        protected void linkbtnadd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }
        private void getunts()
        {
            query = " select Products.ProductID,productname,Dstk_unt from Products  inner join tbl_Dstk on Products.ProductID= tbl_Dstk.ProductID where Dstk_Qty <> 0 and Products.CompanyId='" + Session["CompanyID"] + "' and Products.BranchId='" + Session["BranchID"] + "'";
            DataTable dtgetunts_ = new DataTable();
            dtgetunts_ = new DataTable();
            dtgetunts_ = DBConnection.GetQueryData(query);
            if (dtgetunts_.Rows.Count > 0)
            {
                for (int i = 0; i < GV_POS.Rows.Count; i++)
                {
                    DropDownList ddlUnit = (DropDownList)GV_POS.Rows[i].Cells[1].FindControl("ddlUnit");
                    ddlUnit.DataSource = dtgetunts_;
                    ddlUnit.DataTextField = "Dstk_unt";
                    ddlUnit.DataValueField = "Dstk_unt";
                    ddlUnit.DataBind();
                    ddlUnit.Items.Insert(0, new ListItem("--Select Units--", "0"));
                }
            }
        }
        private void getunts(string proname)
        {
            query = " select Products.ProductID,productname,Dstk_unt from Products  inner join tbl_Dstk on Products.ProductID= tbl_Dstk.ProductID   " +
                " where Pro_Code = '" + proname + "' and Dstk_Qty <> 0 and Products.CompanyId='" + Session["CompanyID"] + "' and Products.BranchId='" + Session["BranchID"] + "'";
            DataTable dtunts_ = new DataTable();
            dtunts_ = new DataTable();
            dtunts_ = DBConnection.GetQueryData(query);

            if (dtunts_.Rows.Count > 0)
            {
                for (int i = 0; i < GV_POS.Rows.Count; i++)
                {
                    DropDownList ddlUnit = (DropDownList)GV_POS.Rows[i].Cells[1].FindControl("ddlUnit");

                    if (ddlUnit.SelectedValue == "0")
                    {
                        ddlUnit.DataSource = dtunts_;
                        ddlUnit.DataTextField = "Dstk_unt";
                        ddlUnit.DataValueField = "Dstk_unt";
                        ddlUnit.DataBind();
                        ddlUnit.Items.Insert(0, new ListItem("--Select Units--", "0"));

                    }
                }
            }
        }
        private void AddNewRow()
        {
            int rowIndex = 0;

            if (ViewState["dt_adItm"] != null)
            {
                DataTable dt = (DataTable)ViewState["dt_adItm"];
                DataRow drRow = null;
                Label lblttl = null;
                Label lblchk_unt= null;

                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        //extract the TextBox value
                        TextBox TBItms = (TextBox)GV_POS.Rows[rowIndex].Cells[0].FindControl("TBItms");
                        TextBox TBItmdesc = (TextBox)GV_POS.Rows[rowIndex].Cells[0].FindControl("TBItmdesc");                        
                        TextBox tbsalpris = (TextBox)GV_POS.Rows[rowIndex].Cells[1].FindControl("tbsalpris");
                        TextBox TBItmQty = (TextBox)GV_POS.Rows[rowIndex].Cells[2].FindControl("TBItmQty");
                        TextBox TBUnit = (TextBox)GV_POS.Rows[rowIndex].Cells[3].FindControl("TBUnit");
                        lblchk_unt = (Label)GV_POS.Rows[rowIndex].Cells[3].FindControl("lblchk_unt");                        
                        lblttl = (Label)GV_POS.Rows[rowIndex].Cells[4].FindControl("lblttl");
                        HiddenField HFDSal = (HiddenField)GV_POS.Rows[rowIndex].Cells[5].FindControl("HFDSal");
                        HiddenField HFPROID = (HiddenField)GV_POS.Rows[rowIndex].Cells[5].FindControl("HFPROID");
                        
                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["Items"] = TBItms.Text;
                        dt.Rows[i - 1]["ItemDesc"] = TBItmdesc.Text;
                        dt.Rows[i - 1]["salpric"] = tbsalpris.Text;
                        dt.Rows[i - 1]["QTY"] = TBItmQty.Text;
                        dt.Rows[i - 1]["UNIT"] = TBUnit.Text;
                        dt.Rows[i - 1]["TTL"] = lblttl.Text;

                        if (HFDSal.Value == null || HFDSal.Value == "")
                        {
                            dt.Rows[i - 1]["Dposid"] = 0;
                        }
                        else
                        {
                            dt.Rows[i - 1]["Dposid"] = HFDSal.Value;
                        }

                        dt.Rows[i - 1]["ProductID"] = HFPROID.Value;
                        
                        rowIndex++;                       
                    }

                    // for total 
                    float GTotal = 0;
                    for (int j = 0; j < GV_POS.Rows.Count; j++)
                    {
                        Label total = (Label)GV_POS.Rows[j].FindControl("lblttl");
                        GTotal += Convert.ToSingle(total.Text);

                    }
                    TBTtl.Text = GTotal.ToString();
                    lblttl.Text = GTotal.ToString();

                    // for total qty
                    float GTQty = 0;
                    for (int j = 0; j < GV_POS.Rows.Count; j++)
                    {
                        TextBox total = (TextBox)GV_POS.Rows[j].FindControl("TBItmQty");
                        GTQty += Convert.ToSingle(total.Text);

                    }
                    lbl_ttlqty.Text = GTQty.ToString();

                    TextBox TBItm = null;
                    for (int j = 0; j < GV_POS.Rows.Count; j++)
                    {
                        TBItm = (TextBox)GV_POS.Rows[j].FindControl("TBItms");
                    }
                    TBItm.Focus();
                    

                    dt.Rows.Add(drRow);
                    ViewState["dt_adItm"] = dt;

                    GV_POS.DataSource = dt;
                    GV_POS.DataBind();

                    
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            
            SetPreRowitm();
        }

        protected void lnkbtn_del_Click(object sender, EventArgs e)
        {
            try
            {
                int del = 0;
                del = DelCust();

                if (del == 1)
                {
                    lblmssg.Text = "Customer Has been Deleted!";
                    cusclear();
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }
        protected void TBItmQty_TextChanged(object sender, EventArgs e)
        {
            decimal stkqty = 0;
            decimal avaqty = 0;
            LinkButton lnkbtnadd = null;
            TextBox TBUnit = null;
            DropDownList ddlUnit = null;
            try
            {
                for (int j = 0; j < GV_POS.Rows.Count; j++)
                {
                    TextBox TBItms = (TextBox)GV_POS.Rows[j].FindControl("TBItms");
                    TextBox TBItmQty = (TextBox)GV_POS.Rows[j].FindControl("TBItmQty");
                    ddlUnit = (DropDownList)GV_POS.Rows[j].FindControl("ddlUnit");
                    TBUnit = (TextBox)GV_POS.Rows[j].FindControl("TBUnit");
                    TextBox tbsalpris = (TextBox)GV_POS.Rows[j].FindControl("tbsalpris");
                    Label lblttl = (Label)GV_POS.Rows[j].FindControl("lblttl");
                    Label lblchkqty = (Label)GV_POS.Rows[j].FindControl("lblchkqty");
                    Label lbl_Flag = (Label)GV_POS.Rows[j].FindControl("lbl_Flag");
                    lnkbtnadd = (LinkButton)GV_POS.Rows[j].FindControl("lnkbtnadd");
                    HiddenField HFPROID = (HiddenField)GV_POS.Rows[j].FindControl("HFPROID");

                    query = " select Dstk_Qty from tbl_Dstk " +
                        " inner join  products on tbl_Dstk.ProductID = Products.ProductID " +
                        " where Pro_Code='" + TBItms.Text.Trim() + "' and Dstk_unt = '" + ddlUnit.SelectedValue.Trim() + "' and Dstk_Qty <> 0 and Products.CompanyId='" + Session["CompanyID"] + "' and Products.BranchId='" + Session["BranchID"] + "'";
                    DataTable dtqty_ = new DataTable(); //TBUnit .Text.Trim()
                     dtqty_ = DBConnection.GetQueryData(query);

                     if (dtqty_.Rows.Count > 0)
                    {
                        avaqty = Convert.ToDecimal(dtqty_.Rows[0]["Dstk_Qty"]);
                        stkqty = Convert.ToDecimal(TBItmQty.Text.Trim());

                        if (avaqty < stkqty)
                        {
                            lblchkqty.Text = "Given Quantity must not greater than available quantity";
                            //TBItmdesc.Enabled = false;
                            TBItms.Enabled = false;
                            tbsalpris.Enabled = false;
                            lnkbtnadd.Enabled = false;
                            TBUnit.Enabled = false;
                            TBItmQty.Focus();
                            TBItmQty.Attributes.Add("onfocusin", "select();");
                            chk_tax1.Checked = false;
                            chk_tax2.Checked = false;
                            TBTaxPer.Text = "";
                            TBOthChrgs.Text = "";
                            TBAdvance.Text = "0";
                            TBBalance.Text = "0";
                        }
                        else
                        {
                            if (TBItms.Text == "")
                            {
                                lbl_Flag.Text = "0";
                            }

                            lblchkqty.Text = "";
                            lnkbtnadd.Enabled = true;
                            TBItms.Enabled = true;
                            tbsalpris.Enabled = true;
                            TBUnit.Enabled = true;
                            lblttl.Text = (Convert.ToDouble(TBItmQty.Text.Trim()) * Convert.ToDouble(tbsalpris.Text.Trim())).ToString();
                            TBTaxPer.Text = "";
                            TBOthChrgs.Text = "";
                            chk_tax1.Checked = false;
                            chk_tax2.Checked = false;
                            TBAdvance.Text = "0";
                            TBBalance.Text = "0";

                        }
                    }
                }

                // for total 
                decimal GTotal = 0;
                decimal GTQty = 0;

                for (int t = 0; t < GV_POS.Rows.Count; t++)
                {
                    Label total = (Label)GV_POS.Rows[t].FindControl("lblttl");
                    GTotal += Convert.ToDecimal(total.Text);
                   

                    TextBox TBQty = (TextBox)GV_POS.Rows[t].FindControl("TBItmQty");
                    GTQty += Convert.ToDecimal(TBQty.Text);
                }

                TBTtl.Text = GTotal.ToString();
                lblttls.Text = GTotal.ToString();
                lbl_ttlqty.Text = GTQty.ToString();


                lbl_itmqty.Text = GV_POS.Rows.Count.ToString();

                lnkbtnadd.Focus();
                //TBUnit.Focus();
                //TBUnit.Attributes.Add("onfocusin", "select();");
                //AddNewRow();

            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }

            
        }

        protected void GV_POS_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row;

            try
            {
                if (e.CommandName == "Add")
                {
                    row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    //string SID = GV_POS.DataKeys[row.RowIndex].Values[0].ToString();


                    //HFMSal.Value = SID;

                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        protected void GV_POS_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ViewState["dt_adItm"] != null)
            {
                DataTable dt = (DataTable)ViewState["dt_adItm"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                HiddenField HFDSal = (HiddenField)GV_POS.Rows[rowIndex].Cells[0].FindControl("HFDSal");

                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["dt_adItm"] = dt;

                    GV_POS.DataSource = dt;
                    GV_POS.DataBind();



                    //Delete from POS
                    query = "delete from tbl_DCPos where DCposid='" + HFDSal.Value.Trim() + "'";

                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();


                    SetPreRowitm();

                    float GTotal = 0;
                    for (int j = 0; j < GV_POS.Rows.Count; j++)
                    {
                        Label lblttl = (Label)GV_POS.Rows[j].FindControl("lblttl");

                        GTotal += Convert.ToSingle(lblttl.Text);
                    }

                    TBTtl.Text = GTotal.ToString();
                    lblttls.Text = GTotal.ToString();
                }
            }
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetSearch(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();

            string str = "select SubHeadCategoriesName from SubHeadCategories inner join Customers_ on  Customers_.CustomerName = SubHeadCategories.SubHeadCategoriesName where SubHeadGeneratedID='0011' and SubHeadCategoriesName like '" + prefixText + "%' and Customers_.CompanyId='" + company + "' and Customers_.BranchId='" + branch + "'";

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
        public static List<string> GetBillNO(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = " select distinct(tbl_mcpos.billno) from tbl_mcpos " +
                " inner join tbl_dcpos on tbl_mcpos.mcposid = tbl_dcpos.mcposid " +
                " where tbl_mcpos.billno like '" + prefixText + "%' and ishold = '0' and iscancel = '0' and tbl_mcpos.CompanyId='" + company + "' and tbl_mcpos.BranchId='" + branch + "'";
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
            string str = " select distinct(Pro_Code) from tbl_Mstk " +
                " inner join tbl_Dstk on tbl_Mstk.Mstk_id = tbl_Dstk.Mstk_id " +
                " inner join Products on tbl_Dstk.ProductID = Products.ProductID " +
                " where Pro_Code like '%" + prefixText + "%' and Products.CompanyId='" + company + "' and Products.BranchId='" + branch + "'";
            //string str = " select distinct(ProductName) from products where ProductName like '%" + prefixText + "%'";
            
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetHoldBill(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select BillNO from tbl_MCHold where BillNO like '" + prefixText + "%' and  CompanyId='" + company + "' and BranchId='" + branch + "'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }

        protected void TBCust_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // For account No
                query = " select  distinct( Customers_.customerid) ,CellNo1,SubHeadCategoriesGeneratedID,  " +
                    " SubHeadCategoriesName from Customers_ inner join SubHeadCategories on  " +
                    " Customers_.CustomerName = SubHeadCategories.SubHeadCategoriesName   " +
                    " where SubHeadCategoriesName='" + TBCust.Text.Trim() + "' and  Customers_.CompanyId='" + Session["CompanyID"] + "' and Customers_.BranchId='" + Session["BranchID"] + "'";


                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt_ = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dt_);

                if (dt_.Rows.Count > 0)
                {   
                    lb_custtyp.Text = "1";
                    lbl_Acc.Text = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                }
                else
                {
                    lbldue.Text = "0.00";
                    lbl_Acc.Text = "";
                }
             
                // For dues
                query = " select  distinct( Customers_.customerid) ,CellNo1, " +
                    " CredAmt, SubHeadCategoriesGeneratedID, SubHeadCategoriesName from Customers_ inner join SubHeadCategories on   " +
                    " Customers_.CustomerName = SubHeadCategories.SubHeadCategoriesName  " +
                    " inner join tbl_Salcredit on SubHeadCategories.SubHeadCategoriesGeneratedID = tbl_Salcredit.CustomerID " +
                    " where SubHeadCategoriesName='" + TBCust.Text.Trim() + "' and Customers_.CompanyId='" + Session["CompanyID"] + "' and Customers_.BranchId='" + Session["BranchID"] + "'"; 

                    dt_ = new DataTable();
                    dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    lbldue.Text = dt_.Rows[0]["CredAmt"].ToString();
                    lb_custtyp.Text = "1";
                }
                else
                {
                    lbldue.Text = "0.00";                 
                }

                TextBox TBItms = (TextBox)GV_POS.Rows[0].Cells[0].FindControl("TBItms");
                TBItms.Focus();
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        private void SetPreRowitm()
        {
            try
            {
                int rowIndex = 0;
                if (ViewState["dt_adItm"] != null)
                {
                    DataTable dt = (DataTable)ViewState["dt_adItm"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TextBox TBItms = (TextBox)GV_POS.Rows[rowIndex].Cells[0].FindControl("TBItms");
                            TextBox TBItmdesc = (TextBox)GV_POS.Rows[rowIndex].Cells[0].FindControl("TBItmdesc");                     
                            TextBox tbsalpris = (TextBox)GV_POS.Rows[rowIndex].Cells[1].FindControl("tbsalpris");
                            TextBox TBItmQty = (TextBox)GV_POS.Rows[rowIndex].Cells[2].FindControl("TBItmQty");
                            TextBox TBUnit = (TextBox)GV_POS.Rows[rowIndex].Cells[3].FindControl("TBUnit");
                            DropDownList ddlUnit = (DropDownList)GV_POS.Rows[rowIndex].Cells[3].FindControl("ddlUnit");                           
                            Label lblttl = (Label)GV_POS.Rows[rowIndex].Cells[4].FindControl("lblttl");
                            HiddenField HFDSal = (HiddenField)GV_POS.Rows[rowIndex].Cells[5].FindControl("HFDSal");
                            HiddenField HFPROID = (HiddenField)GV_POS.Rows[rowIndex].Cells[5].FindControl("HFPROID");
                            Label lbl_Flag = (Label)GV_POS.Rows[i].FindControl("lbl_Flag");

                            string Dposid = dt.Rows[i]["Dposid"].ToString();
                            if (Dposid != "")
                            {
                                HFDSal.Value = dt.Rows[i]["Dposid"].ToString();
                            }
                            else
                            {
                                HFDSal.Value="0";
                            }

                            string Itms = dt.Rows[i]["Items"].ToString();

                            if (Itms != "")
                            {
                                TBItms.Text = dt.Rows[i]["Items"].ToString();
                            }
                            else
                            {
                                TBItms.Text = "";
                            }
                            string Itmdesc = dt.Rows[i]["ItemDesc"].ToString();

                            if (Itmdesc != "")
                            {
                                TBItmdesc.Text = dt.Rows[i]["ItemDesc"].ToString();
                            }
                            else
                            {
                                TBItmdesc.Text = "";
                            }

                            string Salpric = dt.Rows[i]["salpric"].ToString();

                            if (Salpric != "")
                            {
                                tbsalpris.Text = dt.Rows[i]["salpric"].ToString();
                            }
                            else
                            {
                                tbsalpris.Text = "0.00";
                            }

                            string QTY = dt.Rows[i]["QTY"].ToString();

                            if (QTY != "")
                            {
                                TBItmQty.Text = dt.Rows[i]["QTY"].ToString();
                            }
                            else
                            {
                                TBItmQty.Text = "0";
                            }

                           
                            
                            string unt = dt.Rows[i]["UNIT"].ToString();

                            if (unt != "")
                            {
                               
                                getunts(TBItms.Text.Trim());
                                TBUnit.Text = dt.Rows[i]["UNIT"].ToString();

                                if (ddlUnit.SelectedValue == "0")
                                {
                                    ddlUnit.SelectedValue = TBUnit.Text.Trim();
                                }
                                
                                ddlUnit.Width = 100;                            
                            }
                            else
                            {
                                TBUnit.Text = "";
                            }

                            string netttl = dt.Rows[i]["TTL"].ToString();

                            if (netttl != "")
                            {
                                lblttl.Text = dt.Rows[i]["TTL"].ToString();
                            }
                            else
                            {
                                lblttl.Text = "0.00";
                            }

                            string proid = dt.Rows[i]["ProductID"].ToString();

                            if (proid != "")
                            {

                                HFPROID.Value = dt.Rows[i]["ProductID"].ToString();
                            }
                            else
                            {
                                HFPROID.Value = "0";
                            }

                            //HFDSal.Value = dt.Rows[i]["Dposid"].ToString();


                            if (TBItms.Text == "")
                            {
                                lbl_Flag.Text = "0";
                            }
                            else
                            {
                                lbl_Flag.Text = "1";
                            }

                            rowIndex++;
                            TBUnit.Visible = false;
                            TBItmdesc.Width = 100;
                            TBItms.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        public void ptnSno()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                int v = 0;
                int b = 0;
                string str = "select count(MCposid) as [MCposid] from tbl_MCPos  where CompanyId='" + Session["CompanyID"] + 
                    "' and BranchId='" + Session["BranchID"] + "' order by MCposid desc";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (string.IsNullOrEmpty(lbl_BillNo.Text))//if (lbl_BillNo.Text == "" || lbl_BillNo.Text.Trim() == null)
                        {
                            v = Convert.ToInt32(reader["MCposid"].ToString());
                            b = v + 1;
                            lbl_BillNo.Text = b.ToString();

                        }
                        else
                        {
                             v = Convert.ToInt32(reader["MCposid"].ToString());
                             b = v + 1;
                             lbl_BillNo.Text = b.ToString();

                        }
                    }
                }
                else
                {
                    lbl_BillNo.Text = "1";

                }
                con.Close();

            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
            }
        }



        //protected void btn_Cust_Click(object sender, EventArgs e)
        //{
        //    //ModalPopupExtender1.Show();
        //    Panel1.Visible = true;
        //}

        //protected void btn_Pro_Click(object sender, EventArgs e)
        //{
        //    //ModalPopupExtender2.Show();
        //    Panel2.Visible = true;
        //}

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void TBItms_TextChanged(object sender, EventArgs e)
        {
            try
            { 
                TextBox TBItmQty = null;

                for (int j = 0; j < GV_POS.Rows.Count; j++)
                {
                    TextBox TBItms = (TextBox)GV_POS.Rows[j].FindControl("TBItms");
                    TextBox TBItmdesc = (TextBox)GV_POS.Rows[j].FindControl("TBItmdesc");
                    TBItmQty = (TextBox)GV_POS.Rows[j].FindControl("TBItmQty");
                    TextBox tbsalpris = (TextBox)GV_POS.Rows[j].FindControl("tbsalpris");
                    Label lblchkqty = (Label)GV_POS.Rows[j].FindControl("lblchkqty");
                    DropDownList ddlUnit = (DropDownList)GV_POS.Rows[j].FindControl("ddlUnit");
                    Label lblttl = (Label)GV_POS.Rows[j].FindControl("lblttl");
                    Label lbl_Flag = (Label)GV_POS.Rows[j].FindControl("lbl_Flag");
                    LinkButton lnkbtnadd = (LinkButton)GV_POS.Rows[j].FindControl("lnkbtnadd");
                    HiddenField HFPROID = (HiddenField)GV_POS.Rows[j].FindControl("HFPROID");
                    Label lblchkpro = (Label)GV_POS.Rows[j].FindControl("lblchkpro");

                    string str = " select distinct(ProductName) as [ProductName], Pro_Code, " +
                           "  Dstk_Qty as [QTY],Dstk_rat ,RetalPrice,tbl_Dstk.ProductID from tbl_Mstk  " +
                       " inner join tbl_Dstk on tbl_Mstk.Mstk_id = tbl_Dstk.Mstk_id " +
                       " inner join Products on tbl_Dstk.ProductID = Products.ProductID where Pro_Code =  '" + TBItms.Text.Trim() + "' and Dstk_Qty <> 0 and Products.CompanyId='" + Session["CompanyID"] + "' and Products.BranchId='" + Session["BranchID"] + "'";
                    //string str = " select distinct (ProductID) , ProductName,  RetalPrice from products where ProductName = '" + TBItms.Text.Trim() + "'";
                    dt_ = new DataTable();
                    dt_ = DBConnection.GetQueryData(str);
                    if (dt_.Rows.Count > 0)
                    {


                        HFPROID.Value = dt_.Rows[0]["ProductID"].ToString();
                        //lblttl.Text = dt_.Rows[0]["TTL"].ToString();
                        //if (TBItmdesc.Text != "")
                        //{
                        TBItms.Text = dt_.Rows[0]["Pro_Code"].ToString();
                        TBItmdesc.Text = dt_.Rows[0]["ProductName"].ToString();
                        //}
                        string retailpric = dt_.Rows[0]["RetalPrice"].ToString();


                        if (tbsalpris.Text != "" || tbsalpris.Text != "0" || tbsalpris.Text != "0.00")
                        {
                            if (lbl_Flag.Text == "0")
                            {
                                tbsalpris.Text = dt_.Rows[0]["RetalPrice"].ToString();
                            }
                            
                        }
                        else
                        {
                            tbsalpris.Text = "0.00";
                        }

                        if (HFPROID.Value == "0")
                        {
                            HFPROID.Value = dt_.Rows[0]["ProductID"].ToString();
                        }
                        else
                        {
                            //HFPROID.Value = "0";
                        }
                        lblchkpro.Text = "";
                        TBItmdesc.Enabled= true;
                        TBItmQty.Enabled= true;
                        tbsalpris.Enabled = true;

                        lblttl.Text = (Convert.ToDouble(TBItmQty.Text.Trim()) * Convert.ToDouble(tbsalpris.Text.Trim())).ToString();

                        //if (ddlUnit.SelectedValue == "0")
                        //{
                            //Get Units of Products if no product is selected..
                            getunts(TBItms.Text.Trim());
                            ddlUnit.Width= 100;
                        //}
                    }
                    else 
                    {  
                        lblchkpro.Text = "Product is not Available!!";
                        TBItmdesc.Enabled = false;
                        TBItmQty.Enabled = false;
                        tbsalpris.Enabled = false;
                    }
                }

                // for total 
                decimal GTotal = 0;
                decimal GTQty = 0;

                for (int t = 0; t < GV_POS.Rows.Count; t++)
                {
                    Label total = (Label)GV_POS.Rows[t].FindControl("lblttl");
                    GTotal += Convert.ToDecimal(total.Text);
                    
                    TextBox TBQty = (TextBox)GV_POS.Rows[t].FindControl("TBItmQty");
                    GTQty += Convert.ToDecimal(TBQty.Text);
                }

                TBTtl.Text = GTotal.ToString();
                lblttls.Text = GTotal.ToString();
                lbl_ttlqty.Text = GTQty.ToString();

                lbl_itmqty.Text = GV_POS.Rows.Count.ToString();

                TBItmQty.Focus();
                TBItmQty.Attributes.Add("onfocusin", "select();");
                //tbsalpris.Attributes.Add("onkeypress", "button_click(this,'" + this.tbsalpris.ClientID + "')");
                ModalPopupExtender1.Hide();
                
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


        protected void TBAdvance_TextChanged(object sender, EventArgs e)
        {
            try
            {


                int chk = 0;
                //if (Page.IsValid)
                //{


                try
                {
                    string bal = (Convert.ToDecimal(lblttls.Text) - Convert.ToDecimal(TBAdvance.Text.Trim())).ToString();
                    TBBalance.Text = bal;


                    //Check For Validations
                    if (TBCust.Text == "")
                    {
                        lblval.Text = "Customer Name Should not be Empty!!";
                    }
                    else if (lbl_Acc.Text == "")
                    {
                        lblval.Text = "Customer Account Should not be Empty!!";

                    }
                    else if (lbldue.Text == "")
                    {
                        lblval.Text = "Customer Due Should not be Empty!!";
                    }
                    else if (TBAdvance.Text == "")
                    {
                        lblval.Text = "Amount Paid Should not be Empty!!";
                    }
                    else if (TBBalance.Text == "")
                    {
                        lblval.Text = "Amount Balance Should not be Empty!!";
                    }
                    else if (TBTtl.Text == "")
                    {
                        lblval.Text = "Total Should not be Empty!!";
                    }
                    else
                    {
                        TextBox TBItmdesc = null;
                        TextBox TBItms = null;
                        TextBox tbsalpris = null;
                        TextBox TBItmQty = null;

                        for (int j = 0; j < GV_POS.Rows.Count; j++)
                        {
                            TBItms = (TextBox)GV_POS.Rows[j].FindControl("TBItms");
                            TBItmdesc = (TextBox)GV_POS.Rows[j].FindControl("TBItmdesc");
                            TBItmQty = (TextBox)GV_POS.Rows[j].FindControl("TBItmQty");
                            tbsalpris = (TextBox)GV_POS.Rows[j].FindControl("tbsalpris");
                            Label lblchkqty = (Label)GV_POS.Rows[j].FindControl("lblchkqty");
                            Label lblttl = (Label)GV_POS.Rows[j].FindControl("lblttl");
                            Label lbl_Flag = (Label)GV_POS.Rows[j].FindControl("lbl_Flag");
                            LinkButton lnkbtnadd = (LinkButton)GV_POS.Rows[j].FindControl("lnkbtnadd");
                            HiddenField HFPROID = (HiddenField)GV_POS.Rows[j].FindControl("HFPROID");
                            Label lblchkpro = (Label)GV_POS.Rows[j].FindControl("lblchkpro");

                        }
                        if (TBItms.Text == "")
                        {
                            lblval.Text = "Items ID Should not be Empty!!";
                        }
                        else if (TBItmdesc.Text == "")
                        {
                            lblval.Text = "Items Should not be Empty!!";
                        }
                        else if (TBItmQty.Text == "")
                        {
                            lblval.Text = "Items Should not be Empty!!";
                        }
                        else if (tbsalpris.Text == "")
                        {
                            lblval.Text = "Items Sale Price Should not be Empty!!";
                        }
                        else
                        {
                            chk = sav();

                            if (chk == 1)
                            {
                                clear();
                                data();
                                loaditm();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblmssg.Text = ex.Message;
                }
             
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
            finally
            {
                //Session["chek"] = "0";
            }
            }        
 

        private void Update()
        {

           string querys = " Update Customers_ set CustomerName='" + TB_CustNam.Text + "', GST=''" +
                " , category = '', NTN='', customertype_= '',areaid='', Area=''" +
                " , saleper ='', District='', PhoneNo = ''" +
                " , Email='', CellNo1='" + TBNTN.Text + "', PostalCode='', CellNo2='', PostalOfficeContact='', CellNo3='', NIC=''" +
                " , CellNo4='', city_='', Address='', CreatedBy='" + Session["user"].ToString() + "', CreatedDate='" + DateTime.Now + "', IsActive='true', Cus_Code = '', CompanyId='" + Session["CompanyID"] + "'" +
                " , BranchId= '" + Session["BranchID"] + "' where CustomerID= '" + HFCustID.Value.Trim() + "'";
        //            [left_eye], [right_eye],[RSph],[RCyl],[RAxis],[LSph],[LCyl],[LAsix]

            con.Open();


            using (SqlCommand cmd = new SqlCommand(querys, con))
            {


                cmd.ExecuteNonQuery();

            }
            con.Close();

        }

        protected void btn_Sav_Click(object sender, EventArgs e)
        {
            if (HFHold.Value == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_POSReceipt.aspx?ID=POS&POSID=" + lbl_BillNo.Text.Trim() + "&CUST=" + lbl_Acc.Text.Trim() + "&HOLD=0','_blank','height=600px,width=400px,scrollbars=1');", true);
            }
            else if (HFHold.Value == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_POSReceipt.aspx?ID=POS&POSID=" + lbl_BillNo.Text.Trim() + "&CUST=" + lbl_Acc.Text.Trim() + "&Bal=" + lbldue.Text.Trim() + "&HOLD=1','_blank','height=600px,width=400px,scrollbars=1');", true);
            }
        }

        protected void Btn_Cancl_Click(object sender, EventArgs e)
        {   
            Response.Redirect("frm_PSal.aspx");
        }

        protected void btn_prtbil_Click(object sender, EventArgs e)
        {
        }


        private int sav()
        {
            string HFPROID = "";

            con.Open();

            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            // Start a local transaction.
            transaction = con.BeginTransaction("POSTrans");

            // Must assign both transaction object and connection 
            // to Command object for a pending local transaction
            command.Connection = con;
            command.Transaction = transaction;

            try
            {
                if (Session["chek"] == "0")
                {
                    if (HFHold.Value == "0")
                    {
                        if (HFCposid.Value == "0")
                        {
                            // Checking for POS

                            command.CommandText = " select tbl_MCPos.BillNO from tbl_MCPos inner join tbl_DCPos on tbl_MCPos.MCposid= tbl_DCPos.MCposid " +
                        "  where tbl_MCPos.billno ='" + lbl_BillNo.Text.Trim() + "' and tbl_MCPos.CompanyId='" + Session["CompanyID"] +"' and tbl_MCPos.BranchId='" + Session["BranchID"] + "'";

                            DataTable dtchkpos = new DataTable();
                            command.CommandType = CommandType.Text;

                            dtchkpos = new DataTable();

                            SqlDataAdapter posadp = new SqlDataAdapter(command);

                            posadp.Fill(dtchkpos);

                            if (dtchkpos.Rows.Count > 0)
                            {
                                // Do Nothng
                            }
                            else
                            {
                                // For POS
                                #region For POS

                                // Master Counter POS

                                //Getting the Account No for Customers

                                command.CommandText = " select * from SubHeadCategories where SubHeadCategoriesGeneratedID = '" + lbl_Acc.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] +"' and BranchId='" + Session["BranchID"] + "'";

                                command.CommandType = CommandType.Text;

                                dt_ = new DataTable();

                                SqlDataAdapter custadp = new SqlDataAdapter(command);

                                custadp.Fill(dt_);

                                if (dt_.Rows.Count > 0)
                                {
                                    custid = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                                }
                                else
                                {
                                    custid = "00118";
                                }

                                // Master POS Inserting...

                                command.Parameters.Clear();
                                command.CommandText = "sp_mcpos";
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@msalid", HFCposid.Value);
                                command.Parameters.AddWithValue("@BillNO", lbl_BillNo.Text);
                                command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                                command.Parameters.AddWithValue("@BranchId", Session["BranchID"]);
                                command.Parameters.AddWithValue("@Username", Session["Username"]);
                                command.Parameters.AddWithValue("@billdat", lbl_dat.Text);
                                command.Parameters.AddWithValue("@billtim", txtClock.Value);
                                command.Parameters.AddWithValue("@customerid", custid.Trim());
                                command.Parameters.AddWithValue("@CustomerName", TBCust.Text);
                                command.Parameters.AddWithValue("@CellNo1", "");
                                command.Parameters.AddWithValue("@cust_Due", lbldue.Text);
                                command.Parameters.AddWithValue("@custtyp", lb_custtyp.Text);
                                command.Parameters.AddWithValue("@createdby", Session["Username"]);
                                command.Parameters.AddWithValue("@createdat", DateTime.Now.ToString());
                                command.Parameters.AddWithValue("@createterminal", Session["BranchID"]);
                                command.Parameters.AddWithValue("@updatedby", "");
                                command.Parameters.AddWithValue("@updatedat", "");
                                command.Parameters.AddWithValue("@updateterminal", "");
                                command.Parameters.AddWithValue("@ProductID", "0");
                                command.Parameters.AddWithValue("@salprice", "0");
                                command.Parameters.AddWithValue("@ProQty", "0");
                                command.Parameters.AddWithValue("@Amt", "0");
                                command.Parameters.AddWithValue("@ttl_qty", "0");
                                command.Parameters.AddWithValue("@ttl_itms", lbl_itmqty.Text);
                                command.Parameters.AddWithValue("@dis", TBDisc.Text);
                                command.Parameters.AddWithValue("@taxper", chk_tax1.Text);
                                command.Parameters.AddWithValue("@othcharg", chk_tax2.Text);
                                command.Parameters.AddWithValue("@Adv", TBAdvance.Text);
                                command.Parameters.AddWithValue("@bal", TBBalance.Text);
                                command.Parameters.AddWithValue("@Ttl", lblttls.Text);
                                command.Parameters.AddWithValue("@grntttl", TBTtl.Text);
                                command.Parameters.AddWithValue("@ttl_unts", "");
                                command.Parameters.AddWithValue("@Ishold", "0");
                                command.Parameters.AddWithValue("@Tax", TBTaxPer.Text.Trim());
                                command.Parameters.AddWithValue("@OthTax", TBOthChrgs.Text.Trim());
                                command.Parameters.AddWithValue("@RecieverNam", TBREC.Text.Trim());
                                command.Parameters.AddWithValue("@tax1amt", TBTaxPer.Text.Trim());
                                command.Parameters.AddWithValue("@tax2amt", TBOthChrgs.Text.Trim());
                                command.Parameters.AddWithValue("@paytyp", "Cash");
                                command.Parameters.AddWithValue("@disc", TBDisc.Text.Trim());

                                command.ExecuteNonQuery();

                                //Getting the Last POS ID...

                                command.CommandText = " select top 1 MCposid from tbl_MCPos where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "' order by MCposid desc ";

                                command.CommandType = CommandType.Text;

                                dt_ = new DataTable();

                                SqlDataAdapter adp = new SqlDataAdapter(command);

                                adp.Fill(dt_);

                                if (dt_.Rows.Count > 0)
                                {
                                    HFCposid.Value = dt_.Rows[0]["MCposid"].ToString();
                                }

                                // For Details POS

                                foreach (GridViewRow g1 in GV_POS.Rows)
                                {
                                    TBItms = (g1.FindControl("TBItms") as TextBox).Text;
                                    tbsalpris = (g1.FindControl("tbsalpris") as TextBox).Text;
                                    TBItmQty = (g1.FindControl("TBItmQty") as TextBox).Text;
                                    TBUnit = (g1.FindControl("TBUnit") as TextBox).Text;
                                    lblttl = (g1.FindControl("lblttl") as Label).Text;
                                    HFDSal = (g1.FindControl("HFDSal") as HiddenField).Value;
                                    HFPROID = (g1.FindControl("HFPROID") as HiddenField).Value;


                                    //For getting product id 

                                    command.CommandText = " select productid from Products where Pro_Code='" + TBItms.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                                    command.CommandType = CommandType.Text;
                                    DataTable dtgetproid = new DataTable();

                                    SqlDataAdapter adpgetproid = new SqlDataAdapter(command);

                                    adpgetproid.Fill(dtgetproid);

                                    if (dtgetproid.Rows.Count > 0)
                                    {
                                        prodtid = dtgetproid.Rows[0]["productid"].ToString();
                                    }

                                    // For Details POS

                                    command.Parameters.Clear();
                                    command.CommandText = "sp_dcpos";
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@msalid", HFCposid.Value.Trim());
                                    command.Parameters.AddWithValue("@dsalid", HFDSal.Trim());
                                    command.Parameters.AddWithValue("@BillNO", lbl_BillNo.Text);
                                    command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                                    command.Parameters.AddWithValue("@BranchId", Session["BranchID"]);
                                    command.Parameters.AddWithValue("@Username", Session["Username"]);
                                    command.Parameters.AddWithValue("@billdat", lbl_dat.Text);
                                    command.Parameters.AddWithValue("@billtim", txtClock.Value);
                                    command.Parameters.AddWithValue("@customerid", custid.Trim());
                                    command.Parameters.AddWithValue("@CustomerName", TBCust.Text);
                                    command.Parameters.AddWithValue("@CellNo1", "");
                                    command.Parameters.AddWithValue("@cust_Due", lbldue.Text);
                                    command.Parameters.AddWithValue("@custtyp", lb_custtyp.Text);
                                    command.Parameters.AddWithValue("@createdby", Session["Username"]);
                                    command.Parameters.AddWithValue("@createdat", DateTime.Now.ToString());
                                    command.Parameters.AddWithValue("@createterminal", Session["BranchID"]);
                                    command.Parameters.AddWithValue("@updatedby", "");
                                    command.Parameters.AddWithValue("@updatedat", "");
                                    command.Parameters.AddWithValue("@updateterminal", "");
                                    command.Parameters.AddWithValue("@ProductID", prodtid);
                                    command.Parameters.AddWithValue("@salprice", tbsalpris.Trim());
                                    command.Parameters.AddWithValue("@ProQty", TBItmQty.Trim());
                                    command.Parameters.AddWithValue("@Amt", lblttl.Trim());
                                    command.Parameters.AddWithValue("@ttl_qty", lbl_ttlqty.Text);
                                    command.Parameters.AddWithValue("@ttl_itms", lbl_itmqty.Text);
                                    command.Parameters.AddWithValue("@dis", TBDisc.Text);
                                    command.Parameters.AddWithValue("@taxper", TBTaxPer.Text);
                                    command.Parameters.AddWithValue("@othcharg", TBOthChrgs.Text);
                                    command.Parameters.AddWithValue("@Adv", TBAdvance.Text);
                                    command.Parameters.AddWithValue("@bal", TBBalance.Text);
                                    command.Parameters.AddWithValue("@Ttl", lblttls.Text);
                                    command.Parameters.AddWithValue("@grntttl", TBTtl.Text);
                                    command.Parameters.AddWithValue("@ttl_unts", TBUnit.Trim());

                                    command.ExecuteNonQuery();

                                    /// For Stock Maintainance
                                    #region stock

                                    //Detail Stock

                                    DataTable dtstkqty = new DataTable();

                                    command.CommandText = "select Dstk_Qty from tbl_Dstk where ProductID = " + prodtid.Trim() + " and Dstk_unt='" + TBUnit + "' and   CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                                    command.CommandType = CommandType.Text;

                                    SqlDataAdapter Adapter = new SqlDataAdapter(command);
                                    Adapter.Fill(dtstkqty);

                                    if (dtstkqty.Rows.Count > 0)
                                    {
                                        stkqty = dtstkqty.Rows[0]["Dstk_Qty"].ToString();

                                        int qty = Convert.ToInt32(stkqty) - Convert.ToInt32(TBItmQty);
                                        command.CommandText = " UPDATE tbl_Dstk SET Dstk_Qty = " + qty + " where  ProductID = " + prodtid.Trim() + " and Dstk_unt='" + TBUnit + "'  and  CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                                        command.ExecuteNonQuery();
                                    }

                                    #endregion

                                }
                                #endregion

                               
                                /// For Credit Amount of Customers specially Regular Customers
                                #region CreditCash

                                command.CommandText = "select CredAmt from tbl_Salcredit where CustomerID ='" + lbl_Acc.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                                    command.CommandType = CommandType.Text;

                                    SqlDataAdapter stksavcre = new SqlDataAdapter(command);

                                    DataTable dtsavcre = new DataTable();
                                    stksavcre.Fill(dtsavcre);
                                    decimal creamt;
                                    decimal recv;
                                    if (dtsavcre.Rows.Count > 0)
                                    {
                                        creamt = Convert.ToDecimal(dtsavcre.Rows[0]["CredAmt"]);

                                        recv = Convert.ToDecimal(creamt) + Convert.ToDecimal(TBBalance.Text.Trim());

                                        command.CommandText = " Update tbl_Salcredit set CredAmt = '" + recv + "' where CustomerID='" + lbl_Acc.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                                        command.CommandType = CommandType.Text;
                                        command.ExecuteNonQuery();

                                    }
                                    else
                                    {
                                        command.CommandText = " insert into tbl_Salcredit (CustomerID,CredAmt,CompanyId,BranchId) values('" + lbl_Acc.Text.Trim() + "','" + TBBalance.Text.Trim() + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                                        command.CommandType = CommandType.Text;
                                        command.ExecuteNonQuery();

                                    }

                                #endregion

                                /// For Expences or Accounts                                
                                #region For Accounts

                                    //Accounts

                                    dt_ = new DataTable();

                                    command.CommandText = "select SubHeadCategoriesGeneratedID,SubHeadCategoriesName from SubHeadCategories where SubHeadGeneratedID='0011' " +
                                        " and SubHeadCategoriesName='" + TBCust.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] +"' and BranchId='" + Session["BranchID"] + "'";

                                    command.CommandType = CommandType.Text;

                                    SqlDataAdapter adpchkcust = new SqlDataAdapter(command);
                                    adpchkcust.Fill(dt_);

                                    if (dt_.Rows.Count > 0)
                                    {
                                        accno = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                                        acctitle = dt_.Rows[0]["SubHeadCategoriesName"].ToString();

                                    }
                                    else
                                    {
                                        accno = "00118";
                                        acctitle = "walk-in";
                                    }

                                    DataTable dto = new DataTable();

                                    command.CommandText = "";
                                    command.CommandText = " select openbal,openingBal from tbl_expenses where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                                    SqlDataAdapter adpopenbal = new SqlDataAdapter(command);

                                    adpopenbal.Fill(dto);

                                    int i = dto.Rows.Count;

                                    while (--i >= 0)
                                    {

                                        if (dto.Rows[i].IsNull(0)) dto.Rows.RemoveAt(i);

                                    }

                                    if (dto.Rows.Count > 0)
                                    {
                                        for (int k = 0; k < dto.Rows.Count; k++)
                                        {
                                            openingbal = dto.Rows[k]["openbal"].ToString();
                                        }

                                        openbal = Convert.ToDecimal(openingbal);//Convert.ToDecimal(openingbal) - Convert.ToDecimal(lblttls.Text);
                                    }

                                    command.Parameters.Clear();

                                    command.CommandText = "expense";
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@acctitle", acctitle.Trim());
                                    command.Parameters.AddWithValue("@accno", accno.Trim());
                                    command.Parameters.AddWithValue("@expensesdat", lbl_dat.Text.Trim());
                                    command.Parameters.AddWithValue("@billno", lbl_BillNo.Text.Trim());
                                    command.Parameters.AddWithValue("@typeofpay", "Cash");
                                    command.Parameters.AddWithValue("@expencermk", "");
                                    command.Parameters.AddWithValue("@cashamt", TBAdvance.Text.Trim());
                                    command.Parameters.AddWithValue("@CashBnk_id", "1");
                                    command.Parameters.AddWithValue("@CashBnk_nam", "");
                                    command.Parameters.AddWithValue("@bankamt", "");
                                    command.Parameters.AddWithValue("@PaymentIn", TBAdvance.Text.Trim());
                                    command.Parameters.AddWithValue("@PaymentOut", "0");
                                    command.Parameters.AddWithValue("@Amountpaid", TBAdvance.Text.Trim());
                                    command.Parameters.AddWithValue("@prevbal", TBBalance.Text.Trim());
                                    command.Parameters.AddWithValue("@createat", DateTime.Now.ToString());
                                    command.Parameters.AddWithValue("@createby", Session["Username"]);
                                    command.Parameters.AddWithValue("@companyid", Session["CompanyID"]);
                                    command.Parameters.AddWithValue("@branchid", Session["BranchID"]);
                                    command.Parameters.AddWithValue("@ChqDat", DateTime.Now.ToString());
                                    command.Parameters.AddWithValue("@ChqNO", "");
                                    command.Parameters.AddWithValue("@OpenBal", openingbal.Trim());
                                    command.Parameters.AddWithValue("@openingBal", lbl_openbalance1.Text);
                                    command.Parameters.AddWithValue("@opening_balance", lbl_Openbalance.Text.Trim());


                                    command.ExecuteNonQuery();

                                #endregion
                            }
                        }
                        else if (HFCposid.Value != "0")
                        {
                            #region For POS
                            
                            // Master Counter POS

                            // Getting the Account No of Customers..

                            command.CommandText = " select * from SubHeadCategories where SubHeadCategoriesGeneratedID = '" + lbl_Acc.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] +"' and BranchId='" + Session["BranchID"] + "'";

                            command.CommandType = CommandType.Text;

                            dt_ = new DataTable();

                            SqlDataAdapter custadp = new SqlDataAdapter(command);

                            custadp.Fill(dt_);

                            if (dt_.Rows.Count > 0)
                            {
                                custid = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                            }
                            else
                            {
                                custid = "00118";
                            }


                            //Updating the Master POS...

                            command.Parameters.Clear();
                            command.CommandText = "sp_updmcpos";
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@msalid", HFCposid.Value);
                            command.Parameters.AddWithValue("@BillNO", lbl_BillNo.Text);
                            command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                            command.Parameters.AddWithValue("@BranchId", Session["BranchID"]);
                            command.Parameters.AddWithValue("@Username", Session["Username"]);
                            command.Parameters.AddWithValue("@billdat", lbl_dat.Text);
                            command.Parameters.AddWithValue("@billtim", txtClock.Value);
                            command.Parameters.AddWithValue("@customerid", custid.Trim());
                            command.Parameters.AddWithValue("@CustomerName", TBCust.Text);
                            command.Parameters.AddWithValue("@CellNo1", "");
                            command.Parameters.AddWithValue("@cust_Due", lbldue.Text);
                            command.Parameters.AddWithValue("@custtyp", lb_custtyp.Text);
                            command.Parameters.AddWithValue("@createdby", Session["Username"]);
                            command.Parameters.AddWithValue("@createdat", DateTime.Now.ToString());
                            command.Parameters.AddWithValue("@createterminal", Session["BranchID"]);
                            command.Parameters.AddWithValue("@updatedby", "");
                            command.Parameters.AddWithValue("@updatedat", "");
                            command.Parameters.AddWithValue("@updateterminal", "");
                            command.Parameters.AddWithValue("@ProductID", "0");
                            command.Parameters.AddWithValue("@salprice", "0");
                            command.Parameters.AddWithValue("@ProQty", "0");
                            command.Parameters.AddWithValue("@Amt", "0");
                            command.Parameters.AddWithValue("@ttl_qty", "0");
                            command.Parameters.AddWithValue("@ttl_itms", lbl_itmqty.Text);
                            command.Parameters.AddWithValue("@dis", TBDisc.Text);
                            command.Parameters.AddWithValue("@taxper", TBTaxPer.Text);
                            command.Parameters.AddWithValue("@othcharg", TBOthChrgs.Text);
                            command.Parameters.AddWithValue("@Adv", TBAdvance.Text);
                            command.Parameters.AddWithValue("@bal", TBBalance.Text);
                            command.Parameters.AddWithValue("@Ttl", lblttls.Text);
                            command.Parameters.AddWithValue("@grntttl", TBTtl.Text);
                            command.Parameters.AddWithValue("@ttl_unts", "");
                            command.Parameters.AddWithValue("@Ishold", "0");
                            command.Parameters.AddWithValue("@Tax", TBTaxPer.Text.Trim());
                            command.Parameters.AddWithValue("@OthTax", TBOthChrgs.Text.Trim());

                            command.ExecuteNonQuery();

                            foreach (GridViewRow g1 in GV_POS.Rows)
                            {

                                TBItms = (g1.FindControl("TBItms") as TextBox).Text;
                                tbsalpris = (g1.FindControl("tbsalpris") as TextBox).Text;
                                TBItmQty = (g1.FindControl("TBItmQty") as TextBox).Text;
                                TBUnit = (g1.FindControl("TBUnit") as TextBox).Text;

                                lblttl = (g1.FindControl("lblttl") as Label).Text;
                                HFDSal = (g1.FindControl("HFDSal") as HiddenField).Value;
                                HFPROID = (g1.FindControl("HFPROID") as HiddenField).Value;
                                HFDSal = (g1.FindControl("HFDSal") as HiddenField).Value;

                                if (HFDSal != "0" && HFDSal != "")
                                {

                                    // Getting Product ID..

                                    command.CommandText = " select productid from Products where Pro_Code='" + TBItms.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                                    command.CommandType = CommandType.Text;
                                    DataTable dtgetproid = new DataTable();

                                    SqlDataAdapter adpgetproid = new SqlDataAdapter(command);

                                    adpgetproid.Fill(dtgetproid);

                                    if (dtgetproid.Rows.Count > 0)
                                    {
                                        prodtid = dtgetproid.Rows[0]["productid"].ToString();
                                    }


                                    // Getting POS

                                    dt_ = new DataTable();

                                    command.CommandType = CommandType.Text;
                                    command.CommandText = " select * from tbl_MCPos " +
                                        " inner join tbl_DCPos on tbl_MCPos.MCposid = tbl_DCPos.MCposid where ProductID = " + HFPROID.Trim() + " and ttl_unts='" + TBUnit + "'  and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                                    SqlDataAdapter DSalAdp = new SqlDataAdapter(command);

                                    DSalAdp.Fill(dt_);

                                    if (dt_.Rows.Count > 0)
                                    {
                                        salqty = dt_.Rows[0]["ProQty"].ToString();
                                        salrate = dt_.Rows[0]["salprice"].ToString();
                                    }

                                    //Updating the detail POS

                                    command.Parameters.Clear();
                                    command.CommandText = "sp_upddcpos";
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@msalid", HFCposid.Value.Trim());
                                    command.Parameters.AddWithValue("@dsalid", HFDSal.Trim());
                                    command.Parameters.AddWithValue("@BillNO", lbl_BillNo.Text);
                                    command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                                    command.Parameters.AddWithValue("@BranchId", Session["BranchID"]);
                                    command.Parameters.AddWithValue("@Username", Session["Username"]);
                                    command.Parameters.AddWithValue("@billdat", lbl_dat.Text);
                                    command.Parameters.AddWithValue("@billtim", txtClock.Value);
                                    command.Parameters.AddWithValue("@customerid", custid.Trim());
                                    command.Parameters.AddWithValue("@CustomerName", TBCust.Text);
                                    command.Parameters.AddWithValue("@CellNo1", "");
                                    command.Parameters.AddWithValue("@cust_Due", lbldue.Text);
                                    command.Parameters.AddWithValue("@custtyp", lb_custtyp.Text);
                                    command.Parameters.AddWithValue("@createdby", Session["Username"]);
                                    command.Parameters.AddWithValue("@createdat", DateTime.Now.ToString());
                                    command.Parameters.AddWithValue("@createterminal", Session["BranchID"]);
                                    command.Parameters.AddWithValue("@updatedby", "");
                                    command.Parameters.AddWithValue("@updatedat", "");
                                    command.Parameters.AddWithValue("@updateterminal", "");
                                    command.Parameters.AddWithValue("@ProductID", prodtid);
                                    command.Parameters.AddWithValue("@salprice", tbsalpris.Trim());
                                    command.Parameters.AddWithValue("@ProQty", TBItmQty.Trim());
                                    command.Parameters.AddWithValue("@Amt", lblttl.Trim());
                                    command.Parameters.AddWithValue("@ttl_qty", lbl_ttlqty.Text);
                                    command.Parameters.AddWithValue("@ttl_itms", lbl_itmqty.Text);
                                    command.Parameters.AddWithValue("@dis", TBDisc.Text);
                                    command.Parameters.AddWithValue("@taxper", TBTaxPer.Text);
                                    command.Parameters.AddWithValue("@othcharg", TBOthChrgs.Text);
                                    command.Parameters.AddWithValue("@Adv", TBAdvance.Text);
                                    command.Parameters.AddWithValue("@bal", TBBalance.Text);
                                    command.Parameters.AddWithValue("@Ttl", lblttls.Text);
                                    command.Parameters.AddWithValue("@grntttl", TBTtl.Text);
                                    command.Parameters.AddWithValue("@ttl_unts", TBUnit.Trim());

                                    command.ExecuteNonQuery();

                                }
                                else if (HFDSal == "0" || HFDSal == "")
                                {
                                    command.CommandText = " select productid from Products where Pro_Code='" + TBItms.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                                    command.CommandType = CommandType.Text;
                                    DataTable dtgetproid = new DataTable();

                                    SqlDataAdapter adpgetproid = new SqlDataAdapter(command);

                                    adpgetproid.Fill(dtgetproid);

                                    if (dtgetproid.Rows.Count > 0)
                                    {
                                        prodtid = dtgetproid.Rows[0]["productid"].ToString();
                                    }

                                    command.Parameters.Clear();
                                    command.CommandText = "sp_dcpos";
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@msalid", HFCposid.Value.Trim());
                                    command.Parameters.AddWithValue("@dsalid", HFDSal.Trim());
                                    command.Parameters.AddWithValue("@BillNO", lbl_BillNo.Text);
                                    command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                                    command.Parameters.AddWithValue("@BranchId", Session["BranchID"]);
                                    command.Parameters.AddWithValue("@Username", Session["Username"]);
                                    command.Parameters.AddWithValue("@billdat", lbl_dat.Text);
                                    command.Parameters.AddWithValue("@billtim", txtClock.Value);
                                    command.Parameters.AddWithValue("@customerid", custid.Trim());
                                    command.Parameters.AddWithValue("@CustomerName", TBCust.Text);
                                    command.Parameters.AddWithValue("@CellNo1", "");
                                    command.Parameters.AddWithValue("@cust_Due", lbldue.Text);
                                    command.Parameters.AddWithValue("@custtyp", lb_custtyp.Text);
                                    command.Parameters.AddWithValue("@createdby", Session["Username"]);
                                    command.Parameters.AddWithValue("@createdat", DateTime.Now.ToString());
                                    command.Parameters.AddWithValue("@createterminal", Session["BranchID"]);
                                    command.Parameters.AddWithValue("@updatedby", "");
                                    command.Parameters.AddWithValue("@updatedat", "");
                                    command.Parameters.AddWithValue("@updateterminal", "");
                                    command.Parameters.AddWithValue("@ProductID", prodtid);
                                    command.Parameters.AddWithValue("@salprice", tbsalpris.Trim());
                                    command.Parameters.AddWithValue("@ProQty", TBItmQty.Trim());
                                    command.Parameters.AddWithValue("@Amt", lblttl.Trim());
                                    command.Parameters.AddWithValue("@ttl_qty", lbl_ttlqty.Text);
                                    command.Parameters.AddWithValue("@ttl_itms", lbl_itmqty.Text);
                                    command.Parameters.AddWithValue("@dis", TBDisc.Text);
                                    command.Parameters.AddWithValue("@taxper", TBTaxPer.Text);
                                    command.Parameters.AddWithValue("@othcharg", TBOthChrgs.Text);
                                    command.Parameters.AddWithValue("@Adv", TBAdvance.Text);
                                    command.Parameters.AddWithValue("@bal", TBBalance.Text);
                                    command.Parameters.AddWithValue("@Ttl", lblttls.Text);
                                    command.Parameters.AddWithValue("@grntttl", TBTtl.Text);
                                    command.Parameters.AddWithValue("@ttl_unts", TBUnit.Trim());

                                    command.ExecuteNonQuery();
                                }
                            }

                            #endregion

                            #region Stock
                            
                            //Detail Stock

                            DataTable dtstkqt = new DataTable();

                            command.CommandText = "select Dstk_Qty from tbl_Dstk where ProductID = " + prodtid.Trim() + " and Dstk_unt='" + TBUnit.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                            command.CommandType = CommandType.Text;

                            SqlDataAdapter Adapte = new SqlDataAdapter(command);
                            Adapte.Fill(dtstkqt);

                            if (dtstkqt.Rows.Count > 0)
                            {
                                for (int t = 0; t < dtstkqt.Rows.Count; t++)
                                {
                                    stkqty = dtstkqt.Rows[t]["Dstk_Qty"].ToString();

                                    if (Convert.ToDecimal(salqty) > Convert.ToDecimal(TBItmQty))
                                    {
                                        decimal qty = Convert.ToDecimal(salqty) - Convert.ToDecimal(TBItmQty);

                                        decimal qt = Convert.ToDecimal(stkqty) + Convert.ToDecimal(qty);

                                        command.CommandText = " UPDATE tbl_Dstk SET Dstk_Qty = " + qt + " where  ProductID = " + prodtid.Trim() + " and Dstk_unt='" + TBUnit.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                                        command.ExecuteNonQuery();
                                    }
                                    else if (Convert.ToDecimal(salqty) < Convert.ToDecimal(TBItmQty))
                                    {
                                        decimal qty =  Convert.ToDecimal(TBItmQty) - Convert.ToDecimal(salqty);

                                        decimal qt = Convert.ToDecimal(stkqty) - Convert.ToDecimal(qty);


                                        command.CommandText = " UPDATE tbl_Dstk SET Dstk_Qty = " + qt + " where  ProductID = " + prodtid.Trim() + " and Dstk_unt='" + TBUnit.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                                        command.ExecuteNonQuery();
                                    }

                                }
                            }
                            #endregion

                            #region CreditCash
                            int chk = 0;

                            if (chk == 0)
                            {
                                command.CommandText = "select CredAmt from tbl_Salcredit where CustomerID='" + lbl_Acc.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                                command.CommandType = CommandType.Text;

                                SqlDataAdapter stksavcre = new SqlDataAdapter(command);

                                DataTable dtsavcre = new DataTable();
                                stksavcre.Fill(dtsavcre);
                                decimal creamt;
                                decimal recv = 0;
                                decimal remain = 0;
                                if (dtsavcre.Rows.Count > 0)
                                {
                                    creamt = Convert.ToDecimal(dtsavcre.Rows[0]["CredAmt"]);

                                    //recv = Convert.ToDecimal(creamt) - Convert.ToDecimal(TBAdvance.Text.Trim());
                                    if (creamt == 0)
                                    {
                                        //if (Convert.ToDecimal(salqty) > Convert.ToDecimal(TBItmQty))
                                        //{
                                            recv = Convert.ToDecimal(creamt) - Convert.ToDecimal(TBBalance.Text.Trim());
                                            remain = recv; //Convert.ToDecimal(recv) + Convert.ToDecimal(TBAdvance.Text.Trim());
                                        //}
                                        //else if (Convert.ToDecimal(salqty) < Convert.ToDecimal(TBItmQty))
                                        //{

                                        //    recv = Convert.ToDecimal(creamt) + Convert.ToDecimal(TBBalance.Text.Trim());
                                        //    remain = recv; //Convert.ToDecimal(recv) + Convert.ToDecimal(TBAdvance.Text.Trim());

                                        //}
                                    }
                                    else
                                    {   

                                        //if (Convert.ToDecimal(salqty) > Convert.ToDecimal(TBItmQty))
                                        //{
                                        recv = Convert.ToDecimal(creamt) - Convert.ToDecimal(TBBalance.Text.Trim());
                                            remain = recv; //Convert.ToDecimal(recv) + Convert.ToDecimal(TBAdvance.Text.Trim());
                                        //}
                                        //else if (Convert.ToDecimal(salqty) < Convert.ToDecimal(TBItmQty))
                                        //{

                                        //    recv = Convert.ToDecimal(creamt) + Convert.ToDecimal(TBAdvance.Text.Trim());
                                        //    remain = recv; //Convert.ToDecimal(recv) + Convert.ToDecimal(TBAdvance.Text.Trim());

                                        //}
                                    }

                                    command.CommandText = " Update tbl_Salcredit set CredAmt = '" + remain + "' where CustomerID='" + lbl_Acc.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                                    command.CommandType = CommandType.Text;
                                    command.ExecuteNonQuery();
                                    chk = 1;
                                }
                                else
                                {
                                    command.CommandText = " insert into tbl_Salcredit (CustomerID,CredAmt,CompanyId,BranchId) values('" + lbl_Acc.Text.Trim() + "','" + TBBalance.Text.Trim() + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                                    command.CommandType = CommandType.Text;
                                    command.ExecuteNonQuery();
                                    chk = 1;

                                }
                            }
                            else if (chk == 1)
                            {
                                // Do nothing
                            }
                            #endregion

                            #region For Accounts

                            //Accounts

                            dt_ = new DataTable();
                            command.CommandText = "select SubHeadCategoriesGeneratedID,SubHeadCategoriesName from SubHeadCategories where SubHeadGeneratedID='0011' " +
                                " and SubHeadCategoriesName='" + TBCust.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] +"' and BranchId='" + Session["BranchID"] + "'";

                            command.CommandType = CommandType.Text;

                            SqlDataAdapter adpchkcus = new SqlDataAdapter(command);
                            adpchkcus.Fill(dt_);

                            if (dt_.Rows.Count > 0)
                            {
                                accno = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                                acctitle = dt_.Rows[0]["SubHeadCategoriesName"].ToString();
                            }
                            else
                            {
                                accno = "00118";
                                acctitle = "walk-in";
                            }

                            DataTable dtop = new DataTable();

                            command.CommandText = "";
                            command.CommandText = " select openbal,openingBal from tbl_expenses where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                            SqlDataAdapter adpopnbal = new SqlDataAdapter(command);

                            adpopnbal.Fill(dtop);

                            i = dtop.Rows.Count;

                            while (--i >= 0)
                            {

                                if (dtop.Rows[i].IsNull(0)) dtop.Rows.RemoveAt(i);

                            }

                            if (dtop.Rows.Count > 0)
                            {
                                for (int k = 0; k < dtop.Rows.Count; k++)
                                {
                                    openingbal = dtop.Rows[k]["openbal"].ToString();
                                }

                                openbal = Convert.ToDecimal(openingbal);//Convert.ToDecimal(openingbal) - Convert.ToDecimal(lblttls.Text);
                            }


                            command.Parameters.Clear();

                            command.CommandText = "expenseupdate";
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@acctitle", acctitle.Trim());
                            command.Parameters.AddWithValue("@accno", accno.Trim());
                            command.Parameters.AddWithValue("@expensesdat", lbl_dat.Text.Trim());
                            command.Parameters.AddWithValue("@billno", lbl_BillNo.Text.Trim());
                            command.Parameters.AddWithValue("@typeofpay", "Cash");
                            command.Parameters.AddWithValue("@expencermk", "");
                            command.Parameters.AddWithValue("@cashamt", TBAdvance.Text.Trim());
                            command.Parameters.AddWithValue("@CashBnk_id", "1");
                            command.Parameters.AddWithValue("@CashBnk_nam", "");
                            command.Parameters.AddWithValue("@bankamt", "");
                            command.Parameters.AddWithValue("@PaymentIn", TBAdvance.Text.Trim());
                            command.Parameters.AddWithValue("@PaymentOut", "0");
                            command.Parameters.AddWithValue("@Amountpaid", TBAdvance.Text.Trim());
                            command.Parameters.AddWithValue("@prevbal", TBBalance.Text.Trim());
                            command.Parameters.AddWithValue("@createat", DateTime.Now.ToString());
                            command.Parameters.AddWithValue("@createby", Session["Username"]);
                            command.Parameters.AddWithValue("@companyid", Session["CompanyID"]);
                            command.Parameters.AddWithValue("@branchid", Session["BranchID"]);
                            command.Parameters.AddWithValue("@ChqDat", DateTime.Now.ToString());
                            command.Parameters.AddWithValue("@ChqNO", "");
                            command.Parameters.AddWithValue("@OpenBal", openbal);
                            command.Parameters.AddWithValue("@openingBal", lbl_openbalance1.Text);
                            command.Parameters.AddWithValue("@opening_balance", lbl_Openbalance.Text.Trim());

                            command.ExecuteNonQuery();

                            Btn_Cancl.Visible = false;

                            #endregion
                        }

                    }
                    else if (HFHold.Value != "0")
                    {
                        #region POS
                        command.CommandText = " select * from SubHeadCategories where SubHeadCategoriesGeneratedID = '" + lbl_Acc.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] +"' and BranchId='" + Session["BranchID"] + "'";

                        command.CommandType = CommandType.Text;

                        dt_ = new DataTable();

                        SqlDataAdapter custadp = new SqlDataAdapter(command);

                        custadp.Fill(dt_);

                        if (dt_.Rows.Count > 0)
                        {
                            custid = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                        }
                        else
                        {
                            custid = "00118";
                        }

                        command.Parameters.Clear();
                        command.CommandText = "sp_updmcpos";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@msalid", HFCposid.Value);
                        command.Parameters.AddWithValue("@BillNO", lbl_BillNo.Text);
                        command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                        command.Parameters.AddWithValue("@BranchId", Session["BranchID"]);
                        command.Parameters.AddWithValue("@Username", Session["Username"]);
                        command.Parameters.AddWithValue("@billdat", lbl_dat.Text);
                        command.Parameters.AddWithValue("@billtim", txtClock.Value);
                        command.Parameters.AddWithValue("@customerid", custid.Trim());
                        command.Parameters.AddWithValue("@CustomerName", TBCust.Text);
                        command.Parameters.AddWithValue("@CellNo1", "");
                        command.Parameters.AddWithValue("@cust_Due", lbldue.Text);
                        command.Parameters.AddWithValue("@custtyp", lb_custtyp.Text);
                        command.Parameters.AddWithValue("@createdby", Session["Username"]);
                        command.Parameters.AddWithValue("@createdat", DateTime.Now.ToString());
                        command.Parameters.AddWithValue("@createterminal", Session["BranchID"]);
                        command.Parameters.AddWithValue("@updatedby", "");
                        command.Parameters.AddWithValue("@updatedat", "");
                        command.Parameters.AddWithValue("@updateterminal", "");
                        command.Parameters.AddWithValue("@ProductID", "0");
                        command.Parameters.AddWithValue("@salprice", "0");
                        command.Parameters.AddWithValue("@ProQty", "0");
                        command.Parameters.AddWithValue("@Amt", "0");
                        command.Parameters.AddWithValue("@ttl_qty", "0");
                        command.Parameters.AddWithValue("@ttl_itms", lbl_itmqty.Text);
                        command.Parameters.AddWithValue("@dis", TBDisc.Text);
                        command.Parameters.AddWithValue("@taxper", TBTaxPer.Text);
                        command.Parameters.AddWithValue("@othcharg", TBOthChrgs.Text);
                        command.Parameters.AddWithValue("@Adv", TBAdvance.Text);
                        command.Parameters.AddWithValue("@bal", TBBalance.Text);
                        command.Parameters.AddWithValue("@Ttl", lblttls.Text);
                        command.Parameters.AddWithValue("@grntttl", TBTtl.Text);
                        command.Parameters.AddWithValue("@ttl_unts", "");
                        command.Parameters.AddWithValue("@Ishold", "0");
                        command.Parameters.AddWithValue("@Tax", TBTaxPer.Text.Trim());
                        command.Parameters.AddWithValue("@OthTax", TBOthChrgs.Text.Trim());

                        command.ExecuteNonQuery();

                        foreach (GridViewRow g1 in GV_POS.Rows)
                        {
                            TBItms = (g1.FindControl("TBItms") as TextBox).Text;
                            tbsalpris = (g1.FindControl("tbsalpris") as TextBox).Text;
                            TBItmQty = (g1.FindControl("TBItmQty") as TextBox).Text;
                            TBUnit = (g1.FindControl("TBUnit") as TextBox).Text;

                            lblttl = (g1.FindControl("lblttl") as Label).Text;
                            HFDSal = (g1.FindControl("HFDSal") as HiddenField).Value;
                            HFPROID = (g1.FindControl("HFPROID") as HiddenField).Value;

                            if (HFDSal != "0" && HFDSal != "")
                            {
                                command.CommandText = " select productid from Products where Pro_Code='" + TBItms.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                                command.CommandType = CommandType.Text;
                                DataTable dtgetproid = new DataTable();

                                SqlDataAdapter adpgetproid = new SqlDataAdapter(command);

                                adpgetproid.Fill(dtgetproid);

                                if (dtgetproid.Rows.Count > 0)
                                {
                                    prodtid = dtgetproid.Rows[0]["productid"].ToString();
                                }

                                command.Parameters.Clear();
                                command.CommandText = "sp_upddcpos";
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@msalid", HFCposid.Value.Trim());
                                command.Parameters.AddWithValue("@dsalid", HFDSal.Trim());
                                command.Parameters.AddWithValue("@BillNO", lbl_BillNo.Text);
                                command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                                command.Parameters.AddWithValue("@BranchId", Session["BranchID"]);
                                command.Parameters.AddWithValue("@Username", Session["Username"]);
                                command.Parameters.AddWithValue("@billdat", lbl_dat.Text);
                                command.Parameters.AddWithValue("@billtim", txtClock.Value);
                                command.Parameters.AddWithValue("@customerid", custid.Trim());
                                command.Parameters.AddWithValue("@CustomerName", TBCust.Text);
                                command.Parameters.AddWithValue("@CellNo1", "");
                                command.Parameters.AddWithValue("@cust_Due", lbldue.Text);
                                command.Parameters.AddWithValue("@custtyp", lb_custtyp.Text);
                                command.Parameters.AddWithValue("@createdby", Session["Username"]);
                                command.Parameters.AddWithValue("@createdat", DateTime.Now.ToString());
                                command.Parameters.AddWithValue("@createterminal", Session["BranchID"]);
                                command.Parameters.AddWithValue("@updatedby", "");
                                command.Parameters.AddWithValue("@updatedat", "");
                                command.Parameters.AddWithValue("@updateterminal", "");
                                command.Parameters.AddWithValue("@ProductID", prodtid);
                                command.Parameters.AddWithValue("@salprice", tbsalpris.Trim());
                                command.Parameters.AddWithValue("@ProQty", TBItmQty.Trim());
                                command.Parameters.AddWithValue("@Amt", lblttl.Trim());
                                command.Parameters.AddWithValue("@ttl_qty", lbl_ttlqty.Text);
                                command.Parameters.AddWithValue("@ttl_itms", lbl_itmqty.Text);
                                command.Parameters.AddWithValue("@dis", TBDisc.Text);
                                command.Parameters.AddWithValue("@taxper", TBTaxPer.Text);
                                command.Parameters.AddWithValue("@othcharg", TBOthChrgs.Text);
                                command.Parameters.AddWithValue("@Adv", TBAdvance.Text);
                                command.Parameters.AddWithValue("@bal", TBBalance.Text);
                                command.Parameters.AddWithValue("@Ttl", lblttls.Text);
                                command.Parameters.AddWithValue("@grntttl", TBTtl.Text);
                                command.Parameters.AddWithValue("@ttl_unts", TBUnit.Trim());

                                command.ExecuteNonQuery();
                            }
                            else if (HFDSal == "0" || HFDSal == "")
                            {
                                command.CommandText = " select productid from Products where Pro_Code='" + TBItms.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                                command.CommandType = CommandType.Text;
                                DataTable dtgetproid = new DataTable();

                                SqlDataAdapter adpgetproid = new SqlDataAdapter(command);

                                adpgetproid.Fill(dtgetproid);

                                if (dtgetproid.Rows.Count > 0)
                                {
                                    prodtid = dtgetproid.Rows[0]["productid"].ToString();
                                }

                                command.Parameters.Clear();
                                command.CommandText = "sp_dcpos";
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.AddWithValue("@msalid", HFCposid.Value.Trim());
                                command.Parameters.AddWithValue("@dsalid", "");
                                command.Parameters.AddWithValue("@BillNO", lbl_BillNo.Text);
                                command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                                command.Parameters.AddWithValue("@BranchId", Session["BranchID"]);
                                command.Parameters.AddWithValue("@Username", Session["Username"]);
                                command.Parameters.AddWithValue("@billdat", lbl_dat.Text);
                                command.Parameters.AddWithValue("@billtim", txtClock.Value);
                                command.Parameters.AddWithValue("@customerid", custid.Trim());
                                command.Parameters.AddWithValue("@CustomerName", TBCust.Text);
                                command.Parameters.AddWithValue("@CellNo1", "");
                                command.Parameters.AddWithValue("@cust_Due", lbldue.Text);
                                command.Parameters.AddWithValue("@custtyp", lb_custtyp.Text);
                                command.Parameters.AddWithValue("@createdby", Session["Username"]);
                                command.Parameters.AddWithValue("@createdat", DateTime.Now.ToString());
                                command.Parameters.AddWithValue("@createterminal", Session["BranchID"]);
                                command.Parameters.AddWithValue("@updatedby", "");
                                command.Parameters.AddWithValue("@updatedat", "");
                                command.Parameters.AddWithValue("@updateterminal", "");
                                command.Parameters.AddWithValue("@ProductID", prodtid);
                                command.Parameters.AddWithValue("@salprice", tbsalpris.Trim());
                                command.Parameters.AddWithValue("@ProQty", TBItmQty.Trim());
                                command.Parameters.AddWithValue("@Amt", lblttl.Trim());
                                command.Parameters.AddWithValue("@ttl_qty", lbl_ttlqty.Text);
                                command.Parameters.AddWithValue("@ttl_itms", lbl_itmqty.Text);
                                command.Parameters.AddWithValue("@dis", TBDisc.Text);
                                command.Parameters.AddWithValue("@taxper", TBTaxPer.Text);
                                command.Parameters.AddWithValue("@othcharg", TBOthChrgs.Text);
                                command.Parameters.AddWithValue("@Adv", TBAdvance.Text);
                                command.Parameters.AddWithValue("@bal", TBBalance.Text);
                                command.Parameters.AddWithValue("@Ttl", lblttls.Text);
                                command.Parameters.AddWithValue("@grntttl", TBTtl.Text);
                                command.Parameters.AddWithValue("@ttl_unts", TBUnit.Trim());

                                command.ExecuteNonQuery();
                            }

                        }

                        #endregion

                        #region Stock
                        //Detail Stock

                        DataTable dtstkqty = new DataTable();

                        command.CommandText = "select Dstk_Qty from tbl_Dstk where ProductID = " + prodtid.Trim() + " and Dstk_unt='" + TBUnit.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                        command.CommandType = CommandType.Text;

                        SqlDataAdapter Adapter = new SqlDataAdapter(command);
                        Adapter.Fill(dtstkqty);

                        if (dtstkqty.Rows.Count > 0)
                        {
                            stkqty = dtstkqty.Rows[0]["Dstk_Qty"].ToString();

                            int qty = Convert.ToInt32(stkqty) - Convert.ToInt32(TBItmQty);
                            command.CommandText = " UPDATE tbl_Dstk SET Dstk_Qty = " + qty + " where  ProductID = " + prodtid.Trim() + " and Dstk_unt='" + TBUnit.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                            command.ExecuteNonQuery();
                        }

                        #endregion

                        #region CreditCash

                        command.CommandText = "select CredAmt from tbl_Salcredit where CustomerID='" + lbl_Acc.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                        command.CommandType = CommandType.Text;

                        SqlDataAdapter stksavcre = new SqlDataAdapter(command);

                        DataTable dtsavcre = new DataTable();
                        stksavcre.Fill(dtsavcre);
                        decimal creamt;
                        decimal recv;
                        if (dtsavcre.Rows.Count > 0)
                        {
                            creamt = Convert.ToDecimal(dtsavcre.Rows[0]["CredAmt"]);

                            recv = Convert.ToDecimal(creamt) + Convert.ToDecimal(TBBalance.Text.Trim());

                            command.CommandText = " Update tbl_Salcredit set CredAmt = '" + recv + "' where CustomerID='" + lbl_Acc.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                            command.CommandType = CommandType.Text;
                            command.ExecuteNonQuery();

                        }
                        else
                        {
                            command.CommandText = " insert into tbl_Salcredit (CustomerID,CredAmt,CompanyId,BranchId) values('" + lbl_Acc.Text.Trim() + "','" + TBBalance.Text.Trim() + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                            command.CommandType = CommandType.Text;
                            command.ExecuteNonQuery();
                        }

                        #endregion

                        #region For Accounts

                        //Accounts

                        dt_ = new DataTable();
                        command.CommandText = "select SubHeadCategoriesGeneratedID,SubHeadCategoriesName from SubHeadCategories where SubHeadGeneratedID='0011' " +
                            " and SubHeadCategoriesName='" + TBCust.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] +"' and BranchId='" + Session["BranchID"] + "'";

                        command.CommandType = CommandType.Text;

                        SqlDataAdapter adpchkcust = new SqlDataAdapter(command);
                        adpchkcust.Fill(dt_);

                        if (dt_.Rows.Count > 0)
                        {
                            accno = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                            acctitle = dt_.Rows[0]["SubHeadCategoriesName"].ToString();

                        }
                        else
                        {
                            accno = "00118";
                            acctitle = "walk-in";
                        }

                        DataTable dto = new DataTable();

                        command.CommandText = "";
                        command.CommandText = " select openbal,openingBal from tbl_expenses where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                        SqlDataAdapter adpopenbal = new SqlDataAdapter(command);

                        adpopenbal.Fill(dto);
                        if (dto.Rows.Count > 0)
                        {
                            i = dto.Rows.Count;

                            while (--i >= 0)
                            {

                                if (dto.Rows[i].IsNull(0)) dto.Rows.RemoveAt(i);
                            }

                            for (int k = 0; k < dto.Rows.Count; k++)
                            {
                                openingbal = dto.Rows[k]["openbal"].ToString();
                            }

                            openbal = Convert.ToDecimal(openingbal);//Convert.ToDecimal(openingbal) - Convert.ToDecimal(lblttls.Text);
                        }

                        command.Parameters.Clear();

                        command.CommandText = "expense";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@acctitle", acctitle.Trim());
                        command.Parameters.AddWithValue("@accno", accno.Trim());
                        command.Parameters.AddWithValue("@expensesdat", lbl_dat.Text.Trim());
                        command.Parameters.AddWithValue("@billno", lbl_BillNo.Text.Trim());
                        command.Parameters.AddWithValue("@typeofpay", "Cash");
                        command.Parameters.AddWithValue("@expencermk", "");
                        command.Parameters.AddWithValue("@cashamt",TBAdvance.Text.Trim());
                        command.Parameters.AddWithValue("@CashBnk_id", "1");
                        command.Parameters.AddWithValue("@CashBnk_nam", "");
                        command.Parameters.AddWithValue("@bankamt", "");
                        command.Parameters.AddWithValue("@PaymentIn", TBAdvance.Text.Trim());
                        command.Parameters.AddWithValue("@PaymentOut", "0");
                        command.Parameters.AddWithValue("@Amountpaid", TBAdvance.Text.Trim());
                        command.Parameters.AddWithValue("@prevbal", TBBalance.Text.Trim());
                        command.Parameters.AddWithValue("@createat", DateTime.Now.ToString());
                        command.Parameters.AddWithValue("@createby", Session["Username"]);
                        command.Parameters.AddWithValue("@companyid", Session["CompanyID"]);
                        command.Parameters.AddWithValue("@branchid", Session["BranchID"]);
                        command.Parameters.AddWithValue("@ChqDat", DateTime.Now.ToString());
                        command.Parameters.AddWithValue("@ChqNO", "");
                        command.Parameters.AddWithValue("@OpenBal", openbal);
                        command.Parameters.AddWithValue("@openingBal", lbl_openbalance1.Text);
                        command.Parameters.AddWithValue("@opening_balance", lbl_Openbalance.Text.Trim());

                        command.ExecuteNonQuery();

                        #endregion

                        Session["chek"] = "1";

                    }
                    
                    // Attempt to commit the transaction.
                    transaction.Commit();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_POSReceipt.aspx?ID=POS&POSID=" + lbl_BillNo.Text.Trim() + "&cust=" + lbl_Acc.Text.Trim() + "','_blank','height=600px,width=400px,scrollbars=1');", true);
                    
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = "Commit Exception Type: {0}" + ex.GetType();
                lblmssg.Text = "Message: {0}" + ex.Message;

                // Attempt to roll back the transaction. 
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred 
                    // on the server that would cause the rollback to fail, such as 
                    // a closed connection.
                    lblmssg.Text = "Rollback Exception Type: {0}" + ex2.GetType();
                    lblmssg.Text = "Message: {0}" + ex2.Message;
                }
            }
            finally
            {
                con.Close();
            }

            ShowAccount();
            return 1;
        }


        private void SaveHold()
        {
            string HFPROID;

            con.Open();

            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            // Start a local transaction.
            transaction = con.BeginTransaction("POSTrans");

            // Must assign both transaction object and connection 
            // to Command object for a pending local transaction
            command.Connection = con;
            command.Transaction = transaction;

            try
            {
                command.CommandText = " select * from SubHeadCategories where SubHeadCategoriesGeneratedID = '" + lbl_Acc.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] +"' and BranchId='" + Session["BranchID"] + "'";

                command.CommandType = CommandType.Text;

                dt_ = new DataTable();

                SqlDataAdapter custadp = new SqlDataAdapter(command);

                custadp.Fill(dt_);

                if (dt_.Rows.Count > 0)
                {
                    custid = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                }
                else
                {
                    custid = "00118";
                }

                if (HFHoldId.Value == "")
                {
                    command.Parameters.Clear();
                    command.CommandText = "sp_mcpos";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@msalid", HFCposid.Value);
                    command.Parameters.AddWithValue("@BillNO", lbl_BillNo.Text);
                    command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                    command.Parameters.AddWithValue("@BranchId", Session["BranchID"]);
                    command.Parameters.AddWithValue("@Username", Session["Username"]);
                    command.Parameters.AddWithValue("@billdat", lbl_dat.Text);
                    command.Parameters.AddWithValue("@billtim", txtClock.Value);
                    command.Parameters.AddWithValue("@customerid", custid.Trim());
                    command.Parameters.AddWithValue("@CustomerName", TBCust.Text);
                    command.Parameters.AddWithValue("@CellNo1", "");
                    command.Parameters.AddWithValue("@cust_Due", lbldue.Text);
                    command.Parameters.AddWithValue("@custtyp", lb_custtyp.Text);
                    command.Parameters.AddWithValue("@createdby", Session["Username"]);
                    command.Parameters.AddWithValue("@createdat", DateTime.Now.ToString());
                    command.Parameters.AddWithValue("@createterminal", Session["BranchID"]);
                    command.Parameters.AddWithValue("@updatedby", "");
                    command.Parameters.AddWithValue("@updatedat", "");
                    command.Parameters.AddWithValue("@updateterminal", "");
                    command.Parameters.AddWithValue("@ProductID", "0");
                    command.Parameters.AddWithValue("@salprice", "0");
                    command.Parameters.AddWithValue("@ProQty", "0");
                    command.Parameters.AddWithValue("@Amt", "0");
                    command.Parameters.AddWithValue("@ttl_qty", "0");
                    command.Parameters.AddWithValue("@ttl_itms", lbl_itmqty.Text);
                    command.Parameters.AddWithValue("@dis", TBDisc.Text);
                    command.Parameters.AddWithValue("@taxper", chk_tax1.Text);
                    command.Parameters.AddWithValue("@othcharg", chk_tax2.Text);
                    command.Parameters.AddWithValue("@Adv", TBAdvance.Text);
                    command.Parameters.AddWithValue("@bal", TBBalance.Text);
                    command.Parameters.AddWithValue("@Ttl", lblttls.Text);
                    command.Parameters.AddWithValue("@grntttl", TBTtl.Text);
                    command.Parameters.AddWithValue("@ttl_unts", "");
                    command.Parameters.AddWithValue("@Ishold", "1");
                    command.Parameters.AddWithValue("@Tax", TBTaxPer.Text.Trim());
                    command.Parameters.AddWithValue("@OthTax", TBOthChrgs.Text.Trim());
                    command.Parameters.AddWithValue("@RecieverNam", TBREC.Text.Trim());
                    command.Parameters.AddWithValue("@tax1amt", TBTaxPer.Text.Trim());
                    command.Parameters.AddWithValue("@tax2amt", TBOthChrgs.Text.Trim());
                    command.Parameters.AddWithValue("@paytyp", "Cash");
                    command.Parameters.AddWithValue("@disc", TBDisc.Text.Trim());

                    command.ExecuteNonQuery();


                    command.CommandText = " select top 1 MCposid from tbl_MCPos where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "' order by MCposid desc ";

                    command.CommandType = CommandType.Text;

                    dt_ = new DataTable();

                    SqlDataAdapter adp = new SqlDataAdapter(command);

                    adp.Fill(dt_);

                    if (dt_.Rows.Count > 0)
                    {
                        //billno = dt_.Rows[0]["BillNO"].ToString();

                        HFCposid.Value = dt_.Rows[0]["MCposid"].ToString();

                        //command.CommandText = " update tbl_MCHold set IsSal='1' where BillNO='" + billno.Trim() + "'";

                        //command.CommandType = CommandType.Text;

                        //command.ExecuteNonQuery();

                    }

                    foreach (GridViewRow g1 in GV_POS.Rows)
                    {

                        TBItms = (g1.FindControl("TBItms") as TextBox).Text;
                        tbsalpris = (g1.FindControl("tbsalpris") as TextBox).Text;
                        TBItmQty = (g1.FindControl("TBItmQty") as TextBox).Text;
                        TBUnit = (g1.FindControl("TBUnit") as TextBox).Text;
                        lblttl = (g1.FindControl("lblttl") as Label).Text;
                        HFDSal = (g1.FindControl("HFDSal") as HiddenField).Value;
                        HFPROID = (g1.FindControl("HFPROID") as HiddenField).Value;


                        //For getting proid 

                        command.CommandText = " select productid from Products where Pro_Code='" + TBItms.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                        command.CommandType = CommandType.Text;
                        DataTable dtgetproid = new DataTable();

                        SqlDataAdapter adpgetproid = new SqlDataAdapter(command);

                        adpgetproid.Fill(dtgetproid);

                        if (dtgetproid.Rows.Count > 0)
                        {
                            prodtid = dtgetproid.Rows[0]["productid"].ToString();
                        }

                        
                        command.Parameters.Clear();
                        command.CommandText = "sp_dcpos";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@msalid", HFCposid.Value.Trim());
                        command.Parameters.AddWithValue("@dsalid", HFDSal.Trim());
                        command.Parameters.AddWithValue("@BillNO", lbl_BillNo.Text);
                        command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                        command.Parameters.AddWithValue("@BranchId", Session["BranchID"]);
                        command.Parameters.AddWithValue("@Username", Session["Username"]);
                        command.Parameters.AddWithValue("@billdat", lbl_dat.Text);
                        command.Parameters.AddWithValue("@billtim", txtClock.Value);
                        command.Parameters.AddWithValue("@customerid", custid.Trim());
                        command.Parameters.AddWithValue("@CustomerName", TBCust.Text);
                        command.Parameters.AddWithValue("@CellNo1", "");
                        command.Parameters.AddWithValue("@cust_Due", lbldue.Text);
                        command.Parameters.AddWithValue("@custtyp", lb_custtyp.Text);
                        command.Parameters.AddWithValue("@createdby", Session["Username"]);
                        command.Parameters.AddWithValue("@createdat", DateTime.Now.ToString());
                        command.Parameters.AddWithValue("@createterminal", Session["BranchID"]);
                        command.Parameters.AddWithValue("@updatedby", "");
                        command.Parameters.AddWithValue("@updatedat", "");
                        command.Parameters.AddWithValue("@updateterminal", "");
                        command.Parameters.AddWithValue("@ProductID", prodtid);
                        command.Parameters.AddWithValue("@salprice", tbsalpris.Trim());
                        command.Parameters.AddWithValue("@ProQty", TBItmQty.Trim());
                        command.Parameters.AddWithValue("@Amt", lblttl.Trim());
                        command.Parameters.AddWithValue("@ttl_qty", lbl_ttlqty.Text);
                        command.Parameters.AddWithValue("@ttl_itms", lbl_itmqty.Text);
                        command.Parameters.AddWithValue("@dis", TBDisc.Text);
                        command.Parameters.AddWithValue("@taxper", TBTaxPer.Text);
                        command.Parameters.AddWithValue("@othcharg", TBOthChrgs.Text);
                        command.Parameters.AddWithValue("@Adv", TBAdvance.Text);
                        command.Parameters.AddWithValue("@bal", TBBalance.Text);
                        command.Parameters.AddWithValue("@Ttl", lblttls.Text);
                        command.Parameters.AddWithValue("@grntttl", TBTtl.Text);
                        command.Parameters.AddWithValue("@ttl_unts", TBUnit.Trim());

                        command.ExecuteNonQuery();
                    }
                }
                else if (HFHoldId.Value != "")
                {
                   
                    // Master Counter POS

                    command.CommandText = " select * from SubHeadCategories where SubHeadCategoriesGeneratedID = '" + lbl_Acc.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] +"' and BranchId='" + Session["BranchID"] + "'";

                    command.CommandType = CommandType.Text;

                    dt_ = new DataTable();

                    SqlDataAdapter custsadp = new SqlDataAdapter(command);

                    custsadp.Fill(dt_);

                    if (dt_.Rows.Count > 0)
                    {
                        custid = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                    }
                    else
                    {
                        custid = "00118";
                    }

                    command.Parameters.Clear();
                    command.CommandText = "sp_updmcpos";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@msalid", HFCposid.Value);
                    command.Parameters.AddWithValue("@BillNO", lbl_BillNo.Text);
                    command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                    command.Parameters.AddWithValue("@BranchId", Session["BranchID"]);
                    command.Parameters.AddWithValue("@Username", Session["Username"]);
                    command.Parameters.AddWithValue("@billdat", lbl_dat.Text);
                    command.Parameters.AddWithValue("@billtim", txtClock.Value);
                    command.Parameters.AddWithValue("@customerid", custid.Trim());
                    command.Parameters.AddWithValue("@CustomerName", TBCust.Text);
                    command.Parameters.AddWithValue("@CellNo1", "");
                    command.Parameters.AddWithValue("@cust_Due", lbldue.Text);
                    command.Parameters.AddWithValue("@custtyp", lb_custtyp.Text);
                    command.Parameters.AddWithValue("@createdby", Session["Username"]);
                    command.Parameters.AddWithValue("@createdat", DateTime.Now.ToString());
                    command.Parameters.AddWithValue("@createterminal", Session["BranchID"]);
                    command.Parameters.AddWithValue("@updatedby", "");
                    command.Parameters.AddWithValue("@updatedat", "");
                    command.Parameters.AddWithValue("@updateterminal", "");
                    command.Parameters.AddWithValue("@ProductID", "0");
                    command.Parameters.AddWithValue("@salprice", "0");
                    command.Parameters.AddWithValue("@ProQty", "0");
                    command.Parameters.AddWithValue("@Amt", "0");
                    command.Parameters.AddWithValue("@ttl_qty", "0");
                    command.Parameters.AddWithValue("@ttl_itms", lbl_itmqty.Text);
                    command.Parameters.AddWithValue("@dis", TBDisc.Text);
                    command.Parameters.AddWithValue("@taxper", TBTaxPer.Text);
                    command.Parameters.AddWithValue("@othcharg", TBOthChrgs.Text);
                    command.Parameters.AddWithValue("@Adv", TBAdvance.Text);
                    command.Parameters.AddWithValue("@bal", TBBalance.Text);
                    command.Parameters.AddWithValue("@Ttl", lblttls.Text);
                    command.Parameters.AddWithValue("@grntttl", TBTtl.Text);
                    command.Parameters.AddWithValue("@ttl_unts", "");
                    command.Parameters.AddWithValue("@Ishold", "1");
                    command.Parameters.AddWithValue("@Tax", TBTaxPer.Text.Trim());
                    command.Parameters.AddWithValue("@OthTax", TBOthChrgs.Text.Trim());

                    command.ExecuteNonQuery();

                    foreach (GridViewRow g1 in GV_POS.Rows)
                    {

                        TBItms = (g1.FindControl("TBItms") as TextBox).Text;
                        tbsalpris = (g1.FindControl("tbsalpris") as TextBox).Text;
                        TBItmQty = (g1.FindControl("TBItmQty") as TextBox).Text;
                        TBUnit = (g1.FindControl("TBUnit") as TextBox).Text;

                        lblttl = (g1.FindControl("lblttl") as Label).Text;
                        HFDSal = (g1.FindControl("HFDSal") as HiddenField).Value;
                        HFPROID = (g1.FindControl("HFPROID") as HiddenField).Value;
                        HFDSal = (g1.FindControl("HFDSal") as HiddenField).Value;



                        if (HFDSal != "0" && HFDSal != "")
                        {

                            //For getting proid 

                            command.CommandText = " select productid from Products where Pro_Code='" + TBItms.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                            command.CommandType = CommandType.Text;
                            DataTable dtgetproid = new DataTable();

                            SqlDataAdapter adpgetproid = new SqlDataAdapter(command);

                            adpgetproid.Fill(dtgetproid);

                            if (dtgetproid.Rows.Count > 0)
                            {
                                prodtid = dtgetproid.Rows[0]["productid"].ToString();
                            }

                            command.Parameters.Clear();
                            command.CommandText = "sp_upddcpos";
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@msalid", HFCposid.Value.Trim());
                            command.Parameters.AddWithValue("@dsalid", HFDSal.Trim());
                            command.Parameters.AddWithValue("@BillNO", lbl_BillNo.Text);
                            command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                            command.Parameters.AddWithValue("@BranchId", Session["BranchID"]);
                            command.Parameters.AddWithValue("@Username", Session["Username"]);
                            command.Parameters.AddWithValue("@billdat", lbl_dat.Text);
                            command.Parameters.AddWithValue("@billtim", txtClock.Value);
                            command.Parameters.AddWithValue("@customerid", custid.Trim());
                            command.Parameters.AddWithValue("@CustomerName", TBCust.Text);
                            command.Parameters.AddWithValue("@CellNo1", "");
                            command.Parameters.AddWithValue("@cust_Due", lbldue.Text);
                            command.Parameters.AddWithValue("@custtyp", lb_custtyp.Text);
                            command.Parameters.AddWithValue("@createdby", Session["Username"]);
                            command.Parameters.AddWithValue("@createdat", DateTime.Now.ToString());
                            command.Parameters.AddWithValue("@createterminal", Session["BranchID"]);
                            command.Parameters.AddWithValue("@updatedby", "");
                            command.Parameters.AddWithValue("@updatedat", "");
                            command.Parameters.AddWithValue("@updateterminal", "");
                            command.Parameters.AddWithValue("@ProductID", prodtid.Trim());
                            command.Parameters.AddWithValue("@salprice", tbsalpris.Trim());
                            command.Parameters.AddWithValue("@ProQty", TBItmQty.Trim());
                            command.Parameters.AddWithValue("@Amt", lblttl.Trim());
                            command.Parameters.AddWithValue("@ttl_qty", lbl_ttlqty.Text);
                            command.Parameters.AddWithValue("@ttl_itms", lbl_itmqty.Text);
                            command.Parameters.AddWithValue("@dis", TBDisc.Text);
                            command.Parameters.AddWithValue("@taxper", TBTaxPer.Text);
                            command.Parameters.AddWithValue("@othcharg", TBOthChrgs.Text);
                            command.Parameters.AddWithValue("@Adv", TBAdvance.Text);
                            command.Parameters.AddWithValue("@bal", TBBalance.Text);
                            command.Parameters.AddWithValue("@Ttl", lblttls.Text);
                            command.Parameters.AddWithValue("@grntttl", TBTtl.Text);
                            command.Parameters.AddWithValue("@ttl_unts", TBUnit.Trim());

                            command.ExecuteNonQuery();

                        }
                        else if (HFDSal == "0" || HFDSal == "")
                        {

                            command.Parameters.Clear();
                            command.CommandText = "sp_dcpos";
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@msalid", HFCposid.Value.Trim());
                            command.Parameters.AddWithValue("@dsalid", HFDSal.Trim());
                            command.Parameters.AddWithValue("@BillNO", lbl_BillNo.Text);
                            command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"]);
                            command.Parameters.AddWithValue("@BranchId", Session["BranchID"]);
                            command.Parameters.AddWithValue("@Username", Session["Username"]);
                            command.Parameters.AddWithValue("@billdat", lbl_dat.Text);
                            command.Parameters.AddWithValue("@billtim", txtClock.Value);
                            command.Parameters.AddWithValue("@customerid", custid.Trim());
                            command.Parameters.AddWithValue("@CustomerName", TBCust.Text);
                            command.Parameters.AddWithValue("@CellNo1", "");
                            command.Parameters.AddWithValue("@cust_Due", lbldue.Text);
                            command.Parameters.AddWithValue("@custtyp", lb_custtyp.Text);
                            command.Parameters.AddWithValue("@createdby", Session["Username"]);
                            command.Parameters.AddWithValue("@createdat", DateTime.Now.ToString());
                            command.Parameters.AddWithValue("@createterminal", Session["BranchID"]);
                            command.Parameters.AddWithValue("@updatedby", "");
                            command.Parameters.AddWithValue("@updatedat", "");
                            command.Parameters.AddWithValue("@updateterminal", "");
                            command.Parameters.AddWithValue("@ProductID", prodtid.Trim());
                            command.Parameters.AddWithValue("@salprice", tbsalpris.Trim());
                            command.Parameters.AddWithValue("@ProQty", TBItmQty.Trim());
                            command.Parameters.AddWithValue("@Amt", lblttl.Trim());
                            command.Parameters.AddWithValue("@ttl_qty", lbl_ttlqty.Text);
                            command.Parameters.AddWithValue("@ttl_itms", lbl_itmqty.Text);
                            command.Parameters.AddWithValue("@dis", TBDisc.Text);
                            command.Parameters.AddWithValue("@taxper", TBTaxPer.Text);
                            command.Parameters.AddWithValue("@othcharg", TBOthChrgs.Text);
                            command.Parameters.AddWithValue("@Adv", TBAdvance.Text);
                            command.Parameters.AddWithValue("@bal", TBBalance.Text);
                            command.Parameters.AddWithValue("@Ttl", lblttls.Text);
                            command.Parameters.AddWithValue("@grntttl", TBTtl.Text);
                            command.Parameters.AddWithValue("@ttl_unts", TBUnit.Trim());

                            command.ExecuteNonQuery();
                        }
                    }
                                        
                }
                // Attempt to commit the transaction.

                transaction.Commit();
                clear();
            
            }
            catch (Exception ex)
            {
                lblmssg.Text = "Commit Exception Type: {0}" + ex.GetType();
                lblmssg.Text = "Message: {0}" + ex.Message;

                // Attempt to roll back the transaction. 
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred 
                    // on the server that would cause the rollback to fail, such as 
                    // a closed connection.
                    lblmssg.Text = "Rollback Exception Type: {0}" + ex2.GetType();
                    lblmssg.Text = "Message: {0}" + ex2.Message;
                }
            }
            finally
            {
                con.Close();
                //Response.Redirect("frm_PSal.aspx");
                //Response.Redirect("frm_PSal.aspx");
            }
            ShowAccount();
            data();
        }


        private int SaveCust1()
        {

            con.Open();

            int res;

            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            // Start a local transaction.
            transaction = con.BeginTransaction("SampleTransaction");

            // Must assign both transaction object and connection 
            // to Command object for a pending local transaction
            command.Connection = con;
            command.Transaction = transaction;

            try
            {
                #region Customers

                

                command.CommandText = " INSERT INTO [Customers_] ([CustomerName] ,[GST] ,[category] ,[NTN] , " +
                    "[customertype_],[areaid] " +
                    " ,[Area] ,[saleper] ,[District] ,[PhoneNo] ,[Email],[CellNo1],[PostalCode],[CellNo2] " +
                    " ,[PostalOfficeContact],[CellNo3] ,[NIC] ,[CellNo4],[city_],[Address] ,[CreatedBy] " +
                    " ,[CreatedDate] ,[IsActive] ,[Cus_Code] ,[CompanyId] ,[BranchId])  VALUES " +
                    " ('" + TB_CustNam.Text + "' ,'GST','', '" + TBNTN.Text + "'" +
                    " ,'' ,'','','0.00' ,'','','','','','','','','','','','','" + Session["user"].ToString() + "','" + DateTime.Now + "'" +
                    " ,'true' ,'','" + Session["CompanyID"] + "','" + Session["BranchID"] + "' )";

                command.ExecuteNonQuery();

                #endregion


                #region Accounts


                command.CommandText = " INSERT INTO [SubHeadCategories] ([ven_id] ,[SubHeadCategoriesName] ,[SubHeadCategoriesGeneratedID] ,[HeadGeneratedID] , " +
                   "[SubHeadGeneratedID],[CreatedAt] " +
                   " ,[CreatedBy] ,[SubCategoriesKey])  VALUES " +
                   " ('1', '" + TB_CustNam.Text + "' ,'" + "0011" + SubHeadCat.Value + "','001', '0011','" + DateTime.Now + "'" +
                   ",'" + Session["user"] + "','" + "0011" + SubHeadCat.Value + "' )";

                command.ExecuteNonQuery();


               

                #endregion


    
                // Attempt to commit the transaction.
                transaction.Commit();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);

                // Attempt to roll back the transaction. 
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred 
                    // on the server that would cause the rollback to fail, such as 
                    // a closed connection.
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }
            finally
            {
                con.Close();
                res = 1;

            }

            return res;
        }

        public void AutoID()
        {
            ShowAccountCategoryID();
        }

        public void ShowAccountCategoryID()
        {
            try
            {

                str = "select SubHeadCategoriesID from SubHeadCategories where CompanyId='" + Session["CompanyID"] +"' and BranchId='" + Session["BranchID"] + "' order by SubHeadCategoriesID desc";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                DataTable dt_ = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt_);
                if (dt_.Rows.Count <= 0)
                {
                    SubHeadCat.Value = "1";
                }
                else
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (SubHeadCat.Value == "0")
                        {
                            int v = Convert.ToInt32(reader["SubHeadCategoriesID"].ToString());
                            int b = v + 1;
                            SubHeadCat.Value = b.ToString();

                        }
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int UpdCust()
        {
            int j = 1;


            string query = " UPDATE Customers_ SET CustomerName ='" + TB_CustNam.Text + "' , CellNo1='' ,CreatedBy='' , CreatedDate ='' ,left_eye='" + CHK_LeftEye.Checked + "', right_eye='" + CHK_RightEye.Checked + "',RSph='" + TBRSphl.Text + "',RCyl='" + TBRCyln.Text + "',RAxis='" + TBRAXSIS.Text + "',LSph='" + TBLSphl.Text + "',LCyl='" + TBLCyln.Text + "',LAsix='" + TBLAXSIS.Text + "' where CustomerID = '" + HFCustID.Value.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

            con.Open();


            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                cmd.ExecuteNonQuery();

            }
            con.Close();

            return j;
        }
        private int DelCust()
        {
            int j = 1;


            string query = " Delete from Customers_ where CellNo1 ='" + TBNTN.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

            con.Open();


            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                cmd.ExecuteNonQuery();

            }
            con.Close();

            return j;
        }

        protected void BSave_Click(object sender, EventArgs e)
        {
            int res = 0;
            //if (Page.IsValid)
            //{
                try
                {
                    if (HFCustID.Value == "")
                    {
                       res = SaveCust1();
                    }
                    else
                    {
                        res = UpdCust();
                    }
                    if (res == 1)
                    {
                        Response.Redirect("frm_PSal.aspx", false);
                    }
                }
                catch (Exception ex)
                {
                    lblerr.Text = ex.Message;
                }
            //}
        }


        public void cusclear()
        {
            TB_CustNam.Text = "";
            TBNTN.Text = "";            
            ModalPopupExtender1.Show();
            CHK_RightEye.Checked = false;
            CHK_LeftEye.Checked = false;
            TBRSphl.Text = "";
            TBRCyln.Text = "";
            TBRAXSIS.Text = "";
            TBLSphl.Text = "";
            TBLCyln.Text = "";
            TBRADD.Text = "";
            TBLADD.Text = "";
            TBLAXSIS.Text = "";
            HFMobNo.Value = "";
            HFCustID.Value = "";
            lnkbtn_del.Visible = false;
            lblerr.Text = "";
        }

        protected void btn_disrevert_Click(object sender, EventArgs e)
        {
            try
            {
                TBDisc.Text = "0";
                lblttls.Text = TBTtl.Text;
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }
        public void clear()
        {
            SetInitRowPuritm();
            TBAdvance.Text = "0.00";
            TBBalance.Text = "0.00";
            TBTtl.Text = "0.00";
            TBCust.Text = "walk-in";
            TB_SalNO.Text = "";
            ckh_right.Checked = false;
            TBRCyl.Text = "";
            TBRSph.Text = "";
            TBRAxis.Text = "";
            chk_left.Checked = false;
            TBLCyl.Text = "";
            TBLSph.Text = "";
            TBLAxis.Text = "";
            lblmssg.Text = "";
            TBRAdd_.Text = "";
            TBLAdd_.Text = "";
            lblmssg.Text = ""; HFSALHIS.Value = "";
            HFMobNo.Value = "";
            HFCustID.Value = "";
            HFAccountCategoryName.Value = "";
            lblttls.Text = "0.00";
            lbl_itmqty.Text = "0.00";
            lbl_ttlqty.Text = "0.00";
            lbl_Acc.Text = "00118";
            HFHoldId.Value = "";
            HFHold.Value = "0";
            HFCposid.Value = "0";
            chk_tax1.Checked = false;
            chk_tax2.Checked = false;
            TBTaxPer.Text= "0.00";
            TBOthChrgs.Text = "0.00";
            TBDisc.Text = "0";
        }

        protected void btn_Prosav_Click(object sender, EventArgs e)
        {

        }
        protected void btn_Procancl_Click(object sender, EventArgs e)
        {

        }
        protected void BReset_Click(object sender, EventArgs e)
        {
            cusclear();
            ModalPopupExtender1.Show();
        }

        protected void Btn_Hold_Click(object sender, EventArgs e)
        {
            //if (Page.IsValid)
            //{
                try
                {
                    
                        SaveHold();
                    
                }
                catch (Exception ex)
                {
                    lblmssg.Text = ex.Message;
                }
            //}
        }

        protected void TBBillNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                str = " select tbl_MCPos.BillNO,billdat,CustomerName from tbl_MCPos " +
                      " inner join tbl_DCPos on tbl_MCPos.MCposid = tbl_DCPos.MCposid where " +
                      " tbl_MCPos.BillNO ='" + TBBillNo.Text.Trim() + "' and ishold = '1' and Iscancel = '0' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(str);

                if (dt_.Rows.Count > 0)
                {
                    GVHoldList.DataSource = dt_;
                    GVHoldList.DataBind();
                    ModalPopupExtender2.Show();
                }
                else
                {
                    ModalPopupExtender2.Hide();
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        protected void TBSalhisdat_TextChanged(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( './GV_DSR.aspx?ID=SAL&DSRDat=" + TBSalhisdat.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
            //TBSalhisdat.Text = "";
        }

        protected void btn_dsr_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( './GV_DSR.aspx?ID=SAL&DSRFDat=" + TBSalhisfdat.Text.Trim() + "&DSRTDat=" + TBSalhistdat.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
            TBSalhisfdat.Text = "";
            TBSalhistdat.Text = "";
        }
        protected void GVHoldList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row;


                if (e.CommandName == "Select")
                {
                    row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    string BillNO = GVHoldList.DataKeys[row.RowIndex].Values[0].ToString();

                    //string query1 = " select distinct(tbl_MCHold.BillNO),billdat,billtim,CustomerName,CellNo1,cust_Due,ttl_itms ,ttl_qty, " +
                      //  " dis,taxper,othcharg,Adv,bal, grntttl,Ttl,custtyp,custacc from tbl_MCHold " +
                        //" inner join tbl_DCHold on tbl_MCHold.MCholdid = tbl_DCHold.MCholdid where tbl_MCHold.BillNO ='" + BillNO.Trim() + "'";

                    //updated on 10-10-2019

                    string query1 = " select distinct(tbl_mcpos.BillNO),tbl_mcpos.MCposid,billdat,billtim,CustomerName,CellNo1,cust_Due,ttl_itms , " +
                        "  ttl_qty,  dis,taxper,othcharg,Adv,tbl_mcpos.bal,RecieverNam, tax2amt, disc,  tax1amt, paytyp, grntttl,Ttl,custtyp,tbl_mcpos.customerid as [custacc] " +
                        "  from tbl_mcpos  inner " +
                        " join tbl_dcpos on tbl_mcpos.MCposid = tbl_dcpos.MCposid where tbl_mcpos.BillNO ='" + BillNO.Trim() + "' and Ishold = '1' and tbl_mcpos.CompanyId='" + Session["CompanyID"] + "' and tbl_mcpos.BranchId='" + Session["BranchID"] + "'";

                    DataTable dthold = new DataTable();
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query1, con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dthold);

                    if (dthold.Rows.Count > 0)
                    {
                        HFHoldId.Value = dthold.Rows[0]["MCposid"].ToString();
                        lbl_BillNo.Text = dthold.Rows[0]["BillNO"].ToString();
                        lbl_dat.Text = dthold.Rows[0]["billdat"].ToString();
                        TBCust.Text = dthold.Rows[0]["CustomerName"].ToString();                        
                        lbldue.Text = dthold.Rows[0]["cust_Due"].ToString();
                        lbl_itmqty.Text = dthold.Rows[0]["ttl_itms"].ToString();
                        lbl_ttlqty.Text = dthold.Rows[0]["ttl_qty"].ToString();
                        TBDisc.Text = dthold.Rows[0]["dis"].ToString();
                        TBTaxPer.Text = dthold.Rows[0]["taxper"].ToString();
                        TBOthChrgs.Text = dthold.Rows[0]["othcharg"].ToString();
                        TBAdvance.Text = dthold.Rows[0]["Adv"].ToString();
                        TBBalance.Text = dthold.Rows[0]["bal"].ToString();
                        TBTtl.Text = dthold.Rows[0]["grntttl"].ToString();
                        lblttls.Text = dthold.Rows[0]["Ttl"].ToString();
                        lb_custtyp.Text = dthold.Rows[0]["custtyp"].ToString();
                        lbl_Acc.Text = dthold.Rows[0]["custacc"].ToString();
                        TBREC.Text = dthold.Rows[0]["RecieverNam"].ToString();
                        //DDL_typofpayments.SelectedValue = dt_.Rows[0]["paytyp"].ToString();
                        TBTaxPer.Text = dthold.Rows[0]["tax1amt"].ToString();

                        if (TBTaxPer.Text != "0" && TBTaxPer.Text != "")
                        {
                            chk_tax1.Checked = true;
                        }
                        else
                        {
                            chk_tax1.Checked = false;
                        }
                        TBOthChrgs.Text = dthold.Rows[0]["tax2amt"].ToString();

                        if (TBOthChrgs.Text != "0" && TBOthChrgs.Text != "")
                        {
                            chk_tax2.Checked = true;
                        }
                        else
                        {
                            chk_tax2.Checked = false;
                        }

                        TBDisc.Text = dthold.Rows[0]["disc"].ToString();   
                    }

                    con.Close();

                    //query1 = " select distinct(tbl_DCHold.ProductID), tbl_MCHold.MCholdid, ProductName as [ItemDesc], ProID as [Items], ProductName as [ItemDesc],tbl_DCHold.ttl_unts as [UNIT],salprice as [salpric],ProQty as [QTY], " +
                      //  " Amt as [TTL], DCholdid as [Dposid] from tbl_MCHold " +
                        //" inner join tbl_DCHold on tbl_MCHold.MCholdid = tbl_DCHold.MCholdid " +
                        //" inner join Products on tbl_DCHold.ProductID = Products.ProID " +
                        //" where tbl_MCHold.BillNO = '" + BillNO.Trim() + "'";

                    //updated on 10-10-2019
                    query1 = "  select distinct(Products.ProductID), tbl_mcpos.MCposid, ProductName as [ItemDesc]," +
                        " Pro_Code as [Items], ProductName as [ItemDesc],tbl_dcpos.ttl_unts as [UNIT], " +
                        " salprice as [salpric],ProQty as [QTY],  Amt as [TTL], DCposid as [Dposid] " +
                        " from tbl_mcpos  inner join tbl_dcpos on tbl_mcpos.MCposid = tbl_dcpos.MCposid " +
                        " left join Products on tbl_dcpos.ProductID = Products.productid  where tbl_mcpos.BillNO = '" +
                        BillNO.Trim() + "' and Ishold = '1' and tbl_mcpos.CompanyId='" + Session["CompanyID"] + "' and tbl_mcpos.BranchId='" + Session["BranchID"] + "'";
                    
                    dt_ = new DataTable();
                    dt_ = DBConnection.GetQueryData(query1);

                    if (dt_.Rows.Count > 0)
                    {
                        GV_POS.DataSource = dt_;
                        GV_POS.DataBind();

                        

                        for (int j = 0; j < GV_POS.Rows.Count; j++)
                        {
                            TextBox TBUnit = (TextBox)GV_POS.Rows[j].FindControl("TBUnit");
                            TextBox TBItms = (TextBox)GV_POS.Rows[j].FindControl("TBItms");
                            DropDownList ddlUnit = (DropDownList)GV_POS.Rows[j].FindControl("ddlUnit");
                            getunts();
                            getunts(TBItms.Text.Trim());
                            ddlUnit.SelectedValue = TBUnit.Text;
                            ddlUnit.Width = 100;
                        }


                        ViewState["dt_adItm"] = dt_;

                        TBAdvance.Focus();
                    }

                    HFHold.Value = "1";

                    query = " select tbl_MCpos.MCposid, DCposid from tbl_MCpos " +
                            " inner join tbl_DCPos on tbl_MCpos.MCposid = tbl_DCPos.MCposid where tbl_MCPos.BillNO='" + lbl_BillNo.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                    DataTable chksal = new DataTable();

                    chksal = DBConnection.GetQueryData(query);

                    if (chksal.Rows.Count > 0)
                    {  
                        HFCposid.Value = chksal.Rows[0]["MCposid"].ToString();
                    }

                    float GTotal = 0;
                    for (int k = 0; k < GV_POS.Rows.Count; k++)
                    {
                        Label lblttl = (Label)GV_POS.Rows[k].FindControl("lblttl");
                        GTotal += Convert.ToSingle(lblttl.Text);
                    }

                    TBTtl.Text = GTotal.ToString();
                    lblttls.Text = GTotal.ToString(); 
                }
            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
        }

        protected void TBDisc_TextChanged1(object sender, EventArgs e)
        {
            try
            {
                #region Real Logic
                    //string disc = (Convert.ToDouble(lblttls.Text.Trim()) * Convert.ToDouble(TBDisc.Text.Trim()) / 100).ToString();
                    //lblttls.Text = (Convert.ToDecimal(lblttls.Text.Trim()) - Convert.ToDecimal(disc)).ToString();
                #endregion

                #region For NM garments                
                lblttls.Text = (Convert.ToDecimal(lblttls.Text.Trim()) - Convert.ToDecimal(TBDisc.Text.Trim())).ToString();
                for (int i = 0; i < GV_POS.Rows.Count; i++)
                {
                    LinkButton lnkbtnadd = (LinkButton)GV_POS.Rows[i].Cells[6].FindControl("lnkbtnadd");
                    lnkbtnadd.Focus();
                }
                #endregion
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }

        }

        protected void GVRemanItms_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVRemanItms.PageIndex = e.NewPageIndex;
            loaditm();
        }

        protected void TBsearhPro_TextChanged(object sender, EventArgs e)
        {
            try 
            {
                if (TBsearhPro.Text != "")
                {
                    str = " select distinct(ProductName),sum(Dstk_Qty) as [Dstk_Qty],Dstk_rat,Dstk_unt,RetalPrice,tbl_Dstk.ProductID from tbl_Mstk " +
                                " inner join tbl_Dstk on tbl_Mstk.Mstk_id = tbl_Dstk.Mstk_id " +
                                " inner join Products on tbl_Dstk.ProductID = Products.ProductID " +
                                " where Pro_Code='" + TBsearhPro.Text.Trim() + "' and Products.CompanyId='" + Session["CompanyID"] + "' and Products.BranchId='" + Session["BranchID"] +
                                "' group by Dstk_Qty,tbl_Dstk.ProductID,ProductName,Dstk_rat, RetalPrice, Dstk_unt ";
                    //str = " select distinct ( tbl_DCPos.ProductID) , ProductName,  salprice from tbl_DCPos " +
                      //   " inner join products on tbl_Dcpos.ProductID = Products.ProductID where ProductName = '" + TBsearhPro.Text.Trim() + "'";

                   // str = " select * FROM products where ProductName = '" + TBsearhPro.Text.Trim() + "'"; 
                    dt_ = new DataTable();
                    dt_ = DBConnection.GetQueryData(str);

                    if (dt_.Rows.Count > 0)
                    {
                        GVRemanItms.DataSource = dt_;
                        GVRemanItms.DataBind();

                        Session["SelctPro"] = dt_;
                    }
                    else
                    {
                       //
                    }
                }
                else if (TBsearhPro.Text == "")
                {
                    loaditm();
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        
        protected void GVRemanItms_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVRemanItms.EditIndex = e.NewEditIndex;

            if (TBsearhPro.Text != "")
            {
                dt_ = (DataTable)Session["SelctPro"];
            }
            else if (TBsearhPro.Text == "")
            {
                Session["SelctPro"] = null;
            }

            if (dt_ != null)
            {
                GVRemanItms.DataSource = dt_;
                GVRemanItms.DataBind();
            }
            else
            {
            
                loaditm();
            }
        }

        protected void GVRemanItms_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lbl_id = GVRemanItms.Rows[e.RowIndex].FindControl("lbl_id") as Label;
            TextBox txt_Rate = GVRemanItms.Rows[e.RowIndex].FindControl("txt_Rate") as TextBox;
            
            con.Open();

            //updating the record  
            SqlCommand cmd = new SqlCommand("update  Products set RetalPrice='" + txt_Rate.Text + "' where ProductID= '" + lbl_id.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GVRemanItms.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            loaditm();

            TBsearhPro.Text = "";

        }

        protected void GVRemanItms_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVRemanItms.EditIndex = -1;
            loaditm();
        }

        protected void GV_POS_RowDataBound(object sender, EventArgs e)
        {

            getunts();
        }

        protected void tbsalpris_TextChanged(object sender, EventArgs e)
        {
            LinkButton lnkbtnadd = null;
            TextBox TBItmQty = null;
            try
            {
                for (int j = 0; j < GV_POS.Rows.Count; j++)
                {
                    TextBox TBItms = (TextBox)GV_POS.Rows[j].FindControl("TBItms");
                    TBItmQty = (TextBox)GV_POS.Rows[j].FindControl("TBItmQty");
                    TextBox TBUnit = (TextBox)GV_POS.Rows[j].FindControl("TBUnit");
                    TextBox tbsalpris = (TextBox)GV_POS.Rows[j].FindControl("tbsalpris");
                    Label lblttl = (Label)GV_POS.Rows[j].FindControl("lblttl");
                    Label lblchkqty = (Label)GV_POS.Rows[j].FindControl("lblchkqty");
                    Label lbl_Flag = (Label)GV_POS.Rows[j].FindControl("lbl_Flag");
                    lnkbtnadd = (LinkButton)GV_POS.Rows[j].FindControl("lnkbtnadd");
                    HiddenField HFPROID = (HiddenField)GV_POS.Rows[j].FindControl("HFPROID");

                    if (TBItms.Text == "")
                    {
                        lbl_Flag.Text = "0";
                    }

                    lblchkqty.Text = "";
                    lnkbtnadd.Enabled = true;

                    lblttl.Text = (Convert.ToDouble(TBItmQty.Text.Trim()) * Convert.ToDouble(tbsalpris.Text.Trim())).ToString();

                }

                // for total 

                decimal GTotal = 0;
                decimal GTQty = 0;

                for (int t = 0; t < GV_POS.Rows.Count; t++)
                {
                    Label total = (Label)GV_POS.Rows[t].FindControl("lblttl");
                    GTotal += Convert.ToDecimal(total.Text);
                
                    TextBox TBQty = (TextBox)GV_POS.Rows[t].FindControl("TBItmQty");
                    GTQty += Convert.ToDecimal(TBQty.Text);
                }

                TBTtl.Text = GTotal.ToString();
                lblttls.Text = GTotal.ToString();
                lbl_ttlqty.Text = GTQty.ToString();

                lbl_itmqty.Text = GV_POS.Rows.Count.ToString();

                TBItmQty.Focus();
                TBItmQty.Attributes.Add("onfocusin", "select();");
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }

        }

        protected void TB_SalNO_TextChanged(object sender, EventArgs e)
        {
            try
            {
                query = " select tbl_mcpos.MCposid,tbl_mcpos.BillNO,dis,Adv, customerid, CustomerName,cust_Due,ttl_itms,ttl_qty,Amt,tbl_DCPos.bal, " +
                    " grntttl, billdat, Ttl,RecieverNam, tax1amt,tax2amt,paytyp,disc from tbl_mcpos inner join tbl_dcpos on tbl_mcpos.MCposid = tbl_DCPos.MCposid " +
                    " inner join SubHeadCategories on tbl_MCPos.customerid = SubHeadCategories.SubHeadCategoriesGeneratedID " +
                    " where tbl_mcpos.BillNO = '" + TB_SalNO.Text.Trim() + "' and ishold = '0' and iscancel <> '1'  and tbl_MCPos.CompanyId='" + Session["CompanyID"] + "' and tbl_MCPos.BranchId='" + Session["BranchID"] + "'";

                dt_ = new DataTable();

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    HFCposid.Value = dt_.Rows[0]["MCposid"].ToString();
                    TBCust.Text = dt_.Rows[0]["CustomerName"].ToString();
                    lbl_BillNo.Text = dt_.Rows[0]["BillNO"].ToString();
                    lbl_dat.Text = dt_.Rows[0]["billdat"].ToString();
                    lbl_Acc.Text = dt_.Rows[0]["customerid"].ToString();
                    lbldue.Text = dt_.Rows[0]["cust_Due"].ToString();
                    lbl_itmqty.Text = dt_.Rows[0]["ttl_itms"].ToString();
                    lbl_ttlqty.Text = dt_.Rows[0]["ttl_qty"].ToString();
                    TBAdvance.Text = dt_.Rows[0]["Adv"].ToString();
                    TBBalance.Text = dt_.Rows[0]["bal"].ToString();
                    TBTtl.Text = dt_.Rows[0]["grntttl"].ToString();
                    lblttls.Text = dt_.Rows[0]["Ttl"].ToString();
                    TBREC.Text = dt_.Rows[0]["RecieverNam"].ToString();
                    TBDisc.Text = dt_.Rows[0]["dis"].ToString();
                    //D//DL_typofpayments.SelectedValue = dt_.Rows[0]["paytyp"].ToString();
                    TBTaxPer.Text = dt_.Rows[0]["tax1amt"].ToString();

                    if (TBTaxPer.Text != "0" && TBTaxPer.Text != "")
                    {
                        chk_tax1.Checked = true;
                    }
                    else
                    {
                        chk_tax1.Checked = false;
                    }
                    TBOthChrgs.Text = dt_.Rows[0]["tax2amt"].ToString();

                    if (TBOthChrgs.Text != "0" && TBOthChrgs.Text != "")
                    {
                        chk_tax2.Checked = true;
                    }
                    else
                    {
                        chk_tax2.Checked = false;
                    }

                    
                   // TBDisc.Text = dt_.Rows[0]["disc"].ToString();                    
                    btn_POScancl.Visible = true;
                    
                }
                else
                {
                    btn_POScancl.Visible = false;
                    TB_SalNO.Text = "";
                }

                DataTable dtdtl = new DataTable();

                query = " select distinct(tbl_dcpos.productid) as [ProductID],Pro_Code as [Items],dcposid as [Dposid],productname as [ItemDesc] , " +
                    "  salprice as [salpric],proqty as [QTY],ttl_unts as [UNIT],TTL=(proqty * salprice),* from tbl_mcpos " +
                    " inner join tbl_dcpos on tbl_mcpos.MCposid = tbl_DCPos.MCposid  " +
                    " inner join products on tbl_dcpos.productid = products.productid where tbl_mcpos.BillNO = '" +
                     TB_SalNO.Text.Trim() + "' and ishold = '0' and iscancel <> '1' and tbl_mcpos.CompanyId='" + Session["CompanyID"] + "' and tbl_mcpos.BranchId='" + Session["BranchID"] + "'";

                dtdtl = new DataTable();

                dtdtl = DBConnection.GetQueryData(query);

                if (dtdtl.Rows.Count > 0)
                {
                    GV_POS.DataSource = dtdtl;
                    GV_POS.DataBind();

                    ViewState["dt_adItm"] = dtdtl;

                    btn_POScancl.Visible = true;
                    getunts();
                   
                    for (int t = 0; t < GV_POS.Rows.Count; t++)
                    {
                        TextBox TBItms = (TextBox)GV_POS.Rows[t].FindControl("TBItms");
                        TextBox TBUnit = (TextBox)GV_POS.Rows[t].FindControl("TBUnit");
                        DropDownList ddlUnit = (DropDownList)GV_POS.Rows[t].FindControl("ddlUnit");

                        getunts(TBItms.Text.Trim());

                        ddlUnit.SelectedValue = TBUnit.Text;
                        TBUnit.Visible = false;
                    }
                }
                else
                {
                    btn_POScancl.Visible = false;
                    TB_SalNO.Text = "";
                }

            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
            }
            
            //Setting Session for double entries...

            Session["chek"] = "0";
        }

        protected void btn_POScancl_Click(object sender, EventArgs e)
        {
            string HFPROID = "";
            isCancel.Value = "1";
            con.Open();

            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            // Start a local transaction.
            transaction = con.BeginTransaction("POSTrans");

            // Must assign both transaction object and connection 
            // to Command object for a pending local transaction
            command.Connection = con;
            command.Transaction = transaction;

            try
            {
                // For Cancel Sales
                #region Cancel Sales
                
                command.Parameters.Clear();
                command.CommandText = "Iscancel";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@BillNO", lbl_BillNo.Text);
                command.Parameters.AddWithValue("@BranchId", Session["BranchID"].ToString());
                command.Parameters.AddWithValue("@CompanyId", Session["CompanyID"].ToString());
                //command.Parameters.AddWithValue("@Dstk_Qty", Session["CompanyID"].ToString());
                //command.Parameters.AddWithValue("@ProductID", Session["CompanyID"].ToString());
                command.ExecuteNonQuery();
                //CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                #endregion

                #region stock
                //Detail Stock
                foreach (GridViewRow g1 in GV_POS.Rows)
                {

                    TBItms = (g1.FindControl("TBItms") as TextBox).Text;
                    tbsalpris = (g1.FindControl("tbsalpris") as TextBox).Text;
                    TBItmQty = (g1.FindControl("TBItmQty") as TextBox).Text;
                    TBUnit = (g1.FindControl("TBUnit") as TextBox).Text;
                    lblttl = (g1.FindControl("lblttl") as Label).Text;
                    HFDSal = (g1.FindControl("HFDSal") as HiddenField).Value;
                    HFPROID = (g1.FindControl("HFPROID") as HiddenField).Value;

                    DataTable dtstkqty = new DataTable();

                    command.CommandText = "select Dstk_Qty from tbl_Dstk where ProductID = " + HFPROID.Trim() + " and Dstk_unt='" + TBUnit.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                    command.CommandType = CommandType.Text;

                    SqlDataAdapter Adapter = new SqlDataAdapter(command);
                    Adapter.Fill(dtstkqty);

                    if (dtstkqty.Rows.Count > 0)
                    {
                        // for (int t = 0; t < dtstkqty.Rows.Count; t++)
                        {
                            stkqty = dtstkqty.Rows[0]["Dstk_Qty"].ToString();

                            int qty = Convert.ToInt32(stkqty) + Convert.ToInt32(TBItmQty);
                            command.CommandText = " UPDATE tbl_Dstk SET Dstk_Qty = " + qty + " where  ProductID = " + HFPROID.Trim() + "  and Dstk_unt='" + TBUnit.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                            command.ExecuteNonQuery();

                        }
                    }
                }
                #endregion
                #region Sale Credit


                command.CommandText = "select CredAmt from tbl_Salcredit where CustomerID='" + lbl_Acc.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";


                command.CommandType = CommandType.Text;

                SqlDataAdapter stksalcre = new SqlDataAdapter(command);

                DataTable dtsalcre = new DataTable();
                stksalcre.Fill(dtsalcre);


                if (dtsalcre.Rows.Count > 0)
                {
                    avapre = Convert.ToDecimal(dtsalcre.Rows[0]["CredAmt"]);

                    ttlcre = Convert.ToDecimal(TBAdvance.Text.Trim()) - avapre;

                    command.CommandText = " Update tbl_Salcredit set CredAmt = '" + ttlcre + "' where CustomerID='" + lbl_Acc.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }


                #endregion

                #region Expence

                //command.CommandText = "select cashamt,PaymentIn,Amountpaid from tbl_expenses where accno='" + lbl_Acc.Text.Trim() + "' and billno='" + lbl_BillNo.Text + "'";


                //command.CommandType = CommandType.Text;

                //SqlDataAdapter stkcancl = new SqlDataAdapter(command);

                //DataTable dtcancl = new DataTable();
                //stkcancl.Fill(dtcancl);


                //if (dtcancl.Rows.Count > 0)
                //{

                //    DataTable dto = new DataTable();

                //    command.CommandText = "";
                //    command.CommandText = " select openbal,openingBal from tbl_expenses ";

                //    SqlDataAdapter adpopenbal = new SqlDataAdapter(command);

                //    adpopenbal.Fill(dto);

                //    int i = dto.Rows.Count;

                //    while (--i >= 0)
                //    {

                //        if (dto.Rows[i].IsNull(0)) dto.Rows.RemoveAt(i);

                //    }

                //    if (dto.Rows.Count > 0)
                //    {
                //        for (int k = 0; k < dto.Rows.Count; k++)
                //        {
                //            openingbal = dto.Rows[k]["openbal"].ToString();
                //        }

                //        openbal = Convert.ToDecimal(openingbal);//Convert.ToDecimal(openingbal) - Convert.ToDecimal(lblttls.Text);
                //    }
                //    avapre = Convert.ToDecimal(dtcancl.Rows[0]["Amountpaid"]);

                //    ttlcre = Convert.ToDecimal(TBAdvance.Text.Trim()) - avapre;

                //    command.CommandText = " Update tbl_expenses set cashamt = '" + ttlcre +
                //        "' , PaymentIn = '" + ttlcre +
                //        "' , Amountpaid = '" + ttlcre +
                //        "' , openbal = '" + openbal +
                //        "' , openingBal = '" + openingbal.Trim() +
                //        "' where accno='" + lbl_Acc.Text.Trim() + "' and billno='" + lbl_BillNo.Text + "'";
                //    command.CommandType = CommandType.Text;
                //    command.ExecuteNonQuery();
                //}

                command.CommandText = " delete from tbl_expenses where accno='" + lbl_Acc.Text.Trim() + "' and billno='" + lbl_BillNo.Text + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                command.CommandType = CommandType.Text;
                command.ExecuteNonQuery();
                

                #endregion


                // Attempt to commit the transaction.
                transaction.Commit();

                clear();

                }
            catch (Exception ex)
            {
                lblmssg.Text = "Commit Exception Type: {0}" + ex.GetType();
                lblmssg.Text = "Message: {0}" + ex.Message;

                // Attempt to roll back the transaction. 
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred 
                    // on the server that would cause the rollback to fail, such as 
                    // a closed connection.
                    lblmssg.Text = "Rollback Exception Type: {0}" + ex2.GetType();
                    lblmssg.Text = "Message: {0}" + ex2.Message;
                }
            }
            finally
            {
                con.Close();
                ptnSno();
                loaditm();
                btn_POScancl.Visible = false;
            }
            
        }

        protected void GVHoldList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int rowIndex = Convert.ToInt32(e.RowIndex);

                HiddenField HFHoldBill = (HiddenField)GVHoldList.Rows[rowIndex].Cells[3].FindControl("HFHoldBill");

                if (HFHoldBill.Value != "")
                {
                    try
                    {
                        SqlCommand cmd = new SqlCommand("sp_delhold", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BillNO", HFHoldBill.Value.Trim());
                        cmd.Parameters.AddWithValue("@BranchId", Session["BranchID"].ToString());
                        cmd.Parameters.AddWithValue("@CompanyId", Session["CompanyID"].ToString());
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        ModalPopupExtender2.Show();
                        data();
                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
            }
        }

        protected void chk_tax1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_tax1.Checked == true)
                {
                    string taxper = ((Convert.ToDouble(chk_tax1.Text.Trim()) / 100) * Convert.ToDouble(TBTtl.Text.Trim())).ToString();
                    if (TBOthChrgs.Text == "0.00")
                    {
                        lblttls.Text = (Convert.ToDecimal(taxper) + Convert.ToDecimal(TBTtl.Text.Trim())).ToString();
                        TBTaxPer.Text = taxper;
                    }
                    else
                    {

                        lblttls.Text = (Convert.ToDecimal(taxper) + Convert.ToDecimal(lblttls.Text.Trim())).ToString();
                        TBTaxPer.Text = taxper;
                        //lblttls.Text = TBTtl.Text.Trim();
                    }
                }
                else if (chk_tax1.Checked == false)
                {
                    lblttls.Text = (Convert.ToDecimal(lblttls.Text.Trim()) - Convert.ToDecimal(TBTaxPer.Text.Trim())).ToString();
                    TBTaxPer.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        protected void chk_tax2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_tax2.Checked == true)
                {
                    string othper = ((Convert.ToDouble(chk_tax2.Text.Trim()) / 100) * Convert.ToDouble(TBTtl.Text.Trim())).ToString();
                    if (TBTaxPer.Text == "0.00")
                    {
                        lblttls.Text = (Convert.ToDecimal(othper) + Convert.ToDecimal(TBTtl.Text.Trim())).ToString();
                        TBOthChrgs.Text = othper;
                    }
                    else {

                        lblttls.Text = (Convert.ToDecimal(othper) + Convert.ToDecimal(lblttls.Text.Trim())).ToString();
                        TBOthChrgs.Text = othper;
                        //lblttls.Text = TBTtl.Text.Trim();
                    }
                }
                else if (chk_tax2.Checked == false)
                {
                    lblttls.Text = (Convert.ToDecimal(lblttls.Text.Trim()) - Convert.ToDecimal(TBOthChrgs.Text.Trim())).ToString(); 
                    TBOthChrgs.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        protected void DDL_typofpayments_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
 
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < GV_POS.Rows.Count; i++)
            {
                //DropDownList ddlUnit = (DropDownList)GV_POS.Rows[i].Cells[1].FindControl("ddlUnit");
                DropDownList ddlUnit = (DropDownList)GV_POS.Rows[i].Cells[1].FindControl("ddlUnit");
                TextBox TBUnit = (TextBox)GV_POS.Rows[i].Cells[1].FindControl("TBUnit");
                TextBox TBItmQty = (TextBox)GV_POS.Rows[i].Cells[4].FindControl("TBItmQty");
               
                if (ddlUnit.SelectedValue != "0") {
                    TBUnit.Text = ddlUnit.SelectedValue;
                    TBItmQty.Focus();
                }
            }
        }

    }
}