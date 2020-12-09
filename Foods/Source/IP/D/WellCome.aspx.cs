using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using Foods;
using DataAccess;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;



namespace Foods
{
    public partial class WellCome : System.Web.UI.Page
    {
        SqlConnection con = DataAccess.DBConnection.connection();
        DataTable dt_ = null;
        int i = 0;
        string query;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //Get User Info
                    lblUserName.Text = Session["user"].ToString();
                    lbl_compnam.Text = Session["Company"].ToString();
                    
                    pnl_curr.Visible = false;

                    int chk = Convert.ToInt32(Session["Level"]);

                    if (chk != 1)
                    {
                        pnlchkusr.Visible = false;
                        pnlchkpro.Visible = false;
                        pnlDayEnd.Visible = false;
                        pnlsalchart.Visible = false;
                    }
                    else if (chk == 1)
                    {
                        pnlchkusr.Visible = true;
                        pnlchkpro.Visible = true;
                        pnlDayEnd.Visible = true;
                        pnlsalchart.Visible = true;
                    }

                    //Binding repeater

                    LessItmData();
                    //Binding Charts
                    
                    BindChart();

                    //Bind Dropdown list

                    BINDDLL();
                }
                catch
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        private void BINDDLL()
        {
            query = " select * from tbl_Curr where IsActive = 1 ";
            DataTable dt_Curr = new DataTable();

            dt_Curr = DBConnection.GetQueryData(query);

            if (dt_Curr.Rows.Count > 0)
            {
                DDL_Curr.DataSource = dt_Curr;
                DDL_Curr.DataTextField = "Curr_nam";
                DDL_Curr.DataValueField = "Curr_id";
                DDL_Curr.DataBind();
                DDL_Curr.Items.Insert(0, new ListItem("--Select Currency--", "0"));
            }
        }

