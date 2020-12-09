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
using Foods;

using NHibernate;
using NHibernate.Criterion;

namespace Foods
{
    public partial class mcn : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        DataTable dt_ = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                FillGrid();
                SetInitRowitm();
                ptnSno();
                TBCNDat.Text = DateTime.Now.ToShortDateString();
                TBPurDat.Text = DateTime.Now.ToShortDateString();
                chk_Act.Checked = true;
                chk_prtd.Checked = true;
                BindDll();
            }
        }

        private void ptnSno()
        {
            try
            {
                string str = "select isnull(max(cast(Mcn_id as int)),0) as [MCNID]  from tbl_Mcn";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(HFCredtNot.Value))
                    {
                        int v = Convert.ToInt32(reader["MCNID"].ToString());
                        int b = v + 1;
                        HFCredtNot.Value = "CN00" + b.ToString();
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }
        private void SetInitRowitm()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("ITEMS", typeof(string)));
            dt.Columns.Add(new DataColumn("DESCRIPTIONS", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));
            dt.Columns.Add(new DataColumn("UNIT", typeof(string)));
            dt.Columns.Add(new DataColumn("REMARKS", typeof(string)));

            dr = dt.NewRow();

            dr["ITEMS"] = string.Empty;
            dr["DESCRIPTIONS"] = string.Empty;
            dr["QTY"] = string.Empty;
            dr["UNIT"] = string.Empty;
            dr["REMARKS"] = string.Empty;

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["dt_adItm"] = dt;

            GVCNItm.DataSource = dt;
            GVCNItm.DataBind();
        }

        public void FillGrid()
        {
            try
            {
                //DataTable dt_ = new DataTable();
                //dt_ = tbl_McnManager.GetMCNList();

                //GVScrhMcn.DataSource = dt_;
                //GVScrhMcn.DataBind();

                //ViewState["Mcn"] = dt_;
            }
            catch (Exception ex)
            {   
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }

        }

        public void BindDll()
        {
            try
            {
                // For Vendor

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select rtrim('[' + CAST(ven_id AS VARCHAR(200)) + ']-' + ven_nam ) as [ven_nam], ven_id from t_ven";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtSupNam = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtSupNam);

                    ddl_Ven.DataSource = dtSupNam;
                    ddl_Ven.DataTextField = "ven_nam";
                    ddl_Ven.DataValueField = "ven_id";
                    ddl_Ven.DataBind();
                    ddl_Ven.Items.Add(new ListItem("--Select--", "0"));

                    con.Close();
                }

                // For Supplier

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select rtrim('[' + CAST(employeeID AS VARCHAR(200)) + ']-' + employeeName ) as [employeeName], employeeID from tbl_employee";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtSupNam = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtSupNam);

                    ddl_sup.DataSource = dtSupNam;
                    ddl_sup.DataTextField = "employeeName";
                    ddl_sup.DataValueField = "employeeID";
                    ddl_sup.DataBind();
                    ddl_sup.Items.Add(new ListItem("--Select--", "0"));

                    con.Close();
                }

                // Purchase 

                using (SqlCommand cmdpro = new SqlCommand())
                {
                    cmdpro.CommandText = " select MPurID,rtrim( PurNo + '-' + CONVERT(VARCHAR(10), MPurDate, 105) ) as [MPurDate] from MPurchase where ck_Act = 'True'";

                    cmdpro.Connection = con;
                    con.Open();

                    DataTable dtPur = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                    adp.Fill(dtPur);

                    DDL_Pur.DataSource = dtPur;
                    DDL_Pur.DataTextField = "MPurDate";
                    DDL_Pur.DataValueField = "MPurID";
                    DDL_Pur.DataBind();
                    DDL_Pur.Items.Add(new ListItem("--Select--", "0"));

                    con.Close();
                }


                //Master Items Name

                using (SqlCommand cmdpro = new SqlCommand())
                {
                    cmdpro.CommandText = " select rtrim('[' + CAST(ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], ProductID from Products";
                  
                    cmdpro.Connection = con;
                    con.Open();

                    DataTable dtCNItm = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                    adp.Fill(dtCNItm);

                    for (int i = 0; i < GVCNItm.Rows.Count; i++)
                    {
                        DropDownList ddl_Itm = (DropDownList)GVCNItm.Rows[i].FindControl("ddl_Itm");

                        ddl_Itm.DataSource = dtCNItm;
                        ddl_Itm.DataTextField = "ProductName";
                        ddl_Itm.DataValueField = "ProductID";
                        ddl_Itm.DataBind();
                        ddl_Itm.Items.Add(new ListItem("--Select--", "0"));

                        //DropDownList ddl_Itms = (DropDownList)GVCNItm.Rows[i].FindControl("ddl_Itms");

                        //ddl_Itms.DataSource = dtCNItm;
                        //ddl_Itms.DataTextField = "ProductName";
                        //ddl_Itms.DataValueField = "ProductID";
                        //ddl_Itms.DataBind();
                        //ddl_Itms.Items.Add(new ListItem("--Select--", "0"));
                        //ddl_Itm.Visible = true;
                        //ddl_Itms.Visible = false;
                    }

                    con.Close();
                }

                ////Details Items Name

                //using (SqlCommand cmdpro = new SqlCommand())
                //{
                //    cmdpro.CommandText = " select rtrim('[' + CAST(ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], ProductID from Products";

                //    cmdpro.Connection = con;
                //    con.Open();

                //    DataTable dtCNEdt = new DataTable();
                //    SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                //    adp.Fill(dtCNEdt);

                //    for (int i = 0; i < GVCNEdt.Rows.Count; i++)
                //    {
                //        DropDownList ddl_EdtItm = (DropDownList)GVCNEdt.Rows[i].FindControl("ddl_EdtItm");

                //        ddl_EdtItm.DataSource = dtCNEdt;
                //        ddl_EdtItm.DataTextField = "ProductName";
                //        ddl_EdtItm.DataValueField = "ProductID";
                //        ddl_EdtItm.DataBind();
                //        ddl_EdtItm.Items.Add(new ListItem("--Select--", "0"));
                //    }

                //    con.Close();
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
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
                        DropDownList ddl_Itm = (DropDownList)GVCNItm.Rows[rowIndex].Cells[0].FindControl("ddl_Itm");
                        TextBox TbaddPurItmDscptin = (TextBox)GVCNItm.Rows[rowIndex].Cells[1].FindControl("TbaddPurItmDscptin");
                        TextBox TbAddPurItmQty = (TextBox)GVCNItm.Rows[rowIndex].Cells[2].FindControl("TbAddPurItmQty");
                        TextBox TbAddPurUnit = (TextBox)GVCNItm.Rows[rowIndex].Cells[3].FindControl("TbAddPurUnit");
                        TextBox TBDCNRmk = (TextBox)GVCNItm.Rows[rowIndex].Cells[4].FindControl("TBDCNRmk");


                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["ITEMS"] = ddl_Itm.SelectedValue;
                        dt.Rows[i - 1]["DESCRIPTIONS"] = TbaddPurItmDscptin.Text;
                        dt.Rows[i - 1]["QTY"] = TbAddPurItmQty.Text;
                        dt.Rows[i - 1]["UNIT"] = TbAddPurUnit.Text;
                        dt.Rows[i - 1]["REMARKS"] = TBDCNRmk.Text;


                        rowIndex++;

                        ddl_Itm.Focus();
                    }

                    dt.Rows.Add(drRow);
                    ViewState["dt_adItm"] = dt;

                    GVCNItm.DataSource = dt;
                    GVCNItm.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
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
                            DropDownList ddl_Itm = (DropDownList)GVCNItm.Rows[rowIndex].Cells[0].FindControl("ddl_Itm");
                            TextBox TbaddPurItmDscptin = (TextBox)GVCNItm.Rows[rowIndex].Cells[1].FindControl("TbaddPurItmDscptin");
                            TextBox TbAddPurItmQty = (TextBox)GVCNItm.Rows[rowIndex].Cells[2].FindControl("TbAddPurItmQty");
                            TextBox TbAddPurUnit = (TextBox)GVCNItm.Rows[rowIndex].Cells[3].FindControl("TbAddPurUnit");
                            TextBox TBDCNRmk = (TextBox)GVCNItm.Rows[rowIndex].Cells[4].FindControl("TBDCNRmk");

                            ddl_Itm.SelectedValue = dt.Rows[i]["ITEMS"].ToString();
                            TbaddPurItmDscptin.Text = dt.Rows[i]["DESCRIPTIONS"].ToString();
                            TbAddPurItmQty.Text = dt.Rows[i]["QTY"].ToString();
                            TbAddPurUnit.Text = dt.Rows[i]["UNIT"].ToString();
                            TBDCNRmk.Text = dt.Rows[i]["REMARKS"].ToString();
                           
                            rowIndex++;

                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        public void Save()
        {
            try
            {
                //tbl_Mcn mcn = new tbl_Mcn();

                //mcn.Mcn_id =  HFMCN.Value;
                //mcn.Mcn_sono = string.IsNullOrEmpty(HFCredtNot.Value) ? null : HFCredtNot.Value;
                //mcn.Mcn_dat = Convert.ToDateTime(string.IsNullOrEmpty(TBCNDat.Text) ? null : TBCNDat.Text);
                //mcn.Mcn_PurDat = string.IsNullOrEmpty(TBPurDat.Text) ? null : TBPurDat.Text;
                //mcn.ven_id = string.IsNullOrEmpty(ddl_Ven.SelectedValue) ? null : ddl_Ven.SelectedValue;
                //mcn.MPurID = string.IsNullOrEmpty(DDL_Pur.SelectedValue) ? null : DDL_Pur.SelectedValue; 
                //mcn.employeeID = string.IsNullOrEmpty(ddl_sup.SelectedValue) ? null : ddl_sup.SelectedValue;
                //mcn.CreatedBy = Session["user"].ToString();
                //mcn.CreatedAt = DateTime.Today;
                //mcn.ISActive = chk_Act.Checked.ToString();


                //tbl_McnManager cnmng = new tbl_McnManager(mcn);
                //cnmng.Save();

            }
            catch (Exception ex)
            {
                //throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        protected void GVCNItm_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

                    GVCNItm.DataSource = dt;
                    GVCNItm.DataBind();

                    SetPreRowitm();
                }
            }

        }

        protected void DDL_Pur_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand cmdpro = new SqlCommand())
                {
                    cmdpro.CommandText = " select PurNo,CONVERT(VARCHAR(10), MPurDate, 105)  as [MPurDate] from MPurchase where ck_Act = 'True'";

                    cmdpro.Connection = con;
                    con.Open();

                    DataTable dtPur = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                    adp.Fill(dtPur);

                    if (dtPur.Rows.Count > 0)
                    {
                        TBPurDat.Text = dtPur.Rows[0]["MPurDate"].ToString();
                    }
                    con.Close();
                    TBPurDat.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                SaveDetails();
                FillGrid();
                if (chk_prtd.Checked == true)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx','_blank','height=600px,width=600px,scrollbars=1');", true);
                }
                Clear();
                ptnSno();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Clear()
        {
            TBSearchCredtNot.Text = "";
            TBCNDat.Text = DateTime.Now.ToShortDateString();
            TBPurDat.Text = DateTime.Now.ToShortDateString();
            SetInitRowitm();
            BindDll();
        }

        public void SaveDetails()
        {
                try
                {
                    string cmdtxt = "select Mcn_id from tbl_Mcn where Mcn_sono= '" + HFCredtNot.Value.Trim() + "'";

                    SqlCommand cmdMPID = new SqlCommand(cmdtxt, con);

                    SqlDataAdapter adp = new SqlDataAdapter(cmdMPID);

                    DataTable dt = new DataTable();
                    adp.Fill(dt);


                    if (dt.Rows.Count > 0)
                    {
                        string Mcn_id = dt.Rows[0]["Mcn_id"].ToString();

                        foreach (GridViewRow g1 in GVCNItm.Rows)
                        {
                            string CNItm = (g1.FindControl("ddl_Itm") as DropDownList).SelectedValue;
                            string CNItmDscptin = (g1.FindControl("TbaddPurItmDscptin") as TextBox).Text;
                            string CNItmQty = (g1.FindControl("TbAddPurItmQty") as TextBox).Text;
                            string CNUnit = (g1.FindControl("TbAddPurUnit") as TextBox).Text;
                            string Rmk = (g1.FindControl("TBDCNRmk") as TextBox).Text;

                            //tbl_Dcn dcn = new tbl_Dcn();

                            //dcn.Dcnid = HFDCN.Value;
                            //dcn.Dcn_ItmDes = string.IsNullOrEmpty(CNItmDscptin) ? null : CNItmDscptin;
                            //dcn.Dcn_ItmQty = string.IsNullOrEmpty(CNItmQty) ? null : CNItmQty;
                            //dcn.Dcn_ItmUnt = string.IsNullOrEmpty(CNUnit) ? null : CNUnit;
                            //dcn.Dcn_Rmk = string.IsNullOrEmpty(Rmk) ? null : Rmk;
                            //dcn.Mcn_id = string.IsNullOrEmpty(Mcn_id) ? null : Mcn_id;
                            //dcn.ProductID = string.IsNullOrEmpty(CNItm) ? null : CNItm;

                            //tbl_DcnManager dcnmanag = new tbl_DcnManager(dcn);
                            //dcnmanag.Save();
                        }
                        //tran.Commit();
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = ex.Message;
                }
                finally
                {
                    //con.Close();
                }
            }


        protected void GVScrhMcn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "Select")
                {

                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    string MCN = GVScrhMcn.DataKeys[row.RowIndex].Values[1].ToString();
                    HFMCN.Value = MCN;

                    string query1 = " select Mcn_sono,CONVERT(VARCHAR(10), Mcn_dat, 105)  as [Mcn_dat],ISActive,MPurID,CONVERT(VARCHAR(10), Mcn_PurDat, 105)  as [Mcn_PurDat],ven_id, " +
                        " employeeID  from tbl_MCN where Mcn_sono = '" + HFMCN.Value + "'";

                    DataTable dtmcn = new DataTable();
                    SqlCommand cmd = new SqlCommand(query1, con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtmcn);

                    if (dtmcn.Rows.Count > 0)
                    {
                        TBCNDat.Text = dtmcn.Rows[0]["Mcn_dat"].ToString();
                        chk_Act.Checked = Convert.ToBoolean(dtmcn.Rows[0]["ISActive"].ToString());
                        DDL_Pur.SelectedValue = dtmcn.Rows[0]["MPurID"].ToString();
                        TBPurDat.Text = dtmcn.Rows[0]["Mcn_PurDat"].ToString();
                        ddl_Ven.SelectedValue = dtmcn.Rows[0]["ven_id"].ToString();
                        ddl_sup.SelectedValue = dtmcn.Rows[0]["employeeID"].ToString();
                    }

                    string query2 = "select tbl_dcn.mcn_id,dcnid,tbl_dcn.ProductID as [ITEMS],rtrim('[' + CAST(tbl_dcn.ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName],Dcn_ItmDes as [DESCRIPTIONS], " +
                        " Dcn_ItmQty as [QTY], Dcn_ItmUnt as [UNIT],Dcn_Rmk as [REMARKS] from tbl_mcn " +
                        " inner join tbl_dcn on tbl_mcn.mcn_id = tbl_dcn.mcn_id " +
                        " inner join products on tbl_dcn.ProductID = products.ProductID where tbl_mcn.Mcn_sono ='" + MCN + "'";

                    DataTable dtcn = new DataTable();
                    SqlCommand cmdcn = new SqlCommand(query2, con);
                    SqlDataAdapter adpcn = new SqlDataAdapter(cmdcn);
                    adpcn.Fill(dtcn);

                    if (dtcn.Rows.Count > 0)
                    {
                        int rowIndex = 0;
                        //for (int i = 0; i < GVCNItm.Rows.Count; i++)
                        //    {
                        for (int j = 0; j < dtcn.Rows.Count; j++)
                        {

                            DropDownList ddl_Itm = (DropDownList)GVCNItm.Rows[rowIndex].Cells[0].FindControl("ddl_Itm");
                            TextBox TbaddPurItmDscptin = (TextBox)GVCNItm.Rows[rowIndex].Cells[1].FindControl("TbaddPurItmDscptin");
                            TextBox TbAddPurItmQty = (TextBox)GVCNItm.Rows[rowIndex].Cells[2].FindControl("TbAddPurItmQty");
                            TextBox TbAddPurUnit = (TextBox)GVCNItm.Rows[rowIndex].Cells[3].FindControl("TbAddPurUnit");
                            TextBox TBDCNRmk = (TextBox)GVCNItm.Rows[rowIndex].Cells[4].FindControl("TBDCNRmk");

                      
                            ddl_Itm.SelectedValue = dtcn.Rows[j]["ITEMS"].ToString();
                            HFDCN.Value = dtcn.Rows[j]["dcnid"].ToString();
                            TbaddPurItmDscptin.Text = dtcn.Rows[j]["DESCRIPTIONS"].ToString();
                            TbAddPurItmQty.Text = dtcn.Rows[j]["QTY"].ToString();
                            TbAddPurUnit.Text = dtcn.Rows[j]["UNIT"].ToString();
                            TBDCNRmk.Text = dtcn.Rows[j]["REMARKS"].ToString();
                        }
                        //}
                        rowIndex++;
                    }
                    else
                    {
                        SetInitRowitm();
                    }
                    //FillMPurOrd(PurOrd);
                    //FillDPurOrd(PurOrd);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }

        }

        protected void GVScrhMcn_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string MCNID = Server.HtmlDecode(GVScrhMcn.Rows[e.RowIndex].Cells[0].Text.ToString());

                SqlCommand cmd = new SqlCommand();

                cmd = new SqlCommand("sp_del_CN", con);
                cmd.Parameters.Add("@Mcnsono", SqlDbType.NVarChar).Value = MCNID;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                FillGrid();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = MCNID + " has been Deleted!";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        protected void GVScrhMcn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVScrhMcn.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        private void SearchRecord()
        {
            try
            {
                FillGrid();
                
                DataTable _dt = (DataTable)ViewState["GetMCN"];
                DataView dv = new DataView(_dt, "Mcn_sono LIKE '%" + TBSearchCredtNot.Text.Trim().ToUpper() + "%'", "[Mcn_sono] ASC", DataViewRowState.CurrentRows);
                dt_ = dv.ToTable();

                GVScrhMcn.DataSource = dt_;
                GVScrhMcn.DataBind();
                ViewState["GetMCN"] = dt_;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void TBSearchCredtNot_TextChanged(object sender, EventArgs e)
        {
            SearchRecord();
        }

        protected void btnCancl_Click(object sender, EventArgs e)
        {
            Clear();
        }

    }
}
