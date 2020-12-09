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
    public partial class frm_Area : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DBConnection db = new DBConnection();
        string query;


        protected void Page_Load(object sender, EventArgs e)
        {
            //Area categor = new Area();

            if (!this.IsPostBack)
            {
                FillGrid();
            }

        }
        public void clear()
        {
            TBArea.Text = "";
            lblArea.Text = "";
            HFArea.Value = "";
        }

        public void FillGrid()
        {
            try
            {
                DataTable dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData("select areaid, area_ from tbl_area where CompanyId='" + Session["CompanyID"] + "' and BranchId = '" + Session["BranchID"] + "'");

                GVArea.DataSource = dt_;
                GVArea.DataBind();
                ViewState["Area"] = dt_;
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
                DataTable _dt = (DataTable)ViewState["Area"];
                DataView dv = new DataView(_dt, "area_ LIKE '%" + TBSearchArea.Text.Trim().ToUpper() + "%'", "[area_] ASC", DataViewRowState.CurrentRows);
                DataTable dt_ = new DataTable();
                dt_ = dv.ToTable();
                GVArea.DataSource = dt_;
                GVArea.DataBind();
                ViewState["Entity"] = dt_;
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

        protected void GVArea_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "ModalPopUp();", true);

                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    HFArea.Value = GVArea.DataKeys[row.RowIndex].Values[0].ToString();
                    TBArea.Text = Server.HtmlDecode(row.Cells[1].Text);
                }
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {

        }

        private int Save()
        {
            try
            {
                int j = 1;
                query = " INSERT INTO tbl_area " +
                               " (area_,CompanyId,BranchId) VALUES('" + TBArea.Text.Trim() + "','" + Session["CompanyID"] + "','" + Session["BranchID"] + "')";
                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.ExecuteNonQuery();

                }
                con.Close();

                return j;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int update()
        {
            try
            {
                int k = 1;
                query = " update tbl_area set area_ = '" + TBArea.Text + "', CompanyId='" + Session["CompanyID"].ToString() + "', BranchId='" + Session["BranchID"].ToString() + "' where  areaid='" + HFArea.Value + "'";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.ExecuteNonQuery();

                }
                con.Close();
                return k;
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
        protected void BtnCreateArea_Click(object sender, EventArgs e)
        {
            int o;

            if (HFArea.Value == "")
            {
                o = Save();

                if (o == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Area Has been Saved!..";
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
                    lblalert.Text = "Area Has been Updated!..";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Some thing is Wrong please Contact Administrator!..";
                }
            }
            //Area catgory = new Area();


            //catgory.AreaId = HFArea.Value;
            //catgory.Areatype = string.IsNullOrEmpty(TBArea.Text) ? null : TBArea.Text;
            //catgory.Subtype = string.IsNullOrEmpty(TBSubType.Text) ? null : TBSubType.Text;
            //AreaManager catmanager = new AreaManager(catgory);
            //catmanager.Save();
            clear();
            FillGrid();

        }

        protected void BtnCancelArea_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void GVArea_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVArea.PageIndex = e.NewPageIndex;
            FillGrid();
        }

        protected void GVArea_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                HiddenField HFAreaID = (HiddenField)GVArea.Rows[e.RowIndex].FindControl("HFAreaID");


                int h;

                h = delete(HFAreaID.Value);

                if (h == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "The Area As been Deleted..";
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

        private int delete(string HFAreaID)
        {

            string sqlquery = "Delete from tbl_area where areaid = '" + HFAreaID + "'";
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