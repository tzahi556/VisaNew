using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;



public partial class Pages_Test : System.Web.UI.Page
{
   
    private DataTable dtEmpty = new DataTable();
    private   DataTable dtExpert;
    private DataTable dtExpertArchive;
    private DataTable dtExpertDocs;

    public string ExpertId = "0";

    protected void Page_Load(object sender, EventArgs e)
    {

        string RoleId ="1";

        string CompanyId = "0";


    




        FillGrid();
    }

    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTheGrid.PageIndex = e.NewPageIndex;
        gvTheGrid.DataBind();
    }


    private void FillGrid()
    {

        //Hashtable hs = new Hashtable();
        //hs.Add("FirstName", txtName.Text);
        //hs.Add("LastName", txtSurname.Text);
        //hs.Add("CompanyId", ddlCompany.SelectedValue);
        //hs.Add("ExpertId", ExpertId);


        dtExpert = Dal.ExeSp("GetExpert", txtName.Text, txtSurname.Text, ddlCompany.SelectedValue, ExpertId);




        gvTheGrid.DataSource = dtExpert;
        gvTheGrid.DataBind();


        // SetStatisticPanel(dtExpert);



        //dtExpertArchive = dt.Select("[Active]=0 Or diffday < 0").Any() ? dt.Select("[Active]=0 Or diffday < 0").CopyToDataTable() : dtEmpty;
        //GridView1.DataSource = dtExpertArchive;
        //GridView1.DataBind();

        if (!Page.IsPostBack)
        {
           


        // dtExpert = dt.Select("[Active]=1 And diffday >= 0").Any() ? dt.Select("[Active]=1 And diffday >= 0").CopyToDataTable() : dtEmpty;


            gvDocs.DataSource = dtExpert;
            gvDocs.DataBind();

        }
        

    }

    private void SetStatisticPanel(DataTable dtFilter)
    {
        if (dtFilter.Rows.Count > 0)
        {
            int inProgress = dtFilter.Select(" [Approval Exp Date] IS NULL AND [Visa Exp Date] IS NULL AND [Multiple entry Visa] IS NULL ").Count();
            sp0.InnerText = inProgress.ToString();


            int Step1Count = dtFilter.Select(" [Approval Exp Date] IS NOT NULL AND [Visa Exp Date] IS NULL  AND [Multiple entry Visa] IS NULL ").Count();
            sp1.InnerText = Step1Count.ToString();


            int Step2Count = dtFilter.Select("[Approval Exp Date] IS NOT NULL AND [Visa Exp Date] IS NOT NULL AND [Multiple entry Visa] IS NULL").Count();
            sp2.InnerText = Step2Count.ToString();

            int Step3Count = dtFilter.Select("[Approval Exp Date] IS NOT NULL AND [Visa Exp Date] IS NOT NULL AND [Multiple entry Visa] IS NOT NULL").Count();
            sp3.InnerText = Step3Count.ToString();

        }
        else
        {
            sp1.InnerText = "0";
            sp2.InnerText = "0";
            sp3.InnerText = "0";

        }



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
        DataView dataView = new DataView(dtExpert);
        dataView.Sort = e.SortExpression + " " + ViewState["SortDir"];

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


        //DataTable datatable = DataTable();
        //datatable = datatable.Select("[Active]=0 Or diffday < 0").Any() ? datatable.Select("[Active]=0 Or diffday < 0").CopyToDataTable() : dtEmpty;
        DataView dataView = new DataView(dtExpertArchive);
        dataView.Sort = e.SortExpression + " " + ViewState["SortDir"];

        GridView1.DataSource = dataView;
        GridView1.DataBind();
        GridView1.HeaderRow.Cells[GetColumnIndex(e.SortExpression, gvTheGrid)].CssClass = cssSort;

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
        CheckBox chEmployerObligo = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chEmployerObligo");
        CheckBox chAffidavit = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chAffidavit");

        DropDownList ddlMarriageCertificate = (DropDownList)gvDocs.Rows[e.RowIndex].FindControl("ddlMarriageCertificate");

        CheckBox chPhoto = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chPhoto");
        CheckBox chPowerofAttorney = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chPowerofAttorney");


        Dal.ExeSp("SetExpertDocs", ExpertId,chCopy.Checked,ddlDiploma.SelectedValue,ddlCv.SelectedValue,
            chId.Checked,ch33.Checked,ch15.Checked,chEmployerObligo.Checked,chAffidavit.Checked,ddlMarriageCertificate.SelectedValue,
            chPhoto.Checked,chPowerofAttorney.Checked
           );

       


        gvDocs.EditIndex = -1;

        FillGrid();
        gvDocs.DataSource = dtExpert;
        gvDocs.DataBind();
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

        gvDocs.DataSource = dtExpert;
        gvDocs.DataBind();
        //FillGrid();

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
        gvDocs.DataSource = dtExpert;
        gvDocs.DataBind();
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

    protected void gvTheGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //gvTheGrid.PageIndex = e.NewPageIndex;
        //gvTheGrid.DataSource = DataTable();
        //gvTheGrid.DataBind();
    }




    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ExpertId = GridView1.DataKeys[e.Row.RowIndex].Values[1].ToString();
            e.Row.Attributes.Add("ondblclick", "OpenCustomerDetails('" + ExpertId + "');");
          
            Image flagImg = (Image)e.Row.FindControl("imgStatus");

            string diffday = GridView1.DataKeys[e.Row.RowIndex].Values[0].ToString();
            if (!string.IsNullOrEmpty(diffday))
            {
                int diff = int.Parse(diffday);



                if (diff >= 160 && diff != 5000)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/green_flag.png";
                    flagImg.ToolTip = diffday;
                }
                else if (diff < 160 && diff >= 75)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/orange_flag.png";
                    flagImg.ToolTip = diffday;

                }
                else if (diff < 75 && diff >= 0)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/red_flag.png";
                    flagImg.ToolTip = diffday;

                }

                else if (diff != 5000)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/black_flag.png";
                    flagImg.ToolTip = diffday;
                }
            }
            else
            {
                flagImg.Width = 3;
                flagImg.Height = 3;
            }



            // GridViewRow row = e.Row.;

        }
    }


    protected void gvTheGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            string ExpertId = gvTheGrid.DataKeys[e.Row.RowIndex].Values[1].ToString();
            
            e.Row.Attributes.Add("ondblclick", "OpenCustomerDetails('" + ExpertId + "');");
            Image flagImg = (Image)e.Row.FindControl("imgStatus");

            string diffday = gvTheGrid.DataKeys[e.Row.RowIndex].Values[0].ToString();
            if (!string.IsNullOrEmpty(diffday))
            {
                int diff = int.Parse(diffday);



                if (diff >= 160 && diff != 5000)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/green_flag.png";
                    flagImg.ToolTip = diffday;
                }
                else if (diff < 160 && diff >= 75)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/orange_flag.png";
                    flagImg.ToolTip = diffday;

                }
                else if (diff < 75 && diff >= 0)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/red_flag.png";
                    flagImg.ToolTip = diffday;

                }

                else if (diff != 5000)
                {
                    flagImg.ImageUrl = "~/App_Themes/Theme1/Images/black_flag.png";
                    flagImg.ToolTip = diffday;
                }
            }
            else
            {
                flagImg.Width = 3;
                flagImg.Height = 3;
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
        FillGrid();
        gvDocs.EditIndex = -1;
        gvDocs.DataSource = dtExpert;
        gvDocs.DataBind();
       
    }
    protected void ddlCompany_SelectedChange(object sender, EventArgs e)
    {
        FillGrid();
        gvDocs.EditIndex = -1;
        gvDocs.DataSource = dtExpert;
        gvDocs.DataBind();
       
    }

    protected void txt_TextChanged(object sender, EventArgs e)
    {
        FillGrid();
        gvDocs.EditIndex = -1;
        gvDocs.DataSource = dtExpert;
        gvDocs.DataBind();
       
    }
}