using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        HttpCookie cookie = null;

        if (HttpContext.Current.Request.Cookies["UserData"] != null)
        {
            cookie = HttpContext.Current.Request.Cookies["UserData"];
        }

        string RoleId = cookie["RoleId"].ToString();

        if (RoleId != "1")
        {
            btnMenu.Visible = false;
			btnMenuContainer.Visible = false;
        }


		string company = null;
        if (cookie["Company"] != null && !String.IsNullOrEmpty(cookie["Company"] + String.Empty))
            company = ",  " + cookie["Company"].ToString();

        spName.InnerText = cookie["FullName"].ToString() + company;
    }
}