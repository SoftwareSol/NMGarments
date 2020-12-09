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
    public partial class MProQuot : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_ = null;
        int i = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGrid();
                BindCust();               
                SetInitRowQuot();
                ck_act.Checked = true;
                lbl_err.ForeColor = System.Drawing.Color.Red;
                TBQuot_dat.Text = DateTime.Now.ToShortDateString();
                BindPro();
                txtsono.Enabled = false;
                ptnSno();
            }
        }

        #region Miscelleious

        private void ptnSno()
        {
            try
            {

                string str = "select isnull(max(cast(MProQuot_id as int)),0) as [MProQuot_id]  from tbl_MProQuot";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(txtsono.Text))
                    {
                        int v = Convert.ToInt32(reader["MProQuot_id"].ToString());
                        int b = v + 1;
                        txtsono.Text = "Quot00" + b.ToString();
                        //                        TBSubAccGeneratedID.Text = "SubAcc0" + b.ToString();
                        //                        TBAccountCategoryNameID.Text = "SubAccCat00" + b.ToString();
                        //TBaccountcategoriesfourGeneratedID.Text = "SubAccCat010" + b.ToString();
                        //TBsubaccountcategoriesfiveGeneratedID
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void FillGrid()
        {
            try
            {
                DataTable dtGetQuot_ = new DataTable();

                string queryString = " select MProQuot_sono,MProQuot_dat,CustomerName,MProQuot_rmk,  tbl_MProQuot.CreatedBy, tbl_MProQuot.CreatedAt,tbl_MProQuot.MProQuot_id,tbl_MProQuot.CustomerID, " +
                                    "  MProQuot_app, MProQuot_Rej, tbl_MProQuot.ISActive " +
                                    " from tbl_MProQuot " +
                                    " inner join Customers_ on tbl_MProQuot.CustomerID = Customers_.CustomerID " +
                                    " where MProQuot_app = 0 and MProQuot_Rej = 0  and tbl_MProQuot.ISActive = 1  order by tbl_MProQuot.MProQuot_id desc";

                dtGetQuot_ = DBConnection.GetQueryData(queryString);

                GVQuot.DataSource = dtGetQuot_;
                GVQuot.DataBind();
                ViewState["GetQuot"] = dtGetQuot_;
            }
            catch (Exception ex)
            {
                throw;
                //lbl_err.Text = ex.Message;                                 
            }
        }
        private void SetPreRowquot()
        {
            try
            {
                BindPro();
                int rowIndex = 0;
                if (ViewState["dt_adItm"] != null)
                {
                    DataTable dt = (DataTable)ViewState["dt_adItm"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DropDownList ddlpro = (DropDownList)GVDetQuot.Rows[rowIndex].Cells[0].FindControl("DDL_Pro");
                            TextBox Tbproqty = (TextBox)GVDetQuot.Rows[rowIndex].Cells[1].FindControl("TBProQty");
                            TextBox Tbprorat = (TextBox)GVDetQuot.Rows[rowIndex].Cells[2].FindControl("TBProrat");
                            TextBox Tbamt = (TextBox)GVDetQuot.Rows[rowIndex].Cells[3].FindControl("TBAmt");
                            TextBox Tbnarr = (TextBox)GVDetQuot.Rows[rowIndex].Cells[4].FindControl("TBNarr");

                            ddlpro.SelectedValue = dt.Rows[i]["PRODUCTS"].ToString();
                            Tbproqty.Text = dt.Rows[i]["QUANTITY"].ToString();
                            Tbprorat.Text = dt.Rows[i]["RATE"].ToString();
                            Tbamt.Text = dt.Rows[i]["AMOUNT"].ToString();
                            Tbnarr.Text = dt.Rows[i]["NARRATION"].ToString();
                            rowIndex++;

                            // ADDTOTAL();
                            Tbproqty.Focus();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        private void SetInitRowQuot()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("PRODUCTS", typeof(string)));
            dt.Columns.Add(new DataColumn("QUANTITY", typeof(string)));
            dt.Columns.Add(new DataColumn("RATE", typeof(string)));
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));
            dt.Columns.Add(new DataColumn("NARRATION", typeof(string)));

            dr = dt.NewRow();
            dr["PRODUCTS"] = string.Empty;
            dr["QUANTITY"] = string.Empty;
            dr["RATE"] = string.Empty;
            dr["AMOUNT"] = string.Empty;
            dr["NARRATION"] = string.Empty;

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["dt_adItm"] = dt;

            GVDetQuot.DataSource = dt;
            GVDetQuot.DataBind();
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
                        DropDownList DDLPro = (DropDownList)GVDetQuot.Rows[rowIndex].Cells[0].FindControl("DDL_Pro");
                        TextBox TBProQty = (TextBox)GVDetQuot.Rows[rowIndex].Cells[1].FindControl("TBProQty");
                        TextBox TBProrat = (TextBox)GVDetQuot.Rows[rowIndex].Cells[2].FindControl("TBProrat");
                        TextBox TBAmt = (TextBox)GVDetQuot.Rows[rowIndex].Cells[3].FindControl("TBAmt");
                        TextBox TBNarr = (TextBox)GVDetQuot.Rows[rowIndex].Cells[4].FindControl("TBNarr");

                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["PRODUCTS"] = DDLPro.Text;
                        dt.Rows[i - 1]["QUANTITY"] = TBProQty.Text;
                        dt.Rows[i - 1]["RATE"] = TBProrat.Text;
                        dt.Rows[i - 1]["AMOUNT"] = TBAmt.Text;
                        dt.Rows[i - 1]["NARRATION"] = TBNarr.Text;

                        rowIndex++;

                        float GTotal = 0;
                        for (int j = 0; j < GVDetQuot.Rows.Count; j++)
                        {
                            TextBox total = (TextBox)GVDetQuot.Rows[j].FindControl("TBAmt");
                            //GTotal = Convert.ToSingle(TbAddPurNetTtl.Text);
                            GTotal += Convert.ToSingle(total.Text);
                        }
                        TBGrssTotal.Text = GTotal.ToString();
                    }
                    dt.Rows.Add(drRow);
                    ViewState["dt_adItm"] = dt;

                    GVDetQuot.DataSource = dt;
                    GVDetQuot.DataBind();

                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreRowquot();
        }

        #endregion 

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Save();
        }

        protected void linkbtnadd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }

        public void Save()
        {
            try
            {
                tbl_MProQuot quot = new tbl_MProQuot();

                quot.MProQuot_id = HFQuotationID.Value;
                quot.MProQuot_dat = Convert.ToDateTime(string.IsNullOrEmpty(TBQuot_dat.Text) ? null : TBQuot_dat.Text);
                quot.MProQuot_rmk = string.IsNullOrEmpty(TBRmk.Text) ? null : TBRmk.Text;
                quot.CustomerID = string.IsNullOrEmpty(DDL_cust.SelectedValue) ? null : DDL_cust.SelectedValue;
                quot.CreatedBy = Session["user"].ToString();
                quot.CreatedAt = DateTime.Today;
                quot.ISActive = ck_act.Checked.ToString();
                quot.MProQuot_app = "0";
                quot.MProQuot_Rej = "0";
                quot.MProQuot_sono = string.IsNullOrEmpty(txtsono.Text) ? null : txtsono.Text;

                tbl_MProQuotManager quotmanag = new tbl_MProQuotManager(quot);

                quotmanag.Save();
                SavDet();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport&QUOTID=" + txtsono.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);


                FillGrid();
                Clear();
            }
            catch (Exception ex)
            {
                lbl_err.Text = ex.Message.ToString();
            }
        }

        #region Drop Down List

        public void BindCust()
        {
            try
            {
                DataTable dt_cust = new DataTable();

                string queryString = " select CustomerID,CustomerName from Customers_ where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_cust = DBConnection.GetQueryData(queryString);

                DDL_cust.DataSource = dt_cust;
                DDL_cust.DataTextField = "CustomerName";
                DDL_cust.DataValueField = "CustomerID";
                DDL_cust.DataBind();
                DDL_cust.Items.Add(new ListItem("--Select--", "0"));


            }
            catch (Exception ex)
            {
                lbl_err.Text = ex.Message.ToString();
            }
        }
        public void BindPro()
        {
            //Items Name

            using (SqlCommand cmdpro = new SqlCommand())
            {
                cmdpro.CommandText = " select ProductID, ProductName from Products ";

                cmdpro.Connection = con;
                con.Open();

                DataTable dtpro = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                adp.Fill(dtpro);

                //for (int j = 0; j < GVDetQuot.Rows.Count; j++)
                //        {
                //            TextBox total = (TextBox)GVDetQuot.Rows[j].FindControl("TBAmt");

                for (int i = 0; i < GVDetQuot.Rows.Count; i++)
                {
                    //DropDownList ddlitem = (DropDownList)GVDetQuot.Rows[i].FindControl("DDL_Pro");
                    DropDownList DDLPro = (DropDownList)GVDetQuot.Rows[i].Cells[0].FindControl("DDL_Pro");

                    DDLPro.DataSource = dtpro;
                    DDLPro.DataTextField = "ProductName";
                    DDLPro.DataValueField = "ProductID";
                    DDLPro.DataBind();
                }
                con.Close();
            }
        }
        #endregion

        #region
        public void Clear()
        {
            TBQuot_dat.Text = DateTime.Now.ToShortDateString();
            TBRmk.Text = "";
            TBGrssTotal.Text = "";
            GVDetQuot.DataSource = null;
            GVDetQuot.DataBind();
            SetInitRowQuot();
            BindPro();
        }
        #endregion

        #region

        private void SavDet()
        {
            try
            {
                string ProQuot_id = "";

                string cmds = "select max(cast(MProQuot_id as int)) as [MProQuot_id] from tbl_MProQuot";

                SqlCommand cmd = new SqlCommand(cmds,con);
                con.Open();

                DataTable dt_ = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt_);
                {
                    ProQuot_id = dt_.Rows[0]["MProQuot_id"].ToString();
                }
                if (dt_.Rows.Count > 0)
                {
                    foreach (GridViewRow g1 in GVDetQuot.Rows)
                    {
                        string DDLPro = (g1.FindControl("DDL_Pro") as DropDownList).SelectedValue;
                        string TBProQty = (g1.FindControl("TBProQty") as TextBox).Text;
                        string TBProrat = (g1.FindControl("TBProrat") as TextBox).Text;
                        string TBAmt = (g1.FindControl("TBAmt") as TextBox).Text;
                        string TBNarr = (g1.FindControl("TBNarr") as TextBox).Text;

                        tbl_DProQuot dquot = new tbl_DProQuot();

                        dquot.DProQuot_id = HFDQuotID.Value;
                        dquot.DProQuot_SNO = string.IsNullOrEmpty(TBProQty) ? null : TBProQty;
                        dquot.ProductID = string.IsNullOrEmpty(DDLPro) ? null : DDLPro;
                        dquot.DProQuot_rat = string.IsNullOrEmpty(TBProrat) ? null : TBProrat;
                        dquot.MProQuot_amt = string.IsNullOrEmpty(TBAmt) ? null : TBAmt;
                        dquot.MProQuot_tamt = string.IsNullOrEmpty(TBGrssTotal.Text.Trim()) ? null : TBGrssTotal.Text.Trim();
                        dquot.MProQuot_narr = string.IsNullOrEmpty(TBNarr) ? null : TBNarr;
                        dquot.CreatedBy = Session["user"].ToString();
                        dquot.CreatedAt = DateTime.Now;
                        dquot.ISActive = "1";
                        dquot.MProQuot_id = string.IsNullOrEmpty(ProQuot_id) ? null : ProQuot_id;

                        tbl_DProQuotManager dproquotmanag = new tbl_DProQuotManager(dquot);
                        dproquotmanag.Save();
                    }

                }
                else
                {
                    lbl_err.Text = "Sory Record has not been Saved! Inner Error";
                }
                con.Close();

            }
            catch (Exception ex)
            {
                lbl_err.Text = ex.Message.ToString();
            }

        }

        #endregion

        protected void GVDetQuot_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

                    GVDetQuot.DataSource = dt;
                    GVDetQuot.DataBind();

                    SetPreRowquot();

                    float GTotal = 0;
                    for (int j = 0; j < GVDetQuot.Rows.Count; j++)
                    {
                        TextBox total = (TextBox)GVDetQuot.Rows[j].FindControl("TBAmt");
                        //GTotal = Convert.ToSingle(TbAddPurNetTtl.Text);
                        GTotal += Convert.ToSingle(total.Text);
                    }
                    TBGrssTotal.Text = GTotal.ToString();
                    ptnSno();
                }                
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }
        
        protected void GVQuot_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVQuot.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void GVQuot_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                if (e.CommandName == "Show")
                {
                    string QOUTSNO = Server.HtmlDecode(GVQuot.Rows[row.RowIndex].Cells[0].Text.ToString());

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport&QUOTID=" + QOUTSNO + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                }
                ////if (e.CommandName == "Delete")
                ////{
                ////    string MPurID = Server.HtmlDecode(GVQuot.Rows[row.RowIndex].Cells[0].Text.ToString());

                ////    cmd.Parameters.Add("@mPurID", SqlDbType.Int).Value = MPurID;
                ////    cmd = new SqlCommand("sp_del_DPur", con);
                ////    cmd.CommandType = CommandType.StoredProcedure;
                ////    con.Open();
                ////    cmd.ExecuteNonQuery();
                ////    con.Close();
                ////    FillGrid();
                ////}
                ////else 
                //if (e.CommandName == "Select")
                //{

                //    string MPurID = Server.HtmlDecode(GVQuot.Rows[row.RowIndex].Cells[0].Text.ToString());

                //    string cmdtxt = " select * from MPurchase where MPurID ='" + MPurID + "'";
                //    //string cmdtxt = " select a.mPurID, b.dPurId, b.mPurId, a.ven_id, a.VndrAdd, a.VndrCntct,a.PurNo, a.mPurDate, a.CreatedBy, a.CreatedAt, a.cnic, a.ntnno, a.tobePrntd,b.Dpurid, b.ProNam, b.ProDes, b.Qty, b.Total, b.subtotl, b.unit, b.cost, b.protyp,b.grossttal from MPurchase a  inner join DPurchase b on a.mPurID = b.mPurID where a.MPurID =" + MPurID + "";
                //    SqlCommand cmdSlct = new SqlCommand(cmdtxt, con);
                //    SqlDataAdapter adp = new SqlDataAdapter(cmdSlct);

                //    DataTable dt = new DataTable();
                //    adp.Fill(dt);

                //    if (dt.Rows.Count > 0)
                //    {
                //        //TBPONum.Text = dt.Rows[0]["PurNo"].ToString();
                //        //TBPurDat.Text = dt.Rows[0]["mPurDate"].ToString();
                //        //ddlVenNam.SelectedValue = dt.Rows[0]["ven_id"].ToString();
                //        //TBVenAdd.Text = dt.Rows[0]["VndrAdd"].ToString();
                //        //TBVenCon.Text = dt.Rows[0]["VndrCntct"].ToString();
                //        //TBNIC.Text = dt.Rows[0]["cnic"].ToString();
                //        //TBNTN.Text = dt.Rows[0]["ntnno"].ToString();
                //        //ckprntd.Checked = Convert.ToBoolean(dt.Rows[0]["tobePrntd"].ToString());

                //        //DataTable dt_Dpur = new DataTable();
                //        //dt_Dpur = MPurchaseManager.GetDPurList(MPurID);

                //        //GVPurItems.Visible = true;
                //        //GVPurItems.DataSource = dt_Dpur;
                //        //GVPurItems.DataBind();

                //        //for (int i = 0; i < GVAddEdtPur.Rows.Count; i++)
                //        //{
                //        //    DropDownList PurProNam = (DropDownList)GVAddEdtPur.Rows[i].FindControl("ddl_EdtPurProNam");
                //        //    PurProNam.SelectedValue = dt.Rows[0]["ProNam"].ToString();
                //        //}

                //        //GVPurItems.Visible = false;
                //        //TBGrssTotal.Text = dt1.Rows[0]["grossttal"].ToString();=
                //        //GrandTotal();
                //    }

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void GVQuot_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string MProQuotID = Server.HtmlDecode(GVQuot.Rows[e.RowIndex].Cells[0].Text.ToString());

                SqlCommand cmd = new SqlCommand();

                cmd = new SqlCommand("sp_del_Quot", con);
                cmd.Parameters.Add("@MProQuot_sono", SqlDbType.VarChar).Value = MProQuotID;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                FillGrid();

                lbl_err.Text = "Quotation # " + MProQuotID + " has been Deleted!";

            }
            catch (Exception ex)
            {                
                lbl_err.Text = ex.Message;
            }
        }

        protected void SeacrhBtn_Click(object sender, EventArgs e)
        {
            SearchRecord();
        }

        private void SearchRecord()
        {
            try
            {
                FillGrid();
                DataTable _dt = (DataTable)ViewState["GetQuot"];
                DataView dv = new DataView(_dt, "CustomerName LIKE '%" + TBQuotations.Text.Trim().ToUpper() + "%'", "[CustomerName] ASC", DataViewRowState.CurrentRows);
                dt_ = dv.ToTable();
                GVQuot.DataSource = dt_;
                GVQuot.DataBind();
                ViewState["GetQuot"] = dt_;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}