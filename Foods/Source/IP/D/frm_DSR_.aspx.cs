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
using System.Globalization;
using System.IO;
using Foods;
using DataAccess;
using System.Web.Services;

namespace Foods
{
    public partial class frm_DSR_ : System.Web.UI.Page
    {
        SqlConnection con = DataAccess.DBConnection.connection();
        DataTable dt_ = null;
        int i = 0;
        string query, mdsrId, DDL_Itm, DDL_Unt, TBQty, TBSalRat, TBSalRtrn, TBRecy, lblOutstan, TBAmt, TBRmk;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGrid();
                SetInitRow();
                BindDLL();
                TBdsrdat.Text = DateTime.Now.ToShortDateString();
                btnUpd.Enabled = false;
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
            string str = "select ProductName from Products where ProductName like '" + prefixText + "%'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        } 

        #region methods
        public void FillGrid()
        {
            try
            {
                //Sales Order
                using (SqlCommand cmd = new SqlCommand())
                {

                    string lvl = Session["Level"].ToString();

                    if (lvl == "1")
                    {

                        cmd.CommandText = " select tbl_Mdsr.CustomerID,Isdon,CustomerName,convert(varchar, dsrdat, 111) as [dsrdat],tbl_Mdsr.dsrid from tbl_Mdsr inner join Customers_ on tbl_Mdsr.CustomerID = Customers_.CustomerID where tbl_Mdsr.CompanyId = '" + Session["CompanyID"] + "' and tbl_Mdsr.BranchId= '" + Session["BranchID"] + "' order by dsrid desc ";
                    }
                    else
                    {
                        cmd.CommandText = " select tbl_Mdsr.CustomerID,Isdon,CustomerName,convert(varchar, dsrdat, 111) as [dsrdat],tbl_Mdsr.dsrid from tbl_Mdsr inner join Customers_ on tbl_Mdsr.CustomerID = Customers_.CustomerID where tbl_Mdsr.CompanyId = '" + Session["CompanyID"] + "' and tbl_Mdsr.BranchId= '" + Session["BranchID"] + "' order by dsrid desc ";                         
                    }

                    cmd.Connection = con;
                    con.Open();

                    DataTable dtSal_ = new DataTable();

                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    adp.Fill(dtSal_);

                    GVDSR.DataSource = dtSal_;
                    GVDSR.DataBind();

                    con.Close();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }

        private void SetInitRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            //dt.Columns.Add(new DataColumn("ITEMCODE", typeof(string)));
            dt.Columns.Add(new DataColumn("ITEMNAME", typeof(string)));
            dt.Columns.Add(new DataColumn("ITEM TYPE", typeof(string)));
            dt.Columns.Add(new DataColumn("UNITS", typeof(string)));
            dt.Columns.Add(new DataColumn("QTY", typeof(string)));
            dt.Columns.Add(new DataColumn("SALERATE", typeof(string)));
            dt.Columns.Add(new DataColumn("SALERETURN", typeof(string)));
            dt.Columns.Add(new DataColumn("RECOVERY", typeof(string)));
            dt.Columns.Add(new DataColumn("OUTSTANDING", typeof(string)));
            dt.Columns.Add(new DataColumn("AMOUNT", typeof(string)));
            dt.Columns.Add(new DataColumn("REMARKS", typeof(string)));
            dt.Columns.Add(new DataColumn("ddsr", typeof(string)));
            

            dr = dt.NewRow();

            //dr["ITEMCODE"] = string.Empty;
            dr["ITEMNAME"] = string.Empty;
            dr["ITEM TYPE"] = string.Empty;
            dr["UNITS"] = string.Empty;
            dr["QTY"] = "0.00";
            dr["SALERATE"] = "0.00";
            dr["SALERETURN"] = "0.00";
            dr["RECOVERY"] = "0.00";
            dr["OUTSTANDING"] = "0.00";
            dr["AMOUNT"] = "0.00";
            dr["REMARKS"] = string.Empty;
            dr["ddsr"] = "0.00";            
            
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["dt_adItm"] = dt;

            GVPro1.DataSource = dt;
            GVPro1.DataBind();

            int rowIndex1 = 0;
            LinkButton linkbtndel = (LinkButton)GVPro1.Rows[rowIndex1].Cells[0].FindControl("linkbtndel");
            linkbtndel.Visible = false;
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
                        //extract the Controls values

                        DropDownList DDL_Itm = (DropDownList)GVPro1.Rows[rowIndex].Cells[0].FindControl("DDL_Itm");
                        TextBox TextBox1 = (TextBox)GVPro1.Rows[rowIndex].Cells[0].FindControl("TextBox1");
                        DropDownList DDL_Itmtyp = (DropDownList)GVPro1.Rows[rowIndex].Cells[1].FindControl("DDL_Itmtyp");
                        DropDownList DDL_Unt = (DropDownList)GVPro1.Rows[rowIndex].Cells[2].FindControl("DDL_Unt");
                        TextBox TBQty = (TextBox)GVPro1.Rows[rowIndex].Cells[3].FindControl("TBQty");
                        TextBox TBSalRat = (TextBox)GVPro1.Rows[rowIndex].Cells[4].FindControl("TBSalRat");
                        TextBox TBSalRtrn = (TextBox)GVPro1.Rows[rowIndex].Cells[5].FindControl("TBSalRtrn");
                        TextBox TBRecy = (TextBox)GVPro1.Rows[rowIndex].Cells[6].FindControl("TBRecy");
                        TextBox lblOutstan = (TextBox)GVPro1.Rows[rowIndex].Cells[7].FindControl("lblOutstan");
                        TextBox TBAmt = (TextBox)GVPro1.Rows[rowIndex].Cells[8].FindControl("TBAmt");
                        TextBox TBRmk = (TextBox)GVPro1.Rows[rowIndex].Cells[9].FindControl("TBRmk");
                        //TextBox lblttl = (TextBox)GVPro1.Rows[rowIndex].Cells[8].FindControl("lblttl");
                        Label dsrid = (Label)GVPro1.FooterRow.Cells[10].FindControl("HFDSR");
                        LinkButton linkbtndel = (LinkButton)GVPro1.Rows[rowIndex].Cells[0].FindControl("linkbtndel");
                        linkbtndel.Visible = true;

                        drRow = dt.NewRow();

                        //dt.Rows[i - 1]["ITEMCODE"] = DDL_Unt.Text;
                        dt.Rows[i - 1]["ITEMNAME"] = DDL_Itm.SelectedValue;
                        TextBox1.Text = DDL_Itm.SelectedItem.Text;
                        dt.Rows[i - 1]["ITEM TYPE"] = DDL_Itmtyp.Text;
                        dt.Rows[i - 1]["UNITS"] = DDL_Unt.SelectedValue;
                        dt.Rows[i - 1]["QTY"] = TBQty.Text;
                        dt.Rows[i - 1]["SALERATE"] = TBSalRat.Text;
                        dt.Rows[i - 1]["SALERETURN"] = TBSalRtrn.Text;
                        dt.Rows[i - 1]["RECOVERY"] = TBRecy.Text;
                        dt.Rows[i - 1]["OUTSTANDING"] = lblOutstan.Text;
                        dt.Rows[i - 1]["AMOUNT"] = TBAmt.Text;
                        dt.Rows[i - 1]["REMARKS"] = TBRmk.Text;
                        //dt.Rows[i - 1]["ddsr"] = dsrid.Text;                  
                        
                        rowIndex++;

                        //DDL_Unt.Focus();
                    }
                    dt.Rows.Add(drRow);
                    ViewState["dt_adItm"] = dt;

                    GVPro1.DataSource = dt;
                    GVPro1.DataBind();

                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreRow();
        }

