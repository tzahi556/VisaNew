using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Net;

/// <summary>
/// Summary description for SendEmail
/// </summary>
public class SendEmail
{

    public DataTable dtMailList;

    public DataTable dtUsers;

    public string officeMails;

    public SendEmail(string Date)
    {


        dtMailList = Dal.ExeSp("GetEmailList", Date);

        dtUsers = Dal.ExeSp("GetUsersTable");

        DataRow[] result = dtUsers.Select("IsEmail=True And Email<>'' And Email Is Not Null");


        bool isFirst = true;


        foreach (DataRow row in result)
        {

            if (isFirst)
            {
                officeMails = row["Email"].ToString();
                isFirst = false;
            }
            else
            {
                officeMails += "," + row["Email"].ToString();
            }


        }

        PrepareDataSend();

        PrepareOfficeDataSend();

    }

    private void PrepareOfficeDataSend()
    {
        BuildOfficeEmailMessageCompany();
    }

    private void BuildOfficeEmailMessageCompany()
    {


        string body = "";
        string ContactManEmail = "";
        string CompanyIsEmail = "";

        string ContactMan = "";
        string header = "";
        string subtable = "";
        string subject = "Expert Visa";

        int counter = 1;

        DataTable dtOfficeMailList = Dal.ExeSp("GetOfficeEmailList");

        foreach (DataRow row in dtOfficeMailList.Rows)
        {


            string IsEmail = row["IsEmail"].ToString();
            string Passport = row["Passport"].ToString();
            string Email = row["Email"].ToString();
            string Surname = row["Surname"].ToString();
            string Name = row["Name"].ToString();
            string CompanyName = row["CompanyName"].ToString();
            string Level = row["LevelId"].ToString();
            string Wating = row["Wating"].ToString();
            string IsMonthly = (row["IsMonthly"].ToString() == "True") ? "Yes" : "";
            //CompanyIsEmail = row["CompanyIsEmail"].ToString();
            //ContactMan = row["ContactMan"].ToString();
            //ContactManEmail = row["ContactManEmail"].ToString();




            ArrayList ar = GetTypeObject("8", row);

            if (string.IsNullOrEmpty(header))
            {
                subtable = ar[4].ToString();

                subject = "Dg Law Office< - " + ar[0].ToString();
                header = "Dear <b>Dg Law Office</b>. <br><br>" + ar[1]
                    + "<br><br><table cellspacing='0' cellpadding='0'  style='border-collapse: collapse;border:solid 1px #D3DFEE' width='85%'>"
                    + "<tr style='height:27px;'><td colspan='8' style='font-weight:bold;color:white;background:#4F81BD;text-align:center'>" + ar[2] + "</td></tr>"
                    + "<tr style='height:27px;font-weight:bold;background: #D3DFEE;'>"
                        + "<td  align='left'>&nbsp</td>"
                        + "<td  align='left'>Surname</td>"
                        + "<td align='left'>Name</td>"
                        + "<td align='left'>Passport Number</td>"
                        + "<td align='left'>Company Name</td>"
                        + "<td align='left'>Level</td>"
                        + "<td align='left'>30 Days</td>"
                        + "<td align='left'>Wating</td>"
                    + "</tr>";

            }


            //string onebody = "<tr style='height:25px;background:white;border-bottom:solid 1px #D3DFEE'><td align='center'><b>1</b></td><td align='left'>" + Surname + "</td><td align='left'>" + Name + "</td><td align='left'>"
            //         + Passport + "</td><td align='left'>" + ar[5] + "</td><td align='left'>" + ar[7].ToString() + "</td><td align='left'>" + ar[9].ToString() + "</td></tr>";



            body += "<tr style='height:25px;background:white;'>"
                       + "<td style='border-bottom:solid 1px #D3DFEE' align='center'><b>" + counter.ToString() + "</b>&nbsp;</td>"
                       + "<td align='left' style='border-bottom:solid 1px #D3DFEE'><div style='text-overflow: ellipsis; overflow: hidden; width: 80px; height: 1.2em; white-space: nowrap;'>" + Surname + "</div></td>"
                       + "<td style='border-bottom:solid 1px #D3DFEE' align='left'>" + Name + "</td>"
                       + "<td style='border-bottom:solid 1px #D3DFEE' align='left'>" + Passport + "</td>"
                       + "<td style='border-bottom:solid 1px #D3DFEE' align='left' ><div style='text-overflow: ellipsis; overflow: hidden; width: 160px; height: 1.2em; white-space: nowrap;'>" + CompanyName + "</div></td>"
                       + "<td style='border-bottom:solid 1px #D3DFEE' align='left'>" + Level + "</td>"
                       + "<td style='border-bottom:solid 1px #D3DFEE' align='left'>" + IsMonthly + "</td>"
                       + " <td style='border-bottom:solid 1px #D3DFEE' align='left'><b>" + Wating.ToString() + "</b></td>"
                   + "</tr>";

            counter++;

            // ar.Add(" Office Data");
            // ar.Add("This Experts Not Start Any Steps:");
            //ar.Add("Experts List");
            //ar.Add("B-1 VISA Invitation Issue date");
            //ar.Add("<br><br>" + GetInviteSubText());
            //ar.Add(row["Visa/ Invitation Issue date"].ToString());
            //ar.Add("");
            //ar.Add("");
            //ar.Add("");
            //ar.Add("");

        }



        if (!string.IsNullOrEmpty(header)) //להוסיף במקרה של משלוח אוטמטי לחברות //&& CompanyIsEmail == "True" && !string.IsNullOrEmpty(ContactManEmail))
        {
            Send(subject, header + body + "</table>" + subtable, "");
        }


    }

