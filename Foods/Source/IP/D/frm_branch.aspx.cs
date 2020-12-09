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
    public partial class frm_branch : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DBConnection db = new DBConnection();
        string query, str, strname;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Category categor = new Category();

            if (!this.IsPostBack)
            {
                FillGrid();
                BindDll();
                ptnCompNo();
                imgViewFile.ImageUrl = "~/img/blank-img.jpg";
            }

        }
        public void clear()
        {
            TBBranch.Text = "";        }

        private void ptnCompNo()
        {
            try
            {

                str = "select isnull(max(cast(BranchId as int)),0) as [BranchId]  from tbl_branches";// where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    if (string.IsNullOrEmpty(lbl_BrnID.Text))
                    {
                        int v = Convert.ToInt32(reader["BranchId"].ToString());
                        int b = v + 1;
                        lbl_BrnID.Text = "00" + b.ToString();
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

        public void BindDll()
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = " select CompanyId,  Name  from tbl_Companies where ISActive = 'True' and CompanyId='"+ Session["CompanyID"] + "'";

                cmd.Connection = con;
                con.Open();

                DataTable dt_ = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt_);

                ddl_comp.DataSource = dt_;
                ddl_comp.DataTextField = "Name";
                ddl_comp.DataValueField = "CompanyId";
                ddl_comp.DataBind();
                ddl_comp.Items.Insert(0, new ListItem("--Select--", "0"));


                con.Close();
            }
        }

        public void FillGrid()
        {
            try
            {
                DataTable dt_ = new DataTable();
                //dt_ = DBConnection.GetQueryData("select rtrim('[' + CAST(ProductTypeID AS VARCHAR(200)) + ']-' + ProductTypeName ) as [ProductTypeName], ProductTypeID from tbl_branches");
                dt_ = DBConnection.GetQueryData("select tbl_branches.CompanyId, BranchId, tbl_branches.Name as [Branch], tbl_Companies.Name as [Company] from tbl_branches inner join tbl_Companies on tbl_Companies.CompanyId = tbl_branches.CompanyId");

                GVBranch.DataSource = dt_;
                GVBranch.DataBind();
                ViewState["Branch"] = dt_;
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
                DataTable _dt = (DataTable)ViewState["Branch"];
                DataView dv = new DataView(_dt, "Name LIKE '%" + TBSearchBranch.Text.Trim().ToUpper() + "%'", "[Name] ASC", DataViewRowState.CurrentRows);
                DataTable dt_ = new DataTable();
                dt_ = dv.ToTable();
                GVBranch.DataSource = dt_;
                GVBranch.DataBind();
                ViewState["Branch"] = dt_;
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

        protected void GVfrm_branchRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "ModalPopUp();", true);

                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    HFBranch.Value = GVBranch.DataKeys[row.RowIndex].Values[1].ToString();

                    DataTable dt_ = new DataTable();

                    //dt_ = DBConnection.GetQueryData("select * from tbl_branches where  BranchId ='" + HFBranch.Value + "' and CompanyId='" + Session["CompanyID"] + "'");
                    dt_ = DBConnection.GetQueryData("select * from tbl_branches where  BranchId ='" + HFBranch.Value + "'");
                    ddl_comp.SelectedValue = dt_.Rows[0]["CompanyId"].ToString();
                    lbl_BrnID.Text = dt_.Rows[0]["BranchId"].ToString();
                    TBBranch.Text = dt_.Rows[0]["Name"].ToString();
                    imgViewFile.ImageUrl = "~/img/branchlogo/" + dt_.Rows[0]["logo"].ToString();
                    TbAdd.Text = dt_.Rows[0]["Address"].ToString();
                    TBContctPer.Text = dt_.Rows[0]["ContactPerson"].ToString();
                    TB_TelNo.Text = dt_.Rows[0]["TelephoneNo"].ToString();
                    TBMobNo.Text = dt_.Rows[0]["MobileNo"].ToString();
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

            try
            {
                if (FUp_logo.HasFile)
                {
                    strname = FUp_logo.FileName.ToString();
                    FUp_logo.PostedFile.SaveAs(Server.MapPath("~/img/branchlogo/") + strname);
                    imgViewFile.ImageUrl = Server.MapPath("~/img/branchlogo/" + FUp_logo.FileName);

                    query = " INSERT INTO tbl_branches " +
                              " (CompanyId,BranchId,Name,Address,ContactPerson,TelephoneNo,MobileNo,IsActive,CreateBy,CreateDate,CreateTerminal,logo) VALUES('"
                              + ddl_comp.SelectedValue.Trim() + "','" + lbl_BrnID.Text + "','" + TBBranch.Text.Trim() + "','" + TbAdd.Text + "','" + TBContctPer.Text + "','" + TB_TelNo.Text + "','" + TBMobNo.Text+ "','" + ck_act.Checked + "','" + Session["user"].ToString() +
                              " ','" + DateTime.Now + "','::1','" + strname + "')";
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
           
            return j;
        }

        private int update()
        {
            int k = 1;
            try
            {
                con.Open();
                if (FUp_logo.HasFile)
                {
                    strname = FUp_logo.FileName.ToString();
                    FUp_logo.PostedFile.SaveAs(Server.MapPath("~/img/branchlogo/") + strname);
                    imgViewFile.ImageUrl = Server.MapPath("~/img/branchlogo/" + FUp_logo.FileName);

                    query = " update tbl_branches set Name = '" + TBBranch.Text + "', Address='" + TbAdd.Text + "', ContactPerson ='" + TBContctPer.Text + "', TelephoneNo ='" + TB_TelNo.Text + "', MobileNo='" + TBMobNo.Text + "', IsActive ='" + ck_act.Checked + "' , UpdateBy ='" + Session["user"].ToString() + "', UpdateDate='" + DateTime.Now + "', logo ='" + strname + "' where  BranchId='" + lbl_BrnID.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "'";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    query = " update tbl_branches set Name = '" + TBBranch.Text + "', Address='" + TbAdd.Text + "', ContactPerson ='" + TBContctPer.Text + "', TelephoneNo ='" + TB_TelNo.Text + "', MobileNo='" + TBMobNo.Text + "', IsActive ='" + ck_act.Checked + "' , UpdateBy ='" + Session["user"].ToString() + "', UpdateDate='" + DateTime.Now + "' where  BranchId='" + lbl_BrnID.Text.Trim() + "' and CompanyId='" + Session["CompanyID"] + "'";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }   
                    con.Close();
               
            }
            catch (Exception ex)
            { 
            }
            return k;
        }
        protected void BtnCreateBranch_Click(object sender, EventArgs e)
        {
            int o;

            if (HFBranch.Value == "")
            {   
                o = Save();

                if (o == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Branch Has been Saved!..";
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
                   lblalert.Text = "Branch Has been Updated!..";
               }
               else
               {
                   ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                   lblalert.Text = "Some thing is Wrong please Contact Administrator!..";
               }
            }
            //Category catgory = new Category();


            //catgory.CategoryId = HFBranch.Value;
            //catgory.Categorytype = string.IsNullOrEmpty(TBBranch.Text) ? null : TBBranch.Text;
            //catgory.Subtype = string.IsNullOrEmpty(TBSubType.Text) ? null : TBSubType.Text;
            //CategoryManager catmanager = new CategoryManager(catgory);
            //catmanager.Save();
            clear();
            FillGrid();
            
        }

        protected void BtnCancelBranch_Click(object sender, EventArgs e)
        {
            Response.Redirect("frm_branch.aspx");
        }

        protected void GVfrm_branchPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            GVBranch.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void GVfrm_branchRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                HiddenField HFBranchID = (HiddenField)GVBranch.Rows[e.RowIndex].FindControl("HFBranchID");


                int h;

                h = delete(HFBranchID.Value);

                if (h == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "The Branch As been Deleted..";
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

        private int delete(string HFBranchID)
        {

            string sqlquery = "Delete from tbl_branches where BranchId = '" + HFBranchID + "'";
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