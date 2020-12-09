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

namespace Foods
{
    public partial class frm_grn : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        string MPurId, Mstk_id, Mvch_id;
        SqlTransaction tran;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                FillGrid();
                SetInitRowPuritm();
                ptnGRNNo();
                TBGRNNum.Enabled = false;
                PanelShowClosed.Visible = false;
                TBGRNDat.Text = DateTime.Now.ToShortDateString();
                ddlVenNam.Enabled = true;
                chk_Act.Checked = true;
                chk_prtd.Checked = true;
                ddlVenNam.Focus();
                BindDll();
                TBGRNNum.Enabled = false;
            }        
        }

        private void getMpurID()
        {
            try
            {
                string cmdtxt = "select MPurID from tbl_mgrn where PurNo= '" + TBGRNNum.Text.Trim() + "'";

                SqlCommand cmdMPID = new SqlCommand(cmdtxt, con);

                SqlDataAdapter adp = new SqlDataAdapter(cmdMPID);

                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Session["MPurId"] = dt.Rows[0]["MPurID"].ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }

        }

        public void ShwPurId()
        {
            try
            {
                string str = "select mgrn_id, mgrn_sono from tbl_mgrn order by mgrn_id desc";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(TBGRNNum.Text))
                    {
                        int v = Convert.ToInt32(reader["mgrn_id"].ToString());
                        int b = v + 1;
                        TBGRNNum.Text = "GRN00" + b.ToString();

                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                // throw ex;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }

        public void FillGrid()
        {
            try
            {
                //DataTable dt_ = new DataTable();
                //dt_ = tbl_mgrnManager.GetmgrnList();

                //GVScrhrMGRN.DataSource = dt_;
                //GVScrhrMGRN.DataBind();
                
                //ViewState["MGrn"] = dt_;
            }
            catch (Exception ex)
            {
                //throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }

        }

        public void BindDll()
        {
            try
            {
                //For PO
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select rtrim('[' + CAST(MPurOrdsono AS VARCHAR(200)) + '] - ' + ven_nam) as [MPO], MPurOrdid from tbl_MPurOrd inner join t_ven on tbl_MPurOrd.ven_id = t_ven.ven_id";
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtPO = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtPO);

                    DDL_PO.DataSource = dtPO;
                    DDL_PO.DataTextField = "MPO";
                    DDL_PO.DataValueField = "MPurOrdid";
                    DDL_PO.DataBind();
                    DDL_PO.Items.Add(new ListItem("--Select--", "0"));
                    
                    con.Close();
                }

                //For Vendor
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select rtrim('[' + CAST(ven_id AS VARCHAR(200)) + ']-' + ven_nam ) as [ven_nam], ven_id from t_ven";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtSupNam = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtSupNam);

                    ddlVenNam.DataSource = dtSupNam;
                    ddlVenNam.DataTextField = "ven_nam";
                    ddlVenNam.DataValueField = "ven_id";
                    ddlVenNam.DataBind();
                    ddlVenNam.Items.Add(new ListItem("--Select--", "0"));

                    con.Close();
                }

                //Items Name
                using (SqlCommand cmdpro = new SqlCommand())
                {
                    cmdpro.CommandText = " select rtrim('[' + CAST(ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], ProductID  from Products";

                    cmdpro.Connection = con;
                    con.Open();

                    DataTable dtSupNam = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                    adp.Fill(dtSupNam);

                    for (int i = 0; i < GVGRNItems.Rows.Count; i++)
                    {
                        DropDownList ddlitem = (DropDownList)GVGRNItems.Rows[i].FindControl("ddlPurItm");

                        ddlitem.DataSource = dtSupNam;
                        ddlitem.DataTextField = "ProductName";
                        ddlitem.DataValueField = "ProductID";
                        ddlitem.DataBind();
                        ddlitem.Items.Add(new ListItem("--Select--", "0"));
                    }

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

        protected void btnSaveNew_Click(object sender, EventArgs e)
        {


            FillGrid();
            if (ckprntd.Checked == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReGRNrtViewer.aspx','_blank','height=600px,width=600px,scrollbars=1');", true);
            }
            ShwPurId();
            //Response.Redirect("AlPur_Inv.aspx");  
        }

        private int savemgrn()
        {
            try
            {
                int j = 1;

                //tbl_mgrn mgrn = new tbl_mgrn();

                //mgrn.mgrn_id = HFMGRN.Value;
                //mgrn.mgrn_sono = string.IsNullOrEmpty(TBGRNNum.Text) ? null : TBGRNNum.Text;
                //mgrn.mgrn_dat = Convert.ToDateTime(string.IsNullOrEmpty(TBGRNDat.Text) ? null : TBGRNDat.Text);
                //mgrn.MPurOrdid = string.IsNullOrEmpty(DDL_PO.SelectedValue) ? null : DDL_PO.SelectedValue;
                //mgrn.ven_id = string.IsNullOrEmpty(ddlVenNam.SelectedValue) ? null : ddlVenNam.SelectedValue;
                //mgrn.CreatedBy = Session["user"].ToString();
                //mgrn.CreatedAt = DateTime.Today;
                //mgrn.ISActive = chk_Act.Checked.ToString(); ;
                //mgrn.mgrn_rmk = TBRmk.Text;

                //tbl_mgrnManager grnmanag = new tbl_mgrnManager(mgrn);
                //grnmanag.Save();

                return j;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Save()
        {
            try
            {
                int i = 0;

                i = savemgrn();

                if (i > 0)
                {
                    string cmdtxt = "select mgrn_id from tbl_mgrn where Mgrn_sono= '" + TBGRNNum.Text.Trim() + "'";

                    SqlCommand cmdMgrnID = new SqlCommand(cmdtxt, con);

                    SqlDataAdapter adp = new SqlDataAdapter(cmdMgrnID);

                    DataTable dt = new DataTable();
                    adp.Fill(dt);


                    if (dt.Rows.Count > 0)
                    {
                        string Mgrn_id = dt.Rows[0]["mgrn_id"].ToString();

                        foreach (GridViewRow g1 in GVGRNItems.Rows)
                        {
                            string ddlItm = (g1.FindControl("ddlPurItm") as DropDownList).SelectedValue;
                            string ItmDscptin = (g1.FindControl("ItmDscptin") as TextBox).Text;
                            string ItmQty = (g1.FindControl("ItmQty") as TextBox).Text;
                            string TbInsp = (g1.FindControl("TbInsp") as TextBox).Text;
                            string Tbrej = (g1.FindControl("Tbrej") as TextBox).Text;
                            string Tbbal = (g1.FindControl("Tbbal") as TextBox).Text;
                            string Tbrmk = (g1.FindControl("Tbrmk") as TextBox).Text;


                            //tbl_dgrn dgrn = new tbl_dgrn();

                            //dgrn.dgrnid = HFDGRN.Value;
                            //dgrn.dgrn_ItmDes = string.IsNullOrEmpty(ItmDscptin) ? null : ItmDscptin;
                            //dgrn.dgrn_ItmQty = string.IsNullOrEmpty(ItmQty) ? null : ItmQty;
                            //dgrn.dgrn_ItmInsp = string.IsNullOrEmpty(TbInsp) ? null : TbInsp;
                            //dgrn.dgrn_ItmReg = string.IsNullOrEmpty(Tbrej) ? null : Tbrej;
                            //dgrn.dgrn_ItmBal = string.IsNullOrEmpty(Tbbal) ? null : Tbbal;
                            //dgrn.dgrn_Rmk = string.IsNullOrEmpty(Tbrmk) ? null : Tbrmk;
                            //dgrn.mgrn_id = Mgrn_id;
                            //dgrn.ProductID = string.IsNullOrEmpty(ddlItm) ? null : ddlItm;

                            //tbl_dgrnManager dgrnmanag = new tbl_dgrnManager(dgrn);
                            //dgrnmanag.Save();
                        }
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

        public void Clear()
        {   
            BindDll();
            TBRmk.Text = "";
            SetInitRowPuritm();
        }

        private void ptnGRNNo()
        {
            try
            {
                string str = "select isnull(max(cast(mgrn_id as int)),0) as [mgrn_id]  from tbl_mgrn";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(TBGRNNum.Text))
                    {
                        int v = Convert.ToInt32(reader["mgrn_id"].ToString());
                        int b = v + 1;
                        TBGRNNum.Text = "GRN00" + b.ToString();
                    }
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

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                FillGrid();
                Clear();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }

        private void SetInitRowPuritm()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("ProNam", typeof(string)));
            dt.Columns.Add(new DataColumn("dgrn_ItmDes", typeof(string)));
            dt.Columns.Add(new DataColumn("dgrn_ItmQty", typeof(string)));
            dt.Columns.Add(new DataColumn("dgrn_ItmInsp", typeof(string)));
            dt.Columns.Add(new DataColumn("dgrn_ItmReg", typeof(string)));
            dt.Columns.Add(new DataColumn("dgrn_ItmBal", typeof(string)));
            dt.Columns.Add(new DataColumn("dgrn_Rmk", typeof(string)));

            dr = dt.NewRow();
            dr["ProNam"] = string.Empty;
            dr["dgrn_ItmDes"] = string.Empty;
            dr["dgrn_ItmQty"] = string.Empty;
            dr["dgrn_ItmInsp"] = "0.00";
            dr["dgrn_ItmReg"] = "0.00";
            dr["dgrn_ItmBal"] = string.Empty;
            dr["dgrn_Rmk"] = string.Empty;

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["dt_adItm"] = dt;

            GVGRNItems.DataSource = dt;
            GVGRNItems.DataBind();
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
                        DropDownList ddlPurItm = (DropDownList)GVGRNItems.Rows[rowIndex].Cells[0].FindControl("ddlPurItm");
                        TextBox ItmDscptin = (TextBox)GVGRNItems.Rows[rowIndex].Cells[1].FindControl("ItmDscptin");
                        TextBox ItmQty = (TextBox)GVGRNItems.Rows[rowIndex].Cells[2].FindControl("ItmQty");
                        TextBox TbInsp = (TextBox)GVGRNItems.Rows[rowIndex].Cells[3].FindControl("TbInsp");
                        TextBox Tbrej = (TextBox)GVGRNItems.Rows[rowIndex].Cells[4].FindControl("Tbrej");
                        TextBox Tbbal = (TextBox)GVGRNItems.Rows[rowIndex].Cells[5].FindControl("Tbbal");
                        TextBox Tbrmk = (TextBox)GVGRNItems.Rows[rowIndex].Cells[6].FindControl("Tbrmk");

                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["ProNam"] = ddlPurItm.SelectedValue;
                        dt.Rows[i - 1]["dgrn_ItmDes"] = ItmDscptin.Text;
                        dt.Rows[i - 1]["dgrn_ItmQty"] = ItmQty.Text;
                        dt.Rows[i - 1]["dgrn_ItmInsp"] = TbInsp.Text;
                        dt.Rows[i - 1]["dgrn_ItmReg"] = Tbrej.Text;
                        dt.Rows[i - 1]["dgrn_ItmBal"] = Tbbal.Text;
                        dt.Rows[i - 1]["dgrn_Rmk"] = Tbrmk.Text;

                        rowIndex++;

                        //float GTotal = 0, CRAmt = 0, DBAmt = 0;
                        //for (int j = 0; j < GVGRNItems.Rows.Count; j++)
                        //{
                        //    TextBox total = (TextBox)GVGRNItems.Rows[j].FindControl("TbAddPurNetTtl");
                        //    TextBox CRAmtttl = (TextBox)GVGRNItems.Rows[j].FindControl("Tbcramt");
                        //    TextBox DBAmtttl = (TextBox)GVGRNItems.Rows[j].FindControl("Tbdbamt");


                        //    GTotal += Convert.ToSingle(total.Text);
                        //    CRAmt += Convert.ToSingle(CRAmtttl.Text);
                        //    DBAmt += Convert.ToSingle(DBAmtttl.Text);

                        //}

                        //TBGrssTotal.Text = GTotal.ToString();
                        //TBCRAmtttl.Text = CRAmt.ToString();
                        //TBDBAmtttl.Text = DBAmt.ToString();

                        ddlPurItm.Focus();
                    }

                    dt.Rows.Add(drRow);
                    ViewState["dt_adItm"] = dt;

                    GVGRNItems.DataSource = dt;
                    GVGRNItems.DataBind();


                }
            }
            else
            {   
                Response.Write("ViewState is null");
            }

            //Set Previous Data on GRNstbacks
            SetPreRowitm();
        }

        protected void linkbtnadd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        private void SetPreRowitm()
        {
            try
            {
                BindDll();

                int rowIndex = 0;
                if (ViewState["dt_adItm"] != null)
                {
                    DataTable dt = (DataTable)ViewState["dt_adItm"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DropDownList ddlPurItm = (DropDownList)GVGRNItems.Rows[rowIndex].Cells[0].FindControl("ddlPurItm");
                            TextBox ItmDscptin = (TextBox)GVGRNItems.Rows[rowIndex].Cells[1].FindControl("ItmDscptin");
                            TextBox ItmQty = (TextBox)GVGRNItems.Rows[rowIndex].Cells[2].FindControl("ItmQty");
                            TextBox TbInsp = (TextBox)GVGRNItems.Rows[rowIndex].Cells[3].FindControl("TbInsp");
                            TextBox Tbrej = (TextBox)GVGRNItems.Rows[rowIndex].Cells[4].FindControl("Tbrej");
                            TextBox Tbbal = (TextBox)GVGRNItems.Rows[rowIndex].Cells[6].FindControl("Tbbal");
                            TextBox Tbrmk = (TextBox)GVGRNItems.Rows[rowIndex].Cells[7].FindControl("Tbrmk");


                            //TextBox TbAddPurTol = (TextBox)GVGRNItems.Rows[rowIndex].Cells[3].FindControl("TbAddPurTol");
                            //TextBox TbAddSubTotl = (TextBox)GVGRNItems.Rows[rowIndex].Cells[4].FindControl("TbAddSubTotl");
                            //TextBox TbAddPurSalTax = (TextBox)GVGRNItems.Rows[rowIndex].Cells[5].FindControl("TbAddPurSalTax");
                            //DropDownList DDLProTyp = (DropDownList)GVGRNItems.Rows[rowIndex].Cells[9].FindControl("DDLProTyp");

                            ddlPurItm.SelectedValue = dt.Rows[i]["ProNam"].ToString();
                            ItmDscptin.Text = dt.Rows[i]["dgrn_ItmDes"].ToString();
                            ItmQty.Text = dt.Rows[i]["dgrn_ItmQty"].ToString();
                            TbInsp.Text = dt.Rows[i]["dgrn_ItmInsp"].ToString();
                            Tbrej.Text = dt.Rows[i]["dgrn_ItmReg"].ToString();
                            Tbbal.Text = dt.Rows[i]["dgrn_ItmBal"].ToString();
                            Tbrmk.Text = dt.Rows[i]["dgrn_Rmk"].ToString();

                            rowIndex++;

                            // ADDTOTAL();


                        }
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

        protected void GVGRNItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

                    GVGRNItems.DataSource = dt;
                    GVGRNItems.DataBind();

                    SetPreRowitm();

                    //float GTotal = 0;
                    //for (int j = 0; j < GVGRNItems.Rows.Count; j++)
                    //{
                    //    TextBox total = (TextBox)GVGRNItems.Rows[j].FindControl("TbAddPurNetTtl");
                    //    GTotal += Convert.ToSingle(total.Text);
                    //}

                    //TBGrssTotal.Text = GTotal.ToString();
                }
            }
        }

        protected void TbAddCost_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < GVGRNItems.Rows.Count; i++)
                {
                    TextBox TbPurItmQty = (TextBox)GVGRNItems.Rows[i].FindControl("TbAddPurItmQty");
                    TextBox TbAddCost = (TextBox)GVGRNItems.Rows[i].FindControl("TbAddCost");
                    TextBox TbAddPurNetTt = (TextBox)GVGRNItems.Rows[i].FindControl("TbAddPurNetTtl");

                    TbAddPurNetTt.Text = (Convert.ToDouble(TbPurItmQty.Text) * Convert.ToDouble(TbAddCost.Text)).ToString();
                    //TBGrssTotal.Text = TbAddPurNetTt.Text;
                    //TbAddPurNetTt.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }


        protected void btnRevert_Click(object sender, EventArgs e)
        {
            Response.Redirect("AlPur_Inv.aspx");
        }

        protected void GVScrhrMGRN_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVScrhrMGRN.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void GVScrhrMGRN_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string MPurID = Server.HtmlDecode(GVScrhrMGRN.Rows[e.RowIndex].Cells[0].Text.ToString());
                string MPurNo = Server.HtmlDecode(GVScrhrMGRN.Rows[e.RowIndex].Cells[1].Text.ToString());

                SqlCommand cmd = new SqlCommand();

                cmd = new SqlCommand("sp_del_Pur", con);
                cmd.Parameters.Add("@mPurID", SqlDbType.Int).Value = MPurID;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                FillGrid();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = MPurNo + " has been Deleted!";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }

        protected void GVScrhrMGRN_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string cmdtxt;
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                //if (e.CommandName == "Delete")
                //{
                //    string MPurID = Server.HtmlDecode(GVScrhrMGRN.Rows[row.RowIndex].Cells[0].Text.ToString());

                //    cmd.Parameters.Add("@mPurID", SqlDbType.Int).Value = MPurID;
                //    cmd = new SqlCommand("sp_del_DPur", con);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    con.Open();
                //    cmd.ExecuteNonQuery();
                //    con.Close();
                //    FillGrid();
                //}
                //else 
                if (e.CommandName == "Select")
                {

                    string MGRNID = Server.HtmlDecode(GVScrhrMGRN.Rows[row.RowIndex].Cells[0].Text.ToString());

                    cmdtxt = "   select tbl_mgrn.mgrn_id,mgrn_dat,mgrn_sono,tbl_mgrn.ven_id,mgrn_rmk,tbl_mgrn.ISActive " +
                        " from tbl_mgrn inner join  tbl_dgrn on tbl_mgrn.mgrn_id = tbl_dgrn.mgrn_id " +
                        " inner join tbl_MPurOrd on tbl_mgrn.MPurOrdid = tbl_MPurOrd.MPurOrdid " +
                        " inner join t_ven on tbl_mgrn.ven_id = t_ven.ven_id where tbl_mgrn.mgrn_id ='" + MGRNID + "'";

                    SqlCommand cmdSlct = new SqlCommand(cmdtxt, con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmdSlct);

                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        HFMGRN.Value = dt.Rows[0]["mgrn_id"].ToString();
                        TBGRNNum.Text = dt.Rows[0]["mgrn_sono"].ToString();
                        TBGRNDat.Text = dt.Rows[0]["mgrn_dat"].ToString();
                        ddlVenNam.SelectedValue = dt.Rows[0]["ven_id"].ToString();
                        TBRmk.Text = dt.Rows[0]["mgrn_rmk"].ToString();
                        chk_Act.Checked = Convert.ToBoolean(dt.Rows[0]["ISActive"].ToString());


                        cmdtxt = " select tbl_dgrn.ProductID,dgrn_ItmDes,dgrn_ItmQty,dgrn_ItmInsp,dgrn_ItmReg,dgrn_ItmBal,dgrn_Rmk " +
                              " from tbl_dgrn inner join Products on tbl_dgrn.ProductID = Products.ProductID where mgrn_id = '" + MGRNID + "'";


                        SqlCommand cmdDslct = new SqlCommand(cmdtxt, con);
                        SqlDataAdapter dadp = new SqlDataAdapter(cmdDslct);

                        DataTable dt_Dgrn = new DataTable();
                        dadp.Fill(dt_Dgrn);

                        if (dt_Dgrn.Rows.Count > 0)
                        {
                            GVGRNItems.DataSource = dt_Dgrn;
                            GVGRNItems.DataBind();

                        }
                        else
                        {
                            GVGRNItems.DataSource = null;
                            GVGRNItems.DataBind();
                        }
                             //for (int i = 0; i < GVAddEdtPur.Rows.Count; i++)
                        //{
                        //    DropDownList PurProNam = (DropDownList)GVAddEdtPur.Rows[i].FindControl("ddl_EdtPurProNam");
                        //    PurProNam.SelectedValue = dt.Rows[0]["ProNam"].ToString();
                        //}

                        //GVGRNItems.Visible = false;
                        //TBGrssTotal.Text = dt1.Rows[0]["grossttal"].ToString();=
                        //GrandTotal();
                    }
                }
                }catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lbl_Heading.Text = "Error!";
                    lblalert.Text = ex.Message;
                }
        }

        protected void GVAddEdtPur_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        #region Services
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetVendr(string prefixText, int count)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["D"].ConnectionString;

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select distinct(suppliername) from supplier where " +
                    " suppliername like '%' + @SearchText + '%' and IsActive = True";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    List<string> DocumentNum = new List<string>();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            DocumentNum.Add(sdr["PurNo"].ToString());
                        }
                    }
                    conn.Close();
                    return DocumentNum;
                }
            }
        }

        #endregion



        protected void ddlVenNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string query = " select supplierId ,suppliername, AddressOne, mobile, CNIC, NTNNTRNo from supplier " +
                    " where supplierId = '" + ddlVenNam.SelectedValue + "' and IsActive = 'True'";

                cmd = new SqlCommand(query, con);
                con.Open();
                DataTable dtven = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dtven);
                cmd.ExecuteNonQuery();

                if (dtven.Rows.Count > 0)
                {
                    ddlVenNam.SelectedValue = dtven.Rows[0]["supplierId"].ToString();
                    //TBVenAdd.Text = dtven.Rows[0]["AddressOne"].ToString();
                    //TBVenCon.Text = dtven.Rows[0]["mobile"].ToString();
                    //TBNIC.Text = dtven.Rows[0]["CNIC"].ToString();
                    //TBNTN.Text = dtven.Rows[0]["NTNNTRNo"].ToString();

                    ddlVenNam.Enabled = true;
                    //TBVenAdd.Enabled = false;
                    //TBVenCon.Enabled = false;
                    //TBNIC.Enabled = false;
                    //TBNTN.Enabled = false;


                    DropDownList ddlPurItm = (DropDownList)GVGRNItems.Rows[0].FindControl("ddlPurItm");
                    ddlPurItm.Focus();

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
        protected void GVScrhrMGRN_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GVScrhrMGRN.PageIndex = e.NewSelectedIndex;
            FillGrid();
        }

        protected void TBSearchGRNNum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //if (TBSearchGRNNum.Text != "")
                //{
                //    DataTable dt_GRN = new DataTable();
                //    dt_GRN = tbl_mgrnManager.GetmgrnList();

                //    GVScrhrMGRN.DataSource = dt_GRN;
                //    GVScrhrMGRN.DataBind();
                //}
                //else if (TBSearchGRNNum.Text == "")
                //{
                //    FillGrid();
                //}
            }
            catch (Exception ex)
            {
                //throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        protected void DDl_Req_SelectedIndexChanged(object sender, EventArgs e)
        {
            // For Purchase Order

            //using (SqlCommand cmd = new SqlCommand())
            //{
            //    cmd.CommandText = " select distinct rtrim('[' + CAST(MPurOrdid AS VARCHAR(200)) + ']-' + MPurOrdsono ) as [MPurOrd], " +
            //        " MPurOrdid,tbl_MReq.MReq_id from tbl_MPurOrd " +
            //        " inner join tbl_MReq on tbl_MPurOrd.MReq_id = tbl_MReq.MReq_id  " +
            //        " where tbl_MReq.MReq_id = " + DDl_Req.SelectedValue.Trim() + "";

            //    cmd.Connection = con;
            //    con.Open();

            //    DataTable dtGRN = new DataTable();
            //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //    adp.Fill(dtGRN);

            //    DDl_GRN.DataSource = dtGRN;
            //    DDl_GRN.DataTextField = "MPurOrd";
            //    DDl_GRN.DataValueField = "MPurOrdid";
            //    DDl_GRN.DataBind();
            //    DDl_GRN.Items.Add(new ListItem("--Select--", "0"));

            //    con.Close();
            //}


        }

        protected void DDl_GRN_SelectedIndexChanged(object sender, EventArgs e)
        {
            // For Customer

            //using (SqlCommand cmd = new SqlCommand())
            //{
            //    cmd.CommandText = " select rtrim('[' + CAST(tbl_MPurOrd.CustomerID AS VARCHAR(200)) + ']-' + CustomerName ) as [CustomerName], " +
            //        " tbl_MPurOrd.CustomerID from tbl_MPurOrd inner join Customers_ on tbl_MPurOrd.CustomerID =  Customers_.CustomerID " +
            //        " where tbl_MPurOrd.MPurOrdid= " + DDl_GRN.SelectedValue.Trim() + "";

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

            // For Supplier

            //using (SqlCommand cmd = new SqlCommand())
            //{
            //    cmd.CommandText = " select distinct rtrim('[' + CAST(tbl_MReq.ven_id AS VARCHAR(200)) + ']-' + ven_nam ) as [ven_nam], tbl_MReq.ven_id " +
            //        " from tbl_MReq inner join  t_ven on tbl_MReq.ven_id = t_ven.ven_id " +
            //        " where tbl_MReq.MReq_id = " + DDl_Req.SelectedValue.Trim() +"";

            //    cmd.Connection = con;
            //    con.Open();

            //    DataTable dtSupNam = new DataTable();
            //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
            //    adp.Fill(dtSupNam);

            //    ddlVenNam.DataSource = dtSupNam;
            //    ddlVenNam.DataTextField = "ven_nam";
            //    ddlVenNam.DataValueField = "ven_id";
            //    ddlVenNam.DataBind();
            //    ddlVenNam.Items.Add(new ListItem("--Select--", "0"));

            //    con.Close();
            //}
        }

        protected void ddlPurItm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < GVGRNItems.Rows.Count; j++)
                {
                    DropDownList ddlPurItm = (DropDownList)GVGRNItems.Rows[j].FindControl("ddlPurItm");
                    TextBox TBItmDes = (TextBox)GVGRNItems.Rows[j].FindControl("TbaddPurItmDscptin");
                    TextBox TBItmQty = (TextBox)GVGRNItems.Rows[j].FindControl("TbAddPurItmQty");
                    TextBox Tbrat = (TextBox)GVGRNItems.Rows[j].FindControl("Tbrat");
                    DropDownList ddlPurUnit = (DropDownList)GVGRNItems.Rows[j].FindControl("ddlPurUnit");
                    TextBox TbItmCst = (TextBox)GVGRNItems.Rows[j].FindControl("TbAddCosts");
                    TextBox TBNetTtl = (TextBox)GVGRNItems.Rows[j].FindControl("TbAddPurNetTtl");


                    string query = "select ProductID,ProductName,ProductDiscriptions,ProductQuantity,Cost as [Rate], Unit, " +
                        " '' as [Net Total] " +
                        " from Products where ProductID = " + ddlPurItm.SelectedValue.Trim() + "";

                    SqlCommand cmd = new SqlCommand(query, con);
                    DataTable dt_ = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);

                    adp.Fill(dt_);

                    if (dt_.Rows.Count > 0)
                    {
                        TBItmDes.Text = dt_.Rows[0]["ProductDiscriptions"].ToString();
                        //TBItmQty.Text = "";
                        ddlPurUnit.SelectedItem.Text = dt_.Rows[0]["Unit"].ToString();
                        Tbrat.Text = "";
                        TbItmCst.Text = dt_.Rows[0]["Rate"].ToString();
                        //TBNetTtl.Text = dt_.Rows[0]["Net Total"].ToString();
                    }

                    TBItmQty.Focus();
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

        protected void TbAddPurItmQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < GVGRNItems.Rows.Count; j++)
                {
                    DropDownList ddlPurItm = (DropDownList)GVGRNItems.Rows[j].FindControl("ddlPurItm");
                    TextBox TBItmDes = (TextBox)GVGRNItems.Rows[j].FindControl("TbaddPurItmDscptin");
                    TextBox TBItmQty = (TextBox)GVGRNItems.Rows[j].FindControl("TbAddPurItmQty");
                    DropDownList ddlPurUnit = (DropDownList)GVGRNItems.Rows[j].FindControl("ddlPurUnit");
                    TextBox TbItmCst = (TextBox)GVGRNItems.Rows[j].FindControl("TbAddCosts");
                    TextBox TBNetTtl = (TextBox)GVGRNItems.Rows[j].FindControl("TbAddPurNetTtl");

                    //if (TBItmQty.Text != "" && TbItmCst.Text != "")
                    //{
                    //    TBNetTtl.Text = (Convert.ToDouble(TBItmQty.Text.Trim()) * Convert.ToDouble(TbItmCst.Text.Trim())).ToString();
                    //    float GTotal = 0;
                    //    //for (int j = 0; j < GVGRNItems.Rows.Count; j++)
                    //    {
                    //        TextBox total = (TextBox)GVGRNItems.Rows[j].FindControl("TbAddPurNetTtl");
                    //        GTotal += Convert.ToSingle(total.Text);
                    //    }

                    //    TBGrssTotal.Text = GTotal.ToString();
                    //    TBNetTtl.Focus();
                    //}
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }

        protected void DDL_Paytyp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (DDL_Paytyp.SelectedValue == "2")
            //{
            //    pnl_bnk.Visible = true;
            //    pnl_Chqno.Visible = true;
            //    pnl_chqamt.Visible = true;
            //    pnl_cshamt.Visible = false;
            //    DDL_Bnk.Focus();
            //}
            //else if (DDL_Paytyp.SelectedValue != "2")
            //{
            //    pnl_bnk.Visible = false;
            //    pnl_chqamt.Visible = false;
            //    pnl_Chqno.Visible = false;
            //    pnl_cshamt.Visible = true;
            //    TB_CshAmt.Focus();
            //}
        }

        protected void DDL_PO_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //For Vendor

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select rtrim('[' + CAST(MPurOrdsono AS VARCHAR(200)) + '] - ' + ven_nam) as [MGRN], " +
                                      " MPurOrdid from tbl_MPurOrd inner join t_ven on tbl_MPurOrd.ven_id = t_ven.ven_id " +
                                      " where MPurOrdid ='" + DDL_PO.SelectedValue.Trim() + "'";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtVen = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtVen);

                    
                    ddlVenNam.DataSource = dtVen;
                    ddlVenNam.DataTextField = "MGRN";
                    ddlVenNam.DataValueField = "MPurOrdid";
                    ddlVenNam.DataBind();
                    ddlVenNam.Items.Add(new ListItem("--Select--", "0"));

                    con.Close();
                }

                //For Details

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select tbl_DPurOrd.ProductID as [ItemID],rtrim('[' + CAST(tbl_DPurOrd.ProductID AS VARCHAR(200)) + " +
                        " '] - ' + ProductName ) as [Item] ,ProductDiscriptions as [dgrn_ItmDes],DPurOrd_Qty as [dgrn_ItmQty]  from tbl_MPurOrd inner join tbl_DPurOrd " +
                        " on tbl_MPurOrd.MPurOrdid = tbl_DPurOrd.MPurOrdid " +
                        " inner join Products on tbl_DPurOrd.ProductID = Products.ProductID " +
                        " where tbl_MPurOrd.MPurOrdid ='" + DDL_PO.SelectedValue.Trim() + "'";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtdtl = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtdtl);

                    if (dtdtl.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtdtl.Rows.Count; j++)
                        {
                        for (int i = 0; i < GVGRNItems.Rows.Count; i++)
                        {
                            DropDownList ddlitem = (DropDownList)GVGRNItems.Rows[i].FindControl("ddlPurItm");
                            TextBox ItmDscptin = (TextBox)GVGRNItems.Rows[i].FindControl("ItmDscptin");
                            TextBox ItmQty = (TextBox)GVGRNItems.Rows[i].FindControl("ItmQty");

                            ddlitem.SelectedValue= dtdtl.Rows[j]["ItemID"].ToString();
                            ItmDscptin.Text = dtdtl.Rows[j]["dgrn_ItmDes"].ToString();
                            ItmQty.Text = dtdtl.Rows[j]["dgrn_ItmQty"].ToString();
                            }
                        }
                    }
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
    }
}