using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;

public partial class LoginMain : System.Web.UI.Page
{
	private const String userNameHintText = "Username";
	private const String passwordHintText = "Password";
    

    protected void Page_Load(object sender, EventArgs e)
    {
		lblMsg.Visible = false;
		txtUserName.Attributes ["Title"] = userNameHintText;
		txtPassword.Attributes ["Title"] = passwordHintText;

       // lblMsg.Text = "";
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string UserName = txtUserName.Text;
        string Password = txtPassword.Text;

		if(UserName == txtUserName.Attributes["Title"]) UserName = null;
		if (Password == txtPassword.Attributes ["Title"]) Password = null;

		lblMsg.Visible = false;

        if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
        {
            DataTable dt = Dal.ExeSp("GetUser",UserName,Password);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                HttpCookie cookie = new HttpCookie("UserData");
                cookie["FullName"] = row["LastName"].ToString() + "  " + row["FirstName"].ToString();
                cookie["CompanyId"] = row["CompanyId"].ToString();
                cookie["RoleId"] = row["RoleId"].ToString();
                cookie["ExpertId"] = row["ExpertId"].ToString();
                cookie["LastName"] = row["LastName"].ToString();
                cookie["FirstName"] = row["FirstName"].ToString();
                cookie["Company"] = row["Company"].ToString();
                cookie.Expires = DateTime.Now.AddYears(90);
                HttpContext.Current.Response.Cookies.Add(cookie);
                
                Response.Redirect("Default.aspx");

            }
            else
            {
                lblMsg.Text = "The Login Name Or Password Is Incorect";
				lblMsg.Visible = true;
            }

        }
        else
        {
            lblMsg.Text = "Password And User Name Required!";
			lblMsg.Visible = true;
        }
    }
}