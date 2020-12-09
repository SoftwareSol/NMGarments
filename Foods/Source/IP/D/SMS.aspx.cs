using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;


namespace Foods.Source.IP
{
    public partial class SMS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSent_Click(object sender, EventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(5000);
                //label1.Text = "Page refreshed at" + DateTime.Today.ToLongDateString() + "" + DateTime.Now.ToLongTimeString();

                // use the API URL here  
                string strUrl = "http://sendpk.com/api/sms.php?username=923122326301&password=2618&sender=Scopy&mobile=" + TBNo.Text.Trim() + "&message=" + TBMsg.Text.Trim() + "";
                // Create a request object  
                WebRequest request = HttpWebRequest.Create(strUrl);
                // Get the response back  
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                response.Close();
                s.Close();
                readStream.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}