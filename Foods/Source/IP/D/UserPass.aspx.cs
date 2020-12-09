using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using Foods;
using DataAccess;

namespace Foods
{
    public partial class UserPass : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DBConnection db = new DBConnection();
        string query;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void clear()
        {
            TBPass.Text = "";            
            lblerr.Text = "";
        }

        // for password insertion Start //
        public int updatepassword()
        {
            int a;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["D"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("update Users set Password = @Password where Username = '" + Session["Username"] + "'"))
                    {
                        cmd.CommandType = CommandType.Text;
                        //cmd.Parameters.AddWithValue("@Username", txtuser.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", Encrypt(TBPass.Text.Trim()));
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "ModalPopUp();", true);
                lblalert.Text = ex.Message;
                
            }

            a = 1;
            return a;

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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int chk = 0;
            chk= updatepassword();
            if (chk == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "ModalPopUp();", true);
                lblalert.Text = "Your Password has been Updated!!";
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "ModalPopUp();", true);
                lblalert.Text = "Your Password is not Updated Please Contact Administrator!!";
            }

            clear();
        }
    }
}