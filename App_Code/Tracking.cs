using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;

using System.Web.Services.Protocols;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Text;


/// <summary>
/// Summary description for Visa
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
//[System.Web.Script.Services.ScriptService]
public class Visa : System.Web.Services.WebService
{

    public Visa()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }




    [WebMethod]
    public int GetPlusByTwoNumbers(int Num1, int Num2)
    {
        return Num1 + Num2;
    }

    [WebMethod]
    public string GetCompanies()
    {
        DataTable dt = Dal.ExeSp("GetCompany", "", "", "0", "0");

        //AuthorizedSigner	AuthorizedSignerJob	AuthorizedSignerPassport
        for (int i = 29; i > 12; i--)
        {
            if (i != 27 && dt.Columns[i].ToString() != "AuthorizedSigner"
                && dt.Columns[i].ToString() != "AuthorizedSignerJob"
                && dt.Columns[i].ToString() != "AuthorizedSignerPassport"
                && dt.Columns[i].ToString() != "CompanyWork"

                )
                dt.Columns.RemoveAt(i);
        }


        string strJSON = JSON_DataTable(dt);
        return strJSON;

    }



    [WebMethod]
    public string GetCompanyByCompanyId(string CompanyId)
    {
        DataTable dt = Dal.ExeSp("GetCompany", "", "", CompanyId, "0");

        for (int i = 29; i > 12; i--)
        {
            if (i != 27 && dt.Columns[i].ToString() != "AuthorizedSigner"
                && dt.Columns[i].ToString() != "AuthorizedSignerJob"
                && dt.Columns[i].ToString() != "AuthorizedSignerPassport"
                && dt.Columns[i].ToString() != "CompanyWork"

                )
                dt.Columns.RemoveAt(i);
        }

        string strJSON = JSON_DataTable(dt);
        return strJSON;
    }


    [WebMethod]
    public string GetTest()
    {

        DataTable dt = Dal.ExeSp("GetCompany", "", "", "0", "0");
        return dt.Columns.Count.ToString();
    }


    [WebMethod]
    public string GetExpertsByCompanyId(string CompanyId)
    {
        DataTable dt = Dal.ExeSp("GetExpert", "", "", CompanyId, "0", "0", "0");

        DataTable dtEmpty = new DataTable();

        dt = dt.Select("[Visa/ Invitation Issue date] Is Null And [Active]=1 And diffday >= 0").Any() ? dt.Select("[Visa/ Invitation Issue date] Is Null And [Active]=1 And diffday >= 0").CopyToDataTable() : dtEmpty;

        // dt.Columns.Remove("Comments");
        //  int dtCount = dt.Columns.Count - 2;


        if (dt != dtEmpty)

            for (int i = dt.Columns.Count - 1; i > 18; i--)
            {
                dt.Columns.RemoveAt(i);
            }



        string strJSON = JSON_DataTable(dt);
        return strJSON;

    }

    [WebMethod]
    public string GetExpertByExpertId(string ExpertId)
    {

        DataTable dt = Dal.ExeSp("GetExpert", "", "", "0", ExpertId, "0", "0");

        DataTable dtEmpty = new DataTable();

        dt = dt.Select("ExpertId=" + ExpertId).Any() ? dt.Select("ExpertId=" + ExpertId).CopyToDataTable() : dtEmpty;
        //  dt.Columns.Remove("Comments");

        for (int i = dt.Columns.Count - 1; i > 18; i--)
        {
            dt.Columns.RemoveAt(i);
        }



        string strJSON = JSON_DataTable(dt);
        return strJSON;



    }

    [WebMethod]
    public string GetExpertMissSubmision(string CompanyId)
    {

        DataTable dt = Dal.ExeSp("GetExpertMissSubmision", CompanyId);

        string strJSON = JSON_DataTable(dt);
        return strJSON;


    }


    public string JSON_DataTable(DataTable dt)
    {



        StringBuilder JsonString = new StringBuilder();

        JsonString.Append("[");

        string ColName = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            JsonString.Append("{");
            //JsonString.Append("\"COL\":[ ");

            for (int j = 0; j < dt.Columns.Count; j++)
            {
                if (j < dt.Columns.Count - 1)
                {
                    ColName = dt.Columns[j].ColumnName;

                    JsonString.Append("" + "\"" + ColName + "\":\"" +
                                      dt.Rows[i][j].ToString() + "\",");
                }
                else if (j == dt.Columns.Count - 1)
                {
                    ColName = dt.Columns[j].ColumnName;
                    JsonString.Append("" + "\"" + ColName + "\":\"" +
                                      dt.Rows[i][j].ToString() + "\"");
                }
            }
            /*end Of String*/
            if (i == dt.Rows.Count - 1)
            {
                JsonString.Append("}");
            }
            else
            {
                JsonString.Append("},");
            }
        }
        JsonString.Append("]");
        return JsonString.ToString();
    }


    //**************************** Set **************************

    [WebMethod]
    public bool SetExpertByCompanyId(string CompanyId, string Name, string Surname, string Passport, string Comment, string Phone
        , string Address, string Job, string JobHeb, string JobAbroad, string Nationality, string NationalityHeb)
    {


        if (string.IsNullOrEmpty(CompanyId) || string.IsNullOrEmpty(Passport))
        {
            return false;

        }

        try
        {

            Dal.ExeSp("SetExpert", "-1", Name, Surname, Passport, "42", "",
                     "", "", "", "",
                    "", "", "", Comment, true
                    , Phone, Address, Job, JobHeb, JobAbroad, Nationality, NationalityHeb, ""
                      );


        }
        catch (Exception ex)
        {

            return false;
        }
        return true;

    }


}
