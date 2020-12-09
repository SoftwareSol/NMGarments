using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Project;
using System.Configuration;
using System.Data.OleDb;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;


namespace Foods
{
    public class Common
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);


        public void ShowAccountCategoryFiveID(HiddenField SubHeadCatFiv)
        {
            try
            {

                string str = "select max(cast(subheadcategoryfiveID as int))  as [subheadcategoryfiveID] from subheadcategoryfive";


                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();

                DataTable dt_ = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt_);
                if (dt_.Rows.Count <= 0)
                {
                    SubHeadCatFiv.Value = "MB0000001";
                }
                else
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        //SubHeadCatFiv.Value = "";

                        //if (string.IsNullOrEmpty(SubHeadCatFiv.Value) )                        
                        {
                            int v = Convert.ToInt32(reader["subheadcategoryfiveID"].ToString());
                            int b = v + 1;
                            SubHeadCatFiv.Value = "MB000000" + b.ToString();
                        }
                    }

                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void exporttoexcel()
        {
 
        }

        public void getUnit()
        {
            using (SqlCommand cmdpar = new SqlCommand())
            {
                //con.Close();
                //cmdpar.CommandText = " select rtrim('[' + CAST(SubHeadCategoriesGeneratedID AS VARCHAR(200)) + ']-' + SubHeadCategoriesName ) as [SubHeadCategoriesName], SubHeadCategoriesID from SubHeadCategories ";

                cmdpar.CommandText = " select rtrim('[' + CAST(untid AS VARCHAR(200)) + ']-' + untnam ) as [untnam], untid from tbl_unts ";

                cmdpar.Connection = con;
                con.Open();

                DataTable dtpar = new DataTable();
                SqlDataAdapter adp = new SqlDataAdapter(cmdpar);
                adp.Fill(dtpar);

                con.Close();
            }
        }

        public void Import_To_Grid(string FilePath, string Extension, string Step, GridView GridView1)
        {
            try
            {
                string conStr = "";
                switch (Extension)
                {
                    case ".xls": //Excel 97-03
                        conStr = "Provider=Microsoft.ACE.OLEDB.8.0;Data Source=" + FilePath + ";Extended Properties=Excel 8.0 ";
                        break;
                    case ".xlsx": //Excel 07
                        conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=Excel 12.0 ";
                        break;
                }
                conStr = String.Format(conStr, FilePath, 1);
                OleDbConnection connExcel = new OleDbConnection(conStr);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                DataTable dt = new DataTable();
                cmdExcel.Connection = connExcel;
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = null;
                if (Step == "1")
                {
                    SheetName = "CustomersList$";
                }
                connExcel.Close();

                //Read Data from First Sheet
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();

                connExcel.Close();
                //Load data to Grid

                //LoadData(dt);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Upload(FileUpload FileUploadToServer, Label lblMsg, HttpServerUtility Server)
        {
            try
            {
                lblMsg.Text = "";

                if (FileUploadToServer.HasFile)
                {
                    System.Threading.Thread.Sleep(10000);

                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", " progressbar();", true);

                    string FileName = Path.GetFileName(FileUploadToServer.PostedFile.FileName);
                    string Extension = Path.GetExtension(FileUploadToServer.PostedFile.FileName);

                    string FolderPath = ConfigurationManager.AppSettings["FolderPath"];

                    string FilePath = Server.MapPath(FolderPath + FileName);

                    if (Extension == ".xlsx")
                    {
                        lblMsg.Text = "Uploading:";
                        FileUploadToServer.SaveAs(FilePath);
                        //Import_To_Grid(FilePath, Extension, "1",);
                    }
                    else
                    {
                        lblMsg.Text = "Please select .xlsx or Excel File!!";
                    }
                }
                else
                {
                    lblMsg.Text = "Please select some thing to upload!!";
                }
            }
            catch (Exception ex)
            {
                throw ex;                
            }
        }

    }
}