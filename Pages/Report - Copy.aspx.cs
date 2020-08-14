using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        string html = hdn.Value;
        if (html != "0")
        {
            dvDrop.InnerHtml = html;
        }


        if (!IsPostBack)
        {
            //ddlCompany.DataSource = Dal.ExeSp("GetCompany", "", "", "0", "0");
            //ddlCompany.DataTextField = "Name";
            //ddlCompany.DataValueField = "CompanyId";
            //ddlCompany.DataBind();

            InitDropDownList();



        }





    }

    private void InitDropDownList()
    {
        DataTable dtCompany = Dal.ExeSp("GetCompany", "", "", "0", "0");
        chCompanyList.DataSource = dtCompany;
        chCompanyList.DataTextField = "Name";
        chCompanyList.DataValueField = "CompanyId";
        chCompanyList.DataBind();

        RefreshDDLprev();

    }

    private void RefreshDDLprev()
    {
        ddlPrevReport.Items.Clear();
        ddlPrevReport.Items.Add(new ListItem("-- Choose Saved Report --", "0"));
        ddlPrevReport.DataSource = Dal.ExeSp("GetReportSaved");
        ddlPrevReport.DataTextField = "Name";
        ddlPrevReport.DataValueField = "ReportId";
        ddlPrevReport.DataBind();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
       

        if (txtNewName.Text != "")
        {
            Dal.ExeSp("SetReport", txtNewName.Text, dvDrop.InnerHtml, ch160.Checked, ch75160.Checked, ch74.Checked, ch0.Checked);
            RefreshDDLprev();

        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (ddlPrevReport.SelectedValue != "0")
        {
            Dal.ExeSp("DeleteReport", ddlPrevReport.SelectedValue);
            RefreshDDLprev();
        }
    }


    protected void ddlPrevReport_SelectedChange(object sender, EventArgs e)
    {
        if (ddlPrevReport.SelectedValue != "0")
        {

            DataTable dt = Dal.ExeSp("GetPrevReport", ddlPrevReport.SelectedValue);

            string Html = dt.Rows[0]["Html"].ToString();

            dvDrop.InnerHtml = Html;

            ch160.Checked = bool.Parse(dt.Rows[0]["Is160"].ToString());
            ch75160.Checked = bool.Parse(dt.Rows[0]["Is75160"].ToString());
            ch74.Checked = bool.Parse(dt.Rows[0]["Is74"].ToString());
            ch0.Checked = bool.Parse(dt.Rows[0]["IsBelow"].ToString());

        }
    }



    protected void btnExcel_Click(object sender, EventArgs e)
    {





        string strSelectedItem = string.Empty;
        //  CheckBoxList chk = (CheckBoxList)phDDLCHK.FindControl("chkLstItem");
        DropDownList ddl = (DropDownList)Page.FindControl("ddlChkList");

        string CompaniesId = "";
        for (int i = 0; i < chCompanyList.Items.Count; i++)
        {


            if (chCompanyList.Items[i].Selected)
            {
                string val = chCompanyList.Items[i].Value;
                CompaniesId = CompaniesId + val + ",";

                if (val == "0")
                {
                    CompaniesId = "0";
                    break;
                }

            }
        }

        if (CompaniesId != "0" && CompaniesId != "")
            CompaniesId = CompaniesId.Substring(0, CompaniesId.Length - 1);


        string sql_select = "";
        string sql_where = "";
        bool isFirst = true;



        foreach (string key in Request.Form.AllKeys)
        {
            if (key.IndexOf("ch_") != -1)
            {

                string first = key.Replace("ch_", "");
                int lastIndex = first.LastIndexOf("_");
                string field = first.Substring(0, lastIndex);




                string query = GetSQLField(field, true);




                sql_select = sql_select + ((!isFirst) ? "," : "") + query;
                isFirst = false;

                if (field == "Title")
                {
                    sql_select = sql_select + " ,JobHeb as TitleHeb,JobAbroad as TitleAbroad ";
                }


            }

            if (key.IndexOf("radio_") != -1)
            {

                string field = key.Replace("radio_", "");
                string val = Request.Form[key];

                if (field == "Company" || field == "Active" || field == "Days To Expire")
                {
                    continue;
                }

                field = GetSQLField(field, false);

                if (field == "e.MarriageCertificate" || field == "e.Cv" || field == "e.Diploma")
                {
                    if (val == "Done")
                    {
                        sql_where = sql_where + "  and (" + field + "!=0 )";
                    }


                    if (val == "Not Done")
                    {
                        sql_where = sql_where + "  and (" + field + "=0 )";

                    }

                }
                else
                {


                    if (val == "Done")
                    {
                        sql_where = sql_where + "  and (" + field + " IS NOT NULL And " + field + "!='' )";
                    }


                    if (val == "Not Done")
                    {
                        sql_where = sql_where + "  and (" + field + " IS NULL Or " + field + " ='' )";
                        //  isFirstWhere = false;

                    }

                }



            }

        }



        if (sql_select != "" && CompaniesId != "")
        {


            string selected = rdActive.SelectedValue;

            if (selected != "1")
            {

                if (selected == "2")
                {
                    sql_where = sql_where + "  and (e.Active=1 and (GetDate() <= e.[Visa Exp Date] Or e.[Visa Exp Date] IS NULL ))";

                }

                if (selected == "3")
                {
                    sql_where = sql_where + "  and (e.Active=0  OR GetDate() > e.[Visa Exp Date]) ";
                }


            }


            if (ch160.Checked)
            {
                sql_where = sql_where + "  and (Coalesce(DateDiff(day,getDate(),[Visa Exp Date]),5000) >= 120)";

            }

            if (ch75160.Checked)
            {
                sql_where = sql_where + "  and (Coalesce(DateDiff(day,getDate(),[Visa Exp Date]),5000) < 120  and  Coalesce(DateDiff(day,getDate(),[Visa Exp Date]),5000) >=90 )";

            }

            if (ch74.Checked)
            {
                sql_where = sql_where + "  and (Coalesce(DateDiff(day,getDate(),[Visa Exp Date]),5000) < 90  and  Coalesce(DateDiff(day,getDate(),[Visa Exp Date]),5000) >=0 )";

            }

            if (ch0.Checked)
            {
                sql_where = sql_where + "  and (Coalesce(DateDiff(day,getDate(),[Visa Exp Date]),5000) < 0)";

            }

            string sql = "";


            if (CompaniesId == "0")
            {
                sql = " Select " + sql_select + " From Expert e inner join company c on c.CompanyId = e.CompanyId"
                     + " Where (1=1) " + sql_where + " Order By c.CompanyId ";
            }
            else
            {
                sql = " Select " + sql_select + " From Expert e inner join company c on c.CompanyId = e.CompanyId"
                     + " Where (1=1) and c.CompanyId in (" + CompaniesId + ")" + sql_where + " Order By c.CompanyId ";

            }

            DataTable dt = Dal.GetDataTable(sql);


            // dt = city.GetAllCity();//your datatable
            string attachment = "attachment; filename=Report" + DateTime.Now.ToString() + ".xls";
            Response.ClearContent();
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1255");
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();




        }
    }

    private string GetSQLField(string field, bool IsQuery)
    {
        string prev = "";
        string next = "";


        if (field == "Expert Name")
        {
            field = "Name";
            prev = "e.";
            next = " as [Expert Name]";
        }

        if (field == "Expert Surname")
        {
            field = "Surname";
            prev = "e.";
            next = " as [Expert Surname]";
        }


        if (field == "Address")
        {
            field = "Address";
            prev = "e.";
            next = " as [Address]";
        }

        if (field == "Nationality")
        {
            field = "Nationality";
            prev = "e.";
            next = " as [Nationality]";
        }


        if (field == "Family")
        {
            field = "ParentId";
            prev = "e.";
            next = " as [Family]";
        }

        if (field == "Monthly")
        {
            field = "IsMonthly";
            prev = "e.";
            next = " as [Monthly]";
        }


        if (field == "Expert Passport")
        {
            field = "Passport";
            prev = "e.";
            next = " as [Expert Passport]";
        }


        if (field == "Title")
        {
            field = "Job";
            prev = "e.";
            next = " as [Title]";
        }

        //if (field == "First Entry")
        //{
        //    field = "First Entry";
        //    prev = "CONVERT(VARCHAR(9),";
        //    next = " , 6) as [First Entry]";
        //}

        //if (field == "Client authorization Date")
        //{
        //    field = "Client authorization Date";
        //    prev = "CONVERT(VARCHAR(9),";
        //    next = " , 6) as [Client authorization Date]";
        //}


        if (field == "Approval Submission")
        {
            field = "[Approval Submition Date]";
            prev = "CONVERT(VARCHAR(9),";
            next = " , 6) as [Approval Submission]";
        }


        if (field == "Approval From")
        {
            field = "[Approval From Date]";
            prev = "CONVERT(VARCHAR(9),";
            next = " , 6) as [Approval From]";
        }


        if (field == "Approval Expired")
        {
            field = "[Approval Exp Date]";
            prev = "CONVERT(VARCHAR(9),";
            next = " , 6) as [Approval Expired]";
        }

        if (field == "Invitation Date")
        {
            field = "[Visa/ Invitation Issue date]";
            prev = "CONVERT(VARCHAR(9),";
            next = " , 6) as [Invitation Date]";
        }

        if (field == "Visa Expierd")
        {
            field = "[Visa Exp Date]";
            prev = "CONVERT(VARCHAR(9),";
            next = " , 6) as [Visa Expierd]";
        }

        if (field == "Multiple Visa Issue")
        {
            field = "[Multiple Issue Visa]";
            prev = "CONVERT(VARCHAR(9),";
            next = " , 6) as [Multiple Visa Issue]";
        }
        if (field == "Multiple Visa Exp")
        {
            field = "[Multiple entry Visa]";
            prev = "CONVERT(VARCHAR(9),";
            next = " , 6) as [Multiple Visa Exp]";
        }

        if (field == "Comments")
        {
            field = "Comments";
            prev = "e.";
            next = " as [Comments]";
        }

        if (field == "Active")
        {

            if (IsQuery)
            {

                field = "Active = case when e.Active = 1 then 'Yes' else 'No' end";

            }
            //else
            //{
            //    field = "e.Active";

            //}


            // field = "Active";
            // prev = "e.";
            // next = " as CompanyName";
        }


        //************************  מסמכים ******************************
        if (field == "Diploma")
        {

            if (IsQuery)
            {
                field = "Diploma = Case when e.Diploma=1 then 'Yes' when e.Diploma=0 then 'No' when e.Diploma=2 then 'TBT' when e.Diploma=3 then 'T DONE' end";
                // prev = "e.";
                // next = " as [Diploma]";
            }
            else
            {
                field = "e.Diploma";

            }
        }

        if (field == "CV")
        {

            if (IsQuery)
            {
                field = "CV = Case when Cv=1 then 'Yes' when Cv=0 then 'No' when Cv=2 then 'TBT' when Cv=3 then 'T DONE' end ";
            }
            else
            {
                field = "e.Cv";

            }
            //field = "Cv";
            //prev = "e.";
            //next = " as [Cv]";
        }

        if (field == "ID")
        {
            if (IsQuery)
            {

                field = "ID = case when e.CopyID = 1 then 'Yes' else 'No' end";

            }
            else
            {
                field = "e.CopyID";

            }
        }


        if (field == "AL 33")
        {



            if (IsQuery)
            {

                field = "AL33 = case when e.AL33 = 1 then 'Yes' else 'No' end";

            }
            else
            {
                field = "e.AL33";

            }




            //field = "AL33";
            //prev = "e.";
            //next = " as [AL33]";
        }

        if (field == "AL 15")
        {

            if (IsQuery)
            {

                field = "AL15 = case when e.AL15 = 1 then 'Yes' else 'No' end";

            }
            else
            {
                field = "e.AL15";

            }




            //field = "AL15";
            //prev = "e.";
            //next = " as [AL15]";
        }

        if (field == "Passport Copy")
        {

            if (IsQuery)
            {

                field = "[Passport Copy] = case when e.CopyofPassport = 1 then 'Yes' else 'No' end";

            }
            else
            {
                field = "e.CopyofPassport";

            }



            //field = "CopyofPassport";
            //prev = "e.";
            //next = " as [Passport Copy]";
        }


        if (field == "Obligo 6")
        {


            if (IsQuery)
            {

                field = "[Obligo 6] = case when e.Obligo6 = 1 then 'Yes' else 'No' end";

            }
            else
            {
                field = "e.Obligo6";

            }

            //field = "Obligo6";
            //prev = "e.";
            //next = " as [Obligo 6]";
        }

        if (field == "Obligo 2")
        {
            if (IsQuery)
            {

                field = "[Obligo 2] = case when e.Obligo2 = 1 then 'Yes' else 'No' end";

            }
            else
            {
                field = "e.Obligo2";

            }



            //field = "Obligo2";
            //prev = "e.";
            //next = " as [Obligo 2]";
        }


        if (field == "Medical")
        {

            if (IsQuery)
            {

                field = "[Medical] = case when e.MedicalObligo = 1 then 'Yes' else 'No' end";

            }
            else
            {
                field = "e.MedicalObligo";

            }


            //field = "MedicalObligo";
            //prev = "e.";
            //next = " as [Medical]";
        }

        if (field == "Affivavit")
        {


            if (IsQuery)
            {

                field = "[Affidavit] = case when e.Affidavit = 1 then 'Yes' else 'No' end";

            }
            else
            {
                field = "e.Affidavit";

            }



            //field = "Affidavit";
            //prev = "e.";
            //next = " as [Affivavit]";
        }


        if (field == "Marriage Certificate")
        {

            if (IsQuery)
            {
                field = "MarriageCertificate = Case when MarriageCertificate=1 then 'Yes' when MarriageCertificate=0 then 'No' when MarriageCertificate=2 then 'TBT' when MarriageCertificate=3 then 'T DONE' when MarriageCertificate=4 then 'N/A' end";
            }
            else
            {
                field = "e.MarriageCertificate";

            }
            //field = "MarriageCertificate";
            //prev = "e.";
            //next = " as [Marriage Certificate]";
        }

        if (field == "POA")
        {

            if (IsQuery)
            {

                field = "[POA] = case when e.PowerofAttorney = 1 then 'Yes' else 'No' end";

            }
            else
            {
                field = "e.PowerofAttorney";

            }



            //field = "PowerofAttorney";
            //prev = "e.";
            //next = " as [POA]";
        }

        if (field == "Photo")
        {

            if (IsQuery)
            {

                field = "[Photo] = case when e.Photo = 1 then 'Yes' else 'No' end";

            }
            else
            {
                field = "e.Photo";

            }



            //field = "PowerofAttorney";
            //prev = "e.";
            //next = " as [POA]";
        }

        if (field == "Days To Expire")
        {

            if (IsQuery)
            {

                field = "[Days To Expire] = DateDiff(day,getDate(),[Visa Exp Date])";

            }
            else
            {
                // field = "e.Photo";

            }

        }
        //DateDiff(day,getDate(),[Visa Exp Date])

        //************************** חברות ************************

        if (field == "Company Name")
        {
            field = "Name";
            prev = "c.";
            next = " as [Company Name]";
        }

        if (field == "Company Phone")
        {
            field = "Phone";
            prev = "c.";
            next = " as [Company Phone]";
        }


        if (field == "Company Reg")
        {
            field = "Number";
            prev = "c.";
            next = " as [Company Reg]";
        }

        if (field == "Interior Reg")
        {
            field = "[Interior Reg]";
            prev = "c.";
            next = " as [Interior Reg]";
        }


        if (IsQuery)
            return prev + field + next;
        else
        {
            if (prev == "c." || prev == "e.")
                return prev + field;
            else
                return field;
        }
    }


}