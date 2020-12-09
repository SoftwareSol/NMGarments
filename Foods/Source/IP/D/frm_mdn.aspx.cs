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
using DataAccess;
using NHibernate;
using NHibernate.Criterion;

namespace Foods
{
    public partial class frm_mdn : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        DataTable dt_ = null;
        string query, Mdn_id, stkqty, salretnreasn, dstkqty;

        protected void Page_Load(object sender, EventArgs e)
        {
            v_sal.Text = "Please Inter Invoice Number";
            v_type.Text = "Please Select Return Type";
             #region Initials
            if (!this.IsPostBack)
            {
                con.Close();
               
                FillGrid();
                SetInitRowitm();
                ptnSno();
                TBDNDat.Text = DateTime.Now.ToShortDateString();
                TBSalDat.Text = DateTime.Now.ToShortDateString();
                chk_Act.Checked = true;
                chk_prtd.Checked = true;
                pnl_supbook.Visible = false;
                //
                //pnlsaldat.Visible = false;
                //pnl_supbook.Visible = false;
                //pnlsaldat.Visible = false;
                pnl_act.Visible = false;
                BindDll();
                TBReason.Text = "NO Reason";
            }

            #endregion
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetSalNo(string prefixText)
        {
            string CompanyID = HttpContext.Current.Session["CompanyID"].ToString();
            string BranchID = HttpContext.Current.Session["BranchID"].ToString();

            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            //string str = "select salreturnreason from tbl_salretnreasn   where salreturnreason like '" + prefixText + "%'";
            
            string str = "select MSal_sono from tbl_MSal where ISActive = 'True' and  CompanyId = '" +
                CompanyID + "' and BranchId= '" + BranchID + "' and MSal_sono like '%" + prefixText + "%' order by MSal_id desc";

            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        } 

        //select MSal_id,rtrim( MSal_sono + '-' + CONVERT(VARCHAR(10), MSal_dat, 105) ) as [MSal_dat] from tbl_MSal where ISActive = 'True' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' order by MSal_id desc

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetReason(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select salreturnreason from tbl_salretnreasn   where salreturnreason like '" + prefixText + "%'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        } 

        public string ids()
        {
           string str = "[1]";
           string[] tokens = str.Split('[');
           string last = tokens[tokens.Length - 1];
           return last;
        }

        public static String id(String text, String start, String end)
        {
            int pos1 = text.IndexOf(start) + start.Length;
            int pos2 = text.Substring(pos1).IndexOf(end);
            return text.Substring(pos1, pos2);
        }
        public string between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }

        #region Events

        protected void linkbtnadd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        protected void TBSearchDebtNot_TextChanged(object sender, EventArgs e)
        {
            SearchRecord();        
        }   

        protected void GVScrhMDN_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVScrhMDN.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void GVScrhMDN_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row;

