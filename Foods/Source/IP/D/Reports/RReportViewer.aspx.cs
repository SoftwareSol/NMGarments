using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Text;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System.Configuration;
using DataAccess;
using Foods;

namespace Foods
{
    public partial class RReportViewer : System.Web.UI.Page
    {
        SqlCommand cmd;
        ReportDocument cryRpt = new ReportDocument();
        string SAL, CRE, DatTo, SONO, Cust, DatFrm, MNO, PRO, DSR, id, SPRO, EPRO, query, RPTID, EMPID, CAL, PURID, DATW, MON, VENID;
        SqlConnection con = DataAccess.DBConnection.connection();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                 id = Request.QueryString["ID"];
                 SAL = Request.QueryString["SAL"];
                 CRE = Request.QueryString["CRE"];
                 PRO = Request.QueryString["PRO"]; 
                 DatFrm = Request.QueryString["frmDat"];
                 DatTo = Request.QueryString["toDat"];
                 MNO = Request.QueryString["MNO"];
                 SPRO = Request.QueryString["SPRO"];
                 EPRO = Request.QueryString["EPRO"];
                 EMPID = Request.QueryString["EMPID"];
                 CAL = Request.QueryString["CAL"];
                 PURID = Request.QueryString["PURID"];
                 VENID = Request.QueryString["VENID"];
                 DSR = Request.QueryString["DSR"];


                 //PayRoll&EMPID=1
                switch (id)
                {
                    case "SAL"://Done
                        this.getSAL();
                        break;

                    case "CRE"://Done
                        this.getCre(MNO);
                        break;

                    case "PRO"://Done
                        this.getProfit();
                        break;

                    case "PR"://Done
                        this.getPur();
                        break;
                }

           }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void getCre(string mobile)
        {

            try
            {
                cryRpt.Load(Server.MapPath("rpt_credsheet.rpt"));
                cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "HumBros");

                DataTable dt_ = new DataTable();

                if (mobile != null)
                {
                    cmd = new SqlCommand("SELECT * from V_VPCustCred where CellNo1 ='"+ mobile +"'", con);
                }
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt_);
                cryRpt.SetDataSource(dt_);
                cryRpt.SetParameterValue("CompanyName", "View Point");
                cryRpt.SetParameterValue("address", "Shop # 2 Opposite Rafah-e-aam Post Office Malir Halt, Karachi");
                cryRpt.SetParameterValue("PhoneNo", "0321-2010080");
                cryRpt.SetParameterValue("ReportName", "Customer Credit Report");


                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream =
                cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                cryRpt.Close();
                cryRpt.Dispose();
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void getProfit()
        {

            try
            {
                cryRpt.Load(Server.MapPath("rpt_profit.rpt"));
                cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "HumBros");

                DataTable dt_ = new DataTable();

                if (SPRO != null && EPRO != null)
                {
                    cmd = new SqlCommand("SELECT * from V_VPProfit where billdat between '" + SPRO + "' and '" + EPRO + "'", con);
                }
                else
                {
                    cmd = new SqlCommand("SELECT * from V_VPProfit", con); 
                }
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt_);
                cryRpt.SetDataSource(dt_);
                cryRpt.SetParameterValue("CompanyName", "View Point");
                cryRpt.SetParameterValue("address", "Shop # 2 Opposite Rafah-e-aam Post Office Malir Halt, Karachi");
                cryRpt.SetParameterValue("PhoneNo", "0321-2010080");
                cryRpt.SetParameterValue("ReportName", "Profit Sheet");


                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream =
                cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                cryRpt.Close();
                cryRpt.Dispose();
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void getSAL()
        {
            try
            {
                
                cryRpt.Load(Server.MapPath("rpt_DSR.rpt"));
                cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "HumBros");
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ToString());


                if (DatFrm != null && DatTo != null)
                {
                    cmd = new SqlCommand("SELECT * from V_VPDSR where billdat between '" + DatFrm + "' and '" + DatTo + "' and CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'", con);
                }
                else
                {
                    cmd = new SqlCommand("SELECT * from V_VPDSR where CompanyId='" + Session["CompanyID"] + "' and BranchId='" + Session["BranchID"] + "'", con);
                }