        private void SetPreRow()
        {
            try
            {
                //BindDDL();
                int rowIndex = 0;
                if (ViewState["dt_adItm"] != null)
                {
                    DataTable dt = (DataTable)ViewState["dt_adItm"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DropDownList DDL_Itm = (DropDownList)GVPro1.Rows[rowIndex].Cells[0].FindControl("DDL_Itm");
                            TextBox TextBox1 = (TextBox)GVPro1.Rows[rowIndex].Cells[0].FindControl("TextBox1");
                            DropDownList DDL_Itmtyp = (DropDownList)GVPro1.Rows[rowIndex].Cells[1].FindControl("DDL_Itmtyp");
                            DropDownList DDL_Unt = (DropDownList)GVPro1.Rows[rowIndex].Cells[2].FindControl("DDL_Unt");
                            TextBox TBQty = (TextBox)GVPro1.Rows[rowIndex].Cells[3].FindControl("TBQty");
                            TextBox TBSalRat = (TextBox)GVPro1.Rows[rowIndex].Cells[4].FindControl("TBSalRat");
                            TextBox TBSalRtrn = (TextBox)GVPro1.Rows[rowIndex].Cells[5].FindControl("TBSalRtrn");
                            TextBox TBRecy = (TextBox)GVPro1.Rows[rowIndex].Cells[6].FindControl("TBRecy");
                            TextBox lblOutstan = (TextBox)GVPro1.Rows[rowIndex].Cells[7].FindControl("lblOutstan");
                           // Label lblttl = (Label)GVPro1.FooterRow.Cells[8].FindControl("lblttl");
                            TextBox TBAmt = (TextBox)GVPro1.Rows[rowIndex].Cells[8].FindControl("TBAmt");
                            Label lbl_Flag = (Label)GVPro1.Rows[i].FindControl("lbl_Flag");
                            TextBox TBRmk = (TextBox)GVPro1.Rows[rowIndex].Cells[9].FindControl("TBRmk");
                            //HiddenField HFDSR = (HiddenField)GVPro1.Rows[rowIndex].Cells[10].FindControl("HFDSR");
                            Label HFDSR = (Label)GVPro1.Rows[rowIndex].Cells[10].FindControl("HFDSR");

                            
                            //TBItmCd.Text = dt.Rows[i]["ITEMCODE"].ToString();
                            DDL_Itm.SelectedValue = dt.Rows[i]["ITEMNAME"].ToString();
                            if (DDL_Itm.SelectedValue == "0")
                            {
                                TextBox1.Text = "";
                            }
                            else
                            {
                               TextBox1.Text = DDL_Itm.SelectedItem.Text;//dt.Rows[i]["ITEMNAME"].ToString();
                            }
                            DDL_Itmtyp.SelectedValue = dt.Rows[i]["ITEM TYPE"].ToString();
                            DDL_Unt.SelectedValue = dt.Rows[i]["UNITS"].ToString();
                            TBQty.Text = dt.Rows[i]["QTY"].ToString();
                            TBSalRat.Text = dt.Rows[i]["SALERATE"].ToString();
                            TBSalRtrn.Text = dt.Rows[i]["SALERETURN"].ToString();
                            TBRecy.Text = dt.Rows[i]["RECOVERY"].ToString();
                            lblOutstan.Text = dt.Rows[i]["OUTSTANDING"].ToString();
                            TBAmt.Text = dt.Rows[i]["AMOUNT"].ToString();
                            TBRmk.Text = dt.Rows[i]["REMARKS"].ToString();
                            HFDSR.Text = dt.Rows[i]["ddsr"].ToString();                            

                            string qty = dt.Rows[i]["QTY"].ToString();

                            if (qty != "")
                            {
                                TBQty.Text = dt.Rows[i]["QTY"].ToString();
                            }
                            else
                            {
                                TBQty.Text = "0.00";
                            }


                            string salrat = dt.Rows[i]["SALERATE"].ToString();

                            if (salrat != "")
                            {
                                TBSalRat.Text = dt.Rows[i]["SALERATE"].ToString();
                            }
                            else
                            {
                                TBSalRat.Text = "0.00";
                            }

                            string salratun = dt.Rows[i]["SALERETURN"].ToString();

                            if (salratun != "")
                            {
                                TBSalRtrn.Text = dt.Rows[i]["SALERETURN"].ToString();
                            }
                            else
                            {
                                TBSalRtrn.Text = "0.00";
                            }

                            string recovy = dt.Rows[i]["RECOVERY"].ToString();

                            if (recovy != "")
                            {
                                TBRecy.Text = dt.Rows[i]["RECOVERY"].ToString();
                            }
                            else
                            {
                                TBRecy.Text = "0.00";
                            }

                            string outstand = dt.Rows[i]["OUTSTANDING"].ToString();

                            if (outstand != "")
                            {
                                lblOutstan.Text = dt.Rows[i]["OUTSTANDING"].ToString();
                            }
                            else
                            {
                                lblOutstan.Text = "0.00";
                            }
                            string netttl = dt.Rows[i]["AMOUNT"].ToString();

                            if (netttl != "")
                            {
                                TBAmt.Text = dt.Rows[i]["AMOUNT"].ToString();
                            }
                            else
                            {
                                TBAmt.Text = "0.00";
                            }

                            //.HFDSR.Text = dt.Rows[i]["dsrid"].ToString();

                            if (TextBox1.Text == "")
                            {
                                lbl_Flag.Text = "0";
                            }
                            else
                            {
                                lbl_Flag.Text = "1";
                            }
                            //ChkCls.Checked = Convert.ToBoolean(dt.Rows[i]["CLOSED"].ToString());

                            rowIndex++;

                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
                lbl_err.Text = ex.Message.ToString();
            }
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
                command.CommandText =
                    " update tbl_Mdsr set dsrdat = '" + TBdsrdat.Text + "',CustomerID = '" + DDL_Cust.SelectedValue.Trim() + "', CreateBy = '" + Session["user"].ToString() + "', CreateAt = '" + DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "', CompanyId = '" + Session["CompanyID"] + "', BranchId ='" + Session["BranchID"] + "', Username='" + Session["user"].ToString() + "' where dsrid='" + HFdsrID.Value.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                command.ExecuteNonQuery();

                foreach (GridViewRow g1 in GVPro1.Rows)
                {
                    string DDL_Itm = (g1.FindControl("DDL_Itm") as DropDownList).SelectedValue;
                    string DDL_Unt = (g1.FindControl("DDL_Unt") as DropDownList).SelectedValue;
                    string TBQty = (g1.FindControl("TBQty") as TextBox).Text;
                    string TBSalRat = (g1.FindControl("TBSalRat") as TextBox).Text;
                    string TBSalRtrn = (g1.FindControl("TBSalRtrn") as TextBox).Text;                    
                    string TBRecy = (g1.FindControl("TBRecy") as TextBox).Text;
                    string lblOutstan = (g1.FindControl("lblOutstan") as TextBox).Text;
                    string TBAmt = (g1.FindControl("TBAmt") as TextBox).Text;
                    string TBRmk = (g1.FindControl("TBRmk") as TextBox).Text;
                    //string HFDSR = (g1.FindControl("HFDSR") as HiddenField).Value;
                    string HFDSR = (g1.FindControl("HFDSR") as Label).Text;
                    string lbl_Flag = (g1.FindControl("lbl_Flag") as Label).Text;

                    //Detail DSR

                    if (HFDSR != "")
                    {
                        command.CommandText =
                            " update tbl_ddsr set  ProductID ='" + DDL_Itm + "' , untid='" + DDL_Unt + "', Qty = '" + TBQty + "', salrat='" + TBSalRat + "', salrturn ='" + TBSalRtrn + "', recvry='" + TBRecy + "', outstan ='" + lblOutstan + "', Amt='" + TBAmt + "', dsrrmk='" + TBRmk + "', ttlamt='" + tbtotal.Text.Trim() + "', CreateBy='" + Session["user"].ToString() + "', CreateAt='" + DateTime.Today.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture) + "' where dsrid ='" + HFdsrID.Value.Trim() + "' and ddsr='" + HFDSR + "' and CompanyId = '" + Session["CompanyID"] + "'and BranchId ='" + Session["BranchID"] + "'";
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        command.CommandText =
                       " INSERT INTO tbl_ddsr (dsrid, ProductTypeID, ProductID, untid, Qty, salrat, salrturn, recvry, outstan, Amt,  ttlamt, dsrrmk, CompanyId, BranchId, CreateAt, CreateBy) " +
                       " VALUES " +
                       " ('" + HFdsrID.Value.Trim() + "', '', '" + DDL_Itm + "','" + DDL_Unt + "','" + TBQty + "','" + TBSalRat + "','" + TBSalRtrn + "', '" + TBRecy + "','" + lblOutstan + "','" + TBAmt + "','" + tbtotal.Text.Trim() + "','" + TBRmk + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "','" + DateTime.Now.ToShortDateString() + "','" + Session["user"].ToString() + "')";
                        command.ExecuteNonQuery();
                    }

                    #region Credit Sheets

                    command.CommandText = "select CredAmt from tbl_Salcredit where CustomerID='" + DDL_Cust.SelectedValue.Trim() + "'";

                    SqlDataAdapter stksalcre = new SqlDataAdapter(command);

                    DataTable dtsalcre = new DataTable();
                    stksalcre.Fill(dtsalcre);

                    if (dtsalcre.Rows.Count > 0)
                    {
                        double recv = Convert.ToDouble(lblOutstan) - Convert.ToDouble(TBRecy);

                        command.CommandText = " Update tbl_Salcredit set CredAmt = '" + recv + "' where CustomerID='" + DDL_Cust.SelectedValue.Trim() + "'";
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        command.CommandText = " insert into tbl_Salcredit (CustomerID,CredAmt) values('" + DDL_Cust.SelectedValue.Trim() + "','" + lblOutstan + "')";
                        command.ExecuteNonQuery();
                    }

                    #endregion
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
                //FillGrid();
                //ptnSno();
                Response.Redirect("frm_DSR_.aspx");
                //BindDll();
            }

        }
        private int Save()
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

  
                #region DSR

                command.CommandText = " INSERT INTO tbl_Mdsr(dsrdat,CustomerID, CompanyId, BranchId, CreateAt, CreateBy, Isdon, Username) " +
                             " VALUES " +
                             " ('" + TBdsrdat.Text + "','" + DDL_Cust.SelectedValue.Trim() + "','" + Session["CompanyID"] + "', '" + Session["BranchID"] + "','" + DateTime.Now + "','" + Session["user"].ToString() + "',0,'" + Session["user"].ToString() + "')";
                command.ExecuteNonQuery();


                // Master Purchase
                command.CommandText = "select top 1 dsrid from tbl_Mdsr order by dsrid desc";

                SqlDataAdapter adp = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    mdsrId = dt.Rows[0]["dsrid"].ToString();
                }