                if (e.CommandName == "Show")
                {
                    row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    string MDNID = GVScrhMDN.DataKeys[row.RowIndex].Values[0].ToString();

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_mdn.aspx?ID=MDN&MDNID=" + MDNID + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                }

                if (e.CommandName == "Select")
                {
                    row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    string MDN = GVScrhMDN.DataKeys[row.RowIndex].Values[0].ToString();
                    //HFMDN.Value = MDN;

                    string query1 = "  select Mdn_id,Mdn_sono,CONVERT(VARCHAR(10), Mdn_dat, 105)  as [Mdn_dat],ISActive, " +
                        " MSal_id,CONVERT(VARCHAR(10), Mdn_SalDat, 105)  as [Mdn_SalDat],custacc,CustomerID," +
                        " SlmanID  from tbl_Mdn where Mdn_sono ='" + MDN + "'";

                    DataTable dtmdn = new DataTable();
                    SqlCommand cmd = new SqlCommand(query1, con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtmdn);

                    if (dtmdn.Rows.Count > 0)
                    {
                        HFMDN.Value = dtmdn.Rows[0]["Mdn_id"].ToString();
                        HFDebtNot.Value = dtmdn.Rows[0]["Mdn_sono"].ToString();
                        TBDNDat.Text = dtmdn.Rows[0]["Mdn_dat"].ToString();
                        chk_Act.Checked = Convert.ToBoolean(dtmdn.Rows[0]["ISActive"].ToString());
                        DDL_Sal.SelectedValue = dtmdn.Rows[0]["MSal_id"].ToString();
                        TBSalDat.Text = dtmdn.Rows[0]["Mdn_SalDat"].ToString();
                        ddl_Cust.SelectedValue = dtmdn.Rows[0]["custacc"].ToString();
                        //ddl_sup.SelectedValue = dtmdn.Rows[0]["SlmanID"].ToString();
                    }

                    string query2 = " select tbl_ddn.mdn_id,ddnid,tbl_ddn.ProductID as [ITEMS], " +
                        " rtrim('[' + CAST(tbl_ddn.ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], " +
                        " Ddn_ItmDes as [DESCRIPTIONS], Ddn_ItmQty as [QTY], Ddn_ItmUnt as [UNIT],Ddn_Rmk as [REMARKS] from tbl_mdn " +
                        " inner join tbl_ddn on tbl_mdn.mdn_id = tbl_ddn.mdn_id " +
                        " inner join products on tbl_ddn.ProductID = products.ProductID where tbl_mdn.Mdn_sono ='" + MDN + "'";

                    DataTable dtdn = new DataTable();
                    SqlCommand cmddn = new SqlCommand(query2, con);
                    SqlDataAdapter adpdn = new SqlDataAdapter(cmddn);
                    adpdn.Fill(dtdn);

                    if (dtdn.Rows.Count > 0)
                    {
                        int rowIndex = 0;
                        //for (int i = 0; i < GVDNItm.Rows.Count; i++)
                        //    {
                        for (int j = 0; j < dtdn.Rows.Count; j++)
                        {

                            DropDownList ddl_Itm = (DropDownList)GVDNItm.Rows[rowIndex].Cells[0].FindControl("ddl_Itm");
                            TextBox TbaddSalItmDscptin = (TextBox)GVDNItm.Rows[rowIndex].Cells[1].FindControl("TbaddSalItmDscptin");
                            TextBox TbAddSalItmQty = (TextBox)GVDNItm.Rows[rowIndex].Cells[2].FindControl("TbAddSalItmQty");
                            TextBox TbAddSalUnit = (TextBox)GVDNItm.Rows[rowIndex].Cells[3].FindControl("TbAddSalUnit");
                            TextBox TBDDNRmk = (TextBox)GVDNItm.Rows[rowIndex].Cells[4].FindControl("TBDDNRmk");
                            

                            ddl_Itm.SelectedValue = dtdn.Rows[j]["ITEMS"].ToString();
                            HFDDN.Value = dtdn.Rows[j]["ddnid"].ToString();
                            TbaddSalItmDscptin.Text = dtdn.Rows[j]["DESCRIPTIONS"].ToString();
                            TbAddSalItmQty.Text = dtdn.Rows[j]["QTY"].ToString();
                            TbAddSalUnit.Text = dtdn.Rows[j]["UNIT"].ToString();
                            TBDDNRmk.Text = dtdn.Rows[j]["REMARKS"].ToString();
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

        protected void GVScrhMDN_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string MDNID = GVScrhMDN.DataKeys[e.RowIndex].Values[0].ToString();

                SqlCommand cmd = new SqlCommand();

                cmd = new SqlCommand("sp_del_DN", con);
                cmd.Parameters.Add("@Mdn_id", SqlDbType.Int).Value = MDNID;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                FillGrid();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = MDNID + " has been Deleted!";
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

        protected void btnCancl_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void GVDNItm_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

                    GVDNItm.DataSource = dt;
                    GVDNItm.DataBind();

                    SetPreRowitm();
                }
            }
        }

        #endregion

        #region Methods

        private void Clear()
        {
            HFMDN.Value = "";
            HFDebtNot.Value = "";
            HFDDN.Value = "";
            TBSearchDebtNot.Text = "";
            TBSalDat.Text = DateTime.Now.ToShortDateString();
            TBSalDat.Text = DateTime.Now.ToShortDateString();
            SetInitRowitm();
            BindDll();

            //ddl_Cust.SelectedValue = "0";
            //ddl_sup.SelectedValue = "0";
            //ddl_bokr.SelectedValue = "0";
        }

        private void SearchRecord()
        {
            try
            {
                if (TBSearchDebtNot.Text != "")
                {
                    DataTable dt_po = new DataTable();

                    string queryString = " select distinct(tbl_Mdn.Mdn_id),Mdn_dat,tbl_Mdn.CreatedBy,tbl_Mdn.CreatedAt,tbl_Mdn.ISActive,SubHeadCategoriesName as [CustomerName] from tbl_Mdn " +
                       " inner join tbl_Ddn on   tbl_Mdn.Mdn_id = tbl_Ddn.Mdn_id " +
                        " inner join SubHeadCategories on tbl_Mdn.custacc = SubHeadCategories.SubHeadCategoriesGeneratedID where tbl_Mdn.Mdn_id='" + TBSearchDebtNot.Text.Trim() + "' and tbl_Mdn.ISActive = '1'" +// and" +
                        //" tbl_Mdn.CompanyId = '" + Session["CompanyID"] + "' and tbl_Mdn.BranchId= '" + Session["BranchID"] + "'" +
                        " order by tbl_Mdn.Mdn_id desc ";

                    dt_po = DBConnection.GetQueryData(queryString);

                    GVScrhMDN.DataSource = dt_po;
                    GVScrhMDN.DataBind();
                }
                else if (TBSearchDebtNot.Text == "")
                {
                    FillGrid();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void Save()
        {
            string MSalId = "";
            //using (SqlConnection consnection = new SqlConnection(connectionString))
            //{
            con.Open();

            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            // Start a local transaction.
            transaction = con.BeginTransaction("SalesReturnTrans");

            // Must assign both transaction object and connection 
            // to Command object for a pending local transaction
            command.Connection = con;
            command.Transaction = transaction;

            try
            {
                //salretnreasn

                command.CommandText = " select salretnreasn from tbl_salretnreasn where salreturnreason='" 
                    + TBReason.Text + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" 
                    + Session["BranchID"] + "'";

                SqlDataAdapter saladp = new SqlDataAdapter(command);

                DataTable saldt = new DataTable();
                saladp.Fill(saldt);

                if (saldt.Rows.Count > 0)
                {
                    salretnreasn = saldt.Rows[0]["salretnreasn"].ToString();
                }


                //Master Sales Return
                command.CommandText =
                    " INSERT INTO tbl_Mdn  ([Mdn_sono],[Mdn_dat],[Mdn_SalDat],[CustomerID],[custacc],[MSal_id],[SlmanID] " +
                    " ,[bkrID],[CreatedBy],[CreatedAt],[ISActive], [salretnreasn], [returntyp]) VALUES " +
                    " ('" + TBDebitNot.Text + "','" + TBDNDat.Text + "' ,'" + TBSalDat.Text + "' ,'"
                    + ddl_Cust.SelectedValue + "','" + ddl_Cust.SelectedValue + "' " +
                    " ,'" + HFSAL.Value + "' ,'" + ddl_sup.SelectedValue + "' ,'" + ddl_bokr.SelectedValue +
                    "'  ,'" + Session["Username"] + "' ,'" + DateTime.Today.ToString("MM/dd/yyyy") + "' ,'"
                    + chk_Act.Checked + "','" + salretnreasn + "','"+ DDLRetntyp.SelectedValue.Trim() +"')";

                command.ExecuteNonQuery();

                //Details Sales Return

                string TBTtl = "";
                command.CommandText = " select top 1 Mdn_id from tbl_Mdn order by Mdn_id desc ";

                SqlDataAdapter adp = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Mdn_id = dt.Rows[0]["Mdn_id"].ToString();
                }
                    foreach (GridViewRow g1 in GVDNItm.Rows)
                    {
                        string DNItm = (g1.FindControl("ddl_Itm") as DropDownList).SelectedValue;
                        string DNItmDscptin = (g1.FindControl("TbaddSalItmDscptin") as TextBox).Text;
                        string DNItmQty = (g1.FindControl("TbAddSalItmQty") as TextBox).Text;
                        string DNUnit = (g1.FindControl("TbAddSalUnit") as TextBox).Text;
                        string Rmk = (g1.FindControl("TBDDNRmk") as TextBox).Text;
                        TBTtl = (g1.FindControl("TBTtl") as TextBox).Text;
                        

                        command.CommandText = " INSERT INTO tbl_Ddn ([Ddn_ItmDes] ,[Ddn_ItmQty] ,[Ddn_ItmUnt] ,[Ddn_Rmk] " +
                            " ,[Mdn_id] ,[ProductID]) VALUES  ('" + DNItmDscptin + "'  ,'" + DNItmQty + "'" +
                            " ,'" + DNUnit + "','" + Rmk + "' ,'" + Mdn_id + "' ,'" + DNItm + "')";

                        command.ExecuteNonQuery();

                        DataTable dtstkqty = new DataTable();

                        command.CommandText = "select Dstk_Qty, dstkdef from tbl_Dstk where ProductID = '" + DNItm + "' and Dstk_unt='" + DNUnit + "'";

                        SqlDataAdapter Adapter = new SqlDataAdapter(command);
                        Adapter.Fill(dtstkqty);

                        if (dtstkqty.Rows.Count > 0)
                        {

                            for (int t = 0; t < dtstkqty.Rows.Count; t++)
                            {
                                stkqty = dtstkqty.Rows[t]["Dstk_Qty"].ToString();

                                decimal qty = Convert.ToDecimal(stkqty) + Convert.ToDecimal(DNItmQty);

                                command.CommandText = " UPDATE tbl_Dstk SET Dstk_Qty = " + qty + " , returntyp='" + DDLRetntyp.SelectedValue.Trim() + "' where  ProductID = '" + DNItm + "' and Dstk_unt='" + DNUnit + "'";
                                command.ExecuteNonQuery();

                                
                            }

                                for (int o = 0; o < dtstkqty.Rows.Count; o++)
                                {
                               

                                    dstkqty = dtstkqty.Rows[0]["dstkdef"].ToString();

                                    if (dstkqty == null || dstkqty == "")
                                    {
                                        dstkqty = "0";
                                    }
                                    decimal qt = Convert.ToDecimal(dstkqty) + Convert.ToDecimal(DNItmQty);

                                    command.CommandText = " UPDATE tbl_Dstk SET dstkdef = " + qt + " , returntyp='" + DDLRetntyp.SelectedValue.Trim() + "' where  ProductID = '" + DNItm + "' and Dstk_unt='" + DNUnit + "'";
                                    command.ExecuteNonQuery();
                                }

                          
                            
                        }

                    }

                    command.CommandText = "update tbl_msal set IsReturn='" + Mdn_id + "' where MSal_sono='" + TB_Sal.Text.Trim() + "'";
                    command.ExecuteNonQuery();

                    #region Credit Sheets


                    decimal avapre = 0;
                    decimal ttlcre = 0;

                    command.CommandText = "select CredAmt from tbl_Salcredit where CustomerID='" + ddl_Cust.SelectedValue.Trim() + "'";

                    SqlDataAdapter stksalcre = new SqlDataAdapter(command);

                    DataTable dtsalcre = new DataTable();
                    stksalcre.Fill(dtsalcre);

                    if (dtsalcre.Rows.Count > 0)
                    {
                        //double recv = Convert.ToDouble(lblOutstan) - Convert.ToDouble(TBRecy);

                        //avapre = Convert.ToDecimal(dtsalcre.Rows[0]["CredAmt"]);

                        //ttlcre = Convert.ToDecimal(lbl_ttl.Text.Trim()) - avapre;

                        command.CommandText = " Update tbl_Salcredit set CredAmt = '" + lbl_ttl.Text.Trim() + "' where CustomerID='" + ddl_Cust.SelectedValue.Trim() + "'";
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        command.CommandText = " insert into tbl_Salcredit (CustomerID,CredAmt) values('" + ddl_Cust.SelectedValue.Trim() + "','" + lbl_ttl.Text.Trim() + "')";
                        command.ExecuteNonQuery();
                    }

                    #endregion

                    #region Purchase Outstanding

                    command.CommandText = " Update tbl_MSal set Outstanding = '" + lbl_ttl.Text.Trim() + "' where MSal_id='" + HFSAL.Value + "'";
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
                Clear();
                TB_Sal.Focus();
                //Response.Redirect("frm_Sal.aspx");
            }
            //}
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
                            DropDownList ddl_Itm = (DropDownList)GVDNItm.Rows[rowIndex].Cells[0].FindControl("ddl_Itm");
                            TextBox TbaddSalItmDscptin = (TextBox)GVDNItm.Rows[rowIndex].Cells[1].FindControl("TbaddSalItmDscptin");
                            TextBox TbAddSalItmQty = (TextBox)GVDNItm.Rows[rowIndex].Cells[2].FindControl("TbAddSalItmQty");
                            TextBox TbAddSalUnit = (TextBox)GVDNItm.Rows[rowIndex].Cells[3].FindControl("TbAddSalUnit");
                            TextBox TBTtl = (TextBox)GVDNItm.Rows[rowIndex].Cells[4].FindControl("TBTtl");
                            TextBox TBamt = (TextBox)GVDNItm.Rows[rowIndex].Cells[5].FindControl("TBamt");
                            TextBox TBDDNRmk = (TextBox)GVDNItm.Rows[rowIndex].Cells[6].FindControl("TBDDNRmk");
                            
                            ddl_Itm.SelectedValue = dt.Rows[i]["ITEMS"].ToString();
                            TbaddSalItmDscptin.Text = dt.Rows[i]["DESCRIPTIONS"].ToString();
                            TbAddSalItmQty.Text = dt.Rows[i]["QTY"].ToString();
                            TbAddSalUnit.Text = dt.Rows[i]["UNIT"].ToString();
                            TBTtl.Text = dt.Rows[i]["rat"].ToString();
                            TBamt.Text = dt.Rows[i]["Amount"].ToString();
                            
                            TBDDNRmk.Text = dt.Rows[i]["REMARKS"].ToString();


                            rowIndex++;

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        public void SaveDetails()
        {
            try
            {
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
        private void SetInitRowitm()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("ITEMS", typeof(string)));
            dt.Columns.Add(new DataColumn("DESCRIPTIONS", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));
            dt.Columns.Add(new DataColumn("UNIT", typeof(string)));
            dt.Columns.Add(new DataColumn("rat", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("REMARKS", typeof(string)));
            
            dr = dt.NewRow();

            dr["ITEMS"] = string.Empty;
            dr["DESCRIPTIONS"] = string.Empty;
            dr["QTY"] = string.Empty;
            dr["UNIT"] = string.Empty;
            dr["rat"] = string.Empty;
            dr["Amount"] = string.Empty;
            dr["REMARKS"] = string.Empty;

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["dt_adItm"] = dt;

            GVDNItm.DataSource = dt;
            GVDNItm.DataBind();
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
                        DropDownList ddl_Itm = (DropDownList)GVDNItm.Rows[rowIndex].Cells[0].FindControl("ddl_Itm");
                        TextBox TbaddSalItmDscptin = (TextBox)GVDNItm.Rows[rowIndex].Cells[1].FindControl("TbaddSalItmDscptin");
                        TextBox TbAddSalItmQty = (TextBox)GVDNItm.Rows[rowIndex].Cells[2].FindControl("TbAddSalItmQty");
                        TextBox TbAddSalUnit = (TextBox)GVDNItm.Rows[rowIndex].Cells[3].FindControl("TbAddSalUnit");
                        TextBox TBTtl = (TextBox)GVDNItm.Rows[rowIndex].Cells[4].FindControl("TBTtl");
                        TextBox TBamt = (TextBox)GVDNItm.Rows[rowIndex].Cells[5].FindControl("TBamt");
                        TextBox TBDDNRmk = (TextBox)GVDNItm.Rows[rowIndex].Cells[6].FindControl("TBDDNRmk");


                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["ITEMS"] = ddl_Itm.SelectedValue;
                        dt.Rows[i - 1]["DESCRIPTIONS"] = TbaddSalItmDscptin.Text;
                        dt.Rows[i - 1]["QTY"] = TbAddSalItmQty.Text;
                        dt.Rows[i - 1]["UNIT"] = TbAddSalUnit.Text;
                        dt.Rows[i - 1]["rat"] = TBTtl.Text;
                        dt.Rows[i - 1]["Amount"] = TBamt.Text;
                        dt.Rows[i - 1]["REMARKS"] = TBDDNRmk.Text;


                        rowIndex++;

                        ddl_Itm.Focus();
                    }

                    dt.Rows.Add(drRow);
                    ViewState["dt_adItm"] = dt;

                    GVDNItm.DataSource = dt;
                    GVDNItm.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreRowitm();
        }

        public void FillGrid()
        {
            try
            {
                DataTable dt_ = new DataTable();
                query = " select distinct(tbl_Mdn.Mdn_id),Mdn_dat,tbl_Mdn.CreatedBy,tbl_Mdn.CreatedAt,tbl_Mdn.ISActive,SubHeadCategoriesName as [CustomerName] from tbl_Mdn inner join tbl_Ddn on   tbl_Mdn.Mdn_id = tbl_Ddn.Mdn_id " +
                    " inner join SubHeadCategories on tbl_Mdn.CustomerID = SubHeadCategories.SubHeadCategoriesGeneratedID where tbl_Mdn.ISActive = '1' " +
                    " order by tbl_Mdn.Mdn_id desc ";

                dt_ = DBConnection.GetQueryData(query);

                GVScrhMDN.DataSource = dt_;
                GVScrhMDN.DataBind();

                ViewState["GetMDN"] = dt_;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }

        }
        private void ptnSno()
        {
            try
            {
                string str = "select isnull(max(cast(Mdn_id as int)),0) as [Mdn_id]  from tbl_Mdn ";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(HFDebtNot.Value))
                    {
                        int v = Convert.ToInt32(reader["Mdn_id"].ToString());
                        int b = v + 1;
                        HFDebtNot.Value = "DN00" + b.ToString();
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
        public void BindDll()
        {
            try
            {
                con.Open();

                // For Bookers

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from Users where [Level]=3 and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                    cmd.Connection = con;

                    DataTable dtbokr = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtbokr);

                    ddl_bokr.DataSource = dtbokr;
                    ddl_bokr.DataTextField = "Username";
                    ddl_bokr.DataValueField = "Username";
                    ddl_bokr.DataBind();
                    ddl_bokr.Items.Add(new ListItem("--Select--", "0"));

                }

                // For Customer

                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandText = "select * from Customers_ where  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                    cmd.CommandText = " select * from SubHeadCategories  where SubHeadGeneratedID='0011'";
                    cmd.Connection = con;

                    DataTable dtCusNam = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtCusNam);

                    ddl_Cust.DataSource = dtCusNam;
                    ddl_Cust.DataTextField = "SubHeadCategoriesName";
                    ddl_Cust.DataValueField = "SubHeadCategoriesGeneratedID";
                    ddl_Cust.DataBind();
                    ddl_Cust.Items.Add(new ListItem("--Select--", "0"));

                }

                // For Supplier

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select * from supplier where  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                    cmd.Connection = con;

                    DataTable dtSupNam = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtSupNam);

                    ddl_sup.DataSource = dtSupNam;
                    ddl_sup.DataTextField = "suppliername";
                    ddl_sup.DataValueField = "supplierId";
                    ddl_sup.DataBind();
                    ddl_sup.Items.Add(new ListItem("--Select--", "0"));

                }

                // Sales 

                using (SqlCommand cmdpro = new SqlCommand())
                {
                    cmdpro.CommandText = " select MSal_id,rtrim( MSal_sono + '-' + CONVERT(VARCHAR(10), MSal_dat, 105) ) as [MSal_dat] from tbl_MSal where ISActive = 'True' and  CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' order by MSal_id desc";

                    cmdpro.Connection = con;

                    DataTable dtSal = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                    adp.Fill(dtSal);

                    DDL_Sal.DataSource = dtSal;
                    DDL_Sal.DataTextField = "MSal_dat";
                    DDL_Sal.DataValueField = "MSal_id";
                    DDL_Sal.DataBind();
                    DDL_Sal.Items.Add(new ListItem("--Select--", "0"));

                }

                con.Close();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }
        #endregion

        protected void GVDNItm_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try 
            {
                using (SqlCommand cmdpro = new SqlCommand())
                {   
                    con.Close();

                    cmdpro.CommandText = " select distinct(tbl_Dstk.ProductID), ProductName from tbl_Mstk  " +
                        " inner join tbl_Dstk on tbl_Mstk.Mstk_id = tbl_Dstk.Mstk_id " +
                        " inner join Products on tbl_Dstk.ProductID = Products.ProductID ";

                    cmdpro.Connection = con;
                    con.Open();

                    DataTable dtDNItm = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                    adp.Fill(dtDNItm);

                    for (int i = 0; i < GVDNItm.Rows.Count; i++)
                    {
                        DropDownList ddl_Itm = (DropDownList)GVDNItm.Rows[i].FindControl("ddl_Itm");

                        ddl_Itm.DataSource = dtDNItm;
                        ddl_Itm.DataTextField = "ProductName";
                        ddl_Itm.DataValueField = "ProductID";
                        ddl_Itm.DataBind();
                        ddl_Itm.Items.Add(new ListItem("--Select--", "0"));
                    }

                    con.Close();
                }


            }
            catch (Exception ex)
            {
                throw ex;
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

        protected void TBReason_TextChanged(object sender, EventArgs e)
        {
            query = "select * from tbl_salretnreasn where salreturnreason='" + TBReason.Text.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

            dt_ = DBConnection.GetQueryData(query);

            if (dt_.Rows.Count > 0)
            {
                //Do Noting
            }
            else
            {
                string reasonid = RandomNumber().ToString();

                query = " INSERT INTO tbl_salretnreasn " +
                                " ([salreturnreason],[CompanyId],[BranchId]) VALUES('" + TBReason.Text.Trim() 
                                + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        protected void TB_Sal_TextChanged(object sender, EventArgs e)
        {

        }


        protected void TbAddSalItmQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < GVDNItm.Rows.Count; j++)
                {
                    TextBox TbAddSalItmQty = (TextBox)GVDNItm.Rows[j].FindControl("TbAddSalItmQty");
                    TextBox TBTtl = (TextBox)GVDNItm.Rows[j].FindControl("TBTtl");
                    TextBox TBamt = (TextBox)GVDNItm.Rows[j].FindControl("TBamt");
                    Label lblqty = (Label)GVDNItm.Rows[j].FindControl("lblqty");
                    Label lblckqty = (Label)GVDNItm.Rows[j].FindControl("lblckqty");
                    
                     if (Convert.ToDecimal(TbAddSalItmQty.Text) > Convert.ToDecimal(lblqty.Text))
                    {
                        lblckqty.Text = "Quantity is not more then the given Quantity!";
                    }else
                    {
                        decimal ttlamt = Convert.ToDecimal(TbAddSalItmQty.Text.Trim()) * Convert.ToDecimal(TBTtl.Text.Trim());
                        
                        TBamt.Text = ttlamt.ToString();

                        //lbl_ttl.Text = (Convert.ToDecimal(lbl_ttl.Text) - Convert.ToDecimal(TBamt.Text)).ToString();
                    }
                }

                decimal GTotal = 0;
                TextBox total = null;

                for (int j = 0; j < GVDNItm.Rows.Count; j++)
                {
                    total = total = (TextBox)GVDNItm.Rows[j].FindControl("TBamt");

                    GTotal += Convert.ToDecimal(total.Text);
                }


                GTotal = Convert.ToDecimal(lbl_ttl.Text.Trim()) - Convert.ToDecimal(total.Text.Trim());
                lbl_ttl.Text = GTotal.ToString();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        protected void TB_Sal_TextChanged2(object sender, EventArgs e)
        {
            v_sal.Text = "";
            try
            {
                using (SqlCommand cmdpro = new SqlCommand())
                {
                    cmdpro.CommandText = " select MSal_sono, MSal_dat, custacc,Booker,SalMan,tbl_MSal.CustomerID,* from tbl_MSal " +
                    " inner join SubHeadCategories on tbl_MSal.CustomerID= SubHeadCategories.SubHeadCategoriesGeneratedID  " +
                    " where tbl_MSal.ISActive = 'True' and MSal_sono='" + TB_Sal.Text.Trim() + "'";

                    cmdpro.Connection = con;
                    con.Open();

                    DataTable dtSal = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                    adp.Fill(dtSal);
                    //AMT
                    if (dtSal.Rows.Count > 0)
                    {
                        TBSalDat.Text = dtSal.Rows[0]["MSal_dat"].ToString();
                        ddl_Cust.SelectedValue = dtSal.Rows[0]["custacc"].ToString();
                        //ddl_bokr.SelectedValue = dtSal.Rows[0]["Booker"].ToString();
                        HFSAL.Value = dtSal.Rows[0]["MSal_id"].ToString();
                        //ddl_sup.SelectedValue = dtSal.Rows[0]["ddl_sup"].ToString();
                    }

                    query = " select tbl_MSal.MSal_id,tbl_DSal.ProductID as [ITEMS],ProductName as [DESCRIPTIONS] , " +
                        " DSal_ItmQty as [QTY], DSal_ItmUnt as [UNIT],'' as [REMARKS],amt as [AMT],rat,(DSal_ItmQty * rat) as [Amount], " +
                        " * from tbl_MSal inner join tbl_DSal " +
                        " on tbl_MSal.MSal_id = tbl_DSal.MSal_id inner join Products " +
                        " on tbl_DSal.ProductID = Products.ProductID " +
                        // " inner join  tbl_unts on products.Unit = tbl_unts.untid" +
                        " where tbl_MSal.ISActive = 'True' " +
                        " and tbl_MSal.MSal_sono='" + TB_Sal.Text.Trim() + "'";

                    dt_ = DBConnection.GetQueryData(query);

                    ViewState["dt_adItm"] = dt_;

                    if (dt_.Rows.Count > 0)
                    {
                        GVDNItm.DataSource = dt_;
                        GVDNItm.DataBind();

                        //lbl_ttl.Text = dt_.Rows[0]["AMT"].ToString();

                    }

                    for (int i = 0; i < GVDNItm.Rows.Count; i++)
                    {
                        DropDownList ddl_Itm = (DropDownList)GVDNItm.Rows[i].Cells[0].FindControl("ddl_Itm");
                        Label lbl_itm = (Label)GVDNItm.Rows[i].Cells[0].FindControl("lbl_itm");

                        ddl_Itm.SelectedValue = lbl_itm.Text;

                    }
                    con.Close();

                    DataTable dtsalcre = new DataTable();
                    query = "select CredAmt from tbl_Salcredit where CustomerID='" + ddl_Cust.SelectedValue.Trim() + "'";

                    dtsalcre = DBConnection.GetQueryData(query);


                    if (dtsalcre.Rows.Count > 0)
                    {
                        lbl_ttl.Text = dtsalcre.Rows[0]["CredAmt"].ToString();
                    }
                    ddl_Cust.Focus();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }


        }

        protected void DDLRetntyp_TextChanged(object sender, EventArgs e)
        {
            v_type.Text = "";
        }
    }
}