using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Expressions;
using System.Data;
using System.IO;
using System.Configuration;
using System.IO.Compression;
using System.Net.Mail;
using System.Text;
//using System.IO.Compression.FileSystem;

public partial class Pages_PopUp_editExpertRegister : System.Web.UI.Page
{
    protected string ExpertRegId = "";
    public string RoleId = "";
    public bool IsMultiple = false;

    public string StepId = "";
    public string CompanyId = "";

    public string IsArchive = "";
    public HttpCookie cookie = null;

    protected void Page_Load(object sender, EventArgs e)
    {



        if (HttpContext.Current.Request.Cookies["UserData"] != null)
        {
            cookie = HttpContext.Current.Request.Cookies["UserData"];
        }

        RoleId = cookie["RoleId"].ToString();

        ExpertRegId = Request.QueryString["ExpertId"];

        StepId = Request.QueryString["StepId"];

        CompanyId = Request.QueryString["CompanyId"];

        IsArchive = Request.QueryString["IsArchive"];
       
        if (IsArchive == "1")
        {
            btnMove.Visible = false;

        }
        else
        {
            btnGoBack.Visible = false;

        }


        //if (ExpertId.IndexOf(",") != -1)
        //{
        //    IsMultiple = true;
        //}

        if (!Page.IsPostBack)
        {
            InitDropDown();

            FillData();







        }



    }



    private void InitDropDown()
    {
        ddlCompany.DataSource = Dal.ExeSp("GetCompany", "", "", "0", "0");
        ddlCompany.DataTextField = "Name";
        ddlCompany.DataValueField = "CompanyId";
        ddlCompany.DataBind();

        //if (!IsMultiple)
        //{
        //    ddlParents.DataSource = Dal.ExeSp("GetCompanyExpertCombobox", ExpertId, CompanyId);
        //    ddlParents.DataTextField = "fullName";
        //    ddlParents.DataValueField = "ExpertId";
        //    ddlParents.DataBind();

        //}

    }

    private void FillData()
    {


        DataSet ds = Dal.ExeSpDataSet("GetExpertRegisterForSite", ExpertRegId);

        DataTable dt = ds.Tables[0];
        DataTable dtFam = ds.Tables[1];

        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];

            txtName.Text = row["Name"].ToString();
            txtSurname.Text = row["Surname"].ToString();
            txtPassport.Text = row["Passport"].ToString();
            txtComment.Text = row["Comments"].ToString();

            txtEmail.Text = row["Email"].ToString();
            txtAddress.Text = row["StreetAndHouse"].ToString();
            txtCountry.Text = row["Country"].ToString();
            txtTown.Text = row["Town"].ToString();

            txtPhone.Text = row["Phone"].ToString();
            txtJob.Text = row["Job"].ToString();

            txtDateBirth.Text = row["DateofBirth"].ToString();

            lblTitle.Text = row["Surname"].ToString() + " " + row["Name"].ToString();

            txtPassportIssueDate.Text = row["PassportIssueDate"].ToString();
            txtPassportExpDate.Text = row["PassportExpDate"].ToString();
            ddlCompany.SelectedValue = row["CompanyId"].ToString();


            txtStayIsraelStartDate.Text = row["StayIsraelStartDate"].ToString();
            txtStayIsraelEndDate.Text = row["StayIsraelEndDate"].ToString();
            txtCommentExpert.Text = row["Comment"].ToString();


            //string savePath = "~\\ExpertsUpload\\" + txtPassport.Text + "\\";

            //string SitePrefix = ConfigurationManager.AppSettings["SitePrefix"];

            //string webPath = SitePrefix + txtPassport.Text + "/";
            //string[] allFiles = Directory.GetFiles(Server.MapPath(savePath));

            //string AllFilesContainer = "";

            //string butt = "<button type='button' class='dvDownloadExpert ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only' role='button' aria-disabled='false'>"
            //      + "<span class='ui-button-text'>";


            ////  


            //foreach (string item in allFiles)
            //{
            //    AllFilesContainer = AllFilesContainer + butt + "<a  href='" + webPath + Path.GetFileName(item) + "' download>" + Path.GetFileName(item) + "</a>"
            //        + "</span></button>";



            //}







