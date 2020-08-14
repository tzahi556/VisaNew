using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Expressions;
using System.Data;

public partial class Pages_PopUp_editCustomer : System.Web.UI.Page
{
    protected string ExpertId = "";
    public string RoleId = "";
    public bool IsMultiple = false;

    public string StepId = "";
    public string CompanyId = "";
    public HttpCookie cookie = null;

    protected void Page_Load(object sender, EventArgs e)
    {



        if (HttpContext.Current.Request.Cookies["UserData"] != null)
        {
            cookie = HttpContext.Current.Request.Cookies["UserData"];
        }

        RoleId = cookie["RoleId"].ToString();

        ExpertId = Request.QueryString["ExpertId"];

        StepId = Request.QueryString["StepId"];

        CompanyId = Request.QueryString["CompanyId"];

        if (ExpertId.IndexOf(",") != -1)
        {
            IsMultiple = true;
        }

        if (!Page.IsPostBack)
        {
            InitDropDown();
            FillData();
            FillGrid();

        }

      

    }

    private void FillGrid()
    {
        if (!IsMultiple)
        {
            DataTable dtFamily = Dal.ExeSp("GetExpertFamily", ExpertId);
            gvTheGrid.DataSource = dtFamily;
            gvTheGrid.DataBind();
        }
    }

    private void InitDropDown()
    {
        ddlCompany.DataSource = Dal.ExeSp("GetCompany", "", "", "0", "0");
        ddlCompany.DataTextField = "Name";
        ddlCompany.DataValueField = "CompanyId";
        ddlCompany.DataBind();

        if (!IsMultiple)
        {
            ddlParents.DataSource = Dal.ExeSp("GetCompanyExpertCombobox", ExpertId, CompanyId);
            ddlParents.DataTextField = "fullName";
            ddlParents.DataValueField = "ExpertId";
            ddlParents.DataBind();

        }

    }

