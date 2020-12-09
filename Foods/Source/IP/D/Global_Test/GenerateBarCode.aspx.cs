using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;

namespace Foods.Source.IP.D.Global_Test
{
    public partial class GenerateBarCode : System.Web.UI.Page
    {
        //https://www.c-sharpcorner.com/blogs/how-to-generate-barcode-in-asp-net

        //https://www.idautomation.com/free-barcode-products/code39-font/

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnGenrate_Click(object sender, EventArgs e)
        {
            string barCode = txtBarcode.Text;
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            using (Bitmap bitMap = new Bitmap(barCode.Length * 20, 80))
            {
                using (Graphics graphics = Graphics.FromImage(bitMap))
                {
                    //Font oFont = new Font("IDAutomationHC39M", 16);
                    Font oFont = new Font("IDAHC39M Code 39 Barcode", 16);
                    PointF point = new PointF(2f, 2f);
                    SolidBrush blackBrush = new SolidBrush(Color.Black);
                    SolidBrush whiteBrush = new SolidBrush(Color.White);
                    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                    graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();

                    Convert.ToBase64String(byteImage);
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                PlaceHolder1.Controls.Add(imgBarCode);
            }
        }  
    }
}