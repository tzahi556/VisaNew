using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_Users : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // You only need the following 2 lines of code if you are not 
        // using an ObjectDataSource of SqlDataSource
        if (!Page.IsPostBack)
        {

            FillGrid();
            
           
            //   ViewState["SortDir"] = "asc";

        }




        if (GridView1.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute
            GridView1.UseAccessibleHeader = false;

            //This will add the <thead> and <tbody> elements
            GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

            //This adds the <tfoot> element. 
            //Remove if you don't have a footer row
            //GridView1.FooterRow.TableSection = TableRowSection.TableFooter;
        }


    }

    private void FillGrid()
    {
        GridView1.DataSource = DataTable();
        GridView1.DataBind();
        
        if(GridView1.HeaderRow != null)
	        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;


        GridViewRow FooterRow = (GridViewRow)GridView1.FooterRow;

        if (FooterRow != null)
        {


            DropDownList ddlCompany = (DropDownList)FooterRow.FindControl("ddlCompany");

            ddlCompany.DataSource = Dal.ExeSp("GetCompany", "", "", "0", "0");
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();


            DropDownList ddlRoles = (DropDownList)FooterRow.FindControl("ddlRoles");

            ddlRoles.DataSource = Dal.GetDataTableFromSPNoParameter("GetRole");
            ddlRoles.DataTextField = "Name";
            ddlRoles.DataValueField = "RoleId";
            ddlRoles.DataBind();

        }
    }

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {

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
        }


        DataTable datatable = DataTable();
        DataView dataView = new DataView(datatable);
        dataView.Sort = e.SortExpression + " " + ViewState["SortDir"];

        GridView1.DataSource = dataView;
        GridView1.DataBind();
        
        if(GridView1.HeaderRow != null)
	        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        if (e.CommandName == "Insert")
        {
            

            TextBox txtFirstName = (TextBox)GridView1.FooterRow.FindControl("txtFirstName") as TextBox;
            TextBox txtLastName = (TextBox)GridView1.FooterRow.FindControl("txtLastName") as TextBox;

            TextBox txtUserName = (TextBox)GridView1.FooterRow.FindControl("txtUserName") as TextBox;
            TextBox txtPassword = (TextBox)GridView1.FooterRow.FindControl("txtPassword") as TextBox;

            TextBox txtEmail = (TextBox)GridView1.FooterRow.FindControl("txtEmail") as TextBox;

            DropDownList ddlCompany = (DropDownList)GridView1.FooterRow.FindControl("ddlCompany");

            DropDownList ddlRoles = (DropDownList)GridView1.FooterRow.FindControl("ddlRoles");

            CheckBox chActive = (CheckBox)GridView1.FooterRow.FindControl("chActive");
            CheckBox chIsEmail = (CheckBox)GridView1.FooterRow.FindControl("chIsEmail");
            TextBox txtContactManPhone = (TextBox)GridView1.FooterRow.FindControl("txtContactManPhone") as TextBox;


            //DropDownList ddlCompany = (DropDownList)GridView1.FooterRow.FindControl("ddlCompany") as DropDownList;
            //string company = ddlCompany.SelectedValue.ToString();


            string sql = string.Format("insert into Users(FirstName,LastName,UserName,Password,Email,CompanyId, RoleId,Active,IsEmail) "
            + " values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')"
              , txtFirstName.Text, txtLastName.Text, txtUserName.Text, txtPassword.Text, txtEmail.Text, ddlCompany.SelectedValue,
         ddlRoles.SelectedValue, chActive.Checked, chIsEmail.Checked);

            int res = Dal.ExecuteNonQuery(sql);


            FillGrid();

        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ////Set the edit index.
        GridView1.EditIndex = e.NewEditIndex;
        ////Bind data to the GridView control.
        FillGrid();

      
        DropDownList ddlRoles = (DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddlRoles");

        ddlRoles.DataSource = Dal.GetDataTableFromSPNoParameter("GetRole");
        ddlRoles.DataTextField = "Name";
        ddlRoles.DataValueField = "RoleId";
        ddlRoles.DataBind();

        string  RoleId= GridView1.DataKeys[e.NewEditIndex].Values[2].ToString();

        ddlRoles.SelectedValue = RoleId;

        DropDownList ddlCompany = (DropDownList)GridView1.Rows[e.NewEditIndex].FindControl("ddlCompany");

        ddlCompany.DataSource = Dal.ExeSp("GetCompany", "", "", "0", "0");
        ddlCompany.DataTextField = "Name";
        ddlCompany.DataValueField = "CompanyId";
        ddlCompany.DataBind();

        string CompanyId = GridView1.DataKeys[e.NewEditIndex].Values[1].ToString();

        ddlCompany.SelectedValue = CompanyId;


    }


    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        FillGrid();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
         string UserId = GridView1.DataKeys[e.RowIndex].Values[0].ToString();

         TextBox txtFirstName = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtFirstName") as TextBox;
         TextBox txtLastName = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtLastName") as TextBox;

         TextBox txtUserName = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtUserName") as TextBox;
         TextBox txtPassword = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtPassword") as TextBox;

         TextBox txtEmail = (TextBox)GridView1.Rows[e.RowIndex].FindControl("txtEmail") as TextBox;

         DropDownList ddlCompany = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlCompany");

         DropDownList ddlRoles = (DropDownList)GridView1.Rows[e.RowIndex].FindControl("ddlRoles");

         CheckBox chActive = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("chActive");

         CheckBox chIsEmail = (CheckBox)GridView1.Rows[e.RowIndex].FindControl("chIsEmail");



         string sql = string.Format("Update Users set FirstName='{0}',LastName='{1}', UserName='{2}' ,Password='{3}',Email ='{4}' "
             + ",CompanyId='{5}' , RoleId='{6}' ,Active='{7}',IsEmail='{9}' Where UserId='{8}'"
            , txtFirstName.Text, txtLastName.Text, txtUserName.Text, txtPassword.Text, txtEmail.Text, ddlCompany.SelectedValue,
         ddlRoles.SelectedValue, chActive.Checked, UserId, chIsEmail.Checked);

         int res = Dal.ExecuteNonQuery(sql);



        GridView1.EditIndex = -1;

        FillGrid();
    }


    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string UserId = GridView1.DataKeys[e.RowIndex].Values[0].ToString();


        string sql = "delete from Users where UserId = " + UserId;

        int res = Dal.ExecuteNonQuery(sql);

        FillGrid();


    
    }

    protected void GridView1_RowDataBound(object sender,
                         GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    ImageButton l = (ImageButton)e.Row.FindControl("LinkButton1");

            
        //    l.Attributes.Add("onclick", "javascript:return " +
        //    "confirm('Are you sure you want to delete this record " +
        //    DataBinder.Eval(e.Row.DataItem, "Name") + "')");
        //}
    }

    private DataTable DataTable()
    {
        //Create a DataTable instance
        DataTable table = new DataTable();

      //  string sql = "select  Company='asas',Role='xxc' ,* from Users ";



        table = Dal.GetDataTableFromSPNoParameter("dbo.GetUsersTable");


        //if (table.Rows.Count == 0)
        //{

        //    // Define all of the columns you are binding in your GridView
        //    table.Columns.Add("AColumnName");
        //    table.Columns.Add("AColumnName");
        //    table.Columns.Add("AColumnName");
        //    table.Columns.Add("AColumnName");
        //    table.Columns.Add("AColumnName");
            
        //    DataRow dr = table.NewRow();
        //    table.Rows.Add(dr);
        //}




        return table;
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = DataTable();
        GridView1.DataBind();
        
        
        if(GridView1.HeaderRow != null)
	        GridView1.HeaderRow.TableSection = TableRowSection.TableHeader;
    }


}