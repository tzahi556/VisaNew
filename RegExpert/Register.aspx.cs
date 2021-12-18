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

public partial class Register : System.Web.UI.Page
{
    //http://localhost:58894/RegExpert/Register.aspx?companyid=21

    public string CompanyId = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //for dev
        CompanyId = "21"; //Request.QueryString["CompanyId"];

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
                //secChilds.Visible = false;





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



            // dvmyButtonInput.InnerHtml = "tzahi.html";




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


        //HiddenFieldExpertRegId.Value = "0";
        //HiddenFieldSoup.Value = "0";
        //HiddenFieldChild1.Value = "0";
        //HiddenFieldChild2.Value = "0";
        //HiddenFieldChild3.Value = "0";
        //HiddenFieldChild4.Value = "0";





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

            if (!string.IsNullOrEmpty(postedFile.FileName))
                postedFile.SaveAs(Server.MapPath(savePath + postedFile.FileName));
            // postedFile.SaveAs(Server.MapPath(savePath + postedFile.FileName));
            // do something with file here
        }






        //    string To = txtEmail.Text;

        //    string Subject = "New Expert Register - " + txtName.Text + " " + txtSurname.Text;

        //    string Body = "Hello Dear  <br />  New Expert Register To DG LAW <br /> First Name :" + txtName.Text + " <br /> Surename:" + txtSurname.Text
        //         + "< br /> Passport: " + txtPassport.Text + " < br /> Company:" + lblCompany.Text;

        //    Send(Subject, Body, To);

        //    Response.Redirect("RegisterEnd.aspx");



    }

    private string InsertIntoDB(string Surname, string Name, string CompanyId, string Email, string Phone, string Job,
        string Passport, string PassportIssueDate, string PassportExpDate, string ParentId, string StreetAndHouse, string Town,
        string Country, string Maidenname, string Fathersname, string IsFamaly, string DateofBirth, string ExpertFamId)
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





    //public void Send(string Subject, string Body, string To)
    //{
    //    string officeMails = "";

    //    DataTable dtUsers = Dal.ExeSp("GetUsersTable");

    //    DataRow[] result = dtUsers.Select("IsEmail=True And Email<>'' And Email Is Not Null");


    //    bool isFirst = true;


    //    foreach (DataRow row in result)
    //    {

    //        if (isFirst)
    //        {
    //            officeMails = row["Email"].ToString();
    //            isFirst = false;
    //        }
    //        else
    //        {
    //            officeMails += "," + row["Email"].ToString();
    //        }


    //    }



    //    SmtpClient SmtpServer = new SmtpClient();
    //    MailMessage actMSG = new MailMessage();
    //    SmtpServer.Host = "yossilouk.cloudwm.com";
    //    SmtpServer.Port = 25;



    //    SmtpServer.UseDefaultCredentials = false;

    //    string mail_user = "dglaw";
    //    string mail_pass = "jadekia556";

    //    SmtpServer.Credentials = new System.Net.NetworkCredential(mail_user, mail_pass);


    //    actMSG.IsBodyHtml = true;

    //    actMSG.Subject = Subject;
    //    actMSG.Body = String.Format("{0}", Body);

    //    actMSG.To.Add("yossi@louk.com");
    //    //actMSG.To.Add("tzahi556@gmail.com");

    //    //if (!string.IsNullOrEmpty(officeMails))
    //    //{
    //    //    actMSG.To.Add(officeMails);
    //    //}

    //    if (!string.IsNullOrEmpty(To))
    //    {
    //        actMSG.To.Add(To);

    //    }


    //    actMSG.From = new MailAddress("dglaw@yossilouk.cloudwm.com");


    //    try
    //    {

    //        SmtpServer.Send(actMSG);
    //        actMSG.Dispose();
    //    }
    //    catch (Exception ex)
    //    {
    //        HttpContext.Current.Response.Write(ex.Message);
    //    }

    //}


    protected void btnSaveData_Click(object sender, EventArgs e)
    {

        string dddd = HiddenFieldExpertRegId.Value;


        string NewExpertRegisterId = InsertIntoDB(txtSurname.Value,
                                                  txtName.Value,
                                                  CompanyId,
                                                  txtEmail.Value,
                                                  txtPhone.Value,
                                                  txtJob.Value,
                                                  txtPassport.Text,
                                                  txtPassportIssueDate.Value,
                                                  txtPassportExpDate.Value,
                                                  "0",
                                                  txtStreet.Value,
                                                  txtTown.Value,
                                                  txtCountry.Value,
                                                  "",
                                                  "",
                                                  "0",
                                                  txtDateofBirth.Value,
                                                  HiddenFieldExpertRegId.Value
                                                  );

        HiddenFieldExpertRegId.Value = NewExpertRegisterId;





        // מכניסים את האישה
        if (rdButtYes.Checked)
        {
            string SoupExpertRegisterId = InsertIntoDB(txtSoupFamilyname.Value,
                                            txtSoupGivenname.Value,
                                            CompanyId,
                                            "",
                                            "",
                                            "",
                                            txtSoupPassport.Value,
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
                                                    txtChildGivenname.Value,
                                                    CompanyId,
                                                    "",
                                                    "",
                                                    "",
                                                    txtChildPassport.Value,
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
}