    private void PrepareDataSend()
    {
        string Prev_CompanyId = "";
        string CompanyId = "";
        string type = "";
        string Prev_type = "";
        foreach (DataRow row in dtMailList.Rows)
        {


            CompanyId = row["CompanyId"].ToString();
            type = row["type"].ToString();

            if (Prev_CompanyId != CompanyId || Prev_type != type)
            {
                BuildEmailMessageCompany(CompanyId, type);

                Prev_CompanyId = CompanyId;

                Prev_type = type;
            }

        }
    }

    private void BuildEmailMessageCompany(string CompanyId, string type)
    {

        //string 
        string body = "";
        string ContactManEmail = "";
        string CompanyIsEmail = "";

        string ContactMan = "";
        string header = "";
        string subtable = "";
        string subject = "Expert Visa";

        int counter = 1;

        DataRow[] result = dtMailList.Select("CompanyId=" + CompanyId + "And type=" + type);

        foreach (DataRow row in result)
        {


            string IsEmail = row["IsEmail"].ToString();
            string Passport = row["Passport"].ToString();
            string Email = row["Email"].ToString();
            string Surname = row["Surname"].ToString();
            string Name = row["Name"].ToString();
            string CompanyName = row["CompanyName"].ToString();



            CompanyIsEmail = row["CompanyIsEmail"].ToString();
            ContactMan = row["ContactMan"].ToString();
            ContactManEmail = row["ContactManEmail"].ToString();

            // במקרה של פתיחה גם למומחים
            //if ((CompanyIsEmail == "False" || string.IsNullOrEmpty(ContactManEmail)) && (IsEmail == "False" || string.IsNullOrEmpty(Email)))
            //    break;

            //his passport
            ArrayList ar = GetTypeObject(type, row);

            if (string.IsNullOrEmpty(header))
            {
                subtable = ar[4].ToString();
                string FollowingTitle = ar[1].ToString();
                if (result.Length == 1)
                {
                    FollowingTitle = ar[1].ToString().Replace("Experts", "Expert");
                    subtable = subtable.Replace("their Passports", "his Passport").Replace("Experts", "Expert");
                }

                // string FollowingTitle =(result.Length ==1) ? (ar[1].ToString().Replace("Experts", "Expert")):ar[1].ToString();

                subject = CompanyName + " - " + ar[0].ToString();
                header = "Dear <b>" + CompanyName + "</b>. <br><br>" + FollowingTitle
                    + "<br><br><table cellspacing='0' cellpadding='0' style='border-collapse: collapse;border:solid 1px #D3DFEE' width='85%'>"
                    + "<tr style='height:27px;'><td colspan='7' style='font-weight:bold;color:white;background:#4F81BD;text-align:center'>" + ar[2] + "</td></tr>"
                    + "<tr style='height:27px;font-weight:bold;background: #D3DFEE;'><td align='left'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td  align='left'>Surname</td><td align='left'>Name</td><td align='left'>Passport Number</td><td align='left'>" + ar[3] + "</td><td align='left'>" + ar[6].ToString() + "</td><td align='left'>" + ar[8].ToString() + "</td></tr>";

            }


            string onebody = "<tr style='height:25px;background:white;border-bottom:solid 1px #D3DFEE'><td align='center'><b>1</b></td><td align='left'>" + Surname + "</td><td align='left'>" + Name + "</td><td align='left'>"
                     + Passport + "</td><td align='left'>" + ar[5] + "</td><td align='left'>" + ar[7].ToString() + "</td><td align='left'>" + ar[9].ToString() + "</td></tr>";



            body += "<tr style='height:25px;background:white;'><td style='border-bottom:solid 1px #D3DFEE' align='center'><b>" + counter.ToString() + "</b></td><td align='left' style='border-bottom:solid 1px #D3DFEE'>" + Surname + "</td><td style='border-bottom:solid 1px #D3DFEE' align='left'>" + Name + "</td><td style='border-bottom:solid 1px #D3DFEE' align='left'>"
                    + Passport + "</td><td style='border-bottom:solid 1px #D3DFEE' align='left'>" + ar[5] + "</td><td style='border-bottom:solid 1px #D3DFEE' align='left'>" + ar[7].ToString() + "</td><td style='border-bottom:solid 1px #D3DFEE' align='left'>" + ar[9].ToString() + "</td></tr>";

            counter++;


            ////רק באישור יוסף לפתוח כאשר פותחים גם למומחה פנימי 
            //if (IsEmail == "True" && !string.IsNullOrEmpty(Email))
            //{
            //     Send(subject, header.Replace(CompanyName, Surname + "  " + Name) + onebody + "</table>" + subtable, Email);
            //}


        }



        if (!string.IsNullOrEmpty(header)) //להוסיף במקרה של משלוח אוטמטי לחברות //&& CompanyIsEmail == "True" && !string.IsNullOrEmpty(ContactManEmail))
        {

          

            Send(subject, header + body + "</table>" + subtable, ContactManEmail);
        }


    }

