using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;



public partial class Pages_SendEmailWeekly : System.Web.UI.Page
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

              //  btnSearch.Visible = false;

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


    protected void btnSearch_Click(object sender, EventArgs e)
    {

        FillGrid();

    }


    private void FillGrid()
    {

        dtCompany = Dal.ExeSp("GetSetSendEmailWeekly", "1", "0","", "", "", "", "", "", "0", "false","0","");

        gvTheGrid.DataSource = dtCompany;
        gvTheGrid.DataBind();

		if(gvTheGrid.HeaderRow != null)
			gvTheGrid.HeaderRow.TableSection = TableRowSection.TableHeader;


        DataView dataView = new DataView(dtCompany);
        if (!string.IsNullOrEmpty((string)ViewState["FieldSort"]))
        {
            dataView.Sort = ViewState["FieldSort"] + " " + ViewState["SortDir"];
        }
    
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



  

    protected void gvTheGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //gvTheGrid.PageIndex = e.NewPageIndex;
        //gvTheGrid.DataSource = DataTable();
        //gvTheGrid.DataBind();
    }


    protected void gvTheGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string Id = gvTheGrid.DataKeys[e.RowIndex].Values[0].ToString();

        Dal.ExeSp("GetSetSendEmailWeekly", "5", Id, "","", "", "", "", "", "0", "false", "0", "");

        FillGrid();



    }


    //public string GetDate(string p)
    //{
    //    return p;
    //}
   
}