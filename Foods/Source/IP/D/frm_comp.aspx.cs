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
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Foods;
using DataAccess;

namespace Foods
{
    public partial class frm_comp : System.Web.UI.Page
    {
        DBConnection connection;
        DataTable dt_;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        string query, str;
        SqlDataAdapter adapter;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGrid();
                gntCompNo();
                BindDll();
            }
        }

        public void BindDll()
        {
            dt_ = new DataTable();
            dt_ = DBConnection.GetQueryData("select * from tbl_Companies where IsActive = 1");

            if (dt_.Rows.Count > 0)
            {
                DDL_ComNam.DataSource = dt_;
                DDL_ComNam.DataTextField = "Name";
                DDL_ComNam.DataValueField = "CompanyId";
                DDL_ComNam.DataBind();
                DDL_ComNam.Items.Insert(0, new ListItem("--Select Company --", "0"));

            }
        }

        private void gntCompNo()
        {
            try
            {

                str = "select isnull(max(cast(CompanyId as int)),0) as [CompanyId]  from tbl_Companies";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(TBcompid.Value))
                    {
                        int v = Convert.ToInt32(reader["CompanyId"].ToString());
                        int b = v + 1;
                        TBcompid.Value = "COM_00" + b.ToString();
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
        public void FillGrid()
        {
            try
            {
                dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData("select * from users where Level = 1");

                GVComp.DataSource = dt_;
                GVComp.DataBind();
                //ViewState["Comp"] = dt_;
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
            int chck = Save();

            if (chck == 1)
            {
                Response.Redirect("frm_comp.aspx");
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("frm_comp.aspx");
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


        private int Save()
        {
            int j = 1;
            string pass = Encrypt(TBcompid.Value + "123");

            query = " INSERT INTO Users " +
                           " (CompanyId,BranchId,Username,Password,Name,Address,Designation,TelephoneNo,FaxNo,MobileNo, Email, CanChangePassword, [Level], AccountDisable, CreateBy, CreateTime, CreateTerminal, CompanyName) VALUES('"
                           + TBcompid.Value + "','" + TBBrchID.Value + "','" + TBuname.Value + "','" + pass + "','" + TBuname.Value + "','','','','','','','True','1','0','" + Session["user"].ToString() +
                           " ','" + DateTime.Now + "','::1', '"+ DDL_ComNam.SelectedItem.Text +"' )";
            con.Open();

            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                cmd.ExecuteNonQuery();

            }
            con.Close();

            return j;
        }

        private int update()
        {
            int k = 1;
            //query = " update tbl_Companies set Name = '" + TBCompany.Text + "', Address='" + TbAdd.Text + "', ContactPerson='" + TBContctPer.Text + "', TelephoneNo='" + TB_TelNo.Text + "', IsActive ='" + ck_act.Checked + "' , CreateBy='" + Session["user"].ToString() + "', CreateTime='" + DateTime.Now + "' where  CompanyId='" + lbl_CompID.Text.Trim() + "'";

            //con.Open();

            //using (SqlCommand cmd = new SqlCommand(query, con))
            {

            //    cmd.ExecuteNonQuery();

            }
            //con.Close();
            return k;
        }

        protected void GVComp_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                HiddenField HFCompanyID = (HiddenField)GVComp.Rows[e.RowIndex].FindControl("HFUsername");


                int h;

                h = delete(HFCompanyID.Value);

                if (h == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "The Company As been Deleted..";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Some thing is Wrong please Contact Administrator!..";
                }


            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;

            }

        }

        private int delete(string HFUsrID)
        {

            string sqlquery = "Delete from Users where Username = '" + HFUsrID + "'";
            SqlCommand cmd = new SqlCommand(sqlquery, con);

            con.Open();
            cmd.ExecuteNonQuery();
            FillGrid();
            con.Close();
            //clear();

            return 1;
        }

    }
}