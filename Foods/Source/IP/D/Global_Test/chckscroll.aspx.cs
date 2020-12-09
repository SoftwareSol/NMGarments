using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DataAccess;

namespace Foods.Source.IP.D.Global_Test
{
    public partial class chckscroll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetInitRowPuritm();

                tbf.Focus();
            }
        }

        protected void tbf_TextChanged(object sender, EventArgs e)
        {
            TextBox4.Focus();
        }

        private void SetInitRowPuritm()
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("Category", typeof(string)));
            dt.Columns.Add(new DataColumn("Description", typeof(string)));
            dt.Columns.Add(new DataColumn("Sizes", typeof(string)));
            dt.Columns.Add(new DataColumn("Brand", typeof(string)));
            dt.Columns.Add(new DataColumn("Origin", typeof(string)));
            dt.Columns.Add(new DataColumn("PackingSize", typeof(string)));
            dt.Columns.Add(new DataColumn("Rate", typeof(string)));
            dt.Columns.Add(new DataColumn("Qty", typeof(string)));
            dt.Columns.Add(new DataColumn("Unit", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Purchase Rate", typeof(string)));
            dt.Columns.Add(new DataColumn("Sale Rate", typeof(string)));
            dt.Columns.Add(new DataColumn("Particulars", typeof(string)));
            dt.Columns.Add(new DataColumn("Debit Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("Credit Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("NetTotal", typeof(string)));
            dt.Columns.Add(new DataColumn("DPurID", typeof(string)));

            dr = dt.NewRow();

            dr["Category"] = string.Empty;
            dr["Description"] = string.Empty;
            dr["Sizes"] = string.Empty;
            dr["Brand"] = "";
            dr["Origin"] = "";
            dr["PackingSize"] = "";
            dr["Rate"] = "0.00";
            dr["Qty"] = "0.00";
            dr["Unit"] = "";
            dr["Amount"] = "0.00";
            dr["Purchase Rate"] = "0.00";
            dr["Sale Rate"] = "0.00";
            dr["Particulars"] = string.Empty;
            dr["Debit Amount"] = "0.00";
            dr["Credit Amount"] = "0.00";
            dr["NetTotal"] = "0.00";
            dr["DPurID"] = "0";

            dt.Rows.Add(dr);

            //Store the DataTable in ViewState
            ViewState["dt_adItms"] = dt;

            GVPurItems.DataSource = dt;
            GVPurItems.DataBind();
        }

        protected void TBcat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string query;
                DataTable dt_ = new DataTable();

                for (int i = 0; i < GVPurItems.Rows.Count; i++)
                {
                    TextBox TBcat = (TextBox)GVPurItems.Rows[i].Cells[0].FindControl("TBcat");
                    TextBox TbItmDscptin = (TextBox)GVPurItems.Rows[i].Cells[0].FindControl("TbItmDscptin");

                    query = "select * from tbl_producttype where ProductTypeName='"+ TBcat.Text.Trim() + "'";

                    dt_ = DBConnection.GetQueryData(query);

                    if (dt_.Rows.Count > 0)
                    {
                        //Do Noting

                        TbItmDscptin.Focus();
                    }
                    else
                    {
                        query = " select top 1 ProductTypeID as [ProductTypeID]  from tbl_producttype where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' order by ProductTypeID desc ";

                        dt_ = DBConnection.GetQueryData(query);

                        string ProductTypeID = dt_.Rows[0]["ProductTypeID"].ToString();

                        
                    }

                    Session["cat"] = TBcat.Text.Trim();

                    TbItmDscptin.Focus();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}