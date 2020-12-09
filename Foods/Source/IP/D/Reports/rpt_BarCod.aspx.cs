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
    public partial class rpt_BarCod : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["D"].ConnectionString);
        DataTable dt_;
        string query, BarId, compnam, prodesc, lblsiz, lblcol, lblpric;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var check = Session["user"];
                if (check != null)
                {
                    BarId = Request.QueryString["StickId"];
                    getbar(BarId);
                    GenerateBarCode1();
                    GenerateBarCode2();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        public void getbar(string barid)
        {
            try
            {
                //query = " select * from tbl_sticker where stickerid= '" + barid + "' and CompanyId = '" +
                    //Session["CompanyID"] + "' and BranchId= '" + Session["BranchID"] + "'";
                query = " select * from tbl_sticker inner join Products on tbl_sticker.ProductID = Products.ProductID " +
                   " where stickerid= '" + barid + "'";

                dt_ = DBConnection.GetQueryData(query);

                if (dt_.Rows.Count > 0)
                {
                    
                    compnam = dt_.Rows[0]["compnam"].ToString();
                    prodesc = dt_.Rows[0]["ProductID"].ToString();
                    lblsiz = dt_.Rows[0]["siz"].ToString();
                    lblcol = dt_.Rows[0]["color"].ToString();
                    lblpric = dt_.Rows[0]["Price"].ToString();

                    lbl_Comp1.Text = compnam;
                    lbl_comp2.Text = compnam;

                    lbl_pric1.Text = lblpric;
                    lbl_pric2.Text = lblpric;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenerateBarCode1()
        {
            //string barCode = prodesc + "" + lblsiz + "" + lblcol;
            string barCode = prodesc + "" + lblsiz;
            barcode1.Text = barCode;
            barcode1.Style.Add("font-family", "'Libre Barcode 39 Extended Text' !important");
            barcode1.Style.Add("font-size", "41px");
            barcode1.Style.Add("font-weight", "normal");
          
            //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            //using (Bitmap bitMap = new Bitmap(barCode.Length * 20, 80))
            //{
            //    using (Graphics graphics = Graphics.FromImage(bitMap))
            //    {
            //        //Font oFont = new Font("IDAutomationHC39M", 16);
            //        Font oFont = new Font("IDAHC39M Code 39 Barcode", 16);
            //        //Font oFont = new Font("IDAutomationSC39L Demo", 16);

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

            //}
        }
        private void GenerateBarCode2()
        {
            string barCode_2 = prodesc + "" + lblsiz;

            barcode2.Text = barCode_2;
            barcode2.Style.Add("font-family", "'Libre Barcode 39 Extended Text' !important");
            barcode2.Style.Add("font-size", "41px");
            barcode2.Style.Add("font-weight", "normal");
            

            //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            //using (Bitmap bitMap = new Bitmap(barCode.Length * 20, 80))
            //{
            //    using (Graphics graphics = Graphics.FromImage(bitMap))
            //    {
            //        //Font oFont = new Font("IDAutomationHC39M", 16);
            //        Font oFont = new Font("IDAHC39M Code 39 Barcode", 16);
            //        //Font oFont = new Font("IDAutomationSC39L Demo", 16);

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

    }
}