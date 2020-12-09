using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace Foods.Source.IP.D.Global_Test
{
    public partial class frm_CurrConv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        public decimal ConvertYHOO(decimal amount, string fromCurrency, string toCurrency)
        {
            WebClient web = new WebClient();
            string url = string.Format("http://finance.yahoo.com/d/quotes.csv?e=.csv&f=sl1d1t1&s={0}{1}=X", fromCurrency.ToUpper(), toCurrency.ToUpper());
            string response = web.DownloadString(url);
            string[] values = Regex.Split(response, ",");
            decimal rate = System.Convert.ToDecimal(values[1]);
            return rate * amount;
        }
        protected void Convert(object sender, EventArgs e)
        {

            //lblResult.Text = CurrencyConversion(decimal.Parse(txt_amount.Text), txt_fromCurrency.Text, txt_ToCurrency.Text);
            
           

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WebClient web = new WebClient();
            string url = string.Format("https://www.google.co.in/?gws_rd=ssl#q=USD%20to%20PKR");
            string response = web.DownloadString(url);
            string[] values = Regex.Split(response, ",");
            //decimal rate = System.Convert.ToDecimal(values[1]);
            string rate = values[1].ToString();
            //return rate * 1;
            Label1.Text = rate;
        }
      
    }
}