    private void FillData()
    {
        DataTable dt = null;


        if (IsMultiple)
        {
            dt = Dal.ExeSp("GetMultipleExperts", ExpertId, StepId);
            if (dt.Rows.Count > 1)
            {
                ClientScript.RegisterStartupScript(GetType(), "hwa", "parent.ShowMessage('The Data Of Selected Not Equal','4');", true);
                ClientScript.RegisterStartupScript(GetType(), "hwa2", "closeDialog();", true);
                return;
            }

        }
        else
        {
            dt = Dal.ExeSp("GetExpert", "", "", "0", ExpertId, "0","0");
        }


        if (dt.Rows.Count > 0)
        {
            DataRow row = dt.Rows[0];
            if (!IsMultiple)
            {
                txtName.Text = row["Name"].ToString();
                txtSurname.Text = row["Surname"].ToString();
                txtPassport.Text = row["Passport"].ToString();
                txtComment.Text = row["Comments"].ToString();
                chActive.Checked = bool.Parse(row["Active"].ToString());
                chIsEmail.Checked = bool.Parse(row["IsEmail"].ToString());
                chIsMonthly.Checked = bool.Parse(row["IsMonthly"].ToString());
                chIs90.Checked = bool.Parse(row["Is90"].ToString());
                txtEmail.Text = row["Email"].ToString();
                txtAddress.Text = row["Address"].ToString();
                txtPhone.Text = row["Phone"].ToString();
                txtJob.Text = row["Job"].ToString();
                txtJobHeb.Text = row["JobHeb"].ToString();
              //  txtJobAbroad.Text = row["JobAbroad"].ToString();

                txtTown.Text = row["Town"].ToString();
                txtCountry.Text = row["Country"].ToString();
                txtDateofBirth.Text = row["DateOfBirth"].ToString();

                txtNationality.Text = row["Nationality"].ToString();
                //txtNationalityHeb.Text = row["NationalityHeb"].ToString();
                lblTitle.Text = row["Surname"].ToString() + " " + row["Name"].ToString();
                txtClientDate.Text = row["Client authorization Date"].ToString();

                txtInviteVisa.Text = row["Visa/ Invitation Issue date"].ToString();
                txtExpVisa.Text = row["Visa Exp Date"].ToString();
                txtSubmitMulti.Text = row["Multiple Issue Visa"].ToString();
                txtExpMulti.Text = row["Multiple entry Visa"].ToString();
                txtAppSub.Text = row["Approval Submition Date"].ToString();
                txtAppStart.Text = row["Approval From Date"].ToString();
                txtAppExp.Text = row["Approval Exp Date"].ToString();
                txtFirstEntry.Text = row["First Entry"].ToString();
                lblDiff.Text = (row["diffday"].ToString() == "5000") ? "" : row["diffday"].ToString();

                ddlParents.SelectedValue = row["ParentId"].ToString();

                ddlLevel.SelectedValue = row["LevelId"].ToString();

                txtPassportIssueDate.Text = row["PassportIssueDate"].ToString();
                txtPassportExpDate.Text = row["PassportExpDate"].ToString();

                // חדש צחי הוסיף
                txtApplicationref.Text = row["Applicationref"].ToString();

                


            }
            else
            {
                lblTitle.Text = "Selected Experts - Step " + StepId;
                txtName.Enabled = false;
                txtSurname.Enabled = false;
                txtPassport.Enabled = false;
                txtPassportIssueDate.Enabled = false;
                txtPassportExpDate.Enabled = false;

                txtAddress.Enabled = false;
                txtCountry.Enabled = false;
                txtTown.Enabled = false;
                txtDateofBirth.Enabled = false;

                txtPhone.Enabled = false;
                txtJob.Enabled = false;
                txtJobHeb.Enabled = false;
                txtNationality.Enabled = false;
                //txtNationalityHeb.Enabled = false;


                txtApplicationref.Enabled = false;
                txtClientDate.Enabled = false;
                txtFirstEntry.Enabled = false;
                //txtJobAbroad.Enabled = false;
               
                txtEmail.Enabled = false;
                ddlParents.Enabled = false;
                chIsMonthly.Enabled = false;
                chIs90.Enabled = false;
                if (StepId != "1")
                {
                    txtAppSub.Enabled = false;
                    txtAppStart.Enabled = false;
                    txtAppExp.Enabled = false;
                    // txtClientDate.Enabled = false;
                }
                else
                {
                    txtAppSub.Text = row["Approval Submition Date"].ToString();
                    txtAppStart.Text = row["Approval From Date"].ToString();
                    txtAppExp.Text = row["Approval Exp Date"].ToString();
                    // txtClientDate.Text = row["Client authorization Date"].ToString();
                }

                if (StepId != "2")
                {
                    txtInviteVisa.Enabled = false;
                    txtExpVisa.Enabled = false;
                }
                else
                {
                    txtInviteVisa.Text = row["Visa/ Invitation Issue date"].ToString();
                    txtExpVisa.Text = row["Visa Exp Date"].ToString();
                }


                if (StepId != "3")
                {
                    txtSubmitMulti.Enabled = false;
                    txtExpMulti.Enabled = false;
                }
                else
                {
                    txtSubmitMulti.Text = row["Multiple Issue Visa"].ToString();
                    txtExpMulti.Text = row["Multiple entry Visa"].ToString();
                }


                if (StepId != "4")
                {
                    ddlCompany.Enabled = false;
                }
                else
                {

                }


                if (StepId != "5")
                {
                    chActive.Enabled = false;
                    //chIsEmail.Enabled = false;
                }
                else
                {
                    chActive.Checked = bool.Parse(row["Active"].ToString());
                    // chIsEmail.Checked = bool.Parse(row["IsEmail"].ToString());
                }


                if (StepId != "6")
                {
                    txtComment.Enabled = false;
                }
                else
                {
                    txtComment.Text = row["Comments"].ToString();
                }


                if (StepId != "7")
                {
                    chIsEmail.Enabled = false;
                    //chIsEmail.Enabled = false;
                }
                else
                {
                    chIsEmail.Checked = bool.Parse(row["IsEmail"].ToString());
                    // chIsEmail.Checked = bool.Parse(row["IsEmail"].ToString());
                }

                if (StepId == "8")
                {
                    txtJob.Enabled = true;
                    txtJobHeb.Enabled = true;
                   // txtJobAbroad.Enabled = true;
                }





                btnSave.ValidationGroup = "";




            }

            ddlCompany.SelectedValue = row["CompanyId"].ToString();


        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (IsMultiple)
        {

            object Date1 = GetAsDate(txtAppSub.Text);
            object Date2 = GetAsDate(txtAppStart.Text);
            object Date3 = GetAsDate(txtAppExp.Text);
            object Date4 = GetAsDate(txtClientDate.Text);

            bool Active = chActive.Checked;
            string comments = txtComment.Text;



            if (StepId == "2")
            {
                Date1 = GetAsDate(txtInviteVisa.Text);
                Date2 = GetAsDate(txtExpVisa.Text);
            }

            if (StepId == "3")
            {
                Date1 = GetAsDate(txtSubmitMulti.Text);
                Date2 = GetAsDate(txtExpMulti.Text);
            }





            Dal.ExeSp("SetMultipleExperts", ExpertId, ddlCompany.SelectedValue,
                 Date1, Date2, Date3, Date4,
               Active, comments, StepId, chIsEmail.Checked, txtJob.Text, txtJobHeb.Text, ""
                 );

        }
        else
        {
            Dal.ExeSp("SetExpert", ExpertId, txtName.Text, txtSurname.Text, txtPassport.Text, ddlCompany.SelectedValue,
                GetAsDate(txtClientDate.Text),
              GetAsDate(txtAppSub.Text), GetAsDate(txtAppStart.Text), GetAsDate(txtAppExp.Text), GetAsDate(txtInviteVisa.Text),
              GetAsDate(txtExpVisa.Text), GetAsDate(txtSubmitMulti.Text), GetAsDate(txtExpMulti.Text), txtComment.Text,
              chActive.Checked, chIsEmail.Checked
             , txtPhone.Text, txtAddress.Text,txtCountry.Text,txtTown.Text, txtJob.Text, txtJobHeb.Text, "", txtNationality.Text,
             GetAsDate(txtDateofBirth.Text), txtFirstEntry.Text, txtEmail.Text,ddlParents.SelectedValue,chIsMonthly.Checked, chIs90.Checked,
             ddlLevel.SelectedValue,GetAsDate(txtPassportIssueDate.Text),GetAsDate(txtPassportExpDate.Text), txtApplicationref.Text
               );

        }


        //if (ExpertId == "-1")
        //{

        //    txtAppExp.Text = "";
        //    txtAppStart.Text = "";
        //    txtAppSub.Text = "";
        //    txtComment.Text = "";
        //    txtExpMulti.Text = "";
        //    txtExpVisa.Text = "";
        //    txtName.Text = "";
        //    txtPassport.Text = "";
        //    txtSubmitMulti.Text = "";
        //    txtSurname.Text = "";
        //    txtClientDate.Text = "";
        //    ddlCompany.SelectedValue = "0";


        //}

        ClientScript.RegisterStartupScript(GetType(), "hwa", "parent.ShowMessage('The Data Save Succeed!!!','2');", true);
        ClientScript.RegisterStartupScript(GetType(), "hwa2", "closeDialog();", true);
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

