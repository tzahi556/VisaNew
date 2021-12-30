using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

using System.Web.Services.Protocols;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Text;


/// <summary>
/// Summary description for SendEmailAuto
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SendEmailAuto : System.Web.Services.WebService
{

    public SendEmailAuto()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void Send()
    {
        //SendEmailWeekly dd = new SendEmailWeekly();
        SendEmail sm = new SendEmail(DateTime.Now.ToString());
        SendEmailIncoming smincoming = new SendEmailIncoming(DateTime.Now.ToString());
        //return "Hello World";
    }

}
