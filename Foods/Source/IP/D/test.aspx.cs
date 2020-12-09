using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DataAccess;
using System.Net;
using System.Net.Mail;
using System.Text; 



namespace Foods.Source.IP
{
    public partial class test : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        SqlCommand command;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        private void BindGrid()
        {
            string query = " select ROW_NUMBER() OVER(ORDER BY tbl_MSal.MSal_id DESC) AS [ID],Msal_sono,tbl_MSal.MSal_id,ProductName,DSal_ItmQty,rat,Amt, customername,saleper,  " +
                "  Discount= Amt * (saleper/100), AfterDiscount= Amt - (Amt * (saleper/100)), " +
                "  (Amt - (Amt * (saleper/100)))  Total, convert(date, cast(tbl_MSal.CreatedAt as date) ,103) as [CreatedAt] from tbl_MSal " +
                "  inner join tbl_DSal on tbl_MSal.MSal_id = tbl_DSal.MSal_id inner join Products on tbl_DSal.ProductID = Products.ProductID " +
                "  inner join Customers_ on tbl_MSal.CustomerID = Customers_.CustomerID";
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
                            GridView1.DataSource = dt;
                            GridView1.DataBind();

                            //Calculate Sum and display in Footer Row
                            double total = dt.AsEnumerable().Sum(row => row.Field<double>("Total"));
                            GridView1.FooterRow.Cells[1].Text = "Total";
                            GridView1.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                            GridView1.FooterRow.Cells[2].Text = total.ToString("N2");
                        }
                    }
                }
            }
        }

        #region Email Checking
        protected void btnsendmail_Click(object sender, EventArgs e)
        {
            try
            {
                Email();
            }
            catch (Exception ex)
            {
 
            }
        }

        private void Email()
        {            
            string to = "assadali15@yahoo.com"; //To address    
            string from = "assh1984@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "In this article you will learn how to send a email using Asp.Net & C#";
            message.Subject = "Sending Email Using Asp.Net & C#";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("assh1984@gmail.com", "a22394786");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);

                lblmail.Text = "Email has been send";
            }

            catch (Exception ex)
            {
                lblmail.Text= ex.Message;
            }
        }
        #endregion
    }
}