                con.Open();
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@PurNo", "PO009");

                //cmd.CommandText = proc;
                //cmd.Parameters.Add("@PurNo", SqlDbType.VarChar).Value = "PO009";

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                cryRpt.SetDataSource(dt);
                //cryRpt.RecordSelectionFormula = 
                cryRpt.SetParameterValue("CompanyName", "View Point");
                cryRpt.SetParameterValue("address", "Shop # 2 Opposite Rafah-e-aam Post Office Malir Halt, Karachi");
                cryRpt.SetParameterValue("PhoneNo", "0321-2010080");
                cryRpt.SetParameterValue("ReportName", "Sales Report");

                //CrystalReportViewer1.ReportSource = cryRpt;
                //CrystalReportViewer1.DataBind();
                //ReportDocument rpt = (ReportDocument)Session[sessid];
                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream =
                cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                cryRpt.Close();
                cryRpt.Dispose();
                //rprt.Load(Server.MapPath("~/CrystalReport.rpt"));
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private DataTable Get_SPPur()
        {
            string conString = ConfigurationManager.ConnectionStrings["D"].ConnectionString;
           
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    SqlCommand cmd = new SqlCommand("sp_getPur");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@MPurID", SqlDbType.Int).Value = PURID;

                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                   

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable Get_VDATPur(string date)
        {
            string conString = ConfigurationManager.ConnectionStrings["D"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    SqlCommand cmd = new SqlCommand("select * from v_getpur where MpurDate='" + date + "'");
                    cmd.CommandType = CommandType.Text;
                    //cmd.Parameters.Add("@MPurID", SqlDbType.Int).Value = PURID;

                    cmd.Connection = con;
                    sda.SelectCommand = cmd;


                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable Get_VMONPur(string month)
        {
            string conString = ConfigurationManager.ConnectionStrings["D"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    SqlCommand cmd = new SqlCommand("Select DateName( month , DateAdd( month , MONTH(MpurDate) , 0 ) - 1 ) as [Month],* from v_getpur " +
                       " where DateName( month , DateAdd( month , MONTH(MpurDate) , 0 ) - 1 ) ='" + month + "'");

                    cmd.CommandType = CommandType.Text;
                    //cmd.Parameters.Add("@MPurID", SqlDbType.Int).Value = PURID;

                    cmd.Connection = con;
                    sda.SelectCommand = cmd;


                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }
        private DataTable Get_CompPur()
        {
            string conString = ConfigurationManager.ConnectionStrings["D"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    SqlCommand cmd = new SqlCommand("select * from v_compwispur");
                    cmd.CommandType = CommandType.Text;

                    cmd.Connection = con;
                    sda.SelectCommand = cmd;

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable Get_VPur(string purid)
        {
            string conString = ConfigurationManager.ConnectionStrings["D"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    if (purid == null)
                    {
                        cmd = new SqlCommand("select * from v_getPur");
                    }
                    else
                    {
                        cmd = new SqlCommand("select * from v_getPur where MPurID = '" + purid + "'");
                    }

                    cmd.CommandType = CommandType.Text;
                    
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable Get_VPur()
        {
            string conString = ConfigurationManager.ConnectionStrings["D"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd = new SqlCommand("select * from v_getPur");
                 
                    cmd.CommandType = CommandType.Text;

                    cmd.Connection = con;
                    sda.SelectCommand = cmd;

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }

        private DataTable Get_VenPur()
        {
            string conString = ConfigurationManager.ConnectionStrings["D"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    SqlCommand cmd = new SqlCommand("select * from v_getPur where ven_id = '" + VENID + "'");
                    cmd.CommandType = CommandType.Text;

                    cmd.Connection = con;
                    sda.SelectCommand = cmd;

                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    return dt;
                }
            }
        }

        private void getPurCom()
        {

            try
            {
                DataTable dt_ = new DataTable();

                dt_ = Get_CompPur();
                cryRpt.Load(Server.MapPath("rpt_pur_ComWise.rpt"));
                cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");

                cryRpt.SetDataSource(dt_);
                //cryRpt.RecordSelectionFormula = 
                cryRpt.SetParameterValue("Report Name", "Comapny Wise Purchase Report");
                //cryRpt.SetParameterValue("Report Name", "Purchase Report");
                //cryRpt.SetParameterValue("ComName", "Orion Foods");

                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream =
                cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                cryRpt.Close();
                cryRpt.Dispose();
                //rprt.Load(Server.MapPath("~/CrystalReport.rpt"));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void getPur()
        {
            //Local variables

            try
            {
                DataTable dt_ = new DataTable();

                //if (DATW != null)
                //{
                //    dt_ = Get_VDATPur(DATW);

                //    cryRpt.Load(Server.MapPath("rpt_Pur_Tim_wise.rpt"));

                //}
                //else 
                if (PURID != null)
                {
                    dt_ = Get_VPur(PURID);
                    cryRpt.Load(Server.MapPath("rpt_pur.rpt"));

                }
                //else if (MON != null)// || MON != "0")
                //{
                //    dt_ = Get_VMONPur(MON);

                //    cryRpt.Load(Server.MapPath("rpt_Pur_Tim_wise.rpt"));

                //}
                //else if (VENID != null)
                //{
                //    dt_ = Get_VenPur();
                //    cryRpt.Load(Server.MapPath("rpt_Purchase.rpt"));

                //}
                //else
                //{
                //    dt_ = Get_VPur();

                //    cryRpt.Load(Server.MapPath("rpt_Purchase.rpt"));

                //}

                cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "silvermoon");
                cryRpt.SetDataSource(dt_);
                //cryRpt.RecordSelectionFormula = 

                cryRpt.SetParameterValue("ReportName", "Purchase Invoice");
                cryRpt.SetParameterValue("CompName", "Silver Moon");
                cryRpt.SetParameterValue("CompAdd", "Address");
                cryRpt.SetParameterValue("Tel", "0217676");

                /// if no record found
                //if (dt_.Rows.Count < 0)
                //{
                //cryRpt.SetParameterValue("norecord", "No Record Found!!");
                //}
                //else
                //{
                // cryRpt.SetParameterValue("norecord", "");
                //}

                //cryRpt.SetParameterValue("@MPurID","");

                //CrystalReportViewer1.ReportSource = cryRpt;
                //CrystalReportViewer1.DataBind();
                //ReportDocument rpt = (ReportDocument)Session[sessid];
                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream =
                cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                cryRpt.Close();
                cryRpt.Dispose();
                //rprt.Load(Server.MapPath("~/CrystalReport.rpt"));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private void getQuotRpt()
        {
            try
            {
                string rptid, CAL,CUSID;
                rptid = Request.QueryString["QUOTID"];
                CAL = Request.QueryString["CAL"];
                CUSID = Request.QueryString["CUSID"];


                cryRpt.Load(Server.MapPath("rpt_Quot.rpt"));
                cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ToString());
                //SqlCommand cmd = new SqlCommand("SELECT * from v_Quot where MProQuot_sono = '" + rptid + "'", con);

                if (rptid != null && CAL != null && CUSID != null)
                {
                    query = "SELECT * from v_Quot where MProQuot_id = '" + rptid + "' and MProQuot_dat = '" + CAL + "' and CustomerID = '" + CUSID + "'";

                }
                else if (rptid != null && CUSID != null)
                {
                    query = "SELECT * from v_Quot where MProQuot_id = '" + rptid + "' and CustomerID = '" + CUSID + "'";

                }
                else if (rptid != null && CAL != null)
                {
                    query = "SELECT * from v_Quot where MProQuot_id = '" + rptid + "' and MProQuot_dat = '" + CAL + "'";

                }
                else if (CUSID != null && CAL != null)
                {
                    query = "SELECT * from v_Quot where CustomerID = '" + CUSID + "' and MProQuot_dat = '" + CAL + "'";
                }
                else if (rptid != null)
                {
                    query = "SELECT * from v_Quot where MProQuot_id = '" + rptid + "'";
                }
                
                else if (CAL != null)
                {
                    query = "SELECT * from v_Quot where MProQuot_dat = '" + CAL + "'";
                }
                else if (CUSID != null)
                {
                    query = "SELECT * from v_Quot where CustomerID = '" + CUSID + "'"; 
                }
                else
                {
                    query = "SELECT * from v_Quot";
                }

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@PurNo", "PO009");

                //cmd.CommandText = proc;
                //cmd.Parameters.Add("@PurNo", SqlDbType.VarChar).Value = "PO009";

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    cryRpt.SetDataSource(dt);
                    //cryRpt.RecordSelectionFormula = 
                    cryRpt.SetParameterValue("CompayName", "OrionFoods");
                    cryRpt.SetParameterValue("ReportName", "Quotation");
                    cryRpt.SetParameterValue("prdm", "http:\\www.Orion Foods.com");

                    //CrystalReportViewer1.ReportSource = cryRpt;
                    //CrystalReportViewer1.DataBind();
                    //ReportDocument rpt = (ReportDocument)Session[sessid];
                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream =
                    cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.BinaryWrite(byteArray);
                    Response.Flush();
                    Response.Close();
                    cryRpt.Close();
                    cryRpt.Dispose();
                    con.Close();
                    //oStream =
                    //cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    //byteArray = new byte[oStream.Length];
                    //oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                    //Response.ClearContent();
                    //Response.ClearHeaders();
                    //Response.ContentType = "application/pdf";
                    //Response.BinaryWrite(byteArray);
                    //Response.Flush();
                    //Response.Close();
                    //cryRpt.Close();
                    //cryRpt.Dispose();
                    ////rprt.Load(Server.MapPath("~/CrystalReport.rpt"));
                    //con.Close();
                }
                else
                {
                    // Not Record Show
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void getGRN()
        {
            try
            {
                //string purid;
                //purid = Request.QueryString["RPTID"];

                cryRpt.Load(Server.MapPath("rpt_grn.rpt"));
                cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");
                SqlCommand cmd = new SqlCommand("select * from v_grn", con);
                con.Open();
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@PurNo", "PO009");

                //cmd.CommandText = proc;
                //cmd.Parameters.Add("@PurNo", SqlDbType.VarChar).Value = "PO009";

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                cryRpt.SetDataSource(dt);
                //cryRpt.RecordSelectionFormula = 
                cryRpt.SetParameterValue("Report Name", "GRN Report");
                cryRpt.SetParameterValue("ComName", "Orion Foods");
                

                //CrystalReportViewer1.ReportSource = cryRpt;
                //CrystalReportViewer1.DataBind();
                //ReportDocument rpt = (ReportDocument)Session[sessid];
                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream =
                cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                cryRpt.Close();
                cryRpt.Dispose();
                //rprt.Load(Server.MapPath("~/CrystalReport.rpt"));
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void getPO()
        {
            try
            {
                //string purid;
                //purid = Request.QueryString["RPTID"];

                cryRpt.Load(Server.MapPath("rpt_PurOrd.rpt"));
                cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ToString());
                SqlCommand cmd = new SqlCommand("select * from v_PurOrd", con);
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                cryRpt.SetDataSource(dt);
                cryRpt.SetParameterValue("Report Name", "Purchase Order");
                cryRpt.SetParameterValue("ComName", "Orion Foods");
                //CrystalReportViewer1.ReportSource = cryRpt;
                //CrystalReportViewer1.DataBind();                
                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream =
                cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                cryRpt.Close();
                cryRpt.Dispose();
                //rprt.Load(Server.MapPath("~/CrystalReport.rpt"));
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void getCN()
        {
            try
            {
                //string purid;
                //purid = Request.QueryString["RPTID"];

                cryRpt.Load(Server.MapPath("rpt_CN.rpt"));
                cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ToString());
                SqlCommand cmd = new SqlCommand("select * from v_CN", con);
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                cryRpt.SetDataSource(dt);
                cryRpt.SetParameterValue("Report Name", "Purchase Order");
                cryRpt.SetParameterValue("ComName", "Orion Foods");
                //CrystalReportViewer1.ReportSource = cryRpt;
                //CrystalReportViewer1.DataBind();                
                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream =
                cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                cryRpt.Close();
                cryRpt.Dispose();
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        private void getSALARY()
        {
            try
            {
                //string purid;
                //purid = Request.QueryString["RPTID"];


                
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ToString());

                if (EMPID != "0" && CAL != null)
                {
                    cryRpt.Load(Server.MapPath("Rpt_Salary.rpt"));
                    cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");

                    cmd = new SqlCommand("select * from v_Salary where EmployeeID = " + EMPID + " and  month(craete_at) =  month('" + CAL + "')", con);
                }
                else if (EMPID != "0")
                {
                    cryRpt.Load(Server.MapPath("Rpt_Salary.rpt"));
                    cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");

                    cmd = new SqlCommand("select * from v_Salary where EmployeeID = " + EMPID + "", con);
                }
                else if (CAL != null)
                {
                    cryRpt.Load(Server.MapPath("Rpt_Salary.rpt"));
                    cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");

                    cmd = new SqlCommand("select * from v_Salary where month(craete_at) =  month('" + CAL + "')", con);
                }
                else
                {
                    cryRpt.Load(Server.MapPath("Rpt_MSalary.rpt"));
                    cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");

                    cmd = new SqlCommand("select * from v_Salary where month(craete_at) =  month(GETDATE())", con);
                }
                if (RPTID != null)
                {
                    cmd = new SqlCommand("select * from v_Salary where MSal_ID = " + RPTID + "", con);
                }
                //else if (ckcsh != null)
                //{
                //    cmd = new SqlCommand("select * from v_rptSal where iscash = 1", con);
                //}
                //else if (DatFrm != null || DatTo != null)
                //{
                //    cmd = new SqlCommand("select * from v_rptSal where Msal_dat  BETWEEN '" + DatFrm + "' AND '" + DatTo + "'", con);
                //}
                //else if (Cust != null)
                //{
                //    cmd = new SqlCommand("select * from v_rptSal where CustomerID ='" + Cust + "'", con);
                //}
                //else if (Sale != null)
                //{
                //    cmd = new SqlCommand("select * from v_rptSal where MSal_id ='" + Sale + "'", con);
                //}
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    cryRpt.SetDataSource(dt);
                    cryRpt.SetParameterValue("Report Name", "Salary Sheet");
                    cryRpt.SetParameterValue("ComName", "Orion Foods");
                    if (CAL != null)
                    {
                        cryRpt.SetParameterValue("Date", CAL);
                    }
                    else
                    {
                        cryRpt.SetParameterValue("Date", DateTime.Now.ToShortDateString());
                    }
                    //CrystalReportViewer1.ReportSource = cryRpt;
                    //CrystalReportViewer1.DataBind();
                    System.IO.Stream oStream = null;
                    byte[] byteArray = null;
                    oStream =
                    cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    byteArray = new byte[oStream.Length];
                    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.BinaryWrite(byteArray);
                    Response.Flush();
                    Response.Close();
                    cryRpt.Close();
                    cryRpt.Dispose();
                    con.Close();
                }
                else
                { 
                    //Do Nothing
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void getMSALARY()
        {
            try
            {
                //string purid;
                //purid = Request.QueryString["RPTID"];


                cryRpt.Load(Server.MapPath("Rpt_MSalary.rpt"));
                cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ToString());
                cmd = new SqlCommand("select * from v_SubSalary", con);
                //if (RPTID != null)
                //{
                   // cmd = new SqlCommand("select * from v_SubSalary where MSal_ID = " + RPTID + "", con);
                //}
                //else if (ckcsh != null)
                //{
                //    cmd = new SqlCommand("select * from v_rptSal where iscash = 1", con);
                //}
                //else if (DatFrm != null || DatTo != null)
                //{
                //    cmd = new SqlCommand("select * from v_rptSal where Msal_dat  BETWEEN '" + DatFrm + "' AND '" + DatTo + "'", con);
                //}
                //else if (Cust != null)
                //{
                //    cmd = new SqlCommand("select * from v_rptSal where CustomerID ='" + Cust + "'", con);
                //}
                //else if (Sale != null)
                //{
                //    cmd = new SqlCommand("select * from v_rptSal where MSal_id ='" + Sale + "'", con);
                //}
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                cryRpt.SetDataSource(dt);
                cryRpt.SetParameterValue("Report Name", "Sales Sheet");
                cryRpt.SetParameterValue("ComName", "Orion Foods");
                //CrystalReportViewer1.ReportSource = cryRpt;
                //CrystalReportViewer1.DataBind();
                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream =
                cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                cryRpt.Close();
                cryRpt.Dispose();
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void getDSR()
        {
            try
            {
                if (DATW != null && Cust != null)
                {
                    Response.Redirect("frm_dsr.aspx?ID=DSR&DAT=" + DATW + "&Cust=" + Cust + "");
                }
                else if (DATW != null)
                {
                    Response.Redirect("frm_dsr.aspx?ID=DSR&DAT=" + DATW + "");
                }
                else if (Cust != null)
                {
                    Response.Redirect("frm_dsr.aspx?ID=DSR&Cust=" + Cust + "");
                }

                /*cryRpt.Load(Server.MapPath("rpt_dsr.rpt"));Cust
                cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ToString());
                cmd = new SqlCommand("select * from v_dsr where  MSal_dat ='12/9/2018' ", con);
               
                // if (Sale != null)
                //{
                  //  cmd = new SqlCommand("select * from v_rptSal where MSal_id ='" + Sale + "'", con);
                //}

                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                cryRpt.SetDataSource(dt);
                cryRpt.SetParameterValue("Report Name", "Daily Sales Report");
                //cryRpt.SetParameterValue("ComName", "Orion Foods");
                //cryRpt.SetParameterValue("ComName", "Orion Foods");

                //CrystalReportViewer1.ReportSource = cryRpt;
                //CrystalReportViewer1.DataBind();
                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream =
                cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                cryRpt.Close();
                cryRpt.Dispose();
                con.Close();*/

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        

        private void getSO()
        {
            try
            {
                string SOCUSt, SODatFrm, SODatTo;
               
                SOCUSt = Request.QueryString["SOCUSt"];
                SODatFrm = Request.QueryString["SODatFrm"];
                SODatTo = Request.QueryString["SODatTo"];

                cryRpt.Load(Server.MapPath("rpt_SO.rpt"));
                cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ToString());
                SqlCommand cmd = new SqlCommand("select * from v_SO", con);

                if (SONO != null)
                {
                    cmd = new SqlCommand("select * from v_SO where MSalOrdsono='" + SONO + "'", con);
                }
                else if (SOCUSt != null)
                {
                    cmd = new SqlCommand("select * from v_SO where CustomerID = '" + SOCUSt + "'", con);
                }
                else if (SODatFrm != null || SODatTo != null)
                {
                    cmd = new SqlCommand("select * from v_SO where MSalOrd_dat  BETWEEN '" + SODatFrm + "' AND '" + SODatTo + "'", con);
                }

                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                cryRpt.SetDataSource(dt);
                cryRpt.SetParameterValue("Report Name", "Sales Order");
                //cryRpt.SetParameterValue("ComName", "Orion Foods");
                //CrystalReportViewer1.ReportSource = cryRpt;
                //CrystalReportViewer1.DataBind();
                System.IO.Stream oStream = null;
                byte[] byteArray = null;
                oStream =
                cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                cryRpt.Close();
                cryRpt.Dispose();
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}