        public void LessItmData()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(" select tbl_Dstk.ProductID,ProductName,Dstk_Qty from tbl_Mstk inner join tbl_Dstk on tbl_Mstk.Mstk_id = tbl_Dstk.Mstk_id inner join Products on tbl_Dstk.ProductID = Products.ProductID where Dstk_Qty < 5 and tbl_Mstk.CompanyId='" + Session["CompanyID"] + "' and tbl_Mstk.BranchId='" + Session["BranchID"] + "'", con);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);

            RepterDetails.DataSource = ds;
            RepterDetails.DataBind();

            con.Close();
        }

        protected void BindChart()
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            con.Open();

            // string cmdstr = "select top 6 DSal_id, SUM(GTtl) [Total Suppliers] from [tbl_DSal] group by DSal_id";
            //string cmdstr = " select  MSal_dat, SUM(GTtl) as [Total Suppliers]  from tbl_MSal inner join tbl_DSal on tbl_MSal.MSal_id =tbl_DSal.MSal_id where tbl_MSal.MSal_dat between '2019/05/01' and '2019/05/27' group by MSal_dat  ";
            string cmdstr = " select (CONVERT (VARCHAR(4), YEAR(MSal_dat)) + '/' + CONVERT (VARCHAR(2), MONTH(MSal_dat))+ '/' + CONVERT (VARCHAR(2), day(MSal_dat)))  as [MSal_dat], sum(GTtl) as [Total]  from tbl_MSal inner join tbl_DSal on tbl_MSal.MSal_id =tbl_DSal.MSal_id where month(MSal_dat) = month(getdate()) and tbl_MSal.CompanyId='" + Session["CompanyID"] + "' and tbl_MSal.BranchId='" + Session["BranchID"] + "'  group by MSal_dat ";
            SqlDataAdapter adp = new SqlDataAdapter(cmdstr, con);

            adp.Fill(ds);

            dt = ds.Tables[0];

            string[] x = new string[dt.Rows.Count];

            //int[] y = new int[dt.Rows.Count];

            int[] y = new int[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                x[i] = dt.Rows[i][0].ToString();

                y[i] = Convert.ToInt32(dt.Rows[i][1]);

            }

            Chart1.Series[0].Points.DataBindXY(x, y);

            Chart1.Series[0].ChartType = SeriesChartType.Column;

            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

            Chart1.Legends[0].Enabled = true;
            Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = -50;
            Chart1.ChartAreas["ChartArea1"].AxisX.TitleFont = new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold);
            Chart1.ChartAreas["ChartArea1"].AxisY.TitleFont = new System.Drawing.Font("Verdana", 8, System.Drawing.FontStyle.Bold);
            Chart1.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            Chart1.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = true;
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#e5e5e5");
            Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = true;
            Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#e5e5e5");
            Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Font = new Font("Tahoma", 8, FontStyle.Bold);
            Chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Font = new Font("Tahoma", 8, FontStyle.Bold);
            //Chart1.Series[0].IsValueShownAsLabel = true;
            Chart1.ChartAreas["ChartArea1"].AxisY.Title = "Amount";
            Chart1.ChartAreas["ChartArea1"].AxisX.Title = "Month";
            Chart1.Width = 500;

        }

        protected void btnsav_Click(object sender, EventArgs e)
        {
            query = "INSERT INTO [dbo].[Products] ([ProductTypeID] ,[ProductName] ,[CreatedBy] ,[CreatedAt]) VALUES ('1' , 'q mobile' , '120' , '100' , 'Q mobile' , '' , 'piece' , '3' , 'admin' , '12/11/2018' , '', '1')";


            con.Open();

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                //cmd.Parameters.AddWithValue("@ProductTypeID", textbox2.text);
                //cmd.Parameters.AddWithValue("@ProductName", textbox3.text);
                //cmd.Parameters.AddWithValue("@PckSize", textbox2.text);
                //cmd.Parameters.AddWithValue("@Cost", textbox3.text);
                //cmd.Parameters.AddWithValue("@ProductDiscriptions", textbox2.text);
                //cmd.Parameters.AddWithValue("@Supplier_Customer", textbox3.text);
                //cmd.Parameters.AddWithValue("@Unit", textbox2.text);
                //cmd.Parameters.AddWithValue("@ProductType", textbox3.text);
                //cmd.Parameters.AddWithValue("@CreatedBy", textbox2.text);
                //cmd.Parameters.AddWithValue("@CreatedAt", textbox2.text);
                //cmd.Parameters.AddWithValue("@Pro_Code", textbox2.text);
                //cmd.Parameters.AddWithValue("@IsActive", textbox2.text);


                cmd.ExecuteNonQuery();

            }
            con.Close();

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // dtCustCat = DBConnection.GetQueryData("");
                string dbname = "D:\\HumBros_" + DateTime.Now.ToString("MM-dd-yyyy") + ".bak";
                query = "BACKUP DATABASE [HumBros] TO  DISK = '" + dbname + "'";

                con.Open();


                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
               lbl_err.Text= ex.Message;
            }
            finally
            {
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "alert('Data Back up has been Created Please Verify by checking your D Drive!!!...');", true);
            }
        }

        protected void DDL_Curr_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                query = " select * from tbl_Curr where IsActive = 1 and Curr_id= '" + DDL_Curr.SelectedValue.Trim() + "'";

                DataTable dt_ = new DataTable();

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    TBCurrRat.Text = dt_.Rows[0]["Currency_Rate"].ToString();
                    TBExchgRat.Text = dt_.Rows[0]["Exchange_Rat"].ToString();
                }
            }
            catch (Exception ex)
            {
                lbl_err.Text = ex.Message;
            }
        }

        protected void btn_CurrSav_Click(object sender, EventArgs e)
        {
            try
            {
                query = " Update tbl_Curr set Currency_Rate='" +TBCurrRat.Text.Trim() + "' , Exchange_Rat='" + TBExchgRat.Text.Trim() + "' where Curr_id ='" + DDL_Curr.SelectedValue.Trim() + "'";
                
                con.Open();


                using (SqlCommand cmd = new SqlCommand(query, con))
                {

                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                lbl_err.Text= ex.Message;
            }
            finally
            {
                con.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "alert('Currency Rate and Exchange Rate has been Updated!');", true);
            }

        }

        protected void lnkbilsum_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/rpt_BillSumm.aspx?ID=PR&PURID=arif','_blank','height=600px,width=600px,scrollbars=1');", true);
        }
    }
}