    private ArrayList GetTypeObject(string type, DataRow row)
    {
        ArrayList ar = new ArrayList();

        if (type == "1")
        {
            ar.Add("Approval Submission");
            ar.Add("I would like to inform you that we have submitted the Approval Applications for the following Experts:");
            ar.Add("STEP 1 – Approval in Process");
            ar.Add("Submission Date");
            ar.Add("<br><br>We will update with progress. <br><br>Standard Approval process is 60 days.<br><br>");
            ar.Add(row["Approval Submition Date"].ToString());
            ar.Add("Application ref");
            ar.Add(row["Applicationref"].ToString());
            ar.Add("");
            ar.Add("");


        }


        if (type == "2")
        {
            ar.Add("Permit Approval");
            ar.Add("I am happy to inform you that we have received the Permit Approval for the following Experts:");
            ar.Add("Permit Approval");
            ar.Add("Permit Approval is Valid from Date");
            ar.Add("<br><br>Next step would be to address the Interior Office and issue STEP 2 Visa Invitation.");
            ar.Add(row["Approval From Date"].ToString());
            ar.Add("Approval Expiration Date");
            ar.Add(row["Approval Exp Date"].ToString());
            ar.Add("");
            ar.Add("");

        }

        if (type == "3")
        {
            ar.Add("B-1 VISA Invitation Issue Date");
            ar.Add("I am glad to inform you we issued the B-1 Work Permit Invitation for the following Experts:");
            ar.Add("ISSUED – Please Enter Israel");
            ar.Add("B-1 VISA Invitation Issue date");
            ar.Add("<br><br>" + GetInviteSubText());
            ar.Add(row["Visa/ Invitation Issue date"].ToString());
            ar.Add("");
            ar.Add("");
            ar.Add("");
            ar.Add("");


        }



        if (type == "4")
        {
            ar.Add("Visa process completed");
            ar.Add("I am glad to inform you that we have issued the Multiple Entry Visa for the following Experts:");
            ar.Add("COMPLETED!");
            ar.Add("Multiple Issue Visa");
            ar.Add("<br><br>The Immigration process for this Experts is completed!<br>Please arrange for their Passports to be collected from our office.");
            ar.Add(row["Multiple Issue Visa"].ToString());

            ar.Add("Work Permit <br>Expiration Date");
            ar.Add(row["Visa Exp Date"].ToString());

            ar.Add("Multiple Visa <br> Expiration Date");
            ar.Add(row["Multiple entry Visa"].ToString());



        }

        // orange
        if (type == "5")
        {
            ar.Add("Visa Expiration 120 Days");
            ar.Add("I Would like to inform you that the B1 Working Visa for the following Experts are about to expire.<br> Should you require to extend the B1 working <br> Visa please inform us a soon as possible,<br> that will allow us sufficient time to start working on the B1 extension process.");
            ar.Add("Visa Expiration 120 Days");
            ar.Add("B1 Expiration Date");
            ar.Add("<br><br>");
            ar.Add(row["Visa Exp Date"].ToString());
            ar.Add("Remaining Days to Visa expiration");
            ar.Add("120");
            ar.Add("");
            ar.Add("");
        }


        // red
        if (type == "6")
        {
            ar.Add("Visa Expiration 90 Days");
            ar.Add("I Would like to inform you that the B1 Working Visa for the following Experts are about to expire.<br> Should you require to extend the B1 working  Visa please inform us a soon as possible,<br> that will allow us sufficient time to start working on the B1 extension process.");
            ar.Add("Visa Expiration 90 Days");
            ar.Add("B1 Expiration Date");
            ar.Add("<br><br>");
            ar.Add(row["Visa Exp Date"].ToString());
            ar.Add("Remaining Days to Visa expiration");
            ar.Add("90");
            ar.Add("");
            ar.Add("");
        }


        // visa expire
        if (type == "7")
        {
            ar.Add("Visa Expiration");
            ar.Add("I Would like to inform you that the B1 Working Visa for the following Experts are about to expire.<br> Should you require to extend the B1 working Visa please inform us a soon as possible,<br> that will allow us sufficient time to start working on the B1 extension process.");
            ar.Add("Visa Expiration");
            ar.Add("B1 Expiration Date");
            ar.Add("<br><br>");
            ar.Add(row["Visa Exp Date"].ToString());
            ar.Add("");
            ar.Add("");
            ar.Add("");
            ar.Add("");
        }


        if (type == "8")
        {
            ar.Add(" Office Data");
            ar.Add("This Experts Not Start Any Steps:");
            ar.Add("Experts List");
            ar.Add("");
            ar.Add("");
            ar.Add("");
            ar.Add("");
            ar.Add("");
            ar.Add("");
            ar.Add("");


        }

        return ar;

    }

