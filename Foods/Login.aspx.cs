using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Foods;
using DataAccess;


namespace Foods
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        SqlDataAdapter adpt;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bindll();
                pnl_pass.Visible = false;
                ddl_brnch.Focus();
            }
        }

        #region Methods

        private void Bindll()
        {
            try
            {

                string Command = " select distinct(Users.BranchId), tbl_branches.Name from Users inner join tbl_branches on Users.BranchId = tbl_branches.BranchId ";

                adpt = new SqlDataAdapter(Command, con);
                dt = new DataTable();

                adpt.Fill(dt);

                int i = dt.Rows.Count;
                ddl_brnch.DataSource = dt;
                ddl_brnch.DataBind();
                ddl_brnch.DataTextField = "Name";
                ddl_brnch.DataValueField = "BranchId";
                ddl_brnch.DataBind();
                ddl_brnch.Items.Insert(0, new ListItem("--Select Branch--", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert(" + "Message:" + ex.Message + "StackTrace:" + ex.StackTrace + "Inner Exceptions:" + ex.InnerException + ");", true);
            }

        }

        private void CheckDate()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("dbo.spc_GetDateExpiry", con);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter rdr = new SqlDataAdapter(cmd);

                dt = new DataTable();
                rdr.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Logins();
                    //updatepassword();
                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "alert('Your System Password has Been Expired Please Contact The Administrator!');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "alert('" + ex.Message + "');", true);
                //lblalert.Text = ex.Message;
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
        }

        private void Logins()
        {
            try
            {
                //string Command = "Select * from Users where Username = '" + txtuser.Text + "' and BranchId = '" + ddl_brnch.SelectedValue.Trim() + "'";
                string Command = " select tbl_branches.Name as [BranchName],* from users  " +
                    " inner join  tbl_Companies on users.CompanyId = tbl_Companies.CompanyId " +
                    " inner join tbl_branches on users.BranchId = tbl_branches.BranchId " +
                    " where Users.BranchId ='" + ddl_brnch.SelectedValue.Trim() + "'";
                //SqlCommand DataCommand = new SqlCommand(Command, con);


                adpt = new SqlDataAdapter(Command, con);
                dt = new DataTable();
                adpt.Fill(dt);

                int i = dt.Rows.Count;

                foreach (DataRow row in dt.Rows)
                {
                    string password_ = row["Password"].ToString();
                    string pass = password.Text.Trim();

                    var AppPassword = Decrypt(password_);
                    if (pass == AppPassword)
                    {
                        if (i > 0)
                        {
                            Session["user"] = row["Name"];
                            Session["Username"] = row["Username"];
                            Session["Designation"] = row["Designation"];
                            Session["Name"] = row["Name"];
                            Session["Company"] = row["CompanyName"];
                            Session["CompanyID"] = row["CompanyId"];
                            Session["BranchID"] = row["BranchId"];
                            Session["BranchName"] = row["BranchName"];
                            Session["Level"] = row["Level"];
                            Session["CompanyImg"] = row["CompanyImg"];
                            string uname = row["Username"].ToString();
                            string designation = Session["Designation"].ToString();
                            string lvl = row["Level"].ToString();

                            // Get Company Details
                            string query = "Select * from tbl_Companies where TradeLicenseNo='" + Session["CompanyID"] + "'";
                            DataTable dt_ = new DataTable();
                            dt_ = new DataTable();
                            dt_ = DBConnection.GetQueryData(query);

                            if (dt_.Rows.Count > 0)
                            {
                                Session["CompanyAddress"]= dt_.Rows[0]["Address"].ToString();
                                Session["Companyph"] = dt_.Rows[0]["TelephoneNo"].ToString();

                            }

                            
                            if (lvl == "8")
                            {
                                Response.Redirect("~/Source/IP/D/frm_PSal.aspx?UID=" + uname + "&Lvl=" + lvl + "&DESIG=" + designation + "/...");
                            }
                            else
                            {
                                Response.Redirect("~/Source/IP/D/WellCome.aspx");                                //Response.Redirect("~/WellCome.aspx");

                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "alert('Invaliud User Name and Password!!!');", true);
                        password.Focus();
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "alert('Invaliud User Name and Password!!!');", true);
                txtuser.Text = "";
                txtuser.Focus();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert(" + "Message:" + ex.Message + "StackTrace:" + ex.StackTrace + "Inner Exceptions:" + ex.InnerException + ");", true);
            }
        }
        #endregion

        #region Events

        #endregion

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

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }





        #region Update Password

        // for password insertion Start //
        public void updatepassword()
        {
            string constr = ConfigurationManager.ConnectionStrings["D"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("update Users set Password = @Password where Username = '" + txtuser.Text.Trim() + "'"))
                {
                    cmd.CommandType = CommandType.Text;
                    //cmd.Parameters.AddWithValue("@Username", txtuser.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", Encrypt(password.Text.Trim()));
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            CheckDate();
        }

        protected void ddl_com_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtuser.Focus();
        }

        // for password insertion Ends //
        #endregion
       
    }
}