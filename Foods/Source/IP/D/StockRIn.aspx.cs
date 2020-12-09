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
    public partial class StockRIn : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        DataTable dt_;
        SqlTransaction tran;//= new SqlTransaction();
        string itmsiz, ItmQty, PurItm, query;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                SetInitRowPuritm();
                ptnSno();
                TBdat.Text = DateTime.Today.ToString("yyyy/MM/dd");//DateTime.Now.ToShortDateString();
                //BindDDl();
                //FillGrid();
                TBPurdat.Text = DateTime.Today.ToString("yyyy/MM/dd");//DateTime.Now.ToShortDateString();
                chk_act.Checked = true;
                chk_prt.Checked = true;
                pnlpurchase.Visible = false;
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetCat(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select ProductTypeName from tbl_producttype where ProductTypeName like '%" + prefixText + "%'";
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
            string str = "select ProductName from Products where ProductType ='" + protyp + "' and ProductName like '%" + prefixText + "%'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }


        private void SetInitRowPuritm()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Dstk_sizes", typeof(string)));
            dt.Columns.Add(new DataColumn("RDstk_rat", typeof(string)));
            dt.Columns.Add(new DataColumn("Dstk_ItmQty", typeof(string)));

            dr = dt.NewRow();
            dr["Dstk_sizes"] = "0.00";
            dr["RDstk_rat"] = "0.00";
            dr["Dstk_ItmQty"] = "0.00";

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
                        TextBox itmsiz = (TextBox)GVStkItems.Rows[rowIndex].Cells[0].FindControl("itmsiz");
                        TextBox ItmQty = (TextBox)GVStkItems.Rows[rowIndex].Cells[1].FindControl("ItmQty");
                        
                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["Dstk_sizes"] = itmsiz.Text;
                        dt.Rows[i - 1]["Dstk_ItmQty"] = ItmQty.Text;

                        rowIndex++;

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
                            TextBox itmsiz = (TextBox)GVStkItems.Rows[rowIndex].Cells[0].FindControl("itmsiz");
                            TextBox ItmQty = (TextBox)GVStkItems.Rows[rowIndex].Cells[1].FindControl("ItmQty");
                           
                            itmsiz.Text = dt.Rows[i]["Dstk_sizes"].ToString();
                            ItmQty.Text = dt.Rows[i]["Dstk_ItmQty"].ToString();
                           
                            if (itmsiz.Text == "")
                            {
                                itmsiz.Text = "0.00";
                            }

                            if (ItmQty.Text == "")
                            {
                                ItmQty.Text = "0.00";
                            }
                            
                            rowIndex++;
                            
                            float GTotal = 0;
                            for (int k = 0; k < GVStkItems.Rows.Count; k++)
                            {
                                TextBox total = (TextBox)GVStkItems.Rows[k].FindControl("ItmQty");

                                if (total.Text != "")
                                {
                                    GTotal += Convert.ToSingle(total.Text);
                                }
                            }

                            TBttlqty.Text = GTotal.ToString();
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
                }
            }
        }


        private void ptnSno()
        {
            try
            {
                string str = "select isnull(max(cast(RMstk_id as int)),0) as [RMstk_id]  from tbl_RMstk";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(HFStkSONO.Value))
                    {
                        int v = Convert.ToInt32(reader["RMstk_id"].ToString());
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
                    " INSERT INTO tbl_RMstk(RMstk_sono, RMstk_dat, RMstk_Rmk, CreatedBy, CreatedAt, ISActive) " +
                    " VALUES " +
                    " ('" + HFStkSONO.Value + "','" + TBdat.Text.Trim() + "','" + TBrmks.Text.Trim() + "','" +
                    Session["user"].ToString() + "','" + DateTime.Today.ToString("yyyy/MM/dd") + "','" + chk_act.Checked + "')";
                command.ExecuteNonQuery();

                // Master Purchase
                command.CommandText = "select RMstk_id from tbl_RMstk where RMstk_sono = '" + HFStkSONO.Value.Trim() + "'";

                SqlDataAdapter adp = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    MSTkId = dt.Rows[0]["RMstk_id"].ToString();
                }
                DataTable dtstkqty = new DataTable();

                //Detail Stock

                foreach (GridViewRow g1 in GVStkItems.Rows)
                {

                    itmsiz = (g1.FindControl("itmsiz") as TextBox).Text.Trim();
                    ItmQty = (g1.FindControl("ItmQty") as TextBox).Text.Trim();

                    query = "select * from Products where ProductName = '" + TBItms.Text.Trim() + "'";

                    dt_ = DBConnection.GetQueryData(query);
                    if (dt_.Rows.Count > 0)
                    {
                        PurItm = dt_.Rows[0]["ProductID"].ToString();
                    }

                    string quer = "select RDstk_Qty from tbl_RDstk where ProductID = " + PurItm + " and RDstk_unt ='" + itmsiz + "'";

                    command.CommandText = quer;

                    DataTable dtchkqty = new DataTable();

                    SqlDataAdapter Adp = new SqlDataAdapter(command);
                    Adp.Fill(dtchkqty);

                    if (dtchkqty.Rows.Count > 0)
                    {
                        for (int t = 0; t < dtchkqty.Rows.Count; t++)
                        {
                            stkqty = dtchkqty.Rows[t]["RDstk_Qty"].ToString();

                            decimal qty = Convert.ToDecimal(stkqty) + Convert.ToDecimal(ItmQty);
                            command.CommandText = " UPDATE tbl_RDstk SET RDstk_Qty = '" + qty + "' where  ProductID = " + PurItm + " and RDstk_unt='" + itmsiz + "'";
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        command.CommandText = " INSERT INTO tbl_RDstk (procat, RDstk_ItmDes, RDstk_Qty, " +
                            "  RDstk_unt, RDstk_rat, RDstk_purrat, RMstk_id, ProductID) " +
                                " VALUES " +
                                " ('" + TBCat.Text + "','" + TBItms.Text + "', '" + ItmQty + "','" + itmsiz + "','"
                                + TBRat.Text + "','" + TBRat.Text + "' , '" + MSTkId + "', '" + PurItm + "')";
                        
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
            ptnSno();
        }

        return i;
       }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int j = 0;
                j = Save();

                if (j == 1)
                {
                    Response.Redirect("StockRIn.aspx");
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

                        string cmdText2 = " select DPurchase.ProductID as [ProNam],DPurchase.ProDes as [RDstk_ItmDes], DPurchase.Qty as [Dstk_ItmQty], " +
                            " '' as Dstk_Itmwght, DPurchase.Unit as [Dstk_ItmUnt] , Products.Cost as [RDstk_rat], " +
                            " '' as Dstk_salrat, '' as RDstk_purrat from MPurchase inner join DPurchase on MPurchase.MPurID = DPurchase.MPurID " +
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


        protected void GVStkItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void TBCat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Session["cat"] = TBCat.Text.Trim();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void TBRat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal amt = Convert.ToDecimal(TBttlqty.Text) * Convert.ToDecimal(TBRat.Text);
                TBAmt.Text = amt.ToString();

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void TBItms_TextChanged(object sender, EventArgs e)
        {   
            try
            {
                query = " select  * from  Products where ProductName = '" + TBItms.Text.Trim() + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                        PurItm = dt_.Rows[0]["ProductID"].ToString();

                        query = " select '0.00' as [Dstk_ItmQty],RDstk_unt as [Dstk_sizes] from tbl_RDstk where ProductID = '" + PurItm + "'";

                        DataTable dtpro_ = new DataTable();

                        dtpro_ = DBConnection.GetQueryData(query);

                        if (dtpro_.Rows.Count > 0)
                        {
                            GVStkItems.DataSource = dtpro_;
                            GVStkItems.DataBind();

                            ViewState["dt_adItm"] = dtpro_;
                        }

                }
                else
                {
                    query = " INSERT INTO Products " +
                                    " ([ProductName],[ProductType],[CompanyId],[BranchId],[IsActive]) VALUES('"
                                    + TBItms.Text.Trim() + "','" + TBCat.Text.Trim()
                                    + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "','1')";
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ItmQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float GTotal = 0;
                for (int k = 0; k < GVStkItems.Rows.Count; k++)
                {
                    TextBox total = (TextBox)GVStkItems.Rows[k].FindControl("ItmQty");

                    if (total.Text != "")
                    {
                        GTotal += Convert.ToSingle(total.Text);
                    }
                }
                TBttlqty.Text = GTotal.ToString();
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("StockRIn.aspx");
        }
    }
}