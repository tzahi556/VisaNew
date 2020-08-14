using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Expressions;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Text;
using System.IO;

public partial class Pages_PopUp_editSendEmailWeekly : System.Web.UI.Page
{
    public string Id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        Id = Request.QueryString["Id"];
        if (!Page.IsPostBack)
        {
            InitDropDown();
            FillData();
        }

       // lblCompanyIds.Value = "0";

    }

    private void InitDropDown()
    {
      

        DataTable dtCompany = Dal.ExeSp("GetCompany", "", "", "0", "0");
        chCompanyList.DataSource = dtCompany;
        chCompanyList.DataTextField = "Name";
        chCompanyList.DataValueField = "CompanyId";
        chCompanyList.DataBind();



        ddlDays.DataSource = Dal.ExeSp("GetSetSendEmailWeekly", "3", "0","", "", "", "", "", "", "0", "false", "0", "");
        ddlDays.DataTextField = "Name";
        ddlDays.DataValueField = "DayId";
        ddlDays.DataBind();

    }


    protected void chCompanyList_SelectedIndexChnaged(object sender, System.EventArgs e)
    {
        Label1.Text = "";
        //Label1.Text = "You Choose Color:<br /><i>";
        foreach (ListItem li in chCompanyList.Items)
        {
            if (li.Selected == true)
            {
                Label1.Text += li.Text + "<br />";
            }
           
        }
        //Label1.Text += "</i>";



    }

    private void FillData()
    {


        DataTable dt = Dal.ExeSp("GetSetSendEmailWeekly", "2", Id, "", "", "", "", "", "", "0", "false","0","");

        if (dt.Rows.Count > 0)
        {



            DataRow row = dt.Rows[0];

            txtSubject.Text = row["Subject"].ToString();

            txtEmail.Text = row["Email"].ToString();
            txtEmailCopy.Text = row["EmailCopy"].ToString();
            txtEmailHide.Text = row["EmailHide"].ToString();

            txtBody.Value = row["Body"].ToString();
            chActive.Checked = bool.Parse(row["Active"].ToString());
            lblTitle.Text = "Email Task Weekly :" + Id;
            ddlDays.SelectedValue = row["DayId"].ToString();

            txtTitle.Text = row["Title"].ToString();
            ddlHour.SelectedValue = row["HourId"].ToString();

            string[] ComaniesIds = (row["ComaniesIds"].ToString()).Split(',');


            foreach (ListItem li in chCompanyList.Items)
            {


                foreach (string ComaniesId in ComaniesIds)
                {
                    if (li.Value == ComaniesId)
                    {
                        li.Selected = true;
                        lblCompanyIds.Value +=  "," + li.Value;
                        dvComp.InnerHtml += li.Text + "<br/>";

                    }
                       
                }

               
            }


        }
        else
        {
            chActive.Checked = true;

        }



    }


    public bool IsValid(string emailaddress)
    {
        try
        {

            string[] Emails = emailaddress.Split(',');
            foreach (string email in Emails)
            {
                if (!string.IsNullOrEmpty(email))
                {
                    MailAddress m = new MailAddress(email);
                }
                     
            }
           
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {

        if (CheckAllValidation())
        {
            string CompaniesId = lblCompanyIds.Value;
            SendEmailWeekly se = new SendEmailWeekly(txtSubject.Text, "0"+ CompaniesId, txtEmail.Text, txtEmailCopy.Text, txtEmailHide.Text, txtBody.Value);

        }

    }

    private bool CheckAllValidation()
    {
        if (string.IsNullOrEmpty(lblCompanyIds.Value))
        {
            ClientScript.RegisterStartupScript(GetType(), "hwa", "parent.ShowMessage('Select Companies Is Required','1');", true);
            return false;

        }


        if (string.IsNullOrEmpty(txtEmail.Text))
        {
            ClientScript.RegisterStartupScript(GetType(), "hwa", "parent.ShowMessage('Field Email Is Required','1');", true);
            return false;
        }

        if (!IsValid(txtEmail.Text))
        {
            ClientScript.RegisterStartupScript(GetType(), "hwa", "parent.ShowMessage('Field Email Not Valid','3');", true);
            return false;
        }

        if (!string.IsNullOrEmpty(txtEmailCopy.Text))
        {
            if (!IsValid(txtEmailCopy.Text))
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "parent.ShowMessage('Field Email Copy Not Valid','3');", true);
                return false;

            }
        }

        if (!string.IsNullOrEmpty(txtEmailHide.Text))
        {
            if (!IsValid(txtEmailHide.Text))
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "parent.ShowMessage('Field Email Hide Not Valid','3');", true);
                return false;

            }
        }


        if (string.IsNullOrEmpty(txtBody.Value))
        {
            ClientScript.RegisterStartupScript(GetType(), "hwa", "parent.ShowMessage('Field Body Is Required','1');", true);
            return false;
        }

        return true;

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (!CheckAllValidation()) return;

       

        string CompaniesId = lblCompanyIds.Value;
        Dal.ExeSp("GetSetSendEmailWeekly", "4", Id,txtSubject.Text, CompaniesId, txtBody.Value, txtEmail.Text, txtEmailCopy.Text, txtEmailHide.Text, ddlDays.SelectedValue, chActive.Checked,ddlHour.SelectedValue,txtTitle.Text);
        ClientScript.RegisterStartupScript(GetType(), "hwa", "parent.ShowMessage('The Data Save Succeed!!!','2');", true);
        ClientScript.RegisterStartupScript(GetType(), "hwa2", "closeDialog();", true);
        
    }
}