    private string GetInviteSubText()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append(@"

            The Experts can enter Israel, activate their Work Permits upon entry and start working immediately.<br>
            Please see attached Interior office receipts for this Visa payments.<br>
            Please print the receipts and have the Experts carry it with them, in case needed to be presented upon entry.<br>
 
            The application for the Experts was granted and the Work Permits (WP) are available at the Interior Office system.<br>
            (We asked for the invitation NOT be sent to the Israeli consulate in London, to allow the Experts to collect the permits upon entry to Israel).<br><br>

            <b>Next Steps:</b><br><br>
            Work Visa Issue -<br>
            1.     Upon Entry, the Immigration officer at the airport may address the Experts to the Interior office located at Ben Gurion Airport.<br><br>

            2.     The Interior office will issue 1 year B-1 Work Permits to be stamped on the Expert passports.<br><br>

            3.     Upon entry, “White Slip” would be issued to the Experts. The Slip should indicate B-1 Visa (Work Permit).<br><br><br>

 
            <b>Multiple Entry Visa -</b><br><br>
            1.     After Entry, we would address the Interior Office (IO) in Tel Aviv again (We will do this after collecting the original passports from the Experts – Please arrange this).<br><br>

            2.     Upon this meeting we will issue Multiple Entry Visas.<br><br>

            3.     We will do this about 1-2 weeks after the Experts entry to Israel.<b> During this time they will not be able to leave the country.</b><br><br>

            4.     Once the Multiple Entry Visas would be issued the Experts will be free to leave and re-enter.<br><br>

            5.     In addition, in case the IO in Ben Gurion airport would not issue the work permits as needed (as described above) we will address the IO in Tel Aviv, within 2 weeks after entry, as planned, and will issue the work permits needed.<br><br><br><br>


            <b>Please keep us updated with the Experts travel plans to allow us to plan the next steps.</b><br>
            <b>Please forward us the Experts Passport after entry to Israel to allow the Multiple Entry Visa to be issued.</b><br>






            ");


        return sb.ToString();
    }

