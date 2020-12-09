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
    public partial class Reporting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {
                    try
                    {
                        lbl_usr.Text = Session["Name"].ToString();
                        ddl_rpttyp.Visible = false;
                        lbl_mssg.Text = "";
                        Data();
                        Profit.Visible = false;
                        Credit.Visible = false;
                        DailySales.Visible = false;                
                    }
                    
                    catch { Response.Redirect("~/Login.aspx"); }
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        public void Data()
        {
            DataTable dt_ = new DataTable();

            dt_.Columns.AddRange(new DataColumn[] { new DataColumn("Name"), new DataColumn("ID") });
            dt_.Clear();


            dt_.Rows.Add("Daily Sales", "DSal");
            dt_.Rows.Add("Credit", "Cre");
            dt_.Rows.Add("Profit", "PRO");
            
            ddl_rpttyp.DataSource = dt_;
            ddl_rpttyp.DataTextField = "Name";
            ddl_rpttyp.DataValueField = "ID";
            ddl_rpttyp.DataBind();
            //ddl_rpttyp.Items.Insert(0, new ListItem("--Select Report Type--", "0"));
        }

        protected void lnkbtn_Logout_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("~/Login.aspx");
        }

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod]
        public static List<string> GetCustMob(string prefixText)
        {
            SqlConnection con = DataAccess.DBConnection.connection();
            SqlDataAdapter da;
            DataTable dt;
            DataTable Result = new DataTable();
            string str = "select CellNo1 from Customers_ where CellNo1 like '" + prefixText + "%'";
            da = new SqlDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            List<string> Output = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
                Output.Add(dt.Rows[i][0].ToString());
            return Output;
        }

        protected void btn_View_Click(object sender, EventArgs e)
        {
            try
            {
                string id;
                id = ddl_rpttyp.SelectedValue.Trim();

                switch (id)
                {
                    case "DSal":
                        if (TBFDWise.Text != "" && TBTDWise.Text != "")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/RReportViewer.aspx?ID=SAL&frmDat=" + TBFDWise.Text.Trim() + "&toDat=" + TBTDWise.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/RReportViewer.aspx?ID=SAL','_blank','height=600px,width=600px,scrollbars=1');", true);
                        }
                        break;
                    case "Cre":

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/RReportViewer.aspx?ID=CRE&MNO=" + TBMobNo.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                        break;
                    case "PRO":

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "onclick", "javascript:window.open( 'Reports/RReportViewer.aspx?ID=PRO&SPRO=" + TBPFrmdat.Text.Trim() + "&EPRO=" + TBPTrmdat.Text.Trim() + "','_blank','height=600px,width=600px,scrollbars=1');", true);

                        break;
                }
            }
            catch (Exception ex)
            {
                lbl_mssg.Text = ex.Message;
            }
        }

        protected void lnlDSR_Click(object sender, EventArgs e)
        {
            ddl_rpttyp.SelectedValue = "DSal";
            Profit.Visible = false;
            Credit.Visible = false;
            DailySales.Visible = true;
        }
        protected void lnkbtnCreditReports_Click(object sender, EventArgs e)
        {
            ddl_rpttyp.SelectedValue = "Cre";         
            Profit.Visible = false;
            Credit.Visible = true;
            DailySales.Visible = false;
        }
        protected void lnkbtnprofshet_Click(object sender, EventArgs e)
        {
            ddl_rpttyp.SelectedValue = "PRO";
            Profit.Visible = true;
            Credit.Visible = false;
            DailySales.Visible = false;

        }
    }
}