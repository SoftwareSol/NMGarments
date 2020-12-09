using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Foods.Source.IP.D.Global_Test
{
    public partial class Chart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Select * from tbl_MSal", con);
                SqlDataReader mydatareader;

                try
                {
                    con.Open();
                    mydatareader = cmd.ExecuteReader();
                    while (mydatareader.Read())
                    {
                        this.Chart1.Series["MSal_dat"].Points.AddXY(mydatareader["CustomerID"].ToString(), mydatareader["Recovery"].ToString());
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}