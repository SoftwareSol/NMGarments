using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Cfg;
using Foods;
using System.Globalization;
using DataAccess;


namespace Foods
{
    public partial class frm_Purchase_ : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        string MPurId, proid, PurItm, Mstk_id,Purrat, HFDPur, Mvch_id, MSTkId, TBcat, TbPacksiz, Tbqty, stkqty, Tbunt, Tbamt, Tbbrnd, Tborgn, TbItmDscptin, query, PurItmQty, TB_rat, PurRate, sSalRat, Particulars, PurNetTtl, str;
        DataTable dt_ = new DataTable();
        double totalprev;
        decimal resul;
        SqlTransaction tran;
        DBConnection c = new DBConnection();
        Global_ g = new Global_();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                

                FillGrid();
                SetInitRowPuritm();
                ptnSno();
                TBPONum.Enabled = false;
                GVPurItems.Visible = true;
                PanelShowClosed.Visible = false;
                TBPurDat.Text = DateTime.Now.ToShortDateString();
                ddlVenNam.Enabled = true;
                chk_Act.Checked = true;
                chk_prtd.Checked = true;
                ddlVenNam.Focus();
                BindDll();
                
                //pnl_bnk.Visible = false;
                //pnl_chqamt.Visible = false;
                //pnl_Chqno.Visible = false;
                //pnl_cshamt.Visible = false;
            }
        }


        private void ptnSno()
        {
            try
            {

                str = "select isnull(max(cast(MPurID as int)),0) as [MPurID]  from MPurchase  where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(TBPONum.Text))
                    {
                        int v = Convert.ToInt32(reader["MPurID"].ToString());
                        int b =  v + 1;
                        TBPONum.Text = Session["CompanyID"] + b.ToString();
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }


        protected void DDL_Currency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string query = "";

                query = " select * from tbl_Curr where IsActive = 1 and Curr_id= '" + DDL_Currency.SelectedValue.Trim() + "'";

                DataTable dt_ = new DataTable();

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    TBCurrRat.Text = dt_.Rows[0]["Currency_Rate"].ToString();
                    TBExchgRat.Text = dt_.Rows[0]["Exchange_Rat"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }

        }

        protected void btn_CurrSav_Click(object sender, EventArgs e)
        {
            try
            {
                query = " Update tbl_Curr set Currency_Rate='" + TBCurrRat.Text.Trim() + "' , Exchange_Rat='" + TBExchgRat.Text.Trim() + "' where Curr_id ='" + DDL_Currency.SelectedValue.Trim() + "'";

                con.Open();


                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
            finally
            {
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "alert('Currency Rate and Exchange Rate has been Updated!');", true);
            }

        }
        #region Web Methods

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetPO(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select VoucherNo  from tbl_MPO where VoucherNo like '" + prefixText + "%'";
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
        public static List<string> GetCat(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select ProductTypeName from tbl_producttype where ProductTypeName like '" + prefixText + "%'";
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
            string str = "select ProductName from Products where ProductType ='" + protyp + "' and ProductName like '" + prefixText + "%'";
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
        public static List<string> GetBrnd(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select brndnam from tbl_brnd where brndnam like '" + prefixText + "%'";
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
        public static List<string> Getorign(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select orignnam from tbl_orign where orignnam like '" + prefixText + "%'";
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
        public static List<string> Getpakgsiz(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select pakgsiz from tbl_pakgsiz where pakgsiz like '" + prefixText + "%'";
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
        public static List<string> Getunts(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select untnam from tbl_unts where untnam like '" + prefixText + "%'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }

        #endregion



        private void SetInitRowPuritm()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Category", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Brand", typeof(string)));
            dt.Columns.Add(new DataColumn("Origin", typeof(string)));
            dt.Columns.Add(new DataColumn("PackingSize", typeof(string)));
            dt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Purchase Rate", typeof(string)));
            dt.Columns.Add(new DataColumn("Sale Rate", typeof(string)));
            dt.Columns.Add(new DataColumn("Particulars", typeof(string)));
            dt.Columns.Add(new DataColumn("Debit Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Credit Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("NetTotal", typeof(string)));
            dt.Columns.Add(new DataColumn("DPurID", typeof(string)));

            dr = dt.NewRow();

            dr["Category"] = string.Empty;
            dr["Description"] = string.Empty;
            dr["Brand"] = "";
            dr["Origin"] = "";
            dr["PackingSize"] = "";
            dr["Rate"] = "0.00";
            dr["Qty"] = "0.00";
            dr["Unit"] = "";
            dr["Amount"] = "0.00";
            dr["Purchase Rate"] = "0.00";
            dr["Sale Rate"] = "0.00";
            dr["Particulars"] = string.Empty;
            dr["Debit Amount"] = "0.00";
            dr["Credit Amount"] = "0.00";
            dr["NetTotal"] = "0.00";
            dr["DPurID"] = "0";

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["dt_adItm"] = dt;

            GVPurItems.DataSource = dt;
            GVPurItems.DataBind();
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
                        TextBox TBcat = (TextBox)GVPurItems.Rows[rowIndex].Cells[0].FindControl("TBcat");
                        TextBox TbItmDscptin = (TextBox)GVPurItems.Rows[rowIndex].Cells[1].FindControl("TbItmDscptin");
                        TextBox Tbbrnd = (TextBox)GVPurItems.Rows[rowIndex].Cells[2].FindControl("Tbbrnd");
                        TextBox Tborgn = (TextBox)GVPurItems.Rows[rowIndex].Cells[3].FindControl("Tborgn");
                        TextBox TbPacksiz = (TextBox)GVPurItems.Rows[rowIndex].Cells[4].FindControl("TbPacksiz");
                        TextBox TB_rat = (TextBox)GVPurItems.Rows[rowIndex].Cells[5].FindControl("TB_rat");
                        TextBox Tbqty = (TextBox)GVPurItems.Rows[rowIndex].Cells[6].FindControl("Tbqty");
                        TextBox Tbunt = (TextBox)GVPurItems.Rows[rowIndex].Cells[5].FindControl("Tbunt");
                        TextBox Tbamt = (TextBox)GVPurItems.Rows[rowIndex].Cells[5].FindControl("Tbamt");
                        HiddenField HFDPur = (HiddenField)GVPurItems.Rows[rowIndex].Cells[5].FindControl("HFDPur");

                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["Category"] = TBcat.Text;
                        dt.Rows[i - 1]["Description"] = TbItmDscptin.Text;
                        dt.Rows[i - 1]["Brand"] = Tbbrnd.Text;
                        dt.Rows[i - 1]["Origin"] = Tborgn.Text;
                        dt.Rows[i - 1]["PackingSize"] = TbPacksiz.Text;
                        dt.Rows[i - 1]["Rate"] = TB_rat.Text;
                        dt.Rows[i - 1]["Qty"] = Tbqty.Text;
                        dt.Rows[i - 1]["Unit"] = Tbunt.Text;
                        dt.Rows[i - 1]["Amount"] = Tbamt.Text;
                        dt.Rows[i - 1]["DPurID"] = HFDPur.Value;

                        rowIndex++;
                    }

                    dt.Rows.Add(drRow);
                    ViewState["dt_adItm"] = dt;

                    GVPurItems.DataSource = dt;
                    GVPurItems.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreRowitm();
        }


        private void SetPreRowitm()
        {
            try
            {
                //BindDll();

                int rowIndex = 0;
                if (ViewState["dt_adItm"] != null)
                {
                    DataTable dt = (DataTable)ViewState["dt_adItm"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            TextBox TBcat = (TextBox)GVPurItems.Rows[rowIndex].Cells[0].FindControl("TBcat");
                            TextBox TbItmDscptin = (TextBox)GVPurItems.Rows[rowIndex].Cells[1].FindControl("TbItmDscptin");
                            TextBox Tbbrnd = (TextBox)GVPurItems.Rows[rowIndex].Cells[2].FindControl("Tbbrnd");
                            TextBox Tborgn = (TextBox)GVPurItems.Rows[rowIndex].Cells[3].FindControl("Tborgn");
                            TextBox TbPacksiz = (TextBox)GVPurItems.Rows[rowIndex].Cells[4].FindControl("TbPacksiz");
                            TextBox TB_rat = (TextBox)GVPurItems.Rows[rowIndex].Cells[5].FindControl("TB_rat");
                            TextBox Tbqty = (TextBox)GVPurItems.Rows[rowIndex].Cells[6].FindControl("Tbqty");
                            TextBox Tbunt = (TextBox)GVPurItems.Rows[rowIndex].Cells[5].FindControl("Tbunt");
                            TextBox Tbamt = (TextBox)GVPurItems.Rows[rowIndex].Cells[5].FindControl("Tbamt");
                            HiddenField HFDPur = (HiddenField)GVPurItems.Rows[rowIndex].Cells[5].FindControl("HFDPur");
                            Label lbl_Flag = (Label)GVPurItems.Rows[i].FindControl("lbl_Flag");



                            string cat = dt.Rows[i]["Category"].ToString();

                            if (cat != "")
                            {
                                TBcat.Text = dt.Rows[i]["Category"].ToString();
                            }
                            else
                            {
                                TBcat.Text = "";
                            }

                            string desc = dt.Rows[i]["Description"].ToString();

                            if (desc != "")
                            {
                                TbItmDscptin.Text = dt.Rows[i]["Description"].ToString();
                            }
                            else
                            {
                                TbItmDscptin.Text = "";
                            }

                            string brnd = dt.Rows[i]["Brand"].ToString();

                            if (brnd != "")
                            {
                                Tbbrnd.Text = dt.Rows[i]["Brand"].ToString();
                            }
                            else
                            {
                                Tbbrnd.Text = "";
                            }

                            string orgn = dt.Rows[i]["Origin"].ToString();

                            if (orgn != "")
                            {
                                Tborgn.Text = dt.Rows[i]["Origin"].ToString();
                            }
                            else
                            {
                                Tborgn.Text = "";
                            }


                            string packsiz = dt.Rows[i]["PackingSize"].ToString();

                            if (packsiz != "")
                            {
                                TbPacksiz.Text = dt.Rows[i]["PackingSize"].ToString();
                            }
                            else
                            {
                                TbPacksiz.Text = "";
                            }

                            string rat = dt.Rows[i]["Rate"].ToString();

                            if (rat != "")
                            {
                                TB_rat.Text = dt.Rows[i]["Rate"].ToString();
                            }
                            else
                            {
                                TB_rat.Text = "0.00";
                            }

                            string qty = dt.Rows[i]["Qty"].ToString();

                            if (qty != "")
                            {
                                Tbqty.Text = dt.Rows[i]["Qty"].ToString();
                            }
                            else
                            {
                                Tbqty.Text = "0.00";
                            }

                            string amt = dt.Rows[i]["Amount"].ToString();

                            if (amt != "")
                            {
                                Tbamt.Text = dt.Rows[i]["Amount"].ToString();
                            }
                            else
                            {
                                Tbamt.Text = "0.00";
                            }


                            HFDPur.Value = dt.Rows[i]["DPurID"].ToString();

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


        protected void TbItmDscptin_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < GVPurItems.Rows.Count; i++)
                {
                    TextBox TBcat = (TextBox)GVPurItems.Rows[i].Cells[0].FindControl("TBcat");
                    TextBox TbItmDscptin = (TextBox)GVPurItems.Rows[i].Cells[1].FindControl("TbItmDscptin");
                    TextBox TB_rat = (TextBox)GVPurItems.Rows[i].Cells[7].FindControl("TB_rat");


                    query = " select ProductName as [ITEMDESC],Brand as [BRAND],Origin as [ORIGIN], " +
                        " Packing as [PAKGSIZ],Unit as [UNITS], TradPrice,WholSalprice,RetalPrice as [RATE], " +
                        " Pro_Rmks, '' as [SUPRAT], '' as [SUPNAM] from products where  ProductName = '" + TbItmDscptin.Text.Trim() + "'";

                    DataTable dt_ = new DataTable();

                    dt_ = DBConnection.GetQueryData(query);

                    if (dt_.Rows.Count > 0)
                    {
                        TbItmDscptin.Text = dt_.Rows[0]["ITEMDESC"].ToString();
                        TB_rat.Text= dt_.Rows[0]["RATE"].ToString();
                    }
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


        protected void TBcat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                for (int i = 0; i < GVPurItems.Rows.Count; i++)
                {
                    TextBox TBcat = (TextBox)GVPurItems.Rows[i].Cells[0].FindControl("TBcat");

                    query = "select * from tbl_producttype where ProductTypeName='" + TBcat.Text.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                    dt_ = DBConnection.GetQueryData(query);

                    if (dt_.Rows.Count > 0)
                    {
                        //Do Noting
                    }
                    else
                    {
                        query = " select top 1 ProductTypeID as [ProductTypeID]  from tbl_producttype where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' order by ProductTypeID desc ";

                        dt_ = DBConnection.GetQueryData(query);

                        string ProductTypeID = dt_.Rows[0]["ProductTypeID"].ToString();

                        string procatid = RandomNumber().ToString();

                        query = " INSERT INTO tbl_producttype " +
                                        " ([ProductTypeID],[ProductTypeName],[CreateBy],[CreatedAt],[IsActive],[CompanyId],[BranchId]) VALUES('" + procatid + "','" + TBcat.Text.Trim() + "','" + Session["user"].ToString() +
                                        " ','" + DateTime.Now + "','true','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                        con.Open();

                        using (SqlCommand cmd = new SqlCommand(query, con))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        con.Close();
                    }

                    Session["cat"] = TBcat.Text.Trim();
                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }
        protected void linkbtnadd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }


        public void forDetalItm()
        {
            using (SqlCommand cmdpro = new SqlCommand())
            {
                cmdpro.CommandText = " select rtrim('[' + CAST(ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], ProductID  from Products where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                cmdpro.Connection = con;
                con.Open();

                DataTable dtSupNam = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                adp.Fill(dtSupNam);

                for (int o = 0; o < GVPurItems.Rows.Count; o++)
                {
                    DropDownList ddlitem = (DropDownList)GVPurItems.Rows[o].FindControl("ddlPurItm");

                    ddlitem.DataSource = dtSupNam;
                    ddlitem.DataTextField = "ProductName";
                    ddlitem.DataValueField = "ProductID";
                    ddlitem.DataBind();
                    ddlitem.Items.Insert(0, new ListItem("--Select--", "0"));

                }

                con.Close();
            }
        }

        public void forDetalPract()
        {
            //For Particulars

            using (SqlCommand cmdpart = new SqlCommand())
            {
                cmdpart.CommandText = " select subheadcategoriesID as [AccID] ,rtrim('[' + CAST(subheadcategoriesID AS VARCHAR(200)) + ']-' + subheadcategoriesName ) as [AccName]  from subheadcategories ";

                cmdpart.Connection = con;
                //con.Open();

                DataTable dtpart = new DataTable();
                SqlDataAdapter adppart = new SqlDataAdapter(cmdpart);
                adppart.Fill(dtpart);

                for (int k = 0; k < GVPurItems.Rows.Count; k++)
                {
                    DropDownList ddl_Praci = (DropDownList)GVPurItems.Rows[k].FindControl("ddl_Prac");

                    ddl_Praci.DataSource = dtpart;
                    ddl_Praci.DataTextField = "AccName";
                    ddl_Praci.DataValueField = "AccID";
                    ddl_Praci.DataBind();
                    ddl_Praci.Items.Insert(0, new ListItem("--Select--", "0"));

                }
                con.Close();
            }

        }
        public void FillGrid()
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select MPurID, suppliername,VndrAdd,VndrCntct,PurNo,MPurDate,supplier.CNIC,NTNNo, " +
                        " MPurchase.CreatedBy,MPurchase.CreatedAt,ToBePrntd from MPurchase  inner join " +
                        " supplier on MPurchase.ven_id = supplier.supplierId and MPurchase.CompanyId = '" + Session["CompanyID"] + "' and MPurchase.BranchId= '" + Session["BranchID"] + "' order by MPurID desc";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dt_ = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dt_);

                    GVScrhMPur.DataSource = dt_;
                    GVScrhMPur.DataBind();

                    ViewState["MPur"] = dt_;

                    con.Close();
                }


            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }

        }

        public void BindDll()
        {
            try
            {
                //For Requisition

                //using (SqlCommand cmdReq = new SqlCommand())
                //{
                //    cmdReq.CommandText = " select rtrim('[' + CAST(MReq_sono AS VARCHAR(200)) + ']-' + Depart_nam) as [MReq], MReq_id from tbl_MReq  inner join tbl_Depart on tbl_MReq.Depart_id = tbl_Depart.Depart_id";

                //    cmdReq.Connection = con;
                //    con.Open();

                //    DataTable dtreq = new DataTable();
                //    SqlDataAdapter adp = new SqlDataAdapter(cmdReq);
                //    adp.Fill(dtreq);

                //    DDl_Req.DataSource = dtreq;
                //    DDl_Req.Items.Add(new ListItem("--Select--", "0"));
                //    DDl_Req.DataTextField = "MReq";
                //    DDl_Req.DataValueField = "MReq_id";
                //    DDl_Req.DataBind();


                //    con.Close();
                //}

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


                    con.Close();
                }



                //Payment Type
                //using (SqlCommand cmd = new SqlCommand())
                //{
                //    //cmd.CommandText = " select rtrim('[' + CAST(PayTyp_id AS VARCHAR(200)) + ']-' + PayTyp_nam ) as [PayTyp_nam], PayTyp_id  from tbl_PayTyp where ISActive = 'True'";
                //    cmd.CommandText = " select distinct rtrim('[' + CAST(tbl_PayTyp.PayTyp_id AS VARCHAR(200)) + ']-' + PayTyp_nam ) as [PayTyp_nam], vchtyp_id,tbl_PayTyp.PayTyp_id  from tbl_PayTyp " +
                //                       " inner join tbl_vchtyp on tbl_PayTyp.PayTyp_id= tbl_vchtyp.PayTyp_id " +
                //                       "  where tbl_PayTyp.ISActive = 'True'  and tbl_vchtyp.vchtyp_id = '" + DDL_Vchtyp.SelectedValue.Trim() + "'";

                //    cmd.Connection = con;
                //    con.Open();

                //    DataTable dtPaytyp = new DataTable();
                //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //    adp.Fill(dtPaytyp);

                //    DDL_Paytyp.DataSource = dtPaytyp;
                //    DDL_Paytyp.DataTextField = "PayTyp_nam";
                //    DDL_Paytyp.DataValueField = "PayTyp_id";
                //    DDL_Paytyp.DataBind();
                //    DDL_Paytyp.Items.Insert(0, new ListItem("--Select--", "0"));

                //    con.Close();
                //}

                // For Supplier

                using (SqlCommand cmdSupNam = new SqlCommand())
                {
                    //cmd.CommandText = " select rtrim('[' + CAST(ven_id AS VARCHAR(200)) + ']-' + ven_nam ) as [ven_nam], ven_id from t_ven";
                    //cmdSupNam.CommandText = "select subheadcategoryfiveID,subheadcategoryfiveName from subheadcategoryfive where subheadcategoryfourGeneratedID = 'MB000004' and isflag = 1";
                    cmdSupNam.CommandText = "select rtrim('[' + CAST(supplierId AS VARCHAR(200)) + ']-' + suppliername ) as [suppliername], supplierId from supplier where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                    cmdSupNam.Connection = con;
                    con.Open();

                    DataTable dtSupNam = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdSupNam);
                    adp.Fill(dtSupNam);

                    ddlVenNam.DataSource = dtSupNam;
                    ddlVenNam.DataTextField = "suppliername";
                    ddlVenNam.DataValueField = "supplierId";
                    ddlVenNam.DataBind();
                    ddlVenNam.Items.Insert(0, new ListItem("--Select--", "0"));


                    con.Close();
                }

                //For Bank

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select rtrim('[' + CAST(CashBnk_id AS VARCHAR(200)) + ']-' + CashBnk_nam ) as [CashBnk_nam], CashBnk_id  from tbl_CashBnk where ISActive = 'True'";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtbnk = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtbnk);

                    DDL_Bnk.DataSource = dtbnk;
                    DDL_Bnk.DataTextField = "CashBnk_nam";
                    DDL_Bnk.DataValueField = "CashBnk_id";
                    DDL_Bnk.DataBind();
                    DDL_Bnk.Items.Insert(0, new ListItem("--Select--", "0"));

                    con.Close();
                }


                query = " select * from tbl_Curr where IsActive = 1 ";

                DataTable dt_Curr = new DataTable();

                dt_Curr = DBConnection.GetQueryData(query);

                if (dt_Curr.Rows.Count > 0)
                {
                    DDL_Currency.DataSource = dt_Curr;
                    DDL_Currency.DataTextField = "Curr_nam";
                    DDL_Currency.DataValueField = "Curr_id";
                    DDL_Currency.DataBind();
                    DDL_Currency.Items.Insert(0, new ListItem("--Select Currency--", "0"));
                }
 


                // For Customer

                //using (SqlCommand cmd = new SqlCommand())
                //{
                //    cmd.CommandText = " select rtrim('[' + CAST(CustomerID AS VARCHAR(200)) + ']-' + CustomerName ) as [CustomerName], CustomerID from Customers_";

                //    cmd.Connection = con;
                //    con.Open();

                //    DataTable dtCust = new DataTable();
                //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //    adp.Fill(dtCust);

                //    DDL_Cust.DataSource = dtCust;
                //    DDL_Cust.DataTextField = "CustomerName";
                //    DDL_Cust.DataValueField = "CustomerID";
                //    DDL_Cust.DataBind();
                //    DDL_Cust.Items.Add(new ListItem("--Select--", "0"));

                //    con.Close();
                //}
                // for Accounts
                using (SqlCommand cmdacct = new SqlCommand())
                {
                    //cmdacct.CommandText = " select subheadcategoryfourID as [AccID] ,rtrim('[' + CAST(subheadcategoryfourID AS VARCHAR(200)) + ']-' + subheadcategoryfourName ) as [AccName] from subheadcategoryfour";
                    cmdacct.CommandText = "select subheadcategoryfourID,subheadcategoryfourName from subheadcategoryfour where subheadcategoryfourName <> ''";
                    cmdacct.Connection = con;
                    con.Open();

                    DataTable dtacct = new DataTable();
                    SqlDataAdapter adpacct = new SqlDataAdapter(cmdacct);
                    adpacct.Fill(dtacct);
                    ddl_acc.DataSource = dtacct;
                    ddl_acc.DataTextField = "subheadcategoryfourName";
                    ddl_acc.DataValueField = "subheadcategoryfourID";
                    ddl_acc.DataBind();
                    ddl_acc.Items.Insert(0, new ListItem("--Select--", "1"));
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


        protected void TBSearchPONum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TBSearchPONum.Text != "")
                {
                    DataTable dt_po = new DataTable();

                    string queryString = " select MPurID, suppliername,VndrAdd,VndrCntct,PurNo,MPurDate,supplier.CNIC,NTNNo, " +
                    " MPurchase.CreatedBy,MPurchase.CreatedAt,ToBePrntd from MPurchase  inner join " +
                    " supplier on MPurchase.ven_id = supplier.supplierId where PurNo= '" + TBSearchPONum.Text + "' and  MPurchase.CompanyId = '" + Session["CompanyID"] + "' and MPurchase.BranchId= '" + Session["BranchID"] + "'";

                    dt_po = DBConnection.GetQueryData(queryString);

                    GVScrhMPur.DataSource = dt_po;
                    GVScrhMPur.DataBind();
                }
                else if (TBSearchPONum.Text == "")
                {
                    FillGrid();
                }
            }
            catch (Exception ex)
            {
                //throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }

        }


        protected void GVScrhMPur_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVScrhMPur.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void GVScrhMPur_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row;

                if (e.CommandName == "Show")
                {
                    row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    string PURID = GVScrhMPur.DataKeys[row.RowIndex].Values[0].ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/RReportViewer.aspx?ID=PR&PURID=" + PURID + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                }

                if (e.CommandName == "Select")
                {
                    row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    string MPurID = Server.HtmlDecode(GVScrhMPur.Rows[row.RowIndex].Cells[0].Text.ToString());

                    string cmdtxt = "   select MPurchase.MPurID,DPurID, discper, discamt, PayAcc, netamt, Curr_id,  " +
                        " Curr_nam, Currency_Rate, Exchange_Rat, PurNo,DCNO, DatTim, BiltyNo, VehicalNo, DriverNam, " +
                        " DriverMobilno, Transporter, station, DilverOrdr, frieght, mPurDate,ven_id,vchtyp_id,PayTyp_id, " +
                        " subheadcategoryfourID,csh_amt,CashBnk_id,chque_No,MPurRmk,GrossTtal,tobePrntd,ck_Act,Out_Standing,grssttl,Out_Standing from MPurchase " +
                        " inner join DPurchase on MPurchase.MPurID = DPurchase.MPurID where MPurchase.MPurID ='" + MPurID + "' and MPurchase.CompanyId = '" + Session["CompanyID"] + "' and MPurchase.BranchId= '" + Session["BranchID"] + "'";

                    SqlCommand cmdSlct = new SqlCommand(cmdtxt, con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmdSlct);

                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        HFMPur.Value = dt.Rows[0]["MPurID"].ToString();
                        TBPONum.Text = dt.Rows[0]["PurNo"].ToString();
                        txt_outstand.Text = dt.Rows[0]["Out_Standing"].ToString();
                        TBPurDat.Text = dt.Rows[0]["mPurDate"].ToString();
                        ddl_acc.SelectedValue = dt.Rows[0]["PayAcc"].ToString();
                        ddlVenNam.SelectedValue = dt.Rows[0]["ven_id"].ToString();
                        DDL_Vchtyp.SelectedValue = dt.Rows[0]["vchtyp_id"].ToString();

                        getPay_Typ(DDL_Vchtyp.SelectedValue.Trim());

                        int vchtyp = Convert.ToInt32(dt.Rows[0]["vchtyp_id"]);

                        DDL_Paytyp.SelectedValue = dt.Rows[0]["PayTyp_id"].ToString();
                        DDL_Bnk.SelectedValue = dt.Rows[0]["CashBnk_id"].ToString();
                        TBChqNo.Text = dt.Rows[0]["chque_No"].ToString();
                        TB_ChqAmt.Text = dt.Rows[0]["grssttl"].ToString();
                        DDL_Currency.Text = dt.Rows[0]["Curr_id"].ToString();
                        TBCurrRat.Text = dt.Rows[0]["Currency_Rate"].ToString();
                        TBExchgRat.Text = dt.Rows[0]["Exchange_Rat"].ToString();
                        TB_CshAmt.Text = dt.Rows[0]["csh_amt"].ToString();
                        TBTtl.Text = dt.Rows[0]["csh_amt"].ToString();
                        TBGrssTotal.Text = dt.Rows[0]["netamt"].ToString();
                        TBRmk.Text = dt.Rows[0]["MPurRmk"].ToString();
                        chk_prtd.Checked = Convert.ToBoolean(dt.Rows[0]["tobePrntd"].ToString());
                        chk_Act.Checked = Convert.ToBoolean(dt.Rows[0]["ck_Act"].ToString());
                        TBDCNo.Text = dt.Rows[0]["DCNO"].ToString();
                        TBDatTim.Text = dt.Rows[0]["DatTim"].ToString();
                        TBBiltyNo.Text = dt.Rows[0]["BiltyNo"].ToString();
                        TBVNo.Text = dt.Rows[0]["VehicalNo"].ToString();
                        TBDrvNam.Text = dt.Rows[0]["DriverNam"].ToString();
                        TBDrvMobNo.Text = dt.Rows[0]["DriverMobilno"].ToString();
                        TBTrans.Text = dt.Rows[0]["Transporter"].ToString();
                        TbStaton.Text = dt.Rows[0]["station"].ToString();
                        TBDelivOrd.Text = dt.Rows[0]["DilverOrdr"].ToString();
                        TBFright.Text = dt.Rows[0]["frieght"].ToString();

                        TBDiscPer.Text = dt.Rows[0]["discper"].ToString();
                        TBDisc.Text = dt.Rows[0]["discamt"].ToString();

                        string cmdDettxt = " select distinct(DPurchase.productid),ProTyp as [Category], DPurchase.productid as [ProNam], " +
                            " ProDes as [Description],DPurchase.Brand,DPurchase.Origin,Packsiz as [PackingSize], " +
                            " ProDes,Qty,'0.00' as [percent],'0.00' as [Weight], Rate as [Rate], Unit, NetTotal as [Amount], " +
                            " DPurID,NetTotal from DPurchase  " +
                            " inner join tbl_Dstk on DPurchase.ProductID = tbl_Dstk.ProductID  where " +
                            " DPurchase.MPurID = " + MPurID + " and DPurchase.CompanyId = '" + Session["CompanyID"] + "' and DPurchase.BranchId= '" + Session["BranchID"] + "'";

                        DataTable dt_Det = new DataTable();
                        dt_Det = DataAccess.DBConnection.GetDataTable(cmdDettxt);

                        if (dt_Det.Rows.Count > 0)
                        {
                            GVPurItems.DataSource = dt_Det;
                            GVPurItems.DataBind();
                            ViewState["dt_adItm"] = dt_Det;

                            for (int j = 0; j < dt_Det.Rows.Count; j++)
                            {
                                for (int i = 0; i < GVPurItems.Rows.Count; i++)
                                {
                                    HiddenField HFDPur = (HiddenField)GVPurItems.Rows[i].FindControl("HFDPur");
                                    HFDPur.Value = dt_Det.Rows[j]["DPurID"].ToString();
                                    Label lbl_Flag = (Label)GVPurItems.Rows[i].FindControl("lbl_Flag");
                                }
                            }

                            float GTotal = 0;
                            for (int k = 0; k < GVPurItems.Rows.Count; k++)
                            {
                                TextBox total = (TextBox)GVPurItems.Rows[k].FindControl("Tbamt");
                                GTotal += Convert.ToSingle(total.Text);
                            }

                            TBTtl.Text = GTotal.ToString();
                        }
                    }
                    else
                    {
                        lblmssg.Text = "No Record Found!!";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }

        protected void GVScrhMPur_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string MPurID = Server.HtmlDecode(GVScrhMPur.Rows[e.RowIndex].Cells[0].Text.ToString());
                string MPurNo = Server.HtmlDecode(GVScrhMPur.Rows[e.RowIndex].Cells[1].Text.ToString());

                SqlCommand cmd = new SqlCommand();

                cmd = new SqlCommand("sp_del_Pur", con);
                cmd.Parameters.Add("@mPurID", SqlDbType.Int).Value = MPurID;
                cmd.Parameters.Add("@CompanyId", SqlDbType.VarChar).Value = Session["CompanyID"];
                cmd.Parameters.Add("@BranchId", SqlDbType.VarChar).Value = Session["BranchID"];

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = MPurNo + " has been Deleted!";

                Response.Redirect("frm_Purchase.aspx");

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }

        protected void GVScrhMPur_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //GVScrhMPur.PageIndex = e.NewSelectedIndex;
            //FillGrid();
        }

        protected void DDL_Paytyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (DDL_Paytyp.SelectedValue == "2")
            //{
            //    pnl_bnk.Visible = true;
            //    pnl_Chqno.Visible = true;
            //    pnl_chqamt.Visible = true;
            //    pnl_cshamt.Visible = false;
            //    TB_CshAmt.Text = "";
            //    //DDL_Bnk.Focus();
            //}
            //else if (DDL_Paytyp.SelectedValue != "2")
            //{
            //    pnl_bnk.Visible = false;
            //    pnl_chqamt.Visible = false;
            //    pnl_Chqno.Visible = false;
            //    pnl_cshamt.Visible = true;
            //    //TB_CshAmt.Focus();

                //TBChqNo.Text = "";
                //TB_ChqAmt.Text = "";
                //DDL_Bnk.SelectedValue = "0";
            //}
        }



        private void Update()
        {
            con.Open();

            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            // Start a local transaction.
            transaction = con.BeginTransaction("UpdateTransaction");

            // Must assign both transaction object and connection 
            // to Command object for a pending local transaction
            command.Connection = con;
            command.Transaction = transaction;

            try
            {
                if (TBOutstand.Text != "0.00")
                {
                    totalprev = Convert.ToInt32(txt_outstand.Text) + Convert.ToInt32(TBOutstand.Text);
                }
                else if (TBOutstand.Text == "0.00")
                {
                    totalprev = 0.00;
                }//else
                // {
                //    totalprev = Convert.ToInt32(txt_outstand.Text);
                // }

                #region Credit Sheets

                command.CommandText = "select CredAmt from tbl_Purcredit where supplierId='" + ddlVenNam.SelectedValue.Trim() + "'";

                SqlDataAdapter stksalcre = new SqlDataAdapter(command);

                DataTable dtsalcre = new DataTable();
                stksalcre.Fill(dtsalcre);

                if (dtsalcre.Rows.Count > 0)
                {
                    //double recv = Convert.ToDouble(lblOutstan) - Convert.ToDouble(TBRecy);

                    command.CommandText = " Update tbl_Purcredit set CredAmt = '" + totalprev + "' where supplierId='" + ddlVenNam.SelectedValue.Trim() + "'";
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = " insert into tbl_Purcredit (supplierId,CredAmt) values('" + ddlVenNam.SelectedValue.Trim() + "','" + totalprev + "')";
                    command.ExecuteNonQuery();
                }

                #endregion

                command.CommandText =
                    " update MPurchase set VndrNam = '" + ddlVenNam.SelectedItem.Text +
                    "',subheadcategoryfourID = '', VndrAdd = '', VndrCntct = '', PurNo = '" + TBPONum.Text.Trim() 
                    + "', MPurDate = '" + Convert.ToDateTime(TBPurDat.Text).ToString("MM/dd/yyyy") + "', CreatedBy = '" 
                    + Session["user"].ToString() + "', CreatedAt = '" +
                    DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) 
                    + "', CNIC = '', NTNNo = '', ToBePrntd = '" + chk_prtd.Checked.ToString() 
                    + "', ck_Act = '" + chk_Act.Checked.ToString() + "', MPurRmk = '" 
                    + TBRmk.Text + "', ven_id = '" + ddlVenNam.SelectedValue.Trim() 
                    + "', PayTyp_id = '" + DDL_Paytyp.SelectedValue.Trim()
                    + "', vchtyp_id = '" + DDL_Vchtyp.SelectedValue.Trim() + "', csh_amt = '" 
                    + TB_CshAmt.Text + "', CashBnk_id = '" + DDL_Bnk.SelectedValue.Trim() 
                    + "', chque_No = '" + TBChqNo.Text + "', Out_Standing =" + totalprev 
                    + ", grssttl='" + TBGrssTotal.Text + "', CompanyId='" 
                    + Session["CompanyID"] + "', BranchId='" + Session["BranchID"] + "', DCNO='" 
                    + TBDCNo.Text + "', DatTim='" + TBDatTim.Text + "', BiltyNo='" 
                    + TBBiltyNo.Text + "', VehicalNo='" + TBVNo.Text + "', DriverNam='" 
                    + TBDrvNam.Text + "', DriverMobilno='" + TBDrvMobNo.Text + "', Transporter='" 
                    + TBTrans.Text + "', station='" + TbStaton.Text + "', DilverOrdr='" 
                    + TBDelivOrd.Text + "', frieght='" + TBFright.Text + "' where MPurID ='" 
                    + HFMPur.Value.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                command.ExecuteNonQuery();


                //Master Stock

                command.CommandText = "UPDATE [dbo].[tbl_Mstk]   SET [Mstk_sono] = '" + TBPONum.Text.Trim() + "'" +
                    " ,[Mstk_dat] = '" + Convert.ToDateTime(TBPurDat.Text).ToString("MM/dd/yyyy") + "'" +
                    " ,[Mstk_PurDat] = '" + Convert.ToDateTime(TBPurDat.Text).ToString("MM/dd/yyyy") + "'" +
                    " ,[Mstk_Rmk] = '" + TBRmk.Text + "' ,[ven_id] = " + ddlVenNam.SelectedValue.Trim() +
                    " ,[CustomerID] = '',[MPurID] = " + HFMPur.Value.Trim() +
                    " ,[CreatedBy] = '" + Session["user"].ToString() + "'" +
                    " ,[CreatedAt] = '" + DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "'" +
                    " ,[ISActive] = '" + chk_Act.Checked.ToString() + "'" + " ,[MSal_id] = '' ,[wh_id] = 1 WHERE Mstk_sono ='" + TBPONum.Text.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                command.ExecuteNonQuery();

                foreach (GridViewRow g1 in GVPurItems.Rows)
                {
                    TBcat = (g1.FindControl("TBcat") as TextBox).Text;
                    TbItmDscptin = (g1.FindControl("TbItmDscptin") as TextBox).Text;
                    Tbbrnd = (g1.FindControl("Tbbrnd") as TextBox).Text;
                    Tborgn = (g1.FindControl("Tborgn") as TextBox).Text;
                    TbPacksiz = (g1.FindControl("TbPacksiz") as TextBox).Text;
                    TB_rat = (g1.FindControl("TB_rat") as TextBox).Text;
                    Tbqty = (g1.FindControl("Tbqty") as TextBox).Text;
                    Tbunt = (g1.FindControl("Tbunt") as TextBox).Text;
                    Tbamt = (g1.FindControl("Tbamt") as TextBox).Text;
                    string lbl_Flag = (g1.FindControl("lbl_Flag") as Label).Text;
                    HFDPur = (g1.FindControl("HFDPur") as HiddenField).Value;

                    query = " select * from Products where ProductName= '" + TbItmDscptin + "' and CompanyId = '"
                        + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                    dt_ = DBConnection.GetQueryData(query);
                    
                    if (dt_.Rows.Count > 0)
                    {
                        proid = dt_.Rows[0]["ProductId"].ToString();
                        Purrat = dt_.Rows[0]["TradPrice"].ToString();


                    }
                    //Detail Purchase

                    if (HFDPur != "")
                    {
                        command.CommandText = " update DPurchase set ProTyp='" + TBcat + "', ProductID ='" + proid +
                            "' , ProDes='" + TbItmDscptin + "', Brand = '"+Tbbrnd.Trim() +"', Origin = '" + Tborgn.Trim() +
                            "', Packsiz = '" + TbPacksiz.Trim() + "', Rate='" + TB_rat.Trim() + "', purrat='" + TB_rat.Trim() + "', afterrat='" + resul + "' , NetTotal ='" + Tbamt.Trim() +
                            "', Unit='" + Tbunt.Trim() + "', GrossTtal ='" + TBGrssTotal.Text + "', CreatedBy='" + Session["user"].ToString() +
                            "', CreatedAt='" + DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' , Amount ='" + Tbamt.Trim() + "'" +
                                " where MPurID ='" + HFMPur.Value.Trim() + "' and DPurID='" + HFDPur + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        command.CommandText =
                           " INSERT INTO DPurchase (MPurID, ProTyp, ProDes, Brand, origin, Packsiz, Rate, afterrat, purrat, Qty, " +
                           " Unit, NetTotal, ProductID,CompanyId, BranchId, Amount) " +
                           " VALUES " +
                           " ('" + HFMPur.Value.Trim() + "','" + TBcat + "','" + TbItmDscptin + "', '" + Tbbrnd + "','" + Tborgn + "','" + TbPacksiz +
                           "', '" + TB_rat + "','" + TB_rat + "','" + resul + "','" + Tbqty + "', '" + Tbunt + "','" + Tbamt + "','" +
                           proid + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "','" + Tbamt + "')";
                        
                        
                        command.ExecuteNonQuery();
                    }

                    decimal prisaftfreight = Convert.ToDecimal(TBGrssTotal.Text.Trim()) + Convert.ToDecimal(TBFright.Text.Trim());

                    decimal perpiece = prisaftfreight / Convert.ToDecimal(Tbqty.Trim());

                    decimal afterrat = perpiece * Convert.ToDecimal(TBExchgRat.Text);


                    //resul = g.convert(Convert.ToDecimal(TBExchgRat.Text), Convert.ToDecimal(Purrat), Convert.ToDecimal(TBFright.Text));
                    resul = Convert.ToDecimal(Tbqty) * Convert.ToDecimal(afterrat);


                    //resul = g.convert(Convert.ToDecimal(TBExchgRat.Text), Convert.ToDecimal(Purrat), Convert.ToDecimal(TBFright.Text));

                    //Detail Stock

                    command.CommandText = "select Dstk_Qty from tbl_Dstk where ProductID = " + proid + " and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                    DataTable dtstkqt = new DataTable();

                    SqlDataAdapter Adapter = new SqlDataAdapter(command);
                    Adapter.Fill(dtstkqt);

                    if (dtstkqt.Rows.Count > 0)
                    {
                        for (int t = 0; t < dtstkqt.Rows.Count; t++)
                        {
                            stkqty = dtstkqt.Rows[t]["Dstk_Qty"].ToString();

                            int qty = Convert.ToInt32(stkqty) + Convert.ToInt32(PurItmQty);

                            if (lbl_Flag == "0")
                            {
                                command.CommandText = " UPDATE tbl_Dstk SET Dstk_Qty = '" + qty + "' , Dstk_afterrat= '" + resul + "', Dstk_rat='" + afterrat + "' where  ProductID = " + proid + " and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        // Master Stock
                        command.CommandText = "select Mstk_id from tbl_Mstk where Mstk_sono = '" + TBPONum.Text.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                        SqlDataAdapter adpstk = new SqlDataAdapter(command);

                        DataTable dtstk = new DataTable();
                        adpstk.Fill(dtstk);

                        if (dtstk.Rows.Count > 0)
                        {
                            MSTkId = dtstk.Rows[0]["Mstk_id"].ToString();
                        }

                        command.CommandText = " INSERT INTO tbl_Dstk (Dstk_ItmDes, Brand, origin, Dstk_packsiz, Dstk_rat, Dstk_afterrat," +
                            " Dstk_Qty, Dstk_unt, amount, Mstk_id, ProductID, CompanyId, BranchId, MPurID) " +
                                " VALUES " +
                                " ('" + TbItmDscptin + "','" + Tbbrnd + "','" + Tborgn + "','" + TbPacksiz +
                                "','" + afterrat + "','" + resul + "','" + Tbqty + "', '" + Tbunt + "', '" + Tbamt + "','" + MSTkId +
                                "','" + proid + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "','" + MPurId + "')";
                        command.ExecuteNonQuery();
                    }

                }
                // Attempt to commit the transaction.
                transaction.Commit();

                if (chk_prtd.Checked == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Purchase.aspx?ID=PR','_blank','height=600px,width=600px,scrollbars=1');", true);
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
                //FillGrid();
                //ptnSno();
                Response.Redirect("frm_Purchase.aspx");
                //BindDll();
            }

        }
        //private void SaveStk()
        //{

        //    try
        //    {
        //        //AddPurchase();
        //        command.CommandText =
        //            " INSERT INTO tbl_Mstk(Mstk_sono, Mstk_dat, Mstk_PurDat, Mstk_Rmk, ven_id, CustomerID, MPurID, CreatedBy, CreatedAt, ISActive) " +
        //            " VALUES " +
        //            " ('','2018/11/28','2018/11/28','','" + ddlVenNam.SelectedValue.Trim() + "', '','" + MPurId + "','" + Session["user"].ToString() + "','" + DateTime.Today.ToString("yyyy/MM/dd") + "','true')";
        //        command.ExecuteNonQuery();


        //        //if (chk_prt.Checked == true)
        //        //{
        //            //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=PR','_blank','height=600px,width=600px,scrollbars=1');", true);
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
        //        Console.WriteLine("  Message: {0}", ex.Message);
        //    }

        //}

       
        private int SavePurchase()
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
                #region Purchase Record

                string totalprev = "0.0";
                if (TBOutstand.Text != "0.00")
                {
                    if (txt_outstand.Text == "")
                    {
                        txt_outstand.Text = "0.00";
                    }
                    else
                    {
                        totalprev = (Convert.ToDouble(txt_outstand.Text) + Convert.ToDouble(TBOutstand.Text)).ToString();
                    }
                }
                else
                {
                    totalprev = txt_outstand.Text;
                }

                #region Credit Sheets

                command.CommandText = "select CredAmt from tbl_Purcredit where supplierId='" + ddlVenNam.SelectedValue.Trim() + "'";

                SqlDataAdapter stksalcre = new SqlDataAdapter(command);

                DataTable dtsalcre = new DataTable();
                stksalcre.Fill(dtsalcre);

                if (dtsalcre.Rows.Count > 0)
                {
                    //double recv = Convert.ToDouble(lblOutstan) - Convert.ToDouble(TBRecy);

                    command.CommandText = " Update tbl_Purcredit set CredAmt = '" + totalprev + "' where supplierId='" + ddlVenNam.SelectedValue.Trim() + "'";
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = " insert into tbl_Purcredit (supplierId,CredAmt) values('" + ddlVenNam.SelectedValue.Trim() + "','" + totalprev + "')";
                    command.ExecuteNonQuery();
                }

                #endregion

                if (HFMPur.Value == "0")
                {
                    command.CommandText =
                                " INSERT INTO MPurchase(VndrNam,subheadcategoryfourID, VndrAdd, VndrCntct, PurNo, MPurDate, " +
                                " CreatedBy, CreatedAt, CNIC, NTNNo, ToBePrntd, ck_Act, MPurRmk, ven_id, PayTyp_id, " +
                                " vchtyp_id, csh_amt, CashBnk_id, chque_No, prebal, Out_Standing, amtpaid, discper, " +
                                " discamt, netamt, PayAcc, WithHoldingTax, CompanyId, BranchId, DCNO, DatTim, BiltyNo, " +
                                " VehicalNo, DriverNam, DriverMobilno, Transporter, station, DilverOrdr, frieght, Curr_id, " +
                                " Curr_nam, Currency_Rate, Exchange_Rat) " +
                                " VALUES " +
                                " ('" + ddlVenNam.SelectedItem.Text + "', '', '', '', '" +
                                TBPONum.Text.Trim() + "','" + Convert.ToDateTime(TBPurDat.Text).ToString("MM/dd/yyyy") + "','" +
                                Session["user"].ToString() + "','" +
                                DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "', '', '', '" +
                                chk_prtd.Checked.ToString() + "', '" + chk_Act.Checked.ToString() + "', '" + TBRmk.Text +
                                "', '" + ddlVenNam.SelectedValue.Trim() + "', '" + DDL_Paytyp.SelectedValue.Trim() +
                                "', '" + DDL_Vchtyp.SelectedValue.Trim() + "', '" + TB_CshAmt.Text + "', '" +
                                DDL_Bnk.SelectedValue.Trim() + "', '" + TBChqNo.Text + "','" + totalprev + "','" + txt_outstand.Text + "','" +
                                TBAmtPaid.Text + "','" + TBDiscPer.Text + "','" + TBDisc.Text + "','" + TBGrssTotal.Text + "', '" + ddl_acc.SelectedValue.Trim() +
                                "', '" + TBWidholdtx.Text + "', '" + Session["CompanyID"] +
                                "','" + Session["BranchID"] + "','" + TBDCNo.Text + "','" + TBDatTim.Text + "','" + TBBiltyNo.Text + "','" + TBVNo.Text +
                                "','" + TBDrvNam.Text + "','" + TBDrvMobNo.Text + "','" + TBTrans.Text + "','" + TbStaton.Text +
                                "','" + TBDelivOrd.Text + "','" + TBFright.Text + "','" + DDL_Currency.SelectedValue.Trim() + "','"
                                + DDL_Currency.SelectedItem.Text + "','" + TBCurrRat.Text + "','" + TBExchgRat.Text + "')";

                    command.ExecuteNonQuery();
                }
                else
                {
                    totalprev = (Convert.ToDouble(txt_outstand.Text) - Convert.ToDouble(TBOutstand.Text)).ToString();

                    command.CommandText = " UPDATE MPurchase SET Out_Standing = '" + totalprev + "' where  ven_id =" + ddlVenNam.SelectedValue + "";

                    command.ExecuteNonQuery();
                }

                // Master Purchase
                command.CommandText = "select MPurID from mpurchase where PurNo= '" + TBPONum.Text.Trim() + "'";

                SqlDataAdapter adp = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    MPurId = dt.Rows[0]["MPurID"].ToString();
                }

                //Save Master Stock
                command.CommandText =
                    " INSERT INTO tbl_Mstk(Mstk_sono, Mstk_dat, Mstk_PurDat, Mstk_Rmk, ven_id, CustomerID, " +
                    " MPurID, CreatedBy, CreatedAt, ISActive, CompanyId, BranchId) " +
                    " VALUES " +
                    " ('" + TBPONum.Text.Trim() + "','" + TBPurDat.Text + "','" + TBPurDat.Text + "','" +
                    TBRmk.Text.Trim() + "','" + ddlVenNam.SelectedValue.Trim() + "', '','" + MPurId + "','"
                    + Session["user"].ToString() + "','" + DateTime.Today.ToString("yyyy/MM/dd") + "','true', '"
                    + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                command.ExecuteNonQuery();

                // Master Stock
                command.CommandText = "select Mstk_id from tbl_Mstk where Mstk_sono = '" + TBPONum.Text.Trim() + "'";

                SqlDataAdapter adpstk = new SqlDataAdapter(command);

                DataTable dtstk = new DataTable();
                adpstk.Fill(dtstk);

                if (dtstk.Rows.Count > 0)
                {
                    MSTkId = dtstk.Rows[0]["Mstk_id"].ToString();
                }


                foreach (GridViewRow g1 in GVPurItems.Rows)
                {
                    TBcat = (g1.FindControl("TBcat") as TextBox).Text;
                    TbItmDscptin = (g1.FindControl("TbItmDscptin") as TextBox).Text;
                    Tbbrnd = (g1.FindControl("Tbbrnd") as TextBox).Text;
                    Tborgn = (g1.FindControl("Tborgn") as TextBox).Text;
                    TbPacksiz = (g1.FindControl("TbPacksiz") as TextBox).Text;
                    TB_rat = (g1.FindControl("TB_rat") as TextBox).Text;
                    Tbqty = (g1.FindControl("Tbqty") as TextBox).Text;
                    Tbunt = (g1.FindControl("Tbunt") as TextBox).Text;
                    Tbamt = (g1.FindControl("Tbamt") as TextBox).Text;


                    query = " select * from Products where ProductName = '" + TbItmDscptin + "'";

                    dt = DBConnection.GetQueryData(query);
                    if (dt.Rows.Count > 0)
                    {
                        PurItm = dt.Rows[0]["ProductID"].ToString();
                        Purrat = dt.Rows[0]["TradPrice"].ToString();

                        //resul = g.convert(Convert.ToDecimal(TBExchgRat.Text), Convert.ToDecimal(Purrat), Convert.ToDecimal(TBFright.Text));

                    }

                    //Detail Purchase,
                    command.CommandText =
                        " INSERT INTO DPurchase (MPurID, ProTyp, ProDes, Brand, origin, Packsiz, Rate, purrat, afterrat, Qty, " +
                        " Unit, NetTotal, ProductID,CompanyId, BranchId, Amount) " +
                        " VALUES " +
                        " ('" + MPurId + "','" + TBcat + "','" + TbItmDscptin + "', '" + Tbbrnd + "','" + Tborgn + "','" + TbPacksiz +
                        "', '" + TB_rat + "','" + Purrat + "','" + Purrat.Trim() + "', '" + Tbqty + "', '" + Tbunt + "','" + Tbamt + "','" +
                        PurItm + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "','" + Tbamt + "')";

                    command.ExecuteNonQuery();


                    DataTable dtstkqty = new DataTable();

                    //Detail Stock

                    command.CommandText = "select Dstk_Qty from tbl_Dstk where ProductID = " + PurItm + "";

                    SqlDataAdapter Adapter = new SqlDataAdapter(command);
                    Adapter.Fill(dtstkqty);

                    if (dtstkqty.Rows.Count > 0)
                    {
                        for (int t = 0; t < dtstkqty.Rows.Count; t++)
                        {
                            stkqty = dtstkqty.Rows[t]["Dstk_Qty"].ToString();

                            //int qty = Convert.ToInt32(string.IsNullOrEmpty(stkqty)) + Convert.ToInt32(string.IsNullOrEmpty(Tbqty));
                            int qty = Convert.ToInt32(stkqty) + Convert.ToInt32(Tbqty);
                            command.CommandText = " UPDATE tbl_Dstk SET Dstk_Qty = " + qty + " where  ProductID = " + PurItm + "";
                            command.ExecuteNonQuery();

                        }
                    }
                    else
                    {
                        decimal prisaftfreight = Convert.ToDecimal(TBGrssTotal.Text.Trim()) + Convert.ToDecimal(TBFright.Text.Trim());

                        decimal perpiece = prisaftfreight / Convert.ToDecimal(Tbqty.Trim());

                        decimal afterrat = perpiece * Convert.ToDecimal(TBExchgRat.Text);


                        //resul = g.convert(Convert.ToDecimal(TBExchgRat.Text), Convert.ToDecimal(Purrat), Convert.ToDecimal(TBFright.Text));
                        resul = Convert.ToDecimal(Tbqty) * Convert.ToDecimal(afterrat);

                        command.CommandText = " INSERT INTO tbl_Dstk (Dstk_ItmDes, Brand, origin, Dstk_packsiz, Dstk_rat, Dstk_afterrat, " +
                            " Dstk_Qty, Dstk_unt, amount, Mstk_id, ProductID, CompanyId, BranchId, MPurID) " +
                                " VALUES " +
                                " ('" + TbItmDscptin + "','" + Tbbrnd + "','" + Tborgn + "','" + TbPacksiz +
                                "','" + afterrat + "','" + resul + "','" + Tbqty + "', '" + Tbunt + "', '" + Tbamt + "','" + MSTkId +
                                "','" + PurItm + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "','" + MPurId + "')";
                        command.ExecuteNonQuery();

                    }

                }
                #endregion
                #region Stock Record


                #endregion

                // Attempt to commit the transaction.
                transaction.Commit();

                if (chk_prtd.Checked == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_Purchase.aspx?ID=PR','_blank','height=600px,width=600px,scrollbars=1');", true);
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
                res = 1;

            }

            return res;
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            
            if (HFMPur.Value == "0")
            {
                int i = 0;

                //Save Purchase

                i = SavePurchase();

                if (i == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lbl_Heading.Text = "Saved!";
                    lblalert.Text = "Purchase has Been Saved!!";

                    Response.Redirect("frm_Purchase.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lbl_Heading.Text = "Error!";
                    lblalert.Text = "Some thins is worng Please Contact Administrator";
                }
            }
            else
            {
                Update();
            }
        }

        public void Clear()
        {
            //TBVenAdd.Text = "";
            //TBVenCon.Text = "";
            TBPONum.Text = "";
            //TBPurDat.Text = "";
            SetInitRowPuritm();
            // BindDll();
            TBGrssTotal.Text = "";
            //TBNIC.Text = "";
            //TBNTN.Text = "";

        }

        protected void btnRevert_Click(object sender, EventArgs e)
        {
            Response.Redirect("frm_Purchase.aspx");
        }

        protected void ddlPurItm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < GVPurItems.Rows.Count; j++)
                {
                    DropDownList ddlPurItm = (DropDownList)GVPurItems.Rows[j].FindControl("ddlPurItm");
                    TextBox TBItmDes = (TextBox)GVPurItems.Rows[j].FindControl("TbaddPurItmDscptin");
                    TextBox TBItmQty = (TextBox)GVPurItems.Rows[j].FindControl("TbAddPurItmQty");
                    //TextBox Tbwght = (TextBox)GVPurItems.Rows[j].FindControl("Tbwght");
                    //TextBox Tbrat = (TextBox)GVPurItems.Rows[j].FindControl("Tbrat");
                    //TextBox TbItmCst = (TextBox)GVPurItems.Rows[j].FindControl("TbAddCosts");
                    //TextBox TbSalTax = (TextBox)GVPurItems.Rows[j].FindControl("TbSalTax");

                    Label TbPurRat = (Label)GVPurItems.Rows[j].FindControl("TbPurRat");
                    Label TbSalRat = (Label)GVPurItems.Rows[j].FindControl("TbSalRat");

                    //DropDownList ddlPurUnit = (DropDownList)GVPurItems.Rows[j].FindControl("ddlPurUnit");
                    TextBox TBNetTtl = (TextBox)GVPurItems.Rows[j].FindControl("TbAddPurNetTtl");
                    Label lbl_Flag = (Label)GVPurItems.Rows[j].FindControl("lbl_Flag");


                    string query = "select ProductID,ProductName,ProductDiscriptions,PckSize,Cost as [Rate],PurchasePrice,SalePrice, Unit, " +
                        " '' as [Net Total] " +
                        " from Products where ProductID = " + ddlPurItm.SelectedValue.Trim() + " and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                    SqlCommand cmd = new SqlCommand(query, con);
                    DataTable dt_ = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);

                    adp.Fill(dt_);

                    if (dt_.Rows.Count > 0)
                    {
                        TBItmDes.Text = "0.00";//dt_.Rows[0]["ProductDiscriptions"].ToString();
                        //ddlPurUnit.SelectedItem.Text = dt_.Rows[0]["Unit"].ToString();
                        //TBItmQty.Text = "0.00";
                        //Tbwght.Text = "0.00";
                        //Tbrat.Text = "0.00";
                        //TbItmCst.Text = dt_.Rows[0]["Rate"].ToString();
                        //TbSalTax.Text = "0.00";
                        TbPurRat.Text = dt_.Rows[0]["PurchasePrice"].ToString();
                        TbSalRat.Text = dt_.Rows[0]["SalePrice"].ToString();
                        //lbl_Flag.Text = "1";
                    }

                    //TBItmQty.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
                //lbl_err.Text = ex.Message.ToString();
            }

        }


        protected void ddlVenNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string query = " select isnull(SUM(CredAmt) , 0)as 'OUTSTAND' from tbl_Purcredit where supplierId ='" + ddlVenNam.SelectedValue + "'";

                SqlCommand command = new SqlCommand(query, con);
                con.Open();
                DataTable dtven = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(dtven);
                command.ExecuteNonQuery();

                if (dtven.Rows.Count > 0)
                {
                    txt_outstand.Text = dtven.Rows[0]["OUTSTAND"].ToString();
                    //DDL_Vchtyp.Focus();
                }
                else
                {
                    txt_outstand.Text = (0.00).ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }

        }

        protected void TbAddPurItmQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < GVPurItems.Rows.Count; j++)
                {
                    DropDownList ddlPurItm = (DropDownList)GVPurItems.Rows[j].FindControl("ddlPurItm");
                    TextBox TBItmDes = (TextBox)GVPurItems.Rows[j].FindControl("TbaddPurItmDscptin");
                    TextBox TBItmQty = (TextBox)GVPurItems.Rows[j].FindControl("TbAddPurItmQty");
                    //DropDownList ddlPurUnit = (DropDownList)GVPurItems.Rows[j].FindControl("ddlPurUnit");
                    TextBox TbItmCst = (TextBox)GVPurItems.Rows[j].FindControl("TbAddCosts");
                    TextBox TBNetTtl = (TextBox)GVPurItems.Rows[j].FindControl("TbAddPurNetTtl");
                    Label TbPurRat = (Label)GVPurItems.Rows[j].FindControl("TbPurRat");
                    Label TbSalRat = (Label)GVPurItems.Rows[j].FindControl("TbSalRat");
                    Label lbl_Flag = (Label)GVPurItems.Rows[j].FindControl("lbl_Flag");



                    //TextBox DBAmtttl = (TextBox)GVPurItems.Rows[j].FindControl("Tbdbamt");
                    //if (TBItmQty.Text != "" && TbItmCst.Text != "")
                    {
                        TBNetTtl.Text = (Convert.ToDouble(TBItmQty.Text.Trim()) * Convert.ToDouble(TbPurRat.Text.Trim())).ToString();
                        float GTotal = 0;
                        for (int k = 0; k < GVPurItems.Rows.Count; k++)
                        {
                            TextBox total = (TextBox)GVPurItems.Rows[k].FindControl("TbAddPurNetTtl");
                            GTotal += Convert.ToSingle(total.Text);
                        }

                        //double salrat = Convert.ToDouble(TBNetTtl.Text.Trim()) * 0.1;

                        //TbSalRat.Text = (Convert.ToDouble(TBNetTtl.Text.Trim()) + Convert.ToDouble(salrat)).ToString();


                        TBGrssTotal.Text = GTotal.ToString();
                        TBTtl.Text = GTotal.ToString();

                        if (DDL_Paytyp.SelectedValue == "2")
                        {
                            TB_ChqAmt.Text = TBTtl.Text;
                            TB_ChqAmt.Enabled = false;
                        }
                        else
                        {
                            TB_CshAmt.Text = TBTtl.Text;
                            TB_CshAmt.Enabled = false;
                        }

                        if (ddlPurItm.SelectedValue == "0")
                        {
                            lbl_Flag.Text = "0";
                        }
                        else
                        {
                            //lbl_Flag.Text = "1";
                        }
                        //TBNetTtl.Focus();

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }

        protected void GVPurItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

                    GVPurItems.DataSource = dt;
                    GVPurItems.DataBind();

                    SetPreRowitm();

                    float GTotal = 0;
                    for (int j = 0; j < GVPurItems.Rows.Count; j++)
                    {
                        TextBox total = (TextBox)GVPurItems.Rows[j].FindControl("Tbamt");
                        GTotal += Convert.ToSingle(total.Text);

                        TBGrssTotal.Text = GTotal.ToString();
                    }


                }
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
        protected void DDL_Vchtyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_Vchtyp.SelectedValue != "")
            {
                getPay_Typ(DDL_Vchtyp.SelectedValue.Trim());
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = "Please Select The Voucher Type First!";
            }
        }

        protected void TBOutstand_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TBOutstand.Text != "")
                {
                    double ttlafoutstand = Convert.ToDouble(TBGrssTotal.Text) - (Convert.ToDouble(txt_outstand.Text) + Convert.ToDouble(TBOutstand.Text));
                    TBTtl.Text = ttlafoutstand.ToString();
         
                    string disc = ((Convert.ToDecimal(TBTtl.Text)) * (Convert.ToDecimal(TBDiscPer.Text) / 100)).ToString();

                    TBDisc.Text = disc;

                    TBGrssTotal.Text = (Convert.ToDecimal(TBTtl.Text) - Convert.ToDecimal(disc)).ToString();
                    TB_CshAmt.Text = TBGrssTotal.Text;
                    TB_ChqAmt.Text = TBGrssTotal.Text;
                }
                else
                {
                    TBOutstand.Text = "0.00";
                    TBTtl.Text = TBGrssTotal.Text;
                    TB_CshAmt.Text = TBTtl.Text;

                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        protected void TBAmtPaid_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TBAmtPaid.Text != "")
                {
                    double ttlamtpad = (Convert.ToDouble(txt_outstand.Text) - Convert.ToDouble(TBAmtPaid.Text));
                    txt_outstand.Text = ttlamtpad.ToString();

                    TBTtl.Text = (Convert.ToDecimal(TBTtl.Text) + Convert.ToDecimal(TBAmtPaid.Text)).ToString();

                    string disc = ((Convert.ToDecimal(TBTtl.Text)) * (Convert.ToDecimal(TBDiscPer.Text) / 100)).ToString();

                    TBDisc.Text = disc;

                    TBGrssTotal.Text = (Convert.ToDecimal(TBTtl.Text) - Convert.ToDecimal(disc)).ToString();
                    TB_CshAmt.Text = TBGrssTotal.Text;
                    TB_ChqAmt.Text = TBGrssTotal.Text;
                }
                else
                {
                    TBAmtPaid.Text = "0.00";
                    TBTtl.Text = TBGrssTotal.Text;
                    TB_CshAmt.Text = TBTtl.Text;

                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        protected void Tbqty_TextChanged(object sender, EventArgs e)
        {

            try
            {
                for (int i = 0; i < GVPurItems.Rows.Count; i++)
                {
                    TextBox TBcat = (TextBox)GVPurItems.Rows[i].Cells[0].FindControl("TBcat");
                    TextBox TbItmDscptin = (TextBox)GVPurItems.Rows[i].Cells[1].FindControl("TbItmDscptin");
                    TextBox TB_rat = (TextBox)GVPurItems.Rows[i].Cells[7].FindControl("TB_rat");
                    TextBox Tbqty = (TextBox)GVPurItems.Rows[i].Cells[6].FindControl("Tbqty");
                    TextBox Tbamt = (TextBox)GVPurItems.Rows[i].Cells[8].FindControl("Tbamt");
                    Label lbl_Flag = (Label)GVPurItems.Rows[i].FindControl("lbl_Flag");

                    //resul = g.convert(Convert.ToDecimal(TBExchgRat.Text), Convert.ToDecimal(TB_rat.Text.Trim()), Convert.ToDecimal(TBFright.Text));

                    Tbamt.Text = (Convert.ToDouble(Tbqty.Text.Trim()) * Convert.ToDouble(TB_rat.Text.Trim())).ToString();

                    float GTotal = 0;
                    for (int k = 0; k < GVPurItems.Rows.Count; k++)
                    {
                        TextBox total = (TextBox)GVPurItems.Rows[k].FindControl("Tbamt");
                        GTotal += Convert.ToSingle(total.Text);
                    }

                    TBGrssTotal.Text = GTotal.ToString();
                    TBTtl.Text = GTotal.ToString();

                    //if (DDL_Paytyp.SelectedValue == "2")
                    {
                        TB_ChqAmt.Text = TBTtl.Text;
                        TB_ChqAmt.Enabled = false;
                    }
                    //else
                    {
                        TB_CshAmt.Text = TBTtl.Text;
                        TB_CshAmt.Enabled = false;
                    }
                    {
                        //lbl_Flag.Text = "1";
                    }

                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        protected void TBDiscPer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (TBDiscPer.Text != "")
                {
                    string disc = ((Convert.ToDecimal(TBTtl.Text)) * (Convert.ToDecimal(TBDiscPer.Text) / 100)).ToString();

                    TBDisc.Text = disc;

                    TBGrssTotal.Text = (Convert.ToDecimal(TBTtl.Text) - Convert.ToDecimal(disc)).ToString();
                    TB_CshAmt.Text = TBGrssTotal.Text;
                    TB_ChqAmt.Text = TBGrssTotal.Text;
                }
                else
                {
                    TBAmtPaid.Text = "0.00";
                    TBTtl.Text = TBGrssTotal.Text;
                    TB_CshAmt.Text = TBTtl.Text;

                }
            }
            catch (Exception ex)
            {
                lblmssg.Text = ex.Message;
            }
        }

        protected void Tborgn_TextChanged(object sender, EventArgs e)
        {
        }

        protected void TbPacksiz_TextChanged(object sender, EventArgs e)
        {
        }
        protected void Tbunt_TextChanged(object sender, EventArgs e)
        {
        }
        
        

        
        protected void Tbbrnd_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < GVPurItems.Rows.Count; i++)
            {
                TextBox Tbbrnd = (TextBox)GVPurItems.Rows[i].Cells[2].FindControl("Tbbrnd");

                query = "select * from tbl_brnd where brndnam='" + Tbbrnd.Text.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    //Do Noting
                }
                else
                {

                    query = " INSERT INTO tbl_brnd " +
                                    " (brndnam,IsActive,CreateBy,CreateAt,companyid,branchid) VALUES('" + Tbbrnd.Text.Trim() + "','true','" + Session["user"].ToString() +
                                    " ','" + DateTime.Now + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }

        }
    }
}
                        