    //public void Send(string Subject, string Body, string To)
    //{

    //    Body = GetHeader() + Body + GetFooter();


    //    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
    //    MailMessage actMSG = new MailMessage();

    //    SmtpServer.Credentials = new System.Net.NetworkCredential("brokeryogev@gmail.com", "broker556");
    //    SmtpServer.EnableSsl = true;



    //    actMSG.Subject = Subject;
    //    actMSG.IsBodyHtml = true;
    //    actMSG.Body = String.Format("{0}", Body);



    //  //  actMSG.To.Add("tzahi556@gmail.com");


    //    if (!string.IsNullOrEmpty(officeMails))
    //    {
    //        actMSG.To.Add(officeMails);
    //    }

    //    actMSG.From = new MailAddress("ggg@gmail.com");

    //    SmtpServer.Send(actMSG);
    //    actMSG.Dispose();
    //}

    public void Send(string Subject, string Body, string To)
    {

        // System.Threading.Thread.Sleep(30000);
        //  
        //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        //using (SmtpClient client = new SmtpClient("smtp.office365.com", 587))
        //{
        //    MailMessage actMSG = new MailMessage();
        //    try
        //    {

        //        Body = GetHeader() + Body + GetFooter();
        //                  // client.UseDefaultCredentials = false;
        //                  // client.Credentials = new System.Net.NetworkCredential("dglaw@dgtracking.co.il", "Zux74633"); //

        //        client.Credentials = new System.Net.NetworkCredential("dglaw@dgtracking.co.il", "Zux74633"); //
        //        client.EnableSsl = true;

        //        // client.DeliveryMethod = SmtpDeliveryMethod.Network;

        //        actMSG.IsBodyHtml = true;

        //        actMSG.Subject = Subject;
        //        actMSG.Body = String.Format("{0}", Body);
        //        actMSG.From = new MailAddress("dglaw@dgtracking.co.il");

        //        //   actMSG.To.Add("tzahi556@gmail.com");


        //        // client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

        //        if (!string.IsNullOrEmpty(officeMails))
        //        {
        //            actMSG.To.Add(officeMails);
        //        }

        //        //string userState = "test message1";

        //        client.Send(actMSG);

        //        Dal.ExecuteNonQuery("Insert Into LogAuto ([Subject], Error, [To]) values(N'" + Subject + "','','" + officeMails + "') ");

        //      //  actMSG.Dispose();

        //    }
        //    catch (Exception ex)
        //    {
        //        Dal.ExecuteNonQuery("Insert Into LogAuto ([Subject], Error, [To]) values(N'" + Subject + "',N'" + ex.Message.ToString() + ' ' + ex.InnerException + "','" + officeMails + "') ");
        //        // actMSG.Dispose();

        //        System.Threading.Thread.Sleep(1000);
        //        Send(Subject, Body, To);
        //       // HttpContext.Current.Response.Write(ex.Message);
        //    }


        //}





          Body = GetHeader() + Body + GetFooter();
        // SmtpClient SmtpServer = new SmtpClient();
        // MailMessage actMSG = new MailMessage();
        // SmtpServer.Host = "smtp.office365.com";
        // SmtpServer.Port = 587;


        // string mail_user = "dglaw@dgtracking.co.il";
        // string mail_pass = "But60041";
        // SmtpServer.EnableSsl = true;
        //// SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        // SmtpServer.Credentials = new System.Net.NetworkCredential(mail_user, mail_pass);
        // //SmtpServer.UseDefaultCredentials = false;

        // actMSG.IsBodyHtml = true;

        // actMSG.Subject = Subject;
        // actMSG.Body = String.Format("{0}", Body);

        //// actMSG.To.Add("yossi@louk.com");
        // actMSG.To.Add("tzahi556@gmail.com");

        // //if (!string.IsNullOrEmpty(officeMails))
        // //{
        // //    actMSG.To.Add(officeMails);
        // //}

        //// actMSG.From = new MailAddress("dglaw@dgtracking.co.il");


        // try
        // {

        //     SmtpServer.Send(actMSG);
        //     actMSG.Dispose();
        // }
        // catch (Exception ex)
        // {
        //     HttpContext.Current.Response.Write(ex.Message);
        // }



        SmtpClient SmtpServer = new SmtpClient();
        MailMessage actMSG = new MailMessage();
        SmtpServer.Host = "dgtracking.co.il";
        SmtpServer.Port = 25;



        //SmtpServer.UseDefaultCredentials = false;

        string mail_user = "dglawmails@dgtracking.co.il";
        string mail_pass = "Jadekia556";

        SmtpServer.Credentials = new System.Net.NetworkCredential(mail_user, mail_pass);


        actMSG.IsBodyHtml = true;

        actMSG.Subject = Subject;
        actMSG.Body = String.Format("{0}", Body);

        //   actMSG.To.Add("yossi@louk.com");
        //actMSG.To.Add("tzahi556@gmail.com");

        if (!string.IsNullOrEmpty(officeMails))
        {
            actMSG.To.Add(officeMails);
        }

        //if (!string.IsNullOrEmpty(To))
        //{
        //    actMSG.To.Add(To);

        //}


        actMSG.From = new MailAddress("dglawmails@dgtracking.co.il");


        try
        {

            SmtpServer.Send(actMSG);
            actMSG.Dispose();
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(ex.Message);
        }

    }

