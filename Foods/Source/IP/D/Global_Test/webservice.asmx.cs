using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Script.Services;

namespace Foods.Source.IP.D.Global_Test
{
    /// <summary>
    /// Summary description for webservice
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class webservice : System.Web.Services.WebService
    {

        [WebMethod]
        public decimal ConvertYHOO(decimal amount, string fromCurrency, string toCurrency)
        {
            WebClient web = new WebClient();
            string url = string.Format("http://finance.yahoo.com/d/quotes.csv?e=.csv&f=sl1d1t1&s={0}{1}=X", fromCurrency.ToUpper(), toCurrency.ToUpper());
            string response = web.DownloadString(url);
            string[] values = Regex.Split(response, ",");
            decimal rate = System.Convert.ToDecimal(values[1]);
            return rate * amount;
        }
    }
}
