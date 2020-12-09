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
    public partial class Customers : System.Web.UI.Page
    {
        DBConnection connection;
        DataTable dt_;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        string query, areaid, subheadcatid;
        SqlDataAdapter adapter;
        decimal avapre;
        decimal ttlcre;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                FillGrid();
                BindDll();
                AutoID();
                //GridView1.Visible = false;
                lblcity.Visible = false;
                btnadd.Focus();
                TBPrevBal.Text = "0";
            }

        }

        public void FillGrid()
        {
            try
            {
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData(" select * from Customers_  where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");
                
                GVCutomers.DataSource = dt_;
                GVCutomers.DataBind();
                ViewState["Customer"] = dt_;

            }
            catch (Exception ex)
            {
                //throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }


        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetCity(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select City_  from City where City_ like '" + prefixText + "%'";
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
        public static List<string> GetArea(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select area_  from tbl_area where area_ like '" + prefixText + "%'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }

       
        //private int saveHeadFiv()
        //{
        //    int j = 1;
        //    try
        //    {                    
        //        subheadcategoryfive subheadfive = new subheadcategoryfive();

        //        if (HFSubHeadCatFivID.Value == "")
        //        {
        //            Common com = new Common();
        //            com.ShowAccountCategoryFiveID(SubHeadCatFiv);
        //        }
              
        //        subheadfive.subheadcategoryfiveID = HFSubHeadCatFivID.Value;
        //        subheadfive.subheadcategoryfiveName = string.IsNullOrEmpty(TBCustomersName.Value) ? null : TBCustomersName.Value;
        //        subheadfive.subheadcategoryfiveGeneratedID = string.IsNullOrEmpty(SubHeadCatFiv.Value) ? null : SubHeadCatFiv.Value;
        //        subheadfive.HeadGeneratedID = string.IsNullOrEmpty("MB001") ? null : "MB001";
        //        subheadfive.SubHeadGeneratedID = string.IsNullOrEmpty("MB0001") ? null : "MB0001";
        //        subheadfive.SubHeadCategoriesGeneratedID = string.IsNullOrEmpty("MB00002") ? null : "MB00002";
        //        subheadfive.subheadcategoryfourGeneratedID = string.IsNullOrEmpty("MB000003") ? null : "MB000003";
        //        subheadfive.CreatedAt = DateTime.Now;
        //        subheadfive.CreatedBy = Session["user"].ToString();
        //        subheadfive.SubFiveKey = string.IsNullOrEmpty(SubHeadCatFiv.Value) ? null : SubHeadCatFiv.Value;


        //        subheadcategoryfiveManager subheadcatfive = new subheadcategoryfiveManager(subheadfive);
        //        subheadcatfive.Save();


        //        Customers_ customers = new Customers_();

        //        customers.CustomerID = HFCustomerID.Value;
        //        customers.CustomerName = string.IsNullOrEmpty(TBCustomersName.Value) ? null : TBCustomersName.Value;
        //        customers.GST = string.IsNullOrEmpty(TBGST.Value) ? null : TBGST.Value;
        //        customers.category = DDLCategoryID.SelectedItem.Text.ToString() != "0" ? DDLCategoryID.SelectedItem.Text.ToString() : null;
        //        customers.NTN = string.IsNullOrEmpty(TBNTN.Value) ? null : TBNTN.Value;
        //        customers.customertype_ = DDLCustomerType.SelectedItem.Text.ToString() != "0" ? DDLCustomerType.SelectedItem.Text.ToString() : null;
        //        customers.Area = string.IsNullOrEmpty(TBArea.SelectedValue) ? null : TBArea.SelectedValue;
        //        customers.RefNum = string.IsNullOrEmpty(TBRefNum.Value) ? null : TBRefNum.Value;
        //        customers.District = string.IsNullOrEmpty(TBDistrict.Value) ? null : TBDistrict.Value;
        //        customers.PhoneNo = string.IsNullOrEmpty(TBPhone.Value) ? null : TBPhone.Value;
        //        customers.Email = string.IsNullOrEmpty(TBEmail.Value) ? null : TBEmail.Value;
        //        customers.CellNo1 = string.IsNullOrEmpty(TBCellNo.Value) ? null : TBCellNo.Value;
        //        customers.PostalCode = string.IsNullOrEmpty(TBPostalCode.Value) ? null : TBPostalCode.Value;
        //        customers.CellNo2 = string.IsNullOrEmpty(TBCellNo2.Value) ? null : TBCellNo2.Value;
        //        customers.PostalOfficeContact = string.IsNullOrEmpty(TBPostalOfficeContact.Value) ? null : TBPostalOfficeContact.Value;
        //        customers.CellNo3 = string.IsNullOrEmpty(TBCellNo3.Value) ? null : TBCellNo3.Value;
        //        customers.NIC = string.IsNullOrEmpty(TBNIC.Value) ? null : TBNIC.Value;
        //        customers.CellNo4 = string.IsNullOrEmpty(TBCellNo4.Value) ? null : TBCellNo4.Value;
        //        customers.city_ = TBCity.SelectedItem.Text.ToString() != "0" ? TBCity.SelectedItem.Text.ToString() : null;
        //        customers.Address = string.IsNullOrEmpty(TBAddress.Value) ? null : TBAddress.Value;
        //        customers.CreatedBy = Session["user"].ToString();
        //        customers.CreatedDate = DateTime.Now;
        //        customers.IsActive = "true";
        //        customers.Cus_Code = SubHeadCatFiv.Value.Trim();

        //        CustomerManager custmanag = new CustomerManager(customers);
        //        custmanag.Save();
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
        //        lblalert.Text = ex.Message;

        //    }
        //    return j;
        //}

        public void ShowAccountCategoryID()
        {
            try
            {

                query = "select top 1 SubHeadCategoriesID from SubHeadCategories where SubHeadGeneratedID = '0011' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "' order by SubHeadCategoriesID desc";
               SqlCommand cmd = new SqlCommand(query, con);
               con.Open();

               DataTable dt_ = new DataTable();

               SqlDataAdapter adp = new SqlDataAdapter(cmd);
               adp.Fill(dt_);
               if (dt_.Rows.Count <= 0)
               {
                   SubHeadCat.Value = "1";
               }
               else
                {
                    //SqlDataReader reader = cmd.ExecuteReader();


                    //while (reader.Read())
                    {
                        if (SubHeadCat.Value == "")
                        {
                            //int v = Convert.ToInt32(reader["SubHeadCategoriesID"].ToString());
                            int v = Convert.ToInt32(dt_.Rows[0]["SubHeadCategoriesID"].ToString());
                            int b = v + 1;

                            SubHeadCat.Value = b.ToString();

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

        public void AutoID()
        {
            ShowAccountCategoryID();
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
                #region Customers

                if (TBArea.Text != "")
                {
                    query = "select * from tbl_area  where area_= '" + TBArea.Text.Trim() + "'";

                    DataTable dtarea = new DataTable();
                    dtarea = DBConnection.GetQueryData(query);

                    if (dtarea.Rows.Count > 0)
                    {
                        areaid = dtarea.Rows[0]["areaid"].ToString();
                    }
                }
                else
                {
                    areaid = "0";
                }

                command.CommandText= " INSERT INTO [Customers_] ([CustomerName] ,[GST] ,[category] ,[NTN] , " +
                    "[customertype_],[areaid] " +
                    " ,[Area] ,[saleper] ,[District] ,[PhoneNo] ,[Email],[CellNo1],[PostalCode],[CellNo2] " +
                    " ,[PostalOfficeContact],[CellNo3] ,[NIC] ,[CellNo4],[city_],[Address] ,[CreatedBy] " +
                    " ,[CreatedDate] ,[IsActive] ,[Cus_Code] ,[CompanyId] ,[BranchId])  VALUES " +
                    " ('" + TBCustomersName.Value + "' ,'GST','', '" + TBNTN.Value + "'" +
                    " ,'' ,'" + areaid.Trim() + "','" + TBArea.Text + "','0.00' ,'','" +
                    TBPhone.Value + "','" + TBEmail.Value + "','','','','',''" +
                    " ,'" + TBNIC.Value + "','','" + TBCity.Text.ToString() + "'" +
                    " ,'" + TBAddress.Value + "','" + Session["user"].ToString() + "','" + DateTime.Now + "'" +
                    " ,'true' ,'','" + Session["CompanyID"] + "','" + Session["BranchID"] + "' )";

                command.ExecuteNonQuery();

                #endregion


                #region Accounts


                command.CommandText = " INSERT INTO [SubHeadCategories] ([ven_id] ,[SubHeadCategoriesName] ,[SubHeadCategoriesGeneratedID] ,[HeadGeneratedID] , " +
                   "[SubHeadGeneratedID],[CreatedAt] " +
                   " ,[CreatedBy] ,[SubCategoriesKey],CompanyId,BranchId)  VALUES " +
                   " ('1', '" + TBCustomersName.Value + "' ,'" + "0011" +  SubHeadCat.Value + "','001', '0011','" + DateTime.Now + "'" +
                   ",'" + Session["user"] + "','" + "0011" + SubHeadCat.Value + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "' )";

                command.ExecuteNonQuery();


                //SubHeadCategories subheadcat = new SubHeadCategories();

                //subheadcat.SubHeadCategoriesID = HFAccountCategoryName.Value;
                //subheadcat.ven_id = "1";//string.IsNullOrEmpty(DDLAccCat.SelectedValue) ? null : DDLAccCat.SelectedValue;
                //subheadcat.SubHeadCategoriesName = "abc"; //string.IsNullOrEmpty(DDLAccCat.SelectedItem.Text) ? null : DDLAccCat.SelectedItem.Text;
                //subheadcat.SubHeadCategoriesName = string.IsNullOrEmpty(TBCustomersName.Value) ? null : TBCustomersName.Value;
                //subheadcat.SubHeadCategoriesGeneratedID = string.IsNullOrEmpty("0011" + SubHeadCat.Value) ? null : "0011" + SubHeadCat.Value;
                //subheadcat.HeadGeneratedID = string.IsNullOrEmpty("001") ? null : "001";
                //subheadcat.SubHeadGeneratedID = string.IsNullOrEmpty("0011") ? null : "0011";
                //subheadcat.CreatedAt = DateTime.Now;
                //subheadcat.CreatedBy = Session["user"].ToString();
                //subheadcat.SubCategoriesKey = string.IsNullOrEmpty(SubHeadCat.Value) ? null : SubHeadCat.Value;

                //SubHeadCategoriesManager subheadcatmanag = new SubHeadCategoriesManager(subheadcat);
                //subheadcatmanag.Save();
                    

                #endregion


                #region Credit Sheets

                string accno = "";

                command.CommandText = " select SubHeadCategoriesGeneratedID,SubHeadCategoriesName from SubHeadCategories where SubHeadCategoriesName='" + TBCustomersName.Value.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                SqlDataAdapter adpchkcust = new SqlDataAdapter(command);

                DataTable dtchkcust = new DataTable();
                adpchkcust.Fill(dtchkcust);

                if (dtchkcust.Rows.Count > 0)
                {
                    accno = dtchkcust.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                }

                command.CommandText = "select CredAmt from tbl_Salcredit where CustomerID='" + accno.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                SqlDataAdapter stksalcre = new SqlDataAdapter(command);

                DataTable dtsalcre = new DataTable();
                stksalcre.Fill(dtsalcre);

                if (dtsalcre.Rows.Count > 0)
                {
                    //double recv = Convert.ToDouble(lblOutstan) - Convert.ToDouble(TBRecy);

                    //avapre = Convert.ToDecimal(dtsalcre.Rows[0]["CredAmt"]);

                    ttlcre = Convert.ToDecimal(TBPrevBal.Text.Trim());

                    command.CommandText = " Update tbl_Salcredit set CredAmt = '" + ttlcre + "' where CustomerID='" + accno.Trim() + "'  and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                    command.ExecuteNonQuery();
                }
                else
                {
                    //command.CommandText = " insert into tbl_Salcredit (CustomerID,CredAmt) values('" + DDL_CustAcc.SelectedValue.Trim() + "','" + ttlcre + "')";
                    command.CommandText = " insert into tbl_Salcredit (CustomerID,CredAmt,CompanyId,BranchId) values('" + accno.Trim() + "','" + TBPrevBal.Text.Trim() + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                    command.ExecuteNonQuery();
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

            }

            return res;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {

                /*                bool phn = false;
                                bool email = false;
                                Regex regexobjphn = new Regex(@"^([0-9]*|\d*\.\d{1}?\d*)$");
                                Regex regexobjemail = new Regex(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$");


                                if (TBCustomersName.Value == "")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                                    lblalert.Text = "Please Write Customer Name!!";
                                }

                                else if (TBPhone.Value == "")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                                    lblalert.Text = "Please Write Phone Num!!";
                                }

                                else if (TBEmail.Value == "")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                                    lblalert.Text = "Please Write Email Add!!";
                                }
                                else if (TBCellNo.Value == "")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                                    lblalert.Text = "Please Write Cell No!!";
                                }
                                else if (TBNIC.Value == "")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                                    lblalert.Text = "Please Write NIC!!";
                                }
                                else if (TBAddress.Value == "")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                                    lblalert.Text = "Please Write Customer Address!!";
                                }
                                else if (!regexobjphn.IsMatch(TBPhone.Value))
                                {
                                    phn = false;
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                                    lblalert.Text = "Please Write Correct Phone Number!!";
                                }
                                else if (!regexobjemail.IsMatch(TBEmail.Value))
                                {
                                    email = false;
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                                    lblalert.Text = "Please Write Correct Email Address!!";
                                }
                                else*/
                if (TBCustomersName.Value == "")
                {
                    v_name.Text = "Enter Costomer Name";
                    TBCustomersName.Focus();
                }
                else if (TBPrevBal.Text == "")
                {
                    v_prebal.Text = "Please Enter Previous Balance";
                }
                else
                {
                    v_name.Text = "";
                    v_prebal.Text = "";

                    if (HFCustomerID.Value == "")
                    {
                        int a;
                        a = Save();

                        if (a == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                            lblalert.Text = "Customers Has Been Saved!";
                            Clear();
                            FillGrid();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                            lblalert.Text = "Some thing is wrong Call the Administrator!!";
                        }
                    }
                    else
                    {
                        int b;
                        b = Update();

                        if (b == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                            lblalert.Text = "Customers Has Been Update!";
                            Clear();
                            FillGrid();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                            lblalert.Text = "Some thing is wrong Call the Administrator!!";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        private int Update()
        {
            int h;
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
                if (TBArea.Text != "")
                {
                    query = "select * from tbl_area  where area_= '" + TBArea.Text.Trim() + "'";

                    DataTable dtselectare = new DataTable();
                    dtselectare = DBConnection.GetQueryData(query);

                    if (dtselectare.Rows.Count > 0)
                    {
                        areaid = dtselectare.Rows[0]["areaid"].ToString();
                    }
                }
                else
                {
                    areaid = "0";
                }

                command.CommandText = " Update Customers_ set CustomerName='" + TBCustomersName.Value + "', GST=''" +
                    " , category = '', NTN='" + TBNTN.Value + "', customertype_= '',areaid='" + TBArea.Text + "', Area='" + TBArea.Text + "'" +
                    " , saleper ='0.00', District='', PhoneNo = '" + TBPhone.Value + "'" +
                    " , Email='" + TBEmail.Value + "', CellNo1='', PostalCode='', CellNo2='', " +
                    "  PostalOfficeContact='', CellNo3='', NIC='" + TBNIC.Value + "'" +
                    " , CellNo4='', city_='" + TBCity.Text.Trim() + "', Address='" + TBAddress.Value + "', CreatedBy='" + Session["user"].ToString() + "', CreatedDate='" + DateTime.Now + "', IsActive='true', Cus_Code = '', CompanyId='" + Session["CompanyID"] + "'" + " , BranchId= '" + Session["BranchID"] + "' where CustomerID= '" + HFCustomerID.Value.Trim() + "'";
                command.ExecuteNonQuery();

                // For Account

                string accno = "";

                command.CommandText = " select SubHeadCategoriesGeneratedID,SubHeadCategoriesName from SubHeadCategories where SubHeadCategoriesName='" + TBCustomersName.Value.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                SqlDataAdapter adpchkcust = new SqlDataAdapter(command);

                DataTable dtchkcust = new DataTable();
                adpchkcust.Fill(dtchkcust);

                if (dtchkcust.Rows.Count > 0)
                {
                    accno = dtchkcust.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                }

                #region Accounts

                command.CommandText = "Update SubHeadCategories set SubHeadCategoriesName='" + TBCustomersName.Value.Trim() + "' where SubHeadCategoriesGeneratedID='" + accno + "'";
                command.ExecuteNonQuery();
                //SubHeadCategories subheadcat = new SubHeadCategories();

                //subheadcat.SubHeadCategoriesID = HFAccountCategoryName.Value;
                //subheadcat.ven_id = "1";//string.IsNullOrEmpty(DDLAccCat.SelectedValue) ? null : DDLAccCat.SelectedValue;
                //subheadcat.SubHeadCategoriesName = "abc"; //string.IsNullOrEmpty(DDLAccCat.SelectedItem.Text) ? null : DDLAccCat.SelectedItem.Text;
                //subheadcat.SubHeadCategoriesName = string.IsNullOrEmpty(TBCustomersName.Value) ? null : TBCustomersName.Value;
                //subheadcat.SubHeadCategoriesGeneratedID = string.IsNullOrEmpty(accno) ? null : accno;
                //subheadcat.HeadGeneratedID = string.IsNullOrEmpty("001") ? null : "001";
                //subheadcat.SubHeadGeneratedID = string.IsNullOrEmpty("0011") ? null : "0011";
                //subheadcat.CreatedAt = DateTime.Now;
                //subheadcat.CreatedBy = Session["user"].ToString();
                //subheadcat.SubCategoriesKey = string.IsNullOrEmpty(subheadcatid) ? null : subheadcatid;

                //SubHeadCategoriesManager subheadcatmanag = new SubHeadCategoriesManager(subheadcat);
                //subheadcatmanag.Save();

                #endregion

                #region Credit Sheets


                command.CommandText = "select CredAmt from tbl_Salcredit where CustomerID='" + accno.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                SqlDataAdapter stksalcre = new SqlDataAdapter(command);

                DataTable dtsalcre = new DataTable();
                stksalcre.Fill(dtsalcre);

                if (dtsalcre.Rows.Count > 0)
                {
                    //double recv = Convert.ToDouble(lblOutstan) - Convert.ToDouble(TBRecy);

                    //avapre = Convert.ToDecimal(dtsalcre.Rows[0]["CredAmt"]);

                    ttlcre = Convert.ToDecimal(TBPrevBal.Text.Trim());

                    command.CommandText = " Update tbl_Salcredit set CredAmt = '" + ttlcre + "' where CustomerID='" + accno.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                    command.ExecuteNonQuery();
                }
                else
                {
                    //command.CommandText = " insert into tbl_Salcredit (CustomerID,CredAmt) values('" + DDL_CustAcc.SelectedValue.Trim() + "','" + ttlcre + "')";
                    command.CommandText = " insert into tbl_Salcredit (CustomerID,CredAmt,CompanyId,BranchId) values('" + accno.Trim() + "','" + TBPrevBal.Text.Trim() + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                    command.ExecuteNonQuery();
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
                h = 1;
            }
            return h;

        }


        protected void LinkBtnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(2000);
                Response.Clear();
                Response.Buffer = true;
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                string FileName = "CustomersList.xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                GVCutomers.GridLines = GridLines.Both;
                GVCutomers.HeaderStyle.Font.Bold = true;

                GVCutomers.RenderControl(htmltextwrtter);

                Response.Write(strwritter.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void LinkBtnUpload_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    lblMsg.Text = "";

            //    if (FileUploadToServer.HasFile)
            //    {
            //        System.Threading.Thread.Sleep(10000);

            //        // ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", " progressbar();", true);

            //        string FileName = Path.GetFileName(FileUploadToServer.PostedFile.FileName);
            //        string Extension = Path.GetExtension(FileUploadToServer.PostedFile.FileName);

            //        string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

            //        string FilePath = Server.MapPath(FolderPath + FileName);

            //        if (Extension == ".xlsx")
            //        {
            //            lblMsg.Text = "Uploading:";
            //            FileUploadToServer.SaveAs(FilePath);
            //            Import_To_Grid(FilePath, Extension, "1");
            //        }
            //        else
            //        {
            //            lblMsg.Text = "Please select .xlsx or Excel File!!";
            //        }
            //    }
            //    else
            //    {
            //        lblMsg.Text = "Please select some thing to upload!!";
            //    }
            //}
            //catch (Exception ex)
            //{
            //       //throw;
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
            //        lblalert.Text = ex.Message;
            //}
        }

        //public void Import_To_Grid(string FilePath, string Extension, string Step)
        //{
            //try
            //{
            //    string conStr = "";
            //    switch (Extension)
            //    {
            //        case ".xls": //Excel 97-03
            //            conStr = "Provider=Microsoft.ACE.OLEDB.8.0;Data Source=" + FilePath + ";Extended Properties=Excel 8.0 ";
            //            break;
            //        case ".xlsx": //Excel 07
            //            conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=Excel 12.0 ";
            //            break;
            //    }
            //    conStr = String.Format(conStr, FilePath, 1);
            //    OleDbConnection connExcel = new OleDbConnection(conStr);
            //    OleDbCommand cmdExcel = new OleDbCommand();
            //    OleDbDataAdapter oda = new OleDbDataAdapter();
            //    DataTable dt = new DataTable();
            //    cmdExcel.Connection = connExcel;
            //    connExcel.Open();
            //    DataTable dtExcelSchema;
            //    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //    string SheetName = null;
            //    if (Step == "1")
            //    {
            //        SheetName = "CustomersList$";
            //    }
            //    connExcel.Close();

            //    //Read Data from First Sheet
            //    connExcel.Open();
            //    cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            //    oda.SelectCommand = cmdExcel;
            //    oda.Fill(dt);

            //    GridView1.DataSource = dt;
            //    GridView1.DataBind();

            //    connExcel.Close();
            //    LoadData(dt);


            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        //}

        private void LoadData(DataTable dt_)
        {
            try
            {
                for (int i = 0; i < dt_.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt_.Rows[i]["ID"].ToString()))
                    {
                        Customers_ customers = new Customers_();

                        customers.CustomerID = dt_.Rows[i]["ID"].ToString();
                        customers.CustomerName = dt_.Rows[i]["Customer Name"].ToString();
                        customers.GST = dt_.Rows[i]["GST"].ToString();
                        customers.category = dt_.Rows[i]["Category"].ToString();
                        customers.NTN = dt_.Rows[i]["NTN"].ToString();
                        customers.customertype_ = dt_.Rows[i]["Type"].ToString();
                        customers.Area = dt_.Rows[i]["Area"].ToString();
                        customers.RefNum = dt_.Rows[i]["Ref #"].ToString();
                        customers.District = dt_.Rows[i]["District"].ToString();
                        customers.PhoneNo = dt_.Rows[i]["Phone No"].ToString();
                        customers.Email = dt_.Rows[i]["Email"].ToString();
                        customers.CellNo1 = dt_.Rows[i]["Cell No1"].ToString();
                        customers.PostalCode = dt_.Rows[i]["Postal Code"].ToString();
                        customers.CellNo2 = dt_.Rows[i]["Cell No2"].ToString();
                        customers.PostalOfficeContact = dt_.Rows[i]["Postal Office Contact"].ToString();
                        customers.CellNo3 = dt_.Rows[i]["Cell No3"].ToString();
                        customers.NIC = dt_.Rows[i]["NIC"].ToString();
                        customers.CellNo4 = dt_.Rows[i]["Cell No4"].ToString();
                        customers.city_= dt_.Rows[i]["City"].ToString();
                        customers.Address = dt_.Rows[i]["Address"].ToString();
                        customers.CreatedBy = Session["user"].ToString();
                        customers.CreatedDate = DateTime.Now;
                        customers.IsActive = "true";

                        CustomerManager custmanag = new CustomerManager(customers);
                        custmanag.Save();
                        Clear();
                        FillGrid();

                    }
                }
            }
            catch (Exception ex)
            {
                   throw;
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //lblalert.Text = ex.Message;
            }
        }

        public void Clear()
        {
            HFCustomerID.Value = "";
            TBCustomersName.Value = "";            
            TBNTN.Value = "";
            TBPhone.Value = "";
            TBEmail.Value = "";
            TBNIC.Value = "";
            TBAddress.Value = "";
            HFAccountCategoryName.Value = "";
            SubHeadCat.Value = "";
            TBPrevBal.Text = "";
        }
        protected void GVCutomers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {

                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    string CusID = GVCutomers.DataKeys[row.RowIndex].Values[0].ToString();
                    
                    HFCustomerID.Value = CusID;



                    dt_ = DBConnection.GetQueryData("select * from Customers_  where  CustomerID = '" + HFCustomerID.Value.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");
                                       

                    if (dt_.Rows.Count > 0)
                    {
                        TBCustomersName.Value = dt_.Rows[0]["CustomerName"].ToString();
                        TBNTN.Value = dt_.Rows[0]["NTN"].ToString();
                        TBArea.Text = dt_.Rows[0]["areaid"].ToString();
                        TBArea.Text = dt_.Rows[0]["Area"].ToString();
                        TBPhone.Value = dt_.Rows[0]["PhoneNo"].ToString();
                        TBEmail.Value = dt_.Rows[0]["Email"].ToString();
                        TBAddress.Value = dt_.Rows[0]["Address"].ToString();
                        TBNIC.Value = dt_.Rows[0]["NIC"].ToString();
                        TBCity.Text = dt_.Rows[0]["city_"].ToString();
                        TBCustomersName.Focus();

                        string accno = "";

                        query = "select * from SubHeadCategories where SubHeadCategoriesName ='" + TBCustomersName.Value + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                        dt_ = DBConnection.GetQueryData(query);
                        if(dt_.Rows.Count > 0)
                        {
                            HFAccountCategoryName.Value = dt_.Rows[0]["SubHeadCategoriesID"].ToString();
                            accno = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                        }

                        query = " select * from tbl_Salcredit  where CustomerID ='" + accno + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                        
                        DataTable dtsalcre = new DataTable();
                        dtsalcre = DBConnection.GetQueryData(query);

                        if (dtsalcre.Rows.Count > 0)
                        {
                            TBPrevBal.Text = dtsalcre.Rows[0]["CredAmt"].ToString();
                        }
                        else
                        {
                            TBPrevBal.Text = "0";
                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showcustomer();", true);


                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                        lblalert.Text = "Not Record Found!";
                    }

                }
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }

        }

        public void disabled()
        {
            TBCustomersName.Disabled = true;
            TBNTN.Disabled = true;
            TBArea.Enabled = false;
            TBPhone.Disabled = true;
            TBEmail.Disabled = true;
            TBNIC.Disabled = true;
            TBCity.Enabled = false;
            TBAddress.Disabled = true;

        }

        public void enabled()
        {
            TBCustomersName.Disabled = false;
            TBNTN.Disabled = false;
            TBArea.Enabled = true;
            TBPhone.Disabled = false;
            TBEmail.Disabled = false;
            TBNIC.Disabled = false;
            TBCity.Enabled = true;
            TBAddress.Disabled = false;

        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showcustomer();", true);
            TBCustomersName.Focus();
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showcustomer();", true);
            TBCustomersName.Focus();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //btnEdit.Enabled = false;
            //btnReset.Enabled = true;
            //btnSubmit.Enabled = true;
            //enabled();


            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showcustomer();", true);
        }

        protected void TBSearch_TextChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            try
            {
                FillGrid();
                DataTable _dt = (DataTable)ViewState["Customer"];
                DataView dv = new DataView(_dt, "CustomerName LIKE '%" + TBSearch.Text.Trim().ToUpper() + "%'", "[CustomerName] ASC", DataViewRowState.CurrentRows);
                DataTable dt_ = new DataTable();
                dt_ = dv.ToTable();
                GVCutomers.DataSource = dt_;
                GVCutomers.DataBind();
                TBSearch.Text = "";
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }

        }

        public void BindDll()
        {
            try
            {

            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        protected void DDLCategoryID_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void LinkBtnAddCustCategory_Click(object sender, EventArgs e)
        {
            if (TBCustomerCategoryName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "myModalCustCategory();", true);
                TBCustomerCategoryName.Focus();
                BindDll();
            }
            else
            {
                //string query = "select * from CustomerCategory where Category ='" + TBCustomerCategoryName.Text + "'";

                //SqlCommand cmdcategoryname = new SqlCommand(query, con);

                DataTable dtcategoryname = new DataTable();

                //SqlDataAdapter adpcustomertype = new SqlDataAdapter(cmdcategoryname);

                //adpcustomertype.Fill(dtcategoryname);
                
                
                if (dtcategoryname.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Category Name is already Exits!!";
                    BindDll();

                }
                else
                {

                    try
                    {
                        CustomerCategory CustCat = new CustomerCategory();

                        CustCat.CategoryID = HFCustCategory.Value;
                        CustCat.Category = string.IsNullOrEmpty(TBCustomerCategoryName.Text) ? null : TBCustomerCategoryName.Text;

                        CustomerCategoryManager custcatmanag = new CustomerCategoryManager(CustCat);
                        custcatmanag.Save();
                        ClearCustCat();
                        BindDll();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showcustomer();", true);

                    }
                    catch (Exception ex)
                    {
                        //   throw;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                        lblalert.Text = ex.Message;
                    }
                }

            }
        }

        public void ClearCustCat()
        {
            HFCustCategory.Value = "";
            TBCustomerCategoryName.Text = "";
        }

        protected void LinkBtnCancelCustCategory_Click(object sender, EventArgs e)
        {
            ClearCustCat();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showcustomer();", true);
            BindDll();

        }

        protected void DDLCategoryID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (DDLCategoryID.Items.FindByText("< Add New >").Selected == true)
            //{

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "myModalCustCategory();", true);
            //    TBCustomerCategoryName.Focus();

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showcustomer();", true);
            //}
        }

        protected void DDLCityID_TextChanged(object sender, EventArgs e)
        {
            //if (TBCity.Items.FindByText("< Add New >").Selected == true)
            //{

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "myModalCity();", true);
            //    TBCity.Focus();

            //}

        }

        protected void DDLCityID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (TBCity.Items.FindByText("< Add New >").Selected == true)
            //{

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "myModalCity();", true);
            //    TBCity.Focus();

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showsupplier();", true);
            //}
        }

        protected void DDLCustomerType_TextChanged(object sender, EventArgs e)
        {

        }

        protected void DDLCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (DDLCustomerType.Items.FindByText("< Add New >").Selected == true)
            //{

            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "MyModalCustomerType();", true);
            //    TBCustomerType.Focus();

            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showcustomer();", true);
            //}
        }

        protected void LinkBtnInsertCustomerType_Click(object sender, EventArgs e)
        {
            if (TBCustomerType.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "MyModalCustomerType();", true);
                TBCustomerType.Focus();
                BindDll();
            }
            else
            {
                string query = "select * from CustomerType where CustomerType_ ='" + TBCustomerType.Text + "'";

                SqlCommand cmdcustomertype = new SqlCommand(query, con);

                DataTable dtcustomertype = new DataTable();

                SqlDataAdapter adpcustomertype = new SqlDataAdapter(cmdcustomertype);

                adpcustomertype.Fill(dtcustomertype);

                if (dtcustomertype.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Customer Type is already Exits!!";
                    BindDll();
                }
                else
                {
                    try
                    {
                        CustomerType custtype = new CustomerType();

                        custtype.CustomerTypeID = HFMyCustomerType.Value;
                        custtype.CustomerType_ = string.IsNullOrEmpty(TBCustomerType.Text) ? null : TBCustomerType.Text;

                        CustomerTypeManager custtypemanag = new CustomerTypeManager(custtype);
                        custtypemanag.Save();
                        ClearCustomerType();
                        BindDll();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showcustomer();", true);

                    }
                    catch (Exception ex)
                    {
                        //   throw;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                        lblalert.Text = ex.Message;
                    }
                }

            }
        }

        protected void LinkBtnCancelCustomerType_Click(object sender, EventArgs e)
        {
            ClearCustomerType();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showcustomer();", true);
            BindDll();

        }

        public void ClearCustomerType()
        {
            HFMyCustomerType.Value = "";
            TBCustomerType.Text = "";
        }

        protected void LinkBtnCityInsert_Click(object sender, EventArgs e)
        {

            if (TBCity.Text == "")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "myModalCity();", true);
                TBCity.Focus();
                BindDll();
            }
            else
            {
                string query = "select * from City where City_ ='" + TBCity.Text + "'";

                SqlCommand cmdcity = new SqlCommand(query, con);

                DataTable dtcity = new DataTable();

                SqlDataAdapter adpcity = new SqlDataAdapter(cmdcity);

                adpcity.Fill(dtcity);

                if (dtcity.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "City is already Exits!!";
                    BindDll();
                }
                else
                {
                    try
                    {
                        City city = new City();

                        city.CityID = HFCity.Value;
                        city.City_ = string.IsNullOrEmpty(TBCity.Text) ? null : TBCity.Text;

                        CityManager citymanag = new CityManager(city);

                        citymanag.Save();
                        ClearCustomerType();
                        BindDll();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showcustomer();", true);

                    }
                    catch (Exception ex)
                    {
                        //   throw;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                        lblalert.Text = ex.Message;
                    }
                }
            }


        }

        protected void LinkBtnCityCancel_Click(object sender, EventArgs e)
        {
            ClearCity();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showcustomer();", true);
            BindDll();

        }

        public void ClearCity()
        {
            HFCity.Value = "";
            TBCity.Text = "";
        }

        protected void GVCutomers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "AlertDelete();", true);
            lblmodaldelete.Text = "Are you sure you want to Delete !!";
            string id = GVCutomers.DataKeys[e.RowIndex].Values[0] != null ? GVCutomers.DataKeys[e.RowIndex].Values[0].ToString() : null;
            HFCustId.Value = id;
           
        }

        protected void linkmodaldelete_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlquery = " delete from  Customers_ where CustomerID = '" + HFCustId.Value + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                SqlCommand cmd = new SqlCommand(sqlquery, con);

                con.Open();
                cmd.ExecuteNonQuery();
                FillGrid();
                con.Close();

            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;

            }

        }
        protected void btnalertOk_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showcustomer();", true);

            if (TBCustomersName.Value == "")
            {
                TBCustomersName.Focus();

            }
            else if (TBPhone.Value == "")
            {
                TBPhone.Focus();

            }
            else if (TBEmail.Value == "")
            {

                TBEmail.Focus();

            }
            //else if (TBNIC.Value == "")
            //{
            //    TBNIC.Focus();
            //}
            else if (TBAddress.Value == "")
            {
                TBAddress.Focus();
            }
        }

        protected void TBCity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                query = "select * from City where City_='" + TBCity.Text.Trim() + "'";
                    
                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    //Do Noting
                }
                else
                {

                    string cityid = RandomNumber().ToString();

                    query = " INSERT INTO [City] ([CityID] ,[City_]) " +
                        " VALUES ('" + cityid + "' ,'" + TBCity.Text + "')";
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


        public int RandomNumber()
        {
            int min;
            int max;
            min = 0;
            max = 10000;
            Random random = new Random();
            return random.Next(min, max);
        }

        protected void TBArea_TextChanged(object sender, EventArgs e)
        {
            try
            {
                query = "select * from tbl_area where area_='" + TBArea.Text.Trim() + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    //Do Noting
                }
                else
                {  
                    query = " INSERT INTO [tbl_area] ([area_] ,[CompanyId] ,[BranchId]) " +
                        " VALUES ('" + TBArea.Text + "','"
                        + Session["CompanyID"] + "','" + Session["BranchID"] + "' )";
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

        protected void TBPrevBal_TextChanged(object sender, EventArgs e)
        {
            v_prebal.Text = "";
        }

    }
}