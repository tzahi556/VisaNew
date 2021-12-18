using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using Spire.Xls;
using System.IO;
using System.Data.OleDb;
using System.Globalization;


public partial class Pages_Upload : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Text = "";

        if (!IsPostBack)
        {
            ddlCompany.DataSource = Dal.ExeSp("GetCompany", "", "", "0", "0");
            ddlCompany.DataTextField = "Name";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
        }


    }


    protected void SendClientEmails(object sender, EventArgs e)
    {
        SendEmail sm = new SendEmail(txtDate.Text);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        string shitName = txtSheet.Text;
        //     string filename = FileUpload1.FileName;

        ////    FileUpload1.PostedFile.SaveAs(Server.MapPath("."));//Server.MapPath("../../" + FileUpload1.FileName);
        //try
        //{







        if (string.IsNullOrEmpty(FileUpload1.FileName))
        {
            lblMsg.Text = "File Name is missing!!!";
        }
        else
        {
            try
            {
                string filename = Server.MapPath("~/App_Data/" + FileUpload1.FileName);
                FileUpload1.SaveAs(filename);
                DataTable dt = GetDataTableFromCsv(filename, true);
                BulkCsvToDb(dt);
                lblMsg.Text = "The Upload And Load To DataBase Succeed!!!";
            }
            catch (Exception ex)
            {
                lblMsg.Text = "The Upload And Load To DataBase  Failed!!! , Addional Message: " + ex.Message.ToString();
            }



            //BulkExcelToDb(shitName, filename);
        }


    }

    private void BulkCsvToDb(DataTable dt)
    {
        foreach (DataRow row in dt.Rows)
        {
            //string LastName = row[5].ToString();
            //string Name = row[6].ToString();

            //string Nationality = row[13].ToString();
            //string Occupation = row[14].ToString();
            //string NationalityHeb = row[15].ToString();
            //string OccupationHeb = row[16].ToString();
            //string Phone = row[45].ToString();
            //string Address = row[46].ToString() + " " + row[47].ToString() + " " + row[48].ToString();
            //string Passport = row[19].ToString();
            //string CompanyId = ddlCompany.SelectedValue;

            string LastName = row[0].ToString();
            string Name = row[1].ToString();
            string Passport = row[2].ToString();

            string PassportIssueDate = row[3].ToString();
            string PassportExpDate = row[4].ToString();
            string Occupation = row[5].ToString();
            string OccupationHeb = row[6].ToString();
            string Nationality = row[7].ToString();
            string days45 = row[8].ToString();
            string days90 = row[9].ToString();

            bool is45 = (string.IsNullOrEmpty(days45) || days45 == "0") ? false : true;
            bool is90 = (string.IsNullOrEmpty(days90) || days90 == "0") ? false : true;


            string CompanyId = ddlCompany.SelectedValue;

            Dal.ExeSp("SetExpert", "-1", Name, LastName, Passport, CompanyId, "",
            "", "", "", "",
            "", "", "", "", true, false
           , "", "", "", "", Occupation, OccupationHeb, Nationality, "",
            "", "", "", 0, is45, is90, "", PassportIssueDate, PassportExpDate,""
            );

        }


    }

    static DataTable GetDataTableFromCsv(string path, bool isFirstRowHeader)
    {
        string header = isFirstRowHeader ? "Yes" : "No";

        string pathOnly = Path.GetDirectoryName(path);
        string fileName = Path.GetFileName(path);

        string sql = @"SELECT * FROM [" + fileName + "]";

        using (OleDbConnection connection = new OleDbConnection(
                  @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                  ";Extended Properties=\"Text;HDR=" + header + "\""))
        using (OleDbCommand command = new OleDbCommand(sql, connection))
        using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
        {
            DataTable dataTable = new DataTable();
            dataTable.Locale = CultureInfo.CurrentCulture;
            adapter.Fill(dataTable);
            return dataTable;
        }
    }


    private void BulkExcelToDb(string shitName, string filename)
    {
        try
        {
            //Create a workbook
            Workbook workbook = new Workbook();


            //Load the file
            workbook.LoadFromFile(filename, ExcelVersion.Version2010);
            //   workbook.CalculateAllValue();




            //Initailize worksheet
            Worksheet sheet = workbook.Worksheets[shitName];

            //Export datatable
            DataTable dataTable = sheet.ExportDataTable();

            DataRow dr = dataTable.Rows[0];
            dataTable.Rows.Remove(dr);

            //  WriteToDatabase(dataTable);

            lblMsg.Text = "The Upload And Load To DataBase Succeed!!!";

            //  btnUpload.Enabled = false;

        }
        catch (Exception ex)
        {
            lblMsg.Text = "The Upload And Load To DataBase  Failed!!! , Addional Message: " + ex.Message.ToString();
        }
    }

    //private void WriteToDatabase(DataTable dataTable)
    //{
    //    //foreach (DataRow dr in dataTable.Rows)
    //    //{
    //    //    try
    //    //    {
    //    //        string ss = dr["Multiple entry Visa"].ToString();
    //    //        string sql = "insert into Customers (Multiple entry Visa) Values ('" + ss + "')";
    //    //        Dal.ExecuteNonQuery(sql);
    //    //        //try
    //    //        //{
    //    //        //    string ss = dr["Multiple entry Visa"].ToString();
    //    //        //    if (!string.IsNullOrEmpty(ss))
    //    //        //    {
    //    //        //        DateTime dt = Convert.ToDateTime(ss);
    //    //        //    }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {


    //    //    }
    //    //}
    //    string topCustomerId = "";
    //    string sql = " Select Top 1 CustomerId from Customers Order By CustomerId desc";
    //    DataTable dt = Dal.GetDataTable(sql);
    //    if (dt.Rows.Count > 0)
    //    {
    //        topCustomerId = dt.Rows[0]["CustomerId"].ToString();
    //    }

    //    // get your connection string
    //    string connString = Dal._dbConnectionString;
    //    // connect to SQL
    //    using (SqlConnection connection =
    //            new SqlConnection(connString))
    //    {
    //        // make sure to enable triggers
    //        // more on triggers in next post
    //        SqlBulkCopy bulkCopy =
    //            new SqlBulkCopy
    //            (
    //            connection,
    //            SqlBulkCopyOptions.TableLock |
    //            SqlBulkCopyOptions.FireTriggers |
    //            SqlBulkCopyOptions.UseInternalTransaction,
    //            null
    //            );

    //        // set the destination table name
    //        bulkCopy.DestinationTableName = "Customers";
    //        connection.Open();

    //        // write the data in the "dataTable"
    //        bulkCopy.WriteToServer(dataTable);
    //        connection.Close();

    //        sql = " Delete from Customers Where CustomerId < '" + topCustomerId + "' Or CustomerId = '" + topCustomerId + "'";
    //        Dal.ExecuteNonQuery(sql);


    //    }
    //    // reset
    //    //this.dataTable.Clear();
    //    //recordCount = 0;
    //}






}