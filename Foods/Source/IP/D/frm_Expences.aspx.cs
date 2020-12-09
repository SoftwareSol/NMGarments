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
    public partial class frm_Expences : System.Web.UI.Page
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_ = null;
        DBConnection db = new DBConnection();
        int i = 0;
        string query, pass, accid;
        decimal cashamt = 0;
        public static string branch, company; 


        protected void Page_Load(object sender, EventArgs e)
        {
            v_choice.Text = "Select Button For Transaction";
            if (!IsPostBack)
            {

                dllexptyp.Enabled = false;
                exprmk.Enabled = false;
                TBPrebal.Enabled = false;
                amtpad.Enabled = false;
                DDL_Vchtyp.Enabled = false;
                DDL_Paytyp.Enabled = false;
                date.Text = DateTime.Now.ToShortDateString();
                TBIncodat.Text = DateTime.Now.ToShortDateString();
                pnl_bnk.Visible = false;
                pnl_incombnk.Visible = false;
                branch = Session["BranchID"].ToString();
                company = Session["CompanyID"].ToString();

                pnl_chqamt.Visible = false;
                pnl_chqincomamt.Visible = false;
            
                pnl_cshamt.Visible = false;
                pnl_cshamtincom.Visible = false;

                Panel_incom.Visible = false;
                pnl_pay.Visible = true;
                pnl_bill.Visible = false;
                pnl_loan.Visible = false;
                icombillpnl.Visible = false;
                Panel_CashBook.Visible = false;

                BindDll();

                lbl_Prebal.Text = "Prebtnvious Balance";
                SetInitRowBk();
                SetInitRowBnk();

                TextBox TBChqDat = (TextBox)GVbkExp.Rows[0].Cells[3].FindControl("TBChqDat");
                TBChqDat.Text = DateTime.Now.ToShortDateString();

                OpeningBal();

            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int a = 0;
            a = SavePay();
        }



        private void OpeningBal()
        {
            try
            {
                string currbal;
                string OpenBal;

                //query = " select * from v_cshbook where expensesdat ='" + DateTime.Now.ToString("yyyy-MM-dd") +"' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                DataTable dt_ = new DataTable();
                using (var cmd = new SqlCommand("sp_cshbook", con))
                {
                    //cmd.CommandText = " select * from v_rptpur where MPurID='" + PurId + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                    
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandText="sp_cshbook";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@companyid", Session["CompanyID"].ToString());
                        cmd.Parameters.AddWithValue("@branchid", Session["BranchID"].ToString());
                        da.Fill(dt_);
                    }

                    if (dt_.Rows.Count > 0)
                    {
                        //dt_ = DBConnection.GetQueryData(query);

                        if (dt_.Rows.Count > 0)
                        {
                            OpenBal = dt_.Rows[0]["openingBal"].ToString();

                            if (OpenBal == "" || OpenBal == "0" || OpenBal == null)
                            {
                                TBOpeBal.Text = "0";
                                lbl_Openbalance.Text = "0";
                            }
                            else if (OpenBal != "" || OpenBal != "0" || OpenBal != null)
                            {

                                lbl_Openbalance.Text = dt_.Rows[0]["openingBal"].ToString();
                            }

                            lbl_ClsBal.Text = dt_.Rows[0]["openbal"].ToString();

                            lbl_totalSal.Text = dt_.Rows[0]["Sales"].ToString();
                            lbl_ttlExp.Text = dt_.Rows[0]["Expence"].ToString();


                        }
                        else
                        {

                            lbl_Openbalance.Text = "0";
                        }

                        //query = " select openbal  from v_cshbook where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                        DataTable d2t_ = new DataTable();
                        //dt_ = new DataTable();
                        //dt_ = DBConnection.GetQueryData(query);

                        if (dt_.Rows.Count > 0)
                        {
                            currbal = dt_.Rows[0]["openbal"].ToString();

                            TBOpeBal.Text = dt_.Rows[0]["openbal"].ToString();
                            lbl_Openbalance.Text = dt_.Rows[0]["openbal"].ToString();

                        }   //currbal != "" || currbal != "0" || currbal != null ||
                        else
                        {
                            lbl_Openbalance.Text = "0";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        protected void btn_incom_Click(object sender, EventArgs e)
        {
            v_choice.Text = "";
            exprmk.Enabled = true;
            TBPrebal.Enabled = true;
            amtpad.Enabled = true;
            DDL_Vchtyp.Enabled = true;
            DDL_Paytyp.Enabled = true;
            int a = 0;
            a = SaveIncom();
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetIncomAcc(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = " select distinct(SubHeadCategoriesName) as [SubHeadCategoriesName] " +
                         " from SubHeadCategories where  HeadGeneratedID='001' and SubHeadGeneratedID='0011' " +
                         " and SubHeadCategoriesName like '%" + prefixText + "%' and CompanyId = '" + company + "' and BranchId='" + branch + "'";
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
        public static List<string> GetAcc(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = " select distinct(SubHeadCategoriesName) as [SubHeadCategoriesName] " +
                         " from SubHeadCategories where  HeadGeneratedID='002' and SubHeadGeneratedID IN ('0021', '0023', '0024') " +
                         " and SubHeadCategoriesName like '%" + prefixText + "%' and CompanyId = '" + company + "' and BranchId='" + branch + "'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }

        public void BindDll()
        {
            try
            {

                //For Voucher Type

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select distinct(vchtyp_nam) as [vchtyp_nam] , vchtyp_id  from tbl_vchtyp where ISActive = 'True'";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtvch = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtvch);

                    DDL_Vchtyp.DataSource = dtvch;
                    DDL_Vchtyp.DataTextField = "vchtyp_nam";
                    DDL_Vchtyp.DataValueField = "vchtyp_id";
                    DDL_Vchtyp.DataBind();
                    DDL_Vchtyp.Items.Insert(0, new ListItem("--Select--", "0"));

                    DDL_incomvtyp.DataSource = dtvch;
                    DDL_incomvtyp.DataTextField = "vchtyp_nam";
                    DDL_incomvtyp.DataValueField = "vchtyp_id";
                    DDL_incomvtyp.DataBind();
                    DDL_incomvtyp.Items.Insert(0, new ListItem("--Select--", "0"));

                    con.Close();
                }

                //For Bank

                //using (SqlCommand cmd = new SqlCommand())
                //{
                //    cmd.CommandText = " select CashBnk_nam , CashBnk_id  from tbl_CashBnk where ISActive = 'True'";

                //    cmd.Connection = con;
                //    con.Open();

                //    DataTable dtbnk = new DataTable();
                //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //    adp.Fill(dtbnk);

                //    for (int q = 0; q < GVPay.Rows.Count; q++)
                //    {
                //        DropDownList DDL_Bnk = (DropDownList)GVPay.Rows[q].Cells[0].FindControl("DDL_Bnk");

                //        DDL_Bnk.DataSource = dtbnk;
                //        DDL_Bnk.DataTextField = "CashBnk_nam";
                //        DDL_Bnk.DataValueField = "CashBnk_id";
                //        DDL_Bnk.DataBind();
                //        DDL_Bnk.Items.Insert(0, new ListItem("--Select--", "0"));

                //    } for (int i = 0; i < GVbkExp.Rows.Count; i++)
                //    {
                //        DropDownList DDL_incombnk = (DropDownList)GVbkExp.Rows[i].Cells[0].FindControl("DDL_incombnk");
                //        DDL_incombnk.DataSource = dtbnk;
                //        DDL_incombnk.DataSource = dtbnk;
                //        DDL_incombnk.DataTextField = "CashBnk_nam";
                //        DDL_incombnk.DataValueField = "CashBnk_id";
                //        DDL_incombnk.DataBind();
                //        DDL_incombnk.Items.Insert(0, new ListItem("--Select--", "0"));
                //    }

                //    con.Close();
                //}
                
                // for payments

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select distinct(SubHeadCategoriesGeneratedID),SubHeadCategoriesName " +
                        " from SubHeadCategories where HeadGeneratedID='002' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'  and SubHeadGeneratedID IN ('0021', '0023', '0024')";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtacc = new DataTable();
                    SqlDataAdapter adpacc = new SqlDataAdapter(cmd);
                    adpacc.Fill(dtacc);

                    DDL_ACC.DataSource = dtacc;
                    DDL_ACC.DataTextField = "SubHeadCategoriesGeneratedID";
                    DDL_ACC.DataValueField = "SubHeadCategoriesGeneratedID";
                    DDL_ACC.DataBind();
                    DDL_ACC.Items.Insert(0, new ListItem("--Select Account--", "0"));

                    
                    con.Close();
                }

                //for income

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select distinct(SubHeadCategoriesGeneratedID),SubHeadCategoriesName " +
                        " from SubHeadCategories where HeadGeneratedID='001' and SubHeadGeneratedID ='0011' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtacc = new DataTable();
                    SqlDataAdapter adpacc = new SqlDataAdapter(cmd);
                    adpacc.Fill(dtacc);

                    
                    DDL_IncomAcc.DataSource = dtacc;
                    DDL_IncomAcc.DataTextField = "SubHeadCategoriesGeneratedID";
                    DDL_IncomAcc.DataValueField = "SubHeadCategoriesGeneratedID";
                    DDL_IncomAcc.DataBind();
                    DDL_IncomAcc.Items.Insert(0, new ListItem("--Select Account--", "0"));

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        protected void GVbkExp_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

                    GVbkExp.DataSource = dt;
                    GVbkExp.DataBind();

                    SetPreRowitm();

                    TextBox TBChqDat = (TextBox)GVbkExp.Rows[0].Cells[0].FindControl("TBChqDat");
                    TBChqDat.Text = DateTime.Now.ToString();
                }
            }

        }

        private void SetInitRowBk()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("icombank", typeof(string)));
            dt.Columns.Add(new DataColumn("CheqNo", typeof(string)));
            dt.Columns.Add(new DataColumn("CheqAmt", typeof(string)));
            dt.Columns.Add(new DataColumn("CheqDat", typeof(string)));
            
            dr = dt.NewRow();

            dr["icombank"] = "0";
            dr["CheqNo"] = string.Empty;
            dr["CheqAmt"] = "0.00";
            dr["CheqDat"] = string.Empty;

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["dt_adItm"] = dt;
            
            GVbkExp.DataSource = dt;
            GVbkExp.DataBind();


        }

        private void SetInitRowBnk()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("bank", typeof(string)));
            dt.Columns.Add(new DataColumn("CheqNum", typeof(string)));
            dt.Columns.Add(new DataColumn("CheqAmnt", typeof(string)));
            dt.Columns.Add(new DataColumn("CheqDate", typeof(string)));

            dr = dt.NewRow();

            dr["bank"] = "0";
            dr["CheqNum"] = string.Empty;
            dr["CheqAmnt"] = "0.00";
            dr["CheqDate"] = string.Empty;

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["dt_adItms"] = dt;

            GVPay.DataSource = dt;
            GVPay.DataBind();
        }


        private void AddNewRowBnk()
        {
            int rowIndex = 0;

            if (ViewState["dt_adItms"] != null)
            {
                DataTable dt = (DataTable)ViewState["dt_adItms"];
                DataRow drRow = null;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        Label lblbnk = (Label)GVPay.Rows[rowIndex].Cells[0].FindControl("lblbnk");
                        DropDownList DDL_Bnk = (DropDownList)GVPay.Rows[rowIndex].Cells[0].FindControl("DDL_Bnk");
                        TextBox TBChqnum = (TextBox)GVPay.Rows[rowIndex].Cells[1].FindControl("TBChqnum");
                        TextBox TBChqAmnt = (TextBox)GVPay.Rows[rowIndex].Cells[2].FindControl("TBChqAmnt");
                        TextBox TBChqDate = (TextBox)GVPay.Rows[rowIndex].Cells[3].FindControl("TBChqDate");

                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["bank"] = DDL_Bnk.Text;
                        dt.Rows[i - 1]["CheqNum"] = TBChqnum.Text;
                        dt.Rows[i - 1]["CheqAmnt"] = TBChqAmnt.Text;
                        dt.Rows[i - 1]["CheqDate"] = TBChqDate.Text;


                        rowIndex++;
                    }
                    
                    dt.Rows.Add(drRow);
                    ViewState["dt_adItms"] = dt;

                    GVPay.DataSource = dt;
                    GVPay.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreRowBnkitm();
        }


        private void AddNewRow()
        {
            int rowIndex = 0;

            if (ViewState["dt_adItm"] != null)
            {
                DataTable dt = (DataTable)ViewState["dt_adItm"];
                DataRow drRow = null;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        Label lblincombnk = (Label)GVbkExp.Rows[rowIndex].Cells[0].FindControl("lblincombnk");
                        DropDownList DDL_incombnk = (DropDownList)GVbkExp.Rows[rowIndex].Cells[0].FindControl("DDL_incombnk");
                        TextBox TBChqno = (TextBox)GVbkExp.Rows[rowIndex].Cells[1].FindControl("TBChqno");
                        TextBox TBChqAmt = (TextBox)GVbkExp.Rows[rowIndex].Cells[2].FindControl("TBChqAmt");
                        TextBox TBChqDat = (TextBox)GVbkExp.Rows[rowIndex].Cells[3].FindControl("TBChqDat");
                   
                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["icombank"] = DDL_incombnk.Text;
                        dt.Rows[i - 1]["CheqNo"] = TBChqno.Text;
                        dt.Rows[i - 1]["CheqAmt"] = TBChqAmt.Text;
                        dt.Rows[i - 1]["CheqDat"] = TBChqDat.Text;


                        rowIndex++;
                    }

                    dt.Rows.Add(drRow);
                    ViewState["dt_adItm"] = dt;

                    GVbkExp.DataSource = dt;
                    GVbkExp.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreRowitm();
        }


        private void SetPreRowBnkitm()
        {
            try
            {
                int rowIndex = 0;
                if (ViewState["dt_adItms"] != null)
                {
                    DataTable dt = (DataTable)ViewState["dt_adItms"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            Label lblbnk = (Label)GVPay.Rows[rowIndex].Cells[0].FindControl("lblbnk");
                            DropDownList DDL_Bnk = (DropDownList)GVPay.Rows[rowIndex].Cells[0].FindControl("DDL_Bnk");
                            TextBox TBChqnum = (TextBox)GVPay.Rows[rowIndex].Cells[1].FindControl("TBChqnum");
                            TextBox TBChqAmnt = (TextBox)GVPay.Rows[rowIndex].Cells[2].FindControl("TBChqAmnt");
                            TextBox TBChqDate = (TextBox)GVPay.Rows[rowIndex].Cells[3].FindControl("TBChqDate");

                            string incombak = dt.Rows[i]["bank"].ToString();

                            if (incombak != "")
                            {
                                lblbnk.Text = dt.Rows[i]["bank"].ToString();
                                DDL_Bnk.SelectedValue = lblbnk.Text;
                            }
                            else
                            {
                                lblbnk.Text = "";
                            }

                            string CheqNo = dt.Rows[i]["CheqNum"].ToString();

                            if (CheqNo != "")
                            {
                                TBChqnum.Text = dt.Rows[i]["CheqNum"].ToString();
                            }
                            else
                            {
                                TBChqnum.Text = "";
                            }

                            string CheqAmt = dt.Rows[i]["CheqAmnt"].ToString();

                            if (CheqAmt != "")
                            {
                                TBChqAmnt.Text = dt.Rows[i]["CheqAmnt"].ToString();
                            }
                            else
                            {
                                TBChqAmnt.Text = "0.00";
                            }

                            string CheqDat = dt.Rows[i]["CheqDate"].ToString();

                            if (CheqDat != "")
                            {
                                TBChqDate.Text = dt.Rows[i]["CheqDate"].ToString();
                            }
                            else
                            {
                                TBChqDate.Text = DateTime.Now.ToString();
                            }
                            rowIndex++;

                        }
                    }
                }
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

                            Label lblincombnk = (Label)GVbkExp.Rows[rowIndex].Cells[0].FindControl("lblincombnk");
                            DropDownList DDL_incombnk = (DropDownList)GVbkExp.Rows[rowIndex].Cells[0].FindControl("DDL_incombnk");
                            TextBox TBChqno = (TextBox)GVbkExp.Rows[rowIndex].Cells[1].FindControl("TBChqno");
                            TextBox TBChqAmt = (TextBox)GVbkExp.Rows[rowIndex].Cells[2].FindControl("TBChqAmt");
                            TextBox TBChqDat = (TextBox)GVbkExp.Rows[rowIndex].Cells[3].FindControl("TBChqDat");

                            string incombak = dt.Rows[i]["icombank"].ToString();

                            if (incombak != "")
                            {
                                lblincombnk.Text = dt.Rows[i]["icombank"].ToString();
                                DDL_incombnk.SelectedValue = lblincombnk.Text;
                            }
                            else
                            {
                                lblincombnk.Text = "";
                            }

                            string CheqNo = dt.Rows[i]["CheqNo"].ToString();

                            if (CheqNo != "")
                            {
                                TBChqno.Text = dt.Rows[i]["CheqNo"].ToString();
                            }
                            else
                            {
                                TBChqno.Text = "";
                            }

                            string CheqAmt = dt.Rows[i]["CheqAmt"].ToString();

                            if (CheqAmt != "")
                            {
                                TBChqAmt.Text = dt.Rows[i]["CheqAmt"].ToString();
                            }
                            else
                            {
                                TBChqAmt.Text = "0.00";
                            }

                            string CheqDat = dt.Rows[i]["CheqDat"].ToString();

                            if (CheqDat != "")
                            {
                                TBChqDat.Text = dt.Rows[i]["CheqDat"].ToString();
                            }
                            else
                            {
                                TBChqDat.Text = DateTime.Now.ToString();
                            }
                            rowIndex++;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        protected void DDL_Paytyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_Paytyp.SelectedValue == "2" || DDL_Paytyp.SelectedValue == "3")
            {
                pnl_bnk.Visible = true;                
                pnl_chqamt.Visible = false;
                pnl_cshamt.Visible = false;
                TB_CshAmt.Text = "0.00";
            }
            else if (DDL_Paytyp.SelectedValue != "2")
            {
                pnl_bnk.Visible = false;
                pnl_chqamt.Visible = false;
                pnl_cshamt.Visible = false;
                TB_ChqAmt.Text = "0.00";

                for (int i = 0; i < GVPay.Rows.Count; i++)
                {
                    DropDownList DDL_Bnk = (DropDownList)GVPay.Rows[i].Cells[0].FindControl("DDL_Bnk");

                    DDL_Bnk.SelectedValue = "1"; 
                }
            }
        }

        protected void DDL_Paytypincom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_Paytypincom.SelectedValue == "2" || DDL_Paytypincom.SelectedValue == "3")
            {
                pnl_incombnk.Visible = true;
                pnl_chqincomamt.Visible = false;
                pnl_cshamtincom.Visible = false;
                TB_CshAmtincom.Text = "0.00";
            }
            else if (DDL_Paytypincom.SelectedValue != "2")
            {
                pnl_incombnk.Visible = false;
                pnl_chqincomamt.Visible = false;
                pnl_cshamtincom.Visible = false;
                TB_ChqAmtincom.Text = "0.00";

                for (int i = 0; i < GVbkExp.Rows.Count; i++)
                {
                    DropDownList DDL_incombnk = (DropDownList)GVbkExp.Rows[i].Cells[0].FindControl("DDL_incombnk");

                    DDL_incombnk.SelectedValue = "1";
                }
            }
        }

        protected void linkbtnadd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void linkbtnaddbnk_Click(object sender, EventArgs e)
        {
            AddNewRowBnk();
        }

        private int SavePay()
        {
            int j = 1;
            string accid="";
            decimal cashamt = 0 ;

            con.Open();

            SqlCommand command = con.CreateCommand();

            SqlTransaction transaction;

            // Start a local transaction.
            transaction = con.BeginTransaction("AccountTrans");

            // Must assign both transaction object and connection 
            // to Command object for a pending local transaction
            command.Connection = con;
            command.Transaction = transaction;
            try
            {
                if (DDL_Paytyp.SelectedValue == "1")
                {

                    //amtpaid = (Convert.ToDecimal(TBOpeBal.Text.Trim()) - Convert.ToDecimal(amtpad.Text.Trim())).ToString();

                    command.CommandText = "expense";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@acctitle", dllexptyp.Text);
                    command.Parameters.AddWithValue("@accno", DDL_ACC.SelectedValue.Trim());
                    command.Parameters.AddWithValue("@expensesdat", date.Text.Trim());
                    command.Parameters.AddWithValue("@billno", bilno.Text.Trim());
                    command.Parameters.AddWithValue("@typeofpay", DDL_Paytyp.SelectedItem.Text.Trim());
                    command.Parameters.AddWithValue("@expencermk", exprmk.Text.Trim());
                    command.Parameters.AddWithValue("@cashamt", TB_CshAmt.Text.Trim());
                    command.Parameters.AddWithValue("@ChqDat", DateTime.Now.ToString());
                    command.Parameters.AddWithValue("@CashBnk_id", "1");
                    command.Parameters.AddWithValue("@CashBnk_nam", "");
                    command.Parameters.AddWithValue("@bankamt", TB_ChqAmt.Text.Trim());
                    command.Parameters.AddWithValue("@PaymentIn", "0");
                    command.Parameters.AddWithValue("@PaymentOut", amtpad.Text.Trim());
                    command.Parameters.AddWithValue("@Amountpaid", amtpad.Text.Trim());
                    command.Parameters.AddWithValue("@prevbal", TBPrebal.Text.Trim());
                    command.Parameters.AddWithValue("@createat", DateTime.Now.ToString());
                    command.Parameters.AddWithValue("@createby", Session["Username"]);
                    command.Parameters.AddWithValue("@companyid", Session["CompanyID"]);
                    command.Parameters.AddWithValue("@branchid", Session["BranchID"]);
                    command.Parameters.AddWithValue("@ChqNO", "");
                    command.Parameters.AddWithValue("@OpenBal", TBOpeBal.Text.Trim());
                    command.Parameters.AddWithValue("@openingBal", lbl_Openbalance.Text.Trim());
                    command.Parameters.AddWithValue("@opening_balance", lbl_Openbalance.Text.Trim());
                    

                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                    //amtpaid = (Convert.ToDecimal(TBOpeBal.Text.Trim()) - Convert.ToDecimal(amtpad.Text.Trim())).ToString();
                    //
                    if (chkloan.Checked == true)
                    {
                        command.Parameters.Clear();
                        command.CommandText = " select SubHeadCategoriesID  from SubHeadCategories where " +
                            " SubHeadCategoriesGeneratedID='" + DDL_ACC.SelectedValue.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                        command.CommandType = CommandType.Text;

                        SqlDataAdapter adp = new SqlDataAdapter(command);

                        DataTable dt = new DataTable();
                        adp.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            accid = dt.Rows[0]["SubHeadCategoriesID"].ToString();
                        }
                        command.CommandText = "sp_loan";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@loanid", "");
                        command.Parameters.AddWithValue("@loanamt", amtpad.Text.Trim());
                        command.Parameters.AddWithValue("@SubHeadCategoriesID", accid.Trim());
                        command.Parameters.AddWithValue("@AccNo", DDL_ACC.SelectedValue.Trim());
                        command.Parameters.AddWithValue("@createat", DateTime.Now.ToString());
                        command.Parameters.AddWithValue("@createby", Session["Username"]);
                        command.Parameters.AddWithValue("@createterminal", Session["BranchID"]);
                        command.Parameters.AddWithValue("@updateat", DateTime.Now.ToString());
                        command.Parameters.AddWithValue("@updateby", "");
                        command.ExecuteNonQuery();
                    }

                    command.Parameters.Clear();
                    command.CommandText = "sp_Purcre";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@supplierId", DDL_ACC.SelectedValue.Trim());
                    command.Parameters.AddWithValue("@CredAmt", lbl_amtremain.Text.Trim());
                    command.ExecuteNonQuery();

                }
                else if (DDL_Paytyp.SelectedValue == "2" || DDL_Paytyp.SelectedValue == "3")
                {
                    for (int i = 0; i < GVPay.Rows.Count; i++)
                    {
                        DropDownList DDL_Bnk = (DropDownList)GVPay.Rows[i].Cells[0].FindControl("DDL_Bnk");
                        TextBox TBChqDate = (TextBox)GVPay.Rows[i].Cells[3].FindControl("TBChqDate");
                        TextBox TBChqAmnt = (TextBox)GVPay.Rows[i].Cells[2].FindControl("TBChqAmnt");
                        TextBox TBChqnum = (TextBox)GVPay.Rows[i].Cells[1].FindControl("TBChqnum");

                        command.Parameters.Clear();
                        command.CommandText = "expense";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@acctitle", dllexptyp.Text);
                        command.Parameters.AddWithValue("@accno", DDL_ACC.SelectedValue.Trim());
                        command.Parameters.AddWithValue("@expensesdat", date.Text.Trim());
                        command.Parameters.AddWithValue("@billno", bilno.Text.Trim());
                        command.Parameters.AddWithValue("@typeofpay", DDL_Paytyp.SelectedItem.Text.Trim());
                        command.Parameters.AddWithValue("@expencermk", exprmk.Text.Trim());
                        command.Parameters.AddWithValue("@ChqDat", TBChqDate.Text.Trim());
                        command.Parameters.AddWithValue("@CashBnk_id", DDL_Bnk.SelectedValue.Trim());
                        command.Parameters.AddWithValue("@CashBnk_nam", DDL_Bnk.SelectedItem.Text.Trim());
                        command.Parameters.AddWithValue("@bankamt", TBChqAmnt.Text.Trim());
                        command.Parameters.AddWithValue("@ChqNO", TBChqnum.Text.Trim());

                        command.Parameters.AddWithValue("@PaymentIn", "0");

                        if (DDL_Paytyp.SelectedValue == "2")
                        {
                            amtpad.Text = TBChqAmnt.Text.Trim();

                        }
                        else if (DDL_Paytyp.SelectedValue == "3")
                        {
                            float GTotal = 0;
                            for (int k = 0; k < GVPay.Rows.Count; k++)
                            {
                                TextBox total = (TextBox)GVPay.Rows[k].FindControl("TBChqAmnt");
                                GTotal += Convert.ToSingle(total.Text);
                            }

                            cashamt = Convert.ToDecimal(amtpad.Text) - Convert.ToDecimal(GTotal);

                        }

                        command.Parameters.AddWithValue("@PaymentOut", amtpad.Text.Trim());
                        command.Parameters.AddWithValue("@cashamt", cashamt);
                        command.Parameters.AddWithValue("@Amountpaid", amtpad.Text.Trim());
                        command.Parameters.AddWithValue("@prevbal", lbl_amtremain.Text.Trim());
                        command.Parameters.AddWithValue("@createat", DateTime.Now.ToString());
                        command.Parameters.AddWithValue("@createby", Session["Username"]);
                        command.Parameters.AddWithValue("@companyid", Session["CompanyID"]);
                        command.Parameters.AddWithValue("@branchid", Session["BranchID"]);

                        //Opening Balance



                        command.Parameters.AddWithValue("@OpenBal", TBOpeBal.Text.Trim());
                        command.Parameters.AddWithValue("@openingBal", lbl_Openbalance.Text.Trim());
                        command.Parameters.AddWithValue("@opening_balance", lbl_Openbalance.Text.Trim());

                        command.ExecuteNonQuery();

                        if (chkloan.Checked == true)
                        {
                            command.Parameters.Clear();
                            command.CommandText = " select SubHeadCategoriesID  from SubHeadCategories where " +
                                " SubHeadCategoriesGeneratedID='" + DDL_ACC.SelectedValue.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                            command.CommandType = CommandType.Text;

                            SqlDataAdapter adp = new SqlDataAdapter(command);

                            DataTable dt = new DataTable();
                            adp.Fill(dt);

                            if (dt.Rows.Count > 0)
                            {
                                accid = dt.Rows[0]["SubHeadCategoriesID"].ToString();
                            }
                            command.CommandText = "sp_loan";
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@loanid", "");
                            command.Parameters.AddWithValue("@loanamt", amtpad.Text.Trim());
                            command.Parameters.AddWithValue("@SubHeadCategoriesID", accid.Trim());
                            command.Parameters.AddWithValue("@AccNo", DDL_ACC.SelectedValue.Trim());
                            command.Parameters.AddWithValue("@createat", DateTime.Now.ToString());
                            command.Parameters.AddWithValue("@createby", Session["Username"]);
                            command.Parameters.AddWithValue("@createterminal", Session["BranchID"]);
                            command.Parameters.AddWithValue("@updateat", DateTime.Now.ToString());
                            command.Parameters.AddWithValue("@updateby", "");
                            command.ExecuteNonQuery();
                        }

                        command.Parameters.Clear();
                        command.CommandText = "sp_Purcre";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@supplierId", DDL_ACC.SelectedValue.Trim());
                        command.Parameters.AddWithValue("@CredAmt", lbl_amtremain.Text.Trim());
                        command.ExecuteNonQuery();

                    }
                }

                transaction.Commit();
                con.Close();
               
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
                Response.Redirect("frm_Expences.aspx");
            }

            return j;
        }

        private int SaveIncom()
        {
            int j = 1;

            con.Open();

            SqlCommand command = con.CreateCommand();

            SqlTransaction transaction;

            // Start a local transaction.
            transaction = con.BeginTransaction("AccountTrans");

            // Must assign both transaction object and connection 
            // to Command object for a pending local transaction
            command.Connection = con;
            command.Transaction = transaction;
            try
            {
                if (DDL_Paytypincom.SelectedValue == "1")
                {
                    command.CommandText = "expense";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@acctitle", TBincomacc.Text);
                    command.Parameters.AddWithValue("@accno", DDL_IncomAcc.SelectedValue.Trim());
                    command.Parameters.AddWithValue("@expensesdat", TBIncodat.Text.Trim());
                    command.Parameters.AddWithValue("@billno", TBincomBill.Text.Trim());
                    command.Parameters.AddWithValue("@typeofpay", DDL_Paytypincom.SelectedItem.Text.Trim());
                    command.Parameters.AddWithValue("@expencermk", TbincomRmks.Text.Trim());
                    command.Parameters.AddWithValue("@cashamt", TB_CshAmtincom.Text.Trim());
                    command.Parameters.AddWithValue("@ChqDat", DateTime.Now.ToString());
                    command.Parameters.AddWithValue("@CashBnk_id", "1");
                    command.Parameters.AddWithValue("@CashBnk_nam", "");
                    command.Parameters.AddWithValue("@bankamt", "0");
                    command.Parameters.AddWithValue("@PaymentIn", TB_incompaids.Text.Trim());
                    command.Parameters.AddWithValue("@PaymentOut", "0");
                    command.Parameters.AddWithValue("@Amountpaid", TB_incompaids.Text.Trim());
                    command.Parameters.AddWithValue("@prevbal", TBPrebalincom.Text.Trim());
                    command.Parameters.AddWithValue("@createat", DateTime.Now.ToString());
                    command.Parameters.AddWithValue("@createby", Session["Username"]);
                    command.Parameters.AddWithValue("@companyid", Session["CompanyID"]);
                    command.Parameters.AddWithValue("@branchid", Session["BranchID"]);
                    command.Parameters.AddWithValue("@ChqNO", "");
                    command.Parameters.AddWithValue("@OpenBal", TBOpeBal.Text.Trim());
                    command.Parameters.AddWithValue("@openingBal", lbl_Openbalance.Text.Trim());
                    command.Parameters.AddWithValue("@opening_balance", lbl_Openbalance.Text.Trim());

                    command.ExecuteNonQuery();



                    if (DDL_IncomAcc.SelectedValue != "001114")
                    {
                        command.Parameters.Clear();
                        command.CommandText = "sp_Salcre";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CustId", DDL_IncomAcc.SelectedValue.Trim());
                        command.Parameters.AddWithValue("@CredAmt", lbl_amtremainincom.Text.Trim());
                        command.ExecuteNonQuery();
                    }

                }
                else if (DDL_Paytypincom.SelectedValue == "2" || DDL_Paytypincom.SelectedValue == "3")
                {
                    for (int i = 0; i < GVbkExp.Rows.Count; i++)
                    {
                        DropDownList DDL_incombnk = (DropDownList)GVbkExp.Rows[i].Cells[0].FindControl("DDL_incombnk");
                        TextBox TBChqno = (TextBox)GVbkExp.Rows[i].Cells[1].FindControl("TBChqno");
                        TextBox TBChqAmt = (TextBox)GVbkExp.Rows[i].Cells[2].FindControl("TBChqAmt");
                        TextBox TBChqDat = (TextBox)GVbkExp.Rows[i].Cells[3].FindControl("TBChqDat");
                        
                        command.Parameters.Clear();
                        command.CommandText = "expense";
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@acctitle", TBincomacc.Text);
                        command.Parameters.AddWithValue("@accno", DDL_IncomAcc.SelectedValue.Trim());
                        command.Parameters.AddWithValue("@expensesdat", TBIncodat.Text.Trim());
                        command.Parameters.AddWithValue("@billno", TBincomBill.Text.Trim());
                        command.Parameters.AddWithValue("@typeofpay", DDL_Paytypincom.SelectedItem.Text.Trim());
                        command.Parameters.AddWithValue("@expencermk", TbincomRmks.Text.Trim());
                        command.Parameters.AddWithValue("@ChqDat", TBChqDat.Text.Trim());
                        command.Parameters.AddWithValue("@CashBnk_id", DDL_incombnk.SelectedValue.Trim());
                        command.Parameters.AddWithValue("@CashBnk_nam", DDL_incombnk.SelectedItem.Text.Trim());
                        command.Parameters.AddWithValue("@bankamt", TBChqAmt.Text.Trim());
                        command.Parameters.AddWithValue("@ChqNO", TBChqno.Text.Trim());

                        command.Parameters.AddWithValue("@PaymentOut", "0");


                        if (DDL_Paytypincom.SelectedValue == "2")
                        {
                            TB_incompaids.Text = TBChqAmt.Text.Trim();

                        }
                        else if (DDL_Paytypincom.SelectedValue == "3")
                        {
                            float GTotal = 0;
                            for (int k = 0; k < GVbkExp.Rows.Count; k++)
                            {
                                TextBox total = (TextBox)GVbkExp.Rows[k].FindControl("TBChqAmt");
                                GTotal += Convert.ToSingle(total.Text);
                            }

                            cashamt = Convert.ToDecimal(TB_incompaids.Text) - Convert.ToDecimal(GTotal);

                            //command.Parameters.AddWithValue("@PaymentIn", TBChqAmt.Text.Trim());

                        }

                        command.Parameters.AddWithValue("@PaymentIn", TB_incompaids.Text.Trim());
                        command.Parameters.AddWithValue("@cashamt", cashamt);
                        command.Parameters.AddWithValue("@Amountpaid", TB_incompaids.Text.Trim());
                        command.Parameters.AddWithValue("@prevbal", lbl_amtremainincom.Text.Trim());
                        command.Parameters.AddWithValue("@createat", DateTime.Now.ToString());
                        command.Parameters.AddWithValue("@createby", Session["Username"]);
                        command.Parameters.AddWithValue("@companyid", Session["CompanyID"]);
                        command.Parameters.AddWithValue("@branchid", Session["BranchID"]);
                        command.Parameters.AddWithValue("@OpenBal", TBOpeBal.Text.Trim());
                        command.Parameters.AddWithValue("@openingBal", lbl_Openbalance.Text.Trim());
                    
                        command.ExecuteNonQuery();
                        command.Parameters.Clear();

                        if (DDL_IncomAcc.SelectedValue != "001114")
                        {
                            command.Parameters.Clear();
                            command.CommandText = "sp_Salcre";
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@CustId", DDL_IncomAcc.SelectedValue.Trim());
                            command.Parameters.AddWithValue("@CredAmt", lbl_amtremainincom.Text.Trim());
                            command.ExecuteNonQuery();
                        }


                    }
                }
                
                transaction.Commit();
                con.Close();
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
                Response.Redirect("frm_Expences.aspx");
            }

            return j;
        }
        private int Update()
        {
            int u = 1;

            pass = "0";

            con.Open();

            SqlCommand command = con.CreateCommand();

            SqlTransaction transaction;

            // Start a local transaction.
            transaction = con.BeginTransaction("ProductionTrans");

            // Must assign both transaction object and connection 
            // to Command object for a pending local transaction
            command.Connection = con;
            command.Transaction = transaction;
            try
            {
                command.CommandText = "expenseupdate";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@acctitle", dllexptyp.Text);
                command.Parameters.AddWithValue("@accno", DDL_ACC.SelectedValue);
                //command.Parameters.AddWithValue("@expensesdat", ed.Text);
                //command.Parameters.AddWithValue("@billno", bn.Text);
                //command.Parameters.AddWithValue("@amountpaid", ap.Text);
                //command.Parameters.AddWithValue("@expencermk", ep.Text);
                command.Parameters.AddWithValue("@createat", DateTime.Now.ToString());
                command.Parameters.AddWithValue("@createby", "Admin");
                command.Parameters.AddWithValue("@companyid", "com12");
                command.Parameters.AddWithValue("@branchid", "bnc 122");
                command.Parameters.AddWithValue("@expenceid", HFUsrId.Value);
                command.ExecuteNonQuery();

                transaction.Commit();
                Response.Write("<script>alert('Data updated successfully')</script>");
                con.Close();
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
            }

            return u;
        }

        protected void DDL_ACC_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDL_Vchtyp.Enabled = false;
            DDL_Paytyp.Enabled = false;
            try
            {
                query = " select * from SubHeadCategories where HeadGeneratedID='002' " +
                    " and HeadGeneratedID='002' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "' and SubHeadGeneratedID IN ('0021', '0023', '0024') and SubHeadCategoriesGeneratedID= '" +
                        DDL_ACC.SelectedValue.Trim() + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    dllexptyp.Text = dt_.Rows[0]["SubHeadCategoriesName"].ToString();
                }

                query = "select * from  tbl_Purcredit where supplierId='" + DDL_ACC.SelectedValue.Trim() + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    TBPrebal.Text = dt_.Rows[0]["CredAmt"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        protected void DDL_IncomAcc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                query = " select * from SubHeadCategories where " +
                    " HeadGeneratedID='001' and SubHeadGeneratedID ='0011' and SubHeadCategoriesGeneratedID= '" +
                        DDL_IncomAcc.SelectedValue.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    TBincomacc.Text = dt_.Rows[0]["SubHeadCategoriesName"].ToString();
                }

                query = "select CredAmt from tbl_Salcredit where CustomerID ='" + DDL_IncomAcc.SelectedValue.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {   
                    TBPrebalincom.Text = dt_.Rows[0]["CredAmt"].ToString();
                }

            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }



        protected void TBincomacc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                query = " select * from SubHeadCategories where " +
                        " HeadGeneratedID='001' and SubHeadGeneratedID = '0011' and " +
                        " SubHeadCategoriesName= '" + TBincomacc.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    DDL_IncomAcc.SelectedValue = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                }

                query = "select CredAmt from tbl_Salcredit where CustomerID ='" + DDL_IncomAcc.SelectedValue.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    TBPrebalincom.Text = dt_.Rows[0]["CredAmt"].ToString();
                    lbl_amtremainincom.Text = dt_.Rows[0]["CredAmt"].ToString();
                }

            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }

        }

        protected void dllexptyp_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DDL_Vchtyp.Enabled = true;
                // for accounts


                query = " select * from SubHeadCategories where HeadGeneratedID='002' " +
                " and HeadGeneratedID='002' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "' and SubHeadGeneratedID IN ('0021', '0023', '0024') and " +
                " SubHeadCategoriesName= '" + dllexptyp.Text.Trim() + "'";

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    DDL_ACC.SelectedValue = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                }

                string typ = dt_.Rows[0]["SubHeadGeneratedID"].ToString();


                // For Credit
                query = "select * from  tbl_Purcredit where supplierId='" + DDL_ACC.SelectedValue.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    TBPrebal.Text = dt_.Rows[0]["CredAmt"].ToString();
                    lbl_amtremain.Text = dt_.Rows[0]["CredAmt"].ToString();
                }


                
                if (typ == "0023")
                {
                    lbl_Prebal.Text = "Salary";
                    pnl_loan.Visible = true;
                    query = " select * from users " +
                        " inner join  SubHeadCategories on users.Username = SubHeadCategories.SubHeadCategoriesName " +
                        "  where SubHeadGeneratedID = '0023' and username= '" + dllexptyp.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                    dt_ = new DataTable();
                    dt_ = DBConnection.GetQueryData(query);

                    if (dt_.Rows.Count > 0)
                    {
                        TBPrebal.Text = dt_.Rows[0]["Salary"].ToString();
                    }
                }
                else
                {
                    pnl_loan.Visible = false;
                }


            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }

        }

        
        protected void DDL_incomvtyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDL_Paytyp.Enabled = false;
            if (DDL_Vchtyp.SelectedValue != "")
            {
                getPay_Typ(DDL_incomvtyp.SelectedValue.Trim());
            }
            else
            {
                lblmssg.Text = "Please Select The Voucher Type First!";
            }
        }


        protected void DDL_Vchtyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDL_Paytyp.Enabled = true;
            if (DDL_Vchtyp.SelectedValue != "")
            {
                getPay_Typ(DDL_Vchtyp.SelectedValue.Trim());
            }
            else
            {
                lblmssg.Text = "Please Select The Voucher Type First!";
            }
        }


        public void getPay_Typ(string Vchtyp)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandText = " select rtrim('[' + CAST(PayTyp_id AS VARCHAR(200)) + ']-' + PayTyp_nam ) as [PayTyp_nam], PayTyp_id  from tbl_PayTyp where ISActive = 'True'";
                    cmd.CommandText = "  select distinct tbl_PayTyp.PayTyp_id, PayTyp_nam , tbl_PayTyp.vchtyp_id,tbl_PayTyp.PayTyp_id " +
                        " from tbl_PayTyp inner join tbl_vchtyp on tbl_PayTyp.vchtyp_id= tbl_vchtyp.vchtyp_id  " +
                        " where tbl_PayTyp.ISActive = 'True'  and tbl_vchtyp.vchtyp_id = '" + Vchtyp + "'";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtPaytyp = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtPaytyp);

                    DDL_Paytyp.DataSource = dtPaytyp;
                    DDL_Paytyp.DataTextField = "PayTyp_nam";
                    DDL_Paytyp.DataValueField = "PayTyp_id";
                    DDL_Paytyp.DataBind();
                    DDL_Paytyp.Items.Insert(0, new ListItem("--Select--", "0"));

                    DDL_Paytypincom.DataSource = dtPaytyp;
                    DDL_Paytypincom.DataTextField = "PayTyp_nam";
                    DDL_Paytypincom.DataValueField = "PayTyp_id";
                    DDL_Paytypincom.DataBind();
                    DDL_Paytypincom.Items.Insert(0, new ListItem("--Select--", "0"));

                    con.Close();
                }

            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }

        }

        protected void amtpad_TextChanged(object sender, EventArgs e)
        {
            try
            {   
                decimal bal=0;
                if (amtpad.Text != "0")
                {
                    bal = Convert.ToDecimal(TBPrebal.Text.Trim()) - Convert.ToDecimal(amtpad.Text.Trim());
                }
                else if (amtpad.Text != "0")
                {
                    bal = Convert.ToDecimal(TBPrebal.Text.Trim()) - Convert.ToDecimal(amtpad.Text.Trim()); 
                }

                lbl_amtremain.Text = bal.ToString();
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }



        protected void TB_incompaid_TextChanged(object sender, EventArgs e)
        {
            try
            {   
                decimal bal=0;
                if (TBPrebalincom.Text != "0")
                {
                    bal = Convert.ToDecimal(TBPrebalincom.Text.Trim()) - Convert.ToDecimal(TB_incompaids.Text.Trim());
                }
                   
                lbl_amtremainincom.Text = bal.ToString();


            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }


        protected void btn_pay_Click(object sender, EventArgs e)
        {
            DDL_Vchtyp.Enabled = false;
            DDL_Paytyp.Enabled = false;
            dllexptyp.Enabled = true;
            v_choice.Text = "";
            exprmk.Enabled = true;
            TBPrebal.Enabled = true;
            amtpad.Enabled = true;           
            pnl_pay.Visible = true;
            Panel_incom.Visible = false;
            Panel_CashBook.Visible = false;

        }

        protected void btn_incm_Click(object sender, EventArgs e)
        {
            DDL_Vchtyp.Enabled = false;
            DDL_Paytyp.Enabled = false;
            dllexptyp.Enabled = true;
            pnl_pay.Visible = false;
            Panel_incom.Visible = true;
            Panel_CashBook.Visible = false;
        }

        protected void GVbkExp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select CashBnk_nam , CashBnk_id  from tbl_CashBnk where ISActive = 'True'";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtbnk = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtbnk);

                    for (int i = 0; i < GVbkExp.Rows.Count; i++)
                    {
                        DropDownList DDL_incombnk = (DropDownList)GVbkExp.Rows[i].Cells[0].FindControl("DDL_incombnk");
                        DDL_incombnk.DataSource = dtbnk;
                        DDL_incombnk.DataSource = dtbnk;
                        DDL_incombnk.DataTextField = "CashBnk_nam";
                        DDL_incombnk.DataValueField = "CashBnk_id";
                        DDL_incombnk.DataBind();
                        DDL_incombnk.Items.Insert(0, new ListItem("--Select--", "0"));
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        protected void GVPay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select CashBnk_nam , CashBnk_id  from tbl_CashBnk where ISActive = 'True'";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtbnk = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtbnk);

                    for (int q = 0; q < GVPay.Rows.Count; q++)
                    {
                        DropDownList DDL_Bnk = (DropDownList)GVPay.Rows[q].Cells[0].FindControl("DDL_Bnk");

                        DDL_Bnk.DataSource = dtbnk;
                        DDL_Bnk.DataTextField = "CashBnk_nam";
                        DDL_Bnk.DataValueField = "CashBnk_id";
                        DDL_Bnk.DataBind();
                        DDL_Bnk.Items.Insert(0, new ListItem("--Select--", "0"));

                    }

                    con.Close();
                }
            }
            catch (Exception ex) {

                lblmssg.Text = ex.Message;
            }

        }

        protected void GVPay_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ViewState["dt_adItms"] != null)
            {

                DataTable dt = (DataTable)ViewState["dt_adItms"];
                DataRow drCurrentRow = null;
                int rowIndex = Convert.ToInt32(e.RowIndex);
                
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["dt_adItms"] = dt;

                    GVPay.DataSource = dt;
                    GVPay.DataBind();

                    //SetPreRowitm();

                    //TextBox TBChqDat = (TextBox)GVbkExp.Rows[0].Cells[0].FindControl("TBChqDat");
                    //TBChqDat.Text = DateTime.Now.ToString();
                }
            }

        }

        protected void btn_CashBook_Click(object sender, EventArgs e)
        {
            v_choice.Text = "";
            pnl_pay.Visible = false;
            Panel_incom.Visible = false;
            Panel_CashBook.Visible = true;

        }


        protected void TBOpeBal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (lbl_Openbalance.Text == "0" || lbl_Openbalance.Text == "")
                {
                    lbl_Openbalance.Text = TBOpeBal.Text.Trim();
                }else
                {
                    lbl_Openbalance.Text = (Convert.ToDecimal(lbl_Openbalance.Text.Trim()) + Convert.ToDecimal(TBOpeBal.Text.Trim())).ToString();
                    TBOpeBal.Text = lbl_Openbalance.Text;
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }
    }
}