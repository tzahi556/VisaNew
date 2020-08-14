using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;

public partial class Register : System.Web.UI.Page
{
	private const String userNameHintText = "Username";
	private const String passwordHintText = "Password";
    

    protected void Page_Load(object sender, EventArgs e)
    {
		
    }

 
    public void SendClientForm(object sender, EventArgs e)
    {

        string ffdf = "dfdfdf";
        string savePath = "~\\ExpertsUpload\\";

        for (var i = 0; i < Request.Files.Count; i++)
        {
            var postedFileFromClient = Request.Files[i];
            HttpPostedFile postedFile = (HttpPostedFile)postedFileFromClient;
            postedFile.SaveAs(Server.MapPath(savePath + postedFile.FileName));
            // do something with file here
        }


        //int index = 1;
        //foreach (HttpPostedFile postedFile in Request.Files)
        //{
        //    int contentLength = postedFile.ContentLength;
        //    string contentType = postedFile.ContentType;
        //    string fileName = postedFile.FileName;

        //    // postedFile.Save(@"c:\test" + index + ".tmp");

        //    index++;
        //}

    }


    

}