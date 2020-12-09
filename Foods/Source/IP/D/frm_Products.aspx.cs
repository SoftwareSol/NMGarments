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
    public partial class frm_Products : System.Web.UI.Page
    {
        SqlConnection con = DataAccess.DBConnection.connection();
        DataTable dt_ = null;
        int i = 0;
        string query;
        public static string branch, company; 

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Initials

            if (!IsPostBack)
            {
                //ShwProId();
                SetInitRow();
                BindDDL();
                branch = Session["BranchID"].ToString();
                company = Session["CompanyID"].ToString();
                TBProdat.Text = DateTime.Now.ToShortDateString();
                btnDel.Enabled = false;

                TextBox TBCat = null;
                TextBox TBItmCode = null;

                for (int i = 0; i < GVPro.Rows.Count; i++)
                {
                    TBCat = (TextBox)GVPro.Rows[i].Cells[5].FindControl("TBCat");

                    TBItmCode = (TextBox)GVPro.Rows[i].Cells[1].FindControl("TBItmCode"); 

                    //TBItmCode.Text = ShwProId();
                }

                TBCat.Focus();

            #endregion

            }
        }

        #region Web Methods
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetCat(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select ProductTypeName from tbl_producttype where IsActive = 1 and ProductTypeName like '%" + prefixText + "%' and CompanyId='" + 
                company + "' and BranchId='" + branch + "'";
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
            string str = "select brndnam from tbl_brnd where IsActive = 1 and brndnam like '%" + prefixText + "%' and CompanyId='" + company + "' and BranchId='" + branch + "'";
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
            string str = "select orignnam from tbl_orign where orignnam like '%" + prefixText + "%' and CompanyId='" + company + "' and BranchId='" + branch + "'";
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
            string str = "select pakgsiz from tbl_pakgsiz where pakgsiz like '%" + prefixText + "%' and CompanyId='" + company + "' and BranchId='" + branch + "'";
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
            string str = "select untnam from tbl_unts where untnam like '%" + prefixText + "%' and CompanyId='" + company + "' and BranchId='" + branch + "'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }


        #endregion

        public string ShwProId()
        {
            string HFProcd = "";
            try
            {
                string str = "select top 1 Pro_Code from products order by Pro_Code desc";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                DataTable dt = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                adp.Fill(dt);

                if (dt.Rows.Count > 0)
                {

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (string.IsNullOrEmpty(HFProcd))
                        {
                            int v = Convert.ToInt32(reader["Pro_Code"].ToString());
                            int b = v + 1;
                            HFProcd = b.ToString();

                        }
                    }
                }
                else
                {

                }
                con.Close();

            }
            catch (Exception ex)
            {  
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "alert("+ ex.Message +");", true);       
            }
            return HFProcd;
        }

        public string ShwProId(string procod)
        {   
            try
            {                
                //if (string.IsNullOrEmpty(procod))
                {
                    int v = Convert.ToInt32(procod);
                    int b = v + 1;
                    procod = b.ToString();

                }
              
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "alert(" + ex.Message + ");", true);               
            }
            return procod;
        }

        public void BindDDL()
        {
            try
            {
                //Item Name

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData("select ProductID, ProductName from Products where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                DDL_Itm.DataSource = dt_;
                DDL_Itm.DataTextField = "ProductName";
                DDL_Itm.DataValueField = "ProductID";
                DDL_Itm.DataBind();
                DDL_Itm.Items.Insert(0, new ListItem("--Select Items --", "0"));

            }
            catch (Exception ex)
            {
               lbl_err.Text = ex.Message;
            }
        }
      

        protected void linkbtnadd_Click(object sender, EventArgs e)
        {
            AddNewRow();
        }


        private void SetInitRow()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Category", typeof(string)));
            dt.Columns.Add(new DataColumn("Pro_Code", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Brand", typeof(string)));
            dt.Columns.Add(new DataColumn("Origin", typeof(string)));
            dt.Columns.Add(new DataColumn("Packing", typeof(string)));
            dt.Columns.Add(new DataColumn("Units", typeof(string)));
            dt.Columns.Add(new DataColumn("PURCHASEPRICE", typeof(string)));
            dt.Columns.Add(new DataColumn("SALEPRICE", typeof(string)));
            dt.Columns.Add(new DataColumn("RMKS", typeof(string)));


            dr = dt.NewRow();

            dr["Category"] = string.Empty;
            dr["Pro_Code"] = "0";
            dr["Description"] = string.Empty;
            dr["Brand"] = string.Empty;
            dr["Origin"] = string.Empty;
            dr["Packing"] = string.Empty;
            dr["Units"] = string.Empty;
            dr["PURCHASEPRICE"] = "0";
            dr["SALEPRICE"] = "0";
            dr["RMKS"] = string.Empty;

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["dt_adItm"] = dt;

            GVPro.DataSource = dt;
            GVPro.DataBind();
           
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

                        TextBox TBCat = (TextBox)GVPro.Rows[rowIndex].Cells[0].FindControl("TBCat");
                        TextBox TBItmCode = (TextBox)GVPro.Rows[rowIndex].Cells[1].FindControl("TBItmCode");
                        TextBox TBDesc = (TextBox)GVPro.Rows[rowIndex].Cells[2].FindControl("TBDesc");
                        TextBox TBBrnd = (TextBox)GVPro.Rows[rowIndex].Cells[3].FindControl("TBBrnd");
                        TextBox TBOrig = (TextBox)GVPro.Rows[rowIndex].Cells[4].FindControl("TBOrig");
                        TextBox TBPack = (TextBox)GVPro.Rows[rowIndex].Cells[5].FindControl("TBPack");
                        TextBox TBUnit = (TextBox)GVPro.Rows[rowIndex].Cells[6].FindControl("TBUnit");
                        TextBox TBPurPriz = (TextBox)GVPro.Rows[rowIndex].Cells[7].FindControl("TBPurPriz");
                        TextBox TBSalPric = (TextBox)GVPro.Rows[rowIndex].Cells[8].FindControl("TBSalPric");
                        TextBox TBrmks = (TextBox)GVPro.Rows[rowIndex].Cells[9].FindControl("TBrmks");

                        drRow = dt.NewRow();

                        dt.Rows[i - 1]["Category"] = TBCat.Text;
                        dt.Rows[i - 1]["Pro_Code"] = TBItmCode.Text;
                        dt.Rows[i - 1]["Description"] = TBDesc.Text;
                        dt.Rows[i - 1]["Brand"] = TBBrnd.Text;
                        dt.Rows[i - 1]["Origin"] = TBOrig.Text;
                        dt.Rows[i - 1]["Packing"] = TBPack.Text;
                        dt.Rows[i - 1]["Units"] = TBUnit.Text;
                        dt.Rows[i - 1]["PURCHASEPRICE"] = TBPurPriz.Text;
                        dt.Rows[i - 1]["SALEPRICE"] = TBSalPric.Text;
                        dt.Rows[i - 1]["RMKS"] = TBrmks.Text;
                        
                        rowIndex++;

                        //int v = Convert.ToInt32(TBItmCode.Text);
                       // int b = v + 1;
                        //TBItmCode.Text = b.ToString();

                        TBCat.Focus();
                    }

                    dt.Rows.Add(drRow);
                    ViewState["dt_adItm"] = dt;

                    GVPro.DataSource = dt;
                    GVPro.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreRow();

           // For Setting New Row Value by Addition of Last Row..

            //TextBox TBItmCode_ = null;

            //for (int i = 0; i < GVPro.Rows.Count-1; i++)
            //{
            //    TBItmCode_ = (TextBox)GVPro.Rows[i].Cells[1].FindControl("TBItmCode");
            //}

            //foreach (GridViewRow row in GVPro.Rows)
            //{
            //    var textbox = row.FindControl("TBItmCode") as TextBox;

            //    if (string.IsNullOrEmpty(textbox.Text))
            //    {
            //        textbox.Text = ShwProId(TBItmCode_.Text.Trim());
            //    }
            //}

            //TBItmCode_.Enabled = false;
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
                            TextBox TBCat = (TextBox)GVPro.Rows[rowIndex].Cells[0].FindControl("TBCat");
                            TextBox TBItmCode = (TextBox)GVPro.Rows[rowIndex].Cells[1].FindControl("TBItmCode");
                            TextBox TBDesc = (TextBox)GVPro.Rows[rowIndex].Cells[2].FindControl("TBDesc");
                            TextBox TBBrnd = (TextBox)GVPro.Rows[rowIndex].Cells[3].FindControl("TBBrnd");
                            TextBox TBOrig = (TextBox)GVPro.Rows[rowIndex].Cells[4].FindControl("TBOrig");
                            TextBox TBPack = (TextBox)GVPro.Rows[rowIndex].Cells[5].FindControl("TBPack");
                            TextBox TBUnit = (TextBox)GVPro.Rows[rowIndex].Cells[6].FindControl("TBUnit");
                            TextBox TBPurPriz = (TextBox)GVPro.Rows[rowIndex].Cells[7].FindControl("TBPurPriz");
                            TextBox TBSalPric = (TextBox)GVPro.Rows[rowIndex].Cells[8].FindControl("TBSalPric");
                            TextBox TBrmks = (TextBox)GVPro.Rows[rowIndex].Cells[9].FindControl("TBrmks");

                            TBCat.Text = dt.Rows[i]["Category"].ToString();
                            TBItmCode.Text = dt.Rows[i]["Pro_Code"].ToString();
                            TBDesc.Text = dt.Rows[i]["Description"].ToString();
                            TBBrnd.Text = dt.Rows[i]["Brand"].ToString();
                            TBOrig.Text = dt.Rows[i]["Origin"].ToString();
                            TBPack.Text = dt.Rows[i]["Packing"].ToString();
                            TBUnit.Text = dt.Rows[i]["Units"].ToString();
                            TBPurPriz.Text = dt.Rows[i]["PURCHASEPRICE"].ToString();
                            TBSalPric.Text = dt.Rows[i]["SALEPRICE"].ToString();
                            TBrmks.Text = dt.Rows[i]["RMKS"].ToString();

                            rowIndex++;

                            TBCat.Focus();
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

        protected void GVPro_RowDeleting(object sender, GridViewDeleteEventArgs e)
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

                    GVPro.DataSource = dt;
                    GVPro.DataBind();

                    SetPreRow();
                }
            }
        }

        protected void linkbtndel_Click(object sender, EventArgs e)
        {

        }

        private int delpro()
        {
            int i = 1;
            try
            {
                string sqlquery = " delete from  Products where ProductID = '" + DDL_Itm.SelectedValue.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                DBConnection db = new DBConnection();
                db.CRUDRecords(sqlquery);


            }
            catch (Exception ex)
            {
                lbl_err.Text = ex.Message;               
            }
            return i;
        }

        //protected void linkmodaldelete_Click(object sender, EventArgs e)
        //{

        //    con.Open();

        //    SqlCommand command = con.CreateCommand();
        //    SqlTransaction transaction;

        //    // Start a local transaction.
        //    transaction = con.BeginTransaction("SampleTransaction");

        //     //Must assign both transaction object and connection 
        //     //to Command object for a pending local transaction
        //    command.Connection = con;
        //    command.Transaction = transaction;

        //    try
        //    {
        //        int a, b;
        //        a = delpro();                

        //        if (a == 1)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
        //            lblalert.Text = "Product Has Been Delete!";

        //            SubHeadCatFiv.Value = "";
        //            HFSubHeadCatFivID.Value = "";
        //            HFProID.Value = "";
        //            Response.Redirect("frm_Products.aspx");
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
        //            lblalert.Text = "Some thing is wrong Call the Administrator!!";
        //        }

        //        if (con != null && con.State == ConnectionState.Closed)
        //        {
        //            con.Open();
        //        }


        //        if (con != null && con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }


        //         //Attempt to commit the transaction.
        //         transaction.Commit();


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
        //        //FillGrid();
        //    }
        //}

        protected void btnDel_Click(object sender, EventArgs e)
        {
            int o = 0;
            o = delpro();

            if (o == 1)
            {
                Response.Redirect("frm_Products.aspx");
            }
        }

        private int Save()
        {
            int j = 1;

            foreach (GridViewRow g1 in GVPro.Rows)
            {

                string TBCat = (g1.FindControl("TBCat") as TextBox).Text;

                string TBItmCode = (g1.FindControl("TBItmCode") as TextBox).Text;

                string TBDesc = (g1.FindControl("TBDesc") as TextBox).Text;

                string TBBrnd = (g1.FindControl("TBBrnd") as TextBox).Text;

                string TBOrig = (g1.FindControl("TBOrig") as TextBox).Text;

                string TBPack = (g1.FindControl("TBPack") as TextBox).Text;

                string TBUnit = (g1.FindControl("TBUnit") as TextBox).Text;

                string TBPurPriz = (g1.FindControl("TBPurPriz") as TextBox).Text;

                string TBSalPric = (g1.FindControl("TBSalPric") as TextBox).Text;

                string TBrmks = (g1.FindControl("TBrmks") as TextBox).Text;


              


                query = "INSERT INTO [Products] ([ProductName] ,[ProductType] ,[Brand] ,[Origin] " +
                " ,[Packing] ,[Unit] ,[TradPrice] ,[RetalPrice], [WholSalprice], [Pro_Rmks], [CreatedBy] ,[CreatedAt] ,[Pro_Code] " +
                " ,[IsActive], CompanyId, BranchId) VALUES ('" + TBDesc + "' , '" + TBCat + "' , '" + TBBrnd + "' ," +
                " '" + TBOrig + "' , '" + TBPack + "' , '" + TBUnit + "' , '" + TBPurPriz + "' , '" + TBSalPric + "','0' , '"
                + TBrmks + "' , '" + Session["user"].ToString() +
                "' , '" + DateTime.Now + "' , '" + TBItmCode + "', '1','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";


                con.Open();


                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.ExecuteNonQuery();

                }
                con.Close();

            }
            return j;
        }

        private int Update()
        {
            int j = 1;

            foreach (GridViewRow g1 in GVPro.Rows)
            {

                string TBCat = (g1.FindControl("TBCat") as TextBox).Text;

                string TBItmCode = (g1.FindControl("TBItmCode") as TextBox).Text;

                string TBDesc = (g1.FindControl("TBDesc") as TextBox).Text;

                string TBBrnd = (g1.FindControl("TBBrnd") as TextBox).Text;

                string TBOrig = (g1.FindControl("TBOrig") as TextBox).Text;

                string TBPack = (g1.FindControl("TBPack") as TextBox).Text;

                string TBUnit = (g1.FindControl("TBUnit") as TextBox).Text;

                string TBPurPriz = (g1.FindControl("TBPurPriz") as TextBox).Text;

                string TBSalPric = (g1.FindControl("TBSalPric") as TextBox).Text;

                string TBrmks = (g1.FindControl("TBrmks") as TextBox).Text;


                query = "UPDATE [dbo].[Products] SET [ProductName] ='" + TBDesc + "' ,[ProductType] = '" + TBCat +
                    "' ,[Brand] = '" + TBBrnd + "' ,[Origin] = '" + TBOrig + "' ,[Packing] = '" + TBPack +
                    "' ,[Unit] = '" + TBUnit + "' ,[TradPrice] = '" + TBPurPriz + "', [RetalPrice] ='" + TBSalPric +
                    "' ,[WholSalprice] = '0' ,[Pro_Rmks]= '" + TBrmks  +
                    "' ,[Pro_Code] = '" + TBItmCode + "' ,[IsActive] = 1  WHERE ProductID = " + DDL_Itm.SelectedValue.Trim() + "";


                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                 
                    cmd.ExecuteNonQuery();

                }
                con.Close();

            }
            return j;
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                foreach (GridViewRow g1 in GVPro.Rows)
                {

                    string TBc = (g1.FindControl("TBCat") as TextBox).Text;
                    string ItmCode = (g1.FindControl("TBItmCode") as TextBox).Text;
                    string TBDesc = (g1.FindControl("TBDesc") as TextBox).Text;


                    if (TBc == "")
                    {
                        v_gvpro.Text = "Please Fill Category..";
                    }
                    else if (TBDesc == "")
                    {
                        v_gvpro.Text = "Please Fill Products..";
                    }
                    else if (ItmCode == "" || ItmCode == "0")
                    {
                        v_gvpro.Text = "Please Fill Product Code..";
                    }
                    else
                    {
                        if (HFProID.Value != "")
                        {
                            v_gvpro.Text = "";
                            int b;
                            b = Update();

                            if (b == 1)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Product Update!');", true);                              
                                Response.Redirect("frm_Products.aspx");
                            }
                            else
                            {
                                lbl_err.Text = "Not Updated!";
                            }
                        }
                        else
                        {
                            DataTable dtchkpro_ = new DataTable();
                            dtchkpro_ = DBConnection.GetQueryData("select Pro_Code from Products where Pro_Code='" + ItmCode + "'");
                        
                            if (dtchkpro_.Rows.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Code Already Exits in Some Product!');", true);
                                //return;
                            }
                            else
                            {
                                v_gvpro.Text = "";
                                int a;
                                a = Save();

                                if (a == 1)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Product Save!');", true);                                   
                                    Response.Redirect("frm_Products.aspx");
                                }
                                else
                                {
                                    lbl_err.Text = "Not Saved!";
                                }
                            }
                        }
                        SetInitRow();
                        BindDDL();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);
            }
            finally
            {
                con.Close();
            }
        
        }

        protected void DDL_Itm_SelectedIndexChanged(object sender, EventArgs e)
        {
            //From Head Five Table;            
            using (SqlCommand cmdHFiv = new SqlCommand())
            {
                con.Close();
                cmdHFiv.CommandText = " select ProductID, ProductName as [Description],Pro_Code, ProductType as [Category],Brand as [Brand], Origin as [Origin], Packing as [Packing], unit as [Units], TradPrice as [PURCHASEPRICE], RetalPrice as [SALEPRICE], WholSalprice as [WHOLESALPRICE],Pro_Rmks as [RMKS]  from Products  where ProductID = '" + DDL_Itm.SelectedValue.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                cmdHFiv.Connection = con;
                con.Open();

                DataTable dtHFiv = new DataTable();
                SqlDataAdapter adpItm = new SqlDataAdapter(cmdHFiv);
                adpItm.Fill(dtHFiv);
                if (dtHFiv.Rows.Count > 0)
                {
                    HFProID.Value = dtHFiv.Rows[0]["ProductID"].ToString();
                    //HFSubHeadCatFivID.Value = dtHFiv.Rows[0]["subheadcategoryfiveID"].ToString();
                    GVPro.DataSource = dtHFiv;
                    GVPro.DataBind();

                }else
                {
                    lbl_err.Text = "Not Record Found!";                   
                }

                con.Close();
            }
            for (int i = 0; i < GVPro.Rows.Count; i++)
            {
                LinkButton linkbtnadd = (LinkButton)GVPro.Rows[i].Cells[0].FindControl("linkbtnadd");
                TextBox TBItmCode = (TextBox)GVPro.Rows[i].Cells[1].FindControl("TBItmCode");
                linkbtnadd.Enabled = false;
                TBItmCode.Enabled = false;
            }
            btnDel.Enabled = true;
        }

        protected void GVPro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
                {
                //Item Type

                //dt_ = new DataTable();
                //dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(ProductTypeID AS VARCHAR(200)) + ']-' + ProductTypeName ) as [ProductTypeName], ProductTypeID from tbl_producttype where IsActive = 1 and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                //for (int i = 0; i < GVPro.Rows.Count; i++)
                //{
                //    DropDownList DDL_Itmtyp = (DropDownList)GVPro.Rows[i].Cells[0].FindControl("DDL_Itmtyp");
                //    DDL_Itmtyp.DataSource = dt_;
                //    DDL_Itmtyp.DataTextField = "ProductTypeName";
                //    DDL_Itmtyp.DataValueField = "ProductTypeID";
                //    DDL_Itmtyp.DataBind();
                //    DDL_Itmtyp.Items.Insert(0, new ListItem("--Select Items Types--", "0"));
                //}

                //Item Name

                //dt_ = new DataTable();
                //dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], ProductID from Products where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                //DDL_Itm.DataSource = dt_;
                //DDL_Itm.DataTextField = "ProductName";
                //DDL_Itm.DataValueField = "ProductID";
                //DDL_Itm.DataBind();
                //DDL_Itm.Items.Insert(0, new ListItem("--Select Items --", "0"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancl_Click(object sender, EventArgs e)
        {
            Response.Redirect("frm_Products.aspx");
        }

        protected void TBCat_TextChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow g1 in GVPro.Rows)
            {
                string TBC = (g1.FindControl("TBCat") as TextBox).Text;
                
                if (TBC == "")
                {
                    v_gvpro.Text = "Please Fill Category";
                }
                else
                {
                    for (int i = 0; i < GVPro.Rows.Count; i++)
                    {
                        TextBox TBCat = (TextBox)GVPro.Rows[i].Cells[0].FindControl("TBCat");

                        query = "select * from tbl_producttype where ProductTypeName='" + TBCat.Text.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

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
                                            " ([ProductTypeID],[ProductTypeName],[CreateBy],[CreatedAt],[IsActive],[CompanyId],[BranchId]) VALUES('" + procatid + "','" + TBCat.Text.Trim() + "','" + Session["user"].ToString() +
                                            " ','" + DateTime.Now + "','true','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                            con.Open();

                            using (SqlCommand cmd = new SqlCommand(query, con))
                            {
                                cmd.ExecuteNonQuery();
                            }
                            con.Close();
                        }
                    }

                    v_gvpro.Text = "";
                }
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

        protected void TBBrnd_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < GVPro.Rows.Count; i++)
            {
                TextBox TBBrnd = (TextBox)GVPro.Rows[i].Cells[2].FindControl("TBBrnd");

                query = "select * from tbl_brnd where brndnam='" + TBBrnd.Text.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    //Do Noting
                }
                else
                {
                    query = " select top 1 brndid as [brndid]  from tbl_brnd where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' order by brndid desc ";

                    dt_ = DBConnection.GetQueryData(query);

                    if (dt_.Rows.Count > 0)
                    {
                        string brndid = dt_.Rows[0]["brndid"].ToString();
                    }
                    string brnedid = RandomNumber().ToString();

                    query = " INSERT INTO tbl_brnd " +
                                    " (brndnam,IsActive,CreateBy,CreateAt,companyid,branchid) VALUES('" + TBBrnd.Text.Trim() + "','true','" + Session["user"].ToString() +
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

        protected void TBOrig_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < GVPro.Rows.Count; i++)
            {
                TextBox TBOrig = (TextBox)GVPro.Rows[i].Cells[3].FindControl("TBOrig");

                query = "select * from tbl_orign where orignnam ='" + TBOrig.Text.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    //Do Noting
                }
                else
                {
                    query = " select top 1 orignid as [orignid]  from tbl_orign where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' order by orignid desc ";

                    dt_ = DBConnection.GetQueryData(query);

                    if (dt_.Rows.Count > 0)
                    {
                        string orignid = dt_.Rows[0]["orignid"].ToString();
                    }
                    string orgnid = RandomNumber().ToString();

                    query = " INSERT INTO tbl_orign " +
                                    " ([orignnam],[IsActive],[CreateAt],[CreateBy],[companyid],[branchid]) VALUES("
                                    + "'" + TBOrig.Text.Trim() + "','true','" + DateTime.Now +
                                    " ','" + Session["user"].ToString() + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }

        }

        protected void TBUnit_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < GVPro.Rows.Count; i++)
            {
                TextBox TBUnit = (TextBox)GVPro.Rows[i].Cells[5].FindControl("TBUnit");

                query = "select * from tbl_unts where untnam='" + TBUnit.Text.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    //Do Noting
                }
                else
                {
                    query = " select top 1 untid as [untid]  from tbl_unts where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' order by untid desc ";

                    dt_ = DBConnection.GetQueryData(query);
                    if (dt_.Rows.Count > 0)
                    {
                        string untid = dt_.Rows[0]["untid"].ToString();
                    }
                    string unitid = RandomNumber().ToString();

                    query = " INSERT INTO tbl_unts " +
                                    " ([untnam],[CompanyId],[BranchId]) VALUES('" + TBUnit.Text.Trim() + "','"  + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }

        }

        protected void TBPack_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < GVPro.Rows.Count; i++)
            {
                TextBox TBPack = (TextBox)GVPro.Rows[i].Cells[0].FindControl("TBPack");

                query = "select * from tbl_pakgsiz where pakgsiz='" + TBPack.Text.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    //Do Noting
                }
                else
                {
                    query = " select top 1 pakgsizid as [pakgsizid]  from tbl_pakgsiz where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' order by pakgsizid desc ";

                    dt_ = DBConnection.GetQueryData(query);
                    if (dt_.Rows.Count > 0)
                    {
                        string pakgsizid = dt_.Rows[0]["pakgsizid"].ToString();
                    }
                    string procatid = RandomNumber().ToString();

                    query = " INSERT INTO tbl_pakgsiz " +
                                    " ([pakgsiz],[IsActive],[CreateAt],[CreateBy],[companyid],[branchid]) VALUES('" + TBPack.Text.Trim() + "','true','" + DateTime.Now +
                                    " ','" + Session["user"].ToString() + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }

        }

        protected void TBDesc_TextChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow g1 in GVPro.Rows)
            {
                string TBDesc = (g1.FindControl("TBDesc") as TextBox).Text;
                if (TBDesc == "")
                {
                    v_gvpro.Text = "Please Fill Description";
                }
                else
                {
                    v_gvpro.Text = "";
                }
   
            }
            
        }      
    }
}