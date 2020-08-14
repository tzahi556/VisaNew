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

public partial class Register : System.Web.UI.Page
{


    public string CompanyId = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        CompanyId = Request.QueryString["CompanyId"];

        if (string.IsNullOrEmpty(CompanyId))
        {
            dvMainReg.InnerHtml = "<div style='text-align:center;font-size:30px;color:#004681'><img src='../App_Themes/Theme1/Images/login.png' width='265px' height='240px' /><br/><b>The Link Wrong, Go to your administrator!</b></div>";

            return;
        }
        DataTable dt = Dal.ExeSp("GetCompany", "", "", CompanyId, "0");

        if (dt.Rows.Count > 0)
        {

            lblCompany.Text = dt.Rows[0]["Name"].ToString();

        }else
        {
            dvMainReg.InnerHtml = "<div style='text-align:center;font-size:30px;color:#004681'><img src='../App_Themes/Theme1/Images/login.png' width='265px' height='240px' /><br/><b>The Link Wrong, Go to your administrator!</b></div>";

        }

    }

    public void SendClientForm(object sender, EventArgs e)
    {


        string PassportDir = txtPassport.Text;

        string savePath = "~\\ExpertsUpload\\" + PassportDir + "\\";


        Directory.CreateDirectory(Server.MapPath(savePath + ""));

        for (var i = 0; i < Request.Files.Count; i++)
        {
            var postedFileFromClient = Request.Files[i];
            HttpPostedFile postedFile = (HttpPostedFile)postedFileFromClient;

            if(!string.IsNullOrEmpty(postedFile.FileName))
                 postedFile.SaveAs(Server.MapPath(savePath + postedFile.FileName));
            // postedFile.SaveAs(Server.MapPath(savePath + postedFile.FileName));
            // do something with file here
        }



        string NewExpertRegisterId = InsertIntoDB(txtSurname.Text,
        txtName.Text,
        CompanyId,
        txtEmail.Text,
        txtPhone.Text,
        txtJob.Text,
        txtPassport.Text,
        txtPassportIssueDate.Text,
        txtPassportExpDate.Text,
        "0",
        txtStreet.Text,
        txtTown.Text,
        txtCountry.Text,
        "",
        "",
        "0",
        txtDateofBirth.Text,
        HiddenFieldExpertRegId.Value
        );







        // מכניסים את האישה
        if (rdButtYes.Checked)
        {
            string SoupExpertRegisterId = InsertIntoDB(txtSoupFamilyname.Text,
            txtSoupGivenname.Text,
            CompanyId,
            "",
            "",
            "",
            txtSoupPassport.Text,
            txtSoupPassportIsueDate.Text,
            txtSoupPassportExpDate.Text,
            NewExpertRegisterId,
            "",
            "",
            txtSoupPlaceofBirth.Text,
            txtSoupMaidenname.Text,
            txtSoupFathersname.Text,
            "1",
            txtSoupDateofBirth.Text,
            HiddenFieldSoup.Value

        );

            // הכנסה של הילדים
            for (int i = 1; i < 5; i++)
            {
                TextBox txtChildGivenname = Page.FindControl("txtChild" + i.ToString() + "Givenname") as TextBox;
                TextBox txtChildCountryofbirth = Page.FindControl("txtChild" + i.ToString() + "Countryofbirth") as TextBox;
                TextBox txtChildDateofBirth = Page.FindControl("txtChild" + i.ToString() + "DateofBirth") as TextBox;
                TextBox txtChildPassport = Page.FindControl("txtChild" + i.ToString() + "Passport") as TextBox;
                TextBox txtChildPassportIsueDate = Page.FindControl("txtChild" + i.ToString() + "PassportIsueDate") as TextBox;
                TextBox txtChildPassportExpDate = Page.FindControl("txtChild" + i.ToString() + "PassportExpDate") as TextBox;
                HiddenField hdnFiled = Page.FindControl("HiddenFieldChild" + i.ToString()) as HiddenField;

                if (
                        !string.IsNullOrEmpty(txtChildGivenname.Text) &&
                        !string.IsNullOrEmpty(txtChildCountryofbirth.Text) &&
                        !string.IsNullOrEmpty(txtChildDateofBirth.Text) &&
                        !string.IsNullOrEmpty(txtChildPassport.Text) &&
                        !string.IsNullOrEmpty(txtChildPassportIsueDate.Text) &&
                        !string.IsNullOrEmpty(txtChildPassportExpDate.Text)
                    )
                {
                    string ChildExpertRegisterId = InsertIntoDB("",
                                                    txtChildGivenname.Text,
                                                    CompanyId,
                                                    "",
                                                    "",
                                                    "",
                                                    txtChildPassport.Text,
                                                    txtChildPassportIsueDate.Text,
                                                    txtChildPassportExpDate.Text,
                                                    NewExpertRegisterId,
                                                    "",
                                                    "",
                                                    txtChildCountryofbirth.Text,
                                                    "",
                                                    "",
                                                    "2",
                                                    txtChildDateofBirth.Text,
                                                    hdnFiled.Value

                                                    );

                }

            }

        }// end if famaly came


        string To = txtEmail.Text;

        string Subject = "New Expert Register - " + txtName.Text + " " + txtSurname.Text;

        string Body = "Hello Dear  <br />  New Expert Register To DG LAW <br /> First Name :" + txtName.Text + " <br /> Surename:" + txtSurname.Text
             + "< br /> Passport: " + txtPassport.Text + " < br /> Company:" + lblCompany.Text;

        Send(Subject, Body, To);

        Response.Redirect("RegisterEnd.aspx");



    }

    private string InsertIntoDB(string Surname, string Name, string CompanyId, string Email, string Phone, string Job,
        string Passport, string PassportIssueDate, string PassportExpDate, string ParentId, string StreetAndHouse, string Town,
        string Country, string Maidenname, string Fathersname, string IsFamaly, string DateofBirth,string ExpertFamId)
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
         "" 

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


    // public bool IsFirst = true;
    public void txtChanged(object sender, EventArgs e)
    {

        string Passport = ((TextBox)sender).Text;

        DataTable dt = Dal.ExeSp("GetExpertRegister", Passport);

        if (dt.Rows.Count > 0)
        {
           
            // מכניס את המומחה עצמו
            txtSurname.Text = dt.Rows[0]["Surname"].ToString();
            txtName.Text = dt.Rows[0]["Name"].ToString();
           
            txtEmail.Text = dt.Rows[0]["Email"].ToString();
            txtPhone.Text = dt.Rows[0]["Phone"].ToString();
            txtJob.Text = dt.Rows[0]["Job"].ToString();

            txtPassportIssueDate.Text = dt.Rows[0]["PassportIssueDate"].ToString();
            txtPassportExpDate.Text = dt.Rows[0]["PassportExpDate"].ToString();

            txtStreet.Text = dt.Rows[0]["StreetAndHouse"].ToString();
            txtTown.Text = dt.Rows[0]["Town"].ToString();
            txtCountry.Text = dt.Rows[0]["Country"].ToString();

            txtDateofBirth.Text = dt.Rows[0]["DateofBirth"].ToString();

            HiddenFieldExpertRegId.Value = dt.Rows[0]["ExpertRegId"].ToString();

            // אישה
            if (dt.Rows.Count > 1)
            {

                txtSoupFamilyname.Text = dt.Rows[1]["Surname"].ToString();
                txtSoupGivenname.Text = dt.Rows[1]["Name"].ToString();
                txtSoupPassport.Text = dt.Rows[1]["Passport"].ToString();
                txtSoupPassportIsueDate.Text = dt.Rows[1]["PassportIssueDate"].ToString();
                txtSoupPassportExpDate.Text = dt.Rows[1]["PassportExpDate"].ToString();
                txtSoupPlaceofBirth.Text = dt.Rows[1]["Country"].ToString();
                txtSoupMaidenname.Text = dt.Rows[1]["Maidenname"].ToString();
                txtSoupFathersname.Text = dt.Rows[1]["Fathersname"].ToString();
                txtSoupDateofBirth.Text = dt.Rows[1]["DateofBirth"].ToString();
                HiddenFieldSoup.Value = dt.Rows[1]["ExpertRegId"].ToString();

            }
            // ילדים
            if (dt.Rows.Count > 2)
            {

                int ChildCount = dt.Rows.Count - 2;
                for (int i = 1; i <= ChildCount; i++)
                {

                    TextBox txtChildGivenname = Page.FindControl("txtChild" + i.ToString() + "Givenname") as TextBox;
                    txtChildGivenname.Text = dt.Rows[1+i]["Name"].ToString();

                    TextBox txtChildCountryofbirth = Page.FindControl("txtChild" + i.ToString() + "Countryofbirth") as TextBox;
                    txtChildCountryofbirth.Text = dt.Rows[1 + i]["Country"].ToString();

                    TextBox txtChildDateofBirth = Page.FindControl("txtChild" + i.ToString() + "DateofBirth") as TextBox;
                    txtChildDateofBirth.Text = dt.Rows[1 + i]["DateofBirth"].ToString();

                    TextBox txtChildPassport = Page.FindControl("txtChild" + i.ToString() + "Passport") as TextBox;
                    txtChildPassport.Text = dt.Rows[1 + i]["Passport"].ToString();

                    TextBox txtChildPassportIsueDate = Page.FindControl("txtChild" + i.ToString() + "PassportIsueDate") as TextBox;
                    txtChildPassportIsueDate.Text = dt.Rows[1 + i]["PassportIssueDate"].ToString();

                    TextBox txtChildPassportExpDate = Page.FindControl("txtChild" + i.ToString() + "PassportExpDate") as TextBox;
                    txtChildPassportExpDate.Text = dt.Rows[1 + i]["PassportExpDate"].ToString();


                    HiddenField hdnFiled = Page.FindControl("HiddenFieldChild" + i.ToString()) as HiddenField;
                    hdnFiled.Value = dt.Rows[1 + i]["ExpertRegId"].ToString();

                }



            }




        }
    }


    public void Send(string Subject, string Body, string To)
    {
        string officeMails = "";

        DataTable  dtUsers = Dal.ExeSp("GetUsersTable");

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



        SmtpClient SmtpServer = new SmtpClient();
        MailMessage actMSG = new MailMessage();
        SmtpServer.Host = "yossilouk.cloudwm.com";
        SmtpServer.Port = 25;



        SmtpServer.UseDefaultCredentials = false;

        string mail_user = "dglaw";
        string mail_pass = "jadekia556";

        SmtpServer.Credentials = new System.Net.NetworkCredential(mail_user, mail_pass);


        actMSG.IsBodyHtml = true;

        actMSG.Subject = Subject;
        actMSG.Body = String.Format("{0}", Body);

        actMSG.To.Add("yossi@louk.com");
        //actMSG.To.Add("tzahi556@gmail.com");

        //if (!string.IsNullOrEmpty(officeMails))
        //{
        //    actMSG.To.Add(officeMails);
        //}

        if (!string.IsNullOrEmpty(To))
        {
            actMSG.To.Add(To);

        }


        actMSG.From = new MailAddress("dglaw@yossilouk.cloudwm.com");


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

}