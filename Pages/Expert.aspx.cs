using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Xml;
using System.Text;




public partial class Pages_Expert : System.Web.UI.Page
{

    private DataTable dtEmpty = new DataTable();
    private DataTable dtExpert;
    private DataTable dtExpertArchive;
    private DataTable dtExpertDocs;

    public string ExpertId = "0";
    public string RoleId = "0";
    public string CompanyId = "0";
    public HttpCookie cookie = null;

    protected void Page_Load(object sender, EventArgs e)
    {



        if (HttpContext.Current.Request.Cookies["UserData"] != null)
        {
            cookie = HttpContext.Current.Request.Cookies["UserData"];
        }


        RoleId = cookie["RoleId"].ToString();

        CompanyId = cookie["CompanyId"].ToString();

        if (RoleId != "1")
        {

            btnNew.Visible = false;
            btnDelete.Visible = false;
            btnSelected.Visible = false;
            btnDuplicate.Visible = false;
            ddlSteps.Visible = false;
            btnExcel.Visible = false;
        }



        if (RoleId == "3")
        {
            string ExpertIdForSe = cookie["ExpertId"].ToString();
            if (!string.IsNullOrEmpty(ExpertIdForSe))
            {
                ExpertId = ExpertIdForSe;
                btnSearch.Visible = false;
                txtSurname.Enabled = false;
                txtName.Enabled = false;

            }


            ddlCompany.Disabled = true;
            // ddlCompany.Enabled = false;
        }


        if (RoleId == "1")
        {
            CompanyId = "0";
        }



        // You only need the following 2 lines of code if you are not 
        // using an ObjectDataSource of SqlDataSource
        if (!Page.IsPostBack)
        {

            //ddlCompany.DataSource = Dal.ExeSp("GetCompany", "", "", CompanyId, "0");
            //ddlCompany.DataTextField = "Name";
            //ddlCompany.DataValueField = "CompanyId";
            //ddlCompany.DataBind();

            //if (RoleId == "1")
            //{
            //    ddlCompany.SelectedIndex = 1;
            //}
            //else
            //{
            //    ddlCompany.SelectedValue = CompanyId;
            //}





            DataTable dtCompany = GetOptionsForCompanyDDL();//Dal.ExeSp("GetCompanyForMain", CompanyId);

            ddlCompany.DataSource = dtCompany;
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "CompanyId";

            ddlCompany.DataBind();

            if (RoleId == "1")
            {
                //ddlCompany.SelectedIndex = 2;


            }
            else
            {
                ddlCompany.SelectedIndex = 1;
            }




            //  string Options = GetOptionsForCompanyDDL();

            FillGrid();

        }
        //else
        //{
        //    string Name = Request.Form["mylist"].ToString();

        //}

        if (Request.Form["__EVENTTARGET"] == "txtSurname")
        {



            FillGrid();
            // txtSurname.Focus();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "FocusScript", "SetFocusOnPostBack(1);", true);

            //Call the function here which you want

        }

        if (Request.Form["__EVENTTARGET"] == "txtName")
        {
            FillGrid();
            // txtSurname.Focus();
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "FocusScript", "SetFocusOnPostBack(2);", true);
            //Call the function here which you want

        }

        //if (Request.Form["__EVENTTARGET"] == "ddlCompany")
        //{

        //}


