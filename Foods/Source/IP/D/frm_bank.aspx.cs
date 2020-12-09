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
using DataAccess;
using NHibernate;
using NHibernate.Criterion;
using Foods;

namespace Foods
{
    public partial class frm_bank : System.Web.UI.Page
    {
        SqlConnection con = DBConnection.connection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                FillGrid();
            }
        }

        public void clear()
        {
            TBBnk.Text = "";
            lbl_Bnk.Text = "";
            TBBakShrtNam.Text = "";
            HFBnk.Value = "";
        }


        public void FillGrid()
        {
            //try
            //{
            //    DataTable dt_ = new DataTable();
            //    dt_ = BankManager.GetBankList();
            //    GVBnk.DataSource = dt_;
            //    GVBnk.DataBind();
            //    ViewState["Bank"] = dt_;
            //}
            //catch (Exception ex)
            //{
            //    //   throw;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
            //    lblalert.Text = ex.Message;
            //}
        }


        private void SearchRecord()
        {
            try
            { 
                FillGrid();
                DataTable _dt = (DataTable)ViewState["Bank"];
                DataView dv = new DataView(_dt, "Bank_Name LIKE '%" + TBSearchBnk.Text.Trim().ToUpper() + "%'", "[Bank_Name] ASC", DataViewRowState.CurrentRows);
                DataTable dt_ = new DataTable();
                dt_ = dv.ToTable();
                GVBnk.DataSource = dt_;
                GVBnk.DataBind();
                ViewState["Bnk"] = dt_;
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

        protected void lnkDelete_Click(object sender, EventArgs e)
        {

        }

        protected void BtnBnk_Click(object sender, EventArgs e)
        {
            try
            {
                int c = 0;

                c = Save();
                if (c == 1)
                {
                    clear();
                    FillGrid();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Bank has been Saved!";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Some Thing is wrong Please contact Administration!";
                }
            
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message.ToString();
            }
        }

        protected void BtnBnkCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        private int Save()
        {
            int b = 1;

            //Bank bank = new Bank();
            
            //bank.Bank_ID = HFBnk.Value; 
            //bank.Bank_Name = string.IsNullOrEmpty(TBBnk.Text) ? null : TBBnk.Text;
            //bank.Bank_Short_Name = string.IsNullOrEmpty(TBBakShrtNam.Text) ? null : TBBakShrtNam.Text; 
            //bank.COAID = "";
            //bank.Created_By_ID = Session["user"].ToString(); 
            //bank.Created_Date = DateTime.Now;
            //bank.Modified_By_ID = Session["user"].ToString();
            //bank.Modified_Date = DateTime.Now;
            //bank.IsActive = chkact.Checked.ToString();

            //BankManager bankmanager = new BankManager(bank);
            //bankmanager.Save();
        
            return b;

        }
        private int del(string BnkID)
        {
            int i = 1;
            try
            {
                string sqlquery = "Delete from Bank where Bank_ID = '" + BnkID + "'";
                SqlCommand cmd = new SqlCommand(sqlquery, con);

                con.Open();
                cmd.ExecuteNonQuery();
                FillGrid();
                con.Close();
                clear();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message.ToString();
            }
            return i;
        }

        protected void GVBnk_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                HiddenField HFBnkID = (HiddenField)GVBnk.Rows[e.RowIndex].FindControl("HFBnkID");

                //string sql = "select frm_bankid from tbl_frm_bank where frm_bankid = '" + HFBnkID.Value + "'";

                //SqlCommand checkcmd = new SqlCommand(sql, con);
                //DataTable dtcheck = new DataTable();
                //SqlDataAdapter adp = new SqlDataAdapter(checkcmd);
                //adp.Fill(dtcheck);

                //if (dtcheck.Rows.Count == 0)
                //{
                    int j = 0;

                    j = del(HFBnkID.Value);
                    
                    if (j == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                        lblalert.Text = "Bank Info has been Deleted!";
                    }
                //}
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Sorry This Data Can not be Deleted Please Contact Administrator..";
                }

            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;

            }

        }

        protected void GVBnk_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Select")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "ModalPopUp();", true);
                    
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    HFBnk.Value = GVBnk.DataKeys[row.RowIndex].Values[0].ToString();
                    TBBnk.Text = Server.HtmlDecode(row.Cells[1].Text);
                    TBBakShrtNam.Text = Server.HtmlDecode(row.Cells[2].Text);
                }
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }

        protected void GVBnk_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVBnk.PageIndex = e.NewPageIndex;
            FillGrid();
        }
    }
}