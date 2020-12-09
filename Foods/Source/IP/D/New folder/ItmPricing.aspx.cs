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

namespace Foods
{
    public partial class ItmPricing : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                FillGrid();
                BindDll();
            }
            
        }

        public void BindDll()
        {
            try
            {
                DataTable prodt = new DataTable();

                prodt = tbl_ItmPricingManager.getPro();
                DDLProID.DataSource = prodt;
                DDLProID.DataTextField = "ProductName";
                DDLProID.DataValueField = "ProductID";
                DDLProID.DataBind();
                DDLProID.Items.Add(new ListItem("--Select--", "0"));


                DataTable cusdt = new DataTable();

                cusdt = tbl_ItmPricingManager.getCus();
                DDLCusID.DataSource = cusdt;
                DDLCusID.DataTextField = "CustomerName";
                DDLCusID.DataValueField = "CustomerID";
                DDLCusID.DataBind();
                DDLCusID.Items.Add(new ListItem("--Select--", "0"));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void FillGrid()
        {
            try
            {
                DataTable dtGetItmPric_ = new DataTable();

                dtGetItmPric_ = tbl_ItmPricingManager.GetItmPricingsList();

                GVItmPricin.DataSource = dtGetItmPric_;
                GVItmPricin.DataBind();
                ViewState["GetItmPric"] = dtGetItmPric_;
            }
            catch (Exception ex)
            {
                throw;
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);
                //lblalert.Text = ex.Message;
            }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Save();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }
        public void Save()
        {
            try
            {
                tbl_ItmPricing itmpricin= new tbl_ItmPricing();

                itmpricin.ItmPriID = HFItmPriID.Value;
                itmpricin.EffDat = string.IsNullOrEmpty(TBEffDat.Value) ? null : TBEffDat.Value;
                itmpricin.ProductID = string.IsNullOrEmpty(DDLProID.SelectedValue) ? null : DDLProID.SelectedValue;
                itmpricin.CustomerID = string.IsNullOrEmpty(DDLCusID.SelectedValue) ? null : DDLCusID.SelectedValue;
                itmpricin.itmpri_Qty = string.IsNullOrEmpty(TBitmpriQty.Text) ? null : TBitmpriQty.Text;
                itmpricin.unt_cost = string.IsNullOrEmpty(TBuntcost.Text) ? null : TBuntcost.Text;
                itmpricin.cost = string.IsNullOrEmpty(TBCost.Text) ? null : TBCost.Text;                
                itmpricin.crtd_by = Session["user"].ToString();
                itmpricin.crtd_at = DateTime.Today;

                tbl_ItmPricingManager  itmpricinmanag = new tbl_ItmPricingManager(itmpricin);
                itmpricinmanag.Save();

            }
            catch (Exception ex)
            {
                throw ex;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "Alert();", true);

            }
        }
        
    }
    
}

