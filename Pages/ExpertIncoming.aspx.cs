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




public partial class Pages_ExpertIncoming : System.Web.UI.Page
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



        }


        if (RoleId == "1")
        {
            CompanyId = "0";
        }



        // You only need the following 2 lines of code if you are not 
        // using an ObjectDataSource of SqlDataSource
        if (!Page.IsPostBack)
        {
            DataTable dtCompany = GetOptionsForCompanyDDL();//Dal.ExeSp("GetCompanyForMain", CompanyId);

            ddlCompany.DataSource = dtCompany;
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "CompanyId";

            ddlCompany.DataBind();

            //if (RoleId == "1")
            //{
            //    ddlCompany.SelectedIndex = 2;
            //    //ddlCompany.Value = "14";

            //}
            //else
            //{
                ddlCompany.SelectedIndex = 1;
           // }

          

        }

        FillGrid();

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string ExpertRegId = hdnSelectedArchive.Value;

        if (!string.IsNullOrEmpty(ExpertRegId) && ExpertRegId != "0")
        {

            string sql = "delete from ExpertRegister where ExpertRegId = " + ExpertRegId;
            int res = Dal.ExecuteNonQuery(sql);
            FillGrid();



        }



        //string selected = hdnSelected.Value;
        //if (!string.IsNullOrEmpty(selected) && selected != "0")
        //{

        //    Dal.ExeSp("DeleteMultipleExperts", selected);
        //    FillGrid();
        //}
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


    private void FillGrid()
    {



        //string SessionCompanyId = cookie["CompanyId"].ToString();
        //if (RoleId == "1") SessionCompanyId = "0";


        string SelectedCompanyId = ddlCompany.Value.Replace("Single_", "").Replace("Group_", "");

        //DataTable dt = Dal.ExeSp("GetExpert", txtName.Text, txtSurname.Text, SelectedCompanyId, ExpertId, SessionCompanyId, "0");


        //dtExpert = dt.Select(" [Active]=1 And diffday >= 0").Any() ? dt.Select("[Active]=1 And diffday >= 0","Surname,Name").CopyToDataTable() : dtEmpty;

        //dtExpertArchive = dt.Select("[Active]=0 Or diffday < 0").Any() ? dt.Select("[Active]=0 Or diffday < 0", "Surname,Name").CopyToDataTable() : dtEmpty;


        DataTable dt = Dal.ExeSp("GetExpertRegisterForSys", txtName.Text, txtSurname.Text, SelectedCompanyId, "0");

        DataTable dtExpertReg = dt.Select("[Status]=0").Any() ? dt.Select("[Status]=0", "TimeStampReg Desc").CopyToDataTable() : dtEmpty;
        DataTable dtExpertRegArchive = dt.Select("[Status]=1").Any() ? dt.Select("[Status]=1", "TimeStampReg Desc").CopyToDataTable() : dtEmpty;


        grdIncoming.DataSource = dtExpertReg;
        grdIncoming.DataBind();

        if (grdIncoming.HeaderRow != null)
            grdIncoming.HeaderRow.TableSection = TableRowSection.TableHeader;

        grdIncomingArchive.DataSource = dtExpertRegArchive;
        grdIncomingArchive.DataBind();

        if (grdIncomingArchive.HeaderRow != null)
            grdIncomingArchive.HeaderRow.TableSection = TableRowSection.TableHeader;


    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {


        FillGrid();

    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string ExpertRegId = grdIncoming.DataKeys[e.RowIndex].Values[0].ToString();


        string sql = "delete from ExpertRegister where ExpertRegId = " + ExpertRegId;

        int res = Dal.ExecuteNonQuery(sql);

        FillGrid();



    }


    protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //string diffday = gvTheGrid.DataKeys[e.Row.RowIndex].Values[0].ToString();
            //string ExpertId = gvTheGrid.DataKeys[e.Row.RowIndex].Values[1].ToString();
            string Surname = DataBinder.Eval(e.Row.DataItem, "Surname").ToString();
            string Name = DataBinder.Eval(e.Row.DataItem, "Name").ToString();
            string ExpertRegId = DataBinder.Eval(e.Row.DataItem, "ExpertRegId").ToString();
            string Status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
            string CompanyId = DataBinder.Eval(e.Row.DataItem, "CompanyId").ToString();



            e.Row.Attributes.Add("ondblclick", "OpenCustomerDetails('" + ExpertRegId + "', '" + Surname + "&nbsp;" + Name + "',"+CompanyId+","+ Status + ");");



        }
    }





    public override void VerifyRenderingInServerForm(Control control)
    {

        // Confirms that an HtmlForm control is rendered for the specified ASP.NET server control at run time.

    }




}