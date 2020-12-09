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
    public partial class frm_Sal : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_ = null;
        DBConnection db = new DBConnection();
        int i = 0;
        string MSTkId, DDL_Par, DDL_Prorefcde, lblItmQty, TBItmDes, custid, TBItmQty, TbItmunt, Tbrat,
            lbl_Pro, itmsiz, lblrat, ItmQty, TBamt, proid, procatid;
        string PurItm, Tbamt, HFDSal, stkqty, query;
        public static string branch, company;

        double totalprev;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Initials
            if (!IsPostBack)
            {
                TBRecov.Text = "0.00";
                lbl_outstan.Text = "0.00";
                TBoutstan.Text = "0.00";
                TBDISC.Text = "0.00";
                TBDISAMT.Text = "0.00";
                setini();
                SetInitRowSal();
                FillGrid();
                branch = Session["BranchID"].ToString();
                company = Session["CompanyID"].ToString();
                ////BindPar();
                BindCus();

                //getPro();
                BindCustACC();
                //BindSO();
                TBSalDat.Text = DateTime.Today.ToString("MM/dd/yyyy");//DateTime.Now.ToShortDateString();
                //ptnSno();
                ShowSno();
                chk_prtd.Checked = true;
                chk_Act.Checked = true;
                pnlSO.Visible = false;
                pnl_sch.Visible = false;
                pnl_recov.Visible = false;
                pnlgpno.Visible = false;
                salmanbook.Visible = false;
                pnl_crdcsh.Visible = false;

                DDL_Cust.Focus();
            }
            #endregion
        }


        public void setini()
        {
            int rowIndex = 0;
            //GridView GVStkItems = (GridView)e.Row.FindControl("GVStkItems");
           // GridView GVStkItems = (GridView)GVItms.Rows[rowIndex].Cells[2].FindControl("GVStkItems");

            DataTable subdt = new DataTable();
            DataRow subdr = null;

            subdt.Columns.Add(new DataColumn("SIZE", typeof(string)));
            subdt.Columns.Add(new DataColumn("QTY", typeof(string)));

            subdr = subdt.NewRow();
            subdr["SIZE"] = "0.00";
            subdr["QTY"] = "0.00";

            subdt.Rows.Add(subdr);

            //Store the DataTable in ViewState
            ViewState["dt_adItms"] = subdt;

            //GVStkItems.DataSource = subdt;
            //GVStkItems.DataBind();
        }

        #region web methods

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetCat(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select ProductTypeName from tbl_producttype where ProductTypeName like '%" + prefixText + "%' and CompanyId='" + company + "' and BranchId='" + branch + "'";
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
        public static List<string> GetCust(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select CustomerName from Customers_ where CustomerName like '%" + prefixText + "%' and CompanyId='" + company + "' and BranchId='" + branch + "'";
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
        public static List<string> GetPro(string prefixText)
        {
            string protyp = HttpContext.Current.Session["cat"].ToString();

            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            //string str = "select ProductName from Products where ProductType ='" + protyp + "' and ProductName like '%" + prefixText + "%'";
            string str = " select distinct(ProductName) from tbl_Dstk  " +
                " inner join Products on  tbl_dstk.ProductID = Products.ProductID where procat='" + protyp + "' and ProductName like '%" + prefixText + "%' and Products.CompanyId='" + company + "' and Products.BranchId='" + branch + "'";
                        
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        
        }

        #endregion
        #region Method

        private void AddNewItems()
        {
            int rowIndex = 0;

            if (ViewState["dt_salItms"] != null)
            {
                DataTable dt = (DataTable)ViewState["dt_salItms"];

                DataRow drRow = null;

                DataTable dt3 = (DataTable)ViewState["dt_salItm"];

                if (dt3.Rows.Count > 0)
                {
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        dt3.Rows[i]["RATE"] = TB_Rat.Text;
                        //if (dt3.Rows[i]["QTY"] != "0")
                        //{
                        // dt3.Rows[i]["QTY"] = "0"; 
                        //}
                    }

                    dt.Merge(dt3, true, MissingSchemaAction.Ignore);
                }

                //dt.Rows.Add(drRow);
                ViewState["dt_salItms"] = dt;

                GVItms.DataSource = dt;
                GVItms.DataBind();

               
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreitm();
        }

        private void SetPreitm()
        {
            try
            {
                int rowIndex = 0;
                if (ViewState["dt_salItms"] != null)
                {
                    DataTable dt = (DataTable)ViewState["dt_salItms"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            Label lbl_Pro = (Label)GVItms.Rows[i].Cells[0].FindControl("lbl_Pro");
                            Label itmsiz = (Label)GVItms.Rows[i].Cells[1].FindControl("itmsiz");
                            TextBox ItmQty = (TextBox)GVItms.Rows[i].Cells[2].FindControl("ItmQty");
                            TextBox lblrat = (TextBox)GVItms.Rows[i].Cells[3].FindControl("lblrat");
                            TextBox TBamt = (TextBox)GVItms.Rows[i].Cells[4].FindControl("TBamt");
                            //HiddenField HFDSal = (HiddenField)GVItms.Rows[i].Cells[5].FindControl("HFDSal");


                            lbl_Pro.Text = dt.Rows[i]["PRODUCT"].ToString();
                            itmsiz.Text = dt.Rows[i]["SIZE"].ToString();
                            ItmQty.Text = dt.Rows[i]["QTY"].ToString();
                            lblrat.Text = dt.Rows[i]["RATE"].ToString();
                            TBamt.Text = dt.Rows[i]["AMT"].ToString();
                            //HFDSal.Value = dt.Rows[i]["DSal_id"].ToString();

                            if (itmsiz.Text == "")
                            {
                                itmsiz.Text = "0";
                            }

                            if (ItmQty.Text == "")
                            {
                                ItmQty.Text = "0";
                            }

                            if (lblrat.Text == "")
                            {
                                lblrat.Text = "0";
                            }

                            if (TBamt.Text == "")
                            {
                                TBamt.Text = "0";
                            }

                            rowIndex++;

                        }
                    }
                }

                 float GTotal = 0;
                 for (int k = 0; k < GVItms.Rows.Count; k++)
                {
                    TextBox total = (TextBox)GVItms.Rows[k].FindControl("TBamt");
                    GTotal += Convert.ToSingle(total.Text);
                }


                TBGTtl.Text = GTotal.ToString();
                TBTtl.Text = GTotal.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        DataTable GetDataTable(GridView dtg)
        {
            DataTable dt = new DataTable();

            // add the columns to the datatable            
            if (dtg.HeaderRow != null)
            {

                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = row.Cells[i].Text.Replace(" ", "");
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public void BindCustACC()
        {
            //Cust Account No

            DataTable dt_CusNo = new DataTable();

            //dt_Cust = DBConnection.GetQueryData("select rtrim('[' + CAST(CustomerID AS VARCHAR(200)) + ']-' + CustomerName ) as [CustomerName], CustomerID  from Customers_ where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");
            dt_CusNo = DBConnection.GetQueryData("select distinct(SubHeadCategoriesGeneratedID) from SubHeadCategories where SubHeadGeneratedID='0011' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'");
            DDL_CustAcc.DataSource = dt_CusNo;

            DDL_CustAcc.DataTextField = "SubHeadCategoriesGeneratedID";
            DDL_CustAcc.DataValueField = "SubHeadCategoriesGeneratedID";
            DDL_CustAcc.DataBind();
            DDL_CustAcc.Items.Insert(0, new ListItem("--Select Account No--", "0"));

        }

        public void BindCus()
        {
            try
            {
                DataTable dt_Cust = new DataTable();

                //dt_Cust = DBConnection.GetQueryData("select rtrim('[' + CAST(CustomerID AS VARCHAR(200)) + ']-' + CustomerName ) as [CustomerName], CustomerID  from Customers_ where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");
                dt_Cust = DBConnection.GetQueryData("select distinct(SubHeadCategoriesGeneratedID),SubHeadCategoriesName from SubHeadCategories where SubHeadGeneratedID='0011' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'");
                DDL_Cust.DataSource = dt_Cust;
                DDL_Cust.DataTextField = "SubHeadCategoriesName";
                DDL_Cust.DataValueField = "SubHeadCategoriesGeneratedID";
                DDL_Cust.DataBind();
                DDL_Cust.Items.Insert(0, new ListItem("--Select Customer--", "0"));
                
                //Bookers

                DataTable dt_book = new DataTable();

                dt_book = DBConnection.GetQueryData("select  *  from Users where Level = 2 and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                DDL_Book.DataSource = dt_book;
                DDL_Book.DataTextField = "Username";
                DDL_Book.DataValueField = "Username";
                DDL_Book.DataBind();
                DDL_Book.Items.Insert(0, new ListItem("--Select Bookers--", "0"));

                //Sales Man

                DataTable dt_salman = new DataTable();

                dt_salman = DBConnection.GetQueryData("select  *  from Users where Level = 3 and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                DDL_SalMan.DataSource = dt_salman;
                DDL_SalMan.DataTextField = "Username";
                DDL_SalMan.DataValueField = "Username";
                DDL_SalMan.DataBind();
                DDL_SalMan.Items.Insert(0, new ListItem("--Select Sales Man--", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
                //lbl_err.Text = ex.Message.ToString();
            }
        }
        
        public void BindSO()
        {
            try
            {
                DataTable dt_SO = new DataTable();
                 
                dt_SO = tbl_MSalManager.GetSO();
                
                DDL_SO.DataSource = dt_SO;
                DDL_SO.DataTextField = "MSalOrdsono";
                DDL_SO.DataValueField = "MSalOrdid";
                DDL_SO.DataBind();
                DDL_SO.Items.Insert(0, new ListItem("--Select SO--", "0"));             
            }
            catch (Exception ex)
            {
                throw ex;
                //lbl_err.Text = ex.Message.ToString();
            }
        }

        public void ShowSno()
        {
            try
            {

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                //string str = "select count(MCposid) as [MCposid],BillNO from tbl_MCPos  where CompanyId='" + Session["CompanyID"] +
                // "' and BranchId='" + Session["BranchID"] + "'  order by MCposid desc";
                //string str = " select BillNO as [MCposid] from tbl_MCPos  where CompanyId='COM_001' and BranchId='001'";


                string str = " select isnull(max(cast(tbl_Mstk.Mstk_id as int)),0) as [MSal_id]  from tbl_Mstk " +
                    " inner join tbl_MSal " +
                    " on tbl_Mstk.MSal_id = tbl_MSal.MSal_id where tbl_Mstk.CompanyId='" + Session["CompanyID"] + "' and tbl_Mstk.BranchId='" + Session["BranchID"] + "'";
                
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                DataTable dt_ = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt_);
                if (dt_.Rows.Count <= 0)
                {
                    TBSalesNum.Text = "1";
                }
                else
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (TBSalesNum.Text == "")
                        {
                            int v = Convert.ToInt32(reader["MSal_id"].ToString());
                            //int mcid = Convert.ToInt32(reader["MCposid"].ToString());

                            int b = v + 1;

                            TBSalesNum.Text = b.ToString();
                            int id = Convert.ToInt32(TBSalesNum.Text.Trim());

                            if (v == id && v != 0)
                            {
                                //if (v == mcid)
                                //{
                                //    b = mcid + 1;
                                //    lbl_BillNo.Text = b.ToString();
                                //}
                                //else
                                //{
                                b = b + 1;
                                TBSalesNum.Text = b.ToString();

                                //}
                            }
                        }
                        else
                        {
                            int v = Convert.ToInt32(reader["MSal_id"].ToString());

                            int b = v + 1;

                            TBSalesNum.Text = b.ToString();
                            int id = Convert.ToInt32(TBSalesNum.Text.Trim());

                            if (v == id && v != 0)
                            {
                                b = b + 1;
                                TBSalesNum.Text = b.ToString();
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


        private void ptnSno()
        {
            try
            {
                //string str = "select isnull(max(cast(MSal_id as int)),0) as [MSal_id]  from tbl_MSal";
                string str = " select isnull(max(cast(tbl_Mstk.Mstk_id as int)),0) as [MSal_id]  from tbl_Mstk " +
                    " inner join tbl_MSal " +
                    " on tbl_Mstk.MSal_id = tbl_MSal.MSal_id where tbl_Mstk.CompanyId='" + Session["CompanyID"] + "' and tbl_Mstk.BranchId='" + Session["BranchID"] + "'";

                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(TBSalesNum.Text))
                    {
                        int v = Convert.ToInt32(reader["MSal_id"].ToString());
                        int b = v + 1;
                        TBSalesNum.Text = "SAL00" + b.ToString(); 
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                //lbl_err.Text = ex.Message.ToString();
            }
        }

        public void BindPar()
        {
            //Items Name
            try
            {
                using (SqlCommand cmdpro = new SqlCommand())
                {
                    con.Close();
                    //cmdpro.CommandText = " select rtrim('[' + CAST(ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], ProductID from Products ";
                    cmdpro.CommandText = " select rtrim('[' + CAST(ProductTypeID AS VARCHAR(200)) + ']-' + ProductTypeName ) as [ProductTypeName], ProductTypeID from tbl_producttype where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                    cmdpro.Connection = con;
                    con.Open();

                    DataTable dtpro = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                    adp.Fill(dtpro);

                    for (int i = 0; i < GVItms.Rows.Count; i++)
                    {
                        DropDownList DDLPro = (DropDownList)GVItms.Rows[i].Cells[0].FindControl("DDL_Par");
                        DDLPro.DataSource = dtpro;
                        DDLPro.DataTextField = "ProductTypeName";
                        DDLPro.DataValueField = "ProductTypeID";                        
                        DDLPro.DataBind();
                        DDLPro.Items.Insert(0, new ListItem("--Select Product Type--", "0"));
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetInitRowSal()
        {
            //DataTable dt = new DataTable();
            //DataRow dr = null;

            //DataTable dt_adItms = (DataTable)ViewState["dt_adItms"];

            //dt.Columns.Add(new DataColumn("PARTICULARS", typeof(string)));
            //dt.Columns.Add(new DataColumn("PRODUCT", typeof(string)));
            //dt.Columns.Add(new DataColumn("Details", typeof(string)));
            //dt.Columns.Add(new DataColumn("dt_adItms", typeof(DataTable)));
            //dt.Columns.Add(new DataColumn("INVENTORYTYP", typeof(string)));            
            //dt.Columns.Add(new DataColumn("DIS", typeof(string)));
            //dt.Columns.Add(new DataColumn("QTY", typeof(string)));
            //dt.Columns.Add(new DataColumn("QTYAVAIL", typeof(string)));
            //dt.Columns.Add(new DataColumn("UNIT", typeof(string)));
            //dt.Columns.Add(new DataColumn("RATE", typeof(string)));
            //dt.Columns.Add(new DataColumn("AMT", typeof(string)));
            //dt.Columns.Add(new DataColumn("DSal_id",typeof(string)));

            //dr = dt.NewRow();
            //dr["PARTICULARS"] = string.Empty;
            //dr["PRODUCT"] = string.Empty;
            //dr["Details"] = "1";
            //dr["dt_adItms"] = dt_adItms;
            //dr["INVENTORYTYP"] = string.Empty;            
            //dr["DIS"] = "0.00";
            //dr["QTYAVAIL"] = "";            
            //dr["QTY"] = "0.00";
            //dr["UNIT"] = "0.00";
            //dr["RATE"] = "0.00";
            //dr["AMT"] = "0.00";
            //dr["DSal_id"] = "0";

            //dt.Rows.Add(dr);

            ////Store the DataTable in ViewState
            //ViewState["dt_adItm"] = dt;

            //GVItms.DataSource = dt;
            //GVItms.DataBind();

            DataTable dt = new DataTable();
            //DataRow dr = null;

            dt.Columns.Add(new DataColumn("PRODUCT", typeof(string)));
            dt.Columns.Add(new DataColumn("SIZE", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));
            dt.Columns.Add(new DataColumn("RATE", typeof(string)));
            dt.Columns.Add(new DataColumn("AMT", typeof(string)));
            dt.Columns.Add(new DataColumn("DSal_id", typeof(string)));

            ViewState["dt_salItms"] = dt;


            GVItms.DataSource = dt;
            GVItms.DataBind();

            // For Stock
            DataTable dtstk = new DataTable();
            //DataRow drstk = null;

            dtstk.Columns.Add(new DataColumn("SIZE", typeof(string)));
            dtstk.Columns.Add(new DataColumn("QTY", typeof(string)));
            dtstk.Columns.Add(new DataColumn("QUANTY", typeof(string)));
            
            ViewState["dt_salItm"] = dtstk;

            GVStkItems.DataSource = dtstk;
            GVStkItems.DataBind();
        }

        private void AddNewRow()
        {
            int rowIndex = 0;

            if (ViewState["dt_adItm"] != null)
            {
                DropDownList DDL_Prorefcde = null;
                DataTable dt = (DataTable)ViewState["dt_adItm"];

                DataTable dt_adItm = (DataTable)ViewState["dt_adItms"];

                DataRow drRow = null;
                GridView GVStkItems = null;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        //extract the Controls values
                        DropDownList DDLPro = (DropDownList)GVItms.Rows[rowIndex].Cells[0].FindControl("DDL_Par");
                        DDL_Prorefcde = (DropDownList)GVItms.Rows[rowIndex].Cells[1].FindControl("DDL_Prorefcde");
                        DropDownList DDL_invtyp = (DropDownList)GVItms.Rows[rowIndex].Cells[1].FindControl("DDL_invtyp");
                        Label lbl_invtyp = (Label)GVItms.Rows[rowIndex].Cells[1].FindControl("lbl_invtyp"); 
                        TextBox TBDIS = (TextBox)GVItms.Rows[rowIndex].Cells[2].FindControl("TBDIS");
                        Label lbl_Details = (Label)GVItms.Rows[rowIndex].Cells[2].FindControl("lbl_Details");
                        Label lblItmQty = (Label)GVItms.Rows[rowIndex].Cells[3].FindControl("lblItmQty");
                        TextBox TBItmQty = (TextBox)GVItms.Rows[rowIndex].Cells[4].FindControl("TBItmQty");
                        TextBox TbItmunt = (TextBox)GVItms.Rows[rowIndex].Cells[5].FindControl("TbItmunt");
                        TextBox Tbrat = (TextBox)GVItms.Rows[rowIndex].Cells[6].FindControl("Tbrat");
                        TextBox Tbamt = (TextBox)GVItms.Rows[rowIndex].Cells[7].FindControl("Tbamt");
                        HiddenField HFDSal = (HiddenField)GVItms.Rows[rowIndex].Cells[8].FindControl("HFDSal");
                        //CheckBox ChkCls = (CheckBox)GVItms.Rows[rowIndex].Cells[2].FindControl("ChkClosed");
                        GVStkItems = (GridView)GVItms.Rows[rowIndex].Cells[1].FindControl("GVStkItems");
                  
                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["PARTICULARS"] = DDLPro.Text;
                        dt.Rows[i - 1]["INVENTORYTYP"] = DDL_invtyp.SelectedValue;                        
                        dt.Rows[i - 1]["PRODUCT"] = DDL_Prorefcde.Text;
                        dt.Rows[i - 1]["Details"] = lbl_Details.Text;
                        dt.Rows[i - 1]["dt_adItms"] = dt_adItm;
                        dt.Rows[i - 1]["DIS"] = TBDIS.Text;
                        dt.Rows[i - 1]["QTYAVAIL"] = lblItmQty.Text; //QTYAVAIL
                        dt.Rows[i - 1]["QTY"] = TBItmQty.Text; //QTYAVAIL
                        dt.Rows[i - 1]["UNIT"] = TbItmunt.Text;
                        dt.Rows[i - 1]["RATE"] = Tbrat.Text;
                        dt.Rows[i - 1]["AMT"] = Tbamt.Text;
                        dt.Rows[i - 1]["DSal_id"] = HFDSal.Value;
                        //dt.Rows[i - 1]["CLOSED"] = ChkCls.Checked;

                        rowIndex++;

                       
                    }

                    dt.Rows.Add(drRow);
                    ViewState["dt_adItm"] = dt;

                    GVItms.DataSource = dt;
                    GVItms.DataBind();

                    GVStkItems.DataSource = dt_adItm;
                    GVStkItems.DataBind();
                    //DataTable dtstk = new DataTable();
                    //DataRow drstk;

                    //dtstk.Columns.Add(new System.Data.DataColumn("SIZE", typeof(String)));
                    //dtstk.Columns.Add(new System.Data.DataColumn("QTY", typeof(String)));

                    //foreach (GridViewRow row in GVStkItems.Rows)
                    //{
                    //    TextBox itmsiz = (TextBox)row.FindControl("itmsiz");
                    //    TextBox ItmQty = (TextBox)row.FindControl("ItmQty");

                    //    drstk = dtstk.NewRow();
                    //    drstk[0] = itmsiz.Text;
                    //    drstk[1] = ItmQty.Text;
                    //    dtstk.Rows.Add(drstk);

                    //    ViewState["StkItm"] = dtstk;
                        

                    //}


                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreRowSal();
        }

        private void SetPreRowSal()
        {
            try
            {   
                GridView GVStkItems = null;
                Label lbl_Details= null;
                int rowIndex = 0;
                if (ViewState["dt_adItm"] != null)
                {
                    DataTable dt = (DataTable)ViewState["dt_adItm"];
                    
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DropDownList DDLPro = (DropDownList)GVItms.Rows[rowIndex].Cells[0].FindControl("DDL_Par");
                            DropDownList DDL_Prorefcde = (DropDownList)GVItms.Rows[rowIndex].Cells[1].FindControl("DDL_Prorefcde");
                            DropDownList DDL_invtyp = (DropDownList)GVItms.Rows[rowIndex].Cells[1].FindControl("DDL_invtyp");
                            Label lbl_invtyp = (Label)GVItms.Rows[rowIndex].Cells[1].FindControl("lbl_invtyp"); 
                            TextBox TBDIS = (TextBox)GVItms.Rows[rowIndex].Cells[2].FindControl("TBDIS");
                            lbl_Details = (Label)GVItms.Rows[rowIndex].Cells[2].FindControl("lbl_Details");
                            TextBox TBItmQty = (TextBox)GVItms.Rows[rowIndex].Cells[2].FindControl("TBItmQty");
                            TextBox TbItmunt = (TextBox)GVItms.Rows[rowIndex].Cells[3].FindControl("TbItmunt");
                            TextBox Tbrat = (TextBox)GVItms.Rows[rowIndex].Cells[4].FindControl("Tbrat");
                            TextBox Tbamt = (TextBox)GVItms.Rows[rowIndex].Cells[5].FindControl("Tbamt");
                            HiddenField HFDSal = (HiddenField)GVItms.Rows[rowIndex].Cells[7].FindControl("HFDSal");
                            Label lbl_Flag = (Label)GVItms.Rows[i].FindControl("lbl_Flag");
                            Label lblItmQty = (Label)GVItms.Rows[i].FindControl("lblItmQty");
                            GVStkItems = (GridView)GVItms.Rows[i].FindControl("GVStkItems");


                            string invtyp = dt.Rows[i]["INVENTORYTYP"].ToString();

                            if (invtyp != "")
                            {
                                lbl_invtyp.Text = dt.Rows[i]["INVENTORYTYP"].ToString();
                            }
                            else
                            {
                                lbl_invtyp.Text = "0";
                            }

                            DDL_invtyp.SelectedValue = lbl_invtyp.Text;

                            DDLPro.Text = dt.Rows[i]["PARTICULARS"].ToString();

                            DDL_Prorefcde.SelectedValue = dt.Rows[i]["PRODUCT"].ToString();

                            string DIS = dt.Rows[i]["DIS"].ToString();

                            if (DIS != "")
                            {
                                TBDIS.Text = dt.Rows[i]["DIS"].ToString();
                            }
                            else
                            {
                                TBDIS.Text = "0.00";
                            }

                            string QTYAvail = dt.Rows[i]["QTYAVAIL"].ToString();
                            if (QTYAvail != "")
                            {
                                lblItmQty.Text = dt.Rows[i]["QTYAVAIL"].ToString();
                            }
                            else
                            {
                                lblItmQty.Text = "0.00";
                            }

                            string QTY = dt.Rows[i]["QTY"].ToString();
                            if (QTY != "")
                            {
                                TBItmQty.Text = dt.Rows[i]["QTY"].ToString();
                            }
                            else
                            {
                                TBItmQty.Text = "0.00";
                            }
                            TbItmunt.Text = dt.Rows[i]["UNIT"].ToString();

                            string RATE = dt.Rows[i]["RATE"].ToString();
                            if (RATE != "")
                            {
                                Tbrat.Text = dt.Rows[i]["RATE"].ToString();
                            }
                            else
                            {
                                Tbrat.Text = "0.00";
                            }

                            string AMT = dt.Rows[i]["AMT"].ToString();
                            if (AMT != "")
                            {
                                Tbamt.Text = dt.Rows[i]["AMT"].ToString();
                            }
                            else
                            {
                                Tbamt.Text = "0.00";
                            }

                            HFDSal.Value = dt.Rows[i]["DSal_id"].ToString();

                            if (DDL_Prorefcde.SelectedValue == "0")
                            {
                                lbl_Flag.Text = "0";
                            }
                            else
                            {
                                lbl_Flag.Text = "1";
                            }

                            if (TBDISC.Text != "0.00")
                            {
                                string disc = ((Convert.ToDecimal(TBGTtl.Text)) * (Convert.ToDecimal(TBDISC.Text) / 100)).ToString();

                                TBDISAMT.Text = disc;

                                TBTtl.Text = (Convert.ToDecimal(TBGTtl.Text) - Convert.ToDecimal(disc)).ToString();
                            }

                            //if (lbl_Details.Text == "")
                            //{
                            //    DataTable dtdel = (DataTable)ViewState["StkItm"];
                            //    if (dtdel.Rows.Count > 0)
                            //    {
                            //        if (DDL_Prorefcde.SelectedValue != "0")
                            //        {
                            //            GVStkItems.DataSource = dtdel;
                            //            GVStkItems.DataBind();
                            //        }
                            //    }
                            //}


                            string dtls = dt.Rows[i]["Details"].ToString();

                            if (dtls != "")
                            {
                                lbl_Details.Text = dt.Rows[i]["Details"].ToString();
                            }
                            else
                            {
                                lbl_Details.Text = "1";
                            }

                            rowIndex++;

                        //    query = " select Dstk_unt as [SIZE],Dstk_purrat,Dstk_Qty as [QTY] from tbl_Mstk  " +
                        //            " inner join tbl_Dstk on tbl_Mstk.Mstk_id = tbl_Dstk.Mstk_id " +
                        //            " where ProductID = '" + DDL_Prorefcde.SelectedValue.Trim() + "'";

                        //    DataTable dtdetl = new DataTable();

                        //    dtdetl = DBConnection.GetQueryData(query);

                        //    if (dtdetl.Rows.Count > 0)
                        //    {
                        //        GVStkItems.DataSource = dtdetl;
                        //        GVStkItems.DataBind();
                        //    }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //lbl_err.Text = ex.Message.ToString();
            }
        }



        private void update()
        {
            string MSalId = "";
            //using (SqlConnection consnection = new SqlConnection(connectionString))
            //{
            con.Open();

            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            // Start a local transaction.
            transaction = con.BeginTransaction("SalesTrans");

            // Must assign both transaction object and connection 
            // to Command object for a pending local transaction
            command.Connection = con;
            command.Transaction = transaction;

            try
            {
                
                if (TBoutstan.Text != "0.00")
                {
                    totalprev = Convert.ToDouble(lbl_outstan.Text) + Convert.ToDouble(TBoutstan.Text);
                }
                else
                {
                    totalprev = Convert.ToDouble(lbl_outstan.Text);
                }

                //Master Sales 
                command.CommandText =
                    " Update tbl_MSal set MSal_dat ='" + TBSalDat.Text + "' , MSal_Rmk ='" + TBRmk.Text.Trim() +
                    "', MSalOrdid = '" + DDL_SO.SelectedValue + "',CustomerID ='" + DDL_Cust.SelectedValue + "', CreatedBy ='" 
                    + Session["user"].ToString() +
                    "', CreatedAt='" + DateTime.Today.ToString("MM/dd/yyyy") + "', ISActive ='" + chk_Act.Checked + "', iscre='"
                    + ckcrdt.Checked + "',iscash='" +
                    ckcsh.Checked + "',gatpassno ='" + TBGPNo.Text + "',schm ='" + TBSchm.Text + "',bons='" + TBBns.Text +
                    "', Recovery='" + TBRecov.Text.Trim() + "', Outstanding='" + totalprev + "',username='" +
                    Session["Username"] + "', disamt='"+ TBDISAMT.Text.Trim() +"', disc='"+ TBDISC.Text.Trim() +"', custacc='"+DDL_CustAcc.SelectedValue +"' where Msal_sono ='" + TBSalesNum.Text.Trim() + "' and MSal_id='" 
                    + HFMSal.Value.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                command.ExecuteNonQuery();

                    
                // Master Purchase
                command.CommandText = "select MSal_id from tbl_MSal where MSal_sono = '" + TBSalesNum.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                SqlDataAdapter adp = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    MSalId = dt.Rows[0]["MSal_id"].ToString();
                }


                command.CommandText =
                     " Update tbl_Mstk set  Mstk_dat='" + TBSalDat.Text.Trim() +
                     "', Mstk_PurDat='" + TBSalDat.Text.Trim() + "', Mstk_Rmk='" + TBRmk.Text + "', " +
                     " ven_id='', CustomerID='" + DDL_Cust.SelectedValue + "', MPurID='" + HFMSal.Value.Trim() +
                     "', CreatedBy='" + Session["user"].ToString() + "', CreatedAt='" + DateTime.Today.ToString("MM/dd/yyyy") +
                     "', ISActive='" + chk_Act.Checked + "', MSal_id='" + HFMSal.Value.Trim() +
                     "', CompanyId='" + Session["CompanyID"] + "', BranchId='" + Session["BranchID"] + "' where Mstk_sono ='" 
                     + TBSalesNum.Text.Trim() + "' where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                command.ExecuteNonQuery();

                command.CommandText = "select Mstk_id from tbl_Mstk where Mstk_sono = '" + TBSalesNum.Text.Trim() + "' where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                SqlDataAdapter stkadp = new SqlDataAdapter(command);

                DataTable stkdt = new DataTable();
                stkadp.Fill(stkdt);

                if (stkdt.Rows.Count > 0)
                {
                    MSTkId = stkdt.Rows[0]["Mstk_id"].ToString();
                }


                //Detail Sales

                foreach (GridViewRow g1 in GVItms.Rows)
                {
                    string DDL_Par = (g1.FindControl("DDL_Par") as DropDownList).SelectedValue;
                    string DDL_Prorefcde = (g1.FindControl("DDL_Prorefcde") as DropDownList).SelectedValue;
                    //string TBItmDes = (g1.FindControl("TBDISC") as Label).Text;
                    string TBItmQty = (g1.FindControl("TBItmQty") as TextBox).Text;
                    string TbItmunt = (g1.FindControl("TbItmunt") as TextBox).Text;
                    string Tbrat = (g1.FindControl("Tbrat") as TextBox).Text;
                    string Tbamt = (g1.FindControl("Tbamt") as TextBox).Text;
                    string HFDSal = (g1.FindControl("HFDSal") as HiddenField).Value;
                    string lblItmQty = (g1.FindControl("lblItmQty") as Label).Text;
                    
                    
                    if (HFDSal != "")
                    {
                        command.CommandText =
                        " Update tbl_DSal set DSal_ItmDes='" + TBItmDes + "', DSal_ItmQty ='" + TBItmQty + "' , dsalqty='" + lblItmQty + "' , DSal_ItmUnt ='" + TbItmunt +
                        "',DSal_netttl ='" + TBTotal.Text + "', DSal_ttl ='" + TBTotal.Text +
                        "', MSal_id='" + MSalId + "', ProductID ='" + DDL_Prorefcde + "', ProductTypeID='" + DDL_Par + "',Dis='" +
                        TBDISC.Text + "',rat ='" + Tbrat + "', netamt='" + TBTtl.Text + "',  Amt='" + Tbamt + "', GST ='" + TBGST.Text + "', GTtl ='" + TBGTtl.Text + "'where MSal_id ='" + HFMSal.Value.Trim() + "' and DSal_id='" + HFDSal + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        command.CommandText = " INSERT INTO tbl_DSal (DSal_ItmDes, DSal_ItmQty,dsalqty, DSal_ItmUnt, DSal_salcst,  " +
                            " DSal_netttl, DSal_ttl, MSal_id, ProductID, ProductTypeID, Dis, rat, Amt, GST, GTtl,  " +
                            " CompanyId, BranchId, netamt) " +
                            " VALUES " +
                            " ('','" + TBItmQty + "', '" + lblItmQty + "','" + TbItmunt + "','0.00','" + TBTotal.Text + "', '"
                            + TBTotal.Text + "', '" + MSalId + "', '" + DDL_Prorefcde + "','" + DDL_Par + "','" + TBDISC.Text
                            + "','" + Tbrat + "','" + TBTtl.Text + "','" + TBGST.Text + "','" + TBGTtl.Text + "','"
                            + Session["CompanyID"] + "','" + Session["BranchID"] + "','" + TBTtl.Text + "')";

                        command.ExecuteNonQuery();
                    }                    
                

                //Stock Update

                #region Stock Record

                    DataTable dtstkqty = new DataTable();

                    //Detail Stock

                    command.CommandText = "select Dstk_Qty from tbl_Dstk where ProductID = " + DDL_Prorefcde.Trim() + " and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                    SqlDataAdapter Adapter = new SqlDataAdapter(command);
                    Adapter.Fill(dtstkqty);

                    if (dtstkqty.Rows.Count > 0)
                    {
                        for (int t = 0; t < dtstkqty.Rows.Count; t++)
                        {
                            stkqty = dtstkqty.Rows[t]["Dstk_Qty"].ToString();

                            int qty = Convert.ToInt32(stkqty) - Convert.ToInt32(TBItmQty);

                            command.CommandText = " UPDATE tbl_Dstk SET Dstk_Qty = '" + qty + "' where  ProductID = " + DDL_Prorefcde + " and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                            command.ExecuteNonQuery();

                        }
                    }
                    else
                    {
                        command.CommandText = " INSERT INTO tbl_Dstk (Dstk_ItmDes, Dstk_Qty, Dstk_Itmwght, Dstk_ItmUnt, Dstk_rat, Dstk_salrat, Dstk_purrat, Mstk_id, ProductID, CompanyId, BranchId) " +
                        " VALUES " +
                        " ('" + TBItmDes + "','" + TBItmQty + "', '0.00','0.00','" + Tbrat + "','0.00', '0.00', '" + MSTkId + "', '" + DDL_Prorefcde + "','" + Session["CompanyID"] +
                        "','" + Session["BranchID"] + "')";
                        command.ExecuteNonQuery();


                    }
                }
                
                #endregion

                // Attempt to commit the transaction.
                transaction.Commit();

                if (chk_prtd.Checked == true)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=PR','_blank','height=600px,width=600px,scrollbars=1');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_salinv.aspx?ID=SAL&SalID=" + MSalId + "&CUST=" + DDL_Cust.SelectedItem.Text + "','_blank','height=600px,width=600px,scrollbars=1');", true);                
                }

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
                Clear();
                //Response.Redirect("frm_Sal.aspx");
            }
            //}
        }

        private void Save()
        {
            string MSalId = "";
            decimal avapre = 0;
            decimal ttlcre = 0;

            //using (SqlConnection consnection = new SqlConnection(connectionString))
            //{
            con.Open();

            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            // Start a local transaction.
            transaction = con.BeginTransaction("SalesTrans");

            // Must assign both transaction object and connection 
            // to Command object for a pending local transaction
            command.Connection = con;
            command.Transaction = transaction;

            try
            {
                //if (TBoutstan.Text != "0.00" && TBRecov.Text == "0.00")

                if (TBRecov.Text != "0.00")
                {
                    totalprev = Convert.ToDouble(lbl_outstan.Text) - Convert.ToDouble(TBoutstan.Text);
                    lbl_outstan.Text = totalprev.ToString();
                }
                if (TBoutstan.Text != "0.00")
                {
                    totalprev = Convert.ToDouble(lbl_outstan.Text) + Convert.ToDouble(TBoutstan.Text); 
                }
             
                #region Credit Sheets

                command.CommandText = "select CredAmt from tbl_Salcredit where CustomerID='" + DDL_CustAcc.SelectedValue.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                SqlDataAdapter stksalcre = new SqlDataAdapter(command);

                DataTable dtsalcre = new DataTable();
                stksalcre.Fill(dtsalcre);

                if (dtsalcre.Rows.Count > 0)
                {
                    //double recv = Convert.ToDouble(lblOutstan) - Convert.ToDouble(TBRecy);

                    avapre = Convert.ToDecimal(dtsalcre.Rows[0]["CredAmt"]);

                    ttlcre = Convert.ToDecimal(TBTtl.Text.Trim()) + avapre;

                    command.CommandText = " Update tbl_Salcredit set CredAmt = '" + ttlcre + "' where CustomerID='" + DDL_CustAcc.SelectedValue.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                    command.ExecuteNonQuery();
                }
                else
                {
                    //command.CommandText = " insert into tbl_Salcredit (CustomerID,CredAmt) values('" + DDL_CustAcc.SelectedValue.Trim() + "','" + ttlcre + "')";
                    command.CommandText = " insert into tbl_Salcredit (CustomerID,CredAmt,CompanyId,BranchID) values('" + DDL_CustAcc.SelectedValue.Trim() + "','" + TBTtl.Text.Trim() + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                    command.ExecuteNonQuery();
                }

                #endregion
                               


                //Master Sales
                command.CommandText =
                    " INSERT INTO tbl_MSal(MSal_sono, MSal_dat, MSal_Rmk, MSalOrdid, CustomerID, CreatedBy, " +
                    " CreatedAt, ISActive, iscre, iscash, gatpassno, schm, bons, Recovery, Outstanding,  CompanyId, " +
                    " BranchId, username, Booker, SalMan, disamt, disc, custacc) " +
                    "             VALUES  ('" + TBSalesNum.Text.Trim() + "','" + TBSalDat.Text.Trim() + "','" + TBRmk.Text.Trim() + "','" 
                    + DDL_SO.SelectedValue.Trim() + "', '" + DDL_Cust.SelectedValue.Trim() + "','"
                    + Session["user"].ToString() + "','" + DateTime.Today.ToString("MM/dd/yyyy")
                    + "','" + chk_Act.Checked + "','" + ckcrdt.Checked + "','" + ckcsh.Checked + "','"
                    + TBGPNo.Text.Trim() + "','" + TBSchm.Text.Trim() + "','" + TBBns.Text.Trim() + "','" + TBRecov.Text.Trim() + "','" + TBTtl.Text + "','" + Session["CompanyID"] +
                    "','" + Session["BranchID"] + "','" + Session["Username"] + "','"+ DDL_Book.SelectedValue.Trim() + "','"
                    + DDL_SalMan.SelectedValue.Trim() + "','" + TBDISAMT.Text + "','" + TBDISC.Text + "','" + DDL_CustAcc.SelectedValue.Trim() + "')";

                command.ExecuteNonQuery();

                // Master Purchase " + TBSalDat.Text.Trim() + " , " + DateTime.Today + "
                command.CommandText = "select MSal_id from tbl_MSal where MSal_sono = '" + TBSalesNum.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                SqlDataAdapter adp = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    MSalId = dt.Rows[0]["MSal_id"].ToString();
                }

                foreach (GridViewRow g1 in GVItms.Rows)
                {   
                    lbl_Pro = (g1.FindControl("lbl_Pro") as Label).Text;
                    itmsiz = (g1.FindControl("itmsiz") as Label).Text;
                    lblrat = (g1.FindControl("lblrat") as TextBox).Text;
                    ItmQty = (g1.FindControl("ItmQty") as TextBox).Text;
                    TBamt = (g1.FindControl("TBamt") as TextBox).Text;

                    command.CommandText = " select * from  Products where ProductName = '" + lbl_Pro + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                    SqlDataAdapter stkpro = new SqlDataAdapter(command);

                    DataTable prodt = new DataTable();
                    stkpro.Fill(prodt);

                    if (prodt.Rows.Count > 0)
                    {
                        proid = prodt.Rows[0]["ProductID"].ToString();
                    }


                    command.CommandText =
                        " INSERT INTO tbl_DSal (DSal_ItmDes, DSal_ItmQty,dsalqty, DSal_ItmUnt, DSal_salcst,  " +
                        " DSal_netttl, DSal_ttl, MSal_id, ProductID, ProductTypeID, Dis, rat, Amt, GST, GTtl,  " +
                        " CompanyId, BranchId, netamt) " +
                        " VALUES " +
                        " ('" + TBItms.Text + "','" + ItmQty + "', '" + ItmQty + "','" + itmsiz + "','0.00','" + TBTotal.Text + "', '"
                        + TBTotal.Text + "', '" + MSalId + "', '" + proid + "','" + procatid + "','" + TBDISC.Text
                        + "','" + lblrat + "','" + TBTtl.Text + "','" + TBGST.Text + "','" + TBGTtl.Text + "','" 
                        + Session["CompanyID"] + "','" + Session["BranchID"] + "','"+ TBTtl.Text +"')";
                    command.ExecuteNonQuery();
                }

                #region Stock Record

                command.CommandText =
                     " INSERT INTO tbl_Mstk(Mstk_sono, Mstk_dat, Mstk_PurDat, Mstk_Rmk, ven_id, CustomerID, MPurID, " +
                     " CreatedBy, CreatedAt, ISActive, MSal_id, CompanyId, BranchId) " +
                     " VALUES " +
                     " ('" + TBSalesNum.Text.Trim() + "','" + TBSalDat.Text.Trim() + "','" + TBSalDat.Text.Trim() +
                     "','', '','" + DDL_Cust.SelectedValue.Trim() + "','','" + Session["user"].ToString() + "','" +
                     DateTime.Today.ToString("MM/dd/yyyy") + "','" + chk_Act.Checked + "','" +
                     MSalId + "','" + Session["CompanyID"] +  "','" + Session["BranchID"] + "')";

                command.ExecuteNonQuery();

                command.CommandText = "select Mstk_id from tbl_Mstk where Mstk_sono = '" + TBSalesNum.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                SqlDataAdapter stkadp = new SqlDataAdapter(command);

                DataTable stkdt = new DataTable();
                stkadp.Fill(stkdt);

                if (stkdt.Rows.Count > 0)
                {
                    MSTkId = stkdt.Rows[0]["Mstk_id"].ToString();
                }


                command.CommandText = " select * from  Products where ProductName = '" + lbl_Pro + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                SqlDataAdapter stockpro = new SqlDataAdapter(command);

                DataTable prodct = new DataTable();
                stockpro.Fill(prodct);

                if (prodct.Rows.Count > 0)
                {
                    proid = prodct.Rows[0]["ProductID"].ToString();
                }

                foreach (GridViewRow g1 in GVItms.Rows)
                {

                    lbl_Pro = (g1.FindControl("lbl_Pro") as Label).Text;
                    ItmQty = (g1.FindControl("ItmQty") as TextBox).Text;
                    itmsiz = (g1.FindControl("itmsiz") as Label).Text;

                    command.CommandText = " select * from  Products where ProductName = '" + lbl_Pro + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                    SqlDataAdapter stckpro = new SqlDataAdapter(command);

                    DataTable procdt = new DataTable();
                    stckpro.Fill(procdt);

                    if (procdt.Rows.Count > 0)
                    {
                        proid = procdt.Rows[0]["ProductID"].ToString();
                    }


                    DataTable dtstkqty = new DataTable();

                    //Detail Stock

                    command.CommandText = "select Dstk_Qty from tbl_Dstk where ProductID = '" + proid + "' and Dstk_unt= '" + itmsiz + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                    SqlDataAdapter Adapter = new SqlDataAdapter(command);
                    Adapter.Fill(dtstkqty);

                    if (dtstkqty.Rows.Count > 0)
                    {
                        for (int t = 0; t < dtstkqty.Rows.Count; t++)
                        {
                            stkqty = dtstkqty.Rows[t]["Dstk_Qty"].ToString();

                            int qty = Convert.ToInt32(stkqty) - Convert.ToInt32(ItmQty);
                            command.CommandText = " UPDATE tbl_Dstk SET Dstk_Qty = " + qty + " where  ProductID = " + proid + " and Dstk_unt= '" + itmsiz + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                            command.ExecuteNonQuery();

                        }
                    }
                    else
                    {
                        command.CommandText = " INSERT INTO tbl_Dstk (Dstk_ItmDes, Dstk_Qty, Dstk_Itmwght, Dstk_unt, Dstk_rat, Dstk_salrat, Dstk_purrat, Mstk_id, ProductID, CompanyId, BranchId) " +
                            " VALUES " +
                                " ('" + TBItms.Text + "','" + ItmQty + "', '0.00','" + itmsiz + "','" + lblrat + "','0.00', '0.00', '" + MSTkId + "', '" + proid + "','" + Session["CompanyID"] +
                                        "','" + Session["BranchID"] + "')";
                        command.ExecuteNonQuery();

                      

                    }
                }
                
                #endregion

                // Attempt to commit the transaction.
                transaction.Commit();

                if (chk_prtd.Checked == true)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'rpt_salInv.aspx?ID=SAL&SalID=" + MSalId + "&CUSTID=" + DDL_Cust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);                

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_salinv.aspx?ID=SAL&SalID=" + MSalId + "&CUST=" + DDL_Cust.SelectedItem.Text + "','_blank','height=600px,width=600px,scrollbars=1');", true);                


                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'rpt_salInv.aspx?ID=PR','_blank','height=600px,width=600px,scrollbars=1');", true);
                }

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
                Clear();
                //Response.Redirect("frm_Sal.aspx");
            }
            //}
        }


        //public void Save()
        //{
        //    try
        //    {
        //        tbl_MSal msal = new tbl_MSal();
               
        //        msal.MSal_id = HFMSal.Value;
        //        msal.MSal_sono = string.IsNullOrEmpty(TBSalesNum.Text) ? null : TBSalesNum.Text;
        //        msal.MSal_dat = Convert.ToDateTime(string.IsNullOrEmpty(TBSalDat.Text) ? null : TBSalDat.Text);
        //        msal.MSal_Rmk = string.IsNullOrEmpty(TBRmk.Text) ? null : TBRmk.Text;
        //        msal.MSalOrdid = string.IsNullOrEmpty(DDL_SO.SelectedValue) ? null : DDL_SO.SelectedValue;
        //        msal.CustomerID = string.IsNullOrEmpty(DDL_Cust.SelectedValue) ? null : DDL_Cust.SelectedValue; 
        //        msal.CreatedBy = Session["user"].ToString();
        //        msal.CreatedAt = DateTime.Now;
        //        msal.ISActive = Convert.ToBoolean(chk_Act.Checked).ToString();
        //        msal.iscre = Convert.ToBoolean(ckcrdt.Checked).ToString();
        //        msal.iscash = Convert.ToBoolean(ckcsh.Checked).ToString();
        //        msal.gatpassno = string.IsNullOrEmpty(TBGPNo.Text) ? null : TBGPNo.Text;
        //        msal.schm = string.IsNullOrEmpty(TBSchm.Text) ? null : TBSchm.Text;
        //        msal.bons = string.IsNullOrEmpty(TBBns.Text) ? null : TBBns.Text;
                

        //        tbl_MSalManager salmanag = new tbl_MSalManager(msal);
                
        //        salmanag.Save();
                
        //    }
        //    catch (Exception ex)
        //    {
        //        //throw;
        //        lbl_err.Text = ex.Message.ToString();
        //    }
        //}

        public void FillGrid()
        {
            try
            {
                //Sales Order
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandText = " select MSal_id,tbl_MSal.CustomerID, MSal_sono,CustomerName,MSal_dat,tbl_MSal.CreatedBy,tbl_MSal.CreatedAt from tbl_MSal " +
                    //"// inner join Customers_ on tbl_MSal.CustomerID = Customers_.CustomerID where tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' 
                    //and tbl_MSal.BranchId= '" + Session["BranchID"] + "' order by MSal_id desc";

                    cmd.CommandText = " select MSal_id,tbl_MSal.CustomerID, MSal_sono,SubHeadCategoriesName as CustomerName,MSal_dat, " +
                        " tbl_MSal.CreatedBy,tbl_MSal.CreatedAt from tbl_MSal  " +
                        " inner join SubHeadCategories on tbl_MSal.custacc = SubHeadCategories.SubHeadCategoriesGeneratedID  " +
                        " where tbl_MSal.CompanyId = '" + Session["CompanyID"] + "' and tbl_MSal.BranchId= '" +
                        Session["BranchID"] + "' order by MSal_id desc";


                    cmd.Connection = con;
                    con.Open();

                    DataTable dtSal_ = new DataTable();

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtSal_);

                    GVScrhMSal.DataSource = dtSal_;
                    GVScrhMSal.DataBind();
                    ViewState["GetSal"] = dtSal_;

                    con.Close();
                }


            }
            catch (Exception ex)
            {
                throw ex;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //lbl_Heading.Text = "Error!";
                //lblalert.Text = ex.Message;
            }
            try
            {
                
                //dtSal_ = tbl_MSalManager.GetMSalList();

                
            }
            catch (Exception ex)
            {
                //throw;
                throw ex;
            }
        }

        //private void SavDet()
        //{
        //    try
        //    {
        //        string MSalid = "";

        //        string cmds = "select max(cast(MSal_id as int)) as [MSal_id] from tbl_MSal";

        //        SqlCommand cmd = new SqlCommand(cmds, con);
                

        //        DataTable dt_ = new DataTable();
        //        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        //        adp.Fill(dt_);
        //        {
        //            if (HFMSal.Value == "")
        //            {
        //                MSalid = dt_.Rows[0]["MSal_id"].ToString();
        //            }
        //            else if (HFMSal.Value != "")
        //            {
        //                MSalid = HFMSal.Value.Trim();
        //            }
        //        }
        //        if (dt_.Rows.Count > 0)
        //        {
        //            foreach (GridViewRow g1 in GVItms.Rows)
        //            {
        //                string DDL_Par = (g1.FindControl("DDL_Par") as DropDownList).SelectedValue;
        //                string DDL_Prorefcde = (g1.FindControl("DDL_Prorefcde") as DropDownList).SelectedValue;
        //                string TBItmDes = (g1.FindControl("TBDISC") as TextBox).Text;
        //                string TBItmQty = (g1.FindControl("TBItmQty") as TextBox).Text;
        //                string TbItmunt = (g1.FindControl("TbItmunt") as TextBox).Text;
        //                string Tbrat = (g1.FindControl("Tbrat") as TextBox).Text;
        //                string Tbamt = (g1.FindControl("Tbamt") as TextBox).Text;


        //                tbl_DSal dsal = new tbl_DSal();

        //                dsal.DSal_id = HFDSal.Value;
        //                dsal.MSal_id = MSalid;
        //                dsal.ProductID = string.IsNullOrEmpty(DDL_Par) ? null : DDL_Par;
        //                dsal.ProductTypeID = string.IsNullOrEmpty(DDL_Prorefcde) ? null : DDL_Prorefcde;
        //                dsal.DSal_ItmDes = string.IsNullOrEmpty(TBItmDes) ? null : TBItmDes;
        //                dsal.DSal_ItmQty = string.IsNullOrEmpty(TBItmQty) ? null : TBItmQty;
        //                dsal.DSal_ItmUnt = string.IsNullOrEmpty(TbItmunt) ? null : TbItmunt;
        //                dsal.DSal_salcst = string.IsNullOrEmpty(Tbrat) ? null : Tbrat;
        //                dsal.Dis =  string.IsNullOrEmpty(TBItmDes) ? null : TBItmDes;
        //                dsal.rat = string.IsNullOrEmpty(Tbrat) ? null : Tbrat;
        //                dsal.Amt = string.IsNullOrEmpty(Tbamt) ? null : Tbamt;                        
        //                dsal.DSal_netttl = string.IsNullOrEmpty(Tbamt) ? null : Tbamt;
        //                dsal.DSal_ttl = string.IsNullOrEmpty(TBTotal.Text) ? null : TBTotal.Text;
        //                dsal.GST = string.IsNullOrEmpty(TBGST.Text) ? null : TBGST.Text;
        //                dsal.GTtl = string.IsNullOrEmpty(TBGTtl.Text) ? null : TBGTtl.Text;                        

        //                tbl_DSalManager dsalmanag = new tbl_DSalManager(dsal);
        //                dsalmanag.Save();
        //            }
        //        }
        //        else
        //        {
        //            lbl_err.Text = "Sory Record has not been Saved! Inner Error";
        //        }
                

        //    }
        //    catch (Exception ex)
        //    {
        //        lbl_err.Text = ex.Message.ToString();
        //    }

        //}
        #endregion

        #region Events

        protected void TBItms_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new DataColumn("PRODUCT", typeof(string)));
                dt.Columns.Add(new DataColumn("SIZE", typeof(string)));
                dt.Columns.Add(new DataColumn("QTY", typeof(string)));
                dt.Columns.Add(new DataColumn("RATE", typeof(string)));
                dt.Columns.Add(new DataColumn("AMT", typeof(string)));
                dt.Columns.Add(new DataColumn("DSal_id", typeof(string)));

                // Checking Product

                query = " select  * from  Products where ProductName = '" + TBItms.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    PurItm = dt_.Rows[0]["ProductID"].ToString();

                    //query = " select '0.00' as [Dstk_ItmQty],Dstk_unt as [Dstk_sizes] from tbl_Dstk where ProductID = '" + PurItm + "'";
                    query = " select Dstk_ItmDes as [PRODUCT],Dstk_unt as [SIZE],  Dstk_Qty as [QTY], '0' as [QUANTY],  Dstk_rat as [RATE], " +
                            " (Dstk_rat * Dstk_Qty) as [AMT], '' as [DSal_id] from tbl_Dstk where ProductID = '" + PurItm + "' and Dstk_Qty > 0 and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                    DataTable dtpro_ = new DataTable();

                    dtpro_ = DBConnection.GetQueryData(query);

                    if (dtpro_.Rows.Count > 0)
                    {
                        TB_Rat.Text = dtpro_.Rows[0]["RATE"].ToString();

                        GVStkItems.DataSource = dtpro_;
                        GVStkItems.DataBind();

                        decimal ttl = 0;
                        Label itmsiz = null;
                        Label lblQty = null;
                        TextBox ItReqQty = null;

                        foreach (GridViewRow row in GVStkItems.Rows)
                        {
                            itmsiz = (Label)row.FindControl("itmsiz");
                            lblQty = (Label)row.FindControl("lblQty");
                            ItReqQty = (TextBox)row.FindControl("ItReqQty");

                            if (ItReqQty.Text == "")
                            {
                                v_qty.Text = "Please Fill Quantity..";
                                btn_copy.Enabled = false;

                                return;
                            }
                            else
                            {
                                dr = dt.NewRow();
                                dr[0] = TBItms.Text;
                                dr[1] = itmsiz.Text;
                                dr[2] = lblQty.Text;
                                dr[3] = TB_Rat.Text;
                                ttl = Convert.ToDecimal(lblQty.Text.Trim()) * Convert.ToDecimal(TB_Rat.Text.Trim());
                                dr[4] = ttl.ToString();
                                dr[5] = "";

                                dt.Rows.Add(dr);

                                btn_copy.Enabled = true;
                            }
                        }

                        ViewState["dt_salItm"] = dt;
                    }else
                    {
                        TB_Rat.Text = "0";
                        GVStkItems.DataSource = null;
                        GVStkItems.DataBind();
                    }
                }
                else
                {
                    query = " INSERT INTO Products " +
                                    " ([ProductName],[ProductType],[CompanyId],[BranchId],[IsActive]) VALUES('"
                                    + TBItms.Text.Trim() + "','" + TBCat.Text.Trim()
                                    + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "','1')";
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }

                TB_Rat.Focus();
                TB_Rat.Attributes.Add("onfocusin", "select();");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void btn_copy_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBCat.Text == "")
                {
                    v_category.Text = "Fill";
                    TBCat.Focus();
                    v_rate.Text = "";
                    v_items.Text = "";

                }
                else if (TBItms.Text == "")
                {
                    v_category.Text = "";
                    v_rate.Text = "";
                    v_items.Text = "Fill";
                    TBItms.Focus();

                }
                else if (TB_Rat.Text == "")
                {
                    v_rate.Text = "Fill";
                    TB_Rat.Focus();
                    v_category.Text = "";
                    v_items.Text = "";

                }
                else
                {
                    AddNewItems();

                    TBCat.Text = "";
                    TBItms.Text = "";
                    TB_Rat.Text = "";
                    ViewState["dt_salItm"] = null;

                    dt_ = new DataTable();

                    GVStkItems.DataSource = dt_;
                    GVStkItems.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void TBCat_TextChanged(object sender, EventArgs e)
        {
            
            try
            {
                Session["cat"] = TBCat.Text.Trim();
                TBItms.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ItQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox ItReqQty = null;
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Columns.Add(new System.Data.DataColumn("PRODUCT", typeof(String)));
                dt.Columns.Add(new System.Data.DataColumn("SIZE", typeof(String)));
                dt.Columns.Add(new System.Data.DataColumn("QTY", typeof(String)));
                dt.Columns.Add(new System.Data.DataColumn("RATE", typeof(String)));
                dt.Columns.Add(new System.Data.DataColumn("AMT", typeof(String)));


                foreach (GridViewRow row in GVStkItems.Rows)
                {
                    TextBox itq =(TextBox)row.FindControl("ItReqQty");
                    Label itmsiz = (Label)row.FindControl("itmsiz");
                    ItReqQty = (TextBox)row.FindControl("ItReqQty");
                    Label lblQty = (Label)row.FindControl("lblQty");

                    if (ItReqQty.Text == "")
                    {
                        v_qty.Text = "Please Fill Quantity..";
                        btn_copy.Enabled = false;
                    }
                    else
                    {
                        decimal AvailQty = Convert.ToDecimal(lblQty.Text.Trim());
                        decimal ReqQty = Convert.ToDecimal(ItReqQty.Text.Trim());

                        if (ReqQty > AvailQty)
                        {
                            v_qty.Text = "Required Quantity is Greater then Given...";
                            btn_copy.Enabled = false;

                            return;
                        }
                        else
                        {
                            v_qty.Text = "";
                            btn_copy.Enabled = true;

                            decimal ttl = 0;

                            dr = dt.NewRow();
                            dr[0] = TBItms.Text;
                            dr[1] = itmsiz.Text;
                            dr[2] = ItReqQty.Text;
                            dr[3] = TB_Rat.Text;

                            ttl = Convert.ToDecimal(ItReqQty.Text.Trim()) * Convert.ToDecimal(TB_Rat.Text.Trim());

                            dr[4] = ttl.ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                }
                ViewState["dt_salItm"] = dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }        
        }

        protected void TBSearchSalesNum_TextChanged(object sender, EventArgs e)
        {
            try
            {  
                if (TBSearchSales.Text != "")
                {
                    DataTable dt_sal = new DataTable();
                    dt_sal = tbl_MSalManager.GetMSalList(TBSearchSales.Text.Trim());// MPurchaseManager.GetMPurList(TBSearchSales.Text);

                    GVScrhMSal.DataSource = dt_sal;
                    GVScrhMSal.DataBind();
                }
                else if (TBSearchSales.Text == "")
                {
                    FillGrid();
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //lblalert.Text = ex.Message;
            }
        }

        protected void GVScrhMSal_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVScrhMSal.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void GVScrhMSal_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow row;

            try
            {
                if (e.CommandName == "Select")
                {   
                    row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    
                    string SID = GVScrhMSal.DataKeys[row.RowIndex].Values[0].ToString();
                    HFMSal.Value = SID;

                    string query1 = " select MSal_id,MSal_sono,custacc,gatpassno, custacc, disamt, Booker, SalMan, " +
                        " Outstanding, Recovery,  convert(date, cast(MSal_dat as date) ,105) as [MSal_dat],iscash, " +
                        " iscre,MSal_Rmk,disc, disamt " +
                        " MSalOrdid,CustomerID,ISActive from tbl_MSal where MSal_id = '" + HFMSal.Value + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                        
                    DataTable dtMSal = new DataTable();
                    SqlCommand cmd = new SqlCommand(query1, con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtMSal);

                    if (dtMSal.Rows.Count > 0)
                    {
                        HFMSal.Value = dtMSal.Rows[0]["MSal_id"].ToString();
                        chk_Act.Checked = Convert.ToBoolean(dtMSal.Rows[0]["ISActive"].ToString());
                        DDL_SO.SelectedValue = dtMSal.Rows[0]["MSalOrdid"].ToString();
                        DDL_Cust.SelectedValue = dtMSal.Rows[0]["custacc"].ToString();
                        DDL_CustAcc.SelectedValue = dtMSal.Rows[0]["custacc"].ToString();
                        TBSalDat.Text = dtMSal.Rows[0]["MSal_dat"].ToString();
                        TBSalesNum.Text = dtMSal.Rows[0]["MSal_sono"].ToString();
                        TBRmk.Text = dtMSal.Rows[0]["MSal_Rmk"].ToString();
                        TBGPNo.Text = dtMSal.Rows[0]["gatpassno"].ToString();
                        ckcsh.Checked = Convert.ToBoolean(dtMSal.Rows[0]["iscash"]);
                        ckcrdt.Checked = Convert.ToBoolean(dtMSal.Rows[0]["iscre"]);
                        TBoutstan.Text = dtMSal.Rows[0]["Outstanding"].ToString();
                        if (TBoutstan.Text == "")
                        {
                            TBoutstan.Text = "0.00";
                        }
                        TBRecov.Text = dtMSal.Rows[0]["Recovery"].ToString();
                        if (TBRecov.Text == "")
                        {
                            TBRecov.Text = "0.00";
                        }
                        DDL_Book.SelectedValue = dtMSal.Rows[0]["Booker"].ToString();
                        DDL_SalMan.SelectedValue = dtMSal.Rows[0]["SalMan"].ToString();
                        TBDISC.Text = dtMSal.Rows[0]["disc"].ToString();
                        TBDISAMT.Text = dtMSal.Rows[0]["disamt"].ToString();
                    }

                    string query2 = " select tbl_DSal.ProductTypeID as [PARTICULARS] ,tbl_DSal.ProductID as [PRODUCT] , " +
                        " DIS,DSal_ItmQty as [QTY],DSal_ItmUnt as [UNIT],GTtl ,dsalqty as [QTYAVAIL], " +
                        " tbl_DSal.Dsal_id, rat as [RATE],Amt as [AMT] , netamt  from tbl_DSal  inner join Products " +
                        " on tbl_DSal.ProductID =  Products.ProductID " +
                        " inner join tbl_Dstk on Products.ProductID = tbl_Dstk.ProductID " +
                        " where  MSal_id = '" + HFMSal.Value + "' and Products.CompanyId='" + Session["CompanyID"] + "' and Products.BranchId='" + Session["BranchID"] + "'";

                    DataTable dtDSO = new DataTable();
                    SqlCommand cmdcn = new SqlCommand(query2, con);
                    SqlDataAdapter adpcn = new SqlDataAdapter(cmdcn);
                    adpcn.Fill(dtDSO);

                    GVItms.DataSource = dtDSO;
                    GVItms.DataBind();

                    ViewState["dt_adItm"] = dtDSO;

                    
                    for (int k = 0; k < GVItms.Rows.Count; k++)
                    {
                        DropDownList DDL_Prorefcde = (DropDownList)GVItms.Rows[k].Cells[1].FindControl("DDL_Prorefcde");
                        DropDownList DDL_Par = (DropDownList)GVItms.Rows[k].Cells[1].FindControl("DDL_Par");
                        Label lbl_Par = (Label)GVItms.Rows[k].FindControl("lbl_Par");
                        Label lbl_Pro = (Label)GVItms.Rows[k].FindControl("lbl_Pro");

                        for (int j = 0; j < dtDSO.Rows.Count; j++)
                        {
                            DDL_Prorefcde.SelectedValue = lbl_Pro.Text;
                            DDL_Par.SelectedValue = lbl_Par.Text;
                        }
                    }
                    if (dtDSO.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtDSO.Rows.Count; j++)
                        {
                            TBTotal.Text = dtDSO.Rows[j]["PRODUCT"].ToString(); 
                            TBGTtl.Text = dtDSO.Rows[j]["GTtl"].ToString();
                            TBTtl.Text = dtDSO.Rows[j]["netamt"].ToString();
                        }
                    }
            #region OLD LOGIC
                    
                                //if (dtDSO.Rows.Count > 0)
                                //{
                                //    int rowIndex = 0;
                                //    for (int k = 0; k < GVItms.Rows.Count; k++)
                                //    {
                                //        DropDownList DDL_Par = (DropDownList)GVItms.Rows[k].Cells[0].FindControl("DDL_Par");
                                //        DropDownList DDL_Prorefcde = (DropDownList)GVItms.Rows[k].Cells[1].FindControl("DDL_Prorefcde");
                                //        Label lbl_Par = (Label)GVItms.Rows[k].FindControl("lbl_Par");

                                //        for (int j = 0; j < dtDSO.Rows.Count; j++)
                                //        {
                                //            //DDL_Par.SelectedValue = dtDSO.Rows[j]["ProductTypeID"].ToString();
                                //            lbl_Par.Text = dtDSO.Rows[j]["PRODUCT"].ToString();
                                //            //getPro(DDL_Par.SelectedValue.Trim());
                                //            //DDL_Prorefcde.SelectedValue = dtDSO.Rows[j]["ProductID"].ToString();
                                //        }
                                //        //BindPar();
                                //        //DDL_Par.SelectedValue = dtDSO.Rows[j]["ProductTypeID"].ToString();
                                //    }
                                //    rowIndex++;
                                //}
                                //else
                                //{
                                //    SetInitRowSal();
                                //}
            #endregion
                }
                if (e.CommandName == "Show")
                {
                    row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    string SalID = GVScrhMSal.DataKeys[row.RowIndex].Values[0].ToString();
                    string CUST = GVScrhMSal.Rows[row.RowIndex].Cells[1].Text;
                    //string CUSTID = GVScrhMSal.DataKeys[row.RowIndex].Values[1].ToString();

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'rpt_salInv.aspx?ID=SAL&SalID=" + SalID + "','_blank','height=600px,width=600px,scrollbars=1');", true);                
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_salinv.aspx?ID=SAL&SalID=" + SalID + "&CUST=" + CUST + "','_blank','height=600px,width=600px,scrollbars=1');", true);                
                }
            }
            catch (Exception ex)
            {
                throw ex;                
            }
        }

        protected void GVScrhMSal_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string MSalSNO = Server.HtmlDecode(GVScrhMSal.Rows[e.RowIndex].Cells[0].Text.ToString());

                SqlCommand cmd = new SqlCommand();

                cmd = new SqlCommand("sp_del_Sal", con);
                cmd.Parameters.Add("@MSalsono", SqlDbType.VarChar).Value = MSalSNO;
                cmd.Parameters.Add("@CompanyId", SqlDbType.VarChar).Value = Session["CompanyID"].ToString();
                cmd.Parameters.Add("@BranchId", SqlDbType.VarChar).Value = Session["BranchID"].ToString();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                lbl_err.Text = "Sales # " + MSalSNO + " has been Deleted!";
                FillGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void GVScrhMSal_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GVScrhMSal.PageIndex = e.NewSelectedIndex;
            FillGrid();
        }

        protected void ddl_Cust_SelectedIndexChanged(object sender, EventArgs e)
        {
            v_cname.Text = "";
            try
            {
                SqlCommand cmd;
                SqlDataAdapter adp;
                string query;

                DataTable dtout_ = new DataTable();

                query = "select * from tbl_Salcredit where CustomerID ='" + DDL_Cust.SelectedValue.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                 cmd = new SqlCommand(query, con);

                 adp = new SqlDataAdapter(cmd);

                adp.Fill(dtout_);

                if (dtout_.Rows.Count > 0)
                {
                    lbl_outstan.Text = dtout_.Rows[0]["CredAmt"].ToString();
                }
                else
                {
                    lbl_outstan.Text = "0.00";
                }

                query = " select saleper  from Customers_ where CustomerID = " + DDL_Cust.SelectedValue.Trim() +" and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                DataTable dtper = new DataTable();
                cmd = new SqlCommand(query, con);

                adp = new SqlDataAdapter(cmd);

                adp.Fill(dtper);

                if (dtper.Rows.Count > 0)
                {
                    TBDISC.Text = dtper.Rows[0]["saleper"].ToString();
                    
                    if (TBDISC.Text == "")
                    {
                        TBDISC.Text = "0.00";
                    }
                }
                else
                {
                    TBDISC.Text = "0.00";
                }

                // For Account

                query = " select SubHeadCategoriesGeneratedID,SubHeadCategoriesName from SubHeadCategories " +
                    " where  SubHeadCategoriesName = '" + DDL_Cust.SelectedItem.Text.Trim() + "' and SubHeadGeneratedID ='0011' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    DDL_CustAcc.SelectedValue = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                }

                TBCat.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBGTtl.Text == "" || TBGTtl.Text == "0.00" || TBGTtl.Text == "0")
                {
                    v_grand.Text = "Fill Grand Total";
                    TBGTtl.Focus();
                    v_rate.Text = "";
                    v_category.Text = "";
                    v_items.Text = "";

                }
                else if (TBTtl.Text == "" || TBTtl.Text == "0.00" || TBTtl.Text == "0")
                {
                    v_ttl.Text = "Fill Total";
                    v_grand.Text = "";
                    TBTtl.Focus();
                    v_rate.Text = "";
                    v_category.Text = "";
                    v_items.Text = "";
                }
                else if (DDL_CustAcc.SelectedValue == "0")
                {
                    v_cname.Text = "Please Select Customers Account";
                    v_ttl.Text = "";
                    v_grand.Text = "";
                    v_rate.Text = "";
                    v_category.Text = "";
                    v_items.Text = "";
                    DDL_CustAcc.Focus();
                }
                else if (DDL_Cust.SelectedValue == "0")
                {
                    v_cname.Text = "Please Select Customers..";
                    v_ttl.Text = "";
                    v_grand.Text = "";
                    v_rate.Text = "";
                    v_category.Text = "";
                    v_items.Text = "";
                    DDL_Cust.Focus();
                }else
                {
                    if (HFMSal.Value == "")
                    {
                        Save();
                    }
                    else if (HFMSal.Value != "")
                    {
                        update();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnRevert_Click(object sender, EventArgs e)
        {
            //Clear();
            Response.Redirect("frm_Sal.aspx");
        }

        protected void GVItms_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ViewState["dt_adItm"] != null)
            {
                DataTable dt = (DataTable)ViewState["dt_adItm"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);

                HiddenField HFDSal = (HiddenField)GVItms.Rows[rowIndex].Cells[8].FindControl("HFDSal");

                if (HFDSal.Value != "")
                {
                    try
                    {
                        query = "delete from tbl_DSal where DSal_id = '" + HFDSal.Value + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                        DBConnection db = new DBConnection();

                        db.CRUDRecords(query);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["dt_adItm"] = dt;

                    GVItms.DataSource = dt;
                    GVItms.DataBind();

                    SetPreRowSal();

                    float GTotal = 0;
                    for (int j = 0; j < GVItms.Rows.Count; j++)
                    {
                        TextBox total = (TextBox)GVItms.Rows[j].FindControl("Tbamt");

                        GTotal += Convert.ToSingle(total.Text);
                    }
                    TBTotal.Text = GTotal.ToString();
                    TBGTtl.Text = GTotal.ToString();


                    if (TBDISC.Text != "0.00")
                    {
                        string disc = ((Convert.ToDecimal(TBGTtl.Text)) * (Convert.ToDecimal(TBDISC.Text) / 100)).ToString();

                        TBDISAMT.Text = disc;

                        TBTtl.Text = (Convert.ToDecimal(TBGTtl.Text) - Convert.ToDecimal(disc)).ToString();
                    }


                    ptnSno();
                }
            }
        }

        protected void linkbtnadd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void TBItmQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < GVItms.Rows.Count; j++)
                {
                    TextBox TBItmQty = (TextBox)GVItms.Rows[j].FindControl("TBItmQty");
                    Label lbshw = (Label)GVItms.Rows[j].FindControl("lbshw");
                    TextBox TbItmCst = (TextBox)GVItms.Rows[j].FindControl("Tbrat");
                    TextBox Tbamt = (TextBox)GVItms.Rows[j].FindControl("Tbamt");
                    DropDownList DDL_Prorefcde = (DropDownList)GVItms.Rows[j].FindControl("DDL_Prorefcde");
                    Label Isupdat =(Label)GVItms.Rows[j].FindControl("Isupdat");
                    LinkButton lnkbtnadd = (LinkButton)GVItms.Rows[j].FindControl("lnkbtnadd");

                    lbshw.Text = "";
                    TbItmCst.Enabled = true;
                    Tbamt.Enabled = true;
                    lnkbtnadd.Enabled = true;
                    btnSave.Enabled = true;

                    double result = (Convert.ToDouble(TBItmQty.Text.Trim()) * Convert.ToDouble(TbItmCst.Text));
                    Tbamt.Text = result.ToString();

                    float GTotal = 0;
                    for (int k = 0; k < GVItms.Rows.Count; k++)
                    {
                        TextBox total = (TextBox)GVItms.Rows[k].FindControl("TBAmt");
                        GTotal += Convert.ToSingle(total.Text);
                    }

                    TBGTtl.Text = GTotal.ToString();
                    TBTtl.Text = GTotal.ToString();

                    Isupdat.Text = "1";
                }
                if (TBDISC.Text != "0.00")
                {
                    decimal discount = Convert.ToDecimal(TBGTtl.Text) * Convert.ToDecimal(TBDISC.Text.Trim()) / 100;
                    TBTtl.Text = (Convert.ToDecimal(TBGTtl.Text) - discount).ToString();
                }
                else
                {
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GV_DSR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_DSR.PageIndex = e.NewPageIndex;
            FillGrid(tb_cust.Text.Trim(),TBSalDat.Text.Trim());
        }

        protected void GV_DSR_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                double result;

                if (e.CommandName == "Select")
                {
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    //For Main

                    string DSRID = GV_DSR.DataKeys[row.RowIndex].Values[0].ToString();

                    query = " select tbl_Mdsr.dsrid, tbl_ddsr.ProductTypeID,tbl_Mdsr.CustomerID,tbl_Mdsr.saleper, " +
                        " CustomerName, dsrrmk from tbl_MDSR inner join tbl_ddsr  on tbl_Mdsr.dsrid = tbl_ddsr.dsrid " +
                        " inner join Customers_ on tbl_Mdsr.CustomerID = Customers_.CustomerID " +
                        " where tbl_Mdsr.dsrid='" + DSRID + "' and Customers_.CompanyId='" + Session["CompanyID"] + "' and Customers_.BranchId='" + Session["BranchID"] + "'";


                    DataTable dt_ = new DataTable();
                    dt_ = DataAccess.DBConnection.GetDataTable(query);

                    if (dt_.Rows.Count > 0)
                    {
                        HFDSRID.Value = dt_.Rows[0]["dsrid"].ToString();
                        DDL_Cust.SelectedValue = dt_.Rows[0]["CustomerID"].ToString();
                        TBDISC.Text = dt_.Rows[0]["saleper"].ToString();

                        //Sale Percent

                        query = "select * from tbl_Salcredit where CustomerID ='" + DDL_Cust.SelectedValue.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                        DataTable dtsalcre = new DataTable();

                        dtsalcre = DBConnection.GetQueryData(query);

                        if (dtsalcre.Rows.Count > 0)
                        {
                            lbl_outstan.Text = dtsalcre.Rows[0]["CredAmt"].ToString();
                        }
                        else
                        {
                            lbl_outstan.Text = "0.00";
                        }

                        TBRmk.Text = dt_.Rows[0]["dsrrmk"].ToString();
                    }

                    //for account

                    query = " select subheadcategoryfourGeneratedID,subheadcategoryfourName from subheadcategoryfour where subheadcategoryfourName='" + tb_cust.Text.Trim() + "' and  SubHeadCategoriesGeneratedID='MB00004' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                    dt_ = DBConnection.GetQueryData(query);

                    if (dt_.Rows.Count > 0)
                    {
                        //DDL_CustAcc.SelectedValue = dt_.Rows[0]["subheadcategoryfourGeneratedID"].ToString();
                        //TBCust.Text = dt_.Rows[0]["subheadcategoryfourName"].ToString();
                    }

                    query = " select  '' as [PARTICULARS], tbl_DDSR.ProductID as [PRODUCT], " +
                        "  '' as [DIS], tbl_Dstk.Dstk_ItmQty as [QTYAVAIL], tbl_DDSR.Qty as [QTY], " +
                        " Products.Unit as [UNIT],tbl_DDSR.ProductID,  tbl_DDSR.salrat as [RATE], '' as [DSal_id], " +
                        " Amt as [AMT], ttlamt from tbl_MDSR  inner join tbl_DDSR on " +
                        "  tbl_MDSR.dsrid = tbl_DDSR.dsrid  inner join Products on  tbl_DDSR.ProductID = Products.ProductID  " +
                        " inner join  tbl_Dstk on Products.ProductID = tbl_Dstk.ProductID  where tbl_MDSR.dsrid='" + DSRID + "' and Products.CompanyId='" + Session["CompanyID"] + "' and Products.BranchId='" + Session["BranchID"] + "'";

                    dt_ = DataAccess.DBConnection.GetDataTable(query);

                    if (dt_.Rows.Count > 0)
                    {
                        GVItms.DataSource = dt_;
                        GVItms.DataBind();


                        ViewState["dt_adItm"] = dt_;
                        //TBTtl.Text = dt_.Rows[0]["ttlamt"].ToString();



                        for (int i = 0; i < dt_.Rows.Count; i++)
                        {

                        for (int j = 0; j < GVItms.Rows.Count; j++)
                        {
                            Label lbl_Pro = (Label)GVItms.Rows[j].FindControl("lbl_Pro");
                            DropDownList DDL_Prorefcde = (DropDownList)GVItms.Rows[j].FindControl("DDL_Prorefcde");
                            TextBox TBItmQty = (TextBox)GVItms.Rows[j].FindControl("TBItmQty");
                            Label lbshw = (Label)GVItms.Rows[j].FindControl("lbshw");
                            TextBox TbItmCst = (TextBox)GVItms.Rows[j].FindControl("Tbrat");
                            TextBox Tbamt = (TextBox)GVItms.Rows[j].FindControl("Tbamt");
                            Label Isupdat = (Label)GVItms.Rows[j].FindControl("Isupdat");
                            LinkButton lnkbtnadd = (LinkButton)GVItms.Rows[j].FindControl("lnkbtnadd");

                            DDL_Prorefcde.SelectedValue = lbl_Pro.Text.Trim();

                           
                            DataTable stkqty = new DataTable();

                            SqlCommand commnd = new SqlCommand();
                            commnd.Connection = con;
                            commnd.CommandText = "select Dstk_ItmQty from tbl_Dstk where ProductID = " + DDL_Prorefcde.SelectedValue.Trim() + " and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                            con.Open();
                            SqlDataAdapter Adapter = new SqlDataAdapter(commnd);
                            Adapter.Fill(stkqty);

                            if (stkqty.Rows.Count > 0)
                            {
                                for (int t = 0; t < stkqty.Rows.Count; t++)
                                {
                                    int stkquanty = Convert.ToInt32(stkqty.Rows[t]["Dstk_ItmQty"]);
                                    int qty = Convert.ToInt32(TBItmQty.Text);
                                    if (qty > stkquanty)
                                    {
                                        lbshw.Text = "Item Qunatity is less in Store!!";
                                        lbshw.ForeColor = System.Drawing.Color.Red;
                                        TbItmCst.Enabled = false;
                                        Tbamt.Enabled = false;
                                        Tbamt.Text = "0.00";
                                        lnkbtnadd.Enabled = false;
                                        btnSave.Enabled = false;

                                    }
                                    else
                                    {
                                        lbshw.Text = "";
                                        TbItmCst.Enabled = true;
                                        Tbamt.Enabled = true;
                                        lnkbtnadd.Enabled = true;
                                        btnSave.Enabled = true;

                                        float GTotal = 0;
                                        for (int k = 0; k < GVItms.Rows.Count; k++)
                                        {
                                            TextBox total = (TextBox)GVItms.Rows[k].FindControl("TBAmt");
                                            GTotal += Convert.ToSingle(total.Text);
                                            TBGTtl.Text = GTotal.ToString();
                                            //TBTtl.Text = Tbamt.Text;
                                        }
                                        Isupdat.Text = "1";
                                    }
                                }
                            }
                            con.Close();
                            
                            //double result = (Convert.ToDouble(TBItmQty.Text.Trim()) * Convert.ToDouble(TbItmCst.Text));

                            //if (TBDISC.Text != "0.00")
                            //{
                            //    double discount = result * Convert.ToDouble(TBDISC.Text.Trim()) / 100;
                            //    Tbamt.Text = (result - discount).ToString();
                            //}
                            //else
                            //{
                            //    Tbamt.Text = result.ToString();
                            //}

                            //TBGTtl.Text = Tbamt.Text;
                            //TBTtl.Text = Tbamt.Text;

                            //float GTotal = 0;
                            //for (int k = 0; k < GVItms.Rows.Count; k++)
                            //{
                            //    TextBox total = (TextBox)GVItms.Rows[k].FindControl("TBAmt");
                            //    GTotal += Convert.ToSingle(total.Text);
                            //    TBGTtl.Text = GTotal.ToString();
                            //    TBTtl.Text = Tbamt.Text;
                            //}
                            //Isupdat.Text = "1";
                            }
                        }

                        //result = (Convert.ToDouble(TBItmQty.Text.Trim()) * Convert.ToDouble(TbItmCst.Text));

                        if (TBDISC.Text != "0.00")
                        {
                            decimal discount = Convert.ToDecimal(TBGTtl.Text.Trim()) * Convert.ToDecimal(TBDISC.Text.Trim()) / 100;

                            TBTtl.Text = (Convert.ToDecimal(TBGTtl.Text.Trim()) - discount).ToString();
                        }
                        else
                        {
                            TBTtl.Text = TBGTtl.Text.Trim().ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lbl_err.Text = ex.Message;
            }
        }

        protected void GVStkItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ViewState["dt_adItm"] != null)
            {
                DataTable dt = (DataTable)ViewState["dt_adItm"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["dt_adItm"] = dt;

                    GVStkItems.DataSource = dt;
                    GVStkItems.DataBind();

                    //SetPreRowitm();
                }
            }
        }

        #endregion

        public void Clear()
        {
            SetInitRowSal();
            ptnSno();
            BindPar();
            TBSalDat.Text = DateTime.Now.ToShortDateString();
            TBRmk.Text = "";
            chk_prtd.Checked = true;
            TBTotal.Text = "";
            TBGPNo.Text = "GP000";
            DDL_Cust.SelectedValue = "0";
            TBDISC.Text = "0.00";
            TBGTtl.Text = "0.00";
            TBRecov.Text = "0.00";
            TBoutstan.Text = "0.00";
            TBTtl.Text = "0.00";
            HFMSal.Value = "";
            FillGrid();
            DDL_SalMan.SelectedValue = "0";
            DDL_Book.SelectedValue = "0";
            DDL_CustAcc.SelectedValue = "0";
            TBDISAMT.Text = "0.00";
        }

        protected void chk_SO_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_SO.Checked == true)
            {
                pnlSO.Visible = true;
            }
            else if (chk_SO.Checked == false)
            {
                pnlSO.Visible = false; 
            }
        }

        protected void ckSch_CheckedChanged(object sender, EventArgs e)
        {
            if (ckSch.Checked == true)
            {
                pnl_sch.Visible = true;
            }
            else if (ckSch.Checked == false)
            {
                pnl_sch.Visible = false;
            }
        }

        protected void DDL_Par_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
                {
                    DropDownList ddl = (DropDownList)sender;
                    GridViewRow row = (GridViewRow)ddl.NamingContainer;

                    if (row != null)
                    {
                        string selectedValue = ((DropDownList)(row.FindControl("DDL_Par"))).SelectedValue;
                        DropDownList DDL_Prorefcde = (DropDownList)row.FindControl("DDL_Prorefcde");
                        
                        using (SqlCommand cmdpro = new SqlCommand())
                        {
                            cmdpro.CommandText = " select rtrim('[' + CAST(ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], ProductID from Products  where ProductTypeID = '" + selectedValue.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                            cmdpro.Connection = con;
                            con.Open();

                            DataTable dtpro = new DataTable();
                            SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                            adp.Fill(dtpro);

                            if (dtpro.Rows.Count > 0)
                            {
                                DDL_Prorefcde.DataSource = dtpro;
                                DDL_Prorefcde.DataValueField = "ProductID";
                                DDL_Prorefcde.DataTextField = "ProductName";
                                DDL_Prorefcde.DataBind();
                                DDL_Prorefcde.Items.Insert(0, new ListItem("--Select Product--", "0"));
                            }
                            else {

                                DDL_Prorefcde.Items.Clear();
                            }
                            con.Close();
                        }
                        //DDL_Prorefcde.Focus();
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                    //lbl_err.Text = ex.Message.ToString();
                }
        }

        public void getPro()
        {
            try
            {
                using (SqlCommand cmdpro = new SqlCommand())
                {
                    con.Close();

                    //cmdpro.CommandText = " select distinct(ProductID) as [ProductID], Dstk_ItmDes as [ProductName] from tbl_Dstk ";
                    cmdpro.CommandText = " select distinct(ProductID) as [ProductID], Dstk_ItmDes as [ProductName] from tbl_Dstk where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'"+
                        " union all " +
                        " select distinct(ProductID) as [ProductID], RDstk_ItmDes as [ProductName] from tbl_RDstk  where  CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                    cmdpro.Connection = con;
                    con.Open();

                    DataTable dtpro = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                    adp.Fill(dtpro);

                    for (int i = 0; i < GVItms.Rows.Count; i++)
                    {
                        DropDownList DDL_Prorefcde = (DropDownList)GVItms.Rows[i].Cells[1].FindControl("DDL_Prorefcde");

                        if (DDL_Prorefcde.SelectedValue == "0")
                        {
                            DDL_Prorefcde.DataSource = dtpro;
                            DDL_Prorefcde.DataTextField = "ProductName";
                            DDL_Prorefcde.DataValueField = "ProductID";
                            DDL_Prorefcde.DataBind();
                            DDL_Prorefcde.Items.Insert(0, new ListItem("--Select--", "0"));
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void getPro(string DDLPro)
        {
            try
            {
                using (SqlCommand cmdpro = new SqlCommand())
                {
                    con.Close();

                    if (DDLPro != "")
                    {
                        cmdpro.CommandText = " select rtrim('[' + CAST(ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], ProductID from Products  where ProductTypeID = '" + DDLPro + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                        //cmdpro.CommandText = " select rtrim('[' + CAST(ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], ProductID from Products";
                    }

                    cmdpro.Connection = con;
                    con.Open();

                    DataTable dtpro = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                    adp.Fill(dtpro);

                    for (int i = 0; i < GVItms.Rows.Count; i++)
                    {
                        DropDownList DDL_Prorefcde = (DropDownList)GVItms.Rows[i].Cells[1].FindControl("DDL_Prorefcde");

                        if (DDL_Prorefcde.SelectedValue == "")
                        {
                            DDL_Prorefcde.DataSource = dtpro;
                            DDL_Prorefcde.DataTextField = "ProductName";
                            DDL_Prorefcde.DataValueField = "ProductID";
                            DDL_Prorefcde.DataBind();
                            DDL_Prorefcde.Items.Insert(0, new ListItem("--Select--", ""));
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       

        public void getProCod()
         
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
                //lbl_err.Text = ex.Message.ToString();
            }
        }

        protected void DDL_Prorefcde_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox TBItmQty = null;
                GridView GVStkItems = null;
                Label lbl_Details= null;
                float GTotal = 0;

                for (int j = 0; j < GVItms.Rows.Count; j++)
                {
                    DropDownList DDL_Prorefcde = (DropDownList)GVItms.Rows[j].FindControl("DDL_Prorefcde");
                    DropDownList DDL_invtyp = (DropDownList)GVItms.Rows[j].FindControl("DDL_invtyp");
                    TBItmQty = (TextBox)GVItms.Rows[j].FindControl("TBItmQty");
                    GVStkItems = (GridView)GVItms.Rows[j].Cells[2].FindControl("GVStkItems");
                    lbl_Details = (Label)GVItms.Rows[j].Cells[2].FindControl("lbl_Details");

                    if (DDL_invtyp.SelectedValue == "NORM")
                    {
                        query = " select Dstk_unt as [SIZE],Dstk_purrat,Dstk_Qty as [QTY] from tbl_Mstk  " +
                            " inner join tbl_Dstk on tbl_Mstk.Mstk_id = tbl_Dstk.Mstk_id " +
                            " where ProductID = '" + DDL_Prorefcde.SelectedValue.Trim() + "' and tbl_Mstk.CompanyId='" + Session["CompanyID"] + "' and tbl_Mstk.BranchId='" + Session["BranchID"] + "'";
                    }
                    else if (DDL_invtyp.SelectedValue == "DEFEC")
                    {
                        query = " select RDstk_unt as [SIZE],RDstk_purrat,RDstk_Qty as [QTY],ProductID from tbl_RMstk  " +
                            " inner join tbl_RDstk on tbl_RMstk.RMstk_id = tbl_RDstk.RMstk_id " +
                            " where ProductID = '" + DDL_Prorefcde.SelectedValue.Trim() + "' tbl_RMstk.CompanyId='" + Session["CompanyID"] + "' and tbl_RMstk.BranchId='" + Session["BranchID"] + "'";
                    }

                    SqlCommand cmd = new SqlCommand(query, con);
                    DataTable dt_ = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);

                    adp.Fill(dt_);

                    if (dt_.Rows.Count > 0)
                    {
                        GVStkItems.DataSource = dt_;
                        GVStkItems.DataBind();

                    }
                }
                    lbl_Details.Text = "";
    
                    TextBox total = null;

                    for (int k = 0; k < GVStkItems.Rows.Count; k++)
                    {
                        total = (TextBox)GVStkItems.Rows[k].FindControl("ItmQty");
                        GTotal += Convert.ToSingle(total.Text);
                    }

                    
                    TBItmQty.Text = GTotal.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
                //lbl_err.Text = ex.Message.ToString(); 
            }
        }

        protected void GVItms_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //BindPar();
                getPro();

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    GridView GVStkItems = (GridView)e.Row.FindControl("GVStkItems");

                    DataTable subdt = (DataTable)ViewState["dt_adItms"];
                    //DataRow subdr = null;

                    //subdt.Columns.Add(new DataColumn("SIZE", typeof(string)));
                    //subdt.Columns.Add(new DataColumn("QTY", typeof(string)));

                    //subdr = subdt.NewRow();
                    //subdr["SIZE"] = "0.00";
                    //subdr["QTY"] = "0.00";

                    //subdt.Rows.Add(subdr);

                    //Store the DataTable in ViewState

                    //ViewState["dt_adItms"] = subdt;

                    GVStkItems.DataSource = subdt;
                    GVStkItems.DataBind();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            //getPro();
        }

        protected void GVItms_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DropDownList ddl_par = (DropDownList)GVItms.FindControl("DDL_Par");
         
            if (e.CommandName == "Add")
            {
                int i = ddl_par.SelectedIndex;
            }
        }

        protected void TBoutstan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string recovery;

                recovery = (Convert.ToDouble(TBGTtl.Text) - Convert.ToDouble(TBoutstan.Text)).ToString();

                if (TBGTtl.Text == "")
                {
                    TBGTtl.Text = "0.00";
                }
                else
                {
                    TBGTtl.Text = recovery;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void TBRecov_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string recov;

                recov = (Convert.ToDouble(lbl_outstan.Text.Trim()) - Convert.ToDouble(TBRecov.Text.Trim())).ToString();

                if (lbl_outstan.Text == "")
                {
                    TBoutstan.Text = "0.00";
                }
                else
                {
                    lbl_outstan.Text = recov;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void FillGrid(string custname, string dsrdat)
        {
            try
            {
                DataTable dtGetdsr_ = new DataTable();

                query = " select Customers_.CustomerID from Customers_ " +
                    " inner join tbl_Mdsr on Customers_.CustomerID = tbl_Mdsr.CustomerID " +
                    " where CustomerName = '" + tb_cust.Text.Trim() + "' and dsrdat='" + TBDSRDat.Text.Trim() + "' and tbl_Mdsr.CompanyId='" + Session["CompanyID"] + "' and tbl_Mdsr.BranchId='" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);
                if (dt_.Rows.Count > 0)
                {
                    custid = dt_.Rows[0]["CustomerID"].ToString();
                }

                query = " select distinct(CustomerID), tbl_mdsr.dsrid as [Voucher], dsrdat as [Date], " +
                    "  CustomerID from tbl_mdsr inner join tbl_Ddsr on tbl_mdsr.dsrid =  tbl_ddsr.dsrid  " +
                    "  where CustomerID = '" + custid + "'  and dsrdat='" + TBDSRDat.Text.Trim() + "' and tbl_Mdsr.CompanyId='" + Session["CompanyID"] + "' and tbl_Mdsr.BranchId='" + Session["BranchID"] + "'";

                dtGetdsr_ = DBConnection.GetQueryData(query);

                if (dtGetdsr_.Rows.Count > 0)
                {
                    HFDSRID.Value = dtGetdsr_.Rows[0]["Voucher"].ToString();
                }
                GV_DSR.DataSource = dtGetdsr_;
                GV_DSR.DataBind();

            }
            catch (Exception ex)
            {
                lbl_err.Text = ex.Message;
            }
        }


        protected void btn_searchdsr_Click(object sender, EventArgs e)
        {
            FillGrid(tb_cust.Text.Trim(), TBDSRDat.Text.Trim());

            ModalPopupExtender1.Show();
        }

        protected void TBDIS_TextChanged(object sender, EventArgs e)
        {

            try
            {
                for (int j = 0; j < GVItms.Rows.Count; j++)
                {
                    TextBox TBDISC = (TextBox)GVItms.Rows[j].FindControl("TBDISC");
                    TextBox TBrat = (TextBox)GVItms.Rows[j].FindControl("TBrat");
                    TextBox TBamt = (TextBox)GVItms.Rows[j].FindControl("TBamt");
                    TextBox TBItmQty = (TextBox)GVItms.Rows[j].FindControl("TBItmQty");

                    if (TBDISC.Text != "")
                    {
                        double per, percent, salerat;
                        per = (Convert.ToDouble(TBDISC.Text.Trim()) / 100) * Convert.ToDouble(TBrat.Text.Trim());
                        salerat = Convert.ToDouble(TBrat.Text.Trim()) - per;
                        TBamt.Text = (salerat * Convert.ToDouble(TBItmQty.Text.Trim())).ToString();
                    }
                    else
                    {
                        TBDISC.Text = "0.00";
                        TBamt.Text = (Convert.ToDouble(TBrat.Text.Trim()) * Convert.ToDouble(TBItmQty.Text.Trim())).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void GVItms_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void DDL_CustAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                query = " select distinct(CustomerName),SubHeadCategoriesGeneratedID,  CustomerID from Customers_  " +
                    " inner join SubHeadCategories on Customers_.CustomerName = SubHeadCategories.SubHeadCategoriesName   " +
                    " where SubHeadGeneratedID= '0011' and " +
                    " SubHeadCategoriesGeneratedID = '" + DDL_CustAcc.SelectedValue.Trim() + "' and Customers_.CompanyId='" + Session["CompanyID"] + "' and Customers_.BranchId='" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    DDL_Cust.SelectedValue = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                }

            }
            catch (Exception ex)
            {
                lbl_err.Text = ex.Message;
            }

        }

        protected void TBDISC_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TBDISC.Text != "0.00")
                {
                    string disc = ((Convert.ToDecimal(TBGTtl.Text)) * (Convert.ToDecimal(TBDISC.Text) / 100)).ToString();

                    TBDISAMT.Text = disc;

                    TBTtl.Text = (Convert.ToDecimal(TBGTtl.Text) - Convert.ToDecimal(disc)).ToString();                  
                }
            }
            catch (Exception ex)
            {
                lbl_err.Text = ex.Message;
            }
        }

        protected void ItmQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = 0;
                float GTotal = 0;
                
                //   GridView GVStkItems = (GridView)GVItms.Rows[rowIndex].Cells[2].FindControl("GVStkItems");
                //   TextBox TBItmQty = (TextBox)GVItms.Rows[rowIndex].Cells[4].FindControl("TBItmQty");
                   
                //     for (int k = 0; k < GVStkItems.Rows.Count; k++)
                //        {
                //            TextBox total = (TextBox)GVStkItems.Rows[k].FindControl("ItmQty");

                //            if (total.Text != "")
                //            {
                //                GTotal += Convert.ToSingle(total.Text);
                //            }
                //        }
                //TBItmQty.Text = GTotal.ToString();
                GridView GVStkItems = null;
                TextBox TBItmQty = null;

                for (int j = 0; j < GVItms.Rows.Count; j++)
                {
                     GVStkItems = (GridView)GVItms.Rows[j].FindControl("GVStkItems");
                     TBItmQty = (TextBox)GVItms.Rows[j].FindControl("TBItmQty");
                }

                for (int k = 0; k < GVStkItems.Rows.Count; k++)
                {
                    TextBox total = (TextBox)GVStkItems.Rows[k].FindControl("ItmQty");

                    if (total.Text != "")
                    {
                        GTotal += Convert.ToSingle(total.Text);
                    }
                }

                TBItmQty.Text = GTotal.ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void TBrat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < GVItms.Rows.Count; j++)
                {
                    TextBox TBItmQty = (TextBox)GVItms.Rows[j].FindControl("TBItmQty");
                    Label lbshw = (Label)GVItms.Rows[j].FindControl("lbshw");
                    TextBox Tbrat = (TextBox)GVItms.Rows[j].FindControl("Tbrat");
                    TextBox Tbamt = (TextBox)GVItms.Rows[j].FindControl("Tbamt");
                    DropDownList DDL_Prorefcde = (DropDownList)GVItms.Rows[j].FindControl("DDL_Prorefcde");
                    Label Isupdat = (Label)GVItms.Rows[j].FindControl("Isupdat");
                    LinkButton lnkbtnadd = (LinkButton)GVItms.Rows[j].FindControl("lnkbtnadd");

                    /*DataTable stkqty = new DataTable();

                    SqlCommand commnd = new SqlCommand();
                    commnd.Connection = con;
                    commnd.CommandText = "select Dstk_Qty from tbl_Dstk where ProductID = " + DDL_Prorefcde.SelectedValue.Trim() + "";
                    con.Open();
                    SqlDataAdapter Adapter = new SqlDataAdapter(commnd);
                    Adapter.Fill(stkqty);

                    if (stkqty.Rows.Count > 0)
                    {
                        for (int t = 0; t < stkqty.Rows.Count; t++)
                        {
                            int stkquanty = Convert.ToInt32(stkqty.Rows[t]["Dstk_Qty"]);
                            int qty = Convert.ToInt32(TBItmQty.Text);
                            if (qty > stkquanty)
                            {
                                lbshw.Text = "Item Qunatity is less in Store!!";
                                lbshw.ForeColor = System.Drawing.Color.Red;
                                Tbrat.Enabled = false;
                                Tbamt.Enabled = false;
                                Tbamt.Text = "0.00";
                                lnkbtnadd.Enabled = false;
                                btnSave.Enabled = false;

                            }
                            else
                            {*/
                                lbshw.Text = "";
                                Tbrat.Enabled = true;
                                Tbamt.Enabled = true;
                                lnkbtnadd.Enabled = true;
                                btnSave.Enabled = true;

                                decimal result = (Convert.ToDecimal(TBItmQty.Text.Trim()) * Convert.ToDecimal(Tbrat.Text));
                                Tbamt.Text = result.ToString();


                                float GTotal = 0;
                                for (int k = 0; k < GVItms.Rows.Count; k++)
                                {
                                    TextBox total = (TextBox)GVItms.Rows[k].FindControl("TBAmt");
                                    GTotal += Convert.ToSingle(total.Text);
                                }

                                TBGTtl.Text = GTotal.ToString();
                                TBTtl.Text = GTotal.ToString();

                                Isupdat.Text = "1";
                            /*}
                        }
                    }
                    con.Close();*/
                }
                if (TBDISC.Text != "0.00")
                {
                    decimal discount = Convert.ToDecimal(TBGTtl.Text) * Convert.ToDecimal(TBDISC.Text.Trim()) / 100;
                    TBTtl.Text = (Convert.ToDecimal(TBGTtl.Text) - discount).ToString();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                lbl_err.Text = ex.Message;
            }
        }

        protected void DDL_invtyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try 
            {

                for (int j = 0; j < GVItms.Rows.Count; j++)
                {
                    DropDownList DDL_invtyp = (DropDownList)GVItms.Rows[j].FindControl("DDL_invtyp");
                    DropDownList DDL_Prorefcde = (DropDownList)GVItms.Rows[j].FindControl("DDL_Prorefcde");

                    if(DDL_invtyp.SelectedValue == "NORM")
                    {
                        query = " select distinct(ProductID) as [ProductID], Dstk_ItmDes as [ProductName] from tbl_Dstk where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                    }
                    else if (DDL_invtyp.SelectedValue == "DEFEC")
                    {
                        query = " select distinct(ProductID) as [ProductID], RDstk_ItmDes as [ProductName] from tbl_RDstk where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                    }

                    dt_ = new DataTable();

                    dt_ = DBConnection.GetQueryData(query);

                    if (dt_.Rows.Count > 0)
                    {
                        if (DDL_Prorefcde.SelectedValue == "0")
                        {
                            DDL_Prorefcde.DataSource = dt_;
                            DDL_Prorefcde.DataTextField = "ProductName";
                            DDL_Prorefcde.DataValueField = "ProductID";
                            DDL_Prorefcde.DataBind();
                            DDL_Prorefcde.Items.Insert(0, new ListItem("--Select Products--", "0"));
                        }
                    }
                }
                
            }catch(Exception ex)
            {
                lbl_err.Text = ex.Message;
            }
        }

        protected void GVStkItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                lbl_err.Text = ex.Message;
            }
        }

        protected void GVItms_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVItms.EditIndex = e.NewEditIndex;
        }

        protected void GVItms_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                //Retrieve the table from the session object.
                //DataTable dt = (DataTable)ViewState["dt_salItms"];

                //Update the values.
                //GridViewRow row = GVItms.Rows[e.RowIndex];
                //dt.Rows[row.DataItemIndex]["PRODUCT"] = ((Label)(row.Cells[0].Controls[0])).Text;
                //dt.Rows[row.DataItemIndex]["SIZE"] = ((TextBox)(row.Cells[1].Controls[0])).Text;
                //dt.Rows[row.DataItemIndex]["RATE"] = ((Label)(row.Cells[2].Controls[0])).Text;
                //dt.Rows[row.DataItemIndex]["QTY"] = ((TextBox)(row.Cells[3].Controls[0])).Text;
                //dt.Rows[row.DataItemIndex]["AMT"] = ((TextBox)(row.Cells[4].Controls[0])).Text;

                //Reset the edit index.
                GVItms.EditIndex = -1;

                //Bind data to the GridView control.

                //GVItms.EditIndex = -1;

                ////DataTable dt = (DataTable)ViewState["dt_salItms"];
                //DataTable TempTable = new DataTable();

                //TempTable.Columns.Add("PRODUCT", typeof(String));
                //TempTable.Columns.Add("SIZE", typeof(String));
                //TempTable.Columns.Add("RATE", typeof(String));
                //TempTable.Columns.Add("QTY", typeof(String));
                //TempTable.Columns.Add("AMT", typeof(String));  
                
                
                //if (GVItms.Rows.Count != 0)
                //{
                //    //Forloop for header
                //    //for (int i = 0; i < GVItms.HeaderRow.Cells.Count; i++)
                //    //{
                //    //    TempTable.Columns.Add(GVItms.HeaderRow.Cells[i].Text);
                //    //}

                //    //TempTable = dt.Clone();

                //    //foreach (GridViewRow row in GVItms.Rows)
                //    //{
                //    //    DataRow TempRow = TempTable.NewRow();

                //    //    for (int i = 0; i < row.Cells.Count; i++)
                //    //    {
                //    //        //if (row.Cells[i].Controls[0].GetType().Equals(typeof(DataBoundLiteralControl)))
                //    //        //{
                //    //        //    TempRow[i] = ((DataBoundLiteralControl)row.Cells[i].Controls[0] as DataBoundLiteralControl).Text;
                //    //        //}
                //    //        //else

                //    //        if (row.Cells[i].Controls[0].GetType().Equals(typeof(TextBox)))
                //    //        {
                //    //            TempRow[i] = ((TextBox)row.Cells[i].Controls[0]).Text;
                //    //        }
                //    //    }
                //    //    TempTable.Rows.Add(TempRow);
                //    //}
                //}
                ////DataTable dt = (DataTable)ViewState["dt_salItms"];
                ////DataTable dt = new  DataTable();

                ////if (GVItms.Rows.Count != 0)
                ////{
                ////    //Forloop for header
                ////    for (int i = 0; i < GVItms.HeaderRow.Cells.Count; i++)
                ////    {
                ////        dt.Columns.Add(GVItms.HeaderRow.Cells[i].Text);
                ////    }

                //    //foreach for datarow

                //    foreach (GridViewRow row in GVItms.Rows)
                //    {
                //        DataRow dr = dt.NewRow();
                //        for (int j = 0; j < row.Cells.Count; j++)
                //        {
                //            dr[GVItms.HeaderRow.Cells[j].Text] = row.Cells[j].FindControl("").Text;
                //        }
                //        dt.Rows.Add(dr);
                //    }

                //    //Loop for footer
                //    if (GVItms.FooterRow.Cells.Count != 0)
                //    {
                //        DataRow dr = dt.NewRow();
                //        for (int i = 0; i < GVItms.FooterRow.Cells.Count; i++)
                //        {
                //            //You have to re-do the work if you did anything in databound for footer.  
                //        }
                //        dt.Rows.Add(dr);
                //    }
                //    //dt.TableName = "tb";
                //}

                //DataTable dt = new DataTable();
                //for (int i = 0; i < GVItms.Columns.Count; i++)
                //{
                //    dt.Columns.Add("column" + i.ToString());
                //}
                //foreach (GridViewRow row in GVItms.Rows)
                //{
                //    DataRow dr = dt.NewRow();
                //    for (int j = 0; j < GVItms.Columns.Count; j++)
                //    {
                //        dr["column" + j.ToString()] = row.Cells[j].Text;
                //    }
                
                //    dt.Rows.Add(dr);
                //}

                //ViewState["dt_salItms"] = TempTable;

                //GVItms.DataSource = TempTable;
                //GVItms.DataBind();

            }
            catch (Exception ex)
            {
                lbl_err.Text = ex.Message;
            }
        }

        protected void GVItms_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVItms.EditIndex = -1;
        }

        protected void GVItms_SelectedIndexChanged1(object sender, EventArgs e)
        {

        }

        protected void ItmQty_TextChanged1(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < GVItms.Rows.Count; j++)
                {
                    TextBox lblrat = (TextBox)GVItms.Rows[j].FindControl("lblrat");
                    TextBox ItmQty = (TextBox)GVItms.Rows[j].FindControl("ItmQty");
                    TextBox TBamt = (TextBox)GVItms.Rows[j].FindControl("TBamt");

                    TBamt.Text = (Convert.ToDecimal(lblrat.Text) * Convert.ToDecimal(ItmQty.Text)).ToString();
                }

                float GTotal = 0;
                for (int k = 0; k < GVItms.Rows.Count; k++)
                {
                    TextBox total = (TextBox)GVItms.Rows[k].FindControl("TBamt");
                    GTotal += Convert.ToSingle(total.Text);
                }


                TBGTtl.Text = GTotal.ToString();
                TBTtl.Text = GTotal.ToString();
            }
            catch (Exception ex)
            {
                lbl_err.Text = ex.Message;
            }
        }

        protected void lblrat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < GVItms.Rows.Count; j++)
                {
                    TextBox lblrat = (TextBox)GVItms.Rows[j].FindControl("lblrat");
                    TextBox ItmQty = (TextBox)GVItms.Rows[j].FindControl("ItmQty");
                    TextBox TBamt = (TextBox)GVItms.Rows[j].FindControl("TBamt");

                    TBamt.Text = (Convert.ToDecimal(lblrat.Text) * Convert.ToDecimal(ItmQty.Text)).ToString();
                }

                float GTotal = 0;
                for (int k = 0; k < GVItms.Rows.Count; k++)
                {
                    TextBox total = (TextBox)GVItms.Rows[k].FindControl("TBamt");
                    GTotal += Convert.ToSingle(total.Text);
                }

                TBGTtl.Text = GTotal.ToString();
                TBTtl.Text = GTotal.ToString();
            }
            catch (Exception ex)
            {
                lbl_err.Text = ex.Message;
            }
        }

        protected void TB_Rat_TextChanged(object sender, EventArgs e)
        { 
            v_rate.Text = "";

            TextBox itq = (TextBox)GVStkItems.Rows[0].FindControl("ItReqQty");

            itq.Focus();
            itq.Attributes.Add("onfocusin", "select();");
        }
    }
}