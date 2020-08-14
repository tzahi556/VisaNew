using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for SendEmailsSchedular
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class SendEmailsSchedular : System.Web.Services.WebService
{

    public SendEmailsSchedular()
    {
       

    }

   

    [WebMethod]
    public void Send()
    {
        DataTable dt = Dal.ExeSp("GetSetSendEmailWeekly", "1", "0", "", "", "", "", "", "", "0", "false", "0", "");

        foreach (DataRow row in dt.Rows)
        {

            int CurrentHour = DateTime.Now.Hour;
            int CurrentDay = (int)DateTime.Now.DayOfWeek;



            bool IsActive = bool.Parse(row["Active"].ToString());
            string DayId = row["DayId"].ToString();
            string HourId = row["HourId"].ToString();



            if (IsActive && CurrentHour.ToString() == HourId && DayId == CurrentDay.ToString())
            {
                string Subject = row["Subject"].ToString();
                string Email = row["Email"].ToString();
                string EmailCopy = row["EmailCopy"].ToString();
                string EmailHide = row["EmailHide"].ToString();
                string Body = row["Body"].ToString();
                string ComaniesIds = row["ComaniesIds"].ToString();
                string Id = row["Id"].ToString();

                SendEmailWeekly sw = new SendEmailWeekly(Subject,"0" + ComaniesIds,Email,EmailCopy,EmailHide,Body, Id);
                
            }


        }


    }

}
