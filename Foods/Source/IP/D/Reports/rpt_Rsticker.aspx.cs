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
    public partial class rpt_Rsticker : System.Web.UI.Page
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
                    GenerateBarCode1();
                    GenerateBarCode2();
                    
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }


        private void GenerateBarCode1()
        {
            string barCode = lbl_barcd.Text;

            barcode1.Text = barCode;
            barcode1.Style.Add("font-family", "'Libre Barcode 39 Extended Text' !important");
            barcode1.Style.Add("font-size", "30px");
            barcode1.Style.Add("font-weight", "normal");
            //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            //using (Bitmap bitMap = new Bitmap(barCode.Length * 20, 80))
            //{
            //    using (Graphics graphics = Graphics.FromImage(bitMap))
            //    {
            //        //Font oFont = new Font("IDAutomationHC39M", 16);
            //        //Font oFont = new Font("IDAHC39M Code 39 Barcode", 16);
            //        // IDAutomationSC39XL Demo
            //        Font oFont = new Font("IDAHC39M Code 39 Barcode", 16);
                    
            //        PointF point = new PointF(2f, 2f);
            //        SolidBrush blackBrush = new SolidBrush(Color.Black);
            //        SolidBrush whiteBrush = new SolidBrush(Color.White);
            //        graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
            //        //graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
            //        graphics.DrawString(" " + barCode + " ", oFont, blackBrush, point);

            //    }
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //        byte[] byteImage = ms.ToArray();

            //        Convert.ToBase64String(byteImage);
            //        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            //    }
            //    PlaceHolder1.Controls.Add(imgBarCode);
           // }
        }

        private void GenerateBarCode2()
        {
            string barCode = lbl_barcd1.Text;
            barcode2.Text = barCode;
            barcode2.Style.Add("font-family", "'Libre Barcode 39 Extended Text' !important");
            barcode2.Style.Add("font-size", "30px");
            barcode2.Style.Add("font-weight", "normal");
            //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            //using (Bitmap bitMap = new Bitmap(barCode.Length * 20, 80))
            //{
            //    using (Graphics graphics = Graphics.FromImage(bitMap))
            //    {
            //        //Font oFont = new Font("IDAutomationHC39M", 16);
            //        //Font oFont = new Font("IDAHC39M Code 39 Barcode", 16);
            //        Font oFont = new Font("IDAHC39M Code 39 Barcode", 16);

            //        PointF point = new PointF(2f, 2f);
            //        SolidBrush blackBrush = new SolidBrush(Color.Black);
            //        SolidBrush whiteBrush = new SolidBrush(Color.White);
            //        graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
            //        //graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
            //        graphics.DrawString(" " + barCode + " ", oFont, blackBrush, point);

            //    }
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //        byte[] byteImage = ms.ToArray();

            //        Convert.ToBase64String(byteImage);
            //        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            //    }
            //    PlaceHolder2.Controls.Add(imgBarCode);
            //}
        }
        public void getstk(string stkid)
        {
            try
            {
                query = " select * from tbl_Rsticker where stickerid= '" + stkid + "' and CompanyId = '" +
                    Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    lbl_compnam.Text = dt_.Rows[0]["compnam"].ToString();
                    lbl_comp1.Text = dt_.Rows[0]["compnam"].ToString();

                    lbl_prodesc.Text = dt_.Rows[0]["prodesc"].ToString();
                    lbl_prodesc1.Text = dt_.Rows[0]["prodesc"].ToString();

                    lbl_barcd.Text = dt_.Rows[0]["Barcod"].ToString();
                    lbl_barcd1.Text = dt_.Rows[0]["Barcod"].ToString();

                    lblpric.Text = dt_.Rows[0]["Price"].ToString();
                    lblpric1.Text = dt_.Rows[0]["Price"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}