        ScriptManager.RegisterStartupScript(Page, this.GetType(), "DatePickerScript", "UpdateSelectedOnPostBack();", true);

    }

    private DataTable GetOptionsForCompanyDDL()
    {


        DataTable dtRes = new DataTable();

        dtRes.Columns.Add("CompanyId");
        dtRes.Columns.Add("Name");
        //  dtRes.Columns.Add("Order");

        DataTable dtCompany = Dal.ExeSp("GetCompanyForMain", CompanyId);
        DataRow[] dtRows = dtCompany.Select("Parent_Id=0 Or Parent_Id Is Null", "Name ASC");
        if (dtCompany.Rows.Count == 1)
        {
            dtRows = dtCompany.Select("CompanyId=" + CompanyId);


        }



        dtRes.Rows.Add("", "Select...");

        if (RoleId == "1") dtRes.Rows.Add("Single_0", "--All Companies--");

        foreach (DataRow row in dtRows)
        {
            string Name = row["Name"].ToString();
            string rowCompanyId = row["CompanyId"].ToString();


            DataRow[] dtRowsChilds = dtCompany.Select("Parent_Id=" + rowCompanyId);
            if (dtRowsChilds.Count() > 0)
            {
                dtRes.Rows.Add("Group_" + rowCompanyId, Name + " (Group)");
                dtRes.Rows.Add(rowCompanyId, Name);

                // res += "<option value=Group_" + rowCompanyId + ">" + Name + " (Group)</option>";

                foreach (DataRow rowChild in dtRowsChilds)
                {
                    // res += "<option value=" + rowChild["CompanyId"].ToString() + ">" + rowChild["Name"].ToString() + "</option>";


                    dtRes.Rows.Add(rowChild["CompanyId"].ToString(), rowChild["Name"].ToString());
                }

            }
            else
            {
                dtRes.Rows.Add("Single_" + rowCompanyId, Name);
            }

        }


        //for (int i = 0; i < dtRes.Rows.Count; i++)
        //{
        //    dtRes.Rows[i]["Order"] = i.ToString();
        //}


        return dtRes;
    }


    protected void ddlFamily_SelectedChange(object sender, EventArgs e)
    {
        // gvDocs.EditIndex = -1;
        FillGrid();

    }

    private void FillGrid()
    {

        //Hashtable hs = new Hashtable();
        //hs.Add("FirstName", txtName.Text);
        //hs.Add("LastName", txtSurname.Text);
        //hs.Add("CompanyId", ddlCompany.SelectedValue);
        //hs.Add("ExpertId", ExpertId);

        string SessionCompanyId = cookie["CompanyId"].ToString();
        if (RoleId == "1") SessionCompanyId = "0";


        string SelectedCompanyId = ddlCompany.Value.Replace("Single_", "");

        DataTable dt = Dal.ExeSp("GetExpert", txtName.Text, txtSurname.Text, SelectedCompanyId, ExpertId, SessionCompanyId, ddlFamily.SelectedValue);

        //if (CompanyId == "0" && RoleId == "2")
        //{

        //}




        dtExpert = dt.Select(" [Active]=1 And diffday >= 0").Any() ? dt.Select("[Active]=1 And diffday >= 0", "Surname,Name").CopyToDataTable() : dtEmpty;

        gvTheGrid.DataSource = dtExpert;
        gvTheGrid.DataBind();

        if (gvTheGrid.HeaderRow != null)
            gvTheGrid.HeaderRow.TableSection = TableRowSection.TableHeader;

        // SetStatisticPanel(dtExpert);



        dtExpertArchive = dt.Select("[Active]=0 Or diffday < 0").Any() ? dt.Select("[Active]=0 Or diffday < 0", "Surname,Name").CopyToDataTable() : dtEmpty;
        GridView1.DataSource = dtExpertArchive;
        GridView1.DataBind();




        if (GridView1.HeaderRow != null)
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;




        DataView dataView = new DataView(dtExpert);
        if (!string.IsNullOrEmpty((string)ViewState["FieldSort"]))
        {
            dataView.Sort = ViewState["FieldSort"] + " " + ViewState["SortDir"];
        }
        gvDocs.DataSource = dataView;
        gvDocs.DataBind();

        if (gvDocs.HeaderRow != null)
            gvDocs.HeaderRow.TableSection = TableRowSection.TableHeader;

        // }


    }


    protected void gvTheGrid_Sorting(object sender, GridViewSortEventArgs e)
    {

        string cssSort = "gridSortDown";
        if ((string)ViewState["SortDir"] == "asc")
        {
            e.SortDirection = SortDirection.Descending;
            // sort e.SortExpression ascending
            ViewState["SortDir"] = "desc";


        }
        else
        {
            e.SortDirection = SortDirection.Ascending;
            // sort e.SortExpression descending
            ViewState["SortDir"] = "asc";
            cssSort = "gridSortUp";
        }


        //DataTable datatable = DataTable();
        //datatable = datatable.Select("[Active]=1 And diffday >= 0").Any() ? datatable.Select("[Active]=1 And diffday >= 0").CopyToDataTable() : dtEmpty;

        FillGrid();

        DataView dataView = new DataView(dtExpert);
        dataView.Sort = e.SortExpression + " " + ViewState["SortDir"];

        gvTheGrid.DataSource = dataView;
        gvTheGrid.DataBind();


        gvTheGrid.HeaderRow.Cells[GetColumnIndex(e.SortExpression, gvTheGrid)].CssClass = cssSort;

    }

    protected void gvDocs_Sorting(object sender, GridViewSortEventArgs e)
    {


        gvDocs.EditIndex = -1;


        string cssSort = "gridSortDown";
        if ((string)ViewState["SortDir"] == "asc")
        {
            e.SortDirection = SortDirection.Descending;
            // sort e.SortExpression ascending
            ViewState["SortDir"] = "desc";


        }
        else
        {
            e.SortDirection = SortDirection.Ascending;
            // sort e.SortExpression descending
            ViewState["SortDir"] = "asc";
            cssSort = "gridSortUp";
        }


        //DataTable datatable = DataTable();
        //datatable = datatable.Select("[Active]=1 And diffday >= 0").Any() ? datatable.Select("[Active]=1 And diffday >= 0").CopyToDataTable() : dtEmpty;

        FillGrid();

        DataView dataView = new DataView(dtExpert);
        dataView.Sort = e.SortExpression + " " + ViewState["SortDir"];

        ViewState["FieldSort"] = e.SortExpression;

        gvDocs.DataSource = dataView;
        gvDocs.DataBind();


        gvDocs.HeaderRow.Cells[GetColumnIndex(e.SortExpression, gvDocs)].CssClass = cssSort;


    }


    private int GetColumnIndex(string SortExpression, GridView gv)
    {
        int i = 0;
        foreach (DataControlField c in gv.Columns)
        {
            if (c.SortExpression == SortExpression)
                break;
            i++;
        }
        return i;
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string cssSort = "gridSortDown";
        if ((string)ViewState["SortDir"] == "asc")
        {
            e.SortDirection = SortDirection.Descending;
            // sort e.SortExpression ascending
            ViewState["SortDir"] = "desc";
        }
        else
        {
            e.SortDirection = SortDirection.Ascending;
            // sort e.SortExpression descending
            ViewState["SortDir"] = "asc";
            cssSort = "gridSortUp";
        }

        FillGrid();

        //DataTable datatable = DataTable();
        //datatable = datatable.Select("[Active]=0 Or diffday < 0").Any() ? datatable.Select("[Active]=0 Or diffday < 0").CopyToDataTable() : dtEmpty;
        DataView dataView = new DataView(dtExpertArchive);
        dataView.Sort = e.SortExpression + " " + ViewState["SortDir"];

        GridView1.DataSource = dataView;
        GridView1.DataBind();
        GridView1.HeaderRow.Cells[GetColumnIndex(e.SortExpression, gvTheGrid)].CssClass = cssSort;

    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ExpertId = GridView1.DataKeys[e.RowIndex].Values[1].ToString();


        string sql = "delete from Expert where ExpertId = " + ExpertId;

        int res = Dal.ExecuteNonQuery(sql);

        FillGrid();



    }

    

    protected void txtChange(object sender, EventArgs e)
    {
        FillGrid();
        //  SendEmail sm = new SendEmail();
    }
    protected void SendEmailDemo(object sender, EventArgs e)
    {

        //  SendEmail sm = new SendEmail();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        //  CheckBox chkUpdate = (CheckBox)gvDocs.Rows[1].Cells[2].FindControl("chCopy");


        //for (int i = 0; i < gvDocs.Rows.Count; i++)
        //    gvDocs.UpdateRow(i, true);

        //gvDocs.DataBind();

    }

    protected void gvDocs_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {


        string ExpertId = gvDocs.DataKeys[e.RowIndex].Values[0].ToString();



        CheckBox chCopy = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chCopy");
        DropDownList ddlDiploma = (DropDownList)gvDocs.Rows[e.RowIndex].FindControl("ddlDiploma");
        DropDownList ddlCv = (DropDownList)gvDocs.Rows[e.RowIndex].FindControl("ddlCv");
        CheckBox chId = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chId");
        CheckBox ch33 = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("ch33");
        CheckBox ch15 = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("ch15");
        CheckBox chObligo6 = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chObligo6");

        CheckBox chObligo2 = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chObligo2");
        CheckBox chMedicalObligo = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chMedicalObligo");


        CheckBox chAffidavit = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chAffidavit");

        DropDownList ddlMarriageCertificate = (DropDownList)gvDocs.Rows[e.RowIndex].FindControl("ddlMarriageCertificate");

        CheckBox chPhoto = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chPhoto");
        CheckBox chPowerofAttorney = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chPowerofAttorney");


        Dal.ExeSp("SetExpertDocs", ExpertId, chCopy.Checked, ddlDiploma.SelectedValue, ddlCv.SelectedValue,
            chId.Checked, ch33.Checked, ch15.Checked, chObligo6.Checked, chObligo2.Checked, chMedicalObligo.Checked, chAffidavit.Checked, ddlMarriageCertificate.SelectedValue,
            chPhoto.Checked, chPowerofAttorney.Checked
           );




        gvDocs.EditIndex = -1;

        FillGrid();

    }

    protected void gvDocs_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Edit")
        {

            // string sd = "s";

        }
    }

    protected void gvDocs_RowEditing(object sender, GridViewEditEventArgs e)
    {

        ////Set the edit index.
        gvDocs.EditIndex = e.NewEditIndex;
        ////Bind data to the GridView control.


        FillGrid();


        string Cv = gvDocs.DataKeys[e.NewEditIndex].Values[1].ToString();
        DropDownList ddlCv = (DropDownList)gvDocs.Rows[e.NewEditIndex].FindControl("ddlCv");
        ddlCv.SelectedValue = Cv;



        string Diploma = gvDocs.DataKeys[e.NewEditIndex].Values[2].ToString();
        DropDownList ddlDiploma = (DropDownList)gvDocs.Rows[e.NewEditIndex].FindControl("ddlDiploma");
        ddlDiploma.SelectedValue = Diploma;

        string MarriageCertificate = gvDocs.DataKeys[e.NewEditIndex].Values[3].ToString();
        DropDownList ddlMarriageCertificate = (DropDownList)gvDocs.Rows[e.NewEditIndex].FindControl("ddlMarriageCertificate");
        ddlMarriageCertificate.SelectedValue = MarriageCertificate;




        //ddlRoles.DataSource = Dal.GetDataTableFromSPNoParameter("GetRole");
        //ddlRoles.DataTextField = "Name";
        //ddlRoles.DataValueField = "RoleId";
        //ddlRoles.DataBind();

        //string RoleId = GridView1.DataKeys[e.NewEditIndex].Values[2].ToString();

        //ddlRoles.SelectedValue = RoleId;

        //DropDownList ddlCompany = (DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddlCompany");

        //ddlCompany.DataSource = Dal.GetDataTableFromSPNoParameter("GetCompanies");
        //ddlCompany.DataTextField = "Name";
        //ddlCompany.DataValueField = "CompanyId";
        //ddlCompany.DataBind();

        //string CompanyId = GridView1.DataKeys[e.NewEditIndex].Values[1].ToString();

        //ddlCompany.SelectedValue = CompanyId;


    }


    protected void gvDocs_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvDocs.EditIndex = -1;
        FillGrid();
    }





    //private DataTable DataTable()
    //{
    //    //Create a DataTable instance
    //    DataTable table = new DataTable();


    //    Hashtable hs = new Hashtable();
    //    hs.Add("FirstName",txtName.Text);
    //    hs.Add("LastName", txtSurname.Text);
    //    hs.Add("CompanyId", ddlCompany.SelectedValue);
    //    hs.Add("ExpertId", ExpertId);


    //    table = Dal.GetDataTableFromSPMultiParameters("GetExpert", hs);

    //    return table;
    //}

    //protected void gvTheGrid_PageIndexChanging(Object sender, EventArgs e)
    //{
    //    gvTheGrid.SelectedRowStyle.BackColor = System.Drawing.Color.Red;
    //    //gvTheGrid.PageIndex = e.NewPageIndex;
    //    //gvTheGrid.DataSource = DataTable();
    //    //gvTheGrid.DataBind();
    //}

    protected void gvDocs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            if (RoleId != "1")
            {
                gvDocs.Columns[0].Visible = false;
            }



            string VIIDate = gvTheGrid.Rows[e.Row.RowIndex].Cells[8].Text;

            if (VIIDate != "&nbsp;")
            {
                e.Row.Cells[1].Attributes.Add("style", "font-weight:bolder");
                e.Row.Cells[2].Attributes.Add("style", "font-weight:bolder");
            }



            System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml("#EECCCC");
            bool AL33 = bool.Parse(DataBinder.Eval(e.Row.DataItem, "AL33").ToString());
            if (!AL33)
            {
                e.Row.Cells[3].BackColor = color;
                e.Row.Cells[3].CssClass = "neg";
            }

            bool AL15 = bool.Parse(DataBinder.Eval(e.Row.DataItem, "AL15").ToString());
            if (!AL15)
            {
                e.Row.Cells[4].BackColor = color;
                e.Row.Cells[4].CssClass = "neg";
            }

            bool Photo = bool.Parse(DataBinder.Eval(e.Row.DataItem, "Photo").ToString());
            if (!Photo)
            {
                e.Row.Cells[5].BackColor = color;
                e.Row.Cells[5].CssClass = "neg";
            }


            bool CopyofPassport = bool.Parse(DataBinder.Eval(e.Row.DataItem, "CopyofPassport").ToString());
            if (!CopyofPassport)
            {
                e.Row.Cells[6].BackColor = color;
                e.Row.Cells[6].CssClass = "neg";
            }



            bool CopyID = bool.Parse(DataBinder.Eval(e.Row.DataItem, "CopyID").ToString());
            if (!CopyID)
            {
                e.Row.Cells[7].BackColor = color;
                e.Row.Cells[7].CssClass = "neg";
            }

            string CvT = DataBinder.Eval(e.Row.DataItem, "CvT").ToString();
            if (CvT == "No")
            {
                e.Row.Cells[8].BackColor = color;
                e.Row.Cells[8].CssClass = "neg";
            }

            string DiplomaT = DataBinder.Eval(e.Row.DataItem, "DiplomaT").ToString();
            if (DiplomaT == "No")
            {
                e.Row.Cells[9].BackColor = color;
                e.Row.Cells[9].CssClass = "neg";
            }












            bool Obligo6 = bool.Parse(DataBinder.Eval(e.Row.DataItem, "Obligo6").ToString());
            if (!Obligo6)
            {
                e.Row.Cells[10].BackColor = color;
                e.Row.Cells[10].CssClass = "neg";
            }

            bool Obligo2 = bool.Parse(DataBinder.Eval(e.Row.DataItem, "Obligo2").ToString());
            if (!Obligo2)
            {
                e.Row.Cells[11].BackColor = color;
                e.Row.Cells[11].CssClass = "neg";
            }
            bool MedicalObligo = bool.Parse(DataBinder.Eval(e.Row.DataItem, "MedicalObligo").ToString());
            if (!MedicalObligo)
            {
                e.Row.Cells[12].BackColor = color;
                e.Row.Cells[12].CssClass = "neg";
            }



            bool Affidavit = bool.Parse(DataBinder.Eval(e.Row.DataItem, "Affidavit").ToString());
            if (!Affidavit)
            {
                e.Row.Cells[13].BackColor = color;
                e.Row.Cells[13].CssClass = "neg";
            }


            string MarriageCertificateT = DataBinder.Eval(e.Row.DataItem, "MarriageCertificateT").ToString();

            if (MarriageCertificateT == "No")
            {
                e.Row.Cells[14].BackColor = color;
                e.Row.Cells[14].CssClass = "neg";
            }



            bool PowerofAttorney = bool.Parse(DataBinder.Eval(e.Row.DataItem, "PowerofAttorney").ToString());
            if (!PowerofAttorney)
            {
                e.Row.Cells[15].BackColor = color;
                e.Row.Cells[15].CssClass = "neg";
            }




            // e.Row.Cells[0].
        }

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if (RoleId != "1")
            {
                GridView1.Columns[9].Visible = false;
            }

            string diffday = GridView1.DataKeys[e.Row.RowIndex].Values[0].ToString();
            string ExpertId = GridView1.DataKeys[e.Row.RowIndex].Values[1].ToString();




            string Surname = DataBinder.Eval(e.Row.DataItem, "Surname").ToString();
            string Name = DataBinder.Eval(e.Row.DataItem, "Name").ToString();
            e.Row.Attributes.Add("ondblclick", "OpenCustomerDetails('" + ExpertId + "', '" + Surname + "&nbsp;" + Name + "');");

            Image flagImg = (Image)e.Row.FindControl("imgStatus");


            if (!string.IsNullOrEmpty(diffday))
            {
                int diff = int.Parse(diffday);

                if (diff >= 120 && diff != 5000)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/green_flag.png";
                    flagImg.ToolTip = diffday;
                    flagImg.Width = 16;
                    flagImg.Height = 16;
                }
                else if (diff < 120 && diff >= 90)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/orange_flag.png";
                    flagImg.ToolTip = diffday;
                    flagImg.Width = 16;
                    flagImg.Height = 16;
                }
                else if (diff < 90 && diff >= 0)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/red_flag.png";
                    flagImg.ToolTip = diffday;
                    flagImg.Width = 16;
                    flagImg.Height = 16;
                }

                else if (diff != 5000)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/black_flag.png";
                    flagImg.ToolTip = diffday;
                    flagImg.Width = 16;
                    flagImg.Height = 16;
                }
            }
            else
            {
                flagImg.Width = 3;
                flagImg.Height = 3;
            }

            string IsMonthly = GridView1.DataKeys[e.Row.RowIndex].Values[2].ToString();
            if (IsMonthly == "True")
            {

                flagImg.ImageUrl = "~/App_Themes/Theme1/Images/Fav.png";
                flagImg.ToolTip = "Monthly";
                flagImg.Width = 16;
                flagImg.Height = 16;
            }


            // GridViewRow row = e.Row.;

        }
    }


    protected void gvTheGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string diffday = gvTheGrid.DataKeys[e.Row.RowIndex].Values[0].ToString();
            string ExpertId = gvTheGrid.DataKeys[e.Row.RowIndex].Values[1].ToString();
            string Surname = DataBinder.Eval(e.Row.DataItem, "Surname").ToString();
            string Name = DataBinder.Eval(e.Row.DataItem, "Name").ToString();
            string AEDate = DataBinder.Eval(e.Row.DataItem, "AEDate").ToString();
            string VEDate = DataBinder.Eval(e.Row.DataItem, "VEDate").ToString();
            string isPassportShort = DataBinder.Eval(e.Row.DataItem, "isPassportShort").ToString();
            string Is90 = DataBinder.Eval(e.Row.DataItem, "Is90").ToString();

            if (AEDate != VEDate)
            {
                e.Row.Cells[9].Attributes.Add("style", "font-weight:bolder");
            }


            if (isPassportShort == "1")
            {
                e.Row.Cells[4].Attributes.Add("style", "color:red;font-weight:bolder");
            }

            e.Row.Attributes.Add("ondblclick", "OpenCustomerDetails('" + ExpertId + "', '" + Surname + "&nbsp;" + Name + "');");

            Image flagImg = (Image)e.Row.FindControl("imgStatus");


            string IsMonthly = gvTheGrid.DataKeys[e.Row.RowIndex].Values[2].ToString();
            string ParentId = gvTheGrid.DataKeys[e.Row.RowIndex].Values[3].ToString();
            string IsParentOfAny = gvTheGrid.DataKeys[e.Row.RowIndex].Values[4].ToString();

            if (ParentId == "0") ParentId = "";

            string Nextf = "";
            if (!string.IsNullOrEmpty(ParentId)) Nextf = "f2";
            if (!string.IsNullOrEmpty(IsParentOfAny)) Nextf = "F1";

            // 

            if (!string.IsNullOrEmpty(diffday))
            {
                int diff = int.Parse(diffday);


                if (diff >= 120 && diff != 5000)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/green_flag" + Nextf + ".png";
                    flagImg.ToolTip = diffday;
                    flagImg.Width = 16;
                    flagImg.Height = 16;
                }
                else if (diff < 120 && diff >= 90)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/orange_flag" + Nextf + ".png";
                    flagImg.ToolTip = diffday;
                    flagImg.Width = 16;
                    flagImg.Height = 16;
                }
                else if (diff < 90 && diff >= 0)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/red_flag" + Nextf + ".png";
                    flagImg.ToolTip = diffday;
                    flagImg.Width = 16;
                    flagImg.Height = 16;
                }

                else if (diff != 5000)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/black_flag.png";
                    flagImg.ToolTip = diffday;
                    flagImg.Width = 16;
                    flagImg.Height = 16;
                }


                else if (diff == 5000 && !string.IsNullOrEmpty(Nextf))
                {


                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/" + Nextf + ".png";
                    flagImg.ToolTip = diffday;
                    flagImg.Width = 16;
                    flagImg.Height = 16;
                }


            }
            else
            {
                //if (!string.IsNullOrEmpty(Nextf))
                //{

                //    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/" + Nextf + ".png";
                //    flagImg.ToolTip = "Family";
                //    flagImg.Width = 16;
                //    flagImg.Height = 16;
                //}
                //else
                //{
                flagImg.Width = 3;
                flagImg.Height = 3;
                // }
            }

            if (IsMonthly == "True")
            {

                flagImg.ImageUrl = "~/App_Themes/Theme1/Images/Fav.png";
                flagImg.ToolTip = "Monthly";
                flagImg.Width = 16;
                flagImg.Height = 16;
            }

            if (Is90 == "True")
            {

                flagImg.ImageUrl = "~/App_Themes/Theme1/Images/Bordo.png";
                flagImg.ToolTip = "90 Days";
                flagImg.Width = 16;
                flagImg.Height = 16;
            }



            // GridViewRow row = e.Row.;

        }
    }



    //public string GetDate(string p)
    //{
    //    return p;
    //}
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        gvDocs.EditIndex = -1;
        FillGrid();

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string ExpertId = hdnSelectedArchive.Value;

        if (!string.IsNullOrEmpty(ExpertId) && ExpertId!="0")
        {

            string sql = "delete from Expert where ExpertId = " + ExpertId;
            int res = Dal.ExecuteNonQuery(sql);
            FillGrid();

           

        }



        string selected = hdnSelected.Value;
        if (!string.IsNullOrEmpty(selected) && selected!="0")
        {

            Dal.ExeSp("DeleteMultipleExperts", selected);
            FillGrid();
        }
    }


    protected void btnDuplicate_Click(object sender, EventArgs e)
    {

        string selected = hdnSelected.Value;
        Dal.ExeSp("DuplicateExperts", selected);
        FillGrid();
    }




    protected void ddlCompany_SelectedChange(object sender, EventArgs e)
    {
        gvDocs.EditIndex = -1;
        FillGrid();

    }

    protected void txt_TextChanged(object sender, EventArgs e)
    {
        gvDocs.EditIndex = -1;
        FillGrid();

    }

    public void btnExcel_Click(object sender, EventArgs e)
    {

        // gvTheGrid.Columns[0].Visible = false;

        //PrepareGridViewForExport(gvTheGrid);
        PrepareGridViewForExport(gvDocs);
        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Report.xls");

        Response.Charset = "";

        // If you want the option to open the Excel file without saving than

        // comment out the line below

        // Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/Report.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);


        gvDocs.Columns[0].Visible = false;
        gvDocs.RenderControl(htmlWrite);

        string res = stringWrite.ToString().Replace("background", "");

        Response.Write(res);

        Response.End();
    }


    private void PrepareGridViewForExport(Control gv)
    {
        Literal l = new Literal();
        string name = String.Empty;
        for (int i = 0; i < gv.Controls.Count; i++)
        {
            if (gv.Controls[i].GetType() == typeof(CheckBox))
            {


                CheckBox ch = (CheckBox)gv.Controls[i];

                gv.Controls.Remove(gv.Controls[i]);

                Label lbl = new Label();
                if (ch.Checked)
                {
                    lbl.Text = "Yes";
                }
                else
                {
                    lbl.Text = "No";
                }
                gv.Controls.Add(lbl);

                //gv.Controls.AddAt(i, l);
            }
            if (gv.Controls[i].HasControls())
            {
                PrepareGridViewForExport(gv.Controls[i]);
            }
        }
    }



    public override void VerifyRenderingInServerForm(Control control)
    {

        // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.

    }




}