            //dvUploadFile.InnerHtml = AllFilesContainer;

        }



        if (dtFam.Rows.Count > 0)
        {
            txtSoupFamilyname.Text = dtFam.Rows[0]["Surname"].ToString();
            txtSoupGivenname.Text = dtFam.Rows[0]["Name"].ToString();

            txtSoupPassport.Text = dtFam.Rows[0]["Passport"].ToString();
            txtSoupPassportIsueDate.Text = dtFam.Rows[0]["PassportIssueDate"].ToString();
            txtSoupPassportExpDate.Text = dtFam.Rows[0]["PassportExpDate"].ToString();

            txtSoupPlaceofBirth.Text = dtFam.Rows[0]["Country"].ToString();
            txtSoupMaidenname.Text = dtFam.Rows[0]["Maidenname"].ToString();
            txtSoupFathersname.Text = dtFam.Rows[0]["Fathersname"].ToString();
            txtSoupDateofBirth.Text = dtFam.Rows[0]["DateofBirth"].ToString();
            HiddenFieldSoup.Value = dtFam.Rows[0]["ExpertRegId"].ToString();


        }


        for (int i = 1; i < dtFam.Rows.Count; i++)
        {
            TextBox txtChildGivenname = Page.FindControl("txtChild" + i.ToString() + "Givenname") as TextBox;
            txtChildGivenname.Text = dtFam.Rows[i]["Name"].ToString();
            TextBox txtChildCountryofbirth = Page.FindControl("txtChild" + i.ToString() + "Countryofbirth") as TextBox;
            txtChildCountryofbirth.Text = dtFam.Rows[i]["Country"].ToString();

            TextBox txtChildDateofBirth = Page.FindControl("txtChild" + i.ToString() + "DateofBirth") as TextBox;
            txtChildDateofBirth.Text = dtFam.Rows[i]["DateofBirth"].ToString();

            TextBox txtChildPassport = Page.FindControl("txtChild" + i.ToString() + "Passport") as TextBox;
            txtChildPassport.Text = dtFam.Rows[i]["Passport"].ToString();

            TextBox txtChildPassportIsueDate = Page.FindControl("txtChild" + i.ToString() + "PassportIsueDate") as TextBox;
            txtChildPassportIsueDate.Text = dtFam.Rows[i]["PassportIssueDate"].ToString();

            TextBox txtChildPassportExpDate = Page.FindControl("txtChild" + i.ToString() + "PassportExpDate") as TextBox;
            txtChildPassportExpDate.Text = dtFam.Rows[i]["PassportExpDate"].ToString();


            HiddenField hdnFiled = Page.FindControl("HiddenFieldChild" + i.ToString()) as HiddenField;
            hdnFiled.Value = dtFam.Rows[i]["ExpertRegId"].ToString();


        }



        FilesTableGenerate();



    }

    private void FilesTableGenerate()
    {
        string savePath = "~\\ExpertsUpload\\" + txtPassport.Text + "\\";

        string SitePrefix = ConfigurationManager.AppSettings["SitePrefix"];

        string webPath = SitePrefix + txtPassport.Text + "/";
        // string[] allFiles = Directory.GetFiles(Server.MapPath(savePath));

        DataTable dtFiles = Dal.ExeSp("SetExpertUploadFiles",
                         3,
                         0,
                         ExpertRegId,
                          "",
                         false
                         );


        string divContent = "<table id='tblUploadFiles' width='100%'><tr><th></th><th>Send</th> <th></th> <th>Uploaded Docs</th> <th>File Name</th> <th>Check Docs</th> <th>Remark</th> </tr>";

        string imgVV = "<img src='../App_Themes/Theme1/Images/vv.png'/>";
        string imgXX = "<img src='../App_Themes/Theme1/Images/xx.png'/>";
        foreach (DataRow row in dtFiles.Rows)
        {

            //  var ExpertId = row["ExpertId"].ToString();
            var Id = row["Id"].ToString();

            if (string.IsNullOrEmpty(Id)) Id = "0";

            var SeqId = row["SeqId"].ToString();
            var UploadName = row["UploadName"].ToString();
            var FileName = row["FileName"].ToString();
            var IsReadable = row["IsReadable"].ToString();
            var IsSend = row["IsSend"].ToString();
            var Remark = row["Remark"].ToString();
            bool IsUpload = false;
            bool IsReadableBool = false;
            bool IsSendBool = false;

            if (!string.IsNullOrEmpty(FileName))
                IsUpload = true;
            if (!string.IsNullOrEmpty(IsReadable) && IsReadable == "True")
                IsReadableBool = true;

            if (!string.IsNullOrEmpty(IsSend) && IsSend == "True")
                IsSendBool = true;

            divContent += "<tr><td>" + SeqId + "</td><td>&nbsp;<input type='checkbox'  onchange=UpdateRead(this.value," + Id + ","+ SeqId + ") id='sendmail_"+ SeqId + "' runat='server'" + ((IsSendBool)?"checked" : "") + "/></td>";
            divContent += "<td>" + UploadName + "</td>";// + FileName + "-"+ IsReadable + "</div>";
            divContent += "<td style='text-align:center'>" + ((IsUpload) ? imgVV : imgXX) + "</td>";
            divContent += "<td title='"+ FileName + "'><a  href='" + webPath + FileName + "' download><div class='filenamelimit'>" + FileName + "</div></a></td>";
            divContent += "<td style='text-align:center'><div style='cursor:pointer' isreadablebool='"+ IsReadableBool.ToString() + "'  runat='server' id='readable_" + SeqId + "'  onclick=UpdateRead('" + IsReadableBool.ToString() + "'," + Id + ","+ SeqId + ",true) />" + ((IsReadableBool && IsUpload) ? imgVV : ((IsUpload) ? imgXX : "")) + "</div></td>";
            if (IsUpload)
                divContent += "<td style='text-align:center;width:40%;padding:1px'>" +
                    "<input style='width:100%;border:0px !important;padding:2px !important;margin:0 !important;height:30px' id='remark_" + SeqId + "'  type='text'  " +
                    "onchange=UpdateRead(this.value," + Id + ","+ SeqId + ") value ='" + Remark + "' /></td></tr>";
            else
                divContent += "<td style='text-align:center;width:40%;padding:1px'></td></tr>";
        }




        dvDocsRegister.InnerHtml = divContent + "</table>";


        var LastDate =  dtFiles.Select("SendEmailDate is not null").FirstOrDefault();
        if(LastDate!=null)
           lastSendEmail.InnerText = LastDate[0].ToString();
        



        ZipAllFiles();

        string zipPath = "~\\ExpertsUpload\\" + txtPassport.Text + ".zip";
        webPath = SitePrefix + "/";

        if (File.Exists(Server.MapPath(zipPath)))
        {
            spDownload.InnerHtml = "<a id='btnDownloadAll' href='" + webPath + txtPassport.Text + ".zip" + "' download> Dowload All Files</a>";
        }
    }

    private void ZipAllFiles()
    {
        string savePath = "~\\ExpertsUpload\\" + txtPassport.Text + "\\";
        string zipPath = "~\\ExpertsUpload\\" + txtPassport.Text + ".zip";
        string SitePrefix = ConfigurationManager.AppSettings["SitePrefix"];
        string webPath = SitePrefix + txtPassport.Text + "/";

        if (Directory.Exists(Server.MapPath(savePath)))
        {

            //if (!File.Exists(Server.MapPath(zipPath)))
            //{
            try
            {

                if (File.Exists(Server.MapPath(zipPath)))
                {
                    File.Delete(Server.MapPath(zipPath));
                }


                ZipFile.CreateFromDirectory(Server.MapPath(savePath), Server.MapPath(zipPath));
            }
            catch (Exception ex)
            {


            }
            //}


        }
    }

    public void SendEmail_Click(object sender, EventArgs e)
    {
        string divContent = "<table id='tblUploadFiles' style='width: 100%; border-collapse: collapse;'><tr><th></th><th>Upload Name</th> <th>File Name</th> <th>Check Docs</th> <th>Remarks</th> </tr>";

        string imgVV = "<img src='http://dgtracking.co.il/App_Themes/Theme1/Images/vv.png'/>";
        string imgXX = "<img src='http://dgtracking.co.il/App_Themes/Theme1/Images/xx.png'/>";

        DataTable dtFiles = Dal.ExeSp("SetExpertUploadFiles",
                       3,
                       0,
                       ExpertRegId,
                        "",
                       false
                
                       );

        int Counter = 0;

        for (int i = 0; i < dtFiles.Rows.Count; i++)
        {
            DataRow row = dtFiles.Rows[i];
            //}
            //foreach (DataRow row in dtFiles.Rows)
            //{

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
          //  divContent += "<td style='text-align:center'>" + ((IsUpload) ? imgVV : imgXX) + "</td>";
            divContent += "<td>" + FileName + "</td>";
            divContent += "<td style='text-align:center'>" + ((IsReadableBool && IsUpload) ? imgVV : ((IsUpload) ? imgXX : "")) + "</td>";
            if (!string.IsNullOrEmpty(Id))
                divContent += "<td style='text-align:left;width:40%;padding:1px'>"+Remark+"</td></tr>";
            else
                divContent += "<td style='text-align:center;width:40%;padding:1px'></td></tr>";
        }



        string bodyFiles = divContent + "</table>";

     
        string Subject = txtSurname.Text + " " + txtName.Text + " - your Documents status.";
      
        
        //string Body = "<b>Hello Dear</b>  <br /><br />  <b>First Name:</b>" + txtName.Text + " <br /> <b>Surename:</b>" + txtSurname.Text
        //     + "<br/><b> Passport:</b> " + txtPassport.Text + " <br/><br/>";
       
        //    Body += "<br /><b> Please Check Files Error and Send back:</b><br/>" + bodyFiles;




        string Body = @"
    
 <div style='font-family:Calibri;font-size:14px'>
       <b> --- This is an automated email, do not reply. For any further questions, please contact our office: <span style='color:#0000FF;'>michal@dg-immigration.com</span> ---</b>
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



        Body = string.Format(Body, txtSurname.Text + " " + txtName.Text, txtPassport.Text, "https://dgtracking.co.il/RegExpert/Register.aspx?CompanyId=" + CompanyId);



        if (Counter > 0)
        {
            Send(Subject, Body, "");

            hdnSendMail.Value = "1";
                
            string sqlRe = "Update ExperUploadFiles set SendEmailDate='{1}' where ExpertId = {0}";

            sqlRe = string.Format(sqlRe, ExpertRegId,DateTime.Now.ToString("yyyy-MM-dd"));

            int resRe = Dal.ExecuteNonQuery(sqlRe);

            FilesTableGenerate();

            
        }

    }
   
    protected void btnUpdateRead_Click(object sender, EventArgs e)
    {
        string Remark = hdnExperRemark.Value;
        string ExperUploadId = hdnExperUploadId.Value;
        string IsReadableBool = hdnIsReadableBool.Value;
        string IsSendmailBool = hdnIsSendmailBool.Value;
        string SeqId = hdnSeqId.Value;

        // string Current = ExpertRegId;
        int Read = 1;
        if (IsReadableBool == "True")
            Read = 0;

        int Sendmail = 0;
        if (IsSendmailBool == "true")
            Sendmail = 1;

        if (ExperUploadId!="0") {
            string sqlRe = "Update ExperUploadFiles set Remark='" + Remark + "',IsReadable=" + Read + ",IsSend=" + Sendmail + " where Id = " + ExperUploadId;
            if (IsReadableBool== "noClick")
            {
                 sqlRe = "Update ExperUploadFiles set Remark='" + Remark + "',IsSend=" + Sendmail + " where Id = " + ExperUploadId;

            }
             
              int resRe = Dal.ExecuteNonQuery(sqlRe);
        }
        else
        {

            string sqlRe = "insert into ExperUploadFiles (IsSend,ExpertId,UploadId) values(" + Sendmail + "," +ExpertRegId+ ","+ SeqId + ")"; // where Id = " + ExperUploadId;
            int resRe = Dal.ExecuteNonQuery(sqlRe);


        }

        FilesTableGenerate();

        //if (Remark != "0")
        //{
        //    string sqlRe = "Update ExperUploadFiles set Remark='" + Remark + "' where Id = " + ExperUploadId;

        //    int resRe = Dal.ExecuteNonQuery(sqlRe);

        //    FilesTableGenerate();


        //}


        //if (IsReadableBool != "0")
        //{





        //    int Read = 1;
        //    if (IsReadableBool == "True")
        //        Read = 0;
        //    string sql = "Update ExperUploadFiles set IsReadable=" + Read + " where Id = " + ExperUploadId;

        //    int res = Dal.ExecuteNonQuery(sql);

        //    FilesTableGenerate();
        //}
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {


        string NewExpertRegisterId = InsertIntoDB(txtSurname.Text,
        txtName.Text,
        ddlCompany.SelectedValue.ToString(),
        txtEmail.Text,
        txtPhone.Text,
        txtJob.Text,
        txtPassport.Text,
        txtPassportIssueDate.Text,
        txtPassportExpDate.Text,
        "0",
        txtAddress.Text,
        txtTown.Text,
        txtCountry.Text,
        "",
        "",
        "0",
        txtDateBirth.Text,
        ExpertRegId,
        txtComment.Text,
        txtStayIsraelStartDate.Text,
        txtStayIsraelEndDate.Text,
        txtCommentExpert.Text
        );

        string SoupExpertRegisterId = InsertIntoDB(txtSoupFamilyname.Text,
          txtSoupGivenname.Text,
          ddlCompany.SelectedValue,
          "",
          "",
          "",
          txtSoupPassport.Text,
          txtSoupPassportIsueDate.Text,
          txtSoupPassportExpDate.Text,
          ExpertRegId,
          "",
          "",
          txtSoupPlaceofBirth.Text,
          txtSoupMaidenname.Text,
          txtSoupFathersname.Text,
          "1",
          txtSoupDateofBirth.Text,
          HiddenFieldSoup.Value,
          "",
          "",
          "",
          ""
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
                                                ddlCompany.SelectedValue,
                                                "",
                                                "",
                                                "",
                                                txtChildPassport.Text,
                                                txtChildPassportIsueDate.Text,
                                                txtChildPassportExpDate.Text,
                                                ExpertRegId,
                                                "",
                                                "",
                                                txtChildCountryofbirth.Text,
                                                "",
                                                "",
                                                "2",
                                                txtChildDateofBirth.Text,
                                                hdnFiled.Value,
                                                "",
                                                "",
                                                  "",
                                                  ""


                                                );

            }

        }




        ClientScript.RegisterStartupScript(GetType(), "hwa", "parent.ShowMessage('The Data Save Succeed!!!','2');", true);
        ClientScript.RegisterStartupScript(GetType(), "hwa2", "closeDialog();", true);
    }

    protected void btnMove_Click(object sender, EventArgs e)
    {



        Dal.ExeSp("MoveExpertRegisterTOLIVE", ExpertRegId);





        ClientScript.RegisterStartupScript(GetType(), "hwa", "parent.ShowMessage('The Expert Move To Live Experts!!!','2');", true);
        ClientScript.RegisterStartupScript(GetType(), "hwa2", "closeDialog();", true);
    }

    protected void btnGoBack_Click(object sender, EventArgs e)
    {
        string sqlRe = "Update ExpertRegister set Status=0 where ParentId = {0} Or ExpertRegId = {0}";

        sqlRe = string.Format(sqlRe, ExpertRegId);
        
        int resRe = Dal.ExecuteNonQuery(sqlRe);

        ClientScript.RegisterStartupScript(GetType(), "hwa", "parent.ShowMessage('The Expert Move To Incoming Experts!!!','2');", true);
        ClientScript.RegisterStartupScript(GetType(), "hwa2", "closeDialog();", true);

        
    }

    private string InsertIntoDB(string Surname, string Name, string CompanyId, string Email, string Phone, string Job,
       string Passport, string PassportIssueDate, string PassportExpDate, string ParentId, string StreetAndHouse, string Town,
       string Country, string Maidenname, string Fathersname, string IsFamaly, string DateofBirth, string ExpertFamId, string Comment,string StayIsraelStartDate, string StayIsraelEndDate, string CommentExpert)
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
         Comment,
         GetAsDate(StayIsraelStartDate),
         GetAsDate(StayIsraelEndDate),
         CommentExpert,
         true
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

        string Sendformemail = "";
        DataTable CompanyMail = Dal.GetDataTable("Select * from Company where IsSendformemail=1 and Sendformemail is not null and CompanyId=" + CompanyId);
        if (CompanyMail.Rows.Count > 0)
        {
            Sendformemail = CompanyMail.Rows[0]["Sendformemail"].ToString();

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

        actMSG.To.Add(txtEmail.Text);


        if (!string.IsNullOrEmpty(officeMails))
        {
            actMSG.To.Add(officeMails);
        }

        // הוספת שליחה לבן אדם של החברה
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

