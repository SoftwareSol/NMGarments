using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Foods
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var check = Session["user"];
            if (check != null)
            {
                try
                {
                    lblUserName.Text = Session["Name"].ToString();
                    lbl_terminal.Text = Session["BranchName"].ToString();
                    //lbl_Comp.Text = Session["Company"].ToString();
                    //lbl_logo.Text = Session["Company"].ToString();
                    //imglogo.Src ="img/" +  Session["CompanyImg"];
                    //imglogo.Alt = Session["Company"].ToString();
                    BindMenu();
                    lbl_version.Text = "1.0";

                   
                }

                catch { Response.Redirect("~/Login.aspx"); }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void LinkBtnlogout_Click(object sender, EventArgs e)
        {
            Session["user"] = null;
            Response.Redirect("~/Login.aspx");
        }

        protected void rptMenu_OnItemBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptSubMenu = e.Item.FindControl("rptChildMenu") as Repeater;
                //rptSubMenu.DataSource = GetData("SELECT SubMenuId,SubMenuNam,SubMenuDesc,SubMenuUrl FROM tbl_SubMenu WHERE Isview = 'True' and MenuId =" + ((System.Data.DataRowView)(e.Item.DataItem)).Row[0]);

                rptSubMenu.DataSource = GetData(" SELECT tbl_SubMenu.SubMenuId,SubMenuNam,SubMenuDesc,SubMenuUrl FROM tbl_SubMenu " +
                    " inner join tbl_usrmen on tbl_SubMenu.SubMenuId = tbl_usrmen.SubMenuId " +
                    " inner join users on tbl_usrmen.Username = users.Username " +
                    " WHERE Isview = 'True' and tbl_usrmen.MenuId = " + ((System.Data.DataRowView)(e.Item.DataItem)).Row[0] +
                    " and users.[Level] = '" + Session["Level"] + "' and tbl_usrmen.Username = '" + Session["Username"] + "'");

                rptSubMenu.DataBind();

            }
        }

        private void BindMenu()
        {
            //this.rptMenu.DataSource = GetData("select * from tbl_usrmen " +
            //    " inner join tbl_Menu on tbl_usrmen.MenuId = tbl_Menu.MenuId " +
            //    " inner join tbl_SubMenu on tbl_usrmen.SubMenuId = tbl_SubMenu.SubMenuId " +
            //    " inner join Users on tbl_usrmen.Username = Users.Username " +
            //    " where tbl_usrmen.Username = '" + Session["Username"] + "'");
            this.rptMenu.DataSource = GetData(" select tbl_Menu.menuid,title,* from tbl_menusr  " +
                " inner join tbl_Menu on tbl_menusr.MenuId = tbl_Menu.MenuId " +
                " where tbl_menusr.Username = '" + Session["Username"] + "'");

            this.rptMenu.DataBind();
        }

        private DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["D"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }

        protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptSubMenu = e.Item.FindControl("rptChildMenu") as Repeater;
                //rptSubMenu.DataSource = GetData("SELECT SubMenuId,SubMenuNam,SubMenuDesc,SubMenuUrl FROM tbl_SubMenu WHERE Isview = 'True' and MenuId =" + ((System.Data.DataRowView)(e.Item.DataItem)).Row[0]);
                rptSubMenu.DataSource = GetData(" SELECT tbl_SubMenu.SubMenuId,SubMenuNam,SubMenuDesc,SubMenuUrl FROM tbl_SubMenu "+
                    " inner join tbl_usrmen on tbl_SubMenu.SubMenuId = tbl_usrmen.SubMenuId "  +
                    " inner join users on tbl_usrmen.Username = users.Username " +
                    " WHERE Isview = 'True' and tbl_usrmen.MenuId = " +((System.Data.DataRowView)(e.Item.DataItem)).Row[0] +
                    " and users.[Level] = '" + Session["Level"] + "' and tbl_usrmen.Username = '" + Session["Username"] + "'");

                rptSubMenu.DataBind();
            }
        }
    }
}