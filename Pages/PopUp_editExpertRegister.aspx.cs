﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Expressions;
using System.Data;
using System.IO;
using System.Configuration;

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





            string savePath = "~\\ExpertsUpload\\" + txtPassport.Text + "\\";

            string SitePrefix = ConfigurationManager.AppSettings["SitePrefix"];

            string webPath = SitePrefix + txtPassport.Text + "/";
            string[] allFiles = Directory.GetFiles(Server.MapPath(savePath));

            string AllFilesContainer = "";

            string butt = "<button type='button' class='dvDownloadExpert ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only' role='button' aria-disabled='false'>"
                  + "<span class='ui-button-text'>";


            //  


            foreach (string item in allFiles)
            {
                AllFilesContainer = AllFilesContainer + butt + "<a  href='" + webPath + Path.GetFileName(item) + "' download>" + Path.GetFileName(item) + "</a>"
                    + "</span></button>";



            }






            //dvUploadFile.InnerHtml = "<a href="+array1[0].ToString()+" download='sssss.png'>sdsdds</a>";
            // Directory.CreateDirectory(Server.MapPath(savePath + ""));
            //string savePath = "http://localhost/VisaNew/ExpertsUpload/" + txtPassport.Text + "/";
            //string[] array1 = Directory.GetFiles(Server.MapPath(savePath));

            dvUploadFile.InnerHtml = AllFilesContainer;

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
        txtComment.Text
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


    


    private string InsertIntoDB(string Surname, string Name, string CompanyId, string Email, string Phone, string Job,
       string Passport, string PassportIssueDate, string PassportExpDate, string ParentId, string StreetAndHouse, string Town,
       string Country, string Maidenname, string Fathersname, string IsFamaly, string DateofBirth,string ExpertFamId,string Comment)
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
         Comment
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
}

