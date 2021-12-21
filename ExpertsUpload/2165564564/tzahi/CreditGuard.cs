using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;

public static class CreditGuard
{
    public static string terminal_id = Helper.GetCreditGuardData("terminal_id"); //  
    public static string merchant_id = Helper.GetCreditGuardData("merchant_id");
    public static string user = Helper.GetCreditGuardData("user");
    public static string password = Helper.GetCreditGuardData("password");
    public static string cg_gateway_url = Helper.GetCreditGuardData("cg_gateway_url");


    public static string GetIframeSrc(string amount, string succeedFaildUrl, int IDOrder, string email)
    {


        Random rand = new Random();
        string uniqueID = DateTime.Now.ToString("yyyyddMM") + rand.Next(0, 1000);
        String result = "";
        String poststring = "user=" + user +
                              "&password=" + password +
                              "&int_in=<ashrait>" +
                                 "<request>" +
                                  "<version>2000</version>" +
                                  "<language>HEB</language>" +
                                  "<dateTime/>" +
                                  "<command>doDeal</command>" +
                                  "<doDeal>" +
                                       "<terminalNumber>" + terminal_id + "</terminalNumber>" +
                                       "<mainTerminalNumber/>" +
                                       "<cardNo>CGMPI</cardNo>" +
                                       "<total>" + amount + "</total>" +
                                       "<transactionType>Debit</transactionType>" +
                                       "<creditType>Payments</creditType>" +
                                       "<paymentPageData><useCvv>1</useCvv><useId>1</useId></paymentPageData>" +
                                       //"<creditType>RegularCredit</creditType>" + Payments
                                       "<currency>ILS</currency>" +
                                       "<successUrl>" + succeedFaildUrl + "</successUrl><errorUrl>" + succeedFaildUrl + "</errorUrl>" +
                                       "<transactionCode>Phone</transactionCode>" +
                                       "<authNumber/>" +
                                       "<numberOfPayments>1</numberOfPayments>" +
                                       "<firstPayment/>" +
                                       "<periodicalPayment/>" +
                                       "<validation>TxnSetup</validation>" +
                                       "<dealerNumber/>" +
                                       "<user>Balcar</user>" +
                                       "<mid>" + merchant_id + "</mid>" +
                                       "<uniqueid>" + uniqueID + "</uniqueid>" +
                                       "<mpiValidation>autoComm</mpiValidation>" +
                                       "<email>info@balcar.co.il</email>" +
                                       "<clientIP/>" +
                                       "<customerData>" +
                                        "<userData1>" + IDOrder.ToString() + "</userData1>" +
                                        "<userData2>" + email + "</userData2>" +
                                        "<userData3>Regular</userData3>" +
                                       //"<userData4/>" +
                                       //"<userData5/>" +
                                       //"<userData6/>" +
                                       //"<userData7/>" +
                                       //"<userData8/>" +
                                       //"<userData9/>" +
                                       //"<userData10/>" +
                                       "</customerData>" +
                                  "</doDeal>" +
                                 "</request>" +
                                "</ashrait>";


        StreamWriter myWriter = null;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(cg_gateway_url);
        objRequest.Method = "POST";
        objRequest.ContentLength = poststring.Length;
        objRequest.ContentType = "application/x-www-form-urlencoded";

        try
        {
            myWriter = new StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(poststring);
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
            myWriter.Close();
        }

        HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
        using (StreamReader sr =
           new StreamReader(objResponse.GetResponseStream()))
        {
            result = sr.ReadToEnd();

            // Close and clean up the StreamReader
            sr.Close();
        }


        XmlDocument doc = new XmlDocument();
        doc.LoadXml(result);

        XmlNodeList Nodes = doc.GetElementsByTagName("mpiHostedPageUrl");
        string response = Nodes[0].InnerText;
        return response;

    }

    public static string DoDeal(string TotalPrice, string CardNumber, string OwnerName, string Taz,
                                string Month, string Year, string CVV
        )
    {


        Random rand = new Random();
        string uniqueID = DateTime.Now.ToString("yyyyddMM") + rand.Next(0, 1000);
        String result = "";
        String poststring = "user=" + user +
                              "&password=" + password +
                              "&int_in=<ashrait>" +
                                 "<request>" +
                                  "<version>2000</version>" +
                                  "<language>HEB</language>" +
                                  "<dateTime/>" +
                                  "<command>doDeal</command>" +
                                  "<doDeal>" +
                                "<terminalNumber>" + terminal_id + "</terminalNumber>" +
                                "<authNumber/>" +
                                "<transactionCode>Phone</transactionCode>" +
                                "<transactionType>Debit</transactionType>" +
                                "<total>" + TotalPrice + "</total>" +
                                "<creditType>RegularCredit</creditType>" +
                                "<cardNo>"+ CardNumber + "</cardNo>" +
                                "<cvv>"+CVV+"</cvv>" +
                                "<cardExpiration>"+Month+Year+"</cardExpiration>" +
                                "<validation>AutoComm</validation>" +
                                "<numberOfPayments/>" +
                                "<customerData>" +
                                    "<userData1>tzahi hazan</userData1>" +
                                    "<userData2/>" +
                                    "<userData3/>" +
                                    "<userData4/>" +
                                    "<userData5/>" +
                                "</customerData>" +
                                "<currency>ILS</currency>" +
                                "<firstPayment/>" +
                                "<id>"+Taz+"</id>" +
                                "<periodicalPayment/>" +
                            "</doDeal>" +
                                 "</request>" +
                                "</ashrait>";


        StreamWriter myWriter = null;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(cg_gateway_url);
        objRequest.Method = "POST";
        objRequest.ContentLength = poststring.Length;
        objRequest.ContentType = "application/x-www-form-urlencoded";

        try
        {
            myWriter = new StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(poststring);
        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
            myWriter.Close();
        }

        HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
        using (StreamReader sr =
           new StreamReader(objResponse.GetResponseStream()))
        {
            result = sr.ReadToEnd();

            // Close and clean up the StreamReader
            sr.Close();
        }


        XmlDocument doc = new XmlDocument();
        doc.LoadXml(result);

        //XmlNodeList Nodes = doc.GetElementsByTagName("mpiHostedPageUrl");
        //string response = Nodes[0].InnerText;
        return result;

    }

    //public static string GetIframeSrc()
    //{


    //}

}




