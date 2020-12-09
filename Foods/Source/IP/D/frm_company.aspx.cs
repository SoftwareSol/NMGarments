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

using NHibernate;
using NHibernate.Criterion;
using Foods;
using DataAccess;

namespace Foods
{
    public partial class frm_company : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DBConnection db = new DBConnection();
        string query, str;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Category categor = new Category();

            if (!this.IsPostBack)
            {
                FillGrid();
                ptnCompNo();
            }

        }
        public void clear()
        {
            TBCompany.Text = "";
            lbl_Company.Text = "";
        }

        private void ptnCompNo()
        {
            try
            {

                str = "select isnull(max(cast(CompanyId as int)),0) as [CompanyId]  from tbl_Companies";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(lbl_CompID.Text))
                    {
                        int v = Convert.ToInt32(reader["CompanyId"].ToString());
                        int b = v + 1;
                        lbl_CompID.Text = "COM_00"+b.ToString();
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
                DataTable dt_ = new DataTable();
                //dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(ProductTypeID AS VARCHAR(200)) + ']-' + ProductTypeName ) as [ProductTypeName], ProductTypeID from tbl_Companies");
                dt_ = DBConnection.GetQueryData("select CompanyId, Name from tbl_Companies");

                GVCompany.DataSource = dt_;
                GVCompany.DataBind();
                ViewState["Company"] = dt_;
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        private void SearchRecord()
        {
            try
            {
                FillGrid();
                DataTable _dt = (DataTable)ViewState["Company"];
                DataView dv = new DataView(_dt, "Name LIKE '%" + TBSearchCompany.Text.Trim().ToUpper() + "%'", "[Name] ASC", DataViewRowState.CurrentRows);
                DataTable dt_ = new DataTable();
                dt_ = dv.ToTable();
                GVCompany.DataSource = dt_;
                GVCompany.DataBind();
                ViewState["Company"] = dt_;
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        protected void SeacrhBtn_Click(object sender, EventArgs e)
        {
            SearchRecord();
        }

        protected void GVfrm_companyRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "ModalPopUp();", true);

                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    HFCompany.Value = GVCompany.DataKeys[row.RowIndex].Values[0].ToString();

                    DataTable dt_ = new DataTable();

                    dt_ = DBConnection.GetQueryData("select * from tbl_Companies where  CompanyId ='"+ HFCompany.Value +"' and IsActive = 'True'");

                    lbl_CompID.Text = dt_.Rows[0]["CompanyId"].ToString();
                    TBCompany.Text = dt_.Rows[0]["Name"].ToString();
                    TbAdd.Text = dt_.Rows[0]["Address"].ToString();
                    TBContctPer.Text = dt_.Rows[0]["ContactPerson"].ToString();
                    TB_TelNo.Text = dt_.Rows[0]["TelephoneNo"].ToString();
                    ck_act.Checked =Convert.ToBoolean(dt_.Rows[0]["IsActive"].ToString());
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
        
        }

        private int Save()
        {
            int j = 1;
            query = " INSERT INTO tbl_Companies " +
                           " (CompanyId,Name,Address,ContactPerson,TelephoneNo,IsActive,CreateBy,CreateTime,CreateTerminal) VALUES('"
                           + lbl_CompID.Text+"','"+ TBCompany.Text.Trim() + "','" + TbAdd.Text + "','"+ TBContctPer.Text +"','" + TB_TelNo.Text +"','"+ck_act.Checked+ "','"+ Session["user"].ToString() +
                           " ','" + DateTime.Now + "','::1')";
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
            query = " update tbl_Companies set Name = '" + TBCompany.Text + "', Address='" + TbAdd.Text + "', ContactPerson='" + TBContctPer.Text + "', TelephoneNo='" + TB_TelNo.Text + "', IsActive ='" + ck_act.Checked + "' , CreateBy='" + Session["user"].ToString() + "', CreateTime='" + DateTime.Now + "' where  CompanyId='" + lbl_CompID.Text.Trim() + "'";

            con.Open();

            using (SqlCommand cmd = new SqlCommand(query, con))
            {

                cmd.ExecuteNonQuery();

            }
            con.Close();
            return k;
        }
        protected void BtnCreateCompany_Click(object sender, EventArgs e)
        {
            int o;

            if (HFCompany.Value == "")
            {   
                o = Save();

                if (o == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Company Has been Saved!..";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Some thing is Wrong please Contact Administrator!..";
                }

            }
            else
            {
               o = update();

               if (o == 1)
               {
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                   lblalert.Text = "Company Has been Updated!..";
               }
               else
               {
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                   lblalert.Text = "Some thing is Wrong please Contact Administrator!..";
               }
            }
            //Category catgory = new Category();


            //catgory.CategoryId = HFCompany.Value;
            //catgory.Categorytype = string.IsNullOrEmpty(TBCompany.Text) ? null : TBCompany.Text;
            //catgory.Subtype = string.IsNullOrEmpty(TBSubType.Text) ? null : TBSubType.Text;
            //CategoryManager catmanager = new CategoryManager(catgory);
            //catmanager.Save();
            clear();
            FillGrid();
            
        }

        protected void BtnCancelCompany_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void GVfrm_companyPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            GVCompany.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void GVfrm_companyRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                HiddenField HFCompanyID = (HiddenField)GVCompany.Rows[e.RowIndex].FindControl("HFCompanyID");


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

        private int delete(string HFCompanyID)
        {

            string sqlquery = "Delete from tbl_Companies where CompanyId = '" + HFCompanyID + "'";
            SqlCommand cmd = new SqlCommand(sqlquery, con);

            con.Open();
            cmd.ExecuteNonQuery();
            FillGrid();
            con.Close();
            clear();

             return 1;
        }
    }
}