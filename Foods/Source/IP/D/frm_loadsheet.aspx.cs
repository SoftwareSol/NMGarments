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
    public partial class frm_loadsheet : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        string DSR, CAL, EMPID;
        DBConnection db = new DBConnection();
        static Random random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
        }

        public static List<int> GenerateRandom(int count)
        {
            // generate count random values.
            HashSet<int> candidates = new HashSet<int>();
            for (Int32 top = Int32.MaxValue - count; top < Int32.MaxValue; top++)
            {
                Console.WriteLine(top);
                // May strike a duplicate.
                if (!candidates.Add(random.Next(top + 1)))
                {
                    candidates.Add(top);
                }
            }

            // load them in to a list.
            List<int> result = candidates.ToList();

            // shuffle the results:
            int i = result.Count;
            while (i > 1)
            {
                i--;
                int k = random.Next(i + 1);
                int value = result[k];
                result[k] = result[i];
                result[i] = value;
            }
            return result;
        }  

        public void FillGrid()
        {
            try
            {
                DSR = Request.QueryString["ID"];
                CAL = Request.QueryString["CAL"];
                EMPID = Request.QueryString["EMPID"];

                dt_ = new DataTable();

                if (CAL != null)
                {
                    dt_ = DBConnection.GetQueryData(" select * from  v_loadsheet  where   CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and  MSal_dat='" + CAL + "'");
                    //select * from  v_dsr  where  v_dsr.MSal_dat='01/17/2019'  and BranchId= '001' and CompanyId = 'COM_001'
                }
                else if (EMPID != "0")
                {
                    dt_ = DBConnection.GetQueryData(" select * from  v_loadsheet  where   CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and  username='" + EMPID + "'");
                    //select * from  v_dsr  where  v_dsr.MSal_dat='01/17/2019'  and BranchId= '001' and CompanyId = 'COM_001'
                }
                else if (CAL != null && EMPID != "0")
                {
                    dt_ = DBConnection.GetQueryData(" select * from  v_loadsheet  where   CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "' and  MSal_dat='" + CAL + "' and  username='" + EMPID + "'");
                }

                else
                {
                    dt_ = DBConnection.GetQueryData(" select * from  v_loadsheet  where CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");// and  MSal_dat=replace(convert(NVARCHAR, getdate(), 106), ' ', '/')"); 
                }
                //lbltotal.Text = 
                GVLoadSheet.DataSource = dt_;
                GVLoadSheet.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}