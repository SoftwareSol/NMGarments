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
    public partial class Supplier_ : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        DBConnection db= new DBConnection();
        string query;
        decimal ttlcre;
        SqlCommand command;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                FillGrid();
                BindDll();
                AutoID();
                btnEdit.Enabled = false;
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
                dt_ = DBConnection.GetQueryData(" select * from supplier where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' order by supplierId desc");

                GVSupplier.DataSource = dt_;
                GVSupplier.DataBind();
                ViewState["Supplier"] = dt_;
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

        protected void TBSearch_TextChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            try
            {
                FillGrid();
                DataTable _dt = (DataTable)ViewState["Supplier"];
                DataView dv = new DataView(_dt, "suppliername LIKE '%" + TBSearch.Text.Trim().ToUpper() + "%'", "[suppliername] ASC", DataViewRowState.CurrentRows);
                DataTable dt_ = new DataTable();
                dt_ = dv.ToTable();

                GVSupplier.DataSource = dt_;
                GVSupplier.DataBind();
                TBSearch.Text = "";
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
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
                string FileName = "SupplierList.xls";
                StringWriter strwritter = new StringWriter();
                HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
                GVSupplier.GridLines = GridLines.Both;
                GVSupplier.HeaderStyle.Font.Bold = true;

                GVSupplier.RenderControl(htmltextwrtter);

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
            //    //throw;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
            //    lblalert.Text = ex.Message;
            //}
        }

        private void Import_To_Grid(string FilePath, string Extension, string Step)
        {
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
            //        SheetName = "SupplierList$";
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
        }

        public void ShowAccountCategoryID()
        {
            try
            {

                query = "select top 1  SubHeadCategoriesID from SubHeadCategories where SubHeadGeneratedID = '0021' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "' order by SubHeadCategoriesID desc";
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
                            int v = Convert.ToInt32(dt_.Rows[0]["SubHeadCategoriesID"].ToString());
                            //int v = Convert.ToInt32(reader["SubHeadCategoriesID"].ToString());
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

        private void LoadData(DataTable dt_)
        {
            try
            {
                for (int i = 0; i < dt_.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(dt_.Rows[i]["ID"].ToString()))
                    {
                        supplier supplier = new supplier();

                        supplier.supplierId = dt_.Rows[i]["ID"].ToString();
                        supplier.suppliername = dt_.Rows[i]["Supplier Name"].ToString();
                        supplier.contactperson = dt_.Rows[i]["Contact Person"].ToString();
                        supplier.BackUpContact = dt_.Rows[i]["BackUp Contact"].ToString();
                        supplier.City_ = dt_.Rows[i]["City"].ToString();
                        supplier.phoneno = dt_.Rows[i]["Phone No"].ToString();
                        supplier.mobile = dt_.Rows[i]["Mobile"].ToString();
                        supplier.faxno = dt_.Rows[i]["Fax No"].ToString();
                        supplier.postalcode = dt_.Rows[i]["Postal Code"].ToString();
                        supplier.designation = dt_.Rows[i]["Designation"].ToString();
                        supplier.AddressOne = dt_.Rows[i]["Address One"].ToString();
                        supplier.AddressTwo = dt_.Rows[i]["Address Two"].ToString();
                        supplier.CNIC = dt_.Rows[i]["NIC"].ToString();
                        supplier.Url = dt_.Rows[i]["Url"].ToString();
                        supplier.BusinessNature = dt_.Rows[i]["Business Nature"].ToString();
                        supplier.Email = dt_.Rows[i]["Email"].ToString();
                        supplier.NTNNTRNo = dt_.Rows[i]["NTN/NTR No"].ToString();
                        supplier.CreatedBy = Session["user"].ToString();
                        supplier.CreatedDate = DateTime.Now;
                        supplier.IsActive = "True";

                        SupplierManager supmanag = new SupplierManager(supplier);
                        supmanag.Save();
                        FillGrid();

                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                //bool phn = false;
                //bool email = false;
                //Regex regexobjphn = new Regex(@"^([0-9]*|\d*\.\d{1}?\d*)$");
                //Regex regexobjemail = new Regex(@"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$");


                //if (TBSupplierName.Value == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //    lblalert.Text = "Please Write Supplier Name!!";
                //}

                //else if (TBPhoneNo.Value == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //    lblalert.Text = "Please Write Phone Num!!";
                //}

                //else if (TBMobile.Value == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //    lblalert.Text = "Please Write Cell No!!";
                //}
                //else if (TBDesignation.Value == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //    lblalert.Text = "Please Write Designation!!";
                //}
                //else if (TBNIC.Value == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //    lblalert.Text = "Please Write NIC!!";
                //}
                //else if (TBAddressOne.Value == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //    lblalert.Text = "Please Write Address!!";
                //}
                //else if (TBBusinessNature.Value == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //    lblalert.Text = "Please Write Business Nature!!";
                //}

                //else if (!regexobjphn.IsMatch(TBPhoneNo.Value))
                //{
                //    phn = false;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //    lblalert.Text = "Please Write Correct Phone Number!!";
                //}

                //else
                //{
                if (TBContactPerson.Value == "" || TBSupplierName.Value == "")
                {
                    if (TBSupplierName.Value == "")
                    {
                        v_conper.Text = "";
                        v_cname.Text = "Please Company Name";
                        TBSupplierName.Focus();
                    }
                    else
                    {
                        v_conper.Text = "Please Contact Person";
                        v_cname.Text = "";
                        TBContactPerson.Focus();
                    }

                }
                else if (TBPrevBal.Text == "")
                {
                    v_prebal.Text = "Please Write in Previous Balance...";
                }
                else
                {
                    if (HFSupplierID.Value == "")
                    {
                        v_cname.Text = "";
                        v_conper.Text = "";
                        int a;
                        a = Save();

                        if (a == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                            lblalert.Text = "Supplier Has Been Saved!";
                            Clear();
                            FillGrid();
                        }
                    }
                    else if (HFSupplierID.Value != "")
                    {
                        int b;
                        b = update();

                        if (b == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                            lblalert.Text = "Supplier Has Been Updated!";
                            Clear();
                            FillGrid();
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

        public void ShowAccountCategoryFiveID()
        {
            try
            {

            
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private int update()
        {
            int k = 1;
            query = " update supplier set suppliername = '" + TBSupplierName.Value + "', contactperson = '" + TBContactPerson .Value +
                    " ', BackUpContact = '', City_ ='" + TBCity.Text.ToString() +
                    " ', phoneno='" + TBPhoneNo.Value + "',mobile ='08789', faxno='', postalcode='0.00', designation='" + TBDesignation.Value +
                    " ', AddressOne='ksljkldskds', AddressTwo ='" + TBAddressTwo.Value +
                    " ', CNIC='', Url='', BusinessNature='', Email='" + TBEmail.Value +
                    "', NTNNTRNo='" + TBNTNNTRNo.Value + "' where  supplierId='" +
                    HFSupplierID.Value + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + 
                    Session["BranchID"] + "'";

            con.Open();

            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                cmd.ExecuteNonQuery();

            }
            con.Close();




            string accno = "";
            command =new SqlCommand();
            command.Connection = con;

            con.Open();

            command.CommandText = " select SubHeadCategoriesGeneratedID,SubHeadCategoriesName from SubHeadCategories where SubHeadCategoriesName='" + TBSupplierName.Value.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
            SqlDataAdapter adpchkcust = new SqlDataAdapter(command);

            DataTable dtchkcust = new DataTable();
            adpchkcust.Fill(dtchkcust);

            if (dtchkcust.Rows.Count > 0)
            {
                accno = dtchkcust.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
            }


            #region Credit Sheets


            query = "select CredAmt from tbl_Purcredit where supplierId='" + accno.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";


            DataTable dtpurcre = new DataTable();
            dtpurcre = DBConnection.GetQueryData(query);

            if (dtpurcre.Rows.Count > 0)
            {
                //double recv = Convert.ToDouble(lblOutstan) - Convert.ToDouble(TBRecy);

                //avapre = Convert.ToDecimal(dtsalcre.Rows[0]["CredAmt"]);

                ttlcre = Convert.ToDecimal(TBPrevBal.Text.Trim());

                query = " Update tbl_Purcredit set CredAmt = '" + ttlcre + "' where supplierId='" + accno.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                command = new SqlCommand(query, con);

                command.ExecuteNonQuery();
            }
            else
            {
                query = " insert into tbl_Purcredit (supplierId,CredAmt,CompanyId,BranchId) values('" + accno.Trim() + "','" + TBPrevBal.Text.Trim() + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                command = new SqlCommand(query, con);

                command.ExecuteNonQuery();
            }

            con.Close();
            #endregion
            return k;
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


                command.CommandText = "INSERT INTO [supplier] " +
                    " ([suppliername] ,[contactperson] " +
                    " ,[BackUpContact] ,[City_] " +
                    " ,[phoneno] ,[mobile] ,[faxno] ,[postalcode] ,[designation] ,[AddressOne] ,[AddressTwo] ,[CNIC] " +
                    " ,[Url] ,[BusinessNature] ,[Email] ,[NTNNTRNo] ,[CreatedBy] ,[CreatedDate] ,[IsActive] ,[Sup_Code] " +
                    " ,[CompanyId] ,[BranchId]) VALUES " +
                    " ('"+ TBSupplierName.Value +"' ,'"+ TBContactPerson.Value +"' ,'' " +
                    " ,'"+ TBCity.Text +"' ,'" + TBPhoneNo.Value + "' ,''  ,'' " +
                    " ,'' ,'' ,'' " +
                    " ,'"+ TBAddressTwo.Value + "' ,'' ,'" + TBUrl.Value + "' ,'' " +
                    " ,'"+ TBEmail.Value +"' ,'"+ TBNTNNTRNo.Value + "' ,'"+ Session["user"].ToString() +"' ,'" + DateTime.Now + "' " +
                    " ,'1' ,'','" + Session["CompanyID"] + "','" + Session["BranchID"] + "' )";

                command.ExecuteNonQuery();

                #endregion

                #region Accounts


                
                //SubHeadCategories subheadcat = new SubHeadCategories();

                //subheadcat.SubHeadCategoriesID = HFSubHeadCatID.Value;
                //subheadcat.ven_id = "1";//string.IsNullOrEmpty(DDLAccCat.SelectedValue) ? null : DDLAccCat.SelectedValue;
                //subheadcat.SubHeadCategoriesName = "abc"; //string.IsNullOrEmpty(DDLAccCat.SelectedItem.Text) ? null : DDLAccCat.SelectedItem.Text;
                //subheadcat.SubHeadCategoriesName = string.IsNullOrEmpty(TBSupplierName.Value) ? null : TBSupplierName.Value;
                //subheadcat.SubHeadCategoriesGeneratedID = string.IsNullOrEmpty("0021" + SubHeadCat.Value) ? null : "0021" + SubHeadCat.Value;
                //subheadcat.HeadGeneratedID = string.IsNullOrEmpty("002") ? null : "002";
                //subheadcat.SubHeadGeneratedID = string.IsNullOrEmpty("0021") ? null : "0021";
                //subheadcat.CreatedAt = DateTime.Now;
                //subheadcat.CreatedBy = Session["user"].ToString();
                //subheadcat.SubCategoriesKey = string.IsNullOrEmpty(SubHeadCat.Value) ? null : SubHeadCat.Value;

                //SubHeadCategoriesManager subheadcatmanag = new SubHeadCategoriesManager(subheadcat);
                //subheadcatmanag.Save();

                command.CommandText = " INSERT INTO [SubHeadCategories] ([ven_id] ,[SubHeadCategoriesName] ,[SubHeadCategoriesGeneratedID] ,[HeadGeneratedID] , " +
                 "[SubHeadGeneratedID],[CreatedAt] " +
                 " ,[CreatedBy] ,[SubCategoriesKey], CompanyId, BranchId)  VALUES " +
                 " ('1', '" + TBSupplierName.Value + "' ,'" + "0021" + SubHeadCat.Value + "','002', '0021','" + DateTime.Now + "'" +
                 ",'" + Session["user"] + "','" + "0021" + SubHeadCat.Value + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";

                command.ExecuteNonQuery();


                #endregion

                #region Credit Sheets

                string accno = "";

                command.CommandText = " select SubHeadCategoriesGeneratedID,SubHeadCategoriesName from SubHeadCategories where SubHeadCategoriesName='" + TBSupplierName.Value.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                SqlDataAdapter adpchkcust = new SqlDataAdapter(command);

                DataTable dtchkcust = new DataTable();
                adpchkcust.Fill(dtchkcust);

                if (dtchkcust.Rows.Count > 0)
                {
                    accno = dtchkcust.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                }

                command.CommandText = "select CredAmt from tbl_Purcredit where supplierId='" + accno.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                SqlDataAdapter stksalcre = new SqlDataAdapter(command);

                DataTable dtsalcre = new DataTable();
                stksalcre.Fill(dtsalcre);

                if (dtsalcre.Rows.Count > 0)
                {
                    //double recv = Convert.ToDouble(lblOutstan) - Convert.ToDouble(TBRecy);

                    //avapre = Convert.ToDecimal(dtsalcre.Rows[0]["CredAmt"]);

                    ttlcre = Convert.ToDecimal(TBPrevBal.Text.Trim());

                    command.CommandText = " Update tbl_Purcredit set CredAmt = '" + ttlcre + "' where supplierId='" + accno.Trim() + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                    command.ExecuteNonQuery();
                }
                else 
                {
                    //command.CommandText = " insert into tbl_Purcredit (CustomerID,CredAmt) values('" + DDL_CustAcc.SelectedValue.Trim() + "','" + ttlcre + "')";
                    command.CommandText = " insert into tbl_Purcredit (supplierId,CredAmt,CompanyId,BranchId) values('" + accno.Trim() + "','" + TBPrevBal.Text.Trim() + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
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

        public void Clear()
        {
            HFSupplierID.Value = "";
            TBSupplierName.Value = "";
            TBContactPerson.Value = "";            
            TBDesignation.Value = "";
            TBAddressTwo.Value = "";
            TBNIC.Value = "";
            TBUrl.Value = "";
            TBEmail.Value = "";
            TBNTNNTRNo.Value = "";
            TBPrevBal.Text = "";
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
            v_cname.Text = "";
            v_conper.Text = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showsupplier();", true);
            TBSupplierName.Focus();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnReset.Enabled = true;
            btnSubmit.Enabled = true;
            enabled();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showsupplier();", true);
        }

        public void disabled()
        {
            TBSupplierName.Disabled = true;
            TBContactPerson.Disabled = true;            
            TBCity.Enabled = false;
            TBPhoneNo.Disabled = true;
            //TBMobile.Disabled = true;
            //TBFaxNo.Disabled = true;
            //TBPostalCode.Disabled = true;
            TBDesignation.Disabled = true;
            //TBAddressOne.Disabled = true;
            TBAddressTwo.Disabled = true;
            TBNIC.Disabled = true;
            TBUrl.Disabled = true;
            //TBBusinessNature.Disabled = true;
            TBEmail.Disabled = true;
            TBNTNNTRNo.Disabled = true;
        }

        public void enabled()
        {
            TBSupplierName.Disabled = false;
            TBContactPerson.Disabled = false;
            TBCity.Enabled = true;
            TBPhoneNo.Disabled = false;
            //TBMobile.Disabled = false;
            //TBFaxNo.Disabled = false;
            //TBPostalCode.Disabled = false;
            TBDesignation.Disabled = false;
            //TBAddressOne.Disabled = false;
            TBAddressTwo.Disabled = false;
            TBNIC.Disabled = false;
            TBUrl.Disabled = false;
            //TBBusinessNature.Disabled = false;
            TBEmail.Disabled = false;
            TBNTNNTRNo.Disabled = false;
        }

        protected void GVSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showsupplier();", true);

                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    HFSupplierID.Value = GVSupplier.DataKeys[row.RowIndex].Values[1].ToString();
                    //string Query = "select * from supplier inner join subheadcategoryfive on supplier.Sup_Code = subheadcategoryfive.subheadcategoryfiveGeneratedID where supplier.Sup_Code ='" + SubHeadCatFiv.Value.Trim() + "'";
                    string Query = "select * from supplier  where supplierId ='" + HFSupplierID.Value.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                    dt_ = DBConnection.GetDataTable(Query);

                    if (dt_.Rows.Count > 0)
                    {
                        TBSupplierName.Value = dt_.Rows[0]["suppliername"].ToString();
                        TBContactPerson.Value = dt_.Rows[0]["contactperson"].ToString();
                        TBCity.Text = dt_.Rows[0]["City_"].ToString();
                        TBPhoneNo.Value = dt_.Rows[0]["phoneno"].ToString();
                        //TBMobile.Value = dt_.Rows[0]["mobile"].ToString();
                        //TBFaxNo.Value = dt_.Rows[0]["faxno"].ToString();
                        //TBPostalCode.Value = dt_.Rows[0]["postalcode"].ToString();
                        TBDesignation.Value = dt_.Rows[0]["designation"].ToString();
                        //TBAddressOne.Value = dt_.Rows[0]["AddressOne"].ToString();
                        TBAddressTwo.Value = dt_.Rows[0]["AddressTwo"].ToString();
                        TBNIC.Value = dt_.Rows[0]["CNIC"].ToString();
                        TBUrl.Value = dt_.Rows[0]["Url"].ToString();
                        //TBBusinessNature.Value = dt_.Rows[0]["BusinessNature"].ToString();
                        TBEmail.Value = dt_.Rows[0]["Email"].ToString();
                        TBNTNNTRNo.Value = dt_.Rows[0]["NTNNTRNo"].ToString();
                        HFSupplierID.Value = dt_.Rows[0]["supplierId"].ToString();

                        //HFSubHeadCatFivID.Value = dt_.Rows[0]["subheadcategoryfiveID"].ToString();
                        //SubHeadCatFiv.Value = dt_.Rows[0]["subheadcategoryfiveGeneratedID"].ToString();
                    }

                    string accno = "";

                    query = "select * from SubHeadCategories where SubHeadCategoriesName ='" + TBSupplierName.Value + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

                    dt_ = DBConnection.GetQueryData(query);
                    if (dt_.Rows.Count > 0)
                    {
                        accno = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                    }

                    query = " select * from tbl_Purcredit  where supplierId ='" + accno + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";

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

                    disabled();
                    btnEdit.Enabled = true;
                    btnReset.Enabled = false;
                    btnSubmit.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }


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
                        BindDll();
                        clearcity();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showsupplier();", true);

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

        public void clearcity()
        {
            TBCity.Text = "";
        }

        protected void GVSupplier_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "AlertDelete();", true);
            lblmodaldelete.Text = "Are you sure you want to Delete !!";
            string id = GVSupplier.DataKeys[e.RowIndex].Values[1] != null ? GVSupplier.DataKeys[e.RowIndex].Values[1].ToString() : null;
            HFSupID.Value = id;
           
        } 

        protected void linkmodaldelete_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlquery = " delete from  supplier where supplierId = '" + HFSupID.Value + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                db.CRUDRecords(sqlquery);
                FillGrid();

            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;

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

    }
}