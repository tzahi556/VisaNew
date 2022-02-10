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
    public string CompanyId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        CompanyId = Request.QueryString["CompanyId"];
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

    }

    private void FillData()
    {


        DataTable dt = Dal.ExeSp("GetCompany", "", "", CompanyId, "0");

        if (dt.Rows.Count > 0)
        {



            DataRow row = dt.Rows[0];

            if (!string.IsNullOrEmpty(row["Parent_Id"].ToString()))
                ddlCompany.SelectedValue = row["Parent_Id"].ToString();

            txtName.Text = row["Name"].ToString();
            txtNumber.Text = row["Number"].ToString();
            txtPhone.Text = row["Phone"].ToString();

            txtEmail.Text = row["Email"].ToString();
            txtContactMan.Text = row["ContactMan"].ToString();

            txtCountry.Text = row["Country"].ToString();
            txtContactManPhone.Text = row["ContactManPhone"].ToString();
            txtAddress.Text = row["Address"].ToString();

            txtContactManEmail.Text = row["ContactManEmail"].ToString();
            txtContactManJob.Text = row["ContactManJob"].ToString();
            txtAuthorizedSigner.Text = row["AuthorizedSigner"].ToString();
            txtAuthorizedSignerJob.Text = row["AuthorizedSignerJob"].ToString();
            txtAuthorizedSignerPassport.Text = row["AuthorizedSignerPassport"].ToString();
            txtWork.Text = row["CompanyWork"].ToString();
            chActive.Checked = bool.Parse(row["Active"].ToString());
            chIsExp.Checked = bool.Parse(row["IsExpAllow"].ToString());
            chIsEmail.Checked = bool.Parse(row["IsEmail"].ToString());
            lblTitle.Text = row["Name"].ToString();
            txtInteriorReg.Text = row["Interior Reg"].ToString();

            txtPassportCountry.Text = row["PassportCountry"].ToString();
            txtIsraelID.Text = row["IsraelID"].ToString();

            txtSendFormEmail.Text = row["Sendformemail"].ToString();
            chSendFormEmail.Checked = bool.Parse(row["IsSendformemail"].ToString());


        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {


        Dal.ExeSp("SetCompany", CompanyId, txtName.Text, txtNumber.Text, txtPhone.Text,
            txtEmail.Text, txtContactMan.Text, txtContactManEmail.Text, txtContactManPhone.Text,
              txtAddress.Text, chIsExp.Checked, chActive.Checked, chIsEmail.Checked, txtContactManJob.Text, txtAuthorizedSigner.Text,
              txtAuthorizedSignerJob.Text, txtAuthorizedSignerPassport.Text, txtWork.Text, txtCountry.Text, txtInteriorReg.Text, ddlCompany.SelectedValue
           ,txtPassportCountry.Text,txtIsraelID.Text, txtSendFormEmail.Text, chSendFormEmail.Checked);

        //if (CompanyId == "-1")
        //{

        //    txtName.Text = "";
        //    txtNumber.Text = "";
        //    txtPhone.Text = "";
        //    txtEmail.Text = "";
        //    txtContactMan.Text = "";
        //    txtContactManEmail.Text = "";
        //    txtContactManPhone.Text = "";
        //    txtAddress.Text = "";



        //}

        ClientScript.RegisterStartupScript(GetType(), "hwa", "parent.ShowMessage('The Data Save Succeed!!!','2');", true);
        ClientScript.RegisterStartupScript(GetType(), "hwa2", "closeDialog();", true);

        //}

        //private object GetAsDate(string date)
        //{

        //    try
        //    {
        //        return Convert.ToDateTime(date);
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
    }
}

