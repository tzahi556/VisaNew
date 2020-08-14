using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.IO;
using System.Web.UI.HtmlControls;



public partial class Pages_Expert : System.Web.UI.Page
{

    private DataTable dtEmpty = new DataTable();
    private DataTable dtExpert;


    public static string ExpertId = "0";
    public static string CompanyId = "0";
    public string Filter = "-1";
    public DataTable dtGrid = null;
    public HttpCookie cookie = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.Request.Cookies["UserData"] != null)
        {
            cookie = HttpContext.Current.Request.Cookies["UserData"];
        }


        string RoleId = cookie["RoleId"].ToString();


        if (Filter == "-1")
        {
            CompanyId = Request.QueryString["CompanyId"];
            if (RoleId == "3")
            {
                string ExpertIdForSe = cookie["ExpertId"].ToString();
                if (!string.IsNullOrEmpty(ExpertIdForSe))
                {
                    ExpertId = ExpertIdForSe;


                }
            }

        }

        if (!IsPostBack)
        {
            FillGrid();
        }
    }




    private void FillGrid()
    {

        //Hashtable hs = new Hashtable();
        //hs.Add("FirstName", txtName.Text);
        //hs.Add("LastName", txtSurname.Text);
        //hs.Add("CompanyId", ddlCompany.SelectedValue);
        //hs.Add("ExpertId", ExpertId);


        string SessionCompanyId = cookie["CompanyId"].ToString();
        if (cookie["RoleId"].ToString() == "1") SessionCompanyId = "0";

        DataTable dt = Dal.ExeSp("GetExpert", "", "", CompanyId, ExpertId, SessionCompanyId, "0");

        dtExpert = dt.Select("[Active]=1 And diffday >= 0").Any() ? dt.Select("[Active]=1 And diffday >= 0").CopyToDataTable() : dtEmpty;

        if (dtExpert.Rows.Count > 0) { 
        DataTable dtInProgress = dtEmpty;
        DataRow[] dtrow = dtExpert.Select("[Approval Submition Date] IS NOT NULL");
        if (dtrow.Any())
        {
            dtInProgress = dtrow.CopyToDataTable();
        }

        DataTable dtStep1 = dtEmpty;
        dtrow = dtExpert.Select("[Approval Exp Date] IS NOT NULL");
        if (dtrow.Any())
        {
            dtStep1 = dtrow.CopyToDataTable();
        }

        DataTable dtStep2 = dtEmpty;
        dtrow = dtExpert.Select("[Approval Exp Date] IS NOT NULL AND [VIIDate] IS NOT NULL");
        if (dtrow.Any())
        {
            dtStep2 = dtrow.CopyToDataTable();
        }


        DataTable dtStep3 = dtEmpty;
        dtrow = dtExpert.Select("[Approval Exp Date] IS NOT NULL AND [Visa Exp Date] IS NOT NULL AND [Multiple entry Visa] IS NOT NULL");
        if (dtrow.Any())
        {
            dtStep3 = dtrow.CopyToDataTable();
        }


        DataTable dtStep4 = dtEmpty;
        dtrow = dtExpert.Select("[diffday] >= 0 And [diffday] < 90");
        if (dtrow.Any())
        {
            dtStep4 = dtrow.CopyToDataTable();
        }


        DataTable dtStep5 = dtEmpty;
        dtrow = dtExpert.Select("[diffday] >= 90 And [diffday] < 120");
        if (dtrow.Any())
        {
            dtStep5 = dtrow.CopyToDataTable();
        }

        DataTable dtStep6 = dtEmpty;
        dtrow = dtExpert.Select("[diffday] >= 120");
        if (dtrow.Any())
        {
            dtStep6 = dtrow.CopyToDataTable();
        }




        dtGrid = dtExpert;
        if (Filter == "1")
        {
            dtGrid = dtInProgress;
        }

        if (Filter == "2")
        {
            dtGrid = dtStep1;
        }

        if (Filter == "3")
        {
            dtGrid = dtStep2;
        }

        if (Filter == "4")
        {
            dtGrid = dtStep3;
        }


        if (Filter == "5")
        {
            dtGrid = dtStep4;
        }

        if (Filter == "6")
        {
            dtGrid = dtStep5;
        }

        if (Filter == "7")
        {
            dtGrid = dtStep6;
        }




        gvTheGrid.DataSource = dtGrid;
        gvTheGrid.DataBind();


        if (gvTheGrid.HeaderRow != null)
            gvTheGrid.HeaderRow.TableSection = TableRowSection.TableHeader;


        SetStatisticPanel(dtExpert.Rows.Count, dtInProgress.Rows.Count, dtStep1.Rows.Count, dtStep2.Rows.Count,
            dtStep3.Rows.Count, dtStep4.Rows.Count, dtStep5.Rows.Count, dtStep6.Rows.Count);

        }
    }

    private void SetStatisticPanel(int Total, int InProgress, int Step1Count, int Step2Count, int Step3Count, int Step4Count, int Step5Count, int Step6Count)
    {
        if (Total > 0)
        {

            sp0.InnerText = Total.ToString();
            sp1.InnerText = InProgress.ToString() + " - " + (InProgress * 100 / Total).ToString() + " % ";
            sp2.InnerText = Step1Count.ToString() + " - " + (Step1Count * 100 / Total).ToString() + " % ";
            sp3.InnerText = Step2Count.ToString() + " - " + (Step2Count * 100 / Total).ToString() + " % ";
            sp4.InnerText = Step3Count.ToString() + " - " + (Step3Count * 100 / Total).ToString() + " % ";
            sp5.InnerText = Step4Count.ToString() + " - " + (Step4Count * 100 / Total).ToString() + " % ";
            sp6.InnerText = Step5Count.ToString() + " - " + (Step5Count * 100 / Total).ToString() + " % ";
            sp7.InnerText = Step6Count.ToString() + " - " + (Step6Count * 100 / Total).ToString() + " % ";

        }
        else
        {
            sp0.InnerText = "0";
            sp1.InnerText = "0";
            sp2.InnerText = "0";
            sp3.InnerText = "0";
            sp4.InnerText = "0";
            sp5.InnerText = "0";
            sp6.InnerText = "0";
            sp7.InnerText = "0";
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
        DataView dataView = new DataView(dtGrid);
        dataView.Sort = e.SortExpression + " " + ViewState["SortDir"];

        gvTheGrid.DataSource = dataView;
        gvTheGrid.DataBind();

        if (gvTheGrid.HeaderRow != null)
            gvTheGrid.HeaderRow.TableSection = TableRowSection.TableHeader;

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





    protected void gvTheGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //string ExpertId = gvTheGrid.DataKeys[e.Row.RowIndex].Values[1].ToString();


            Label lbldiff = (Label)e.Row.FindControl("lbldiff");

            string diffday = gvTheGrid.DataKeys[e.Row.RowIndex].Values[0].ToString();

            lbldiff.Text = (diffday == "5000") ? "" : diffday;
            //if (!string.IsNullOrEmpty(diffday))
            //{
            //    int diff = int.Parse(diffday);



            //    if (diff >= 160 && diff != 5000)
            //    {
            //        flagImg.ImageUrl = "~/App_Themes/Theme1/Images/green_flag.png";
            //        flagImg.ToolTip = diffday;
            //    }
            //    else if (diff < 160 && diff >= 75)
            //    {
            //        flagImg.ImageUrl = "~/App_Themes/Theme1/Images/orange_flag.png";
            //        flagImg.ToolTip = diffday;

            //    }
            //    else if (diff < 75 && diff >= 0)
            //    {
            //        flagImg.ImageUrl = "~/App_Themes/Theme1/Images/red_flag.png";
            //        flagImg.ToolTip = diffday;

            //    }

            //    else if (diff != 5000)
            //    {
            //        flagImg.ImageUrl = "~/App_Themes/Theme1/Images/black_flag.png";
            //        flagImg.ToolTip = diffday;
            //    }
            //}
            //else
            //{
            //    flagImg.Width = 3;
            //    flagImg.Height = 3;
            //}



            // GridViewRow row = e.Row.;

        }
    }



    public override void VerifyRenderingInServerForm(Control control)
    {

        // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.

    }


    public void btnExcel_Click(object sender, EventArgs e)
    {

        // gvTheGrid.Columns[0].Visible = false;

        //PrepareGridViewForExport(gvTheGrid);

        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename=Report.xls");

        Response.Charset = "";

        // If you want the option to open the Excel file without saving than

        // comment out the line below

        // Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.ContentType = "application/Report.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);

        gvTheGrid.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();
    }

    public void btnStatic_Click(object sender, EventArgs e)
    {

        Filter = hdnFilter.Value;

        FillGrid();

    }
}