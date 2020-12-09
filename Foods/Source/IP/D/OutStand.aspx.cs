using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using Foods;


namespace Foods.Source.IP
{
    public partial class OutStand : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);       

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_outstand.Text = "0.0";
                TBOutstand.Text = "0.00";

                using (SqlCommand cmdSupNam = new SqlCommand())
                {
                    //cmd.CommandText = " select rtrim('[' + CAST(ven_id AS VARCHAR(200)) + ']-' + ven_nam ) as [ven_nam], ven_id from t_ven";
                    //cmdSupNam.CommandText = "select subheadcategoryfiveID,subheadcategoryfiveName from subheadcategoryfive where subheadcategoryfourGeneratedID = 'MB000004' and isflag = 1";
                    cmdSupNam.CommandText = "select rtrim('[' + CAST(supplierId AS VARCHAR(200)) + ']-' + suppliername ) as [suppliername], supplierId from supplier";
                    cmdSupNam.Connection = con;
                    con.Open();

                    DataTable dtSupNam = new DataTable();
                    SqlDataAdapter adp = new SqlDataAdapter(cmdSupNam);
                    adp.Fill(dtSupNam);

                    ddlVenNam.DataSource = dtSupNam;
                    ddlVenNam.DataTextField = "suppliername";
                    ddlVenNam.DataValueField = "supplierId";
                    ddlVenNam.DataBind();
                    ddlVenNam.Items.Insert(0, new ListItem("--Select--", "0"));


                    con.Close();
                }

            }
        }

        protected void ddlVenNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string query = " select isnull(SUM(Out_Standing) , 0)as 'OUTSTAND' from MPurchase where ven_id ='" + ddlVenNam.SelectedValue + "'";

                SqlCommand command = new SqlCommand(query, con);
                con.Open();
                DataTable dtven = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(command);
                adp.Fill(dtven);
                command.ExecuteNonQuery();

                if (dtven.Rows.Count > 0)
                {
                    txt_outstand.Text = dtven.Rows[0]["OUTSTAND"].ToString();                   
                }
                else
                {
                    txt_outstand.Text = (0.00).ToString();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        protected void btnoutstand_Click(object sender, EventArgs e)
        {

            try
            {
                con.Open();
                SqlCommand command = con.CreateCommand();

                string totalprev = "0.0";



                if (TBOutstand.Text != "0.00")
                {
                    if (txt_outstand.Text == "")
                    {
                        txt_outstand.Text = "0.00";
                    }
                    else
                    {
                        totalprev = (Convert.ToDouble(txt_outstand.Text) + Convert.ToDouble(TBOutstand.Text)).ToString();

                        command.CommandText =
                                   " INSERT INTO MPurchase(Out_Standing, ven_id) " +
                                   " VALUES " +
                                   " ('" + totalprev + "','" + ddlVenNam.SelectedValue + "')";

                        command.ExecuteNonQuery();

                    }
                }
                else
                {
                    totalprev = txt_outstand.Text;

                    command.CommandText = " UPDATE MPurchase SET Out_Standing = '" + totalprev + "' where  ven_id =" + ddlVenNam.SelectedValue + "";

                    command.ExecuteNonQuery();

                }

                con.Close();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}