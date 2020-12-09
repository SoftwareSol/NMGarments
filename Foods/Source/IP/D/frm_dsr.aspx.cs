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
    public partial class frm_dsr : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        string DSR, DAT, Cust, Usr;
        DBConnection db = new DBConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
        }

        public void FillGrid()
        {
            try
            {
                DSR = Request.QueryString["ID"];
                DAT = Request.QueryString["DAT"];
                Cust = Request.QueryString["Cust"];
                Usr = Request.QueryString["Usr"];

                
                dt_ = new DataTable();
                /*if (DAT != null && Cust != "0" && Usr != null)
                {
                    dt_ = DBConnection.GetQueryData(" select * from  v_dsr  where   CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and  MSal_dat='" + DAT + "' and  CustomerID='" + Cust + "' and  username='" + Usr + "'");

                }
                else if (DAT != null && Cust != "0")
                {
                    dt_ = DBConnection.GetQueryData(" select * from  v_dsr  where   CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and  MSal_dat='" + DAT + "' and  CustomerID='" + Cust + "'");

                }else if (DAT != null && Usr!= null)
                {
                    dt_ = DBConnection.GetQueryData(" select * from  v_dsr  where   CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and  MSal_dat='" + DAT + "' and username='" + Usr + "'");
                
                 }
                else if (Cust != "0" && Usr != null)
                {
                    dt_ = DBConnection.GetQueryData(" select * from  v_dsr  where   CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and CustomerID='" + Cust + "' and  username='" + Usr + "'");

                }
                else if (DAT != null)
                {
                    dt_ = DBConnection.GetQueryData(" select * from  v_dsr  where   CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and  MSal_dat='" + DAT + "'");
                    //select * from  v_dsr  where  v_dsr.MSal_dat='01/17/2019'  and BranchId= '001' and CompanyId = 'COM_001'
                }
                else if(Cust != null)
                {
                    dt_ = DBConnection.GetQueryData(" select * from  v_dsr  where   CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and  CustomerID='" + Cust + "'");
                    //select * from  v_dsr  where  v_dsr.MSal_dat='01/17/2019'  and BranchId= '001' and CompanyId = 'COM_001'
                }
                 else*/
                //if (Usr != null)
                // {
                //     //dt_ = DBConnection.GetQueryData(" select * from  view_dsr  where   CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and  username='" + Usr + "'");
                //     //select * from  v_dsr  where  v_dsr.MSal_dat='01/17/2019'  and BranchId= '001' and CompanyId = 'COM_001'
                // }
                 
                //else              
                {
                    dt_ = DBConnection.GetQueryData(" select * from  view_dsr  where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");// and  MSal_dat=replace(convert(NVARCHAR, getdate(), 106), ' ', '/')"); 
                }
                GVDSR.DataSource = dt_;
                GVDSR.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}