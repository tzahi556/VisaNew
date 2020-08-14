using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;



public partial class Pages_Company : System.Web.UI.Page
{

    private DataTable dtEmpty = new DataTable();
    private DataTable dtCompany;
    public string CompanyId = "0";
    public HttpCookie cookie = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.Cookies["UserData"] != null)
        {
            cookie = HttpContext.Current.Request.Cookies["UserData"];
        }



        string RoleId = cookie["RoleId"].ToString();

        string CompanyId = cookie["CompanyId"].ToString();


        if (RoleId != "1")
        {
            if (!string.IsNullOrEmpty(CompanyId))
            {

                btnNew.Visible = false;
            }
        }

        if (RoleId == "3")
        {
            string ExpertIdForSe = Session["ExpertId"].ToString();
            if (!string.IsNullOrEmpty(ExpertIdForSe))
            {

                btnSearch.Visible = false;

            }
        }


        ScriptManager.RegisterStartupScript(Page, this.GetType(), "DatePickerScript", "UpdateSelectedOnPostBack();", true);


        // You only need the following 2 lines of code if you are not 
        // using an ObjectDataSource of SqlDataSource
        if (!Page.IsPostBack)
        {
            FillGrid();

        }
        else
        {

        }



    }

    private void FillGrid()
    {

        dtCompany = Dal.ExeSp("GetCompany", txtName.Text, txtNumber.Text, "0", CompanyId);

        gvTheGrid.DataSource = dtCompany;
        gvTheGrid.DataBind();

		if(gvTheGrid.HeaderRow != null)
			gvTheGrid.HeaderRow.TableSection = TableRowSection.TableHeader;


        DataView dataView = new DataView(dtCompany);
        if (!string.IsNullOrEmpty((string)ViewState["FieldSort"]))
        {
            dataView.Sort = ViewState["FieldSort"] + " " + ViewState["SortDir"];
        }
        gvDocs.DataSource = dataView;
        gvDocs.DataBind();

		if(gvDocs.HeaderRow != null)
			gvDocs.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void gvTheGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string CompanyId = gvTheGrid.DataKeys[e.Row.RowIndex].Values[0].ToString();
            string Name = DataBinder.Eval(e.Row.DataItem, "Name").ToString();
            
            e.Row.Attributes.Add("ondblclick", "OpenCompanyDetails('" + CompanyId + "', '" + Name + "');");
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

        FillGrid();

        //DataTable datatable = DataTable();
        //datatable = datatable.Select("[Active]=1 And diffday >= 0").Any() ? datatable.Select("[Active]=1 And diffday >= 0").CopyToDataTable() : dtEmpty;
        DataView dataView = new DataView(dtCompany);
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

        FillGrid();
        //DataTable datatable = DataTable();
        //datatable = datatable.Select("[Active]=1 And diffday >= 0").Any() ? datatable.Select("[Active]=1 And diffday >= 0").CopyToDataTable() : dtEmpty;
        DataView dataView = new DataView(dtCompany);
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



    protected void btnSave_Click(object sender, EventArgs e)
    {



    }

    protected void gvDocs_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {


        string CompanyId = gvDocs.DataKeys[e.RowIndex].Values[0].ToString();



        CheckBox chAffidavit = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chAffidavit");
       // CheckBox chApplicationForm = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chApplicationForm");
        DropDownList ddlAttorney = (DropDownList)gvDocs.Rows[e.RowIndex].FindControl("ddlAttorney");
        DropDownList ddlcertificate = (DropDownList)gvDocs.Rows[e.RowIndex].FindControl("ddlcertificate");
        CheckBox chObligo6 = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chObligo6");
        CheckBox chObligo2 = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chObligo2");
        CheckBox chMedicalObligo = (CheckBox)gvDocs.Rows[e.RowIndex].FindControl("chMedicalObligo");




        Dal.ExeSp("SetCompanyDocs", CompanyId, chAffidavit.Checked, true,
            ddlAttorney.SelectedValue, ddlcertificate.SelectedValue,
           chObligo6.Checked,chObligo2.Checked, chMedicalObligo.Checked

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
        //FillGrid();





        string PowerofAttorney = gvDocs.DataKeys[e.NewEditIndex].Values[1].ToString();
        DropDownList ddlAttorney = (DropDownList)gvDocs.Rows[e.NewEditIndex].FindControl("ddlAttorney");
        ddlAttorney.SelectedValue = PowerofAttorney;

        string certificate = gvDocs.DataKeys[e.NewEditIndex].Values[2].ToString();
        DropDownList ddlcertificate = (DropDownList)gvDocs.Rows[e.NewEditIndex].FindControl("ddlcertificate");
        ddlcertificate.SelectedValue = certificate;




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

    protected void gvTheGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //gvTheGrid.PageIndex = e.NewPageIndex;
        //gvTheGrid.DataSource = DataTable();
        //gvTheGrid.DataBind();
    }


    protected void gvTheGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string CompanyId = gvTheGrid.DataKeys[e.RowIndex].Values[0].ToString();


        string sql = "delete from Company where CompanyId = " + CompanyId;

        int res = Dal.ExecuteNonQuery(sql);

        FillGrid();



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

    protected void txt_TextChanged(object sender, EventArgs e)
    {
        gvDocs.EditIndex = -1;
        FillGrid();

    }
}