                foreach (GridViewRow g1 in GVPro1.Rows)
                {
                    DDL_Itm = (g1.FindControl("DDL_Itm") as DropDownList).SelectedValue;
                    DDL_Unt = (g1.FindControl("DDL_Unt") as DropDownList).SelectedValue;
                    TBQty = (g1.FindControl("TBQty") as TextBox).Text;
                    TBSalRat = (g1.FindControl("TBSalRat") as TextBox).Text;
                    TBSalRtrn = (g1.FindControl("TBSalRtrn") as TextBox).Text;
                    lblOutstan = (g1.FindControl("lblOutstan") as TextBox).Text;
                    TBRecy = (g1.FindControl("TBRecy") as TextBox).Text;
                    TBAmt = (g1.FindControl("TBAmt") as TextBox).Text;
                    TBRmk = (g1.FindControl("TBRmk") as TextBox).Text;

                    //Detail Purchase,
                    command.CommandText =
                        " INSERT INTO tbl_ddsr (dsrid, ProductTypeID, ProductID, untid, Qty, salrat, salrturn, recvry, outstan, Amt, ttlamt, dsrrmk, CompanyId, BranchId, CreateAt, CreateBy) " +
                        " VALUES " +
                        " ('" + mdsrId + "', '', '" + DDL_Itm + "','" + DDL_Unt + "','" + TBQty + "','" + TBSalRat + "','" + TBSalRtrn + "', '" + TBRecy + "','" + lblOutstan + "','" + TBAmt + "','" + tbtotal.Text  + "','" + TBRmk + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "','" + DateTime.Now.ToShortDateString() + "','" + Session["user"].ToString() + "')";
                    command.ExecuteNonQuery();

                    #region Credit Sheets

                    /*command.CommandText = "select CredAmt from tbl_Salcredit where CustomerID='" + DDL_Cust.SelectedValue.Trim() + "'";

                    SqlDataAdapter stksalcre = new SqlDataAdapter(command);

                    DataTable dtsalcre = new DataTable();
                    stksalcre.Fill(dtsalcre);

                    if (dtsalcre.Rows.Count > 0)
                    {
                        double recv = Convert.ToDouble(lblOutstan) - Convert.ToDouble(TBRecy);

                        command.CommandText = " Update tbl_Salcredit set CredAmt = '" + recv + "' where CustomerID='" + DDL_Cust.SelectedValue.Trim() + "'";
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        command.CommandText = " insert into tbl_Salcredit (CustomerID,CredAmt) values('" + DDL_Cust.SelectedValue.Trim() + "','" + lblOutstan + "')";
                        command.ExecuteNonQuery();
                    }*/

                    #endregion


                }
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
                res = 1;
                Response.Redirect("frm_DSR_.aspx");
            }

