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
    public partial class frm_Products_ : System.Web.UI.Page
    {
        SqlConnection con = DataAccess.DBConnection.connection();
        DataTable dt_ = null;
        int i = 0;
        string query;

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Initials

            if (!IsPostBack)
            {
                SetInitRow();
                BindDDL();
                TBProdat.Text = DateTime.Now.ToShortDateString();
                btnDel.Enabled = false;
                #region Item Code

                //ShowAccountCategoryFiveID();

                //ptnHead();
                //ptnSubHead();
                //ptnSHCat();
                //ptnSubHeadCatFour();
                //ptnSubHeadCatFive();

                //HFProID.Value = HFHead.Value + HFSubHead.Value + SubHeadCat.Value + SubHeadCatFou.Value + SubHeadCatFiv.Value;

                #endregion
            }
            #endregion

        }


        public void ShwProId()
        {
            try
            {
                string str = "exec SP_getProCode";
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
                        if (string.IsNullOrEmpty(HFProcd.Value))
                        {
                            int v = Convert.ToInt32(reader["Pro Code"].ToString());
                            int b = v + 1;
                            HFProcd.Value = b.ToString();

                        }
                    }
                }
                else
                {
                   // HFProcd.Value = "BP00 1";

                }
                con.Close();

            }
            catch (Exception ex)
            {
                // throw ex;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //lbl_Heading.Text = "Error!";
                lblalert.Text = ex.Message;
            }
        }
        
        // Head
        public void ptnHead()
        {
            try
            {

                string str = "select isnull(max(cast(HeadID as int)),0) as [HeadID]  from Head";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(HFHead.Value))
                    {
                        int v = Convert.ToInt32(reader["HeadID"].ToString());
                        int b = v + 1;
                        HFHead.Value =  b.ToString();
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // Sub Head
        public void ptnSubHead()
        {
            try
            {

                string str = "select isnull(max(cast(SubHeadID as int)),0) as [SubHeadID]  from SubHead";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(HFSubHead.Value))
                    {
                        int v = Convert.ToInt32(reader["SubHeadID"].ToString());
                        int b = v + 1;
                        HFSubHead.Value = b.ToString();
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        // Sub Head Categories
        public void ptnSHCat()
        {
            try
            {

                string str = "select isnull(max(cast(SubHeadCategoriesID as int)),0) as [SubHeadCategoriesID]  from SubHeadCategories";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(SubHeadCat.Value))
                    {
                        int v = Convert.ToInt32(reader["SubHeadCategoriesID"].ToString());
                        int b = v + 1;
                        SubHeadCat.Value = b.ToString();
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Sub Head Cat Four
        public void ptnSubHeadCatFour()
        {
            try
            {

                string str = "select isnull(max(cast(subheadcategoryfourID as int)),0) as [subheadcategoryfourID]  from subheadcategoryfour";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(SubHeadCatFou.Value))
                    {
                        int v = Convert.ToInt32(reader["subheadcategoryfourID"].ToString());
                        int b = v + 1;
                        SubHeadCatFou.Value =  b.ToString();
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Sub Head Cat Five
        public void ptnSubHeadCatFive()
        {
            string id = "";
            DBConnection db = new DBConnection();
            //id = db.ptnID();

            //try
            //{

            //    string str = "select isnull(max(cast(subheadcategoryfiveID as int)),0) as [subheadcategoryfiveID]  from subheadcategoryfive";
            //    SqlCommand cmd = new SqlCommand(str, con);
            //    con.Open();

            //    SqlDataReader reader = cmd.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        if (string.IsNullOrEmpty(SubHeadCatFiv.Value))
            //        {
            //            int v = Convert.ToInt32(reader["subheadcategoryfiveID"].ToString());
            //            int b = v + 1;
            //            SubHeadCatFiv.Value = b.ToString();
            //        }
            //    }
            //    con.Close();
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }


        
        public void BindDDL()
        {
            try
            {
                //Units
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(untid AS VARCHAR(200)) + ']-' + untnam ) as [untnam], untid from tbl_unts where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                for (int i = 0; i < GVPro.Rows.Count; i++)
                {
                    DropDownList DDL_Unt = (DropDownList)GVPro.Rows[i].Cells[0].FindControl("DDL_Unt");
                    DDL_Unt.DataSource = dt_;
                    DDL_Unt.DataTextField = "untnam";
                    DDL_Unt.DataValueField = "untid";
                    DDL_Unt.DataBind();
                    DDL_Unt.Items.Insert(0, new ListItem("--Select Units--", "0"));
                }

                //Item Type

                dt_ = new DataTable();
                //dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(ProductTypeID AS VARCHAR(200)) + ']-' + ProductTypeName ) as [ProductTypeName], ProductTypeID from tbl_producttype");
                dt_ = DBConnection.GetQueryData("select * from tbl_producttype where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                for (int i = 0; i < GVPro.Rows.Count; i++)
                {
                    DropDownList DDL_Itmtyp = (DropDownList)GVPro.Rows[i].Cells[0].FindControl("DDL_Itmtyp");
                    DDL_Itmtyp.DataSource = dt_;
                    DDL_Itmtyp.DataTextField = "ProductTypeName";
                    DDL_Itmtyp.DataValueField = "ProductTypeID";
                    DDL_Itmtyp.DataBind();
                    DDL_Itmtyp.Items.Insert(0, new ListItem("--Select Items Types--", "0"));
                }

                //Item Name

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], ProductID from Products where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                DDL_Itm.DataSource = dt_;
                DDL_Itm.DataTextField = "ProductName";
                DDL_Itm.DataValueField = "ProductID";
                DDL_Itm.DataBind();
                DDL_Itm.Items.Insert(0, new ListItem("--Select Items --", "0"));



                //using (SqlCommand cmdpar = new SqlCommand())
                //{
                //    //con.Close();
                //    //cmdpar.CommandText = " select rtrim('[' + CAST(SubHeadCategoriesGeneratedID AS VARCHAR(200)) + ']-' + SubHeadCategoriesName ) as [SubHeadCategoriesName], SubHeadCategoriesID from SubHeadCategories ";

                //    cmdpar.CommandText = " ";

                //    cmdpar.Connection = con;
                //    con.Open();

                //    DataTable dtpar = new DataTable();
                //    SqlDataAdapter adp = new SqlDataAdapter(cmdpar);
                //    adp.Fill(dtpar);



                //    con.Close();
                //}

                //using (SqlCommand cmdItmnam = new SqlCommand())
                //{
                //    con.Close();
                //    //cmdpar.CommandText = "  ";

                //    cmdItmnam.CommandText = " ";

                //    cmdItmnam.Connection = con;
                //    con.Open();

                //    DataTable dtItmnam = new DataTable();
                //    SqlDataAdapter adpItmnam = new SqlDataAdapter(cmdItmnam);
                //    adpItmnam.Fill(dtItmnam);

                //    for (int i = 0; i < GVPro.Rows.Count; i++)
                //    {
                //        DropDownList DDL_Itmtyp = (DropDownList)GVPro.Rows[i].Cells[0].FindControl("DDL_Itmtyp");
                //        DDL_Itmtyp.DataSource = dtItmnam;
                //        DDL_Itmtyp.DataTextField = "";
                //        DDL_Itmtyp.DataValueField = "";
                //        DDL_Itmtyp.DataBind();
                //        DDL_Itmtyp.Items.Insert(0, new ListItem("--Select Items Types--", "0"));
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


        public void ShowAccountCategoryFiveID()
        {
            try
            {

                string str = "select max(cast(subheadcategoryfiveID as int))  as [subheadcategoryfiveID] from subheadcategoryfive";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                DataTable dt_ = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt_);
                if (dt_.Rows.Count <= 0)
                {
                    SubHeadCatFiv.Value = "MB0000001";
                }
                else
                { 
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        //SubHeadCatFiv.Value = "";

                        //if (string.IsNullOrEmpty(SubHeadCatFiv.Value) )                        
                        {
                            int v = Convert.ToInt32(reader["subheadcategoryfiveID"].ToString());
                            int b = v + 1;
                            SubHeadCatFiv.Value = "MB000000" + b.ToString();
                        }
                    }

                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private int savePro()
        {
            int k = 1;
            try
            {
               
                foreach (GridViewRow g1 in GVPro.Rows)
                {
                    ShowAccountCategoryFiveID();

                    

                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return k;
        }

        private int saveHeadFiv()
        {
            int j = 1;
            try
            {                    
                    subheadcategoryfive subheadfive = new subheadcategoryfive();

                    foreach (GridViewRow g1 in GVPro.Rows)
                    {
                        if (HFSubHeadCatFivID.Value == "")
                        {
                            ShowAccountCategoryFiveID();
                        }
                        string TBItmNam = (g1.FindControl("TBItmNam") as TextBox).Text;
                        string DDL_Unt = (g1.FindControl("DDL_Unt") as DropDownList).SelectedValue;
                        string DDL_Itmtyp = (g1.FindControl("DDL_Itmtyp") as DropDownList).SelectedValue;                        
                        string TBpksiz = (g1.FindControl("TBpksiz") as TextBox).Text;
                        string TBRtlPrc = (g1.FindControl("TBRtlPrc") as TextBox).Text;

                        subheadfive.subheadcategoryfiveID = HFSubHeadCatFivID.Value;
                        subheadfive.subheadcategoryfiveName = string.IsNullOrEmpty(TBItmNam) ? null : TBItmNam;
                        subheadfive.subheadcategoryfiveGeneratedID = string.IsNullOrEmpty(SubHeadCatFiv.Value) ? null : SubHeadCatFiv.Value;
                        subheadfive.HeadGeneratedID = string.IsNullOrEmpty("MB001") ? null : "MB001";
                        subheadfive.SubHeadGeneratedID = string.IsNullOrEmpty("MB0001") ? null : "MB0001";
                        subheadfive.SubHeadCategoriesGeneratedID = string.IsNullOrEmpty("MB00001") ? null : "MB00001";
                        subheadfive.subheadcategoryfourGeneratedID = string.IsNullOrEmpty("MB000002") ? null : "MB000002";
                        subheadfive.CreatedAt = DateTime.Now;
                        subheadfive.CreatedBy = Session["user"].ToString();
                        subheadfive.SubFiveKey = string.IsNullOrEmpty(SubHeadCatFiv.Value) ? null : SubHeadCatFiv.Value;


                        subheadcategoryfiveManager subheadcatfive = new subheadcategoryfiveManager(subheadfive);
                        subheadcatfive.Save();

                        Products products = new Products();
                        products.ProductID = HFProID.Value;
                        products.ProductTypeID = DDL_Itmtyp != "0" ? DDL_Itmtyp : null;
                        products.ProductName = string.IsNullOrEmpty(TBItmNam) ? null : TBItmNam;
                        products.PckSize = string.IsNullOrEmpty(TBpksiz) ? null : TBpksiz;
                        products.Cost = string.IsNullOrEmpty(TBRtlPrc) ? null : TBRtlPrc;
                        products.ProductDiscriptions = string.IsNullOrEmpty(TBItmNam) ? null : TBItmNam;
                        products.Supplier_CUstomer = "";//string.IsNullOrEmpty(TBSupCus.Value) ? null : TBSupCus.Value;
                        products.Unit = string.IsNullOrEmpty(DDL_Unt) ? null : DDL_Unt;
                        products.ProductType = DDL_Itmtyp != "0" ? DDL_Itmtyp : null;
                        products.CreatedBy = Session["user"].ToString();
                        products.CreatedAt = DateTime.Now;
                        products.Pro_Code = string.IsNullOrEmpty(SubHeadCatFiv.Value) ? null : SubHeadCatFiv.Value;

                        ProductsManager promanag = new ProductsManager(products);
                        promanag.Save();

                    }
   
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;

            }
            return j;
        }

        public void FillGrid()
        {
            try
            {
                DataTable dt_ = new DataTable();
                dt_ = tbl_mjvManager.GetJVList();

                //GVScrhJV.DataSource = dt_;
                //GVScrhJV.DataBind();

                ViewState["Pro"] = dt_;
            }
            catch (Exception ex)
            {
                //throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
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

            //dt.Columns.Add(new DataColumn("ITEMCODE", typeof(string)));
            dt.Columns.Add(new DataColumn("ITEMNAME", typeof(string)));
            dt.Columns.Add(new DataColumn("ITEM TYPE", typeof(string)));
            dt.Columns.Add(new DataColumn("UNITS", typeof(string)));
            dt.Columns.Add(new DataColumn("PURCHASEPRICE", typeof(string)));
            dt.Columns.Add(new DataColumn("SALEPRICE", typeof(string)));
            //dt.Columns.Add(new DataColumn("RETAILPRICE", typeof(string)));
            dt.Columns.Add(new DataColumn("PACKINGSIZE", typeof(string)));
            dt.Columns.Add(new DataColumn("PACKINCARTONS", typeof(string)));

            
            dr = dt.NewRow();

            //dr["ITEMCODE"] = string.Empty;
            dr["ITEMNAME"] = string.Empty;
            dr["ITEM TYPE"] = string.Empty;
            dr["UNITS"] = string.Empty;
            dr["PURCHASEPRICE"] = string.Empty;
            dr["SALEPRICE"] = string.Empty;
            //dr["RETAILPRICE"] = string.Empty;
            dr["PACKINGSIZE"] = string.Empty;
            dr["PACKINCARTONS"] = string.Empty;

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
                        
                        TextBox TBItmNam = (TextBox)GVPro.Rows[rowIndex].Cells[0].FindControl("TBItmNam");
                        DropDownList DDL_Itmtyp = (DropDownList)GVPro.Rows[rowIndex].Cells[1].FindControl("DDL_Itmtyp");
                        DropDownList DDL_Unt = (DropDownList)GVPro.Rows[rowIndex].Cells[2].FindControl("DDL_Unt");
                        TextBox TBPurPrc = (TextBox)GVPro.Rows[rowIndex].Cells[3].FindControl("TBPurPrc");
                        TextBox TBSalPrc = (TextBox)GVPro.Rows[rowIndex].Cells[3].FindControl("TBSalPrc");
                        TextBox TBRtlPrc = (TextBox)GVPro.Rows[rowIndex].Cells[3].FindControl("TBRtlPrc");
                        TextBox TBpksiz = (TextBox)GVPro.Rows[rowIndex].Cells[4].FindControl("TBpksiz");
                        TextBox PACKINCARTONS = (TextBox)GVPro.Rows[rowIndex].Cells[5].FindControl("TBcartsiz");
                        
                        drRow = dt.NewRow();

                        //dt.Rows[i - 1]["ITEMCODE"] = DDL_Unt.Text;
                        dt.Rows[i - 1]["ITEMNAME"] = TBItmNam.Text;
                        dt.Rows[i - 1]["ITEM TYPE"] = DDL_Itmtyp.Text;
                        dt.Rows[i - 1]["UNITS"] = DDL_Unt.Text;
                        dt.Rows[i - 1]["PURCHASEPRICE"] = TBPurPrc.Text;
                        dt.Rows[i - 1]["SALEPRICE"] = TBSalPrc.Text;
                        //dt.Rows[i - 1]["RETAILPRICE"] = TBRtlPrc.Text;
                        dt.Rows[i - 1]["PACKINGSIZE"] = TBpksiz.Text;
                        dt.Rows[i - 1]["PACKINCARTONS"] = PACKINCARTONS.Text;
                        rowIndex++;

                        DDL_Unt.Focus();
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
        }

        private void SetPreRow()
        {
            try
            {
                BindDDL();
                int rowIndex = 0;
                if (ViewState["dt_adItm"] != null)
                {
                    DataTable dt = (DataTable)ViewState["dt_adItm"];
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            TextBox TBItmNam = (TextBox)GVPro.Rows[rowIndex].Cells[0].FindControl("TBItmNam");
                            DropDownList DDL_Itmtyp = (DropDownList)GVPro.Rows[rowIndex].Cells[1].FindControl("DDL_Itmtyp");
                            DropDownList DDL_Unt = (DropDownList)GVPro.Rows[rowIndex].Cells[2].FindControl("DDL_Unt");
                            TextBox TBPurPrc = (TextBox)GVPro.Rows[rowIndex].Cells[3].FindControl("TBPurPrc");
                            TextBox TBSalPrc = (TextBox)GVPro.Rows[rowIndex].Cells[3].FindControl("TBSalPrc");
                            TextBox TBRtlPrc = (TextBox)GVPro.Rows[rowIndex].Cells[3].FindControl("TBRtlPrc");
                            TextBox TBpksiz = (TextBox)GVPro.Rows[rowIndex].Cells[4].FindControl("TBpksiz");
                            TextBox TBcartsiz = (TextBox)GVPro.Rows[rowIndex].Cells[5].FindControl("TBcartsiz");

                            //TBItmCd.Text = dt.Rows[i]["ITEMCODE"].ToString();
                            TBItmNam.Text = dt.Rows[i]["ITEMNAME"].ToString();
                            DDL_Itmtyp.SelectedValue = dt.Rows[i]["ITEM TYPE"].ToString();
                            DDL_Unt.SelectedValue = dt.Rows[i]["UNITS"].ToString();
                            TBPurPrc.Text = dt.Rows[i]["PURCHASEPRICE"].ToString();
                            TBSalPrc.Text = dt.Rows[i]["SALEPRICE"].ToString();
                            //TBRtlPrc.Text = dt.Rows[i]["RETAILPRICE"].ToString();
                            TBpksiz.Text = dt.Rows[i]["PACKINGSIZE"].ToString();
                            //ChkCls.Checked = Convert.ToBoolean(dt.Rows[i]["CLOSED"].ToString());
                            //PACKINCARTONS
                            TBcartsiz.Text = dt.Rows[i]["PACKINCARTONS"].ToString();
                            rowIndex++;

                            DDL_Unt.Focus();
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
                    //ptnSno();
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
                string sqlquery = " delete from  Products where ProductID = '" + HFProID.Value + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                DBConnection db = new DBConnection();
                db.CRUDRecords(sqlquery);       
        

            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;

            }
            return i;
        }

        protected void linkmodaldelete_Click(object sender, EventArgs e)
        {

            con.Open();

            SqlCommand command = con.CreateCommand();
            SqlTransaction transaction;

            // Start a local transaction.
            transaction = con.BeginTransaction("SampleTransaction");

             //Must assign both transaction object and connection 
             //to Command object for a pending local transaction
            command.Connection = con;
            command.Transaction = transaction;

            try
            {
                int a, b;
                a = delpro();                

                if (a == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Product Has Been Delete!";

                    SubHeadCatFiv.Value = "";
                    HFSubHeadCatFivID.Value = "";
                    HFProID.Value = "";
                    Response.Redirect("frm_Products.aspx");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Some thing is wrong Call the Administrator!!";
                }

                if (con != null && con.State == ConnectionState.Closed)
                {
                    con.Open();
                }


                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }


                 //Attempt to commit the transaction.
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
            }
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            int o = 0;
            o =  delpro();

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

                string TBItmNam = (g1.FindControl("TBItmNam") as TextBox).Text;
                string DDL_Unt = (g1.FindControl("DDL_Unt") as DropDownList).SelectedValue;
                string DDL_Itmtyp = (g1.FindControl("DDL_Itmtyp") as DropDownList).SelectedValue;

                string TBpksiz = (g1.FindControl("TBpksiz") as TextBox).Text;

                string TBPurPrc = (g1.FindControl("TBPurPrc") as TextBox).Text;

                string TBSalPrc = (g1.FindControl("TBSalPrc") as TextBox).Text;

                string TBcartsiz = (g1.FindControl("TBcartsiz") as TextBox).Text;

                //string TBRtlPrc = (g1.FindControl("TBRtlPrc") as TextBox).Text;


                query = "INSERT INTO [dbo].[Products] ([ProductTypeID] ,[ProductName] ,[PckSize] ,[Cost] " +
                " ,[ProductDiscriptions] ,[Supplier_Customer] ,[Unit] ,[PurchasePrice], [SalePrice], [stk_carton],  [ProductType] ,[CreatedBy] ,[CreatedAt] ,[Pro_Code] " +
                " ,[IsActive], CompanyId, BranchId) VALUES ('" + DDL_Itmtyp + "' , '" + TBItmNam + "' , '" + TBpksiz + "' , ''" +
                " , '" + TBItmNam + "' , '' , '" + DDL_Unt + "' , '" + TBPurPrc + "' , '" + TBSalPrc + "' , '" + TBcartsiz + "','" + DDL_Itmtyp + "' , '" + Session["user"].ToString() +
                "' , '" + DateTime.Now + "' , '', '1','" + Session["CompanyID"] + "','" + Session["BranchID"] + "' )";


                con.Open();


                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    //cmd.Parameters.AddWithValue("@ProductTypeID", textbox2.text);
                    //cmd.Parameters.AddWithValue("@ProductName", textbox3.text);
                    //cmd.Parameters.AddWithValue("@PckSize", textbox2.text);
                    //cmd.Parameters.AddWithValue("@Cost", textbox3.text);
                    //cmd.Parameters.AddWithValue("@ProductDiscriptions", textbox2.text);
                    //cmd.Parameters.AddWithValue("@Supplier_Customer", textbox3.text);
                    //cmd.Parameters.AddWithValue("@Unit", textbox2.text);
                    //cmd.Parameters.AddWithValue("@ProductType", textbox3.text);
                    //cmd.Parameters.AddWithValue("@CreatedBy", textbox2.text);
                    //cmd.Parameters.AddWithValue("@CreatedAt", textbox2.text);
                    //cmd.Parameters.AddWithValue("@Pro_Code", textbox2.text);
                    //cmd.Parameters.AddWithValue("@IsActive", textbox2.text);


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

                string TBItmNam = (g1.FindControl("TBItmNam") as TextBox).Text;
                string DDL_Unt = (g1.FindControl("DDL_Unt") as DropDownList).SelectedValue;
                string DDL_Itmtyp = (g1.FindControl("DDL_Itmtyp") as DropDownList).SelectedValue;
                string TBpksiz = (g1.FindControl("TBpksiz") as TextBox).Text;
                string TBPurPrc = (g1.FindControl("TBPurPrc") as TextBox).Text;                
                string TBSalPrc = (g1.FindControl("TBSalPrc") as TextBox).Text;
                string TBcartsiz = (g1.FindControl("TBcartsiz") as TextBox).Text;

                //string TBRtlPrc = (g1.FindControl("TBRtlPrc") as TextBox).Text;


                query = "UPDATE [dbo].[Products] SET [ProductTypeID] ='" + DDL_Itmtyp + "' ,[ProductName] = '" + TBItmNam + "' ,[PckSize] = '" + TBpksiz + "' ,[Cost] = '0.00' ,[ProductDiscriptions] = '" + TBItmNam + "' ,[Supplier_Customer] = '' ,[Unit] = " + DDL_Unt + ", [PurchasePrice] ='" + TBPurPrc + "', [SalePrice] = '" + TBSalPrc + "' ,[stk_carton]= '" + TBcartsiz + "' ,[ProductType] = '" + DDL_Itmtyp + "' ,[CreatedBy] = '" + Session["user"].ToString() + "' ,[CreatedAt] = '" + DateTime.Now + "' ,[Pro_Code] = '' ,[IsActive] = 1  WHERE ProductID = " + DDL_Itm.SelectedValue.Trim() + "";


                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    //cmd.Parameters.AddWithValue("@ProductTypeID", textbox2.text);
                    //cmd.Parameters.AddWithValue("@ProductName", textbox3.text);
                    //cmd.Parameters.AddWithValue("@PckSize", textbox2.text);
                    //cmd.Parameters.AddWithValue("@Cost", textbox3.text);
                    //cmd.Parameters.AddWithValue("@ProductDiscriptions", textbox2.text);
                    //cmd.Parameters.AddWithValue("@Supplier_Customer", textbox3.text);
                    //cmd.Parameters.AddWithValue("@Unit", textbox2.text);
                    //cmd.Parameters.AddWithValue("@ProductType", textbox3.text);
                    //cmd.Parameters.AddWithValue("@CreatedBy", textbox2.text);
                    //cmd.Parameters.AddWithValue("@CreatedAt", textbox2.text);
                    //cmd.Parameters.AddWithValue("@Pro_Code", textbox2.text);
                    //cmd.Parameters.AddWithValue("@IsActive", textbox2.text);


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
                if (HFProID.Value != "")
                {
                    int b;
                    b = Update();

                    if (b == 1)
                    {
                        Response.Redirect("frm_Products.aspx");
                    }
                    else
                    {
                        lbl_err.Text = "Not Updated!";
                    }
                }
                else
                {
                    int a;

                    a = Save();

                    if (a == 1)
                    {
                        Response.Redirect("frm_Products.aspx");
                    }
                    else
                    {
                        lbl_err.Text = "Not Saved!";
                    }                    
                }

                SetInitRow();
                BindDDL();
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
                cmdHFiv.CommandText = " select * from Products  where ProductID = '" + DDL_Itm.SelectedValue.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                cmdHFiv.Connection = con;
                con.Open();

                DataTable dtHFiv = new DataTable();
                SqlDataAdapter adpItm = new SqlDataAdapter(cmdHFiv);
                adpItm.Fill(dtHFiv);
                if (dtHFiv.Rows.Count > 0)
                {
                    //HFSubHeadCatFivID.Value = dtHFiv.Rows[0]["subheadcategoryfiveID"].ToString();

                }
                
                con.Close();
            }


            //From Product Table;

            //using (SqlCommand cmdPro = new SqlCommand())
            //{
            //    con.Close();
            //    cmdPro.CommandText = " select ProductID from Products where Pro_Code = '" + DDL_Itm.SelectedValue.Trim() + "' ";

            //    cmdPro.Connection = con;
            //    con.Open();

            //    DataTable dtPro = new DataTable();
            //    SqlDataAdapter adpItm = new SqlDataAdapter(cmdPro);
            //    adpItm.Fill(dtPro);
             dt_ = DBConnection.GetQueryData("select Pro_Code,ProductID, ProductName,ProductTypeID,Unit,Cost as [RETAILPRICE], PckSize as [PACKINGSIZE] from Products where  ProductID = '" + DDL_Itm.SelectedValue.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

             if (dt_.Rows.Count > 0)
             {
                 if (dt_.Rows.Count > 0)
                 {
                     HFProID.Value = dt_.Rows[0]["ProductID"].ToString();
                 }
             }
            //    con.Close();
            //}

            //From Product Details;

            //dt_ = DBConnection.GetQueryData("select ProductName,ProductTypeID,Unit,Cost as [RETAILPRICE], PckSize as [PACKINGSIZE] from Products where ProductID = '" + DDL_Itm.SelectedValue.Trim() + "'");
             dt_ = DBConnection.GetQueryData("select Products.Pro_Code,stk_carton,ProductID, ProductName,ProductTypeID,Unit,PurchasePrice,SalePrice,Cost as [RETAILPRICE], PckSize as [PACKINGSIZE] from Products where  Products.ProductID = '" + DDL_Itm.SelectedValue.Trim() + "' and Products.CompanyId = '" + Session["CompanyID"] + "' and Products.BranchId= '" + Session["BranchID"] + "'");

            if (dt_.Rows.Count > 0)
            {
                for (int i = 0; i < GVPro.Rows.Count; i++)
                {
                    TextBox TBItmNam = (TextBox)GVPro.Rows[i].Cells[0].FindControl("TBItmNam");
                    DropDownList DDL_Itmtyp = (DropDownList)GVPro.Rows[i].Cells[1].FindControl("DDL_Itmtyp");
                    DropDownList DDL_Unt = (DropDownList)GVPro.Rows[i].Cells[2].FindControl("DDL_Unt");
                    TextBox TBPurPrc = (TextBox)GVPro.Rows[i].Cells[3].FindControl("TBPurPrc");
                    TextBox TBSalPrc = (TextBox)GVPro.Rows[i].Cells[3].FindControl("TBSalPrc");

                    //TextBox TBRtlPrc = (TextBox)GVPro.Rows[i].Cells[3].FindControl("TBRtlPrc");
                    TextBox TBpksiz = (TextBox)GVPro.Rows[i].Cells[4].FindControl("TBpksiz");
                    TextBox TBcartsiz = (TextBox)GVPro.Rows[i].Cells[5].FindControl("TBcartsiz");


                    TBItmNam.Text = dt_.Rows[0]["ProductName"].ToString();
                    DDL_Unt.SelectedValue = dt_.Rows[0]["Unit"].ToString();
                    DDL_Itmtyp.SelectedValue = dt_.Rows[0]["ProductTypeID"].ToString();
                    TBPurPrc.Text = dt_.Rows[0]["PURCHASEPRICE"].ToString();
                    TBSalPrc.Text = dt_.Rows[0]["SALEPRICE"].ToString();
                    //TBRtlPrc.Text = dt_.Rows[0]["RETAILPRICE"].ToString();
                    TBpksiz.Text = dt_.Rows[0]["PACKINGSIZE"].ToString();
                    HFProID.Value = dt_.Rows[0]["ProductID"].ToString();
                    TBcartsiz.Text = dt_.Rows[0]["stk_carton"].ToString();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = "Not Record Found!";
            }
            //using (SqlCommand cmdPro = new SqlCommand())
            //{
            //    con.Close();
            //    cmdPro.CommandText = " select ProductName,ProductTypeID,Unit,Cost as [RETAILPRICE], PckSize as [PACKINGSIZE] from Products where ProductID = '" + DDL_Itm.SelectedValue.Trim() + "' ";

            //    cmdPro.Connection = con;
            //    con.Open();

            //    //DataTable dtPro = new DataTable();
            //    SqlDataAdapter adpItm = new SqlDataAdapter(cmdPro);
            //    adpItm.Fill(dt_);
            //    if (dt_.Rows.Count > 0)
            //    {
            //        //BindDDL();

            //        for (int i = 0; i < GVPro.Rows.Count; i++)
            //        {
            //            TextBox TBItmNam = (TextBox)GVPro.Rows[i].Cells[0].FindControl("TBItmNam");
            //            DropDownList DDL_Itmtyp = (DropDownList)GVPro.Rows[i].Cells[1].FindControl("DDL_Itmtyp");
            //            DropDownList DDL_Unt = (DropDownList)GVPro.Rows[i].Cells[2].FindControl("DDL_Unt");
            //            TextBox TBRtlPrc = (TextBox)GVPro.Rows[i].Cells[3].FindControl("TBRtlPrc");
            //            TextBox TBpksiz = (TextBox)GVPro.Rows[i].Cells[4].FindControl("TBpksiz");

            //            TBItmNam.Text = dt_.Rows[0]["ProductName"].ToString();
            //            DDL_Unt.SelectedValue = dt_.Rows[0]["Unit"].ToString();
            //            DDL_Itmtyp.SelectedValue = dt_.Rows[0]["ProductTypeID"].ToString();
            //            TBRtlPrc.Text = dt_.Rows[0]["RETAILPRICE"].ToString();
            //            TBpksiz.Text = dt_.Rows[0]["PACKINGSIZE"].ToString();
            //        }
            //    }

            //    con.Close();
            //}
            btnDel.Enabled = true;

            for (int i = 0; i < GVPro.Rows.Count; i++)
            {
                LinkButton linkbtnadd = (LinkButton)GVPro.Rows[i].Cells[0].FindControl("linkbtnadd");
                linkbtnadd.Enabled = false;    
            }

            //this.GVPro.AutoGenerateDeleteButton = false;
        }

        protected void GVPro_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //Item Type

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(ProductTypeID AS VARCHAR(200)) + ']-' + ProductTypeName ) as [ProductTypeName], ProductTypeID from tbl_producttype where IsActive = 1 and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                for (int i = 0; i < GVPro.Rows.Count; i++)
                {
                    DropDownList DDL_Itmtyp = (DropDownList)GVPro.Rows[i].Cells[0].FindControl("DDL_Itmtyp");
                    DDL_Itmtyp.DataSource = dt_;
                    DDL_Itmtyp.DataTextField = "ProductTypeName";
                    DDL_Itmtyp.DataValueField = "ProductTypeID";
                    DDL_Itmtyp.DataBind();
                    DDL_Itmtyp.Items.Insert(0, new ListItem("--Select Items Types--", "0"));
                }

                //Item Name

                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(ProductID AS VARCHAR(200)) + ']-' + ProductName ) as [ProductName], ProductID from Products where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                DDL_Itm.DataSource = dt_;
                DDL_Itm.DataTextField = "ProductName";
                DDL_Itm.DataValueField = "ProductID";
                DDL_Itm.DataBind();
                DDL_Itm.Items.Insert(0, new ListItem("--Select Items --", "0"));

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
    }
}