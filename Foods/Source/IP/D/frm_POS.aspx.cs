using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Foods.Source.IP
{
    public partial class frm_POS : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        string TBItms, lblItmpris, TBItmQty, lblcat, lblttl, HFDSal;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbldat.Text = DateTime.Now.ToShortDateString();
                //txtClock.Text = DateTime.Now.ToShortTimeString();
                lblusr.Text = Session["Username"].ToString();
                Data();
                SetInitRowPuritm();
                ptnSno();
                //txtClock.Text = DateTime.Now.ToString("hh:mm:ss");
            }
        }

        protected void LinkBtnlogout_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("~/Login.aspx");
        }
        
        protected void btnclear_Click(object sender, EventArgs e)
        {
            Response.Redirect("frm_POS.aspx"); 
        }

        public void Data()
        {
            DataTable dt_ = new DataTable();

            dt_.Columns.AddRange(new DataColumn[] { new DataColumn("Name"), new DataColumn("ID") });
            dt_.Clear();


            dt_.Rows.Add("Regular Customers","1");
            dt_.Rows.Add("New Customers","2");
            
            ddl_custtyp.DataSource = dt_;
            ddl_custtyp.DataTextField = "Name";
            ddl_custtyp.DataValueField = "ID";
            ddl_custtyp.DataBind();
            ddl_custtyp.Items.Insert(0, new ListItem("--Select Customers Type--", "0"));
        }


        private void SetInitRowPuritm()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Items", typeof(string)));
            dt.Columns.Add(new DataColumn("Itempric", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));
            dt.Columns.Add(new DataColumn("Itemcat", typeof(string)));
            dt.Columns.Add(new DataColumn("TTL", typeof(string)));
            dt.Columns.Add(new DataColumn("Dposid", typeof(string)));
            dt.Columns.Add(new DataColumn("ProductID", typeof(string)));
            dt.Columns.Add(new DataColumn("ProductTypeID", typeof(string)));

            dr = dt.NewRow();

            dr["Items"] = string.Empty;           
            dr["Itempric"] = "0.00";
            dr["QTY"] = "0.00";
            dr["Itemcat"] = string.Empty;
            dr["TTL"] = "0.00";
            dr["Dposid"] = string.Empty;
            dr["ProductID"] = string.Empty;
            dr["ProductTypeID"] = string.Empty;

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["dt_adItm"] = dt;

            GV_POS.DataSource = dt;
            GV_POS.DataBind();
        }

        protected void TBItmQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //string saleper = "";

                for (int j = 0; j < GV_POS.Rows.Count; j++)
                {
                    TextBox TBItms = (TextBox)GV_POS.Rows[j].FindControl("TBItms");
                    TextBox TBItmQty = (TextBox)GV_POS.Rows[j].FindControl("TBItmQty");
                    Label lblItmpris = (Label)GV_POS.Rows[j].FindControl("lblItmpris");                    
                    //Label lblttl = (Label)GV_POS.FooterRow.FindControl("lblttl");
                    Label lblttl = (Label)GV_POS.Rows[j].FindControl("lblttl");
                    Label lbl_Flag = (Label)GV_POS.Rows[j].FindControl("lbl_Flag");

                    if (TBItms.Text == "")
                    {
                        lbl_Flag.Text = "0";
                    }

                    //string query = " select saleper,* from Customers_ where CustomerID=" + DDL_Cust.SelectedValue.Trim() + " and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                    //SqlCommand command = new SqlCommand(query, con);
                    //con.Open();
                    //DataTable dt_ = new DataTable();
                    //SqlDataAdapter adp = new SqlDataAdapter(command);
                    //adp.Fill(dt_);
                    //command.ExecuteNonQuery();

                    //if (dt_.Rows.Count > 0)
                    //{
                    //    saleper = dt_.Rows[0]["saleper"].ToString();
                    //}
                    //else
                    //{
                    //    saleper = "0.00";
                    //}
                    //con.Close();

                    lblttl.Text = (Convert.ToDouble(TBItmQty.Text.Trim()) * Convert.ToDouble(lblItmpris.Text.Trim())).ToString();
                }

                float GTotal = 0;
                for (int k = 0; k < GV_POS.Rows.Count; k++)
                {
                    Label lblttl = (Label)GV_POS.Rows[k].FindControl("lblttl");

                    //double discount = Convert.ToDouble(total.Text) * Convert.ToDouble(saleper) / 100;
                    //string ttlamt = (Convert.ToDouble(lblttl.Text) - discount).ToString();
                    string ttlamt = Convert.ToDouble(lblttl.Text).ToString();

                    GTotal += Convert.ToSingle(ttlamt);
                    TBTtl.Text = GTotal.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                        //extract the TextBox values
                        TextBox TBItms = (TextBox)GV_POS.Rows[rowIndex].Cells[0].FindControl("TBItms");
                        Label lblItmpris = (Label)GV_POS.Rows[rowIndex].Cells[1].FindControl("lblItmpris");
                        TextBox TBItmQty = (TextBox)GV_POS.Rows[rowIndex].Cells[2].FindControl("TBItmQty");
                        Label lblcat = (Label)GV_POS.Rows[rowIndex].Cells[3].FindControl("lblcat");
                        Label lblttl = (Label)GV_POS.Rows[rowIndex].Cells[4].FindControl("lblttl");
                        HiddenField HFDSal = (HiddenField)GV_POS.Rows[rowIndex].Cells[5].FindControl("HFDSal");
                        HiddenField HFPROID = (HiddenField)GV_POS.Rows[rowIndex].Cells[0].FindControl("PROID");
                        HiddenField HFPROCATID = (HiddenField)GV_POS.Rows[rowIndex].Cells[3].FindControl("PROCATID");

                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["Items"] = TBItms.Text;
                        dt.Rows[i - 1]["Itempric"] = lblItmpris.Text;
                        dt.Rows[i - 1]["QTY"] = TBItmQty.Text;
                        dt.Rows[i - 1]["Itemcat"] = lblcat.Text;
                        dt.Rows[i - 1]["TTL"] = lblttl.Text;
                        dt.Rows[i - 1]["Dposid"] = HFDSal.Value;
                        dt.Rows[i - 1]["ProductID"] = HFPROID.Value;
                        dt.Rows[i - 1]["ProductTypeID"] = HFPROCATID.Value;

                        rowIndex++;

                        float GTotal = 0, CRAmt = 0, DBAmt = 0;
                        for (int j = 0; j < GV_POS.Rows.Count; j++)
                        {
                            Label total = (Label)GV_POS.Rows[j].FindControl("lblttl");
                            //TextBox CRAmtttl = (TextBox)GV_POS.Rows[j].FindControl("Tbcramt");
                            //TextBox DBAmtttl = (TextBox)GV_POS.Rows[j].FindControl("Tbdbamt");

                            GTotal += Convert.ToSingle(total.Text);

                            /*if (CRAmtttl.Text != "0.00" || DBAmtttl.Text != "0.00")
                            {   
                                CRAmt += Convert.ToSingle(CRAmtttl.Text);
                                DBAmt += Convert.ToSingle(DBAmtttl.Text);
                            }*/
                            TBTtl.Text = GTotal.ToString();

                        }

                        //TBGrssTotal.Text = GTotal.ToString();
                        //TBTtl.Text = GTotal.ToString();

                        //if (DDL_Paytyp.SelectedValue == "2")
                        //{
                        //    TB_ChqAmt.Text = TBTtl.Text;
                        //    TB_ChqAmt.Enabled = false;
                        //}
                        //else
                        //{
                        //    TB_CshAmt.Text = TBTtl.Text;
                        //    TB_CshAmt.Enabled = false;
                        //}
                        //TBCRAmtttl.Text = CRAmt.ToString();
                        //Customersttl.Text = DBAmt.ToString();


                        //ddlPurItm.Focus();
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
                            TextBox TBItms = (TextBox)GV_POS.Rows[rowIndex].Cells[0].FindControl("TBItms");
                            Label lblItmpris = (Label)GV_POS.Rows[rowIndex].Cells[1].FindControl("lblItmpris");
                            TextBox TBItmQty = (TextBox)GV_POS.Rows[rowIndex].Cells[2].FindControl("TBItmQty");
                            Label lblcat = (Label)GV_POS.Rows[rowIndex].Cells[3].FindControl("lblcat");
                            Label lblttl = (Label)GV_POS.Rows[rowIndex].Cells[4].FindControl("lblttl");
                            HiddenField HFDSal = (HiddenField)GV_POS.Rows[rowIndex].Cells[5].FindControl("HFDSal");
                            Label lbl_Flag = (Label)GV_POS.Rows[i].FindControl("lbl_Flag");
                            HiddenField HFPROID = (HiddenField)GV_POS.Rows[rowIndex].Cells[0].FindControl("PROID");
                            HiddenField HFPROCATID = (HiddenField)GV_POS.Rows[rowIndex].Cells[3].FindControl("PROCATID");

                            string Itms = dt.Rows[i]["Items"].ToString();

                            if (Itms != "")
                            {
                                TBItms.Text = dt.Rows[i]["Items"].ToString();
                            }
                            else
                            {
                                TBItms.Text = "";
                            }

                            string Itempric = dt.Rows[i]["Itempric"].ToString();

                            if (Itempric != "")
                            {
                                lblItmpris.Text = dt.Rows[i]["Itempric"].ToString();
                            }
                            else
                            {
                                lblItmpris.Text = "0.00";
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

                            string Itemcat = dt.Rows[i]["Itemcat"].ToString();

                            if (Itemcat != "")
                            {
                                lblcat.Text = dt.Rows[i]["Itemcat"].ToString();
                            }
                            else
                            {
                                lblcat.Text = "0.00";
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
                            HFPROCATID.Value = dt.Rows[i]["ProductTypeID"].ToString();


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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "alert('some');", true);
                //lbl_Heading.Text = "Error!";
                //lblalert.Text = ex.Message;

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
                throw ex;
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


        private void Save()
        {
            string MSalId="";
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
                string query = " INSERT INTO tbl_MPos ([BillNO],[CompanyId],[BranchId],[Username],[billdat],[billtim],[CustomerName],[custtyp] " +
                   " ,[createdby] ,[createdat] ,[createterminal] ,[updatedby] ,[updatedat] ,[updateterminal]) VALUES " +
                   " ('" + lblbilno.Text + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "','" + Session["Username"] + "'" +
                   " ,'" + lbldat.Text + "','" + txtClock.Value + "','" + TBCust.Text +"','" + ddl_custtyp.SelectedValue.Trim() + "','" + Session["user"].ToString() + "','" + DateTime.Today.ToString("MM/dd/yyyy") +
                   "','::1' ,'" + Session["user"].ToString() + "' ,'" + DateTime.Today.ToString("MM/dd/yyyy") + "' ,'::1') ";

                command.CommandText = query;
                command.ExecuteNonQuery();

                // Master Purchase " + TBSalDat.Text.Trim() + " , " + DateTime.Today + "
                command.CommandText = "select Mposid from tbl_MPos where BillNO = '" + lblbilno.Text.Trim() + "'";

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
                    lblItmpris = (g1.FindControl("lblItmpris") as Label).Text;
                    TBItmQty = (g1.FindControl("TBItmQty") as TextBox).Text;
                    lblcat = (g1.FindControl("lblcat") as Label).Text;
                    lblttl = (g1.FindControl("lblttl") as Label).Text;
                    HFDSal = (g1.FindControl("HFDSal") as HiddenField).Value;
                    HFPROID = (g1.FindControl("PROID") as HiddenField).Value;
                    HFPROCATID = (g1.FindControl("PROCATID") as HiddenField).Value;

                    command.CommandText =
                        " INSERT INTO [dbo].[tbl_DPos] ([BillNO] ,[Mposid] ,[ProductID] ,[ProQty] ,[ProductTypeID] ,[Ttl] " +
                        " ,[grntttl] ,[createdby] ,[createdat] ,[createterminal] ,[updatedby] ,[updatedat] ,[updateterminal]) " +
                        " VALUES ('" + lblbilno.Text + "'" +
                        " ,'" + MSalId + "' ,'" + HFPROID + "' ,'" + TBItmQty + "' ,'" + HFPROCATID + "' ,'" + lblttl + "'" +
                        " ,'" + TBTtl.Text + "' ,'" + Session["user"].ToString() + "' ,getdate()" +
                        " ,'" + Session["BranchID"] + "' ,'" + Session["user"].ToString() + "' ,getdate()" +
                        " ,'" + Session["BranchID"] + "') ";
                       
                    command.ExecuteNonQuery();
                }

               
                // Attempt to commit the transaction.
                transaction.Commit();

               
               //ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'ReportViewer.aspx?ID=PR','_blank','height=600px,width=600px,scrollbars=1');", true);
               
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
                Response.Redirect("frm_POS.aspx");
            }
            //}
        }
        private void ptnSno()
        {
            try
            {
                string str = "select isnull(max(cast(Mposid as int)),0) as [Mposid]  from tbl_MPos";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(lblbilno.Text))
                    {
                        int v = Convert.ToInt32(reader["Mposid"].ToString());
                        int b = v + 1;
                        lblbilno.Text = "SAL00" + b.ToString();
                    }
                    else
                    {
                        lblbilno.Text = "SAL001";
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                //lbl_err.Text = ex.Message.ToString();
            }
        }

        protected void TBItms_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < GV_POS.Rows.Count; j++)
                {

                    TextBox TBItms = (TextBox)GV_POS.Rows[j].FindControl("TBItms");
                    TextBox TBItmQty = (TextBox)GV_POS.Rows[j].FindControl("TBItmQty");
                    Label lblItmpris = (Label)GV_POS.Rows[j].FindControl("lblItmpris");
                    HiddenField PROID = (HiddenField)GV_POS.Rows[j].FindControl("PROID");
                    HiddenField PROCATID = (HiddenField)GV_POS.Rows[j].FindControl("PROCATID");

                    Label lblcat = (Label)GV_POS.Rows[j].FindControl("lblcat");
                    Label lbl_Flag = (Label)GV_POS.Rows[j].FindControl("lbl_Flag");


                    string query = " select Products.ProductID,Products.ProductTypeID, cost as [Itempric],ProductTypeName as [Itemcat] from Products " +
                        " inner join tbl_producttype on Products.ProductTypeID = tbl_producttype.ProductTypeID " +
                        " where ProductName =  '" + TBItms.Text.Trim() + "'";

                    SqlCommand cmd = new SqlCommand(query, con);
                    DataTable dt_ = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);

                    adp.Fill(dt_);

                    if (dt_.Rows.Count > 0)
                    {

                        lblItmpris.Text = dt_.Rows[0]["Itempric"].ToString();
                        lblcat.Text = dt_.Rows[0]["Itemcat"].ToString();
                        PROID.Value = dt_.Rows[0]["ProductID"].ToString();
                        PROCATID.Value = dt_.Rows[0]["ProductTypeID"].ToString();
                        lbl_Flag.Text = "1";
                        TBItmQty.Focus();
                        if (lbl_Flag.Text == "0")
                        {
                            //TBItmQty.Text = dt_.Rows[0]["Qty"].ToString();
                        }
                    }
                    else
                    {
                        lblItmpris.Text = "0.00";
                        lblcat.Text = "0.00";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (POSID.Value == "")
                {
                    Save();
                }
                else
                {
                    update();
                }
            }
            catch (Exception ex)
            { 
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError", "alert('" + ex.Message + "');", true);
            }
        }

        protected void TBPos_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query = " select tbl_MPos.Mposid,tbl_DPos.ProductID,ProductName as [Items],grntttl,tbl_MPos.CustomerName,  " +
                    " Cost as [Itempric],tbl_DPos.ProQty as [Qty],tbl_MPos.BillNO,custtyp, " +
                    " Products.ProductTypeID,ProductTypeName as [Itemcat],tbl_DPos.Ttl as [TTL],Dposid " +
                    " from tbl_MPos inner join tbl_DPos on tbl_MPos.Mposid = tbl_DPos.Mposid  " +
                    //" inner join  Customers_ on tbl_MPos.CustomerName= Customers_.CustomerName  " +
                    " inner join Products on tbl_DPos.ProductID = Products.ProductID " +
                    " inner join tbl_producttype on Products.ProductTypeID = tbl_producttype.ProductTypeID " +
                    " where tbl_MPos.BillNO = '" + TBPos.Text.Trim() + "'";

                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt_ = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dt_);

                if (dt_.Rows.Count > 0)
                {
                    ddl_custtyp.SelectedValue = dt_.Rows[0]["custtyp"].ToString();
                    TBCust.Text = dt_.Rows[0]["CustomerName"].ToString();
                    lblbilno.Text = dt_.Rows[0]["BillNO"].ToString();
                    TBTtl.Text = dt_.Rows[0]["grntttl"].ToString();
                    POSID.Value = dt_.Rows[0]["Mposid"].ToString();

                    GV_POS.DataSource = dt_;
                    GV_POS.DataBind();

                    ViewState["dt_adItm"] = dt_;
                }
                else {

                    ddl_custtyp.SelectedValue = "0";
                    TBCust.Text = "";
                    lblbilno.Text = "";
                    DataTable d = new DataTable();
                    GV_POS.DataSource = d;
                    GV_POS.DataBind();
                }
            }catch( Exception ex)
            {
                throw ex;
            }
        }

        private void update()
        {
            string MSalId = "";
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

                //Master Sales 
                command.CommandText =
                    " Update tbl_MPos set BillNO ='" + lblbilno.Text + "' , CompanyId ='" + Session["CompanyID"] + "', BranchId='" + Session["BranchID"] +
                    "', Username = '" + Session["user"].ToString() + "',billdat ='" + lbldat.Text + "', billtim ='" + txtClock.Value +
                    "', CustomerName='" + TBCust.Text + "',custtyp=1, updatedby='" + Session["user"].ToString() + "', updatedat =getdate(),[updateterminal]='" + Session["BranchID"] + "' where Mposid=" + POSID.Value + "";

                command.ExecuteNonQuery();

                // Master Purchase " + TBSalDat.Text.Trim() + " , " + DateTime.Today + "
                command.CommandText = "select Mposid from tbl_MPos where BillNO = '" + TBPos.Text.Trim() + "'";

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
                    string PROID = (g1.FindControl("PROID") as HiddenField).Value;
                    string PROCATID = (g1.FindControl("PROCATID") as HiddenField).Value;
                    string TBItms = (g1.FindControl("TBItms") as TextBox).Text;
                    string lblItmpris = (g1.FindControl("lblItmpris") as Label).Text;
                    string TBItmQty = (g1.FindControl("TBItmQty") as TextBox).Text;
                    string ProductTypeID = (g1.FindControl("PROCATID") as HiddenField).Value;
                    string HFDSal = (g1.FindControl("HFDSal") as HiddenField).Value;
                    string Itemcat = (g1.FindControl("lblcat") as Label).Text;
                    string lblttl = (g1.FindControl("lblttl") as Label).Text;

                    if (HFDSal != "")
                    {
                        command.CommandText =
                        " Update tbl_DPos set BillNO='" + lblbilno.Text + "', Mposid ='" + MSalId + "' , ProductID ='" + PROID +
                        "', ProQty ='" + TBItmQty + "', ProductTypeID ='" + PROCATID +
                        "', Ttl='" + lblttl + "', grntttl ='" + TBTtl.Text + "', updatedby='" + Session["user"].ToString() + "',updatedat=getdate()" +
                        ", updateterminal ='" + Session["BranchID"] + "' where Dposid = "+ HFDSal + "";

                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        command.CommandText =
                           " INSERT INTO [dbo].[tbl_DPos] ([BillNO] ,[Mposid] ,[ProductID] ,[ProQty] ,[ProductTypeID] ,[Ttl] " +
                           " ,[grntttl] ,[createdby] ,[createdat] ,[createterminal] ,[updatedby] ,[updatedat] ,[updateterminal]) " +
                           " VALUES ('" + lblbilno.Text + "'" +
                           " ,'" + MSalId + "' ,'" + PROID + "' ,'" + TBItmQty + "' ,'" + PROCATID + "' ,'" + lblttl + "'" +
                           " ,'" + TBTtl.Text + "' ,'" + Session["user"].ToString() + "' ,getdate()" +
                           " ,'" + Session["BranchID"] + "' ,'" + Session["user"].ToString() + "' ,getdate()" +
                           " ,'" + Session["BranchID"] + "') ";

                        command.ExecuteNonQuery();
                    }
                }

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
                Response.Redirect("frm_POS.aspx");
            }
            //}
        }


        protected void TBCust_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    string query = "select * from Customers_ where CustomerName ='" + TBCust.Text.Trim() + "' and IsActive = 1 '";

            //    SqlCommand cmd = new SqlCommand(query, con);
            //    DataTable dt_ = new DataTable();
            //    SqlDataAdapter adp = new SqlDataAdapter(cmd);

            //    adp.Fill(dt_);

            //    if (dt_.Rows.Count > 0)
            //    {
            //        TBCust.Text = dt_.Rows[0]["CustomerName"].ToString();
                    
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

    }
}