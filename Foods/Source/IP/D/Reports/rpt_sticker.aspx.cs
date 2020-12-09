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
using System.Drawing;

using Foods;
using DataAccess;

namespace Foods
{
    public partial class rpt_sticker : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        string query, StickId;

        DBConnection db = new DBConnection();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {
                    StickId = Request.QueryString["StickId"];
                    getstk(StickId);
                  

                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        public void getstk(string stkid)
        {
            try
            {
                query = " select * from tbl_sticker inner join Products on tbl_sticker.ProductID = Products.ProductID " +
                    " where stickerid= '" + stkid + "'";
                //and CompanyId = '" +     Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    lbl_compnam.Text = dt_.Rows[0]["compnam"].ToString();
                    lbl_prodesc.Text = dt_.Rows[0]["ProductName"].ToString();
                    lblsiz.Text = dt_.Rows[0]["siz"].ToString();
                    lblcol.Text = dt_.Rows[0]["color"].ToString();

                    lblcomp2.Text = dt_.Rows[0]["compnam"].ToString();
                    lbl_prodes2.Text = dt_.Rows[0]["ProductName"].ToString();
                    lblsiz2.Text = dt_.Rows[0]["siz"].ToString();
                    lblcol2.Text = dt_.Rows[0]["color"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}