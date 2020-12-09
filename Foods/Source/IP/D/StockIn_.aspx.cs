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
    public partial class StockIn_ : System.Web.UI.Page
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
                TBdat.Text = DateTime.Today.ToString("yyyy/MM/dd");//DateTime.Now.ToShortDateString();
                BindDDl();
                FillGrid();
                TBPurdat.Text = DateTime.Today.ToString("yyyy/MM/dd");//DateTime.Now.ToShortDateString();
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
                dt_ = DBConnection.GetQueryData(" select tbl_Mstk.Mstk_id as [ID], convert(varchar(200), Mstk_dat, 101) as [Date],suppliername as [VendorName],PurNo as [PNO], Mstk_Rmk as [Rmk] from tbl_Mstk inner join MPurchase on tbl_Mstk.MPurID = MPurchase.MPurID inner join supplier on tbl_Mstk.ven_id = supplier.supplierId");
                
                if (dt_.Rows.Count > 0)
                {
                    GVStckIn.DataSource = dt_;
                    GVStckIn.DataBind();
                    ViewState["StckIn"] = dt_;
                }
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
                    cmd.CommandText = " select rtrim('[' + CAST(MPurID AS VARCHAR(200)) + ']-' + PurNo ) as [Purchase],MPurID from MPurchase where ck_Act = 'True'";

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtpur = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtpur);

                    if (dtpur.Rows.Count > 0)
                    {

                        ddlstckIn.DataSource = dtpur;
                        ddlstckIn.DataTextField = "Purchase";
                        ddlstckIn.DataValueField = "MPurID";
                        ddlstckIn.DataBind();
                        ddlstckIn.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                    con.Close();
                }


                //For Vendor
                using (SqlCommand cmd = new SqlCommand())
                {
                    //cmd.CommandText = " select rtrim('[' + CAST(ven_id AS VARCHAR(200)) + ']-' + ven_nam ) as [ven_nam], ven_id from t_ven";
                    cmd.CommandText = "select rtrim('[' + CAST(supplierId AS VARCHAR(200)) + ']-' + suppliername ) as [suppliername], supplierId from supplier where IsActive = 1";
                    cmd.Connection = con;
                    con.Open();

                    DataTable dtSupNam = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtSupNam);

                    if (dtSupNam.Rows.Count > 0)
                    {
                        ddl_ven.DataSource = dtSupNam;
                        ddl_ven.DataTextField = "suppliername";
                        ddl_ven.DataValueField = "supplierId";
                        ddl_ven.DataBind();
                        ddl_ven.Items.Insert(0, new ListItem("--Select--", "0"));
                    }
                    con.Close();
                }

                ////Items Name
                //using (SqlCommand cmdpro = new SqlCommand())
                //{
                //    cmdpro.CommandText = " select rtrim('[' + CAST(ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], ProductID  from Products";

                //    cmdpro.Connection = con;
                //    con.Open();

                //    DataTable dtItem = new DataTable();
                //    SqlDataAdapter adp = new SqlDataAdapter(cmdpro);
                //    adp.Fill(dtItem);

                //    if (dtItem.Rows.Count > 0)
                //    {

                //        for (int i = 0; i < GVStkItems.Rows.Count; i++)
                //        {
                //            DropDownList ddlitem = (DropDownList)GVStkItems.Rows[i].FindControl("ddlstkItm");

                //            ddlitem.DataSource = dtItem;
                //            ddlitem.DataTextField = "ProductName";
                //            ddlitem.DataValueField = "ProductID";
                //            ddlitem.DataBind();
                //            ddlitem.Items.Add(new ListItem("--Select--", "0"));
                //        }

                //    }
                //    con.Close();
                //}

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

        private int Save()
        {
            string MSTkId = "";
            string cmdtxt, stkqty = "";

            int i = 1;
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
                //AddPurchase();
                command.CommandText =
                    " INSERT INTO tbl_Mstk(Mstk_sono, Mstk_dat, Mstk_PurDat, Mstk_Rmk, ven_id, CustomerID, MPurID, CreatedBy, CreatedAt, ISActive) " +
                    " VALUES " +
                    " ('" + HFStkSONO.Value + "','" + TBdat.Text.Trim() + "','" + TBPurdat.Text.Trim() + "','','" + ddl_ven.SelectedValue.Trim() + "', '','" + ddlstckIn.SelectedValue.Trim() + "','" + Session["user"].ToString() + "','" + DateTime.Today.ToString("yyyy/MM/dd") + "','" + chk_act.Checked + "')";
                command.ExecuteNonQuery();

                // Master Purchase
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

                    command.CommandText ="select Dstk_ItmQty from tbl_Dstk where ProductID = " + lblPurItm + "";

                    SqlDataAdapter Adapter = new SqlDataAdapter(command);
                    Adapter.Fill(dtstkqty);
           
                    if (dtstkqty.Rows.Count > 0)
                    {
                        for (int t = 0; t < dtstkqty.Rows.Count; t++)
                        {
                            stkqty = dtstkqty.Rows[t]["Dstk_ItmQty"].ToString();

                            int qty = Convert.ToInt32(stkqty) + Convert.ToInt32(ItmQty);
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

        return i;
       }

        //private int Save()
        //{
        //    string cmdtxt = "";
        //    int i = 1;

        //    con.Open();

        //    SqlCommand command = con.CreateCommand();
        //    SqlTransaction transaction;

        //    // Start a local transaction.
        //    transaction = con.BeginTransaction("StockTrans");

        //    // Must assign both transaction object and connection 
        //    // to Command object for a pending local transaction
        //    command.Connection = con;
        //    command.Transaction = transaction;

        //    try
        //    {
        //        command.CommandText =
        //            " INSERT INTO tbl_Mstk(Mstk_sono, Mstk_dat, Mstk_PurDat, Mstk_Rmk, ven_id, CustomerID, MPurID, CreatedBy, CreatedAt, ISActive) " +
        //            " VALUES " +
        //            " ('" + HFStkSONO.Value + "','" + TBdat.Text.Trim() + "','" + TBPurdat.Text.Trim() + "','','" + ddl_ven.SelectedValue.Trim() + "', '','" + ddlstckIn.SelectedValue.Trim() + "','" + Session["user"].ToString() + "','" + DateTime.Today.ToString() + "','" + chk_act.Checked + "')";
        //        command.ExecuteNonQuery();


        //        // Master Purchase
        //        cmdtxt = "select Mstk_id from tbl_Mstk where Mstk_sono = '" + HFStkSONO.Value.Trim() + "'";
        //        DataTable dtstkid = new DataTable();

        //        dtstkid = DataAccess.DBConnection.GetDataTable(cmdtxt);

        //        /*
        //        SqlDataAdapter adp = new SqlDataAdapter(cmd,con);
        //        adp.Fill(dt);

        //        if (dt.Rows.Count > 0)
        //        {
        //            HFStckIn.Value = dt.Rows[0]["Mstk_id"].ToString();
        //            Mstk_id = HFStckIn.Value;
        //        }

        //        */
        //        //Detail Stock

        //        foreach (GridViewRow g1 in GVStkItems.Rows)
        //        {
        //            string ddlstkItm = (g1.FindControl("ddlstkItm") as DropDownList).SelectedValue;
        //            string ItmDscptin = (g1.FindControl("ItmDscptin") as TextBox).Text;
        //            string ItmQty = (g1.FindControl("ItmQty") as TextBox).Text;
        //            string Tbwght = (g1.FindControl("Tbwght") as TextBox).Text;
        //            string Tbunts = (g1.FindControl("Tbunts") as TextBox).Text;
        //            string Tbrat = (g1.FindControl("Tbrat") as TextBox).Text;
        //            string Tbsalrat = (g1.FindControl("Tbsalrat") as TextBox).Text;
        //            string Tbpurrat = (g1.FindControl("Tbpurrat") as TextBox).Text;


        //            // Inventory Logic Starts here

        //                cmdtxt = "select ProductID from tbl_Dstk where ProductID = " + ddlstkItm + "";
        //                DataTable dtckpro = new DataTable();
        //                dtckpro = DataAccess.DBConnection.GetDataTable(cmdtxt);

        //                if (dtckpro.Rows.Count > 0)
        //                {
        //                    cmdtxt = "select Dstk_ItmQty from tbl_Dstk where ProductID = " + ddlstkItm + "";
        //                    DataTable dtstkqty = new DataTable();
        //                    dtstkqty = DataAccess.DBConnection.GetDataTable(cmdtxt);

        //                    if (dtstkqty.Rows.Count > 0)
        //                    {
        //                        string stk_qty = dtstkqty.Rows[0]["Dstk_ItmQty"].ToString();
        //                        int Itemqty =  Convert.ToInt32(ItmQty) + Convert.ToInt32(stk_qty);

        //                        command.CommandText = " UPDATE tbl_Dstk SET Dstk_ItmQty = " + Itemqty + " where  ProductID = " + ddlstkItm + "";
        //                        command.ExecuteNonQuery();
        //                    }

        //             // Inventory Logic Ends here

        //                }
        //                else
        //                {
        //                    command.CommandText =
        //                    " INSERT INTO tbl_Dstk (Dstk_ItmDes, Dstk_ItmQty, Dstk_Itmwght, Dstk_ItmUnt, Dstk_rat, Dstk_salrat, Dstk_purrat, Mstk_id, ProductID) " +
        //                    " VALUES " +
        //                    " ('" + ItmDscptin + "','" + ItmQty + "', '" + Tbwght + "','" + Tbunts + "','" + Tbrat + "','" + Tbsalrat + "', '" + Tbpurrat + "', '" + HFStckIn.Value + "', '" + ddlstkItm + "')";
        //                    command.ExecuteNonQuery();                                                       
        //                }
        //        }
        //        // Attempt to commit the transaction.
        //        transaction.Commit();

        //        if (chk_prt.Checked == true)
        //        {
        //            //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=PR','_blank','height=600px,width=600px,scrollbars=1');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
        //        Console.WriteLine("  Message: {0}", ex.Message);

        //        // Attempt to roll back the transaction. 
        //        try
        //        {
        //            transaction.Rollback();
        //        }
        //        catch (Exception ex2)
        //        {
        //            // This catch block will handle any errors that may have occurred 
        //            // on the server that would cause the rollback to fail, such as 
        //            // a closed connection.
        //            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
        //            Console.WriteLine("  Message: {0}", ex2.Message);
        //        }
        //    }
        //    finally
        //    {
        //        con.Close();
        //        //Clear();
        //        FillGrid();
        //        ptnSno();                
        //    }
        //    //}

        //    return i;
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int j = 0;
                j = Save();

                if (j == 1)
                {
                    Response.Redirect("StockIn_.aspx");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }

        protected void ddlstckIn_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string cmdtxt1 = " select CONVERT(VARCHAR(10), MPurDate, 101) as [MPurDate], ven_id from MPurchase where MPurID = " + ddlstckIn.SelectedValue.Trim() + " and ck_Act = 'True' ";

                    DataTable dpurdat = new DataTable();
                    dpurdat = DataAccess.DBConnection.GetDataTable(cmdtxt1);

                    if (dpurdat.Rows.Count > 0)
                    {

                        TBPurdat.Text = dpurdat.Rows[0]["MPurDate"].ToString();
                        ddl_ven.SelectedValue = dpurdat.Rows[0]["ven_id"].ToString();

                        string cmdText2 = " select DPurchase.ProductID as [ProNam],DPurchase.ProDes as [Dstk_ItmDes], DPurchase.Qty as [Dstk_ItmQty], " +
                            " '' as Dstk_Itmwght, DPurchase.Unit as [Dstk_ItmUnt] , Products.Cost as [Dstk_rat], " +
                            " '' as Dstk_salrat, '' as Dstk_purrat from MPurchase inner join DPurchase on MPurchase.MPurID = DPurchase.MPurID " +
                            " inner join Products on  DPurchase.ProductID = Products.ProductID where ck_Act = 'True' and MPurchase.MPurID  = '" + ddlstckIn.SelectedValue.Trim() + "'";

                        DataTable dtpurdtl = new DataTable();
                        dtpurdtl = DataAccess.DBConnection.GetDataTable(cmdText2);
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
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }

        protected void GVStckIn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVStckIn.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void GVStckIn_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string MStkID = Server.HtmlDecode(GVStckIn.Rows[e.RowIndex].Cells[0].Text.ToString());

                SqlCommand cmd = new SqlCommand();

                cmd = new SqlCommand("sp_del_Stk", con);
                cmd.Parameters.Add("@Mstk_id", SqlDbType.Int).Value = MStkID;
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
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        protected void GVStckIn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                if (e.CommandName == "Show")
                {
                    string STKID = GVStckIn.DataKeys[row.RowIndex].Values[0].ToString();

                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=PR&PURID=" + PURID + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=QuotaionReport&QUOTID=" + QOUTSNO + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                }

                if (e.CommandName == "Select")
                {

                    string MPurID = Server.HtmlDecode(GVStckIn.Rows[row.RowIndex].Cells[0].Text.ToString());

                    string cmdtxt = " select  ISActive,MPurID,Mstk_PurDat,Mstk_dat,ven_id from tbl_Mstk " +
                        " inner join tbl_Dstk on tbl_Mstk.Mstk_id = tbl_Dstk.Mstk_id " +
                        " where tbl_Mstk.Mstk_id ='" + MPurID + "'";

                    //string cmdtxt = " select a.mPurID, b.dPurId, b.mPurId, a.ven_id, a.VndrAdd, a.VndrCntct,a.PurNo, a.mPurDate, a.CreatedBy, a.CreatedAt, a.cnic, a.ntnno, a.tobePrntd,b.Dpurid, b.ProNam, b.ProDes, b.Qty, b.Total, b.subtotl, b.unit, b.cost, b.protyp,b.grossttal from MPurchase a  inner join DPurchase b on a.mPurID = b.mPurID where a.MPurID =" + MPurID + "";

                    SqlCommand cmdSlct = new SqlCommand(cmdtxt, con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmdSlct);

                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                        lbl_Heading.Text = "Alert!";
                        lblalert.Text = "No Record Found!!";
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

    }
}