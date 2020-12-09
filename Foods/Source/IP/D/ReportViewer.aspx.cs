using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Foods
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Redirect("~/Source/OP/ReportViewer.aspx");
           

            string txtsono, id;

            id = Request.QueryString["ID"];

            txtsono = Request.QueryString["QUOTID"];

            Response.Redirect("~/Source/OP/ReportViewer.aspx?ID=" + id + "&QUOTID=" + txtsono);
        }
    }
}