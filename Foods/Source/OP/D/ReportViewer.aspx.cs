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

namespace Foods.Source.OP
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        SqlCommand cmd;
        ReportDocument cryRpt = new ReportDocument();
        string ckcrdt, ckcsh, DatTo, DatFrm, Cust, Sale, DSR, id, SONO, query, RPTID, EMPID, CAL, PURID, DATW, MON, VENID;
        SqlConnection con = DataAccess.DBConnection.connection();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                 id = Request.QueryString["ID"];
                 DATW = Request.QueryString["DAT"];
                 MON = Request.QueryString["MON"]; 
                 ckcrdt = Request.QueryString["ckcrdt"];
                 ckcsh = Request.QueryString["ckcsh"];
                 DatFrm = Request.QueryString["DatFrm"];
                 DatTo = Request.QueryString["DatTo"];
                 Cust = Request.QueryString["Cust"];
                 Sale = Request.QueryString["Sale"];
                 SONO = Request.QueryString["SONO"];
                 RPTID = Request.QueryString["RPTID"];
                 EMPID = Request.QueryString["EMPID"];
                 CAL = Request.QueryString["CAL"];
                 PURID = Request.QueryString["PURID"];
                 VENID = Request.QueryString["VENID"];
                 DSR = Request.QueryString["DSR"];


                 //PayRoll&EMPID=1
                switch (id)
                {
                    case "Req"://Done
                        this.getreq();
                        break;

                    case "QuotaionReport"://Done
                        this.getQuotRpt();
                        break;

                    case "PR"://Done
                        this.getPur();
                        break;
                    case "PRCOM":
                        this.getPurCom();
                        break;
                    case "STK":
                        this.getstk();
                        break;

                    case "PO":
                        this.getPO();
                        break;

                    case "GRN":
                        this.getGRN();
                        break;

                    case "CN":
                        this.getCN();
                        break;

                    case "SONO":
                        this.getSO();
                        break;

                    case "SAL":
                        this.getSAL();
                        break;

                    case "DSR":
                        this.getDSR();
                        break;

                    case "Salaryrpt":
                        this.getSALARY();
                        break;

                    case "SalarMyrpt":
                        this.getMSALARY();
                        break;

                    case "PayRoll":
                        this.getSALARY();
                        break;
                }


                //string purid;
                //purid = Request.QueryString["PurID"];

                //cryRpt.Load(Server.MapPath("rpt_Pur.rpt"));
                ////cryRpt.SetDatabaseLogon("sa", "friend", "ASSAD-PC", "Orion Foods");
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ToString());
                //SqlCommand cmd = new SqlCommand("SELECT * from v_pur where PurNo = 'PO008'", con);
                //con.Open();
                ////cmd.CommandType = CommandType.StoredProcedure;
                ////cmd.Parameters.AddWithValue("@PurNo", "PO009");

                ////cmd.CommandText = proc;
                ////cmd.Parameters.Add("@PurNo", SqlDbType.VarChar).Value = "PO009";

                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //sda.Fill(dt);
                //cryRpt.SetDataSource(dt);
                ////cryRpt.RecordSelectionFormula = 
                //cryRpt.SetParameterValue("Report Name", "Purchase Report");
                //cryRpt.SetParameterValue("ComName", "Orion Foods");
                ////CrystalReportViewer1.ReportSource = cryRpt;
                ////CrystalReportViewer1.DataBind();
                ////ReportDocument rpt = (ReportDocument)Session[sessid];
                //System.IO.Stream oStream = null;
                //byte[] byteArray = null;
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
                //rprt.Load(Server.MapPath("~/CrystalReport.rpt"));
                //con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void getreq()
        {
            try
            {
                string reqid, ReqDat;
                reqid = Request.QueryString["REQID"];
                ReqDat = Request.QueryString["ReqDat"];

                cryRpt.Load(Server.MapPath("rpt_Req.rpt"));
                cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ToString());
                
                if (reqid != null && ReqDat != "")
                {
                    cmd = new SqlCommand("SELECT * from v_Req where MReq_sono = '" + reqid + "' and MReq_dat = '" + ReqDat + "' and isactive = 1", con);
                }
                else if (reqid != null)
                {
                    cmd = new SqlCommand("SELECT * from v_Req where MReq_sono = '" + reqid + "' and isactive = 1 ", con);
                }
                else if (ReqDat != "")
                {
                    cmd = new SqlCommand("SELECT * from v_Req where MReq_dat = '" + ReqDat + "' and isactive = 1", con);

                }else if (reqid == null)
                {
                    cmd = new SqlCommand("SELECT * from v_Req where isactive = 1", con);
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
                cryRpt.SetParameterValue("Report Name", "Requisition Report");
                //cryRpt.SetParameterValue("ComName", "Orion Foods");
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


        private void getstk()
        {
            try {

                Response.Redirect("test.aspx");
            }
            catch(Exception ex)
            {
                throw;
            }
            //try
            //{
            //    string WHID;
            //    WHID = Request.QueryString["WHID"];

            //    cryRpt.Load(Server.MapPath("rpt_stk.rpt"));
            //    cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");
            //    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ToString());

            //    if (WHID == null)
            //    {
            //        cmd = new SqlCommand("SELECT * from v_stck", con);                     
            //    }
            //    else if (WHID != "")
            //    {
            //        cmd = new SqlCommand("SELECT * from v_stck where wh_id = '" + WHID + "'", con);
            //    }
                
            //    con.Open();
            //    //cmd.CommandType = CommandType.StoredProcedure;
            //    //cmd.Parameters.AddWithValue("@PurNo", "PO009");

            //    //cmd.CommandText = proc;
            //    //cmd.Parameters.Add("@PurNo", SqlDbType.VarChar).Value = "PO009";

            //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //    DataTable dt = new DataTable();
            //    sda.Fill(dt);
            //    cryRpt.SetDataSource(dt);
            //    //cryRpt.RecordSelectionFormula = 
            //    cryRpt.SetParameterValue("Report Name", "Stock Report");
            //    //cryRpt.SetParameterValue("ComName", "Orion Foods");
            //    //CrystalReportViewer1.ReportSource = cryRpt;
            //    //CrystalReportViewer1.DataBind();
            //    //ReportDocument rpt = (ReportDocument)Session[sessid];
            //    System.IO.Stream oStream = null;
            //    byte[] byteArray = null;
            //    oStream =
            //    cryRpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //    byteArray = new byte[oStream.Length];
            //    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
            //    Response.ClearContent();
            //    Response.ClearHeaders();
            //    Response.ContentType = "application/pdf";
            //    Response.BinaryWrite(byteArray);
            //    Response.Flush();
            //    Response.Close();
            //    cryRpt.Close();
            //    cryRpt.Dispose();
            //    //rprt.Load(Server.MapPath("~/CrystalReport.rpt"));
            //    con.Close();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
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
                         cmd = new SqlCommand("select * from v_getPur where PurNo = '" + purid + "'");
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
                DataTable dt_=new DataTable();


                if (DATW != null)
                {
                    dt_ = Get_VDATPur(DATW);

                    cryRpt.Load(Server.MapPath("rpt_Pur_Tim_wise.rpt"));

                }
                else if (PURID != null)
                {
                    dt_ = Get_VPur(PURID);
                    cryRpt.Load(Server.MapPath("rpt_Purchase.rpt"));

                }
                else if (MON != null)// || MON != "0")
                {
                    dt_ = Get_VMONPur(MON);

                    cryRpt.Load(Server.MapPath("rpt_Pur_Tim_wise.rpt"));

                }
                else if (VENID != null)
                {
                    dt_ = Get_VenPur();
                    cryRpt.Load(Server.MapPath("rpt_Purchase.rpt"));

                }
                else
                {
                    dt_ = Get_VPur();

                    cryRpt.Load(Server.MapPath("rpt_Purchase.rpt"));
                    
                }

                cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");
                cryRpt.SetDataSource(dt_);
                //cryRpt.RecordSelectionFormula = 

                cryRpt.SetParameterValue("Report Name", "Purchase Report");
                cryRpt.SetParameterValue("ComName", "Orion Foods");

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
        private void getSAL()
        {
            try
            {

                
                cryRpt.Load(Server.MapPath("Rpt_Sal.rpt"));
                cryRpt.SetDatabaseLogon("sa", "friend", "ASAD-PC", "OrionFoods");
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ToString());
                cmd = new SqlCommand("select * from v_rptSal", con);
                if (ckcrdt != null)
                {
                    cmd = new SqlCommand("select * from v_rptSal where iscre = 1", con);
                }
                else if (ckcsh != null)
                {
                    cmd = new SqlCommand("select * from v_rptSal where iscash = 1", con); 
                }
                else if (DatFrm != null || DatTo != null)
                {
                    cmd = new SqlCommand("select * from v_rptSal where Msal_dat  BETWEEN '" + DatFrm + "' AND '" + DatTo + "'", con);
                }
                else if (Cust != null)
                {
                    cmd = new SqlCommand("select * from v_rptSal where CustomerID ='" + Cust + "'", con);
                }
                else if (Sale != null)
                {
                    cmd = new SqlCommand("select * from v_rptSal where MSal_id ='" + Sale + "'", con); 
                }
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                cryRpt.SetDataSource(dt);
                cryRpt.SetParameterValue("Report Name", "Sales Report");
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