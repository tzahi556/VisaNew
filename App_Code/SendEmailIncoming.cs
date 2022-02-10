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

            divContent = "<table id='tblUploadFiles' style='width: 100%; border-collapse: collapse;'><tr><th></th><th>Upload Name</th><th>File Name</th> <th>Check Docs</th> <th>Remarks</th> </tr>";

            var result = dtFiles.AsEnumerable().Where(myRow => myRow.Field<int>("ExpertId") == Int32.Parse(item));


            DataTable dt = result.CopyToDataTable();



            var ExpertRegId = "";
            var Name ="";
            var Surname = "";
            var Passport = "";
            var Email = "";
            var CompanyId = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                 ExpertRegId = row["ExpertId"].ToString();
                 Name = row["Name"].ToString();
                 Surname = row["Surname"].ToString();
                 Passport = row["Passport"].ToString();
                 Email = row["Email"].ToString();
                CompanyId = row["CompanyId"].ToString();

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
            //    divContent += "<td style='text-align:center'>" + ((IsUpload) ? imgVV : imgXX) + "</td>";
                divContent += "<td>" + FileName + "</td>";
                divContent += "<td style='text-align:center'>" + ((IsReadableBool && IsUpload) ? imgVV : ((IsUpload) ? imgXX : "")) + "</td>";
                if (!string.IsNullOrEmpty(Id))
                    divContent += "<td style='text-align:left;width:40%;padding:1px'>" + Remark + "</td></tr>";
                else
                    divContent += "<td style='text-align:center;width:40%;padding:1px'></td></tr>";

            }



            string bodyFiles = divContent + "</table>";


            string Subject = Surname + " " + Name + " - your Documents status.";


            string Body = @"
    <div style='font-family:Calibri;font-size:14px'>
      <b>  --- This is an automated email, do not reply. For any further questions, please contact our office: <span style='color:#0000FF;'>michal@dg-immigration.com</span> ---</b>
   <br /><br />
   </div>

    <div style='font-family:Arial;font-size:15px'>

        Dear: <b>{0}.</b> <br />
        Passport number: <b>{1}</b> <br /><br />

        We checked the documents that you sent and we have the following remarks:<br/><br/>

    </div>" + bodyFiles + @"
      <br/>
     <div style='font-family:Arial;font-size:15px'>

      Please resend these documents through our system. <br/><br/>

      Once you are ready to resend the above documents, please  click on the following link upload the revised document and submit. <br/>

        {2}<br/><br/>
      The system will identify you by entering your passport number and your name.<br/><br/>

       <span style='color:red;font-weight:bold;'> *Please note that delay in submitting all required documents may cause delay in applying your permit application.  </span>
    </div>
     "
 ;



            Body = string.Format(Body, Surname + " " + Name, Passport, "https://dgtracking.co.il/RegExpert/Register.aspx?CompanyId=" + CompanyId);


            //string Body = "<b>Hello Dear</b>  <br /><br />  <b>First Name:</b>" + Name + " <br /> <b>Surename:</b>" + Surname
            //     + "<br/><b> Passport:</b> " + Passport + " <br/><br/>";

            //Body += "<br /><b> Please Check Files Error and Send back:</b><br/>" + bodyFiles;



            if (Counter > 0)
            {

                string Sendformemail = "";
                DataTable CompanyMail = Dal.GetDataTable("Select * from Company where IsSendformemail=1 and Sendformemail is not null and CompanyId=" + CompanyId);
                if (CompanyMail.Rows.Count > 0)
                {
                    Sendformemail = CompanyMail.Rows[0]["Sendformemail"].ToString();

                }



                Send(Subject, Body, Email, Sendformemail);

                string sqlRe = "Update ExperUploadFiles set SendEmailDate='{1}' where ExpertId = {0}";

                sqlRe = string.Format(sqlRe, ExpertRegId, DateTime.Now.ToString("yyyy-MM-dd"));

                int resRe = Dal.ExecuteNonQuery(sqlRe);



            }
        }


    }


    public void Send(string Subject, string Body, string To,string Sendformemail)
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
        if (!string.IsNullOrEmpty(Sendformemail))
        {
            actMSG.To.Add(Sendformemail);
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

<html>
<body>


   <br/>

    <div style='font-family:Arial;font-size:14px'>
        Kind regards.<br/>
        Michal<br/>
        <span style='color:#0000FF'>michal@dg-immigration.com</span> <br/>
        <br />
    </div>
    <table class='' border='0' cellspacing='10' cellpadding='10' style='border-collapse: collapse;'>


        <tr>
            <td rowspan='5' valign='top' style='width:150px;'>
             
                <img  alt= '' src= 'http://dgtracking.co.il/images/dgtrack.png' style='height:145px' />
                <img style='position:absolute; top:120px;margin-left:20px;' alt= '' src= 'http://dgtracking.co.il/images/line1.png' />
               
            </td>

        </tr>

        <tr>



            <td valign='top' style='width:300px'>
                <p style = 'font-family:Georgia;font-weight:bold;font-size:20px;color:#004571;margin:0px' >
                    Michal Shmuely,
                    <br />
                    Immigration Manager
                </p>
            </td>

            <td rowspan='4' valign= 'top' style= 'width:240px;' >
                <img style='position:absolute; top:120px' alt= '' src= 'http://dgtracking.co.il/images/line1.png' />
                <img style='margin-left:20px;' alt= '' src= 'http://dgtracking.co.il/images/movil.png' />
            </td>


        </tr>
        <tr>
            <td>
                <p style= 'font-family:Georgia;font-style:italic;font-size:15px;margin:0px'>
                    Dardik Gross &amp; Co.Law Firm
                </p>
            </td>
        </tr>
        <tr>
            <td>
                <p style= 'font-family:Arial;font-size:13px;margin:0px'>
                    14 Abba Hillel Rd., Ramat Gan, Israel 52506
                </p>
            </td>
        </tr>
        <tr>
            <td valign='top'>
                <p style= 'font-family:Arial;font-size:11px;margin:0px'>
                              <table border='0' cellspacing='0' cellpadding='0' style='font-family:Arial;font-size:12px;'>
                        <tr>
                            <td style='padding-right:15px'>
                                <span><color style='color:#004571'> Tel:</color> +972 3 6122624</span>
                            </td>
                            <td>
                               <img src='http://dgtracking.co.il/images/line2.png'  />
                            </td>
                            <td style='padding-left:15px'>
                                <span style='color:#0000FF;'> info@dglaw.co.il</span> 
                            </td>

                        </tr>

                        <tr>
                            <td style='padding-right:15px'>
                                <span><color style='color:#004571'> Fax:</color> +972 3 6122587</span>

                            </td>

                            <td>
                             
                            </td>
                            <td style='padding-left:15px'>
                                <span><a href='https://www.dglaw.co.il'>www.dglaw.co.il</a></span>
                            </td>

                        </tr>

                    </table>

                </p>
            </td>



        </tr>
        <tr style = 'height: 6.5pt' >
            <td valign='top' style='' colspan='4'>
                <p class='MsoNormal' style='margin-bottom: 0cm; margin-bottom: .0001pt; line-height: normal'>
                    <span style = 'font-size: 7.0pt; font-family: Arial,sans-serif; color: #5F5F5F' >
                        The information
                        in this e-mail is intended only for the person or entity to<span class='GramE'>whom</span>
                        it is addressed and may contain confidential material.<br /> If you are not the intended
                        recipient, please notify the sender immediately (tel.
                    </span><a href = 'tel:%2B972%283%29612-2624'
                              target= '_blank'>
                        <span style= 'font-size: 7.0pt; font-family: Arial,sans-serif; color: blue'>
                            +972(3)612-2624
                        </span>
                    </a><span style = 'font-size: 7.0pt; font-family: Arial,sans-serif;
                                    color: #5F5F5F'>
                        ) and do not disclose the contents to any other person,<br /> use it for
                        any purpose, or store or copy the information in any medium.Any views expressed
                        within this e-mail, which do not record an advice<br /> under the terms of an engagement
                        letter previously agreed in writing, do not constitute an opinion and/or reflect
                        the views of the firm.
                    </span>



                </p>

            </td>
        </tr>
    </table>
</body>
</html>");


        return sb.ToString();

    }









}