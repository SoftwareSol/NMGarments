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

using Foods;
using DataAccess;

namespace Foods
{
    public partial class Rpt_PurchaseForm : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        string compid, branchid, compnam, imglogo_, add, no;
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDll();
                Data();
                pnl_wh.Visible = false;
                pnl_payroll.Visible = false;
                pnl_Quot.Visible = false;
                pnl_Req.Visible = false;
                pnl_local.Visible = false;
                pnl_pur.Visible = false;
                pnlSal.Visible = false;
                Pnlpro.Visible = false;
                LoadSheet.Visible = false;
                pnlstk.Visible = false;
                PnlEmp.Visible = false;
                pnl_prof.Visible = false;
                pnl_Expence.Visible = false;
                pnl_rej.Visible = false;
                pnl_Transc.Visible = false;
                pnl_acc.Visible = false;

                TBStckDat.Text = DateTime.Now.ToString("yyyy/MM/dd");
                TBFDWise.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TBFDatWisSal.Text = DateTime.Now.ToString("MM/dd/yyyy");
                TBEFdat.Text = DateTime.Now.ToString("MM/dd/yyyy");
                TBFrmDat.Text = DateTime.Now.ToString("MM/dd/yyyy");
                TBSExp.Text = DateTime.Now.ToString("dd/MM/yyyy");
                TBFExp.Text = DateTime.Now.ToString("dd/MM/yyyy");
                ddl_rpttyp.Visible = false;

               
            }
        }

        #region Web Services
        //[System.Web.Script.Services.ScriptMethod()]
        //[System.Web.Services.WebMethod]
        //public static List<string> GetSearch(string prefixText)
        //{

        //    SqlConnection con = DataAccess.DBConnection.connection();
        //    SqlDataAdapter da;
        //    DataTable dt;
        //    DataTable Result = new DataTable();
        //    string str = " select ProductName from tbl_Mstk inner join tbl_Dstk on  tbl_Mstk.Mstk_id = tbl_Dstk.Mstk_id inner join Products on tbl_Dstk.ProductID = Products.ProductID  where  ProductName like '" + prefixText + "%'";
        //    da = new SqlDataAdapter(str, con);
        //    dt = new DataTable();
        //    da.Fill(dt);
        //    List<string> Output = new List<string>();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //        Output.Add(dt.Rows[i][0].ToString());
        //    return Output;
        //} 

        #endregion
        public void Data()
        {
            DataTable dt_ = new DataTable();

            dt_.Columns.AddRange(new DataColumn[] { new DataColumn("Name"), new DataColumn("ID") });
            dt_.Clear();


            dt_.Rows.Add("Inventory", "STK");
            //dt_.Rows.Add("Inventory Warehouse Wise", "StkWH");
            //dt_.Rows.Add("Quotations", "QuotaionReport");
            //dt_.Rows.Add("Requisitions", "Req");
            //dt_.Rows.Add("Purchase Order", "PO");
            dt_.Rows.Add("Employee", "EMP");
            dt_.Rows.Add("Purchase", "PR");
            //dt_.Rows.Add("Purchase Company Wise", "PRCOM");
            //dt_.Rows.Add("GRN", "GRN");
            //dt_.Rows.Add("Credit Note", "CN");
            //dt_.Rows.Add("Sales Order", "SO");
            dt_.Rows.Add("Sales", "Sal");
            dt_.Rows.Add("Credit Sheet", "CS");            
            dt_.Rows.Add("Load Sheet", "LoadSheet");
            dt_.Rows.Add("Daily Sales Report", "DSR");
            //dt_.Rows.Add("Debit Note", "DN");
            dt_.Rows.Add("Employee", "EMP");
            dt_.Rows.Add("Profit", "PROF");
            dt_.Rows.Add("Expence", "EXP");
            //dt_.Rows.Add("Pay Roll", "PayRoll");
            dt_.Rows.Add("Accounts", "ACC");
            dt_.Rows.Add("Transactions", "TRANS"); 

            ddl_rpttyp.DataSource = dt_;
            ddl_rpttyp.DataTextField = "Name";
            ddl_rpttyp.DataValueField = "ID";
            ddl_rpttyp.DataBind();
            //ddl_rpttyp.Items.Insert(0, new ListItem("--Select Report Type--", "0"));
        }

        public void BindDll()
        {
            try
            {

                //For Bank

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select CashBnk_id,CashBnk_nam from tbl_CashBnk where CashBnk_id <> '1'";
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtbank = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtbank);

                    DDL_bnk.DataSource = dtbank;
                    DDL_bnk.DataTextField = "CashBnk_nam";
                    DDL_bnk.DataValueField = "CashBnk_id";
                    DDL_bnk.DataBind();
                    DDL_bnk.Items.Insert(0, new ListItem("--Select Bank--", "0"));

                    con.Close();
                }

                //For Ware House
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select rtrim('[' + CAST(wh_id AS VARCHAR(200)) + '] - ' + wh_nam) as [whnam], wh_id from tbl_wh";
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtwh = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtwh);

                    ddl_wh.DataSource = dtwh;
                    ddl_wh.DataTextField = "whnam";
                    ddl_wh.DataValueField = "wh_id";
                    ddl_wh.DataBind();
                    ddl_wh.Items.Insert(0, new ListItem("--Select Ware House--", "0"));

                    con.Close();
                }

                // For Sizes
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct(Dstk_unt) from tbl_dstk";
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtwh = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtwh);

                    DDL_Itemsiz.DataSource = dtwh;
                    DDL_Itemsiz.DataTextField = "Dstk_unt";
                    DDL_Itemsiz.DataValueField = "Dstk_unt";
                    DDL_Itemsiz.DataBind();
                    DDL_Itemsiz.Items.Insert(0, new ListItem("--Select Sizes--", "0"));

                    con.Close();
                }


                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct(SubHeadCategoriesID), SubHeadCategoriesGeneratedID,  SubHeadCategoriesName from subheadcategories where SubHeadGeneratedID IN ('0024','0021', '0023','0011')";
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtex = new DataTable();
                    SqlDataAdapter adpex = new SqlDataAdapter(cmd);
                    adpex.Fill(dtex);

                    ddlexpence.DataSource = dtex;
                    ddlexpence.DataTextField = "SubHeadCategoriesName";
                    ddlexpence.DataValueField = "SubHeadCategoriesGeneratedID";
                    ddlexpence.DataBind();
                    ddlexpence.Items.Insert(0, new ListItem("--Select Expences--", "0"));

                    DDL_LedgerAcc.DataSource = dtex;
                    DDL_LedgerAcc.DataTextField = "SubHeadCategoriesName";
                    DDL_LedgerAcc.DataValueField = "SubHeadCategoriesGeneratedID";
                    DDL_LedgerAcc.DataBind();
                    DDL_LedgerAcc.Items.Insert(0, new ListItem("--Select Expences--", "0"));


                    DDL_BnkExp.DataSource = dtex;
                    DDL_BnkExp.DataTextField = "SubHeadCategoriesName";
                    DDL_BnkExp.DataValueField = "SubHeadCategoriesGeneratedID";
                    DDL_BnkExp.DataBind();
                    DDL_BnkExp.Items.Insert(0, new ListItem("--Select Expences--", "0"));

                    DDL_CashExp.DataSource = dtex;
                    DDL_CashExp.DataTextField = "SubHeadCategoriesName";
                    DDL_CashExp.DataValueField = "SubHeadCategoriesGeneratedID";
                    DDL_CashExp.DataBind();
                    DDL_CashExp.Items.Insert(0, new ListItem("--Select Expences--", "0"));


                    con.Close();
                }
                //Employees
                string query = "select distinct(SubHeadCategoriesID), SubHeadCategoriesGeneratedID,  SubHeadCategoriesName from subheadcategories where SubHeadGeneratedID = '0023'";
                DataTable dtemp = new DataTable();
                dtemp = DataAccess.DBConnection.GetDataTable(query);

                if (dtemp.Rows.Count > 0)
                {
                    DDL_CashEmp.DataSource = dtemp;
                    DDL_CashEmp.DataTextField = "SubHeadCategoriesName";
                    DDL_CashEmp.DataValueField = "SubHeadCategoriesGeneratedID";
                    DDL_CashEmp.DataBind();
                    DDL_CashEmp.Items.Insert(0, new ListItem("--Select Employee--", "0"));

                    DDL_Bnklabour.DataSource = dtemp;
                    DDL_Bnklabour.DataTextField = "SubHeadCategoriesName";
                    DDL_Bnklabour.DataValueField = "SubHeadCategoriesGeneratedID";
                    DDL_Bnklabour.DataBind();
                    DDL_Bnklabour.Items.Insert(0, new ListItem("--Select Employee--", "0"));

                }

                //Users
                string usrqy = "select  Username,CompanyId,BranchId  from Users where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                DataTable dtusr = new DataTable();
                dtusr = DataAccess.DBConnection.GetDataTable(usrqy);

                if (dtusr.Rows.Count > 0)
                {
                    DDLLS.DataSource = dtusr;
                    DDLLS.DataTextField = "Username";
                    DDLLS.DataValueField = "Username";
                    DDLLS.DataBind();
                    DDLLS.Items.Insert(0, new ListItem("--Select User--", "0"));

                    DDLEmp.DataSource = dtusr;
                    DDLEmp.DataTextField = "Username";
                    DDLEmp.DataValueField = "Username";
                    DDLEmp.DataBind();
                    DDLEmp.Items.Insert(0, new ListItem("--Select Employee--", "0"));

                    DDLLS.DataSource = dtusr;
                    DDLEmp.DataTextField = "Username";
                    DDLEmp.DataValueField = "Username";
                    DDLEmp.DataBind();
                    DDLEmp.Items.Insert(0, new ListItem("--Select Employee--", "0"));

                    DDLLS.DataSource = dtusr;
                    DDLLS.DataTextField = "Username";
                    DDLLS.DataValueField = "Username";
                    DDLLS.DataBind();
                    DDLLS.Items.Insert(0, new ListItem("--Select Employee--", "0"));

                }

                //Bookers
                string bookqry = "select * from Users where [Level] = 2 and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                DataTable dtbok = new DataTable();
                dtbok = DataAccess.DBConnection.GetDataTable(bookqry);

                if (dtbok.Rows.Count > 0)
                {
                    DDL_BOOk.DataSource = dtbok;
                    DDL_BOOk.DataTextField = "Username";
                    DDL_BOOk.DataValueField = "Username";
                    DDL_BOOk.DataBind();
                    DDL_BOOk.Items.Insert(0, new ListItem("--Select Employee--", "0"));

                }

                //saleman
                string salmnqry = "select * from Users where [Level] = 3 and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                DataTable dtsalmn = new DataTable();
                dtsalmn = DataAccess.DBConnection.GetDataTable(salmnqry);

                if (dtsalmn.Rows.Count > 0)
                {
                    DDL_SalMan.DataSource = dtsalmn;
                    DDL_SalMan.DataTextField = "Username";
                    DDL_SalMan.DataValueField = "Username";
                    DDL_SalMan.DataBind();
                    DDL_SalMan.Items.Insert(0, new ListItem("--Select Employee--", "0"));

                }

                //Customers

                //string query1 = "select CustomerID,CustomerName from Customers_  where IsActive = 1 and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                string query1 = " select SubHeadCategoriesGeneratedID as [CustomerID], SubHeadCategoriesName as [CustomerName] from SubHeadCategories where SubHeadGeneratedID = '0011' ";
                DataTable dtcust = new DataTable();
                dtcust = DataAccess.DBConnection.GetDataTable(query1);

                if (dtcust.Rows.Count > 0)
                {
                    DDLCust.DataSource = dtcust;
                    DDLCust.DataTextField = "CustomerName";
                    DDLCust.DataValueField = "CustomerID";
                    DDLCust.DataBind();
                    DDLCust.Items.Insert(0, new ListItem("--Select Customer--", "0"));

                    DDLCustWis.DataSource = dtcust;
                    DDLCustWis.DataTextField = "CustomerName";
                    DDLCustWis.DataValueField = "CustomerID";
                    DDLCustWis.DataBind();
                    DDLCustWis.Items.Insert(0, new ListItem("--Select Customer--", "0"));

                    DDL_Cust.DataSource = dtcust;
                    DDL_Cust.DataTextField = "CustomerName";
                    DDL_Cust.DataValueField = "CustomerID";
                    DDL_Cust.DataBind();
                    DDL_Cust.Items.Insert(0, new ListItem("--ALL Customer--", "1"));

                    DDL_BnkCust.DataSource = dtcust;
                    DDL_BnkCust.DataTextField = "CustomerName";
                    DDL_BnkCust.DataValueField = "CustomerID";
                    DDL_BnkCust.DataBind();
                    DDL_BnkCust.Items.Insert(0, new ListItem("--ALL Customer--", "0"));

                    DDL_CashCust.DataSource = dtcust;
                    DDL_CashCust.DataTextField = "CustomerName";
                    DDL_CashCust.DataValueField = "CustomerID";
                    DDL_CashCust.DataBind();
                    DDL_CashCust.Items.Insert(0, new ListItem("--ALL Customer--", "0"));

                }

                

                //Quotations
                string queryQuot = "select MProQuot_id,MProQuot_sono from tbl_MProQuot where ISActive = 1";
                DataTable dtQuot = new DataTable();
                dtQuot = DataAccess.DBConnection.GetDataTable(queryQuot);

                if (dtQuot.Rows.Count > 0)
                {
                    DDLQuot.DataSource = dtQuot;
                    DDLQuot.DataTextField = "MProQuot_sono";
                    DDLQuot.DataValueField = "MProQuot_id";
                    DDLQuot.DataBind();
                    DDLQuot.Items.Insert(0, new ListItem("--Select Quotations--", "0"));
                }

                //Requisition
                string queryReq = "select MReq_id,MReq_sono  from tbl_MReq where ISActive = 1";
                DataTable dtReq = new DataTable();
                dtReq = DataAccess.DBConnection.GetDataTable(queryReq);

                if (dtReq.Rows.Count > 0)
                {
                    DDLReq.DataSource = dtReq;
                    DDLReq.DataTextField = "MReq_sono";
                    DDLReq.DataValueField = "MReq_sono";
                    DDLReq.DataBind();
                    DDLReq.Items.Insert(0, new ListItem("--Select Requisition--", "0"));
                }

                //Vendor
                string queryVen = " select SubHeadCategoriesGeneratedID as [supplierId], SubHeadCategoriesName as [suppliername] from SubHeadCategories where SubHeadGeneratedID = '0021' ";

                DataTable dtVen = new DataTable();
                dtVen = DataAccess.DBConnection.GetDataTable(queryVen);

                if (dtVen.Rows.Count > 0)
                {
                    DDL_Ven.DataSource = dtVen;
                    DDL_Ven.DataTextField = "suppliername";
                    DDL_Ven.DataValueField = "supplierId";
                    DDL_Ven.DataBind();
                    DDL_Ven.Items.Insert(0, new ListItem("--Select Vendor--", "0"));

                    DDLVenWis.DataSource = dtVen;
                    DDLVenWis.DataTextField = "suppliername";
                    DDLVenWis.DataValueField = "supplierId";
                    DDLVenWis.DataBind();
                    DDLVenWis.Items.Insert(0, new ListItem("--Select Supplier--", "0"));

                    DDL_BnkSup.DataSource = dtVen;
                    DDL_BnkSup.DataTextField = "suppliername";
                    DDL_BnkSup.DataValueField = "supplierId";
                    DDL_BnkSup.DataBind();
                    DDL_BnkSup.Items.Insert(0, new ListItem("--Select Supplier--", "0"));

                    DDL_CashSup.DataSource = dtVen;
                    DDL_CashSup.DataTextField = "suppliername";
                    DDL_CashSup.DataValueField = "supplierId";
                    DDL_CashSup.DataBind();
                    DDL_CashSup.Items.Insert(0, new ListItem("--Select Supplier--", "0"));

                }

                //ReqEmployeee

                string queryReqEmp = "select employeeID, employeeName from tbl_employee";
                DataTable dtEmp = new DataTable();
                dtEmp = DataAccess.DBConnection.GetDataTable(queryReqEmp);

                if (dtemp.Rows.Count > 0)
                {
                    DDL_emp.DataSource = dtEmp;
                    DDL_emp.DataTextField = "employeeName";
                    DDL_emp.DataValueField = "employeeID";
                    DDL_emp.DataBind();
                    DDL_emp.Items.Insert(0, new ListItem("--Select Employee--", "0"));
                }

                //Department

                string querydepart = "select Depart_id,Depart_nam from tbl_Depart where ISActive = 1 and Depart_id <> 0";
                DataTable dtdepart = new DataTable();
                dtdepart = DataAccess.DBConnection.GetDataTable(querydepart);

                if (dtdepart.Rows.Count > 0)
                {
                    DDL_Dpt.DataSource = dtdepart;
                    DDL_Dpt.DataTextField = "Depart_nam";
                    DDL_Dpt.DataValueField = "Depart_id";
                    DDL_Dpt.DataBind();
                    DDL_Dpt.Items.Insert(0, new ListItem("--Select Department--", "0"));
                }

                //Years

                string queryyr = "select * from tbl_year";
                DataTable dtyr = new DataTable();
                dtyr = DataAccess.DBConnection.GetDataTable(queryyr);

                if (dtyr.Rows.Count > 0)
                {
                    DDLyr.DataSource = dtyr;
                    DDLyr.DataTextField = "yr";
                    DDLyr.DataValueField = "yrid";
                    DDLyr.DataBind();
                    DDLyr.Items.Insert(0, new ListItem("--Select Year--", "0"));

                    DDLYrwis.DataSource = dtyr;
                    DDLYrwis.DataTextField = "yr";
                    DDLYrwis.DataValueField = "yrid";
                    DDLYrwis.DataBind();
                    DDLYrwis.Items.Insert(0, new ListItem("--Select Year--", "0"));

                }

                //Month Wise

                DataTable dt_MW_ = new DataTable();

                dt_MW_.Columns.AddRange(new DataColumn[] { new DataColumn("Name"), new DataColumn("ID") });
                dt_MW_.Clear();


                dt_MW_.Rows.Add("January", "1");
                dt_MW_.Rows.Add("february", "2");
                dt_MW_.Rows.Add("March", "3");
                dt_MW_.Rows.Add("April", "4");
                dt_MW_.Rows.Add("May", "5");
                dt_MW_.Rows.Add("June", "6");
                dt_MW_.Rows.Add("July", "7");
                dt_MW_.Rows.Add("August", "8");
                dt_MW_.Rows.Add("September", "9");
                dt_MW_.Rows.Add("October", "10");
                dt_MW_.Rows.Add("November", "11");
                dt_MW_.Rows.Add("December", "12");


                DDLMnthwis.DataSource = dt_MW_;
                DDLMnthwis.DataTextField = "Name";
                DDLMnthwis.DataValueField = "ID";
                DDLMnthwis.DataBind();
                DDLMnthwis.Items.Insert(0, new ListItem("--Select Month--", "0"));

                DDLMonWisSal.DataSource = dt_MW_;
                DDLMonWisSal.DataTextField = "Name";
                DDLMonWisSal.DataValueField = "ID";
                DDLMonWisSal.DataBind();
                DDLMonWisSal.Items.Insert(0, new ListItem("--Select Month--", "0"));
                
                // Get Purchase No

                //string queryPNO = "select * from get_PurNo() where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                //DataTable dtPO = new DataTable();
                //dtPO = DataAccess.DBConnection.GetDataTable(queryPNO);

                //if (dtPO.Rows.Count > 0)
                //{
                //    DDLPNowis.DataSource = dtPO;
                //    DDLPNowis.DataTextField = "Purchase";
                //    DDLPNowis.DataValueField = "PurNo";
                //    DDLPNowis.DataBind();
                //    DDLPNowis.Items.Insert(0, new ListItem("--Select Purchase No--", "0"));
                //}

                // Get Item Type
                string queryItmtyp = "select * from tbl_producttype where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                DataTable dtItmtyp = new DataTable();
                dtItmtyp = DataAccess.DBConnection.GetDataTable(queryItmtyp);

                if (dtItmtyp.Rows.Count > 0)
                {
                    DDLProtyp.DataSource = dtItmtyp;
                    DDLProtyp.DataTextField = "ProductTypeName";
                    DDLProtyp.DataValueField = "ProductTypeID";
                    DDLProtyp.DataBind();
                    DDLProtyp.Items.Insert(0, new ListItem("--Select Item Type--", "0"));

                    //dll_protyp.DataSource = dtItmtyp;
                    //dll_protyp.DataTextField = "ProductTypeName";
                    //dll_protyp.DataValueField = "ProductTypeID";
                    //dll_protyp.DataBind();
                    //dll_protyp.Items.Insert(0, new ListItem("--Select Item Type--", "0"));
                    
                }

                // Get Products

                string querypro = "select * from Products where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                DataTable dtpro = new DataTable();
                dtpro = DataAccess.DBConnection.GetDataTable(querypro);

                if (dtpro.Rows.Count > 0)
                {
                    //dll_pro.DataSource = dtpro;
                    //dll_pro.DataTextField = "ProductName";
                    //dll_pro.DataValueField = "ProductID";
                    //dll_pro.DataBind();
                    //dll_pro.Items.Insert(0, new ListItem("--Select Item --", "0"));

                    DDLProWis.DataSource = dtpro;
                    DDLProWis.DataTextField = "ProductName";
                    DDLProWis.DataValueField = "ProductID";
                    DDLProWis.DataBind();
                    DDLProWis.Items.Insert(0, new ListItem("--Select Item --", "0"));

                    // Purchase Product Wise

                    DDLPNowis.DataSource = dtpro;
                    DDLPNowis.DataTextField = "ProductName";
                    DDLPNowis.DataValueField = "ProductID";
                    DDLPNowis.DataBind();
                    DDLPNowis.Items.Insert(0, new ListItem("--Select Item --", "0"));

                }
                //dll_pro

                //DDL Inventory Reporting

                string stkItm = "select distinct(tbl_Dstk.ProductID) as [stkProid],ProductName from tbl_Mstk inner join tbl_Dstk on  tbl_Mstk.Mstk_id = tbl_Dstk.Mstk_id inner join Products on tbl_Dstk.ProductID = Products.ProductID ";
               
                DataTable dtstkItm = new DataTable();
                dtItmtyp = DataAccess.DBConnection.GetDataTable(stkItm);

                if (dtItmtyp.Rows.Count > 0)
                {
                    DDL_stkItm.DataSource = dtItmtyp;
                    DDL_stkItm.DataTextField = "ProductName";
                    DDL_stkItm.DataValueField = "stkProid";
                    DDL_stkItm.DataBind();
                    DDL_stkItm.Items.Insert(0, new ListItem("--Select Items--", "0"));


                }

                //Area Wise tbl_area

                string area = "select * from tbl_area  where CompanyId= '" + Session["CompanyID"] + "' and BranchId ='" + Session["BranchID"] + "'";

                DataTable dtarea = new DataTable();
                dtarea = DataAccess.DBConnection.GetDataTable(area);

                if (dtarea.Rows.Count > 0)
                {
                   
                    DDl_Ara.DataSource = dtarea;
                    DDl_Ara.DataTextField = "area_";
                    DDl_Ara.DataValueField = "areaid";
                    DDl_Ara.DataBind();
                    DDl_Ara.Items.Insert(0, new ListItem("--Select Area--", "0"));

                }

                //For Defected Stock

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct(ProductID),Dstk_ItmDes from tbl_Dstk where returntyp='Defected'";
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtdstk = new DataTable();
                    SqlDataAdapter addstk = new SqlDataAdapter(cmd);
                    addstk.Fill(dtdstk);

                    ddl_defected.DataSource = dtdstk;
                    ddl_defected.DataTextField = "Dstk_ItmDes";
                    ddl_defected.DataValueField = "ProductID";
                    ddl_defected.DataBind();
                    ddl_defected.Items.Insert(0, new ListItem("--Select Items--", "0"));

                    con.Close();
                } 

                // For Rejected Stock

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct(ProductID),Dstk_ItmDes from tbl_Dstk where returntyp='Rejected'";
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtrstk = new DataTable();
                    SqlDataAdapter adrstk = new SqlDataAdapter(cmd);
                    adrstk.Fill(dtrstk);

                    DDL_Rej.DataSource = dtrstk;
                    DDL_Rej.DataTextField = "Dstk_ItmDes";
                    DDL_Rej.DataValueField = "ProductID";
                    DDL_Rej.DataBind();
                    DDL_Rej.Items.Insert(0, new ListItem("--Select Items--", "0"));

                    con.Close();
                }

                // For Defected Stock Sizes

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct(Dstk_unt) from tbl_Dstk where returntyp='Defected'";
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtrejitm = new DataTable();
                    SqlDataAdapter adprej = new SqlDataAdapter(cmd);
                    adprej.Fill(dtrejitm);

                    DDL_defectsiz.DataSource = dtrejitm;
                    DDL_defectsiz.DataTextField = "Dstk_unt";
                    DDL_defectsiz.DataValueField = "Dstk_unt";
                    DDL_defectsiz.DataBind();
                    DDL_defectsiz.Items.Insert(0, new ListItem("--Select Sizes--", "0"));

                    con.Close();
                } 
                // For Rejected Stock Sizes

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct(Dstk_unt) from tbl_Dstk where returntyp='Rejected'";
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtrejitm = new DataTable();
                    SqlDataAdapter adprej = new SqlDataAdapter(cmd);
                    adprej.Fill(dtrejitm);

                    DDL_RejItmsiz.DataSource = dtrejitm;
                    DDL_RejItmsiz.DataTextField = "Dstk_unt";
                    DDL_RejItmsiz.DataValueField = "Dstk_unt";
                    DDL_RejItmsiz.DataBind();
                    DDL_RejItmsiz.Items.Insert(0, new ListItem("--Select Sizes--", "0"));

                    con.Close();
                } 
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetBill(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select MSal_sono from tbl_MSal where MSal_sono like '" + prefixText + "%'";
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

            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            //string str = "select ProductName from Products where ProductType ='" + protyp + "' and ProductName like '%" + prefixText + "%'";
            string str = " select distinct(ProductName) from tbl_Dstk  " +
                " inner join Products on  tbl_dstk.ProductID = Products.ProductID where ProductName like '%" + prefixText + "%'";

            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;

        }


        protected void btn_shwrpt_Click(object sender, EventArgs e)
        {
            try
            {

                try
                {
                    string id;
                    id = ddl_rpttyp.SelectedValue.Trim();
                    string WHID = ddl_wh.SelectedValue.Trim();

                    
                    switch (id)
                    {
                        case "SO":
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=SO','_blank','height=600px,width=600px,scrollbars=1');", true);

                            break;
                        case "CN":
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=CN','_blank','height=600px,width=600px,scrollbars=1');", true);

                            break;
                        case "GRN":
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=GRN','_blank','height=600px,width=600px,scrollbars=1');", true);

                            break;
                        case "PO":
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=PO','_blank','height=600px,width=600px,scrollbars=1');", true);

                            break;
                        case "ACC":
                            if (TBFLed.Text != "" && TBTLed.Text != "" && DDL_LedgerAcc.SelectedValue != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_IncomProf.aspx?ID=ACC&FDAT=" + TBFLed.Text.Trim() + "&TDAT=" + TBTLed.Text.Trim() + "&LEDG=" + DDL_LedgerAcc.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (TBFLed.Text != "" && TBTLed.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_IncomProf.aspx?ID=ACC&FDAT=" + TBFLed.Text.Trim() + "&TDAT=" + TBTLed.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }

                            break;
                        case "CS":

                            if (TBFrmDat.Text != "" && TBtDat.Text != "" && DDL_Cust.SelectedValue != "1" && DDl_Ara.SelectedValue != "0" && TBCreBill.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_custcrdt.aspx?ID=CS&FRMDat=" + TBFrmDat.Text.Trim() + "&TDat=" + TBtDat.Text.Trim() + "&CreCust=" + DDL_Cust.SelectedValue.Trim() + "&CreArea=" + DDl_Ara.SelectedValue + "&CreBill=" + TBCreBill.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (TBFrmDat.Text != "" && TBtDat.Text != ""  && DDL_Cust.SelectedValue != "1" && DDl_Ara.SelectedValue != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_custcrdt.aspx?ID=CS&FRMDat=" + TBFrmDat.Text.Trim() + "&TDat=" + TBtDat.Text.Trim() + "&CreCust=" + DDL_Cust.SelectedValue.Trim() + "&CreArea=" + DDl_Ara.SelectedValue + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (TBFrmDat.Text != "" && TBtDat.Text != "" && DDL_Cust.SelectedValue != "1")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_custcrdt.aspx?ID=CS&FRMDat=" + TBFrmDat.Text.Trim() + "&TDat=" + TBtDat.Text.Trim() + "&CreCust=" + DDL_Cust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDl_Ara.SelectedValue != "0" && TBFrmDat.Text != "" && TBtDat.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_custcrdt.aspx?ID=CS&FRMDat=" + TBFrmDat.Text.Trim() + "&TDat=" + TBtDat.Text.Trim() + "&CreArea=" + DDl_Ara.SelectedValue + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDL_Cust.SelectedValue != "1" && TBFrmDat.Text != "" && TBtDat.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_custcrdt.aspx?ID=CS&FRMDat=" + TBFrmDat.Text.Trim() + "&TDat=" + TBtDat.Text.Trim() + "&CreCust=" + DDL_Cust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (TBFrmDat.Text != "" && TBtDat.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_custcrdt.aspx?ID=CS&FRMDat=" + TBFrmDat.Text.Trim() + "&TDat=" + TBtDat.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDl_Ara.SelectedValue != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_custcrdt.aspx?ID=CS&CreArea=" + DDl_Ara.SelectedValue + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (TBCreBill.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_custcrdt.aspx?ID=CS&CreBill=" + TBCreBill.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDL_Cust.SelectedValue != "1")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_custcrdt.aspx?ID=CS&CreCust=" + DDL_Cust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDL_Cust.SelectedValue == "1")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_custcrdt.aspx?ID=CS&CreCust=" + DDL_Cust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                               // ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_custcrdt.aspx?ID=CS','_blank','height=600px,width=600px,scrollbars=1');", true);
                            

                            break;
                        case "StkWH":
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=STK&WHID=" + WHID + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                            break;

                        case "EXP":

                            if (TBSExp.Text != "" && TBFExp.Text != "" && ddlexpence.SelectedValue != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Expence.aspx?ID=EXP&FDAT=" + TBSExp.Text.Trim() + "&TDAT=" + TBFExp.Text.Trim() + "&EXP=" + ddlexpence.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                            }
                            else if (TBSExp.Text != "0" && TBFExp.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Expence.aspx?ID=EXP&FDAT=" + TBSExp.Text.Trim() + "&TDAT=" + TBFExp.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            break;
                            
                        case "Req"://Done

                            //if (DDLQuot.SelectedValue != "0" && TBDate.Text != "" && DDLCust.SelectedValue != "0")
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport&QUOTID=" + DDLQuot.SelectedValue.Trim() + "&CAL=" + TBDate.Text.Trim() + "&CUSID=" + DDLCust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            //}
                            //else if (DDLQuot.SelectedValue != "0" && DDLCust.SelectedValue != "0")
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport&QUOTID=" + DDLQuot.SelectedValue.Trim() + "&CUSID=" + DDLCust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            //}
                            //else if (DDLQuot.SelectedValue != "0" && TBDate.Text != "")
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport&QUOTID=" + DDLQuot.SelectedValue.Trim() + "&CAL=" + TBDate.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            //}
                            //else if (TBDate.Text != "" && DDLCust.SelectedValue != "0")
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport&CAL=" + TBDate.Text.Trim() + "&CUSID=" + DDLCust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            //}
                            //else if (DDLQuot.SelectedValue != "0")
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport&QUOTID=" + DDLQuot.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                            //}
                            //
                            if (DDLReq.SelectedValue != "0" && TBReq_dat.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=Req&REQID=" + DDLReq.SelectedValue.Trim() + "&ReqDat=" + TBReq_dat.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (TBReq_dat.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=Req&ReqDat=" + TBReq_dat.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                            }
                            else if (DDLReq.SelectedValue != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=Req&REQID=" + DDLReq.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=Req','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }


                            break;

                        case "QuotaionReport"://Done

                             if (DDLQuot.SelectedValue != "0" && TBDate.Text != "" && DDLCust.SelectedValue != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport&QUOTID=" + DDLQuot.SelectedValue.Trim() + "&CAL=" + TBDate.Text.Trim() + "&CUSID=" + DDLCust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                             else if (DDLQuot.SelectedValue != "0" && DDLCust.SelectedValue != "0")
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport&QUOTID=" + DDLQuot.SelectedValue.Trim() + "&CUSID=" + DDLCust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                             }
                             else if (DDLQuot.SelectedValue != "0" && TBDate.Text != "")
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport&QUOTID=" + DDLQuot.SelectedValue.Trim() + "&CAL=" + TBDate.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                             }
                             else if ( TBDate.Text != "" && DDLCust.SelectedValue != "0")
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport&CAL=" + TBDate.Text.Trim() + "&CUSID=" + DDLCust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                             }
                             else if (DDLQuot.SelectedValue != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport&QUOTID=" + DDLQuot.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                            }else if (TBDate.Text != "")
                            {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport&CAL="+ TBDate.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                            }else if (DDLCust.SelectedValue != "0")
                            {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport&CUSID=" +  DDLCust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                             else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }

                            break;

                        case "PRCOM":

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=PRCOM','_blank','height=600px,width=600px,scrollbars=1');", true);

                            break;
                        case "PR"://Done

                            if (DDLVenWis.SelectedValue.Trim() != "0" && TBFDWise.Text != "" && TBTDWise.Text != "" && DDLMnthwis.SelectedValue.Trim() != "0" && DDLYrwis.SelectedValue.Trim() != "0" && DDLPNowis.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Pur.aspx?ID=PR&FDAT=" + TBFDWise.Text.Trim() + "&LDAT=" + TBTDWise.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDLVenWis.SelectedValue.Trim() != "0" && DDLMnthwis.SelectedValue.Trim() != "0" && DDLYrwis.SelectedValue.Trim() != "0" && DDLPNowis.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_venwise.aspx?ID=PR&VENID=" + DDLVenWis.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDLVenWis.SelectedValue.Trim() != "0" && DDLPNowis.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_venwise.aspx?ID=PR&VENID=" + DDLVenWis.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDLPNowis.SelectedValue.Trim() != "0" && TBFDWise.Text != "" && TBTDWise.Text != "" && DDLMnthwis.SelectedValue.Trim() != "0" && DDLYrwis.SelectedValue.Trim() != "0" && DDLVenWis.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Pur.aspx?ID=PR&FDAT=" + TBFDWise.Text.Trim() + "&LDAT=" + TBTDWise.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDLPNowis.SelectedValue.Trim() != "0" && DDLVenWis.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Pur.aspx?ID=PR&FDAT=" + TBFDWise.Text.Trim() + "&LDAT=" + TBTDWise.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDLVenWis.SelectedValue.Trim() != "0" && TBFDWise.Text != "" && TBTDWise.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_venwise.aspx?ID=PR&VENID=" + DDLVenWis.SelectedValue.Trim() + "&FDAT=" + TBFDWise.Text.Trim() + "&LDAT=" + TBTDWise.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDLVenWis.SelectedValue.Trim() != "0" && DDLMnthwis.SelectedValue.Trim() != "0" && DDLYrwis.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_venwise.aspx?ID=PR&VENID=" + DDLVenWis.SelectedValue.Trim() + "&MON=" + DDLMnthwis.SelectedValue.Trim() + "&YER=" + DDLYrwis.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            
                            //Product Wise Purchase

                            else if (DDLPNowis.SelectedValue.Trim() != "0" && DDLMnthwis.SelectedValue.Trim() != "0" && DDLYrwis.SelectedValue.Trim() != "0" && DDLVenWis.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Pur.aspx?ID=PR&PRO=" + DDLPNowis.SelectedValue.Trim() + "&MON=" + TBFDWise.Text.Trim() + "&YR=" + TBTDWise.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }

                            else if (DDLPNowis.SelectedValue.Trim() != "0" && TBFDWise.Text != "" && TBTDWise.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Pur.aspx?ID=PR&PRO=" + DDLPNowis.SelectedValue.Trim() + "&FDAT=" + TBFDWise.Text.Trim() + "&LDAT=" + TBTDWise.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }

                            else if (DDLPNowis.SelectedValue.Trim() != "0" && DDLYrwis.SelectedValue != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Pur.aspx?ID=PR&PRO=" + DDLPNowis.SelectedValue.Trim() + "&FDAT=" + TBFDWise.Text.Trim() + "&LDAT=" + TBTDWise.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }


                            else if (TBFDWise.Text != "" && TBTDWise.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Pur.aspx?ID=PR&FDAT=" + TBFDWise.Text.Trim() + "&LDAT=" + TBTDWise.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDLMnthwis.SelectedValue.Trim() != "0" && DDLYrwis.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_purmonwis.aspx?ID=SAL&MONID=" + DDLMnthwis.SelectedValue.Trim() + "&YRID=" + DDLYrwis.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDLPNowis.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/frm_dailyPur.aspx?ID=PR&PURID=" + DDLPNowis.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDLVenWis.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_venwise.aspx?ID=PR&VENID=" + DDLVenWis.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/frm_dailyPur.aspx?ID=PR','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            break;

                        case "STK":

                            if (DDLProtyp.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_stk.aspx?ID=STK&Protyp=" + DDLProtyp.SelectedItem.Text.Trim() + "&dat=" + TBStckDat.Text + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDL_stkItm.SelectedValue.Trim() != "0" && DDL_Itemsiz.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_stk.aspx?ID=STK&ProID=" + DDL_stkItm.SelectedValue.Trim() + "&ITMSIZ=" + DDL_Itemsiz.SelectedValue + "&dat=" + TBStckDat.Text + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDL_stkItm.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_stk.aspx?ID=STK&ProID=" + DDL_stkItm.SelectedValue.Trim() + "&dat=" + TBStckDat.Text + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDL_Itemsiz.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_stk.aspx?ID=STK&ITMSIZ=" + DDL_Itemsiz.SelectedValue.Trim() + "&dat=" + TBStckDat.Text + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                                //For Rejected
                            //else if (DDL_Rej.SelectedValue.Trim() != "0" && DDL_RejItmsiz.SelectedValue.Trim() != "0")
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Rstk.aspx?ID=RSTK&dat=" + TBStckDat.Text.Trim() + "&REJSTK=" + DDL_RejItmsiz.SelectedValue.Trim() + "&REJ=" + DDL_Rej.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            //}
                            //else if (DDL_RejItmsiz.SelectedValue.Trim() != "0")
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Rstk.aspx?ID=RSTK&dat=" + TBStckDat.Text.Trim() + "&REJSTK=" + DDL_RejItmsiz.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            //}
                            //else if (DDL_Rej.SelectedValue.Trim() != "0")
                            //{
                            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Rstk.aspx?ID=RSTK&dat=" + TBStckDat.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            //}
                            else if (ddl_defected.SelectedValue.Trim() != "0" && DDL_defectsiz.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Rstk.aspx?ID=RSTK&dat=" + TBStckDat.Text.Trim() + "&REJSTK=" + DDL_defectsiz.SelectedValue.Trim() + "&REJ=" + ddl_defected.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDL_defectsiz.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Rstk.aspx?ID=RSTK&dat=" + TBStckDat.Text.Trim() + "&REJSTK=" + DDL_defectsiz.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (ddl_defected.SelectedValue.Trim() != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Rstk.aspx?ID=RSTK&dat=" + TBStckDat.Text.Trim() + "&REJ=" + ddl_defected.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (TBStckDat.Text.Trim() != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_stk.aspx?ID=STK&dat=" + TBStckDat.Text + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:alert('Please Select Some Criteria!!');", true);
                            }
                            break;
                       case "Sal":

                             if (DDLCustWis.SelectedValue != "0" && TBFDatWisSal.Text != "" && TBTDatWisSal.Text != "" && DDLMonWisSal.SelectedValue != "0" && DDLyr.SelectedValue != "0" && DDLProWis.SelectedValue != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rptsal_custwise.aspx?ID=SAL&CUSTID=" + DDLCustWis.SelectedValue + "&FDAT=" + TBFDatWisSal.Text + "&LDAT=" + TBTDatWisSal.Text + "&MONID=" + DDLMonWisSal.SelectedValue + "&YRID=" + DDLyr.SelectedValue + "&PROID=" + DDLProWis.SelectedValue + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                            }else if (DDLCustWis.SelectedValue != "0" && DDLMonWisSal.SelectedValue != "0" && DDLyr.SelectedValue != "0" && DDLProWis.SelectedValue != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rptsal_custwise.aspx?ID=SAL&CUSTID=" + DDLCustWis.SelectedValue + "&MONID=" + DDLMonWisSal.SelectedValue + "&YRID=" + DDLyr.SelectedValue + "&PROID=" + DDLProWis.SelectedValue + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                             else if (DDLCustWis.SelectedValue != "0"  && DDLProWis.SelectedValue != "0")
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rptsal_custwise.aspx?ID=SAL&CUSTID=" + DDLCustWis.SelectedValue + "&PROID=" + DDLProWis.SelectedValue + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                             }
                             else if (DDLCustWis.SelectedValue != "0" && DDLProWis.SelectedValue != "0")
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rptsal_custwise.aspx?ID=SAL&CUSTID=" + DDLCustWis.SelectedValue + "&PROID=" + DDLProWis.SelectedValue + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                             }
                             else if (DDLCustWis.SelectedValue != "0")
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rptsal_custwise.aspx?ID=SAL&CUSTID=" + DDLCustWis.SelectedValue + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                             }
                             else if (DDLCustWis.SelectedValue != "0" && DDLProWis.SelectedValue != "0")
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rptsal_custwise.aspx?ID=SAL&CUSTID=" + DDLCustWis.SelectedValue + "&PROID=" + DDLProWis.SelectedValue + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                             }                             
                             else if (DDLMonWisSal.SelectedValue != "0" && DDLyr.SelectedValue != "0")
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_monwis.aspx?ID=SAL&MONID=" + DDLMonWisSal.SelectedValue + "&YRID=" + DDLyr.SelectedValue + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                             }
                            else if (TBFDatWisSal.Text != "" && TBTDatWisSal.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_sal.aspx?ID=SAL&FDAT=" + TBFDatWisSal.Text + "&LDAT=" + TBTDatWisSal.Text + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                             else if (DDLCustWis.SelectedValue != "0")
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rptsal_custwise.aspx?ID=SAL&CUSTID=" + DDLCustWis.SelectedValue + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                             }
                             else if (DDLProWis.SelectedValue != "0")
                             {
                                 ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_salesprowise.aspx?ID=SAL&PROID=" + DDLProWis.SelectedValue + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                             }

                            break;

                        case "DSR":
                             //Response.Redirect(".aspx");
                            if (TBFDatWisSal.Text != "" && DDLCustWis.SelectedValue != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'frm_dsr.aspx?ID=DSR&DAT=" + TBFDatWisSal.Text.Trim() + "&Cust=" + DDLCustWis.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                            }else if (TBFDatWisSal.Text != "" && DDLCustWis.SelectedValue != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'frm_dsr.aspx?ID=DSR&DAT=" + TBFDatWisSal.Text.Trim() + "&Cust=" + DDLCustWis.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (TBFDatWisSal.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'frm_dsr.aspx?ID=DSR&DAT=" + TBFDatWisSal.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                            }
                            else if (DDLCustWis.SelectedValue != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'frm_dsr.aspx?ID=DSR&Cust=" + DDLCustWis.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'frm_dsr.aspx?ID=DSR','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            break;
                        case "EMP":

                            if (DDL_BOOk.SelectedValue != "0" && TBEFdat.Text != "" && TBELdat.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Emp.aspx?ID=EMP&BOOkID=" + DDL_BOOk.SelectedItem.Text + "&FDatEmp=" + TBEFdat.Text + "&EDatEmp=" + TBELdat.Text + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDL_SalMan.SelectedValue != "0" && TBEFdat.Text != "" && TBELdat.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Emp.aspx?ID=EMP&SALMNID=" + DDL_SalMan.SelectedItem.Text + "&FDatEmp=" + TBEFdat.Text + "&EDatEmp=" + TBELdat.Text + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDL_BOOk.SelectedValue != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Emp.aspx?ID=EMP&BOOkID=" + DDL_BOOk.SelectedItem.Text + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            else if (DDL_SalMan.SelectedValue != "0")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Emp.aspx?ID=EMP&SALMNID=" + DDL_SalMan.SelectedItem.Text + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                            }
                            break;
                        case "LoadSheet":

                            if (DDLLS.SelectedValue != "" && TBDatLS.Text != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'frm_loadsheet.aspx?ID=LS&EMPID=" + DDLLS.SelectedValue.Trim() + "&CAL=" + TBDatLS.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                            }
                            else if (DDLLS.SelectedValue != "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'frm_loadsheet.aspx?ID=LS&EMPID=" + DDLLS.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'frm_loadsheet.aspx?ID=LS','_blank','height=600px,width=600px,scrollbars=1');", true);                            
                            }
                            
                            
                           break;
                        case "PayRoll":

                           if (DDLEmp.SelectedValue != "" && TBCal.Text != "")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=PayRoll&EMPID=" + DDLEmp.SelectedValue.Trim() + "&CAL=" + TBCal.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                           }
                           else if (DDLEmp.SelectedValue != "")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=PayRoll&EMPID=" + DDLEmp.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                           }
                           else
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=PayRoll','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }

                           break;

                        case "PROF":

                           if (TBPro.Text != "" && TBFProf.Text != "" && TBTProf.Text != "")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_ProfitSheet.aspx?ID=PROF&Pro=" + TBPro.Text.Trim() + "&FrmDat=" + TBFProf.Text.Trim() + "&Todat=" + TBTProf.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                           }else if (TBFProf.Text != "" && TBTProf.Text != "")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_ProfitSheet.aspx?ID=PROF&FrmDat=" + TBFProf.Text.Trim() + "&Todat=" + TBTProf.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                           }
                           else
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_ProfitSheet.aspx?ID=PROF','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }

                           break;
                        case "TRANS":
                           
                        //Cash

                           if (TBCashFDat.Text.Trim() != "" && TBCashTDat.Text.Trim() != "" && DDL_CashCust.SelectedValue != "0" && DDL_CashEmp.SelectedValue != "0" && DDL_CashSup.SelectedValue != "0" && DDL_CashExp.SelectedValue != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_CashTrans.aspx?ID=TRANS&FrmDat=" + TBCashFDat.Text.Trim() + "&Todat=" + TBCashTDat.Text.Trim() + "&CashCust=" + DDL_CashCust.SelectedValue.Trim() + "&CashEmp=" + DDL_CashEmp.SelectedValue + "&CashSup=" + DDL_CashSup.SelectedValue.Trim() + "&CashExp=" + DDL_CashExp.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }
                           else if (TBCashFDat.Text.Trim() != "" && TBCashTDat.Text.Trim() != "" && DDL_CashCust.SelectedValue != "0" && DDL_CashEmp.SelectedValue != "0" && DDL_CashSup.SelectedValue != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_CashTrans.aspx?ID=TRANS&FrmDat=" + TBCashFDat.Text.Trim() + "&Todat=" + TBCashTDat.Text.Trim() + "&CashCust=" + DDL_CashCust.SelectedValue.Trim() + "&CashEmp=" + DDL_CashEmp.SelectedValue + "&CashSup=" + DDL_CashSup.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }
                           else if (TBCashFDat.Text.Trim() != "" && TBCashTDat.Text.Trim() != "" && DDL_CashCust.SelectedValue != "0" && DDL_CashEmp.SelectedValue != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_CashTrans.aspx?ID=TRANS&FrmDat=" + TBCashFDat.Text.Trim() + "&Todat=" + TBCashTDat.Text.Trim() + "&CashCust=" + DDL_CashCust.SelectedValue.Trim() + "&CashEmp=" + DDL_CashEmp.SelectedValue + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }
                           else if (TBCashFDat.Text.Trim() != "" && TBCashTDat.Text.Trim() != "" && DDL_CashCust.SelectedValue != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_CashTrans.aspx?ID=TRANS&FrmDat=" + TBCashFDat.Text.Trim() + "&Todat=" + TBCashTDat.Text.Trim() + "&CashCust=" + DDL_CashCust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }
                           else if (TBCashFDat.Text.Trim() != "" && TBCashTDat.Text.Trim() != "")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_CashTrans.aspx?ID=TRANS&FrmDat=" + TBCashFDat.Text.Trim() + "&Todat=" + TBCashTDat.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }
                           else if (DDL_CashCust.SelectedValue != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_CashTrans.aspx?ID=TRANS&CashCust=" + DDL_CashCust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }
                           else if (DDL_CashEmp.SelectedValue != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_CashTrans.aspx?ID=TRANS&CashEmp=" + DDL_CashEmp.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }
                           else if (DDL_CashSup.SelectedValue != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_CashTrans.aspx?ID=TRANS&CashSup=" + DDL_CashSup.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                           }
                           else if (DDL_CashExp.SelectedValue != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_CashTrans.aspx?ID=TRANS&CashExp=" + DDL_CashExp.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                           }

                           //Bank

                           if (TBCheqdat.Text.Trim() != "" && DDL_bnk.SelectedValue.Trim() != "0" && 
                               DDL_BnkCust.SelectedValue != "0" && DDL_Bnklabour.SelectedValue != "0" && 
                               DDL_BnkSup.SelectedValue != "0" && DDL_BnkExp.SelectedValue != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), 
                                   "onclick", "javascript:window.open( 'Reports/rpt_BnkTrans.aspx?ID=TRANS&CheqDat=" 
                                   + TBCheqdat.Text.Trim() + "&Bnk=" + DDL_bnk.SelectedValue.Trim() + "&CheqCust=" + 
                                   DDL_BnkCust.SelectedValue.Trim() + "&CheqEmp=" + DDL_Bnklabour.SelectedValue + 
                                   "&CheqSup=" + DDL_BnkSup.SelectedValue.Trim() + "&CheqExp=" + 
                                   DDL_BnkExp.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }

                           else  if (TBCheqdat.Text.Trim() != "" && DDL_bnk.SelectedValue.Trim() != "0" && 
                               DDL_BnkCust.SelectedValue != "0" && DDL_Bnklabour.SelectedValue != "0" && 
                               DDL_BnkSup.SelectedValue != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), 
                                   "onclick", "javascript:window.open( 'Reports/rpt_BnkTrans.aspx?ID=TRANS&CheqDat=" 
                                   + TBCheqdat.Text.Trim() + "&Bnk=" + DDL_bnk.SelectedValue.Trim() + "&CheqCust=" + 
                                   DDL_BnkCust.SelectedValue.Trim() + "&CheqEmp=" + DDL_Bnklabour.SelectedValue + 
                                   "&CheqSup=" + DDL_BnkSup.SelectedValue.Trim() +"','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }
                           else if (TBCheqdat.Text.Trim() != "" && DDL_bnk.SelectedValue.Trim() != "0" && 
                               DDL_BnkCust.SelectedValue != "0" && DDL_Bnklabour.SelectedValue != "0" )
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), 
                                   "onclick", "javascript:window.open( 'Reports/rpt_BnkTrans.aspx?ID=TRANS&CheqDat=" 
                                   + TBCheqdat.Text.Trim() + "&Bnk=" + DDL_bnk.SelectedValue.Trim() + "&CheqCust=" + 
                                   DDL_BnkCust.SelectedValue.Trim() + "&CheqEmp=" + DDL_Bnklabour.SelectedValue + 
                                   "','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }
                           else if (TBCheqdat.Text.Trim() != "" && DDL_bnk.SelectedValue.Trim() != "0" && 
                               DDL_BnkCust.SelectedValue != "0" )
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), 
                                   "onclick", "javascript:window.open( 'Reports/rpt_BnkTrans.aspx?ID=TRANS&CheqDat=" 
                                   + TBCheqdat.Text.Trim() + "&Bnk=" + DDL_bnk.SelectedValue.Trim() + "&CheqCust=" + 
                                   DDL_BnkCust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }
                           else if (TBCheqdat.Text.Trim() != "" && DDL_bnk.SelectedValue.Trim() != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), 
                                   "onclick", "javascript:window.open( 'Reports/rpt_BnkTrans.aspx?ID=TRANS&CheqDat=" 
                                   + TBCheqdat.Text.Trim() + "&Bnk=" + DDL_bnk.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }
                           else if (DDL_bnk.SelectedValue.Trim() != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), 
                                   "onclick", "javascript:window.open( 'Reports/rpt_BnkTrans.aspx?ID=TRANS&Bnk=" + DDL_bnk.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }

                           else if (TBCheqdat.Text.Trim() != "")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_BnkTrans.aspx?ID=TRANS&CheqDat=" + TBCheqdat.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }
                           else if (DDL_BnkCust.SelectedValue != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_BnkTrans.aspx?ID=TRANS&CheqCust=" + DDL_BnkCust.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }
                           else if (DDL_Bnklabour.SelectedValue != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_BnkTrans.aspx?ID=TRANS&CheqEmp=" + DDL_Bnklabour.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                           }
                           else if (DDL_BnkSup.SelectedValue != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_BnkTrans.aspx?ID=TRANS&CheqSup=" + DDL_BnkSup.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                           }
                           else if (DDL_BnkExp.SelectedValue != "0")
                           {
                               ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_BnkTrans.aspx?ID=TRANS&CheqExp=" + DDL_BnkExp.SelectedValue.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                           }

                           break;
                        //default: 
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=SAL','_blank','height=600px,width=600px,scrollbars=1');", true);                            
                            
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }

        protected void DDL_BOOk_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDL_SalMan.SelectedValue = "0";
        }
        protected void DDL_SalMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDL_BOOk.SelectedValue = "0";
        }

        protected void ddl_rpttyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Stock Ware House

            //if (ddl_rpttyp.SelectedValue == "StkWH")STK
            if (ddl_rpttyp.SelectedValue == "STK")
            {
                //pnl_wh.Visible = true;
                pnlSal.Visible = false;
                LoadSheet.Visible = false;
                pnlstk.Visible = true;
            }
            else 
            {
                pnl_wh.Visible = false; 
            }

            if (ddl_rpttyp.SelectedValue == "LoadSheet")
            {
                LoadSheet.Visible = true;
                pnlSal.Visible = false;
            }
            else
            {
                pnl_wh.Visible = false;
            }
            
            //Sales

            if (ddl_rpttyp.SelectedValue == "Sal")
            {
                pnlSal.Visible = true;
            }
            else
            {
                pnl_payroll.Visible = false;
            }

            //Pay Roll

            if (ddl_rpttyp.SelectedValue == "PayRoll")
            {
                pnl_payroll.Visible = true;
            }
            else
            {
                pnl_payroll.Visible = false;
            }

            //Quotations

            if (ddl_rpttyp.SelectedValue == "QuotaionReport")
            {
                pnl_Quot.Visible = true;
            }
            else
            {
                pnl_Quot.Visible = false;
            }

            //Requisition

            if (ddl_rpttyp.SelectedValue == "Req")
            {
                pnl_Req.Visible = true;
            }
            else
            {
                pnl_Req.Visible = false;
            }


            if (ddl_rpttyp.SelectedValue == "Req")
            {
                pnl_Req.Visible = true;
            }
            else
            {
                pnl_Req.Visible = false;
            }

            if (ddl_rpttyp.SelectedValue == "PR")
            {
                pnl_pur.Visible = true;
                pnlSal.Visible = false;
            }
            else
            {
                pnl_pur.Visible = false;
            }
            //Sales

            if (ddl_rpttyp.SelectedValue == "DSR")
            {
                pnlSal.Visible = true;
                LoadSheet.Visible = false;
            }
            else
            {
                //pnl_pur.Visible = false;
            }
            if (ddl_rpttyp.SelectedValue == "EMP")
            {
                pnlSal.Visible = true;
                LoadSheet.Visible = false;
            }
            else
            {
                //pnl_pur.Visible = false;
            }

            if (ddl_rpttyp.SelectedValue == "CS")
            {
                btn_shwrpt.Visible = true;
            }
        }

        protected void chk_req_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_req.Checked == true)
            {
                pnl_local.Visible = true;
            }
            else if (chk_req.Checked == false)
            {
                pnl_local.Visible = false;
            }
        }

        protected void lnkbtn_inv_Click(object sender, EventArgs e)
        {
                ddl_rpttyp.SelectedValue = "STK";
                pnlstk.Visible = true;    
                pnl_pur.Visible = false;
                pnlSal.Visible = false;
                ddl_rpttyp.Visible = false;
                Pnlpro.Visible = false;
                PnlEmp.Visible = false;
                pnl_prof.Visible = false;
                pnl_Expence.Visible = false;
                pnl_acc.Visible = false;
                pnl_Transc.Visible = false;

        }

        protected void lnkbtn_sal_Click(object sender, EventArgs e)
        {
            ddl_rpttyp.SelectedValue = "Sal";
            pnlstk.Visible = false;
            pnl_pur.Visible = false;
            pnlSal.Visible = true;
            ddl_rpttyp.Visible = false;
            Pnlpro.Visible = false;
            PnlEmp.Visible = false;
            pnl_Expence.Visible = false;
            pnl_acc.Visible = false;
            pnl_prof.Visible = false;
            pnl_Transc.Visible = false;

        }

        protected void lnkbtn_pur_Click(object sender, EventArgs e)
        {
            ddl_rpttyp.SelectedValue = "PR";
            pnlstk.Visible = false;
            pnl_pur.Visible = true;
            pnlSal.Visible = false;
            ddl_rpttyp.Visible = false;
            Pnlpro.Visible = false;
            PnlEmp.Visible = false;
            pnl_prof.Visible = false;
            pnl_Expence.Visible = false;
            pnl_acc.Visible = false;
            pnl_Transc.Visible = false;

        }

        protected void lnkbtn_pro_Click(object sender, EventArgs e)
        {
            ddl_rpttyp.SelectedValue = "CS";
            Pnlpro.Visible = true;
            pnlstk.Visible = false;
            pnl_pur.Visible = false;
            pnlSal.Visible = false;
            ddl_rpttyp.Visible = false;
            PnlEmp.Visible = false;
            pnl_prof.Visible = false;
            pnl_Expence.Visible = false;
            pnl_acc.Visible = false;
            pnl_Transc.Visible = false;

        }

        protected void lnkbtn_emp_Click(object sender, EventArgs e)
        {
            ddl_rpttyp.SelectedValue = "EMP";
            PnlEmp.Visible = true;
            Pnlpro.Visible = false;
            pnlstk.Visible = false;
            pnl_pur.Visible = false;
            pnlSal.Visible = false;
            ddl_rpttyp.Visible = false;
            pnl_prof.Visible = false;
            pnl_Expence.Visible = false;
            pnl_acc.Visible = false;
            pnl_Transc.Visible = false;
     
        }

        protected void lnkbtn_dsr_Click(object sender, EventArgs e)
        {
            ddl_rpttyp.SelectedValue = "DSR";
            Pnlpro.Visible = false;
            pnlstk.Visible = false;
            pnl_pur.Visible = false;
            pnlSal.Visible = false;
            ddl_rpttyp.Visible = false;
            PnlEmp.Visible = false;
            pnl_prof.Visible = false;
            pnl_Expence.Visible = false;
            pnl_acc.Visible = false;
            pnl_Transc.Visible = false;

        }

        protected void DDLProtyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDL_stkItm.SelectedValue ="0";
            DDL_Itemsiz.SelectedValue = "0";
            DDL_Rej.SelectedValue = "0";
            DDL_RejItmsiz.SelectedValue = "0";
            DDL_defectsiz.SelectedValue = "0";
        }

        protected void DDL_stkItm_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLProtyp.SelectedValue = "0";
            DDL_Rej.SelectedValue = "0";
            DDL_RejItmsiz.SelectedValue = "0";
            DDL_defectsiz.SelectedValue = "0";
            ddl_defected.SelectedValue = "0";
        }

        protected void DDLVenWis_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DDLPNowis.SelectedValue = "0";
            //DDLMnthwis.SelectedValue = "0";
            //DDLYrwis.SelectedValue = "0";
            //TBTDWise.Text = "";
            //TBFDWise.Text = DateTime.Now.ToShortDateString();
        }

        protected void DDL_Rej_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDL_stkItm.SelectedValue = "0";
            DDLProtyp.SelectedValue = "0";
            DDL_Itemsiz.SelectedValue = "0";
            DDL_defectsiz.SelectedValue = "0";
            ddl_defected.SelectedValue = "0";
        }

        protected void ddl_defected_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDL_stkItm.SelectedValue = "0";
            DDLProtyp.SelectedValue = "0";
            DDL_Itemsiz.SelectedValue = "0";
            DDL_defectsiz.SelectedValue = "0";
        }

        protected void DDL_defectsiz_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDL_stkItm.SelectedValue = "0";
            DDLProtyp.SelectedValue = "0";
            DDL_Itemsiz.SelectedValue = "0";
           
        }

        protected void DDL_Itemsiz_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDL_Rej.SelectedValue = "0";
            DDLProtyp.SelectedValue = "0";
            DDL_RejItmsiz.SelectedValue = "0";
            DDL_defectsiz.SelectedValue = "0";
            ddl_defected.SelectedValue = "0";
        }

        protected void lnkprof_Click(object sender, EventArgs e)
        {
            ddl_rpttyp.SelectedValue = "PROF";
            Pnlpro.Visible = false;
            pnlstk.Visible = false;
            pnl_pur.Visible = false;
            pnlSal.Visible = false;
            ddl_rpttyp.Visible = false;
            PnlEmp.Visible = false;
            pnl_prof.Visible = true;
            pnl_Expence.Visible = false;
            pnl_acc.Visible = false;
            pnl_Transc.Visible = false;
        }

        protected void lnkexp_Click(object sender, EventArgs e)
        {
            ddl_rpttyp.SelectedValue = "EXP";
            Pnlpro.Visible = false;
            pnlstk.Visible = false;
            pnl_pur.Visible = false;
            pnlSal.Visible = false;
            ddl_rpttyp.Visible = false;
            PnlEmp.Visible = false;
            pnl_prof.Visible = false;
            pnl_Expence.Visible = true;
            pnl_acc.Visible = false;
            pnl_Transc.Visible = false;
        }

        protected void lnk_acc_Click(object sender, EventArgs e)
        {
            ddl_rpttyp.SelectedValue = "ACC";
            Pnlpro.Visible = false;
            pnlstk.Visible = false;
            pnl_pur.Visible = false;
            pnlSal.Visible = false;
            ddl_rpttyp.Visible = false;
            PnlEmp.Visible = false;
            pnl_prof.Visible = false;
            pnl_Expence.Visible = false;
            pnl_Transc.Visible = false;
            pnl_acc.Visible = true;

        }

        protected void lnk_Transc_Click(object sender, EventArgs e)
        {
            ddl_rpttyp.SelectedValue = "TRANS";
            Pnlpro.Visible = false;
            pnlstk.Visible = false;
            pnl_pur.Visible = false;
            pnlSal.Visible = false;
            ddl_rpttyp.Visible = false;
            PnlEmp.Visible = false;
            pnl_prof.Visible = false;
            pnl_Expence.Visible = false;
            pnl_acc.Visible = false;
            pnl_Transc.Visible = true;

        }

        protected void DDLMnthwis_SelectedIndexChanged(object sender, EventArgs e)
        {
            TBTDWise.Text = "";
        }
    }
}