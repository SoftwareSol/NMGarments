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
    public partial class frm_loadsheet_ : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        string LODSHT, CAL, EMPID;
        DBConnection db = new DBConnection();
        static Random random = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGrid();
                lblcom.Text = "Ham Brothers";
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
                LODSHT = Request.QueryString["LODSHT"];
                
                dt_ = new DataTable();

               
                SqlCommand cmd = new SqlCommand("dbo.sp_loadsheet", con);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@dsrdat", LODSHT);
                SqlDataAdapter rdr = new SqlDataAdapter(cmd);

                rdr.Fill(dt_);
                //dt_ = DBConnection.GetQueryData(" select * from  v_loadsheet  where dsrdat ='" + LODSHT + "' and CompanyId = '" + Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'");

                if (dt_.Rows.Count > 0)
                {
                    //lblsalman.Text = dt_.Rows[0]["Purchase Rate"].ToString();
                    GVLoadSheet.DataSource = dt_;
                    GVLoadSheet.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}