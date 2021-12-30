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
public class SendEmailIncoming
{

    public DataTable dtMailList;

    public DataTable dtUsers;

    public string officeMails;

    public SendEmailIncoming(string Date)
    {


        dtMailList = Dal.ExeSp("GetSchedularIncomingEmailList", Date);



        SendMailContent();



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

    public void SendMailContent()
    {
        string divContent = "<table id='tblUploadFiles' style='width: 100%; border-collapse: collapse;'><tr><th></th><th>Upload Name</th><th>Is Upload</th> <th>File Name</th> <th>Is Readable</th> <th>Remark</th> </tr>";

        string imgVV = "<img src='http://dgtracking.co.il/App_Themes/Theme1/Images/vv.png'/>";
        string imgXX = "<img src='http://dgtracking.co.il/App_Themes/Theme1/Images/xx.png'/>";

        DataTable dtFiles = dtMailList;//Dal.ExeSp("SetExpertUploadFiles",



        int Counter = 0;

        List<string> ExpertList = dtFiles.AsEnumerable().Select(x => x["ExpertId"].ToString()).Distinct().ToList();


        foreach (var item in ExpertList)
        {

            divContent = "<table id='tblUploadFiles' style='width: 100%; border-collapse: collapse;'><tr><th></th><th>Upload Name</th><th>Is Upload</th> <th>File Name</th> <th>Is Readable</th> <th>Remark</th> </tr>";

            var result = dtFiles.AsEnumerable().Where(myRow => myRow.Field<int>("ExpertId") == Int32.Parse(item));


            DataTable dt = result.CopyToDataTable();



            var ExpertRegId = "";
            var Name ="";
            var Surname = "";
            var Passport = "";
            var Email = "";


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                 ExpertRegId = row["ExpertId"].ToString();
                 Name = row["Name"].ToString();
                 Surname = row["Surname"].ToString();
                 Passport = row["Passport"].ToString();
                 Email = row["Email"].ToString();

                var IsSend = row["IsSend"].ToString();

                if (string.IsNullOrEmpty(IsSend) || IsSend != "True") continue;

                Counter++;
                //  var ExpertId = row["ExpertId"].ToString();
                var Id = row["Id"].ToString();
                var SeqId = Counter;  //row["SeqId"].ToString();
                var UploadName = row["UploadName"].ToString();
                var FileName = row["FileName"].ToString();
                var IsReadable = row["IsReadable"].ToString();
                var Remark = row["Remark"].ToString();
                bool IsUpload = false;
                bool IsReadableBool = false;

                if (!string.IsNullOrEmpty(FileName))
                    IsUpload = true;
                if (!string.IsNullOrEmpty(IsReadable) && IsReadable == "True")
                    IsReadableBool = true;

                divContent += "<tr><td>" + SeqId + "</td>";
                divContent += "<td>" + UploadName + "</td>";
                divContent += "<td style='text-align:center'>" + ((IsUpload) ? imgVV : imgXX) + "</td>";
                divContent += "<td>" + FileName + "</td>";
                divContent += "<td style='text-align:center'>" + ((IsReadableBool && IsUpload) ? imgVV : ((IsUpload) ? imgXX : "")) + "</td>";
                if (!string.IsNullOrEmpty(Id))
                    divContent += "<td style='text-align:left;width:40%;padding:1px'>" + Remark + "</td></tr>";
                else
                    divContent += "<td style='text-align:center;width:40%;padding:1px'></td></tr>";

            }



            string bodyFiles = divContent + "</table>";

          

            string Subject = " Dg Law Documents - " + Name + " " + Surname;

            string Body = "<b>Hello Dear</b>  <br /><br />  <b>First Name:</b>" + Name + " <br /> <b>Surename:</b>" + Surname
                 + "<br/><b> Passport:</b> " + Passport + " <br/><br/>";

            Body += "<br /><b> Please Check Files Error and Send back:</b><br/>" + bodyFiles;



            if (Counter > 0)
            {
                Send(Subject, Body, Email);

                string sqlRe = "Update ExperUploadFiles set SendEmailDate='{1}' where ExpertId = {0}";

                sqlRe = string.Format(sqlRe, ExpertRegId, DateTime.Now.ToString("yyyy-MM-dd"));

                int resRe = Dal.ExecuteNonQuery(sqlRe);



            }
        }


    }


    public void Send(string Subject, string Body, string To)
    {

        string officeMails = "";
        DataTable dtUsers = Dal.ExeSp("GetUsersTable");
        DataRow[] result = dtUsers.Select("IsIncomingMail=True And Email<>'' And Email Is Not Null");
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




        Body = GetHeader() + Body + GetFooter();




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

        actMSG.To.Add(To);


        if (!string.IsNullOrEmpty(officeMails))
        {
            actMSG.To.Add(officeMails);
        }



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


    private string GetHeader()
    {
        StringBuilder sb = new StringBuilder();

        //<span style='color:red'>**</span><u style='font-family: Arial,sans-serif;font-size: 10.5pt;'>This in an automated mail notification, <b>do not reply</b> to this mail </u> <span style='color:red'>**</span>
        sb.Append(@"<html><head>

         <style>
                    #tblUploadFiles th {
                        text-align: center !important;
                        border: 1px solid #d3d3d3;
                        background: #e6e6e6 url(images/ui-bg_glass_75_e6e6e6_1x400.png) 50% 50% repeat-x;
                        font-weight: normal;
                        color: #555555;
                    }

                    #tblUploadFiles, #tblUploadFiles td, #tblUploadFiles th {
                        border: 1px solid #ddd;
                        text-align: left;
                    }

                    #tblUploadFiles {
                        border-collapse: collapse;
                        width: 100%;
                    }

                        #tblUploadFiles th, #tblUploadFiles td {
                            padding: 10px;
                        }
                </style>

</head><body dir='ltr' style='padding:5px'>
               
              
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