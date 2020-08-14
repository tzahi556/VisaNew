using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for SendEmailWeekly
/// </summary>
public class SendEmailWeekly
{

    public ExcelWorksheet firstWorksheet;

    public SendEmailWeekly()
    {

    }

    public SendEmailWeekly(string Subject, string CompaniesId, string Email, string EmailCopy, string EmailHide, string Body, string Id="0")
    {

        //Opening an existing Excel file

        string TemplatePath = HttpContext.Current.Server.MapPath("~/App_Data/");
        FileInfo fi = new FileInfo(TemplatePath + "dgTracking.xlsx");
        DataTable dt = new DataTable();


        int total = 0, totalApproved = 0, totalB1 = 0, totalmult = 0;
        using (ExcelPackage excelPackage = new ExcelPackage(fi))
        {
            firstWorksheet = excelPackage.Workbook.Worksheets[1];

            dt = Dal.ExeSp("GetExpertWithoutFamily", CompaniesId);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                firstWorksheet.Cells[i + 2, 1].Value = (dt.Rows[i]["CompanyName"].ToString()).Replace("(Noble)","");
                firstWorksheet.Cells[i + 2, 2].Value = dt.Rows[i]["Surname"].ToString();
                firstWorksheet.Cells[i + 2, 3].Value = dt.Rows[i]["Name"].ToString();
                firstWorksheet.Cells[i + 2, 4].Value = dt.Rows[i]["Nationality"].ToString();
                firstWorksheet.Cells[i + 2, 5].Value = dt.Rows[i]["Approval Submition Date"].ToString();
                firstWorksheet.Cells[i + 2, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                firstWorksheet.Cells[i + 2, 6].Value = dt.Rows[i]["Approval From Date"].ToString();
                firstWorksheet.Cells[i + 2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                firstWorksheet.Cells[i + 2, 7].Value = dt.Rows[i]["Approval Exp Date"].ToString();
                firstWorksheet.Cells[i + 2, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                firstWorksheet.Cells[i + 2, 8].Value = dt.Rows[i]["Visa/ Invitation Issue date"].ToString();
                firstWorksheet.Cells[i + 2, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                firstWorksheet.Cells[i + 2, 9].Value = dt.Rows[i]["Visa Exp Date"].ToString();
                firstWorksheet.Cells[i + 2, 9].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                firstWorksheet.Cells[i + 2, 10].Value = dt.Rows[i]["Multiple Issue Visa"].ToString();
                firstWorksheet.Cells[i + 2, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            }






            for (int i = 2; i < dt.Rows.Count + 2; i++)
            {
                if (i % 2 == 0)
                {

                    for (int j = 1; j < 11; j++)
                    {
                        firstWorksheet.Cells[i, j].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        firstWorksheet.Cells[i, j].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }

                }

            }


           
            SetBorderToRange(2, 1, dt.Rows.Count + 1, 10, 2);// על הטבלה הגדולה

            int startRowTotal = dt.Rows.Count + 6;

           
            firstWorksheet.Cells[startRowTotal, 2].Value = "Total Number of Experts";
            total = dt.Rows.Count;
            firstWorksheet.Cells[startRowTotal, 3].Value = total;


            firstWorksheet.Cells[startRowTotal + 1, 2].Value = "(STEP 1) Total Applications Approved.";
            totalApproved= dt.Select("[Approval From Date] IS NOT NULL").Count();
            firstWorksheet.Cells[startRowTotal + 1, 3].Value = totalApproved;

            firstWorksheet.Cells[startRowTotal + 2, 2].Value = "(STEP 2) Total B1 Visas issued (out of total  STEP 1).";
            totalB1 = dt.Select("[Visa/ Invitation Issue date] IS NOT NULL").Count();
            firstWorksheet.Cells[startRowTotal + 2, 3].Value = totalB1;

            firstWorksheet.Cells[startRowTotal + 3, 2].Value = "(STEP 3) Processes completed (out of total STEP 2).";
            totalmult = dt.Select("[Multiple Issue Visa] IS NOT NULL").Count();
            firstWorksheet.Cells[startRowTotal + 3, 3].Value = totalmult;



            SetBorderToRange(startRowTotal, 2, startRowTotal + 3, 3, 1);
            SetStyleForTotal(startRowTotal, startRowTotal + 3);


            

            try
            {
                FileInfo fiSend = new FileInfo(TemplatePath + "dgTrackingSend" +Id+ ".xlsx");
                excelPackage.SaveAs(fiSend);
            }
            catch
            {


            }


        }


        Body = ReplaceBodyWithTotal(Body, total, totalApproved , totalB1 , totalmult);

        if (dt.Rows.Count > 0) SendEmailWeeklyModel(Subject, CompaniesId, Email, EmailCopy, EmailHide, Body,Id);


    }

    private string ReplaceBodyWithTotal(string body, int total, int totalApproved, int totalB1, int totalmult)
    {

        string res = "<table cellpadding='0' cellspacing='0'><tr><td style='border:solid 1px black;padding:3px;background:lightGray'>Total Number of Experts</td><td style='border:solid 1px black;padding:3px;background:lightGray;text-align:center;width:50px'>" + total+"</td></tr>"
                  + " <tr><td style='border:solid 1px black;padding:3px;'>(STEP 1) Total Applications Approved.</td><td style='border:solid 1px black;padding:3px;;text-align:center;width:50px'>" + totalApproved + "</td></tr>"
                  + " <tr><td style='border:solid 1px black;padding:3px;background:lightGray'>(STEP 2) Total B1 Visas issued (out of total  STEP 1).</td><td style='border:solid 1px black;padding:3px;background:lightGray;text-align:center;width:50px'>" + totalB1+"</td></tr>"
                  + " <tr><td style='border:solid 1px black;padding:3px;'>(STEP 3) Processes completed (out of total STEP 2).</td><td style='border:solid 1px black;padding:3px;text-align:center;width:50px'>" + totalmult+"</td></tr></table>";


        if(body.Contains("@Total"))
             body = body.Replace("@Total", res);
        else
            body += "<br>" + res;



        return body;



    }

    private void SendEmailWeeklyModel(string Subject, string companiesId, string email, string emailCopy, string emailHide, string body, string Id)
    {
        try
        {

            SmtpClient client = new SmtpClient("smtp.office365.com", 25);
            client.Credentials = new System.Net.NetworkCredential("dglaw@dgtracking.co.il", "But60041");
            client.EnableSsl = true;
            MailMessage actMSG = new MailMessage();
            actMSG.IsBodyHtml = true;

            actMSG.Subject = Subject;
            actMSG.Body = String.Format("{0}", body);
            actMSG.From = new MailAddress("dglaw@dgtracking.co.il");
            //  actMSG.To.Add("yossi@louk.com");
            //  actMSG.To.Add("tzahi556@gmail.com");

            actMSG.To.Add(email);
            if(!string.IsNullOrEmpty(emailCopy)) actMSG.CC.Add(emailCopy);
            if (!string.IsNullOrEmpty(emailHide)) actMSG.Bcc.Add(emailHide);

            string TemplatePath = HttpContext.Current.Server.MapPath("~/App_Data/dgTrackingSend"+Id+".xlsx");
            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(TemplatePath);
            actMSG.Attachments.Add(attachment);

            client.Send(actMSG);
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(ex.Message);
        }



    }

    private void SetStyleForTotal(int RowStart, int RowEnd)
    {

        for (int i = RowStart; i <= RowEnd; i++)
        {
            firstWorksheet.Row(i).Height = 18;
            firstWorksheet.Row(i).Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            firstWorksheet.Cells[i, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            if (i == RowStart || i == RowStart + 2)
            {
                firstWorksheet.Cells[i, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                firstWorksheet.Cells[i, 2].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                firstWorksheet.Cells[i, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                firstWorksheet.Cells[i, 3].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

            }
        }


    }

    private void SetBorderToRange(int LeftX, int LeftY, int RightX, int RightY, int type)
    {

        // type 1 גם בפנים גם בחוץ
        // type 2 רק בחוץ



        if (type == 1)
            for (int x = LeftX; x <= RightX; x++)
            {

                for (int y = LeftY; y <= RightY; y++)
                {
                    firstWorksheet.Cells[x, y].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    firstWorksheet.Cells[x, y].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    firstWorksheet.Cells[x, y].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    firstWorksheet.Cells[x, y].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }

            }


        var FirstTableRange = firstWorksheet.Cells[LeftX, LeftY, RightX, RightY];
        FirstTableRange.Style.Border.BorderAround(ExcelBorderStyle.Medium);


    }
}