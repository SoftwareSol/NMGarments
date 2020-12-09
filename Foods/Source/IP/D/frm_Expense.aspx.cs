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

using NHibernate;
using NHibernate.Criterion;
using Foods;
using DataAccess;

namespace Foods
{
    public partial class frm_Expense : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_ = null;
        DBConnection db = new DBConnection();
        int i = 0;
        string query, pass;

        protected void Page_Load(object sender, EventArgs e)
        {
            dropdownlist2();
            filldata();
            dropdownlist();
            //ed.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int a, b = 0;


            if (HFUsrId.Value != "")
            {
                b = Update();

                if (b == 1)
                {
                    Response.Redirect("frm_Expense.aspx");
                }

            }
            else if (HFUsrId.Value == "")
            {
                a = Save();
                if (a == 1)
                {
                    Response.Redirect("frm_Expense.aspx");
                }

            }         
        }
       
        public void filldata()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select * from tbl_expenses", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            con.Close();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetCurr(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select SubHeadCategoriesName from SubHeadCategories where SubHeadCategoriesName like '" + prefixText + "%' and SubHeadGeneratedID='0023'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }
        private int Save()
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
                command.CommandText = "expense";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@acctitle", tb_acct.Text);
                command.Parameters.AddWithValue("@accno", DropDownList2.SelectedValue);             
                command.Parameters.AddWithValue("@billno", bilno.Text);
                command.Parameters.AddWithValue("@expenceaccount", DropDownList1.SelectedValue);
                command.Parameters.AddWithValue("@amountpaid", amtpad.Text);
                command.Parameters.AddWithValue("@expencetitle", dllexptyp.Text);
                command.Parameters.AddWithValue("@expensesdat", bilno.Text);
                command.Parameters.AddWithValue("@expencermk", exprmk.Text);
                command.Parameters.AddWithValue("@createat", DateTime.Now.ToString() );
                command.Parameters.AddWithValue("@createby", "Admin");
                command.Parameters.AddWithValue("@companyid", "com12");
                command.Parameters.AddWithValue("@branchid", "bnc 122");
                command.ExecuteNonQuery();
                
                transaction.Commit();
                Response.Write("<script>alert('Data inserted successfully')</script>");
                con.Close();
                filldata();

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
  private int Update()
        {
            int u = 1;

            pass = "0";

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
                command.CommandText = "expenseupdate";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@acctitle", tb_acct.Text);
                command.Parameters.AddWithValue("@accno", DropDownList2.SelectedValue);
                //command.Parameters.AddWithValue("@expensesdat", ed.Text);
                //command.Parameters.AddWithValue("@billno", bn.Text);
                //command.Parameters.AddWithValue("@amountpaid", ap.Text);
                //command.Parameters.AddWithValue("@expencermk", ep.Text);
                command.Parameters.AddWithValue("@createat", DateTime.Now.ToString());
                command.Parameters.AddWithValue("@createby", "Admin");
                command.Parameters.AddWithValue("@companyid", "com12");
                command.Parameters.AddWithValue("@branchid", "bnc 122");
                 command.Parameters.AddWithValue("@expenceid", HFUsrId.Value);
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
                Response.Write("<script>alert('Data updated successfully')</script>");
                con.Close();
                filldata();

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

            return u;
        }

  protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
  {
      GridViewRow gr = GridView1.SelectedRow;
      HFUsrId.Value = gr.Cells[3].Text;
      
      tb_acct.Text = gr.Cells[5].Text;
      //ed.Text = gr.Cells[6].Text;
      //bn.Text = gr.Cells[7].Text;
      //ap.Text = gr.Cells[8].Text;
      //ep.Text = gr.Cells[9].Text;
     
  }

  protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
  {
      try
      {
          string MPurID = Server.HtmlDecode(GridView1.Rows[e.RowIndex].Cells[3].Text.ToString());


          SqlCommand cmd = new SqlCommand("expence_del", con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.AddWithValue("@expenceid", MPurID);
          con.Open();
          int k = cmd.ExecuteNonQuery();
          if (k != 0)
          {
              Response.Write("<script>alert('Data is deleted successfully')</script>");
              con.Close();
          }

          filldata();

      }
      catch (Exception ex)
      {
          throw ex;
      }
            
  }
        private void dropdownlist()
        {
            SqlCommand cmd = new SqlCommand("select * from SubHeadCategories where HeadGeneratedID='002' and SubHeadGeneratedID='0023'", con);        
        con.Open();
        DataTable dt  = new DataTable();
        dt.Load(cmd.ExecuteReader());
        con.Close();
        DropDownList1.DataSource = dt;
        DropDownList1.DataTextField = "SubHeadCategoriesGeneratedID";
        DropDownList1.DataValueField = "SubHeadCategoriesGeneratedID";
        DropDownList1.DataBind(); 
        }
        private void dropdownlist2()
        {
            SqlCommand cmd = new SqlCommand("select * from SubHeadCategories where HeadGeneratedID='002' and SubHeadGeneratedID='0023'", con);
            con.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            con.Close();
            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "SubHeadCategoriesGeneratedID";
            DropDownList2.DataValueField = "SubHeadCategoriesGeneratedID";
            DropDownList2.DataBind();
        }

  protected void an_TextChanged(object sender, EventArgs e)
  {

  }

  protected void TextBox1_TextChanged(object sender, EventArgs e)
  {

  }

  protected void bn0_TextChanged(object sender, EventArgs e)
  {

  }

  protected void tb_ac_TextChanged(object sender, EventArgs e)
  {

  }




           
    }
}