using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Foods.Source.IP
{
    public partial class rpt_salInv : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
           

            

            if (!IsPostBack)
            {
                this.BindGrid();
                //try
                //{
                //    string query = "select * from v_salinvoice where MSal_id =" + SALID + "";

                //    SqlCommand cmdSlct = new SqlCommand(query, con);
                //    SqlDataAdapter adp = new SqlDataAdapter(cmdSlct);

                //    DataTable dt = new DataTable();
                //    adp.Fill(dt);

                //    if (dt.Rows.Count > 0)
                //    {
                //        lblcust.Text = dt.Rows[0]["customername"].ToString();
                //        lbldat.Text = dt.Rows[0]["CreatedAt"].ToString();
                //        lblbillno.Text = dt.Rows[0]["Msal_sono"].ToString();

                //        GVSal.DataSource = dt;
                //        GVSal.DataBind();
                //    }

                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
            }
        }

        private void BindGrid()
        {
            string SALID;
            SALID = Request.QueryString["SalID"];

            string query = " select ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID],Msal_sono,tbl_MSal.MSal_id,ProductName,DSal_ItmQty,rat,Amt, customername,saleper,  " +
                "  Discount= Amt * (saleper/100), AfterDiscount= Amt - (Amt * (saleper/100)), " +
                "  (Amt - (Amt * (saleper/100)))  Total, convert(date, cast(tbl_MSal.CreatedAt as date) ,103) as [CreatedAt] from tbl_MSal " +
                "  inner join tbl_DSal on tbl_MSal.MSal_id = tbl_DSal.MSal_id inner join Products on tbl_DSal.ProductID = Products.ProductID " +
                "  inner join Customers_ on tbl_MSal.CustomerID = Customers_.CustomerID where tbl_MSal.MSal_id = " + SALID + "";

            string constr = ConfigurationManager.ConnectionStrings["D"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GVSal.DataSource = dt;
                            GVSal.DataBind();

                            //Calculate Sum and display in Footer Row
                            double total = dt.AsEnumerable().Sum(row => row.Field<double>("Total"));
                            double Qty = dt.AsEnumerable().Sum(row => row.Field<double>("DSal_ItmQty"));

                            GVSal.FooterRow.Cells[5].Text = "Total";
                            GVSal.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                            GVSal.FooterRow.Cells[6].Text = total.ToString("N2");

                            GVSal.FooterRow.Cells[1].Text = "Total Qty";
                            GVSal.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                            GVSal.FooterRow.Cells[2].Text = Qty.ToString("N2");
                        }
                    }
                }
            }
        }
        protected void GVSal_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                //float GTotal = 0;

                //string SALID = GVSal.DataKeys[e.Row.RowIndex].Values[0].ToString();

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //    Label lbl_amt = (Label)e.Row.FindControl("lbl_amt");

                   

                }
                //decimal TotalSales = (decimal)0.0;

                if (e.Row.RowType == DataControlRowType.Footer)
                {

                    //TotalSales += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));

                    //Label lbl_ttlamt = (Label)e.Row.FindControl("lbl_ttlamt");
                    ////GTotal += Convert.ToSingle(lbl_amt.Text);
                    //lbl_ttlamt.Text = TotalSales.ToString();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}