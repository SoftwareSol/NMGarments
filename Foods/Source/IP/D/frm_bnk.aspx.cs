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


namespace Foods.Source.IP.D
{
    public partial class frm_bnk : System.Web.UI.Page

    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DBConnection db = new DBConnection();
        string query;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                FillGrid();
            }
        }
        public void FillGrid()
        {
            try
            {
                DataTable dt_ = new DataTable();
                dt_ = DBConnection.GetQueryData("select CashBnk_id, CashBnk_nam from tbl_CashBnk where  CashBnk_nam <> '1'");

                GVArea.DataSource = dt_;
                GVArea.DataBind();
                ViewState["Bank"] = dt_;
            }
            catch (Exception ex)
            {
                //   throw;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                lblalert.Text = ex.Message;
            }
        }
        public void clear()
        {
            TBbnk.Text = "";
            lblArea.Text = "";
            HFbnkid.Value = "";
        }

        private void SearchRecord()
        {
            try
            {
                FillGrid();
                DataTable _dt = (DataTable)ViewState["Bank"];
                DataView dv = new DataView(_dt, "CashBnk_nam LIKE '%" + TBSearchArea.Text.Trim().ToUpper() + "%'", "[CashBnk_nam] ASC", DataViewRowState.CurrentRows);
                DataTable dt_ = new DataTable();
                dt_ = dv.ToTable();
                GVArea.DataSource = dt_;
                GVArea.DataBind();
                ViewState["Bank"] = dt_;
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
                    HFbnkid.Value = GVArea.DataKeys[row.RowIndex].Values[0].ToString();
                    TBbnk.Text = Server.HtmlDecode(row.Cells[1].Text);
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
            {
                int j = 1;

                con.Open();

                SqlCommand command = con.CreateCommand();

                SqlTransaction transaction;

                // Start a local transaction.
                transaction = con.BeginTransaction("ProductionTrans");

                // Must assign both transaction object and connection 
                // to Command object for a pending local transaction
                command.Connection = con;
                command.Transaction = transaction;
                try
                {
                    command.CommandText = "tbl_cashbnka";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Cashbnk_nam", TBbnk.Text);
                    command.Parameters.AddWithValue("@CreatedBy", "Admin");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString());
                    command.Parameters.AddWithValue("@isActive", "True");

                    command.ExecuteNonQuery();
                    /*using (SqlCommand cmd = new SqlCommand("cashbnk", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CashBnk_nam", cashbnk.Text);
                        cmd.Parameters.AddWithValue("@CreatedBY", "Admin");
                        cmd.Parameters.AddWithValue("@Createdat", DateTime.Now.ToString());
                        cmd.Parameters.AddWithValue("@isActivated", "true");
   
                        int k = cmd.ExecuteNonQuery();
                        if (k != 0)
                        {
                            Response.Write("<script>alert('Data inserted successfully')</script>");
                            con.Close();
                        }
                     */
                    transaction.Commit();
                    FillGrid();
                    con.Close();


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
                    //Response.Redirect("frm_Sal.aspx");
                }

                return j;

            }
        }

        private int update()
        {
             int j = 1;

                con.Open();

                SqlCommand command = con.CreateCommand();

                SqlTransaction transaction;

                // Start a local transaction.
                transaction = con.BeginTransaction("ProductionTrans");

                // Must assign both transaction object and connection 
                // to Command object for a pending local transaction
                command.Connection = con;
                command.Transaction = transaction;
                try
                {
                    command.CommandText = "tbl_cashbnkaupdate";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Cashbnk_nam", TBbnk.Text);
                    command.Parameters.AddWithValue("@CreatedBy", "Admin");
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now.ToString());
                    command.Parameters.AddWithValue("@isActive", "True");
                    command.Parameters.AddWithValue("@Cashbnk_id", HFbnkid.Value);

                    command.ExecuteNonQuery();
                    /*using (SqlCommand cmd = new SqlCommand("cashbnk", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CashBnk_nam", cashbnk.Text);
                        cmd.Parameters.AddWithValue("@CreatedBY", "Admin");
                        cmd.Parameters.AddWithValue("@Createdat", DateTime.Now.ToString());
                        cmd.Parameters.AddWithValue("@isActivated", "true");
   
                        int k = cmd.ExecuteNonQuery();
                        if (k != 0)
                        {
                            Response.Write("<script>alert('Data inserted successfully')</script>");
                            con.Close();
                        }
                     */
                    transaction.Commit();
                    FillGrid();
                    con.Close();


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
                    //Response.Redirect("frm_Sal.aspx");
                }

                return j;

            }
        
        protected void BtnCreateArea_Click(object sender, EventArgs e)
        {
            int o;
            con.Close();
            con.Open();
            SqlCommand cm = new SqlCommand("select CashBnk_nam from tbl_CashBnk where CashBnk_nam='" + TBbnk.Text.Trim() + "'", con);
            SqlDataReader dr = cm.ExecuteReader();
            if (dr.Read())
            {
                v_bank.Text = "Already Exist";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "ModalPopUp();", true);
            }
            else if (HFbnkid.Value == "")
            {
                con.Close();
                v_bank.Text = "";
                o = Save();

                if (o == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Bank Has been Saved!..";
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Some thing is Wrong please Contact Administrator!..";
                }

            }
            else
            {
                con.Close();
                v_bank.Text = "";
                o = update();

                if (o == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "Bank Has been Updated!..";
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
            v_bank.Text = "";
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
                HiddenField HFbnkid = (HiddenField)GVArea.Rows[e.RowIndex].FindControl("HFbnkid");


                int h;

                h = delete(HFbnkid.Value);

                if (h == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                    lblalert.Text = "The Bank As been Deleted..";
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
        private int delete(string HFbnkid)
        {
               int j = 1;

                con.Open();

                SqlCommand command = con.CreateCommand();

                SqlTransaction transaction;

                // Start a local transaction.
                transaction = con.BeginTransaction("ProductionTrans");

                // Must assign both transaction object and connection 
                // to Command object for a pending local transaction
                command.Connection = con;
                command.Transaction = transaction;
                try
                {
                    command.CommandText = "tblcashbnkadelete";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Cashbankid", HFbnkid);
                    command.ExecuteNonQuery();
                    /*using (SqlCommand cmd = new SqlCommand("cashbnk", con))
                    {

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CashBnk_nam", cashbnk.Text);
                        cmd.Parameters.AddWithValue("@CreatedBY", "Admin");
                        cmd.Parameters.AddWithValue("@Createdat", DateTime.Now.ToString());
                        cmd.Parameters.AddWithValue("@isActivated", "true");
   
                        int k = cmd.ExecuteNonQuery();
                        if (k != 0)
                        {
                            Response.Write("<script>alert('Data inserted successfully')</script>");
                            con.Close();
                        }
                     */
                    transaction.Commit();
                    FillGrid();
                    con.Close();
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
                    //Response.Redirect("frm_Sal.aspx");
                }
            return 1;
        }
    }
}