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
    public partial class AssignForms : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DBConnection db = new DBConnection();
        string query;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fillgrid();
                BindDll();
            }
        }

        public void BindDll()
        {
            try
            {
                DataTable dtUsr = new DataTable();

                string usrqy = "select  Username,CompanyId,BranchId  from Users where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dtUsr = DBConnection.GetQueryData(usrqy);

                DDL_Usr.DataSource = dtUsr;
                DDL_Usr.DataTextField = "Username";
                DDL_Usr.DataValueField = "Username";
                DDL_Usr.DataBind();
                DDL_Usr.Items.Add(new ListItem("--Select Users--", "0"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void clear()
        {
            DDL_Usr.SelectedValue = "0";
            lblerr.Text = "";
        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int chk = 0;
            //chk= updatepassword();
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

        private int saveSPage()
        {
            try
            {
                foreach (GridViewRow g1 in GVModul.Rows)
                {
                    string module = (g1.FindControl("HFSlctModul") as TextBox).Text;
                    string chk_pg = ((g1.FindControl("chk_slct") as CheckBox).Checked).ToString();
                }

                foreach (GridViewRow g1 in GVSlectPgs.Rows)
                {
                    string slctPg = (g1.FindControl("HFSlctPg") as TextBox).Text;
                    string chk_spg = ((g1.FindControl("chk_slctpg") as CheckBox).Checked).ToString();
                }
                SqlCommand cmd = new SqlCommand("saveSubPage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Username", DDL_Usr.SelectedValue.Trim());
               // cmd.Parameters.AddWithValue("MenuId", TextBox2.Text);
               // cmd.Parameters.AddWithValue("SubMenuId", TextBox3.Text);
                //cmd.Parameters.AddWithValue("IsAssign", TextBox4.Text);
                con.Open();
                int k = cmd.ExecuteNonQuery();  
                if (k != 0) {
                    lblerr.Text = "Record Inserted Succesfully into the Database";
                    lblerr.ForeColor = System.Drawing.Color.CornflowerBlue;  
                }  
            }
            catch (Exception ex)
            {
                lblerr.Text = ex.Message;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "alert('" + ex.Message + "');", true);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
            return 1;
        }

        public void fillgrid()
        {
            try
            {
                //For Modules
                DataTable dt_ = new DataTable();

                string usrqy = "select * from tbl_Menu";

                dt_ = DBConnection.GetQueryData(usrqy);

                if (dt_.Rows.Count > 0)
                {
                    GVModul.DataSource = dt_;
                    GVModul.DataBind();
                }

                //For Pages

                DataTable dt1_ = new DataTable();

                string submen = "SELECT * FROM [tbl_SubMenu]";

                dt1_ = DBConnection.GetQueryData(submen);

                if (dt1_.Rows.Count > 0)
                {
                    GVSlectPgs.DataSource = dt1_;
                    GVSlectPgs.DataBind();
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        protected void DDL_Usr_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtmen = new DataTable();

                string usrqy = "select * from tbl_menusr inner join tbl_Menu on tbl_menusr.MenuId = tbl_Menu.MenuId where Username= '" + DDL_Usr.SelectedValue.Trim() + "'";

                dtmen = DBConnection.GetQueryData(usrqy);

                if (dtmen.Rows.Count > 0)
                {
                    //GVModul.DataSource = dtmen;
                    //GVModul.DataBind();

                    for (int i = 0; i < GVModul.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtmen.Rows.Count; j++)
                        {
                            CheckBox chk_slct = (CheckBox)GVModul.Rows[i].FindControl("chk_slct");
                            HiddenField HFSlctModul = (HiddenField)GVModul.Rows[i].FindControl("HFSlctModul");

                            HFSlctModul.Value = dtmen.Rows[j]["MenuId"].ToString();

                            if (HFSlctModul.Value != "")
                            {
                                chk_slct.Checked = true;
                            }
                        }
                    }        
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}