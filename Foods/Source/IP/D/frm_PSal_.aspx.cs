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
    public partial class frm_PSal_ : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_ = null;
        DBConnection db = new DBConnection();
        string TBItms, tbItmpris, TBItmQty, lblcat, lblttl, HFDSal, tbbal, tbadv, DDL_PROTYPID, tbsalpris, tbfitpric; 

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
                        ptnSno();
                        lnkbtn_del.Visible = false;
                        lbl_dat.Text = DateTime.Now.ToShortDateString();
                        SetInitRowPuritm();
                        data();
                        lblmssg.Text = "";
                    }

                    catch { Response.Redirect("~/Login.aspx"); }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }

        }

        private void data()
        {

            dt_ = new DataTable();
            dt_ = DBConnection.GetQueryData("select * from tbl_producttype where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

            for (int i = 0; i < GV_POS.Rows.Count; i++)
                {
                    DropDownList DDL_Unt = (DropDownList)GV_POS.Rows[i].Cells[1].FindControl("DDL_PROTYPID");
                    DDL_Unt.DataSource = dt_;
                    DDL_Unt.DataTextField = "ProductTypeName";
                    DDL_Unt.DataValueField = "ProductTypeID";
                    DDL_Unt.DataBind();
                    DDL_Unt.Items.Insert(0, new ListItem("--Select Items Type --", "0"));
                }

                //DDL_PROTYPID.Items.Insert(0, new ListItem("--Select Report Type--", "0"));
        }
        private void SetInitRowPuritm()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Items", typeof(string)));
            dt.Columns.Add(new DataColumn("ProductID", typeof(string)));
            dt.Columns.Add(new DataColumn("PROTYPID", typeof(string)));
            dt.Columns.Add(new DataColumn("fitpric", typeof(string)));            
            dt.Columns.Add(new DataColumn("Itempric", typeof(string)));
            dt.Columns.Add(new DataColumn("salpric", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));
            dt.Columns.Add(new DataColumn("TTL", typeof(string)));
            dt.Columns.Add(new DataColumn("Dposid", typeof(string)));
            
            dr = dt.NewRow();

            dr["Items"] = string.Empty;
            dr["ProductID"] = string.Empty;
            dr["PROTYPID"] = string.Empty;
            dr["fitpric"] = "0.00";
            dr["Itempric"] = "0.00";
            dr["salpric"] = "0.00";
            dr["QTY"] = "0.00";
            dr["TTL"] = "0.00";
            dr["Dposid"] = string.Empty;
            
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
                        //extract the TextBox value
                        TextBox TBItms = (TextBox)GV_POS.Rows[rowIndex].Cells[0].FindControl("TBItms");
                        HiddenField HFPROID = (HiddenField)GV_POS.Rows[rowIndex].Cells[0].FindControl("PROID");
                        Label PROTYPID = (Label)GV_POS.Rows[rowIndex].Cells[1].FindControl("PROTYPID");
                        DropDownList DDL_PROTYPID = (DropDownList)GV_POS.Rows[rowIndex].Cells[1].FindControl("DDL_PROTYPID");
                        TextBox tbfitpric = (TextBox)GV_POS.Rows[rowIndex].Cells[2].FindControl("tbfitpric");                     
                        TextBox tbItmpris = (TextBox)GV_POS.Rows[rowIndex].Cells[3].FindControl("tbItmpris");
                        TextBox tbsalpris = (TextBox)GV_POS.Rows[rowIndex].Cells[4].FindControl("tbsalpris");
                        TextBox TBItmQty = (TextBox)GV_POS.Rows[rowIndex].Cells[5].FindControl("TBItmQty");
                        Label lblttl = (Label)GV_POS.Rows[rowIndex].Cells[6].FindControl("lblttl");
                        HiddenField HFDSal = (HiddenField)GV_POS.Rows[rowIndex].Cells[7].FindControl("HFDSal");

                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["Items"] = TBItms.Text;
                        dt.Rows[i - 1]["ProductID"] = HFPROID.Value;                        
                        dt.Rows[i - 1]["PROTYPID"] = DDL_PROTYPID.SelectedValue;
                        dt.Rows[i - 1]["fitpric"] = tbfitpric.Text;
                        dt.Rows[i - 1]["Itempric"] = tbItmpris.Text;
                        dt.Rows[i - 1]["salpric"] = tbsalpris.Text;
                        dt.Rows[i - 1]["QTY"] = TBItmQty.Text;
                        dt.Rows[i - 1]["TTL"] = lblttl.Text;
                        dt.Rows[i - 1]["Dposid"] = HFDSal.Value;

                        rowIndex++;

                        float GTotal = 0, CRAmt = 0, DBAmt = 0;
                        for (int j = 0; j < GV_POS.Rows.Count; j++)
                        {
                            Label total = (Label)GV_POS.Rows[j].FindControl("lblttl");
                            GTotal += Convert.ToSingle(total.Text);
                            TBTtl.Text = GTotal.ToString();
                        }
                    }

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
            try
            {
                for (int j = 0; j < GV_POS.Rows.Count; j++)
                {
                    TextBox TBItms = (TextBox)GV_POS.Rows[j].FindControl("TBItms");
                    TextBox TBItmQty = (TextBox)GV_POS.Rows[j].FindControl("TBItmQty");
                    TextBox tbsalpris = (TextBox)GV_POS.Rows[j].FindControl("tbsalpris");
                    Label lblttl = (Label)GV_POS.Rows[j].FindControl("lblttl");
                    Label lbl_Flag = (Label)GV_POS.Rows[j].FindControl("lbl_Flag");
                    LinkButton lnkbtnadd = (LinkButton)GV_POS.Rows[j].FindControl("lnkbtnadd");

                    if (TBItms.Text == "")
                    {
                        lbl_Flag.Text = "0";
                    }

                    lblttl.Text = (Convert.ToDouble(TBItmQty.Text.Trim()) * Convert.ToDouble(tbsalpris.Text.Trim())).ToString();
                    lnkbtnadd.Focus();
                }

                float GTotal = 0;
                for (int k = 0; k < GV_POS.Rows.Count; k++)
                {
                    Label lblttl = (Label)GV_POS.Rows[k].FindControl("lblttl");
                    string ttlamt = Convert.ToDouble(lblttl.Text).ToString();

                    GTotal += Convert.ToSingle(ttlamt);
                    TBTtl.Text = GTotal.ToString();
                }
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
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["dt_adItm"] = dt;

                    GV_POS.DataSource = dt;
                    GV_POS.DataBind();

                    SetPreRowitm();

                    float GTotal = 0;
                    for (int j = 0; j < GV_POS.Rows.Count; j++)
                    {
                        Label lblttl = (Label)GV_POS.Rows[j].FindControl("lblttl");

                        GTotal += Convert.ToSingle(lblttl.Text);
                    }

                    TBTtl.Text = GTotal.ToString();

                    ptnSno();
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

        protected void TBCust_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from Customers_ where CustomerName ='" + TBCust.Text.Trim() + "' and IsActive = 1 ";

                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt_ = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dt_);

                if (dt_.Rows.Count > 0)
                {
                    TBCust.Text = dt_.Rows[0]["CustomerName"].ToString();
                    TB_MobNo.Text = dt_.Rows[0]["CellNo1"].ToString();
                    //TBUpNo.Text = "";
                    //TBLwNo.Text = "";
                }
                else
                {
                    TBCust.Text = "";
                    TB_MobNo.Text = "";
                    //TBUpNo.Text = "";
                    //TBLwNo.Text = "";
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
                data();
                int rowIndex = 0;
                if (ViewState["dt_adItm"] != null)
                {
                    DataTable dt = (DataTable)ViewState["dt_adItm"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TextBox TBItms = (TextBox)GV_POS.Rows[rowIndex].Cells[0].FindControl("TBItms");
                            HiddenField HFPROID = (HiddenField)GV_POS.Rows[rowIndex].Cells[0].FindControl("PROID");
                            Label PROTYPID = (Label)GV_POS.Rows[rowIndex].Cells[1].FindControl("PROTYPID");
                            DropDownList DDL_PROTYPID = (DropDownList)GV_POS.Rows[rowIndex].Cells[1].FindControl("DDL_PROTYPID");
                            TextBox tbfitpric = (TextBox)GV_POS.Rows[rowIndex].Cells[2].FindControl("tbfitpric");
                            TextBox tbItmpris = (TextBox)GV_POS.Rows[rowIndex].Cells[3].FindControl("tbItmpris");
                            TextBox tbsalpris = (TextBox)GV_POS.Rows[rowIndex].Cells[4].FindControl("tbsalpris");
                            TextBox TBItmQty = (TextBox)GV_POS.Rows[rowIndex].Cells[5].FindControl("TBItmQty");
                            Label lblttl = (Label)GV_POS.Rows[rowIndex].Cells[6].FindControl("lblttl");
                            HiddenField HFDSal = (HiddenField)GV_POS.Rows[rowIndex].Cells[7].FindControl("HFDSal");
                            Label lbl_Flag = (Label)GV_POS.Rows[i].FindControl("lbl_Flag");

                            string Itms = dt.Rows[i]["Items"].ToString();

                            if (Itms != "")
                            {
                                TBItms.Text = dt.Rows[i]["Items"].ToString();
                            }
                            else
                            {
                                TBItms.Text = "";
                            }
                            string PROTYP = dt.Rows[i]["PROTYPID"].ToString();

                            if (PROTYP != "")
                            {
                                DDL_PROTYPID.SelectedValue = dt.Rows[i]["PROTYPID"].ToString();
                            }
                            else
                            {
                                DDL_PROTYPID.SelectedValue = "0";
                            }

                            string fitpric = dt.Rows[i]["fitpric"].ToString();

                            if (fitpric != "")
                            {
                                tbfitpric.Text = dt.Rows[i]["fitpric"].ToString();
                            }
                            else
                            {
                                tbfitpric.Text = "0.00";
                            }
                            string Itempric = dt.Rows[i]["Itempric"].ToString();

                            if (Itempric != "")
                            {
                                tbItmpris.Text = dt.Rows[i]["Itempric"].ToString();
                            }
                            else
                            {
                                tbItmpris.Text = "0.00";
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
                                TBItmQty.Text = "0.00";
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

                            HFDSal.Value = dt.Rows[i]["Dposid"].ToString();
                            HFPROID.Value = dt.Rows[i]["ProductID"].ToString();


                            if (TBItms.Text == "")
                            {
                                lbl_Flag.Text = "0";
                            }
                            else
                            {
                                lbl_Flag.Text = "1";
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

        private void ptnSno()
        {
            try
            {
                //string str = "select isnull(max(cast(MSal_id as int)),0) as [MSal_id]  from tbl_MSal";
                string str = " select isnull(max(cast(Mposid as int)),0) as [Mposid]  from tbl_MPos where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(lbl_BillNo.Text))
                    {
                        int v = Convert.ToInt32(reader["Mposid"].ToString());
                        int b = v + 1;
                        lbl_BillNo.Text = "SAL00" + b.ToString();
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
                //lbl_err.Text = ex.Message.ToString();
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
            //try
            //{
            //    for (int j = 0; j < GV_POS.Rows.Count; j++)
            //    {

            //        TextBox TBItms = (TextBox)GV_POS.Rows[j].FindControl("TBItms");
            //        TextBox TBItmQty = (TextBox)GV_POS.Rows[j].FindControl("TBItmQty");
            //        Label tbItmpris = (Label)GV_POS.Rows[j].FindControl("tbItmpris");
            //        HiddenField PROID = (HiddenField)GV_POS.Rows[j].FindControl("PROID");
            //        HiddenField PROCATID = (HiddenField)GV_POS.Rows[j].FindControl("PROCATID");

            //        Label lbl_Flag = (Label)GV_POS.Rows[j].FindControl("lbl_Flag");


            //        string query = " select Products.ProductID,Products.ProductTypeID, cost as [Itempric],ProductTypeName as [Itemcat] from Products " +
            //            " inner join tbl_producttype on Products.ProductTypeID = tbl_producttype.ProductTypeID " +
            //            " where ProductName =  '" + TBItms.Text.Trim() + "'";

            //        SqlCommand cmd = new SqlCommand(query, con);
            //        DataTable dt_ = new DataTable();
            //        SqlDataAdapter adp = new SqlDataAdapter(cmd);

            //        adp.Fill(dt_);

            //        if (dt_.Rows.Count > 0)
            //        {
            //            tbItmpris.Text = dt_.Rows[0]["Itempric"].ToString();
            //            PROID.Value = dt_.Rows[0]["ProductID"].ToString();
            //            lbl_Flag.Text = "1";
            //            TBItmQty.Focus();
            //            if (lbl_Flag.Text == "0")
            //            {
            //                //TBItmQty.Text = dt_.Rows[0]["Qty"].ToString();
            //            }
            //        }
            //        else
            //        {
            //            tbItmpris.Text = "0.00";
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
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
                string bal = (Convert.ToInt32(TBTtl.Text) - Convert.ToInt32(TBAdvance.Text.Trim())).ToString();

                TBBalance.Text = bal;
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        protected void TB_MobNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from Customers_ where CellNo1 ='" + TB_MobNo.Text.Trim() + "' and IsActive = 1 ";

                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt_ = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dt_);

                if (dt_.Rows.Count > 0)
                {
                    TBCust.Text = dt_.Rows[0]["CustomerName"].ToString();
                    TB_MobNo.Text = dt_.Rows[0]["CellNo1"].ToString();
                    HFMobNo.Value = dt_.Rows[0]["CellNo1"].ToString();
                    ckh_right.Checked = Convert.ToBoolean(dt_.Rows[0]["right_eye"].ToString());
                    chk_left.Checked = Convert.ToBoolean(dt_.Rows[0]["left_eye"].ToString());
                    TBRSph.Text = dt_.Rows[0]["RSph"].ToString();
                    TBRCyl.Text = dt_.Rows[0]["RCyl"].ToString();
                    TBRAxis.Text = dt_.Rows[0]["RAxis"].ToString();

                    TBRAdd_.Text = dt_.Rows[0]["RAdd"].ToString();
                    
                    TBLSph.Text = dt_.Rows[0]["LSph"].ToString();
                    TBLCyl.Text = dt_.Rows[0]["LCyl"].ToString();
                    TBLAxis.Text = dt_.Rows[0]["LAsix"].ToString();

                    TBLAdd_.Text = dt_.Rows[0]["LAdd"].ToString();


                }
                else
                {
                    TBCust.Text = "";
                    TB_MobNo.Text = "";
                    HFMobNo.Value = "";
                    ckh_right.Checked = false;
                    chk_left.Checked = false;
                    TBRSph.Text = "";
                    TBRCyl.Text = "";
                    TBRAxis.Text = "";
                    TBLSph.Text = "";
                    TBLCyl.Text = "";
                    TBLAxis.Text = "";
                    HFMobNo.Value = "";
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }

        }

        private void Update()
        {

           string querys = " Update Customers_ set CustomerName='" + TB_CustNam.Text + "', GST=''" +
                " , category = '', NTN='', customertype_= '',areaid='', Area=''" +
                " , saleper ='', District='', PhoneNo = ''" +
                " , Email='', CellNo1='" + TBMobNo.Text + "', PostalCode='', CellNo2='', PostalOfficeContact='', CellNo3='', NIC=''" +
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
            if (Page.IsValid)
            {
                try
                {
                    Save();
                }
                catch (Exception ex)
                {
                    lblmssg.Text = ex.Message;
                }
            }
        }

        protected void Btn_Cancl_Click(object sender, EventArgs e)
        {   
            Response.Redirect("frm_PSal.aspx");
        }

        protected void btn_prtbil_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_POSReceipt.aspx?ID=POS&POSID=" + lbl_BillNo.Text.Trim() + "','_blank','height=600px,width=400px,scrollbars=1');", true);
        }

        private void Save()
        {
            string MSalId = "";
            string HFPROID, HFPROCATID = "";
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
                //Master POS
                string query = " INSERT INTO tbl_MPos ([BillNO],[CompanyId],[BranchId],[Username],[billdat],[billtim],[CustomerName],[CellNo1],[left_eye], [right_eye],[RSph],[RCyl],[RAxis],[RAdd],[LSph],[LCyl],[LAsix],[LAdd],[custtyp] " +
                   " ,[createdby] ,[createdat] ,[createterminal] ,[updatedby] ,[updatedat] ,[updateterminal]) VALUES " +
                   " ('" + lbl_BillNo.Text + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "','" + Session["Username"] + "'" +
                   " ,'" + lbl_dat.Text + "','" + txtClock.Value + "','" + TBCust.Text + "','" + HFMobNo.Value + "','" + CHK_LeftEye.Checked + "','" + CHK_RightEye.Checked + "','" + TBRSphl.Text + "','" + TBRCyln.Text + "','" + TBRAXSIS.Text + "','" + TBRADD.Text + "','" + TBLSphl.Text + "','" + TBLCyln.Text + "','" + TBLAXSIS.Text + "','" + TBLADD.Text + "','','" + Session["user"].ToString() + "','" + DateTime.Today.ToString("MM/dd/yyyy") +
                   "','::1' ,'" + Session["user"].ToString() + "' ,'" + DateTime.Today.ToString("MM/dd/yyyy") + "' ,'::1') ";

                command.CommandText = query;
                command.ExecuteNonQuery();

                // Master Purchase " + TBSalDat.Text.Trim() + " , " + DateTime.Today + "
                command.CommandText = "select Mposid from tbl_MPos where BillNO = '" + lbl_BillNo.Text.Trim() + "'";

                SqlDataAdapter adp = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    MSalId = dt.Rows[0]["Mposid"].ToString();
                }

                //Detail Sales

                foreach (GridViewRow g1 in GV_POS.Rows)
                {

                    TBItms = (g1.FindControl("TBItms") as TextBox).Text;
                    tbfitpric = (g1.FindControl("tbfitpric") as TextBox).Text;
                    tbItmpris = (g1.FindControl("tbItmpris") as TextBox).Text;
                    tbsalpris = (g1.FindControl("tbsalpris") as TextBox).Text;
                    DDL_PROTYPID = (g1.FindControl("DDL_PROTYPID") as DropDownList).SelectedValue;
                    TBItmQty = (g1.FindControl("TBItmQty") as TextBox).Text;
                    lblttl = (g1.FindControl("lblttl") as Label).Text;
                    HFDSal = (g1.FindControl("HFDSal") as HiddenField).Value;
                    HFPROID = (g1.FindControl("PROID") as HiddenField).Value;

                    tbItmpris = (Convert.ToInt32(tbfitpric) + Convert.ToInt32(tbItmpris)).ToString();

                    command.CommandText =
                        " INSERT INTO [dbo].[tbl_DPos] ([BillNO] ,[Mposid] ,[ProductID] ,[ProQty] ,[purprice], [salprice], [ProductTypeID] ,[Adv], [bal], [Ttl] " +
                        " ,[grntttl] ,[createdby] ,[createdat] ,[createterminal] ,[updatedby] ,[updatedat] ,[updateterminal]) " +
                        " VALUES ('" + lbl_BillNo.Text + "'" +
                        " ,'" + MSalId + "' ,'" + TBItms + "','" + TBItmQty + "','" + tbItmpris + "','" + tbsalpris + "','" + DDL_PROTYPID + "','" + TBAdvance.Text + "','" + TBBalance.Text + "','" + lblttl + "'" +
                        " ,'" + TBTtl.Text + "' ,'" + Session["user"].ToString() + "' ,getdate()" +
                        " ,'" + Session["BranchID"] + "' ,'" + Session["user"].ToString() + "' ,getdate()" +
                        " ,'" + Session["BranchID"] + "') ";

                    command.ExecuteNonQuery();
                }

                // Attempt to commit the transaction.
                transaction.Commit();



                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_POSReceipt.aspx?ID=POS&POSID=" + lbl_BillNo.Text.Trim() + "','_blank','height=600px,width=400px,scrollbars=1');", true);

            }
            catch (Exception ex)
            {
                lblmssg.Text = "Commit Exception Type: {0}"+ ex.GetType();
                lblmssg.Text = "Message: {0}"+ ex.Message;

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
                    lblmssg.Text = "Message: {0}"+ ex2.Message;
                }
            }
            finally
            {
                con.Close();
                //Response.Redirect("frm_PSal.aspx");
                //clear();
            }
        }

        private int SaveCust()
        {
            int j = 1;


            string query = " INSERT INTO [dbo].[Customers_] ([CustomerName] ,[GST] ,[category] ,[NTN] ,[customertype_],[areaid] " +
                    " ,[Area] ,[saleper] ,[District] ,[PhoneNo] ,[Email],[CellNo1],[PostalCode],[CellNo2] " +
                    " ,[PostalOfficeContact],[CellNo3] ,[NIC] ,[CellNo4],[city_],[Address] ,[CreatedBy] " +
                    " ,[CreatedDate] ,[IsActive] ,[Cus_Code] ,[CompanyId] ,[BranchId],[left_eye], [right_eye],[RSph],[RCyl],[RAxis],[RAdd],[LSph],[LCyl],[LAsix],[LAdd])  VALUES " +
                    " ('" + TB_CustNam.Text + "' ,'','', '' " +
                    " ,'' ,'','','' ,'',''" +
                    " ,'','" + TBMobNo.Text + "',''" +
                    " ,'','',''" +
                    " ,'','',''" +
                    " ,'','" + Session["user"].ToString() + "','" + DateTime.Now + "'" +
                    " ,'true' ,'','" + Session["CompanyID"] + "','" + Session["BranchID"] + "','" + CHK_LeftEye.Checked + "','" + CHK_RightEye.Checked + "','" + TBRSphl.Text + "','" + TBRCyln.Text + "','" + TBRAXSIS.Text + "','" + TBRADD.Text + "','" + TBLSphl.Text + "','" + TBLCyln.Text + "','" + TBLAXSIS.Text + "','" + TBLADD.Text + "')";

            con.Open();


            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                cmd.ExecuteNonQuery();

            }
            con.Close();

            return j;
        }
        
        private int UpdCust()
        {
            int j = 1;


            string query = " UPDATE Customers_ SET CustomerName ='" + TB_CustNam.Text + "' , CellNo1='" + TBMobNo.Text + "' ,CreatedBy='' , CreatedDate ='' ,left_eye='" + CHK_LeftEye.Checked + "', right_eye='" + CHK_RightEye.Checked + "',RSph='" + TBRSphl.Text + "',RCyl='" + TBRCyln.Text + "',RAxis='" + TBRAXSIS.Text + "',LSph='" + TBLSphl.Text + "',LCyl='" + TBLCyln.Text + "',LAsix='" + TBLAXSIS.Text + "' where CustomerID = '" + HFCustID.Value.Trim() + "'";

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


            string query = " Delete from Customers_ where CellNo1 ='" + TBMobNo.Text.Trim() +"'";

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
            if (Page.IsValid)
            {
                try
                {
                    if (HFCustID.Value == "")
                    {
                        SaveCust();
                    }
                    else
                    {
                        UpdCust();
                    }
                    Response.Redirect("frm_PSal.aspx");
                }
                catch (Exception ex)
                {
                    lblerr.Text = ex.Message;
                }
            }
        }


        public void cusclear()
        {
            TB_CustNam.Text = "";
            TBMobNo.Text = "";            
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
       
        public void clear()
        {
            SetInitRowPuritm();
            TBAdvance.Text = "0.00";
            TBBalance.Text = "0.00";
            TBTtl.Text = "0.00";
            TBCust.Text = "";
            TB_MobNo.Text = "";
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
            lblmssg.Text = "";
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

        protected void TBMobNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "select * from Customers_ where CellNo1 ='" + TBMobNo.Text.Trim() + "'";

                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt_ = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dt_);

                if (dt_.Rows.Count > 0)
                {
                    ModalPopupExtender1.Show();
                    TB_CustNam.Text = dt_.Rows[0]["CustomerName"].ToString();
                    TBMobNo.Text = dt_.Rows[0]["CellNo1"].ToString();
                    CHK_RightEye.Checked = Convert.ToBoolean(dt_.Rows[0]["right_eye"].ToString());
                    CHK_LeftEye.Checked = Convert.ToBoolean(dt_.Rows[0]["left_eye"].ToString());
                    TBRSphl.Text = dt_.Rows[0]["RSph"].ToString();
                    TBRCyln.Text = dt_.Rows[0]["RCyl"].ToString();
                    TBRAXSIS.Text = dt_.Rows[0]["RAxis"].ToString();
                    TBRADD.Text = dt_.Rows[0]["RAdd"].ToString();
                    TBLSphl.Text = dt_.Rows[0]["LSph"].ToString();
                    TBLCyln.Text = dt_.Rows[0]["LCyl"].ToString();
                    TBLAXSIS.Text = dt_.Rows[0]["LAsix"].ToString();
                    TBRADD.Text = dt_.Rows[0]["LAdd"].ToString();
                    HFMobNo.Value = dt_.Rows[0]["CellNo1"].ToString();
                    HFCustID.Value = dt_.Rows[0]["CustomerID"].ToString();
                    lnkbtn_del.Visible = true;
                }
                else
                {
                    ModalPopupExtender1.Show();
                    TBCust.Text = "";
                    TB_MobNo.Text = "";
                    CHK_RightEye.Checked = false;
                    CHK_LeftEye.Checked = false;
                    TBRSphl.Text = "";
                    TBRCyln.Text = "";
                    TBRAXSIS.Text = "";
                    TBLSphl.Text = "";
                    TBLCyln.Text = "";
                    TBLAXSIS.Text = "";
                    HFMobNo.Value = "";
                    HFCustID.Value = "";
                    lnkbtn_del.Visible = false;

                }

            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
            }
        }

        protected void GV_POS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                data();
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }
    }
}