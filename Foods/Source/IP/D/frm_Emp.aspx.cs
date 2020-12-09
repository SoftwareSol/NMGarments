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
using Foods;
using System.Security.Cryptography;
using System.Text;
using DataAccess;

namespace Foods
{
    public partial class frm_Emp : System.Web.UI.Page
    {
        DBConnection connection;
        DataTable dt_;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        string query,pass;
        SqlDataAdapter adapter;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGrid();
                level();
                AutoID();
                TBSal.Value = "0";
            }
        }

        public void ShowAccountCategoryID()
        {
            try
            {

               // query = "select SubHeadCategoriesID from SubHeadCategories where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "' order by SubHeadCategoriesID desc";
                query = "select top 1 SubHeadCategoriesID from SubHeadCategories where SubHeadGeneratedID = '0023' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "' order by SubHeadCategoriesID desc";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                DataTable dt_ = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt_);
                if (dt_.Rows.Count <= 0)
                {
                    HFSubHead.Value = "1";
                }
                else
                {
                    //SqlDataReader reader = cmd.ExecuteReader();

                    //while (reader.Read())
                    {
                        if (HFSubHead.Value == "")
                        {
                            //int v = Convert.ToInt32(reader["SubHeadCategoriesID"].ToString());
                            int v = Convert.ToInt32(dt_.Rows[0]["SubHeadCategoriesID"].ToString());
                            int b = v + 1;
                            HFSubHead.Value = b.ToString();

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

        public void FillGrid()
        {
            try
            {
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData("   select * from users  where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                GVEmp.DataSource = dt_;
                GVEmp.DataBind();
                ViewState["Emp"] = dt_;
            }
            catch (Exception ex)
            {
                //throw;
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
                DataTable _dt = (DataTable)ViewState["Emp"];
                DataView dv = new DataView(_dt, "Username LIKE '%" + TBSearch.Text.Trim().ToUpper() + "%'", "[Username] ASC", DataViewRowState.CurrentRows);
                DataTable dt_ = new DataTable();
                dt_ = dv.ToTable();
                GVEmp.DataSource = dt_;
                GVEmp.DataBind();
                TBSearch.Text = "";
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }

        }

        public void level()
        {
            DataTable dt_ = new DataTable();

            dt_.Columns.AddRange(new DataColumn[] { new DataColumn("Name"), new DataColumn("ID") });
            dt_.Clear();


            dt_.Rows.Add("Manager", "1");
            dt_.Rows.Add("Booker", "2");
            dt_.Rows.Add("Sales Man", "3");
            
            
        }

        


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (TBEmpName.Value == "" || TBdesig.Value =="")
                {
                    if(TBEmpName.Value=="")
                    {

                        v_desc.Text = "";
                        
                        v_employee.Text = "Enter Employee Name";
                        TBEmpName.Focus();
                    }
                    else
                    {
                        v_employee.Text = "";
                        v_desc.Text = "Enter Designation";
                        TBdesig.Focus();
                    }
                    
                }
                else if (HFUsrNam.Value == "")
                {
                    v_desc.Text = "";
                    v_employee.Text = "";

                    int a;
                    a = Save();

                    if (a == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                        lblalert.Text = "Employee Has Been Saved!";
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

                    v_desc.Text = "";
                    v_employee.Text = "";
                    int b;
                    b = Update();

                    if (b == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                        lblalert.Text = "Employee Has Been Update!";
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

            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        public void Clear()
        {
            HFUsrNam.Value = "";
            TBEmpName.Value = "";
            TBAdd.Value = "";
            TBdesig.Value = "";
            TBphno.Value = "";
            TBfxno.Value = "";
            TBmbno.Value = "";
            TBemal.Value = "";
            TBSal.Value = "";
            chkchgpass.Checked = false;
            chkaccdisbl.Checked = false;
        }
        private int Save()
        {

            con.Open();

            int res;
            pass = Encrypt(TBEmpName.Value);
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
                #region Employees

                command.CommandText= " INSERT INTO Users (CompanyId ,BranchId ,Username ,Password ,Name " +
                    " ,Address ,Designation ,TelephoneNo ,FaxNo ,MobileNo ,Email,CanChangePassword,Level " +
                    " ,AccountDisable,CreateBy ,CreateTime ,CreateTerminal, CompanyName,Salary)  VALUES " +
                    " ('" + Session["CompanyID"] + "' ,'" + Session["BranchID"] + "', '" + TBEmpName.Value + "'" +
                    " ,'" + pass + "' ,'" + TBEmpName.Value + "','" + TBAdd.Value + "' ,'" + TBdesig.Value + "','" + TBphno.Value + "'" +
                    " ,'" + TBfxno.Value + "','" + TBmbno.Value + "','" + TBemal.Value + "'" +
                    " ,'" + chkchgpass.Checked + "','" + DDL_Desig.SelectedValue + "','" + chkaccdisbl.Checked + "'" +
                    " ,'" + Session["user"].ToString() + "','" + DateTime.Now + "','::1'" +
                    " ,'" + Session["Company"] + "','" + TBSal.Value + "')";

                command.ExecuteNonQuery();

                #endregion

                #region Accounts


                //SubHeadCategories subheadcat = new SubHeadCategories();

                //subheadcat.SubHeadCategoriesID = HFAccountCategoryName.Value;
                //subheadcat.ven_id = "1";//string.IsNullOrEmpty(DDLAccCat.SelectedValue) ? null : DDLAccCat.SelectedValue;
                //subheadcat.SubHeadCategoriesName = "abc"; //string.IsNullOrEmpty(DDLAccCat.SelectedItem.Text) ? null : DDLAccCat.SelectedItem.Text;
                //subheadcat.SubHeadCategoriesName = string.IsNullOrEmpty(TBEmpName.Value) ? null : TBEmpName.Value;
                //subheadcat.SubHeadCategoriesGeneratedID = string.IsNullOrEmpty("0023" + HFSubHead.Value) ? null : "0023" + HFSubHead.Value;
                //subheadcat.HeadGeneratedID = string.IsNullOrEmpty("001") ? null : "001";
                //subheadcat.SubHeadGeneratedID = string.IsNullOrEmpty("0023") ? null : "0023";
                //subheadcat.CreatedAt = DateTime.Now;
                //subheadcat.CreatedBy = Session["user"].ToString();
                //subheadcat.SubCategoriesKey = string.IsNullOrEmpty(HFSubHead.Value) ? null : HFSubHead.Value;

                //SubHeadCategoriesManager subheadcatmanag = new SubHeadCategoriesManager(subheadcat);
                //subheadcatmanag.Save();


                command.CommandText = " INSERT INTO [SubHeadCategories] ([ven_id] ,[SubHeadCategoriesName] ,[SubHeadCategoriesGeneratedID] ,[HeadGeneratedID] , " +
                 "[SubHeadGeneratedID],[CreatedAt] " +
                 " ,[CreatedBy] ,[SubCategoriesKey],CompanyId,BranchId)  VALUES " +
                 " ('1', '" + TBEmpName.Value + "' ,'" + "0023" + HFSubHead.Value + "','002', '0023','" + DateTime.Now + "'" +
                 ",'" + Session["user"] + "','" + "0023" + HFSubHead.Value + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "' )";

                command.ExecuteNonQuery();


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



        protected void GVEmp_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "AlertDelete();", true);
            lblmodaldelete.Text = "Are you sure you want to Delete !!";
            string id = GVEmp.DataKeys[e.RowIndex].Values[0] != null ? GVEmp.DataKeys[e.RowIndex].Values[0].ToString() : null;
            HFUsrId.Value = id; 
        }


        protected void linkmodaldelete_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlquery = " delete from  Users where Username = '" + HFUsrId.Value + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                SqlCommand cmd = new SqlCommand(sqlquery, con);

                con.Open();
                cmd.ExecuteNonQuery();
                FillGrid();
                con.Close();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }

        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private int Update()
        {
            int h;
            con.Open();

            pass = Encrypt(TBEmpName.Value);

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
                query = " Update Users set CompanyId='" + Session["CompanyID"] + "', BranchId='" + Session["BranchID"] + "'" +
                    " , Username = '" + TBEmpName.Value + "', Password='" + pass + "', Name='" + TBEmpName.Value + "', Address='" + TBAdd.Value + "'" +
                    " , Designation ='" + TBdesig.Value + "', TelephoneNo='" + TBphno.Value + "', FaxNo = '" + TBfxno.Value + "'" +
                    " , MobileNo='" + TBmbno.Value + "', Email='" + TBemal.Value + "', CanChangePassword='" + chkchgpass.Checked +
                    "', Level='" + DDL_Desig.SelectedValue + "', AccountDisable='" + chkaccdisbl.Checked + "', CreateBy='" + Session["user"].ToString() + "', CreateTime='" + DateTime.Now +
                    "', CreateTerminal='::1', CompanyName='" + Session["Company"] + "', Salary='"+ TBSal.Value +"' where Username= '" + HFUsrNam.Value.Trim() + "'";

                command.CommandText = query;
                command.ExecuteNonQuery();

                // For Account

                //query = "select * from SubHeadCategories  where SubHeadCategoriesID= '" + HFAccountCategoryName.Value.Trim() + "'";


                //dt_ = DBConnection.GetQueryData(query);

                //if (dt_.Rows.Count > 0)
                //{
                //    subheadcatid = dt_.Rows[0]["SubHeadCategoriesGeneratedID"].ToString();
                //}


                //SubHeadCategories subheadcat = new SubHeadCategories();

                //subheadcat.SubHeadCategoriesID = HFAccountCategoryName.Value;
                //subheadcat.ven_id = "1";//string.IsNullOrEmpty(DDLAccCat.SelectedValue) ? null : DDLAccCat.SelectedValue;
                //subheadcat.SubHeadCategoriesName = "abc"; //string.IsNullOrEmpty(DDLAccCat.SelectedItem.Text) ? null : DDLAccCat.SelectedItem.Text;
                //subheadcat.SubHeadCategoriesName = string.IsNullOrEmpty(TBCustomersName.Value) ? null : TBCustomersName.Value;
                //subheadcat.SubHeadCategoriesGeneratedID = string.IsNullOrEmpty(subheadcatid) ? null : subheadcatid;
                //subheadcat.HeadGeneratedID = string.IsNullOrEmpty("001") ? null : "001";
                //subheadcat.SubHeadGeneratedID = string.IsNullOrEmpty("0011") ? null : "0011";
                //subheadcat.CreatedAt = DateTime.Now;
                //subheadcat.CreatedBy = Session["user"].ToString();
                //subheadcat.SubCategoriesKey = string.IsNullOrEmpty(subheadcatid) ? null : subheadcatid;

                //SubHeadCategoriesManager subheadcatmanag = new SubHeadCategoriesManager(subheadcat);
                //subheadcatmanag.Save();

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


        protected void GVEmp_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {

                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    string CusID = GVEmp.DataKeys[row.RowIndex].Values[0].ToString();
                    HFUsrNam.Value = CusID;

                    dt_ = DBConnection.GetQueryData("select * from Users  where  Username = '" + HFUsrNam.Value.Trim() + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");


                    if (dt_.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "showempyee();", true);

                        TBEmpName.Value = dt_.Rows[0]["Name"].ToString();
                        TBAdd.Value = dt_.Rows[0]["Address"].ToString();
                        TBdesig.Value = dt_.Rows[0]["Designation"].ToString();
                        TBphno.Value = dt_.Rows[0]["TelephoneNo"].ToString();
                        TBfxno.Value = dt_.Rows[0]["FaxNo"].ToString();
                        TBmbno.Value = dt_.Rows[0]["MobileNo"].ToString();
                        TBemal.Value = dt_.Rows[0]["Email"].ToString();
                        TBSal.Value = dt_.Rows[0]["Salary"].ToString();
                        DDL_Desig.SelectedValue = dt_.Rows[0]["Level"].ToString();
                        chkchgpass.Checked = Convert.ToBoolean(dt_.Rows[0]["CanChangePassword"].ToString());                        
                        chkaccdisbl.Checked = Convert.ToBoolean(dt_.Rows[0]["AccountDisable"].ToString());
                        TBEmpName.Focus();

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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("frm_Emp.aspx");
        }
    }
}