using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.IO;
using System.Net.Mail;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Register : System.Web.UI.Page
{
    //http://localhost:58894/RegExpert/Register.aspx?companyid=21

    public string CompanyId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //for dev
        CompanyId = Request.QueryString["CompanyId"];

        if (string.IsNullOrEmpty(CompanyId))
        {
            // dvMainReg.InnerHtml = "<div style='text-align:center;font-size:30px;color:#004681'><img src='../App_Themes/Theme1/Images/login.png' width='265px' height='240px' /><br/><b>The Link Wrong, Go to your administrator!</b></div>";

            return;
        }
        DataTable dt = Dal.ExeSp("GetCompany", "", "", CompanyId, "0");

        if (dt.Rows.Count > 0)
        {
            // var myDiv = new HtmlGenericControl("div");
            lblCompany.InnerText = dt.Rows[0]["Name"].ToString();
            // txtPassport.Value = dt.Rows[0]["Name"].ToString();
            //txtPassport.InnerHTML = dt.Rows[0]["Name"].ToString();
        }
        else
        {
            // dvMainReg.InnerHtml = "<div style='text-align:center;font-size:30px;color:#004681'><img src='../App_Themes/Theme1/Images/login.png' width='265px' height='240px' /><br/><b>The Link Wrong, Go to your administrator!</b></div>";

        }

        //  ClientScript.RegisterClientScriptInclude(this.GetType(), "myScript", "js/main.js");
        //  Page.Header.Controls.Add(new LiteralControl("<script type='text/javascript' src='" + Page.ResolveUrl("js/main.js") + "'></script>"));



    }

    //[WebMethod]
    //public static string GetIfExistPassport(string Passport)
    //{


    //    return "Result";
    //}

    public void txtChanged(object sender, EventArgs e)
    {



        InitPageBeforeChange();

        string Passport = txtPassport.Text;

        DataTable dt = Dal.ExeSp("GetExpertRegister", Passport);

        if (dt.Rows.Count > 0)
        {

            // מכניס את המומחה עצמו
            txtSurname.Value = dt.Rows[0]["Surname"].ToString();
            txtName.Value = dt.Rows[0]["Name"].ToString();
            txtEmail.Value = dt.Rows[0]["Email"].ToString();
            txtPhone.Value = dt.Rows[0]["Phone"].ToString();
            txtJob.Value = dt.Rows[0]["Job"].ToString();
            txtPassportIssueDate.Value = dt.Rows[0]["PassportIssueDate"].ToString();
            txtPassportExpDate.Value = dt.Rows[0]["PassportExpDate"].ToString();
            txtStreet.Value = dt.Rows[0]["StreetAndHouse"].ToString();
            txtTown.Value = dt.Rows[0]["Town"].ToString();
            txtCountry.Value = dt.Rows[0]["Country"].ToString();
            txtDateofBirth.Value = dt.Rows[0]["DateofBirth"].ToString();

            HiddenFieldExpertRegId.Value = dt.Rows[0]["ExpertRegId"].ToString();

            //UpdatePanel1.Update();


            // UpdatePanel2.Update();
            // HiddenFieldExpertRegId.Value = dt.Rows[0]["ExpertRegId"].ToString();

            // אישה
            if (dt.Rows.Count > 1)
            {
                rdButtYes.Checked = true;
                secSoup.Style["display"] = "true";



                txtSoupFamilyname.Value = dt.Rows[1]["Surname"].ToString();
                txtSoupGivenname.Value = dt.Rows[1]["Name"].ToString();
                txtSoupPassport.Value = dt.Rows[1]["Passport"].ToString();
                txtSoupPassportIsueDate.Value = dt.Rows[1]["PassportIssueDate"].ToString();
                txtSoupPassportExpDate.Value = dt.Rows[1]["PassportExpDate"].ToString();
                txtSoupPlaceofBirth.Value = dt.Rows[1]["Country"].ToString();
                txtSoupMaidenname.Value = dt.Rows[1]["Maidenname"].ToString();
                txtSoupFathersname.Value = dt.Rows[1]["Fathersname"].ToString();
                txtSoupDateofBirth.Value = dt.Rows[1]["DateofBirth"].ToString();
                HiddenFieldSoup.Value = dt.Rows[1]["ExpertRegId"].ToString();

                // UpdatePanel2.Update();
            }

            //// ילדים
            if (dt.Rows.Count > 2)
            {

                int ChildCount = dt.Rows.Count - 2;
                for (int i = 1; i <= ChildCount; i++)
                {

                    HtmlInputText txtChildGivenname = Page.FindControl("txtChild" + i.ToString() + "Givenname") as HtmlInputText;
                    txtChildGivenname.Value = dt.Rows[1 + i]["Name"].ToString();

                    HtmlInputText txtChildCountryofbirth = Page.FindControl("txtChild" + i.ToString() + "Countryofbirth") as HtmlInputText;
                    txtChildCountryofbirth.Value = dt.Rows[1 + i]["Country"].ToString();

                    HtmlInputText txtChildDateofBirth = Page.FindControl("txtChild" + i.ToString() + "DateofBirth") as HtmlInputText;
                    txtChildDateofBirth.Value = dt.Rows[1 + i]["DateofBirth"].ToString();

                    HtmlInputText txtChildPassport = Page.FindControl("txtChild" + i.ToString() + "Passport") as HtmlInputText;
                    txtChildPassport.Value = dt.Rows[1 + i]["Passport"].ToString();

                    HtmlInputText txtChildPassportIsueDate = Page.FindControl("txtChild" + i.ToString() + "PassportIsueDate") as HtmlInputText;
                    txtChildPassportIsueDate.Value = dt.Rows[1 + i]["PassportIssueDate"].ToString();

                    HtmlInputText txtChildPassportExpDate = Page.FindControl("txtChild" + i.ToString() + "PassportExpDate") as HtmlInputText;
                    txtChildPassportExpDate.Value = dt.Rows[1 + i]["PassportExpDate"].ToString();


                    HiddenField hdnFiled = Page.FindControl("HiddenFieldChild" + i.ToString()) as HiddenField;
                    hdnFiled.Value = dt.Rows[1 + i]["ExpertRegId"].ToString();

                }



            }







        }



    }

    private void InitPageBeforeChange()
    {
        secChilds.Style["display"] = "none";
        secSoup.Style["display"] = "none";
        rdButtYes.Checked = false;
        rdButtNo.Checked = true;


        txtSurname.Value = "";
        txtName.Value = "";
        txtEmail.Value = "";
        txtPhone.Value = "";
        txtJob.Value = "";
        txtPassportIssueDate.Value = "";
        txtPassportExpDate.Value = "";
        txtStreet.Value = "";
        txtTown.Value = "";
        txtCountry.Value = "";
        txtDateofBirth.Value = "";


        txtSoupFamilyname.Value = "";
        txtSoupGivenname.Value = "";
        txtSoupPassport.Value = "";
        txtSoupPassportIsueDate.Value = "";
        txtSoupPassportExpDate.Value = "";
        txtSoupPlaceofBirth.Value = "";
        txtSoupMaidenname.Value = "";
        txtSoupFathersname.Value = "";
        txtSoupDateofBirth.Value = "";

        for (int i = 1; i <= 4; i++)
        {

            HtmlInputText txtChildGivenname = Page.FindControl("txtChild" + i.ToString() + "Givenname") as HtmlInputText;
            txtChildGivenname.Value = "";

            HtmlInputText txtChildCountryofbirth = Page.FindControl("txtChild" + i.ToString() + "Countryofbirth") as HtmlInputText;
            txtChildCountryofbirth.Value = "";

            HtmlInputText txtChildDateofBirth = Page.FindControl("txtChild" + i.ToString() + "DateofBirth") as HtmlInputText;
            txtChildDateofBirth.Value = "";

            HtmlInputText txtChildPassport = Page.FindControl("txtChild" + i.ToString() + "Passport") as HtmlInputText;
            txtChildPassport.Value = "";

            HtmlInputText txtChildPassportIsueDate = Page.FindControl("txtChild" + i.ToString() + "PassportIsueDate") as HtmlInputText;
            txtChildPassportIsueDate.Value = "";

            HtmlInputText txtChildPassportExpDate = Page.FindControl("txtChild" + i.ToString() + "PassportExpDate") as HtmlInputText;
            txtChildPassportExpDate.Value = "";


            //HiddenField hdnFiled = Page.FindControl("HiddenFieldChild" + i.ToString()) as HiddenField;
            //hdnFiled.Value = dt.Rows[1 + i]["ExpertRegId"].ToString();

        }


        HiddenFieldExpertRegId.Value = "0";
        HiddenFieldSoup.Value = "0";
        HiddenFieldChild1.Value = "0";
        HiddenFieldChild2.Value = "0";
        HiddenFieldChild3.Value = "0";
        HiddenFieldChild4.Value = "0";





    }

    public void SendClientForm(object sender, EventArgs e)
    {
      // var IsNew = HiddenFieldExpertRegId.Value;


        btnSaveData_Click(sender, e);


        string FilesList = "";

        string ExpertId = HiddenFieldExpertRegId.Value;

        if (rdButtNo.Checked)
        {

            string sql = "Delete from  ExpertRegister  where ParentId = " + ExpertId;

            int res = Dal.ExecuteNonQuery(sql);

        }

        string PassportDir = txtPassport.Text;

        string savePath = "~\\ExpertsUpload\\" + PassportDir + "\\";


        Directory.CreateDirectory(Server.MapPath(savePath + ""));

        for (var i = 0; i < Request.Files.Count; i++)
        {
            var postedFileFromClient = Request.Files[i];
            HttpPostedFile postedFile = (HttpPostedFile)postedFileFromClient;

            if (!string.IsNullOrEmpty(postedFile.FileName))
            {
                postedFile.SaveAs(Server.MapPath(savePath + postedFile.FileName));
                Dal.ExeSp("SetExpertUploadFiles",
                            1,
                            i + 1,
                            ExpertId,
                            postedFile.FileName,
                            false
                            );

                FilesList += postedFile.FileName + "<br/>";
            }
            // postedFile.SaveAs(Server.MapPath(savePath + postedFile.FileName));
            // do something with file here
        }






        string To = txtEmail.Value;

        string Subject = "New Expert Register - " + txtName.Value + " " + txtSurname.Value;

        string Body = "<b>Hello Dear</b>  <br /><br />  New Expert Register To DG LAW <br /> <b>First Name:</b>" + txtName.Value + " <br /> <b>Surename:</b>" + txtSurname.Value
             + "<br/><b> Passport:</b> " + txtPassport.Text + " <br/> <b>Company:</b>" + lblCompany.InnerText + "<br/>";
        if (!string.IsNullOrEmpty(FilesList))
            Body += "<br /><b> We recieve This Files:</b><br/>" + FilesList;



        Send(Subject, Body, To);

        Response.Redirect("RegisterEnd.aspx");



    }

    private string InsertIntoDB(string Surname, string Name, string CompanyId, string Email, string Phone, string Job,
        string Passport, string PassportIssueDate, string PassportExpDate, string ParentId, string StreetAndHouse, string Town,
        string Country, string Maidenname, string Fathersname, string IsFamaly, string DateofBirth, string ExpertFamId, bool isFinish = false)
    {
        DataTable ExpertRegister = Dal.ExeSp("SetExpertRegister",
         Surname,
         Name,
         CompanyId,
         Email,
         Phone,
         Job,
         Passport,
         GetAsDate(PassportIssueDate),
         GetAsDate(PassportExpDate),
         ParentId,
         StreetAndHouse,
         Town,
         Country,
         Maidenname,
         Fathersname,
         IsFamaly,
         GetAsDate(DateofBirth),
         ExpertFamId,
         "",
         isFinish

         );

        return ExpertRegister.Rows[0][0].ToString();
    }

    private object GetAsDate(string date)
    {

        try
        {
            return Convert.ToDateTime(date);
        }
        catch (Exception ex)
        {
            return "";
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

        actMSG.To.Add(txtEmail.Value);
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

    protected void btnSaveData_Click(object sender, EventArgs e)
    {


        LinkButton button = (LinkButton)sender;
        string SenderId = button.ID;
        bool isFinish = false;
        if (SenderId == "btnSendForm")
        {

            isFinish = true;
        }

        string NewExpertRegisterId = InsertIntoDB(GetUpperLowerCase(txtSurname.Value,1),
                                                  GetUpperLowerCase(txtName.Value,2),
                                                  CompanyId,
                                                  txtEmail.Value,
                                                  txtPhone.Value,
                                                  txtJob.Value,
                                                  GetUpperLowerCase(txtPassport.Text,1),
                                                  txtPassportIssueDate.Value,
                                                  txtPassportExpDate.Value,
                                                  "0",
                                                  txtStreet.Value,
                                                  txtTown.Value,
                                                  GetUpperLowerCase(txtCountry.Value,2),
                                                  "",
                                                  "",
                                                  "0",
                                                  txtDateofBirth.Value,
                                                  HiddenFieldExpertRegId.Value,
                                                  isFinish
                                                  );

        HiddenFieldExpertRegId.Value = NewExpertRegisterId;


        // מכניסים את האישה
        if (rdButtYes.Checked)
        {
            string SoupExpertRegisterId = InsertIntoDB(GetUpperLowerCase(txtSoupFamilyname.Value,1),
                                            GetUpperLowerCase(txtSoupGivenname.Value,2),
                                            CompanyId,
                                            "",
                                            "",
                                            "",
                                            GetUpperLowerCase(txtSoupPassport.Value,1),
                                            txtSoupPassportIsueDate.Value,
                                            txtSoupPassportExpDate.Value,
                                            NewExpertRegisterId,
                                            "",
                                            "",
                                            txtSoupPlaceofBirth.Value,
                                            txtSoupMaidenname.Value,
                                            txtSoupFathersname.Value,
                                            "1",
                                            txtSoupDateofBirth.Value,
                                            HiddenFieldSoup.Value
                                          );
            HiddenFieldSoup.Value = SoupExpertRegisterId;


            // הכנסה של הילדים
            for (int i = 1; i < 5; i++)
            {
                HtmlInputText txtChildGivenname = Page.FindControl("txtChild" + i.ToString() + "Givenname") as HtmlInputText;
                HtmlInputText txtChildCountryofbirth = Page.FindControl("txtChild" + i.ToString() + "Countryofbirth") as HtmlInputText;
                HtmlInputText txtChildDateofBirth = Page.FindControl("txtChild" + i.ToString() + "DateofBirth") as HtmlInputText;
                HtmlInputText txtChildPassport = Page.FindControl("txtChild" + i.ToString() + "Passport") as HtmlInputText;
                HtmlInputText txtChildPassportIsueDate = Page.FindControl("txtChild" + i.ToString() + "PassportIsueDate") as HtmlInputText;
                HtmlInputText txtChildPassportExpDate = Page.FindControl("txtChild" + i.ToString() + "PassportExpDate") as HtmlInputText;
                HiddenField hdnFiled = Page.FindControl("HiddenFieldChild" + i.ToString()) as HiddenField;

                if (
                        !string.IsNullOrEmpty(txtChildGivenname.Value) &&
                        !string.IsNullOrEmpty(txtChildCountryofbirth.Value) &&
                        !string.IsNullOrEmpty(txtChildDateofBirth.Value) &&
                        !string.IsNullOrEmpty(txtChildPassport.Value) &&
                        !string.IsNullOrEmpty(txtChildPassportIsueDate.Value) &&
                        !string.IsNullOrEmpty(txtChildPassportExpDate.Value)
                    )
                {
                    string ChildExpertRegisterId = InsertIntoDB("",
                                                    GetUpperLowerCase(txtChildGivenname.Value,2),
                                                    CompanyId,
                                                    "",
                                                    "",
                                                    "",
                                                    GetUpperLowerCase(txtChildPassport.Value,1),
                                                    txtChildPassportIsueDate.Value,
                                                    txtChildPassportExpDate.Value,
                                                    NewExpertRegisterId,
                                                    "",
                                                    "",
                                                    txtChildCountryofbirth.Value,
                                                    "",
                                                    "",
                                                    "2",
                                                    txtChildDateofBirth.Value,
                                                    hdnFiled.Value
                                                    );

                    hdnFiled.Value = ChildExpertRegisterId;

                }

            }

        }// end if famaly came
       


    }

    private string GetUpperLowerCase(string value, int type)
    {
        if (string.IsNullOrEmpty(value.Trim())) return "";
        //שם משפחה
        if (type == 1)
            return value.ToUpper();
        else
        {
            string res = "";
            string[] NameVal = value.Split(' ');

            for (int i = 0; i < NameVal.Length; i++)
            {
                string item = NameVal[i];
                if(i==0)
                    res += item[0].ToString().ToUpper() + ((item.Length > 1) ? item.Substring(1) : "");
                else
                    res += " " + item[0].ToString().ToUpper() + ((item.Length > 1) ? item.Substring(1) : "");
            }
           
            return res;
           // return value[0].ToString().ToUpper() + ((value.Length > 1) ? value.Substring(1) : "");
        }
           

    }

    protected void btnGetFiles_Click(object sender, EventArgs e)
    {
        string ExpertId = HiddenFieldExpertRegId.Value;

        if (ExpertId == "0") return;
        DataTable dt = Dal.ExeSp("SetExpertUploadFiles",
                           2,
                           0,
                           ExpertId,
                            "",
                           false
                           );



        foreach (DataRow row in dt.Rows)
        {
            string FileName = row["FileName"].ToString();
            int UploadId = Convert.ToInt32(row["UploadId"].ToString());
            UploadId = UploadId - 1;
            string UploadIdStr = UploadId.ToString();
            if (UploadId == 0) UploadIdStr = "";
            HtmlGenericControl div = FindControl("dvmyButton" + UploadIdStr + "Input") as HtmlGenericControl;
            if (div != null)
                div.InnerHtml = "<span class='fileUploadName'>" + FileName + "</span>";

        }

    }
}