    private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        // Get the unique identifier for this asynchronous operation.
        String token = (string)e.UserState;


        string[] tokenList = token.Split('|');
        string Subject = tokenList[0];
        string EmailList = "";
        if (tokenList.Length > 1)
        {
            EmailList = tokenList[1];

        }

        //if (e.Cancelled)
        //{
        //    Console.WriteLine("[{0}] Send canceled.", token);
        //}
        if (e.Error != null)
        {

            // Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            Dal.ExecuteNonQuery("Insert Into LogAuto ([Subject], Error, [To]) values(N'" + Subject + "',N'" + e.Error + "','" + EmailList + "') ");

        }
        else
        {
            Dal.ExecuteNonQuery("Insert Into LogAuto ([Subject], Error, [To]) values(N'" + Subject + "','','" + EmailList + "') ");
        }





    }

    private string GetHeader()
    {
        StringBuilder sb = new StringBuilder();

        //<span style='color:red'>**</span><u style='font-family: Arial,sans-serif;font-size: 10.5pt;'>This in an automated mail notification, <b>do not reply</b> to this mail </u> <span style='color:red'>**</span>
        sb.Append(@"<html><head></head><body dir='ltr' style='padding:5px'>
               
              
           ");


        return sb.ToString();
    }

    private string GetFooter()
    {
        StringBuilder sb = new StringBuilder();


        sb.Append(@"

    <div><br><br><br>Best Regards,<br><br></div>
    


    <table class='' border='0' cellspacing='0' cellpadding='0' style='width: 100%; border-collapse: collapse;'>
       
       
       <tr>
        <td rowspan='5'>
          <img alt='' src='http://dgtracking.co.il/images/dg.png' />
       
        </td>
       
       </tr>
       
        <tr>
            <td valign='top' style='height: 30.1pt'>
                <p class='' style='margin-bottom: 0cm; margin-bottom: .0001pt; line-height: normal;
                    font-weight: bold; font-size: 12.0pt; font-family: Georgia,serif; color: #365F91'>
                    Kobi (Yaakov) Neeman , Adv.
                    <br />
                    Dardik Gross &amp; Co. Law Firm
                </p>
            </td>
        </tr>
        <tr style='height: 2.25pt'>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style='font-size: 9.0pt; font-family: Arial,sans-serif; color: #5F5F5F;'>
                Abba Hillel Silver Rd 14 &nbsp; &nbsp; &nbsp; &nbsp; Tel: <span style='color: blue'>
                    <u>+972 3 6122624</u></span> &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <a href='http://www.dglaw.co.il/'
                        target='_blank'><span style='font-size: 9.0pt; font-family: Arial,sans-serif; color: #5F5F5F;
                            text-decoration: none; text-underline: none'>www.dglaw.co.il</span></a>
                <br />
                Ramat Gan, Israel 52506 &nbsp; &nbsp; &nbsp; Fax: <span style='color: blue'><u>+972
                    3 6122587 </u></span>&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp; <a href='mailto:neeman@dglaw.co.il'
                        target='_blank'><span style='font-size: 9.0pt; font-family: Arial,sans-serif; color: #365F91;
                            text-decoration: none; text-underline: none'>neeman@dglaw.co.il</span></a>
            </td>
        </tr>
        <!--  <tr>
            <td valign='top' style='padding: 0cm 5.4pt 0cm 5.4pt; height: 34.25pt'>
                <p class='MsoNormal' style='margin-bottom: 0cm; margin-bottom: .0001pt; line-height: normal'>
                    <span style='font-size: 9.0pt; font-family: Arial,sans-serif; color: #5F5F5F'>Abba Hillel
                        Silver Rd 14</span><span style='font-size: 12.0pt; font-family: Times New Roman,serif;'></span></p>
                <p class='MsoNormal' style='margin-bottom: 0cm; margin-bottom: .0001pt; line-height: normal'>
                    <span style='font-size: 9.0pt; font-family: Arial,sans-serif; color: #5F5F5F'>Ramat
                        <span>Gan</span>, Israel 52506</span><span style='font-size: 12.0pt; font-family: Times New Roman,serif;'></span></p>
            </td>
            <td valign='top' style='width: 150.1pt; height: 34.25pt'>
                <p class='MsoNormal' style='margin-bottom: 0cm; margin-bottom: .0001pt; line-height: normal'>
                    <span style='font-size: 9.0pt; font-family: Arial,sans-serif; color: #5F5F5F'>Tel:
                    </span><a href='tel:%2B972%203%206122624' target='_blank'><span style='font-size: 9.0pt;
                        font-family: Arial,sans-serif; color: blue'>+972 3 6122624</span></a><span style='font-size: 9.0pt;
                            font-family: Arial,sans-serif; color: #5F5F5F'> </span><span style='font-size: 12.0pt;
                                font-family: Times New Roman,serif;'></span>
                </p>
                <p class='MsoNormal' style='margin-bottom: 0cm; margin-bottom: .0001pt; line-height: normal'>
                    <span style='font-size: 9.0pt; font-family: Arial,sans-serif; color: #5F5F5F'>Fax:
                    </span><a href='tel:%2B972%203%206122587' target='_blank'><span style='font-size: 9.0pt;
                        font-family: Arial,sans-serif; color: blue'>+972 3 6122587</span></a><span style='font-size: 9.0pt;
                            font-family: Arial,sans-serif; color: #5F5F5F'> </span><span style='font-size: 12.0pt;
                                font-family: Times New Roman,serif;'></span>
                </p>
            </td>
            <td colspan='2' valign='top' style='width: 131.65pt;
                height: 34.25pt'>
                <p class='MsoNormal' style='margin-bottom: 0cm; margin-bottom: .0001pt; line-height: normal'>
                    <a href='http://www.dglaw.co.il/' target='_blank'><span style='font-size: 9.0pt;
                        font-family: Arial,sans-serif; color: #5F5F5F; text-decoration: none; text-underline: none'>
                        www.dglaw.co.il</span></a><span style='font-size: 9.0pt; font-family: Arial,sans-serif;
                            color: #5F5F5F'> </span><span style='font-size: 12.0pt; font-family: Times New Roman,serif;'>
                            </span>
                </p>
                <p class='MsoNormal' style='margin-bottom: 0cm; margin-bottom: .0001pt; line-height: normal'>
                    <a href='mailto:neeman@dglaw.co.il' target='_blank'><span style='font-size: 9.0pt;
                        font-family: Arial,sans-serif; color: #365F91; text-decoration: none; text-underline: none'>
                        neeman@dglaw.co.il</span></a><span style='font-size: 9.0pt; font-family: Arial,sans-serif;
                            color: #365F91'> </span><span style='font-size: 12.0pt; font-family: Times New Roman,serif;'>
                            </span>
                </p>
            </td>
            <td valign='top' style='width: 28.15pt; height: 34.25pt'>
                <p class='MsoNormal' style='margin-bottom: 0cm; margin-bottom: .0001pt; line-height: normal'>
                    <span style='font-size: 12.0pt; font-family: Times New Roman,serif;'>&nbsp;</span></p>
            </td>
            <td valign='top' style='width: 35.75pt; height: 34.25pt'>
                <p class='MsoNormal' style='margin-bottom: 0cm; margin-bottom: .0001pt; line-height: normal'>
                    <span style='font-size: 12.0pt; font-family: Times New Roman,serif;'>&nbsp;</span></p>
            </td>
            <td style='width: 16.1pt; padding: 0cm 0cm 0cm 0cm; height: 34.25pt'>
                <p class='MsoNormal' style='margin-bottom: 0cm; margin-bottom: .0001pt; line-height: normal'>
                    <span style='font-size: 12.0pt; font-family: Times New Roman,serif;'>&nbsp;</span></p>
            </td>
        </tr>-->
        <tr style='height: 6.5pt'>
            <td valign='top' style=''>
                <p class='MsoNormal' style='margin-bottom: 0cm; margin-bottom: .0001pt; line-height: normal'>
                    <span style='font-size: 7.0pt; font-family: Arial,sans-serif; color: #5F5F5F'>The information
                        in this e-mail is intended only for the person or entity to <span class='GramE'>whom</span>
                        it is addressed and may contain confidential material.<br /> If you are not the intended
                        recipient, please notify the sender immediately (tel. </span><a href='tel:%2B972%283%29612-2624'
                            target='_blank'><span style='font-size: 7.0pt; font-family: Arial,sans-serif; color: blue'>
                                +972(3)612-2624</span></a><span style='font-size: 7.0pt; font-family: Arial,sans-serif;
                                    color: #5F5F5F'>) and do not disclose the contents to any other person,<br /> use it for
                                    any purpose, or store or copy the information in any medium. Any views expressed
                                    within this e-mail, which do not record an advice<br /> under the terms of an engagement
                                    letter previously agreed in writing, do not constitute an opinion and/or reflect
                                    the views of the firm. </span><span style='font-size: 12.0pt; font-family: Times New Roman,serif;'>
                                    </span><br /><br />
                                     <span style='font-size: 8.0pt; font-family: Arial,sans-serif; color: #365F91;margin-left: 36.0pt;'>Please
                        consider the environment. Do you really need to print this email?</span><span style='font-size: 12.0pt;
                            font-family: Times New Roman,serif;'></span>

                </p>
               
            </td>
        </tr>
    </table>
</body>
</html>


              ");


        return sb.ToString();

    }

}