            return res;
        }

        public void BindDLL()
        {
            //Customer Name

            dt_ = new DataTable();
            dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(CustomerID AS VARCHAR(200)) + ']-' + CustomerName ) as [CustomerName], CustomerID from Customers_ where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

            DDL_Cust.DataSource = dt_;
            DDL_Cust.DataTextField = "CustomerName";
            DDL_Cust.DataValueField = "CustomerID";
            DDL_Cust.DataBind();
            DDL_Cust.Items.Insert(0, new ListItem("--Select Customer --", "0"));
        }

        #endregion
        protected void GVDSR_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                GridViewRow row;

                //string PURID = GVDSR.DataKeys[row.RowIndex].Values[0].ToString();

                if (e.CommandName == "Select")
                {
                    row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    string MDSRID = Server.HtmlDecode(GVDSR.Rows[row.RowIndex].Cells[0].Text.ToString());

                    string cmdtxt = "select tbl_Mdsr.CustomerID, CustomerName, replace(convert(NVARCHAR, dsrdat, 101), ' ', '/') as [dsrdat],tbl_Mdsr.dsrid from tbl_Mdsr inner join tbl_ddsr on " +
                        " tbl_Mdsr.dsrid = tbl_ddsr.dsrid inner join Customers_ on tbl_Mdsr.CustomerID = Customers_.CustomerID  inner join Products on tbl_ddsr.ProductID = Products.ProductID " +
                        " where tbl_Mdsr.CompanyId = '" + Session["CompanyID"] + "' and tbl_Mdsr.BranchId= '" + Session["BranchID"] + "' and tbl_Mdsr.dsrid =" + MDSRID + "";

                    //string cmdtxt = " select a.mPurID, b.dPurId, b.mPurId, a.ven_id, a.VndrAdd, a.VndrCntct,a.PurNo, a.mPurDate, a.CreatedBy, a.CreatedAt, a.cnic, a.ntnno, a.tobePrntd,b.Dpurid, b.ProNam, b.ProDes, b.Qty, b.Total, b.subtotl, b.unit, b.cost, b.protyp,b.grossttal from MPurchase a  inner join DPurchase b on a.mPurID = b.mPurID where a.MPurID =" + MPurID + "";

                    SqlCommand cmdSlct = new SqlCommand(cmdtxt, con);
                    SqlDataAdapter adp = new SqlDataAdapter(cmdSlct);

                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        HFdsrID.Value = dt.Rows[0]["dsrid"].ToString();
                        TBdsrdat.Text = dt.Rows[0]["dsrdat"].ToString();
                        DDL_Cust.SelectedValue = dt.Rows[0]["CustomerID"].ToString();

                        string cmdDettxt = " select ddsr,'' as [ITEM TYPE],tbl_Ddsr.ProductID as [ITEMNAME],ProductName,untid as [UNITS],tbl_Ddsr.dsrid,Qty,Amt as [AMOUNT], salrat as [SALERATE],salrturn as [SALERETURN],recvry as [RECOVERY],outstan as [OUTSTANDING],ttlamt, '' as [AMOUNT],dsrrmk as[REMARKS] from tbl_Ddsr  " +
                            "  inner join tbl_Mdsr  on tbl_Ddsr.dsrid = tbl_Mdsr.dsrid inner join Products on  tbl_Ddsr.ProductID =  Products.ProductID" +
                            "  where tbl_Mdsr.CompanyId = '" + Session["CompanyID"] + "' and  tbl_Mdsr.BranchId='" + Session["BranchID"] + "' and tbl_Ddsr.dsrid = " + MDSRID + "";

                        DataTable dt_Det = new DataTable();
                        dt_Det = DataAccess.DBConnection.GetDataTable(cmdDettxt);

                        if (dt_Det.Rows.Count > 0)
                        {
                            GVPro1.DataSource = dt_Det;
                            GVPro1.DataBind();
                            
                            ViewState["dt_adItm"] = dt_Det;

                            for (int j = 0; j < dt_Det.Rows.Count; j++)
                            {  
                                for (int i = 0; i < GVPro1.Rows.Count; i++)
                                {
                                    Label lbl_pro = (Label)GVPro1.Rows[i].FindControl("lblPurItm");
                                    DropDownList DDL_Itm = (DropDownList)GVPro1.Rows[i].FindControl("DDL_Itm");
                                    Label lbl_unt = (Label)GVPro1.Rows[i].FindControl("lbl_unt");
                                    DropDownList DDL_Unt = (DropDownList)GVPro1.Rows[i].FindControl("DDL_Unt");
                                    Label HFDSR = (Label)GVPro1.Rows[i].FindControl("HFDSR");
                                    Label lbl_Flag = (Label)GVPro1.Rows[i].FindControl("lbl_Flag");
                                    TextBox TextBox1 = (TextBox)GVPro1.Rows[i].FindControl("TextBox1");

                                 
                                    //lbl_pro.Text = dt_Det.Rows[j]["ITEMNAME"].ToString();                                    
                                    DDL_Itm.SelectedValue = lbl_pro.Text.Trim();
                                    //lbl_unt.Text = dt_Det.Rows[j]["UNITS"].ToString();                                   
                                    DDL_Unt.SelectedValue = lbl_unt.Text.Trim();
                                    TextBox1.Text = DDL_Itm.SelectedItem.Text; //dt_Det.Rows[j]["ProductName"].ToString();
                                    //HiddenField HFDSR = (HiddenField)GVPro1.Rows[i].FindControl("HFDSR");
                                    //HFDSR.Text = dt_Det.Rows[j]["ddsr"].ToString();
                                    tbtotal.Text = dt_Det.Rows[j]["ttlamt"].ToString();

                                    lbl_Flag.Text = "1";
                                    btnUpd.Enabled = true;
                                  }
                             }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);                        
                        lblalert.Text = "No Record Found!!";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);                
                lblalert.Text = ex.Message;
            }
        }

        protected void GVDSR_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string MPurID = Server.HtmlDecode(GVDSR.Rows[e.RowIndex].Cells[0].Text.ToString());
                //string MPurNo = Server.HtmlDecode(GVDSR.Rows[e.RowIndex].Cells[1].Text.ToString());

                SqlCommand cmd = new SqlCommand();

                cmd = new SqlCommand("sp_del_Dsr", con);
                cmd.Parameters.Add("@mDsrID", SqlDbType.Int).Value = MPurID;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                Response.Redirect("frm_DSR_.aspx");

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
               
                lblalert.Text = ex.Message;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        protected void linkbtnadd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }
        protected void linkbtndel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GVPro1.Rows.Count; i++)
            {
                Label HFDSR = (Label)GVPro1.Rows[i].FindControl("HFDSR");
                SqlCommand cmd = new SqlCommand();

                cmd = new SqlCommand("delete from tbl_ddsr where ddsr = " + HFDSR.Text.Trim() + " and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                FillGrid();

            }//HFDSR
        }
        protected void btnCancl_Click(object sender, EventArgs e)
        {
            Response.Redirect("frm_DSR_.aspx");
        }
        protected void linkmodaldelete_Click(object sender, EventArgs e)
        {

        }
        protected void GVPro1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //Item Type

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(ProductTypeID AS VARCHAR(200)) + ']-' + ProductTypeName ) as [ProductTypeName], ProductTypeID from tbl_producttype where IsActive = 1  and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                for (int i = 0; i < GVPro1.Rows.Count; i++)
                {
                    DropDownList DDL_Itmtyp = (DropDownList)GVPro1.Rows[i].Cells[0].FindControl("DDL_Itmtyp");
                    DDL_Itmtyp.DataSource = dt_;
                    DDL_Itmtyp.DataTextField = "ProductTypeName";
                    DDL_Itmtyp.DataValueField = "ProductTypeID";
                    DDL_Itmtyp.DataBind();
                    DDL_Itmtyp.Items.Insert(0, new ListItem("--Select Items Types--", "0"));
                }

                //Item Name

                dt_ = new DataTable();
                //dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], ProductID from Products where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");
                dt_ = DBConnection.GetQueryData("select ProductID, ProductName from Products where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");
                for (int i = 0; i < GVPro1.Rows.Count; i++)
                {
                    DropDownList DDL_Itm = (DropDownList)GVPro1.Rows[i].Cells[0].FindControl("DDL_Itm");
                    DDL_Itm.DataSource = dt_;
                    DDL_Itm.DataTextField = "ProductName";
                    DDL_Itm.DataValueField = "ProductID";
                    DDL_Itm.DataBind();
                    DDL_Itm.Items.Insert(0, new ListItem("--Select Items --", "0"));
                }

                dt_ = new DataTable();
                //dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(untid AS VARCHAR(200)) + ']-' + untnam ) as [untnam], untid from tbl_unts where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");
                dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(untid AS VARCHAR(200)) + ']-' + untnam ) as [untnam], untid from tbl_unts where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                for (int i = 0; i < GVPro1.Rows.Count; i++)
                {
                    DropDownList DDL_Unt = (DropDownList)GVPro1.Rows[i].Cells[0].FindControl("DDL_Unt");
                    DDL_Unt.DataSource = dt_;
                    DDL_Unt.DataTextField = "untnam";
                    DDL_Unt.DataValueField = "untid";
                    DDL_Unt.DataBind();
                    DDL_Unt.Items.Insert(0, new ListItem("--Select Units--", "0"));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //protected void GVPro1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    if (ViewState["dt_adItm"] != null)
        //    {
        //        DataTable dt = (DataTable)ViewState["dt_adItm"];
        //        DataRow drCurrentRow = null;
        //        int rowIndex = Convert.ToInt32(e.RowIndex);
        //        if (dt.Rows.Count > 1)
        //        {
        //            dt.Rows.Remove(dt.Rows[rowIndex]);
        //            drCurrentRow = dt.NewRow();
        //            ViewState["dt_adItm"] = dt;

        //            GVPro1.DataSource = dt;
        //            GVPro1.DataBind();

        //            SetPreRow();
        //            //ptnSno();
        //        }
        //    }
        //}

        protected void DDL_Itm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < GVPro1.Rows.Count; j++)
                {
                    DropDownList DDL_Itm = (DropDownList)GVPro1.Rows[j].FindControl("DDL_Itm");

                    TextBox TBQty = (TextBox)GVPro1.Rows[j].FindControl("TBQty");

                    TextBox TbSalRat = (TextBox)GVPro1.Rows[j].FindControl("TbSalRat");

                    
                    Label lbl_Flag = (Label)GVPro1.Rows[j].FindControl("lbl_Flag");


                    string query = "select ProductID,Dstk_ItmQty as [Qty],Dstk_salrat as [Rate] " +
                                   " from tbl_dstk where ProductID = " + DDL_Itm.SelectedValue.Trim() + " and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                    SqlCommand cmd = new SqlCommand(query, con);
                    DataTable dt_ = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);

                    adp.Fill(dt_);

                    if (dt_.Rows.Count > 0)
                    {
                        
                        TbSalRat.Text = dt_.Rows[0]["Rate"].ToString();
                        //lbl_Flag.Text = "1";
                        if (lbl_Flag.Text == "0")
                        {
                            TBQty.Text = dt_.Rows[0]["Qty"].ToString();
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
                //lbl_err.Text = ex.Message.ToString();
            }
        }
        protected void DDL_Cust_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string query = " select CredAmt from tbl_Salcredit where CustomerID='" + DDL_Cust.SelectedValue.Trim() + "'";

                SqlCommand command = new SqlCommand(query, con);
                con.Open();
                DataTable dtven = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(dtven);
                command.ExecuteNonQuery();
                for (int i = 0; i < GVPro1.Rows.Count; i++)
                {
                    TextBox lblOutstan = (TextBox)GVPro1.Rows[i].Cells[7].FindControl("lblOutstan");

                    if (dtven.Rows.Count > 0)
                    {
                        lblOutstan.Text = dtven.Rows[0]["CredAmt"].ToString();
                    }
                    else
                    {
                        lblOutstan.Text = "0.00";
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }

        protected void TBQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string saleper="";

                for (int j = 0; j < GVPro1.Rows.Count; j++)
                {
                    DropDownList DDL_Itm = (DropDownList)GVPro1.Rows[j].FindControl("DDL_Itm");
                    TextBox TBQty = (TextBox)GVPro1.Rows[j].FindControl("TBQty");
                    TextBox TextBox1 = (TextBox)GVPro1.Rows[j].FindControl("TextBox1");
                    TextBox TBAmt = (TextBox)GVPro1.Rows[j].FindControl("TBAmt");
                    TextBox TbSalRat = (TextBox)GVPro1.Rows[j].FindControl("TbSalRat");
                    //Label lblttl = (Label)GVPro1.FooterRow.FindControl("lblttl");
                    Label lbl_Flag = (Label)GVPro1.Rows[j].FindControl("lbl_Flag");

                    if (TextBox1.Text == "")
                    {
                        lbl_Flag.Text = "0";
                    }

                    string query = " select saleper,* from Customers_ where CustomerID=" + DDL_Cust.SelectedValue.Trim() + " and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                    SqlCommand command = new SqlCommand(query, con);
                    con.Open();
                    DataTable dt_ = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(command);
                    adp.Fill(dt_);
                    command.ExecuteNonQuery();

                    if (dt_.Rows.Count > 0)
                    {
                        saleper = dt_.Rows[0]["saleper"].ToString();
                    }
                    else
                    {
                        saleper = "0.00";
                    }
                    con.Close();

                    TBAmt.Text = (Convert.ToDouble(TBQty.Text.Trim()) * Convert.ToDouble(TbSalRat.Text.Trim())).ToString();
                }

                float GTotal = 0;
                for (int k = 0; k < GVPro1.Rows.Count; k++)
                {
                    TextBox total = (TextBox)GVPro1.Rows[k].FindControl("TBAmt");

                    double discount = Convert.ToDouble(total.Text) * Convert.ToDouble(saleper) / 100;
                    string ttlamt = (Convert.ToDouble(total.Text) - discount).ToString();

                    GTotal += Convert.ToSingle(ttlamt);
                    tbtotal.Text = GTotal.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void TBRecy_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < GVPro1.Rows.Count; i++)
                {
                    TextBox TBRecy = (TextBox)GVPro1.Rows[i].Cells[6].FindControl("TBRecy");
                    TextBox lblOutstan = (TextBox)GVPro1.Rows[i].Cells[7].FindControl("lblOutstan");

                    if (lblOutstan.Text != "0.00")
                    {
                        double ttlafoutstand = (Convert.ToDouble(lblOutstan.Text.Trim()) - Convert.ToDouble(TBRecy.Text.Trim()));
                        lblOutstan.Text = ttlafoutstand.ToString();
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lblOutstan_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < GVPro1.Rows.Count; i++)
                {
                    TextBox TBAmt = (TextBox)GVPro1.Rows[i].Cells[6].FindControl("TBAmt");
                    TextBox lblOutstan = (TextBox)GVPro1.Rows[i].Cells[7].FindControl("lblOutstan");

                    if (lblOutstan.Text != "0.00")
                    {
                        double ttlafoutstand = (Convert.ToDouble(TBAmt.Text.Trim()) - Convert.ToDouble(lblOutstan.Text.Trim()));
                        double ttlafoutstands = (Convert.ToDouble(tbtotal.Text.Trim()) - Convert.ToDouble(lblOutstan.Text.Trim()));

                        TBAmt.Text = ttlafoutstand.ToString();
                        tbtotal.Text = ttlafoutstands.ToString();                        
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnUpd_Click(object sender, EventArgs e)
        {
            Update();
        }

        protected void GVPro1_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

                    GVPro1.DataSource = dt;
                    GVPro1.DataBind();

                    SetPreRow();
                    //ptnSno();
                }
            }
        }

        protected void GVDSR_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkDon = e.Row.FindControl("lnkSelect") as LinkButton;
                HiddenField hflock = e.Row.FindControl("hflock") as HiddenField;
                if (hflock.Value == "1")
                {
                    lnkDon.Enabled = false;
                }
             
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int j = 0; j < GVPro1.Rows.Count; j++)
                {
                    DropDownList DDL_Itm = (DropDownList)GVPro1.Rows[j].FindControl("DDL_Itm");
                    TextBox TextBox1 = (TextBox)GVPro1.Rows[j].FindControl("TextBox1");

                    TextBox TBQty = (TextBox)GVPro1.Rows[j].FindControl("TBQty");

                    TextBox TbSalRat = (TextBox)GVPro1.Rows[j].FindControl("TbSalRat");


                    Label lbl_Flag = (Label)GVPro1.Rows[j].FindControl("lbl_Flag");


                    string query = "select tbl_dstk.ProductID,Dstk_ItmQty as [Qty],Dstk_salrat as [Rate] " +
                                   " from tbl_dstk inner join Products on tbl_dstk.ProductID = Products.ProductID where ProductName = '" + TextBox1.Text.Trim() + "' and tbl_dstk.CompanyId = '" + Session["CompanyID"] + "' and tbl_dstk.BranchId= '" + Session["BranchID"] + "'";

                    SqlCommand cmd = new SqlCommand(query, con);
                    DataTable dt_ = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);

                    adp.Fill(dt_);

                    if (dt_.Rows.Count > 0)
                    {

                        TbSalRat.Text = dt_.Rows[0]["Rate"].ToString();
                        DDL_Itm.SelectedValue = dt_.Rows[0]["ProductID"].ToString();
                        //lbl_Flag.Text = "1";
                        if (lbl_Flag.Text == "0")
                        {
                            TBQty.Text = dt_.Rows[0]["Qty"].ToString();
                        }
                    }
                    else
                    {
                        TbSalRat.Text = "0.00";
                        TBQty.Text = "0.00";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
                //lbl_err.Text = ex.Message.ToString();
            }
        }

        protected void GVDSR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVDSR.PageIndex = e.NewPageIndex;
            FillGrid();
        }
    }
}