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
using DataAccess;

using NHibernate;
using NHibernate.Criterion;

namespace Foods
{
    public partial class stockout_ : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        DataTable dt_;
        SqlTransaction tran;//= new SqlTransaction();
        string ddlstkItm;
        string ItmDscptin;
        string ItmQty;
        string Tbwght;
        string Tbunts;
        string Tbrat;
        string Tbsalrat;
        string Tbpurrat;
        string lblPurItm;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                SetInitRowPuritm();
                ptnSno();
                TBdat.Text = DateTime.Today.ToString("MM/dd/yyyy");
                BindDDl();
                FillGrid();
                TBSaldat.Text = DateTime.Today.ToString("MM/dd/yyyy");
                chk_act.Checked = true;
                chk_prt.Checked = true;
            }
        }

        private void SetInitRowPuritm()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("ProNam", typeof(string)));
            dt.Columns.Add(new DataColumn("Dstk_ItmDes", typeof(string)));
            dt.Columns.Add(new DataColumn("Dstk_ItmQty", typeof(string)));
            dt.Columns.Add(new DataColumn("Dstk_Itmwght", typeof(string)));
            dt.Columns.Add(new DataColumn("Dstk_ItmUnt", typeof(string)));
            dt.Columns.Add(new DataColumn("Dstk_rat", typeof(string)));
            dt.Columns.Add(new DataColumn("Dstk_salrat", typeof(string)));
            dt.Columns.Add(new DataColumn("Dstk_purrat", typeof(string)));

            dr = dt.NewRow();
            dr["ProNam"] = string.Empty;
            dr["Dstk_ItmDes"] = string.Empty;
            dr["Dstk_ItmQty"] = string.Empty;
            dr["Dstk_Itmwght"] = "0.00";
            dr["Dstk_ItmUnt"] = "0.00";
            dr["Dstk_rat"] = "0.00";
            dr["Dstk_salrat"] = "0.00";
            dr["Dstk_purrat"] = "0.00";

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["dt_adItm"] = dt;

            GVStkItems.DataSource = dt;
            GVStkItems.DataBind();
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
                        DropDownList ddlstkItm = (DropDownList)GVStkItems.Rows[rowIndex].Cells[0].FindControl("ddlstkItm");
                        TextBox ItmDscptin = (TextBox)GVStkItems.Rows[rowIndex].Cells[1].FindControl("ItmDscptin");
                        TextBox ItmQty = (TextBox)GVStkItems.Rows[rowIndex].Cells[2].FindControl("ItmQty");
                        TextBox Tbwght = (TextBox)GVStkItems.Rows[rowIndex].Cells[3].FindControl("Tbwght");
                        TextBox Tbunts = (TextBox)GVStkItems.Rows[rowIndex].Cells[4].FindControl("Tbunts");
                        TextBox Tbrat = (TextBox)GVStkItems.Rows[rowIndex].Cells[5].FindControl("Tbrat");
                        TextBox Tbsalrat = (TextBox)GVStkItems.Rows[rowIndex].Cells[6].FindControl("Tbsalrat");
                        TextBox Tbpurrat = (TextBox)GVStkItems.Rows[rowIndex].Cells[7].FindControl("Tbpurrat");

                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["ProNam"] = ddlstkItm.SelectedValue;
                        dt.Rows[i - 1]["Dstk_ItmDes"] = ItmDscptin.Text;
                        dt.Rows[i - 1]["Dstk_ItmQty"] = ItmQty.Text;
                        dt.Rows[i - 1]["Dstk_Itmwght"] = Tbwght.Text;
                        dt.Rows[i - 1]["Dstk_ItmUnt"] = Tbunts.Text;
                        dt.Rows[i - 1]["Dstk_rat"] = Tbrat.Text;
                        dt.Rows[i - 1]["Dstk_salrat"] = Tbsalrat.Text;
                        dt.Rows[i - 1]["Dstk_purrat"] = Tbpurrat.Text;

                        rowIndex++;

                        //float GTotal = 0, CRAmt = 0, DBAmt = 0;
                        //for (int j = 0; j < GVStkItems.Rows.Count; j++)
                        //{
                        //    TextBox total = (TextBox)GVStkItems.Rows[j].FindControl("TbAddPurNetTtl");
                        //    TextBox CRAmtttl = (TextBox)GVStkItems.Rows[j].FindControl("Tbcramt");
                        //    TextBox DBAmtttl = (TextBox)GVStkItems.Rows[j].FindControl("Tbdbamt");


                        //    GTotal += Convert.ToSingle(total.Text);
                        //    CRAmt += Convert.ToSingle(CRAmtttl.Text);
                        //    DBAmt += Convert.ToSingle(DBAmtttl.Text);

                        //}

                        //TBGrssTotal.Text = GTotal.ToString();
                        //TBCRAmtttl.Text = CRAmt.ToString();
                        //TBDBAmtttl.Text = DBAmt.ToString();

                        ddlstkItm.Focus();
                    }

                    dt.Rows.Add(drRow);
                    ViewState["dt_adItm"] = dt;

                    GVStkItems.DataSource = dt;
                    GVStkItems.DataBind();
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
                BindDDl();

                int rowIndex = 0;
                if (ViewState["dt_adItm"] != null)
                {
                    DataTable dt = (DataTable)ViewState["dt_adItm"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        { 
                            DropDownList ddlstkItm = (DropDownList)GVStkItems.Rows[rowIndex].Cells[0].FindControl("ddlstkItm");
                            TextBox ItmDscptin = (TextBox)GVStkItems.Rows[rowIndex].Cells[1].FindControl("ItmDscptin");
                            TextBox ItmQty = (TextBox)GVStkItems.Rows[rowIndex].Cells[2].FindControl("ItmQty");
                            TextBox Tbwght = (TextBox)GVStkItems.Rows[rowIndex].Cells[3].FindControl("Tbwght");
                            TextBox Tbunts = (TextBox)GVStkItems.Rows[rowIndex].Cells[4].FindControl("Tbunts");
                            TextBox Tbrat = (TextBox)GVStkItems.Rows[rowIndex].Cells[6].FindControl("Tbrat");
                            TextBox Tbsalrat = (TextBox)GVStkItems.Rows[rowIndex].Cells[7].FindControl("Tbsalrat");
                            TextBox Tbpurrat = (TextBox)GVStkItems.Rows[rowIndex].Cells[8].FindControl("Tbpurrat");


                            ddlstkItm.SelectedValue = dt.Rows[i]["ProNam"].ToString();
                            ItmDscptin.Text = dt.Rows[i]["Dstk_ItmDes"].ToString();
                            ItmQty.Text = dt.Rows[i]["Dstk_ItmQty"].ToString();
                            Tbwght.Text = dt.Rows[i]["Dstk_Itmwght"].ToString();
                            Tbunts.Text = dt.Rows[i]["Dstk_ItmUnt"].ToString();
                            Tbrat.Text = dt.Rows[i]["Dstk_rat"].ToString();
                            Tbsalrat.Text = dt.Rows[i]["Dstk_salrat"].ToString();
                            Tbpurrat.Text = dt.Rows[i]["Dstk_purrat"].ToString();

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

        public void FillGrid()
        {
            try
            {
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(" select tbl_Mstk.Mstk_id as [ID], convert(varchar(200), Mstk_dat, 101) as [Date],MSal_sono as [SNO],CustomerName, Mstk_Rmk as [Rmk] from tbl_Mstk inner join tbl_MSal on tbl_Mstk.MSal_id = tbl_MSal.MSal_id inner join Customers_ on tbl_Mstk.CustomerID = Customers_.CustomerID ");
                GVStckOut.DataSource = dt_;
                GVStckOut.DataBind();
                ViewState["StckOut"] = dt_;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }
        public void BindDDl()
        {
            try
            {

                //For Purchase
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select rtrim('[' + CAST(MSal_id AS VARCHAR(200)) + ']-' + MSal_sono ) as [Sales],MSal_id from tbl_MSal where ISActive = 'True' ";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtpur = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtpur);

                    if (dtpur.Rows.Count > 0)
                    {
                        
                        ddlStckOut.DataSource = dtpur;
                        ddlStckOut.DataTextField = "Sales";
                        ddlStckOut.DataValueField = "MSal_id";
                        ddlStckOut.DataBind();
                        ddlStckOut.Items.Add(new ListItem("--Select--", "0"));
                    }
                    con.Close();
                }


                //For Vendor
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select rtrim('[' + CAST(CustomerID AS VARCHAR(200)) + ']-' + CustomerName ) as [CustomerName], CustomerID from Customers_ where ISActive = 'True'";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtSupNam = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtSupNam);

                    if (dtSupNam.Rows.Count > 0)
                    {
                        ddl_cust.DataSource = dtSupNam;
                        ddl_cust.DataTextField = "CustomerName";
                        ddl_cust.DataValueField = "CustomerID";
                        ddl_cust.DataBind();
                        ddl_cust.Items.Add(new ListItem("--Select--", "0"));
                    }
                    con.Close();
                }

                //Items Name
                using (SqlCommand cmdpro = new SqlCommand())
                {
                    cmdpro.CommandText = " select rtrim('[' + CAST(ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], ProductID  from Products";

                    cmdpro.Connection = con;
                    con.Open();

                    DataTable dtItem = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                    adp.Fill(dtItem);

                    if (dtItem.Rows.Count > 0)
                    {

                        for (int i = 0; i < GVStkItems.Rows.Count; i++)
                        {
                            DropDownList ddlitem = (DropDownList)GVStkItems.Rows[i].FindControl("ddlstkItm");

                            ddlitem.DataSource = dtItem;
                            ddlitem.DataTextField = "ProductName";
                            ddlitem.DataValueField = "ProductID";
                            ddlitem.DataBind();
                            ddlitem.Items.Add(new ListItem("--Select--", "0"));
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


        private void ptnSno()
        {
            try
            {
                string str = "select isnull(max(cast(Mstk_id as int)),0) as [Mstk_id]  from tbl_Mstk";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(HFStkSONO.Value))
                    {
                        int v = Convert.ToInt32(reader["Mstk_id"].ToString());
                        int b = v + 1;
                        HFStkSONO.Value = "STK00" + b.ToString();
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
        private void Save()
        {
            string MSTkId = "";
            string cmdtxt, stkqty = "";

            //using (SqlConnection consnection = new SqlConnection(connectionString))
            //{
            con.Open();

            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            // Start a local transaction.
            transaction = con.BeginTransaction("StockTrans");

            // Must assign both transaction object and connection 
            // to Command object for a pending local transaction
            command.Connection = con;
            command.Transaction = transaction;

            try
            {
                //Master Stock
                command.CommandText =
                    " INSERT INTO tbl_Mstk(Mstk_sono, Mstk_dat, Mstk_PurDat, Mstk_Rmk, ven_id, CustomerID, MPurID, CreatedBy, CreatedAt, ISActive, MSal_id) " +
                    " VALUES " +
                    " ('" + HFStkSONO.Value + "','" + TBdat.Text.Trim() + "','" + TBSaldat.Text.Trim() + "','', '','" + ddl_cust.SelectedValue.Trim() + "','','" + Session["user"].ToString() + "','" + DateTime.Today.ToString("MM/dd/yyyy") +"','" + chk_act.Checked + "','" + ddlStckOut.SelectedValue.Trim() + "')";
                command.ExecuteNonQuery();

                command.CommandText = "select Mstk_id from tbl_Mstk where Mstk_sono = '" + HFStkSONO.Value.Trim() + "'";

                SqlDataAdapter adp = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    MSTkId = dt.Rows[0]["Mstk_id"].ToString();
                }

                DataTable dtstkqty = new DataTable(); 

                //Detail Stock

                foreach (GridViewRow g1 in GVStkItems.Rows)
                {

                    ddlstkItm = (g1.FindControl("ddlstkItm") as DropDownList).SelectedValue.Trim();
                    ItmDscptin = (g1.FindControl("ItmDscptin") as TextBox).Text.Trim();
                    ItmQty = (g1.FindControl("ItmQty") as TextBox).Text.Trim();
                    Tbwght = (g1.FindControl("Tbwght") as TextBox).Text.Trim();
                    Tbunts = (g1.FindControl("Tbunts") as TextBox).Text.Trim();
                    Tbrat = (g1.FindControl("Tbrat") as TextBox).Text.Trim();
                    Tbsalrat = (g1.FindControl("Tbsalrat") as TextBox).Text.Trim();
                    Tbpurrat = (g1.FindControl("Tbpurrat") as TextBox).Text.Trim();
                    lblPurItm = (g1.FindControl("lblPurItm") as Label).Text.Trim();

                    //dtstkqty = DataAccess.DBConnection.GetDataTable(cmdtxt);

                    command.CommandText = "select Dstk_ItmQty from tbl_Dstk where ProductID = " + lblPurItm + "";

                    SqlDataAdapter Adapter = new SqlDataAdapter(command);
                    Adapter.Fill(dtstkqty);

                    if (dtstkqty.Rows.Count > 0)
                    {
                        for (int t = 0; t < dtstkqty.Rows.Count; t++)
                        {
                            stkqty = dtstkqty.Rows[t]["Dstk_ItmQty"].ToString();

                            int qty = Convert.ToInt32(stkqty) - Convert.ToInt32(ItmQty);
                            command.CommandText = " UPDATE tbl_Dstk SET Dstk_ItmQty = " + qty + " where  ProductID = " + ddlstkItm + "";
                            command.ExecuteNonQuery();

                        }
                    }
                    else
                    {
                        command.CommandText = " INSERT INTO tbl_Dstk (Dstk_ItmDes, Dstk_ItmQty, Dstk_Itmwght, Dstk_ItmUnt, Dstk_rat, Dstk_salrat, Dstk_purrat, Mstk_id, ProductID) " +
                                " VALUES " +
                                " ('" + ItmDscptin + "','" + ItmQty + "', '" + Tbwght + "','" + Tbunts + "','" + Tbrat + "','" + Tbsalrat + "', '" + Tbpurrat + "', '" + MSTkId + "', '" + ddlstkItm + "')";
                        command.ExecuteNonQuery();

                    }
                }
                /*
                for (int j = 0; j < GVStkItems.Rows.Count; j++)
                {
                    Label lblPurItm = (Label)GVStkItems.Rows[j].FindControl("lblPurItm");
                    cmdtxt = "select Dstk_ItmQty from tbl_Dstk where ProductID = " + lblPurItm.Text.Trim() + "";
                    DataTable dtstkqty = new DataTable();
                    dtstkqty = DataAccess.DBConnection.GetDataTable(cmdtxt);

                    if (dtstkqty.Rows.Count > 0)
                    {
                        for (int t = 0; t < dtstkqty.Rows.Count; t++)
                        {
                            stkqty = dtstkqty.Rows[t]["Dstk_ItmQty"].ToString();
                        }
                    }
                }


                foreach (GridViewRow g1 in GVStkItems.Rows)
                {
                    string ddlstkItm = (g1.FindControl("ddlstkItm") as DropDownList).SelectedValue;
                    string ItmDscptin = (g1.FindControl("ItmDscptin") as TextBox).Text;
                    string ItmQty = (g1.FindControl("ItmQty") as TextBox).Text;
                    string Tbwght = (g1.FindControl("Tbwght") as TextBox).Text;
                    string Tbunts = (g1.FindControl("Tbunts") as TextBox).Text;
                    string Tbrat = (g1.FindControl("Tbrat") as TextBox).Text;
                    string Tbsalrat = (g1.FindControl("Tbsalrat") as TextBox).Text;
                    string Tbpurrat = (g1.FindControl("Tbpurrat") as TextBox).Text;

                    //command.CommandText =
                    //    " INSERT INTO tbl_Dstk (Dstk_ItmDes, Dstk_ItmQty, Dstk_Itmwght, Dstk_ItmUnt, Dstk_rat, Dstk_salrat, Dstk_purrat, Mstk_id, ProductID) " +
                    //    " VALUES " +
                    //    " ('" + ItmDscptin + "','" + ItmQty + "', '" + Tbwght + "','" + Tbunts + "','" + Tbrat + "','" + Tbsalrat + "', '" + Tbpurrat + "', '" + MSTkId + "', '" + ddlstkItm + "')";
                    //command.ExecuteNonQuery();

                    if (stkqty == "")
                    {
                        command.CommandText =
                        " INSERT INTO tbl_Dstk (Dstk_ItmDes, Dstk_ItmQty, Dstk_Itmwght, Dstk_ItmUnt, Dstk_rat, Dstk_salrat, Dstk_purrat, Mstk_id, ProductID) " +
                        " VALUES " +
                        " ('" + ItmDscptin + "','" + ItmQty + "', '" + Tbwght + "','" + Tbunts + "','" + Tbrat + "','" + Tbsalrat + "', '" + Tbpurrat + "', '" + MSTkId + "', '" + ddlstkItm + "')";
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        int qty = Convert.ToInt32(stkqty) - Convert.ToInt32(ItmQty);
                        command.CommandText = " UPDATE tbl_Dstk SET Dstk_ItmQty = " + qty + " where  ProductID = " + ddlstkItm + "";
                        command.ExecuteNonQuery();
                    }

                }
                */

                // Attempt to commit the transaction.
                transaction.Commit();

                if (chk_prt.Checked == true)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=PR','_blank','height=600px,width=600px,scrollbars=1');", true);
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
                //Clear();
                FillGrid();
                ptnSno();
            }
            //}
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }

        protected void ddlStckOut_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = " select CONVERT(VARCHAR(8), MSal_dat, 3) as [MSalDate], CustomerID from tbl_MSal where MSal_id = '" + ddlStckOut.SelectedValue.Trim() + "' and tbl_MSal.ISActive = 'True' ";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dpurdat = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dpurdat);

                    if (dpurdat.Rows.Count > 0)
                    {
                        //select CONVERT(VARCHAR(8), MPurDate, 3) as [MPurDate],ven_id,* from MPurchaseinner join DPurchase on MPurchase.MPurID = DPurchase.MPurID where MPurchase.MPurID = 20 and ck_Act = 'True'

                        TBSaldat.Text = dpurdat.Rows[0]["MSalDate"].ToString();
                        ddl_cust.SelectedValue = dpurdat.Rows[0]["CustomerID"].ToString();

                        cmd.CommandText = " select tbl_DSal.ProductID as [ProNam], Dsal_ItmDes as [Dstk_ItmDes], Dsal_ItmQty as [Dstk_ItmQty], " +
                            " '' as Dstk_Itmwght, Dsal_ItmUnt as [Dstk_ItmUnt] , Products.Cost as [Dstk_rat], " +
                            " '' as Dstk_salrat, '' as Dstk_purrat from tbl_MSal inner join tbl_DSal on tbl_MSal.MSal_id = tbl_DSal.MSal_id " +
                            " inner join Products on  tbl_DSal.ProductID = Products.ProductID " +
                            " where tbl_MSal.ISActive = 'True' and tbl_MSal.MSal_id  = '" + ddlStckOut.SelectedValue.Trim() + "'";

                        cmd.Connection = con;
                       

                        DataTable dtpurdtl = new DataTable();
                        SqlDataAdapter adppurdt = new SqlDataAdapter(cmd);
                        adppurdt.Fill(dtpurdtl);

                        if (dtpurdtl.Rows.Count > 0)
                        {
                            GVStkItems.DataSource = dtpurdtl;
                            GVStkItems.DataBind();

                            for (int i = 0; i < GVStkItems.Rows.Count; i++)
                            {
                                DropDownList ddlitem = (DropDownList)GVStkItems.Rows[i].FindControl("ddlstkItm");
                                Label lbl_pro = (Label)GVStkItems.Rows[i].FindControl("lblPurItm");

                                ddlitem.SelectedValue = lbl_pro.Text.Trim();
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

        protected void GVStckOut_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVStckOut.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void GVStckOut_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string MStkID = Server.HtmlDecode(GVStckOut.Rows[e.RowIndex].Cells[0].Text.ToString());

                SqlCommand cmd = new SqlCommand();

                cmd = new SqlCommand("sp_del_Stk", con);
                cmd.Parameters.Add("@MStkID", SqlDbType.Int).Value = MStkID;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                FillGrid();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = MStkID + " has been Deleted!";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }

        }

        protected void GVStkItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                string cmdtext = " select rtrim('[' + CAST(ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], ProductID  from Products";

                DataTable dtItem = new DataTable();
                dtItem = DataAccess.DBConnection.GetDataTable(cmdtext);

                if (dtItem.Rows.Count > 0)
                {
                    for (int i = 0; i < GVStkItems.Rows.Count; i++)
                    {
                        DropDownList ddlitem = (DropDownList)GVStkItems.Rows[i].FindControl("ddlstkItm");

                        ddlitem.DataSource = dtItem;
                        ddlitem.DataTextField = "ProductName";
                        ddlitem.DataValueField = "ProductID";
                        ddlitem.DataBind();
                        ddlitem.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}